using GreenwichButchers.Models;
using GreenwichButchers.SystemClasses;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace GreenwichButchers.Pages.Public
{
    public class ShopRCModel : PageModel
    {
        [BindProperty]
        // Property used to display and receive product information
        public List<ProductM> ProductList { get; set; }
        [BindProperty]
        public string _OrderType { get; set; }

        // Category list used to display categories only
        public List<ProductCategoryM> CategoryList { get; private set; }

        // Used to create an order object to set and get order cookie
        public OrderM _Order { get; private set; }



        // Default HTTP GET request which accepts "Product Category" as 
        // parameter
        public async Task OnGet(string ProductCategory, int ID)
        {
            switch (ProductCategory)
            {
                case "Update":
                    ProductCategory = "";
                    if (ID > 0)
                    {
                        _Order = (await new OrderM().GetOrdersAsync("OrderID", ID.ToString()).ConfigureAwait(false))[0];
                        await _Order._Customer.GetOneCustomerAsync(_Order._Customer.CustomerID).ConfigureAwait(false);

                        // Check if order has any item object in it's "ItemList" property
                        if (_Order.ItemList.Count > 0)
                        {
                            // If so, Create a base64 string by passing the "_Order" object *********************
                            // to "ObjectToString64" method of "ObjectConvert" class
                            var Base64 = new ObjectConvert().ObjectToString64(_Order);
                            // Instantiate new object of "CookieEncryptM" class
                            var SecureCookie = new CookieEncryptM();

                            // Append the base64 string value of order as "Basket"
                            // cookie to the response request
                            // this cookie will expire 60 minutes after creation
                            if (SecureCookie.WriteRead(Request.Cookies["Basket"], Base64))
                            {
                                Response.Cookies.Append("Basket"
                               , SecureCookie.HashCookieID
                               , new CookieOptions
                               {
                                   Expires = DateTime.Now.AddMinutes(60)
                               });

                                ViewData["NewHashCookieID"] = SecureCookie.HashCookieID;
                            }
                        }
                    }
                    break;
                case "New":
                    ProductCategory = "";
                    _Order = new OrderM();
                    if (ID > 0)
                    {
                        _Order._Customer = await new CustomerM().GetOneCustomerAsync(ID).ConfigureAwait(false);
                    }
                    var Base64New = new ObjectConvert().ObjectToString64(_Order);
                    // Instantiate new object of "CookieEncryptM" class
                    var SecureCookieNew = new CookieEncryptM();

                    // Append the base64 string value of order as "Basket"
                    // cookie to the response request
                    // this cookie will expire 60 minutes after creation
                    if (SecureCookieNew.WriteRead(Request.Cookies["Basket"], Base64New))
                    {
                        Response.Cookies.Append("Basket"
                       , SecureCookieNew.HashCookieID
                       , new CookieOptions
                       {
                           Expires = DateTime.Now.AddMinutes(60)
                       });

                        ViewData["NewHashCookieID"] = SecureCookieNew.HashCookieID;
                    }
                    break;

            }
            // Call for local "GetCategories" method to set the categories
            // in shop page
            await GetCategories(ProductCategory).ConfigureAwait(false);
            
        }

        // This method is used add the products to the "Basket" cookie
        // to hold the state of the Order
        public async Task OnPostAddToBasketAsync()
        {
            // To hold the value of the selected category when looping 
            // through the product list
            string category = "";

            // Instantiate new object of "CookieEncryptM" class
            var SecureCookie = new CookieEncryptM();

            // Check if the "Basket" cookie exists to add to the
            // existing order.
            if (Request.Cookies["Basket"] != null)
            {

                if (SecureCookie.Read(Request.Cookies["Basket"]))
                {
                    // Set the "_Order" property.
                    // Extract the bulk order state from the cookie by passing the 
                    // cookie to "String64ToObject" method of "ObjectConvert", which
                    // will convert the cookie value to an object value. Then
                    // convert the object to "Order" object   
                    _Order = (OrderM)(new ObjectConvert()
                        .String64ToObject(SecureCookie.Base64Value));
                    _Order.Type = _OrderType;
                }
                else
                {
                    Response.Cookies.Delete("Basket", new CookieOptions { Expires = DateTime.Now });
                    return;
                }
            }
            else // it is a new order 
            {
                // Create a new Order Object
                _Order = new OrderM
                {
                    // Create a new ItemList with in the order
                    ItemList = new List<OrderItemM>(),
                    Type = _OrderType
                };
            }

            // Go through the "ProductList" received from the user
            foreach (var product in ProductList)
            {
                // Hold the category name to be used in the current method
                // This value will be the same since all the product in the list
                // are from one category
                category = product.CategoryName;

                // If the user has chosen more than 0 quantity for the current
                // product
                if (product.ItemQuantity > 0)
                {
                    // Instantiate a new boolean variable with false value
                    bool InCookie = false;

                    // Check if the use is updating the order by going
                    // through the order itemList
                    if (_Order?.ItemList != null)
                    {
                        foreach (var item in _Order.ItemList)
                        {
                            // if the current product is found in the order itemList
                            if (item.ProductName == product.ProductName)
                            {
                                if (_OrderType == "Shop")
                                {
                                    item.ItemPrice = item.RetailPrice;
                                }
                                // Set the "InCookie" variable to true
                                InCookie = true;
                            }
                        }
                    }
                    else
                    {
                        _Order.ItemList = new List<OrderItemM>();
                    }

                    // Check the boolean value of the "InCookie" variable and
                    // if it is true
                    if (InCookie)
                    {
                        // Go through the order ItemList
                        foreach (var item in _Order.ItemList)
                        {
                            // Find the Current product in the list
                            if (item.ProductName == product.ProductName)
                            {
                                // Change the quantity value of the product
                                // in the order
                                item.ItemQuantity = product.ItemQuantity;
                            }
                        }
                    }
                    // Else if the product does not exist in order
                    // add the current product to the Order
                    else if (!InCookie)
                    {
                        // Create a new Order item object 
                        var Item = new OrderItemM
                        {
                            // By using the current product properties
                            ProductName = product.ProductName,
                            ItemQuantity = product.ItemQuantity,
                            RetailPrice = product.RetailPrice,
                            RetailUnit = product.RetailUnit,
                            CategoryName = product.CategoryName
                        };
                        // Add the new item to the order's ListItems property
                        _Order.ItemList.Add(Item);
                    }
                }
            }


            // Call for local "GetCategories" method with the current selected
            // category to hold the sate of the client page 
            await GetCategories(category).ConfigureAwait(false);

            // Check if order has any item object in it's "ItemList" property
            if (_Order.ItemList.Count > 0)
            {
                // If so, Create a base64 string by passing the "_Order" object *********************
                // to "ObjectToString64" method of "ObjectConvert" class
                var Base64 = new ObjectConvert().ObjectToString64(_Order);

                // Append the base64 string value of order as "Basket"
                // cookie to the response request
                // this cookie will expire 60 minutes after creation
                if (SecureCookie.WriteRead(Request.Cookies["Basket"], Base64))
                {
                    Response.Cookies.Append("Basket"
                   , SecureCookie.HashCookieID
                   , new CookieOptions
                   {
                       Expires = DateTime.Now.AddMinutes(60)
                   });

                    ViewData["NewHashCookieID"] = SecureCookie.HashCookieID;
                }
            }
            // Refresh the page by redirecting the page to the currently
            // selected category
            Response.Redirect("/Shop/" + category, true);
        }

        // This private method is used to receive a list of
        // product categories from DB or one which can be passed as parameter
        private async Task GetCategories(string ProductCategory)
        {
            // Set the "CategoryList" to the list received form "GetCategories"
            // method if "ProductCategory" class with "ProductCategory" as
            // parameter
            CategoryList = await new ProductCategoryM().GetCategoriesAsync(ProductCategory).ConfigureAwait(false);

            // If there is only one category loaded
            if (CategoryList.Count == 1)
            {
                // Load the products for that category
                // By calling the "GetProduct" method
                // and pass the "ProductCategory" as
                // parameter
                await GetProducts(ProductCategory).ConfigureAwait(false);
            }
            else // else if its is not one
            {
                // Set product list to an empty list
                ProductList = new List<ProductM>();
            }
        }
        // This private method is used to load products for the parameter
        // category
        private async Task GetProducts(string ProductCategory)
        {
            // Set the "ProductList" by calling the "GetProducts" method
            // of "Product" class and pass the parameter category to it.
            ProductList = await new ProductM().GetProductsAsync(ProductCategory, "Category").ConfigureAwait(false);

            // Check if the "Basket" which holds the order state exists.
            if (Request.Cookies["Basket"] != null)
            {
                var SecureCookie = new CookieEncryptM();
                if (!SecureCookie.Read(Request.Cookies["Basket"]))
                {
                    Response.Cookies.Delete("Basket", new CookieOptions { Expires = DateTime.Now });
                    Response.Redirect("index", false);
                    return;
                }
                // Extract the order object from the cookie
                if (SecureCookie.Base64Value != null)
                {
                    var Basket = (OrderM)(new ObjectConvert().String64ToObject(SecureCookie.Base64Value));
                    _OrderType = Basket.Type;

                    // Go through the ItemList of the order
                    if (Basket?.ItemList != null)
                    {
                        foreach (var OrderItem in Basket?.ItemList?.ToArray())
                        {
                            // go through the product list received from the database
                            foreach (var product in ProductList)
                            {
                                // If product exists in the order 
                                if (OrderItem.ProductName == product.ProductName)
                                {
                                    // Restore the item quantity value from the cookie 
                                    product.ItemQuantity = OrderItem.ItemQuantity;
                                }
                            }
                        }
                    }

                }
                else
                {
                    Response.Cookies.Delete("Basket", new CookieOptions { Expires = DateTime.Now });
                    Response.Redirect("index", false);
                    return;
                }
            }
        }
    }
}