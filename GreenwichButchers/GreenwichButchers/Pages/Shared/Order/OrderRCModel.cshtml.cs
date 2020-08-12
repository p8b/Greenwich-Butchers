using GreenwichButchers.Models;
using GreenwichButchers.SystemClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GreenwichButchers.Pages.Shared
{
    [BindProperties]
    public class OrderRCModel : PageModel
    {
        public OrderM _Order { get; set; }
        public AddressM _Address { get; set; }
        public List<OrderM> _OrderList { get; set; }
        public List<SupplierM> _SuppliersList { get; set; }
        public List<StockLocationM> _StockLocationList { get; set; }

        public string _SearchValue { get; set; }
        public string _FilterStatus { get; set; } = "All";
        public string _FilterType { get; set; } = "All";
        public int _ShowItemPerPage { get; set; } = 5;
        public int _ShowPageNum { get; set; } = 1;
        public ListError _ListError { get; set; }

        public async Task OnGetAsync(string Panel, int IDValue)
        {
            var SecureCookie = new CookieEncryptM();
            // Extract the login state from the cookie and convert it from Base64String to
            // Login object
            var user = new LoginCheck(Request.Cookies["Cookie"], false);

            if (SecureCookie.Read(Request.Cookies["Basket"] ?? ""))
            {
                // Extract the order state from the cookie by passing the
                // cookie to "String64ToObject" method of "ObjectConvert", which
                // will convert the cookie value to an object value. Then
                // convert the object to "Order" object
                _Order = (OrderM)(new ObjectConvert().String64ToObject(SecureCookie.Base64Value));

                switch (CUser.UserRole)
                {
                    case "Customer":
                        if (_Order?.OrderID > 0)
                        {
                            _Address = (await new AddressM().GetAddressesAsync(_Order.DeliveryAddressID, "AddressID").ConfigureAwait(false))[0];
                        }
                        // Set the page title
                        ViewData["OrderPageTitle"] = "Edit My Order";
                        ViewData["ViewOrder"] = true;
                        break;
                    case "Staff":
                    case "Manager":
                        if(_Order?.DeliveryAddressID > 0)
                        {
                            _Address = (await new AddressM().GetAddressesAsync(_Order.DeliveryAddressID, "AddressID").ConfigureAwait(false))[0];
                        }
                        if (_Order?.OrderID > 0)
                        {

                            // Set the page title
                            ViewData["OrderPageTitle"] = "Edit Customer Order";
                        } else
                        {
                            // Set the page title
                            ViewData["OrderPageTitle"] = "New Customer Order";
                        }
                        ViewData["ViewOrder"] = true;
                        ViewData["disabledBack"] = "disabled";
                        return;
                }
            }

            switch (CUser.UserRole)
            {
                case "Customer": // if the user is Customer
                    // If Order ID is more than 0. (Existing Order)
                    if (_Order?.OrderID > 0)
                    {
                        _Order._Customer = await new CustomerM().GetOneCustomerAsync(CUser.UserID).ConfigureAwait(false);
                        ViewData["OrderPageTitle"] = "My Order";
                    }
                    // if Order ID is 0. (New Order)
                    else if (_Order?.OrderID == 0 && Panel == "Checkout")
                    {
                        _SearchValue = CUser.UserID.ToString();
                        _Order._Customer = await new CustomerM().GetOneCustomerAsync(CUser.UserID).ConfigureAwait(false);
                        // Update Bulk Basket cookie to hold the Order state
                        var Base64 = new ObjectConvert().ObjectToString64(_Order);
                        // Append the base64 string value of order as "Basket"
                        // cookie to the response request
                        if (SecureCookie.WriteRead(Request.Cookies["Basket"], Base64))
                        {
                            Response.Cookies.Append("Basket"
                           , SecureCookie.HashCookieID);

                            ViewData["TotalItems"] = _Order.ItemList.Count;
                        }
                        foreach (var item in _Order?._Customer?.AddressList)
                        {
                            if (item.AddressID == _Order.DeliveryAddressID)
                            {
                                _Address = item;
                            }
                        }
                        ViewData["OrderPageTitle"] = "Checkout";
                    }
                    else // Else Show all the Customer Orders
                    {
                        _SearchValue = CUser.UserID.ToString();
                        await OnPostSearchAsync().ConfigureAwait(false);
                        ViewData["OrderPageTitle"] = "My Orders";
                    }
                    break;

                case "Staff":
                case "Manager":
                    _StockLocationList = await new StockLocationM().GetStockLocationsAsync().ConfigureAwait(false);
                    if (_Order?.DeliveryAddressID > 0)
                    {
                        _Address = (await new AddressM().GetAddressesAsync(_Order.DeliveryAddressID, "AddressID").ConfigureAwait(false))[0];
                        // Set the page title
                        ViewData["ViewOrder"] = true;
                        ViewData["disabledBack"] = "disabled";
                    }
                    
                    ViewData["OrderPageTitle"] = "Customer Order";
                    break;

                default:// User is not known
                    // if basket is empty
                    if (Request.Cookies["Basket"] == null)
                    {
                        // redirect the user to the product page
                        Response.Redirect("/Shop", false);
                        return;
                    }

                    // for unknown user set the customer attribute of the current
                    // order to null
                    if (_Order?._Customer != null)
                    {
                        SecureCookie.Delete(Request.Cookies["Basket"]);
                        Response.Cookies.Delete("Basket",
                            new CookieOptions
                            { Expires = new DateTime(1991, 6, 3, 22, 15, 0) }
                            );
                        Response.Redirect("/Shop", false);
                        return;
                    }
                    break;
            }
            // This switch statement is used as panel control
            switch (Panel)
            {
                // If the user is trying to access the Checkout panel
                case "Checkout":
                    // Show their order
                    ViewData["ViewOrder"] = true;
                    // Set the page title
                    ViewData["OrderPageTitle"] = "Checkout";
                    // if basket is empty
                    if (Request.Cookies["Basket"] == null && _Order == null)
                    {
                        // redirect the user to the product page
                        Response.Redirect("/Shop", false);
                        return;
                    }
                    break;
                // If the user is trying to view an order
                case "View":
                    switch (CUser.UserRole)
                    {
                        case "Customer":
                        case "Staff":
                        case "Manager":
                            // Pass the Order ID to
                            await OnPostSelectOrderAsync(IDValue).ConfigureAwait(false);
                            // Show their order
                            ViewData["ViewOrder"] = true;
                            break;
                    }
                    break;

                case "Back":
                    ViewData["ViewOrder"] = false;
                    ReadSearchTempData();
                    if (_SearchValue == "ShowAll")
                    {
                        await OnPostShowAllAsync().ConfigureAwait(false);
                    }
                    else
                    {
                        await OnPostSearchAsync().ConfigureAwait(false);
                    }
                    break;

                default:
                    ViewData["ViewOrder"] = false;
                    if (CUser.UserRole == "Customer" && Request.Cookies["Basket"] != null)
                    {
                        Response.Redirect("/Order/Checkout", false);
                    }
                    break;
            }
            if (_Order?.ItemList?.Count > 0)
            {
                if (_Order?.Type == "Shop")
                {
                    foreach (var item in _Order.ItemList)
                    {
                        item.ItemPrice = (await new ProductM().GetProductsAsync(item.ProductName, "ProductName").ConfigureAwait(false))[0].RetailPrice;
                        item.RetailPrice = item.ItemPrice;
                    }
                }
                else if (_Order?.Type == "Bulk" && _Order._Payment == null)
                {
                    _Order._Payment = new PaymentM
                    {
                        TotalPrice = 0
                    };
                    foreach (var item in _Order.ItemList)
                    {
                        item.ItemPrice = 0;
                        item.RetailPrice = (await new ProductM().GetProductsAsync(item.ProductName, "ProductName").ConfigureAwait(false))[0].RetailPrice;
                    }
                }
            }
            if (_Order?.OrderID > 0)
            {
                await _Order.GetLinkedEmployeesAsync().ConfigureAwait(false);
            }
        }

        public async Task OnPostSearchAsync()
        {
            if (Int32.TryParse(_SearchValue, out int IntergerTest))
            {
                _OrderList = await new OrderM().GetOrdersAsync("Off", "0").ConfigureAwait(false);
                foreach (var item in _OrderList.ToArray())
                {
                    if (item._Customer.CustomerID != Convert.ToInt32(_SearchValue) && item.OrderID != Convert.ToInt32(_SearchValue))
                    {
                        _OrderList.Remove(item);
                    }
                }
            }
            FilterOrders();

            if (_OrderList?.Count == 0)
            {
                ViewData["NoRecordsFound"] = true;
            }
            WriteSearchTempData();
        }
        // This method is used to show all the orders on the database
        public async Task OnPostShowAllAsync()
        {
            _OrderList = await new OrderM().GetOrdersAsync("Off", "0").ConfigureAwait(false);

            FilterOrders();
            _SearchValue = "";
            if (_OrderList.Count == 0)
            {
                ViewData["NoRecordsFound"] = true;
            }

            _SearchValue = "ShowAll";
            WriteSearchTempData();
        }

        public async Task OnPostSubmitOrderAsync(string Panel)
        {
            // If request is from "View" Panel which handles existing orders
            if (Panel == "View")
            {
                // Create a copy of the order details received from the user
                var UpdatedOrder = _Order;
                try
                {
                    // Set the "_Order" to the vales received from the database
                    _Order = (await new OrderM().GetOrdersAsync("OrderID", _Order?.OrderID.ToString()).ConfigureAwait(false))[0];
                }
                // if "ArgumentOutOfRangeException" is thrown, set the "_Order" to a new instance
                catch (ArgumentOutOfRangeException e) { Debug.WriteLine(e); _Order = new OrderM(); }

                // attach the new information to the current instance of "_Order" property
                // by using the information received from the user
                _Order.ItemList = UpdatedOrder.ItemList;
                _Order.Note = UpdatedOrder.Note;
                _Order.DeliveryDate = UpdatedOrder.DeliveryDate;
                _Order.Type = UpdatedOrder.Type;

                // If the user change the status of the order from cancel to anything else
                if (_Order?.Status == "Cancelled" && UpdatedOrder?.Status != "Cancelled")
                {
                    // Change the status to pending only
                    _Order.Status = "Pending";
                }
                // else if the user changes the status of the order from "Pending" to
                // "Complete" and the order type is "Bulk"
                else if (_Order?.Status == "Pending" && UpdatedOrder?.Status == "Complete"
                            && _Order?.Type == "Bulk")
                {
                    // Change the order status to "Pending"
                    _Order.Status = "Pending";
                    // Show error message asking the user to confirm the order quotes 
                    ViewData["FailedMsg"] = "Please Confirm order quotes";
                    //  redirect the user to the order page
                    ViewData["FailedRedirect"] = "href=/Order/View/" + _Order?.OrderID;
                    // stop the code from continuing 
                    return;
                }
                else // Default
                {
                    // Change the status to the user specified status
                    _Order.Status = UpdatedOrder.Status;
                    try
                    {
                        // try to get the payment information from the database
                        _Order._Payment = (await new PaymentM().GetAsync("OrderID",_Order.OrderID).ConfigureAwait(false))[0];
                    }
                    // if no payment record exists then "ArgumentOutOfRangeException" is thrown and cough appropriately 
                    catch (ArgumentOutOfRangeException e) { Debug.WriteLine(e); }
                }

                // if the order has a payment by checking the "FKOrderID" property
                if (_Order?._Payment?.FKOrderID > 1)
                {
                    if (CUser.UserRole == "Staff" || CUser.UserRole == "Manager")
                    {
                        await _Order.LinkEmployeeAsync(CUser.UserID).ConfigureAwait(false);
                    }
                    // If so, Update the profit margin of the payment with the
                    // info received from the user
                    _Order._Payment.ProfitMargin = UpdatedOrder._Payment.ProfitMargin;
                    // If the order status is not "Cancelled"
                    if (_Order?.Status != "Cancelled")
                    {
                        // call the "AddAsync" method of the "_Payment" property 
                        // which will also update the payment record
                        await _Order._Payment.AddUpdateAsync().ConfigureAwait(false);
                    }
                    else // else the order status is "Cancelled"
                    {
                        // Delete the payment details of the order
                        await _Order._Payment.DeleteAsync().ConfigureAwait(false);
                    }
                }
                // no payment records exist for the current order and if the order type is "Shop"
                // and the order status is "Completed"
                else if (_Order?.Type == "Shop" && _Order?.Status == "Complete")
                {
                    switch (CUser.UserRole)
                    {
                        case "Staff":
                        case "Manager":
                            _Order.Status = "Complete";
                            await _Order.LinkEmployeeAsync(CUser.UserID).ConfigureAwait(false);
                            break;
                        default:
                            _Order.Status = "Pending";
                            break;
                    }
                    _Order._Payment = new PaymentM
                    {
                        ProfitMargin = 100,
                        FKOrderID = _Order.OrderID,
                        PaymentDate = DateTime.Now.ToShortDateString()

                    };
                    foreach (var item in _Order?.ItemList)
                    {

                        _Order._Payment.TotalPrice += Math.Round(item.ItemPrice * item.ItemQuantity, 2);
                    }
                    await _Order._Payment.AddUpdateAsync().ConfigureAwait(false);
                }
                // If the "UpdateOrderAsync" method if the current "_Order" instance
                // returns true and the order id exists
                if (_Order?.OrderID > 0 && await _Order.UpdateOrderAsync().ConfigureAwait(false))
                {
                    // Loop through the order's "ItemsList"
                    foreach (var item in _Order.ItemList)
                    {
                        // if the current item quantity is zero
                        if (item.ItemQuantity == 0)
                        {
                            // remove the current item
                            await new OrderItemM().DeleteOrderItemAsync(item.ItemID).ConfigureAwait(false);
                        }
                        else // else
                        {
                            // Update the current item
                            await item.UpdateOrderItemAsync(_Order.OrderID).ConfigureAwait(false);
                        }
                    }
                    // Show success massage 
                    ViewData["SuccessMsg"] = "Order Updated";
                    // redirect the user to the same page in order to refresh the page
                    ViewData["SuccessRedirect"] = "href=/Order/View/" + _Order?.OrderID;
                    // Stop the code from continuing
                    return;
                }
            }

            // If the request is from "Checkout" Panel and the "Basket" cookie exists.
            // the following code will handle the new orders and adding new items
            // the order. it is also able to edit existing order details
            if (Request.Cookies["Basket"] != null && Panel == "Checkout")
            {
                // Create a new instance of "CookieEncryptM" to handle the user's cookie
                var SecureCookie = new CookieEncryptM();
                // call for the "Read" method of the "SecureCookie" and pass the "Basket" as
                // parameter. if false is returned by the method
                if (!SecureCookie.Read(Request.Cookies["Basket"]))
                {
                    // delete the basket cookie since the cookie is not known
                    Response.Cookies.Delete("Basket", new CookieOptions { Expires = DateTime.Now });
                    // redirect the user to the index page.
                    Response.Redirect("/Index", false);
                    // Stop the code from continuing
                    return;
                }
                // Else if the cookie is read

                // Create a new instance of the "_Order" received from the use
                var UpdatedOrder = _Order;

                // Pass the string base to the "String64ToObject" method of "ObjectConvert"
                // to convert the string base 64 to an object which is then converted to "OrderM"
                _Order = (OrderM)new ObjectConvert().String64ToObject(SecureCookie.Base64Value);

                // If the order type is Shop
                if (_Order.Type == "Shop")
                {
                    // Set the total price to 0
                    _Order._Payment = new PaymentM
                    {
                        TotalPrice = 0
                    };
                    // Go through the order's "ItemList"
                    foreach (var item in _Order?.ItemList)
                    {
                        // Set the item price to its retail price
                        item.ItemPrice = item.RetailPrice;
                        // Add the current item price times by its quantity to the total price of the order
                        _Order._Payment.TotalPrice += (item.ItemPrice * item.ItemQuantity);
                    }
                }

                // Set the rest of the order properties received from the user
                _Order.ItemList = UpdatedOrder.ItemList;
                _Order.Note = UpdatedOrder.Note;
                _Order.DeliveryDate = UpdatedOrder.DeliveryDate;
                _Order.Type = UpdatedOrder.Type;

                // Go through the item list
                foreach (var item in _Order.ItemList.ToArray())
                {
                    // if the item quantity is set to 0
                    if (item.ItemQuantity == 0)
                    {
                        if (_Order?.OrderID > 0)
                        {
                            await new OrderItemM().DeleteOrderItemAsync(item.ItemID).ConfigureAwait(false);
                        }
                        // Remove the current item from the Order Item List
                        _Order.ItemList.Remove(item);
                    }
                }

                // Make sure if it is a new order the status property is set to "Pending"
                if (_Order.OrderID == 0)
                {
                    _Order.Status = "Pending";

                }
                // if Order exists
                else if (_Order?.OrderID > 0)
                {
                    // Set the SuccessMsg
                    ViewData["SuccessMsg"] = "Order Updated.";
                }

                // Add the current instance of the order to the database.
                // The "AddOrderAsync" is also capable of handing existing order
                // If true is returned
                if (await _Order.AddOrderAsync().ConfigureAwait(false))
                {
                    // Delete basket secure cookie from the database
                    SecureCookie.Delete(Request.Cookies["Basket"]);
                    // Set the success message is not set above
                    if (ViewData["SuccessMsg"] == null)
                    {
                        // Set the Success message
                        ViewData["SuccessMsg"] = "Thank you for your order.";
                    }
                    // refresh the page after the user click on the message
                    ViewData["SuccessRedirect"] = "href=/Order/View/" + _Order?.OrderID;
                    // delete the basket cookie from the client's device
                    Response.Cookies.Delete("Basket",
                        new CookieOptions
                        { Expires = new DateTime(1991, 6, 3, 22, 15, 0) }
                        );
                    // stop the code from continuing
                    return;
                }
            }

            // if code is not stopped until now then the action must have failed
            // notify the user
            ViewData["FailedMsg"] = "Order failed. Please try again later.";
            // redirect them to index page after clicking on the message
            ViewData["SuccessRedirect"] = "href=/Index";
            // set the success message to null
            ViewData["SuccessMsg"] = null;
        }
        public async Task OnPostSubmitShopOrderAsync(string Panel, string StockLocation)
        {
            if (StockLocation != null && (CUser.UserRole == "Staff" || CUser.UserRole == "Manager"))
            {

                _ListError = new ListError { Status = true};
                foreach(var item in _Order?.ItemList)
                {
                    var Error = new CustomError
                    {
                        ErrNumber = item.ItemID,
                        ItemErrorMsg = item.ProductName
                    };
                    item.StockInfo = await new StockM().GetStockInfoAsync("Product", item.ProductName).ConfigureAwait(false);
                    // used to check if the current item exists within the stock
                    var itemInStock = false;
                    // Loop through the stock locations
                    foreach (var loc in item?.StockInfo)
                    {
                        if (loc.LocationName == StockLocation && loc.StockQuantity >= item.ItemQuantity)
                        {
                            itemInStock = true;
                        }
                    }

                    if (!itemInStock)
                    {
                        _ListError.Status = false;
                        _ListError.ErrorList.Add(Error);
                    }
                }

                if (_ListError.Status)
                {
                    foreach (var item in _Order?.ItemList)
                    {
                        // Loop through the stock locations
                        foreach (var loc in item?.StockInfo)
                        {
                            if (loc.LocationName == StockLocation && loc.StockQuantity >= item.ItemQuantity)
                            {
                                loc.StockQuantity -= item.ItemQuantity;
                                await loc.AddUpdateStockInfoAsync(loc.ProductName, loc.StockQuantity, loc.LocationName).ConfigureAwait(false);
                            }
                        }
                    }
                } else
                {

                    ViewData["NoStock"] = true;
                    ViewData["ViewOrder"] = true;
                    await OnGetAsync(Panel, _Order.OrderID).ConfigureAwait(false);
                    return;
                }
            }
            _Order.Status = "Complete";

            await OnPostSubmitOrderAsync(Panel).ConfigureAwait(false);
        }
        public async Task OnPostDeleteOrderAsync(string Orderid)
        {
            _Order = (await new OrderM().GetOrdersAsync("OrderID", Orderid).ConfigureAwait(false))[0];
            if (_Order.OrderID > 0 && await _Order.DeleteOrderAsync().ConfigureAwait(false))
            {
                ViewData["SuccessMsg"] = "Deleted.";
                ViewData["SuccessRedirect"] = "href=/Order";
            }
            else
            {

                ViewData["FailedMsg"] = "Deleted.";
                ViewData["ViewOrder"] = true;
            }
        }
        public async Task OnPostAddQuoteAsync(string PName)
        {
            if (_Order?.OrderID > 0 && PName != "")
            {
                await _Order.LinkEmployeeAsync(CUser.UserID).ConfigureAwait(false);

                foreach (var item in _Order.ItemList)
                {
                    if (item.ProductName == PName)
                    {
                        await item._ItemQuote.AddUpdateOrderQuote(item.ItemID).ConfigureAwait(false);
                    }
                }
                ViewData["ViewOrderQuote"] = "active show";
                await OnGetAsync("View", _Order.OrderID).ConfigureAwait(false);
            }
            else
            {
                ViewData["FailedMsg"] = "Failed to add quote.";
                @ViewData["FailedRedirect"] = "href=/Order";
            }
            ViewData["ViewOrder"] = true;
        }
        public async Task OnPostConfirmQuotesAsync()
        {
            // Extract the login state from the cookie and convert it from Base64String to
            // Login object
            var user = new LoginCheck(Request.Cookies["Cookie"], false);
            if (CUser.UserRole == "Staff" || CUser.UserRole == "Manager")
            {
                await _Order.LinkEmployeeAsync(CUser.UserID).ConfigureAwait(false);
            }
            switch (CUser.UserRole)
            {
                case "Customer":
                case "Staff":
                case "Manager":
                    if (_Order?.ItemList[0]?._ItemQuote?.QuoteID > 0)
                    {
                        var NewOrderInfo = _Order;
                        _Order = (await new OrderM().GetOrdersAsync("OrderID", _Order.OrderID.ToString()).ConfigureAwait(false))[0];
                        _Order.ItemList = NewOrderInfo.ItemList;

                        var payment = new PaymentM()
                        {
                            FKOrderID = _Order.OrderID,
                            ProfitMargin = 10,
                            PaymentDate = DateTime.Now.ToString()
                        };

                        foreach (var item in _Order.ItemList)
                        {
                            item._QuoteList = await new OrderItemQuoteM().GetOrderItemQuoteAsync("ItemID", item.ItemID).ConfigureAwait(false);
                            foreach (var quote in item._QuoteList)
                            {
                                if (quote.QuoteID == item._ItemQuote.QuoteID)
                                {
                                    item._ItemQuote = quote;
                                } else
                                {
                                    await quote.DeleteOrderQuoteAsync().ConfigureAwait(false);
                                }
                            }
                            item._ItemQuote = (await new OrderItemQuoteM().GetOrderItemQuoteAsync("QuoteID",item._ItemQuote.QuoteID).ConfigureAwait(false))[0];
                            item.ItemPrice = item._ItemQuote.QuotePrice;
                            payment.TotalPrice += item.ItemPrice * item.ItemQuantity;
                            await item.UpdateOrderItemAsync(_Order.OrderID).ConfigureAwait(false);
                        }

                        _Order._Payment = payment;
                        _Order.Status = "Complete";

                        if (await _Order.UpdateOrderAsync().ConfigureAwait(false))
                        {
                            var test = await _Order._Payment.AddUpdateAsync().ConfigureAwait(false);
                            ViewData["SuccessMsg"] = "Order Confirmed";
                            ViewData["SuccessRedirect"] = "href=/Order/View/" + _Order.OrderID;
                        }
                        else
                        {
                            ViewData["FailedMsg"] = "Order Confirmation Failed";
                            ViewData["ViewOrderQuote"] = "active show";
                        }
                    }
                    break;
                default:
                    Response.Redirect("/Login", false);
                    break;
            }

        }
        public async Task OnPostUpdateBasketAsync(string Panel)
        {
            if (Panel == "Checkout")
            {
                var SecureCookie = new CookieEncryptM();

                if (!SecureCookie.Read(Request.Cookies["Basket"] ?? ""))
                {
                    Response.Cookies.Delete("Basket", new CookieOptions { Expires = DateTime.Now });
                    Response.Redirect("/Shop", false);
                    return;
                }
                var UpdatedOrder = _Order;
                // Extract the bulk order state from the cookie by passing the
                // cookie to "String64ToObject" method of "ObjectConvert", which
                // will convert the cookie value to an obj
                _Order = (OrderM)(new ObjectConvert().String64ToObject(SecureCookie.Base64Value));
                _Order.ItemList = UpdatedOrder.ItemList;
                _Order.Note = UpdatedOrder.Note;
                _Order.DeliveryDate = UpdatedOrder.DeliveryDate;
                _Order.Type = UpdatedOrder.Type;
                // Go through the item list
                foreach (var item in _Order?.ItemList.ToArray())
                {
                    if (_Order?.Type == "Shop")
                    {
                        item.ItemPrice = item.RetailPrice;
                    }
                    // if the item quantity is set to 0
                    if (item.ItemQuantity == 0)
                    {
                        if (_Order?.OrderID > 0)
                        {
                            await new OrderItemM().DeleteOrderItemAsync(item.ItemID).ConfigureAwait(false);
                        }
                        // Remove the current item from the Order Item List
                        _Order.ItemList.Remove(item);
                    }
                }

                // If there are no items in the basket
                if (_Order.ItemList.Count <= 0)
                {
                    // Call the OnPostDeleteBasket local method to
                    // remove the Basket cookie and redirect to the shop page
                    OnPostDeleteBasket();

                    // Stop the code from continuing
                    return;
                }

                // Update the "Basket" cookie
                // Create a base64 string by passing the "_Order" object
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
                    ViewData["TotalItems"] = _Order?.ItemList?.Count;
                }

                // if the order id is more than 0 then update the order on the
                // database
                if (_Order?.OrderID > 0)
                {
                    await _Order.UpdateOrderAsync().ConfigureAwait(false);
                }

                ViewData["SuccessMsg"] = "Update Successful";
                ViewData["SuccessRedirect"] = "href=/Order/Checkout";
            }
        }
        public void OnPostDeleteBasket()
        {
            new CookieEncryptM().Delete(Request.Cookies["Basket"]);
            // Delete the "Basket" cookie by calling the "Delete" method
            // and passing the Cookie name and a cookie option to expire the cookie
            Response.Cookies.Delete("Basket"
                , new CookieOptions
                {
                    Expires = new DateTime(1991, 6, 3, 22, 15, 0)
                });
            if (_Order?.OrderID > 0)
            {
                Response.Redirect("/Order/View/" + _Order?.OrderID, false);
                return;
            }
            // redirect the user to the shop page
            Response.Redirect("/Shop", false);
        }
        public async Task OnPostSelectThisAddAsync(string Panel, int SelectedAddID)
        {
            if (Panel == "View")
            {
                if (CUser.UserRole == "Staff" || CUser.UserRole == "Manager")
                {
                    await _Order.LinkEmployeeAsync(CUser.UserID).ConfigureAwait(false);
                }
                _Order = (await new OrderM().GetOrdersAsync("OrderID", _Order?.OrderID.ToString()).ConfigureAwait(false))[0];
                _Order.DeliveryAddressID = SelectedAddID;
                await _Order.UpdateOrderAsync().ConfigureAwait(false);
                Response.Redirect("/Order/View/" + _Order?.OrderID, false);
                return;
            }
            if (Panel == "Checkout")
            {
                var SecureCookie = new CookieEncryptM();

                if (!SecureCookie.Read(Request.Cookies["Basket"] ?? ""))
                {
                    Response.Cookies.Delete("Basket", new CookieOptions { Expires = DateTime.Now });
                    Response.Redirect("/Index", false);
                    return;
                }
                // Extract the bulk order state from the cookie by passing the
                // cookie to "String64ToObject" method of "ObjectConvert", which
                // will convert the cookie value to an obj
                _Order = (OrderM)(new ObjectConvert().String64ToObject(SecureCookie.Base64Value));

                _Order._Customer = await _Order._Customer.GetOneCustomerAsync(_Order._Customer.CustomerID).ConfigureAwait(false);

                //Loop through the customer's Addresses
                foreach (var item in _Order._Customer.AddressList)
                {
                    // If the Address id of selected Address is
                    // the same as parameter (id) of the current method
                    if (item.AddressID == SelectedAddID)
                    {
                        // set the Model.Address property to the info of the selected Address
                        _Address = item;
                    }
                }
                // Set the deliver Address id of the Order to the selected Address
                _Order.DeliveryAddressID = SelectedAddID;

                // Update Bulk Basket cookie to hold the Order state
                var Base64 = new ObjectConvert().ObjectToString64(_Order);
                // Append the base64 string value of order as "Basket"
                // cookie to the response request
                if (SecureCookie.WriteRead(Request.Cookies["Basket"], Base64))
                {
                    Response.Cookies.Append("Basket"
                   , SecureCookie.HashCookieID);

                    ViewData["TotalItems"] = _Order.ItemList.Count;
                }
                Response.Redirect("/Order/Checkout", false);
            }
        }

        // This method is used to select a delivery address from the available
        // addresses from the drop down menu


        // This method is used to update the item quantity of the ordered product
        public async Task OnPostPageControllerAsync(int ShowPageNum)
        {
            // keep the value locally to remember its value
            var ShowItemPerPage = _ShowItemPerPage;
            if (TempData["TempSearchValuesOrders"] != null)
            {
                ReadSearchTempData();
                if (ShowPageNum > 0)
                {
                    _ShowPageNum = ShowPageNum;
                }

                await OnGetAsync("PageController", 0).ConfigureAwait(false);
                // if the value of the "_ShowPageNum" is more than total pages for
                // the current order list or if it is 0
                if (_ShowPageNum > Convert.ToInt32(Math.Ceiling((_OrderList.Count + 0.0) / _ShowItemPerPage)))
                {
                    // Set the "_ShowPageNum" to the max page number allowed for the
                    // current order list
                    _ShowPageNum = Convert.ToInt32(Math.Ceiling((_OrderList.Count + 0.0) / _ShowItemPerPage));
                }
                if (_ShowPageNum == 0)
                {
                    _ShowPageNum = 1;
                }
            }
            // Extract the login state from the cookie and convert it from Base64String to
            // Login object
            var user = new LoginCheck(Request.Cookies["Cookie"], false);

            switch (CUser.UserRole)
            {
                case "Staff":
                case "Manager":
                    _OrderList = await new OrderM().GetOrdersAsync("Off", "0").ConfigureAwait(false);
                    break;

                case "Customer":
                    _OrderList = await new OrderM().GetOrdersAsync("CustomerID", CUser.UserID.ToString()).ConfigureAwait(false);
                    break;

                default:
                    return;
            }
            if (ShowPageNum != 0)
            {
                _ShowPageNum = ShowPageNum;
            }
            // restore the value
            _ShowItemPerPage = ShowItemPerPage;
            WriteSearchTempData();
        }
        public async Task OnPostSelectOrderAsync(int SelectIDValue)
        {
            if (SelectIDValue > 0)
            {
                TempData.Keep("TempSearchValuesOrders");
                _Order = (await new OrderM().GetOrdersAsync("OrderID", SelectIDValue.ToString()).ConfigureAwait(false))[0];
                if (_Order?.DeliveryAddressID > 0)
                {
                    try
                    {
                        _Address = (await new AddressM().GetAddressesAsync(_Order.DeliveryAddressID, "AddressID").ConfigureAwait(false))[0];
                    } catch (Exception e)
                    {
                        Debug.WriteLine(e.Message);
                        _Address = new AddressM
                        {
                            AddressName = "Adderess was deleted"
                        };
                    }
                }
                var payments = await new PaymentM().GetAsync("OrderID", _Order.OrderID).ConfigureAwait(false);
                if (payments.Count > 0)
                {
                    foreach (var pay in payments)
                    {
                        if (pay.FKOrderID == _Order.OrderID)
                        {
                            _Order._Payment = pay;
                        }
                    }
                }
                ViewData["ViewOrder"] = true;
            }
            else
            {
                ViewData["FailedMsg"] = "Failed to load the order.";
                ViewData["FailedRedirect"] = "href=/Order";
            }
        }

        private void FilterOrders()
        {
            if (_FilterStatus != "All")
            {
                foreach (var item in _OrderList.ToArray())
                {
                    if (item.Status != _FilterStatus)
                    {
                        _OrderList.Remove(item);
                    }
                }
            }
            if (_FilterType != "All")
            {
                foreach (var item in _OrderList.ToArray())
                {
                    if (item.Type != _FilterType)
                    {
                        _OrderList.Remove(item);
                    }
                }
            }
        }

        private void ReadSearchTempData()
        {
            var TempSearchData = (TempData["TempSearchValuesOrders"] ?? "@@ @@ @@ @@").ToString().Split("@@");
            if (Convert.ToInt32(TempSearchData[4]) == 0)
            {
                return;
            }
            _SearchValue = TempSearchData[0] ?? "";
            _FilterStatus = TempSearchData[1] ?? "All";
            _FilterType = TempSearchData[2] ?? "All";
            _ShowItemPerPage = Convert.ToInt32(TempSearchData[3] ?? "5");
            _ShowPageNum = Convert.ToInt32(TempSearchData[4] ?? "1");

            ViewData["OrderPageTitle"] = "Customer Orders";

            TempData.Remove("TempSearchValuesOrders");
        }

        private void WriteSearchTempData()
        {
            // TempData used to hold search results temporarily
            TempData["TempSearchValuesOrders"] = _SearchValue + "@@" + _FilterStatus + "@@" + _FilterType + "@@" + _ShowItemPerPage + "@@" + _ShowPageNum;

            if (_SearchValue == "ShowAll")
            {
                _SearchValue = "";
            }
            ViewData["OrderPageTitle"] = "Customer Orders";
            ViewData["ViewOrder"] = false;
        }
    }
}