CREATE TABLE [dbo].[LinkEmployeeOrders](
	[Employee_Id] [int] NOT NULL,
	[Order_Id] [int] NOT NULL,
	[Initial_Employee] [bit] NOT NULL,
 CONSTRAINT [PK_Link_EmployeeOrders] PRIMARY KEY CLUSTERED 
(
	[Employee_Id] ASC,
	[Order_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblAddresses]    Script Date: 17/04/2019 16:05:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblAddresses](
	[Address_Id] [int] IDENTITY(1,1) NOT NULL,
	[Customer_Id] [int] NOT NULL,
	[address_Name] [varchar](100) NULL,
	[address_1stLine] [varchar](200) NOT NULL,
	[address_2ndLine] [varchar](200) NULL,
	[address_City] [varchar](100) NOT NULL,
	[address_PostCode] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tblAddresses] PRIMARY KEY CLUSTERED 
(
	[Address_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblCookieEncrypt]    Script Date: 17/04/2019 16:05:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCookieEncrypt](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Hash_Id] [varchar](max) NULL,
	[StringBase64Value] [varchar](max) NOT NULL,
 CONSTRAINT [PK_tblCookieEncrypt] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblCustomers]    Script Date: 17/04/2019 16:05:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCustomers](
	[Customer_Id] [int] IDENTITY(1,1) NOT NULL,
	[Person_Id] [int] NOT NULL,
	[customer_CompanyName] [varchar](200) NULL,
 CONSTRAINT [PK_Cusotmer] PRIMARY KEY CLUSTERED 
(
	[Customer_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblEmployee]    Script Date: 17/04/2019 16:05:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblEmployee](
	[Employee_Id] [int] IDENTITY(1,1) NOT NULL,
	[Person_Id] [int] NOT NULL,
	[Position_Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tblStaff] PRIMARY KEY CLUSTERED 
(
	[Employee_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblMailingList]    Script Date: 17/04/2019 16:05:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblMailingList](
	[MailingList_Email] [varchar](200) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblOrderItems]    Script Date: 17/04/2019 16:05:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblOrderItems](
	[Item_Id] [int] IDENTITY(1,1) NOT NULL,
	[Order_Id] [int] NOT NULL,
	[Product_Name] [varchar](300) NULL,
	[Item_Price] [decimal](8, 2) NOT NULL,
	[Item_Quantity] [decimal](6, 2) NOT NULL,
 CONSTRAINT [PK_LinkOrderProduct] PRIMARY KEY CLUSTERED 
(
	[Item_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblOrderQuotes]    Script Date: 17/04/2019 16:05:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblOrderQuotes](
	[Quote_Id] [int] IDENTITY(1,1) NOT NULL,
	[Item_Id] [int] NOT NULL,
	[Supplier_Id] [int] NOT NULL,
	[quote_Price] [decimal](8, 2) NULL,
	[quote_DeliveryDate] [varchar](50) NULL,
 CONSTRAINT [PK_tblOrderQuotes_1] PRIMARY KEY CLUSTERED 
(
	[Quote_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblOrders]    Script Date: 17/04/2019 16:05:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblOrders](
	[Order_Id] [int] IDENTITY(1,1) NOT NULL,
	[Customer_Id] [int] NULL,
	[Address_Id] [int] NULL,
	[order_Date] [varchar](10) NOT NULL,
	[order_DeliveryDate] [varchar](10) NOT NULL,
	[order_Note] [varchar](500) NULL,
	[order_Status] [varchar](50) NOT NULL,
	[order_Type] [nchar](30) NOT NULL,
 CONSTRAINT [PK_tblOrders] PRIMARY KEY CLUSTERED 
(
	[Order_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblPayment]    Script Date: 17/04/2019 16:05:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblPayment](
	[Payment_Id] [int] IDENTITY(1,1) NOT NULL,
	[Order_Id] [int] NULL,
	[payment_Total] [decimal](9, 2) NULL,
	[payment_Supplier] [decimal](9, 2) NULL,
	[payment_Profit] [decimal](9, 2) NULL,
	[payment_ProfitMargin] [int] NULL,
	[payment_Date] [varchar](10) NULL,
 CONSTRAINT [PK_tblPayment_1] PRIMARY KEY CLUSTERED 
(
	[Payment_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblPersons]    Script Date: 17/04/2019 16:05:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblPersons](
	[Person_Id] [int] IDENTITY(1,1) NOT NULL,
	[person_Title] [varchar](50) NOT NULL,
	[person_Name] [varchar](100) NOT NULL,
	[person_Surname] [varchar](100) NOT NULL,
	[person_Tel] [varchar](15) NOT NULL,
	[person_Email] [varchar](200) NOT NULL,
	[person_Password] [nvarchar](max) NULL,
 CONSTRAINT [PK_tblPersons] PRIMARY KEY CLUSTERED 
(
	[Person_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblPosition]    Script Date: 17/04/2019 16:05:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblPosition](
	[Position_Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tblPosition] PRIMARY KEY CLUSTERED 
(
	[Position_Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblProductCategories]    Script Date: 17/04/2019 16:05:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblProductCategories](
	[ProductCategory] [varchar](80) NOT NULL,
	[ProductType] [varchar](80) NOT NULL,
	[ProductCategory_imgPath] [varchar](500) NULL,
 CONSTRAINT [PK_tblProductCategories_1] PRIMARY KEY CLUSTERED 
(
	[ProductCategory] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblProducts]    Script Date: 17/04/2019 16:05:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblProducts](
	[Product_Name] [varchar](300) NOT NULL,
	[productCategory] [varchar](80) NOT NULL,
	[product_RetailUnit] [varchar](50) NOT NULL,
	[product_RetailPrice] [decimal](6, 2) NOT NULL,
 CONSTRAINT [PK_tblProducts_1] PRIMARY KEY CLUSTERED 
(
	[Product_Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblStock]    Script Date: 17/04/2019 16:05:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblStock](
	[Product_Name] [varchar](300) NOT NULL,
	[StockLocation_Name] [varchar](50) NOT NULL,
	[stock_Quantity] [decimal](6, 2) NULL,
 CONSTRAINT [PK_tblStock_1] PRIMARY KEY CLUSTERED 
(
	[StockLocation_Name] ASC,
	[Product_Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblStockLocation]    Script Date: 17/04/2019 16:05:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblStockLocation](
	[StockLocation_Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tbl_StockLocation_1] PRIMARY KEY CLUSTERED 
(
	[StockLocation_Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblSupplierProductCategories]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblSupplierProductCategories](
	[Supplier_Id] [int] NOT NULL,
	[ProductCategory] [varchar](80) NOT NULL,
 CONSTRAINT [PK_tblSupplierProductCategories_1] PRIMARY KEY CLUSTERED 
(
	[Supplier_Id] ASC,
	[ProductCategory] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblSuppliers]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblSuppliers](
	[Supplier_Id] [int] IDENTITY(1,1) NOT NULL,
	[supplier_Company] [varchar](200) NOT NULL,
	[supplier_FullName] [varchar](250) NOT NULL,
	[supplier_Tel] [varchar](15) NOT NULL,
	[supplier_Email] [varchar](200) NOT NULL,
	[supplier_Description] [varchar](1000) NOT NULL,
 CONSTRAINT [PK_tblSuppliers] PRIMARY KEY CLUSTERED 
(
	[Supplier_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[LinkEmployeeOrders] ([Employee_Id], [Order_Id], [Initial_Employee]) VALUES (5, 55, 1)
GO
INSERT [dbo].[LinkEmployeeOrders] ([Employee_Id], [Order_Id], [Initial_Employee]) VALUES (5, 56, 0)
GO
INSERT [dbo].[LinkEmployeeOrders] ([Employee_Id], [Order_Id], [Initial_Employee]) VALUES (5, 57, 1)
GO
INSERT [dbo].[LinkEmployeeOrders] ([Employee_Id], [Order_Id], [Initial_Employee]) VALUES (37, 56, 1)
GO
SET IDENTITY_INSERT [dbo].[tblAddresses] ON 
GO
INSERT [dbo].[tblAddresses] ([Address_Id], [Customer_Id], [address_Name], [address_1stLine], [address_2ndLine], [address_City], [address_PostCode]) VALUES (28, 60, N'Default', N'45 Guild Street', NULL, N'LONDON', N'EC1V 7EB')
GO
INSERT [dbo].[tblAddresses] ([Address_Id], [Customer_Id], [address_Name], [address_1stLine], [address_2ndLine], [address_City], [address_PostCode]) VALUES (29, 60, N'Kitchen', N'52 Crown Street', N'Ing Lane', N'LONDON', N'SW1H 8BA')
GO
INSERT [dbo].[tblAddresses] ([Address_Id], [Customer_Id], [address_Name], [address_1stLine], [address_2ndLine], [address_City], [address_PostCode]) VALUES (44, 66, N'Default', N'28  Lairg Road', N'NEWBYDougherty', N'Dougherty', N'LA2 4RP')
GO
SET IDENTITY_INSERT [dbo].[tblAddresses] OFF
GO
SET IDENTITY_INSERT [dbo].[tblCustomers] ON 
GO
INSERT [dbo].[tblCustomers] ([Customer_Id], [Person_Id], [customer_CompanyName]) VALUES (60, 97, N'Emily kitchens')
GO
INSERT [dbo].[tblCustomers] ([Customer_Id], [Person_Id], [customer_CompanyName]) VALUES (66, 122, N'Good Guys')
GO
SET IDENTITY_INSERT [dbo].[tblCustomers] OFF
GO
SET IDENTITY_INSERT [dbo].[tblEmployee] ON 
GO
INSERT [dbo].[tblEmployee] ([Employee_Id], [Person_Id], [Position_Name]) VALUES (5, 10, N'Manager')
GO
INSERT [dbo].[tblEmployee] ([Employee_Id], [Person_Id], [Position_Name]) VALUES (37, 120, N'Staff')
GO
SET IDENTITY_INSERT [dbo].[tblEmployee] OFF
GO
SET IDENTITY_INSERT [dbo].[tblOrderItems] ON 
GO
INSERT [dbo].[tblOrderItems] ([Item_Id], [Order_Id], [Product_Name], [Item_Price], [Item_Quantity]) VALUES (132, 55, N'Lamb Leg', CAST(19.99 AS Decimal(8, 2)), CAST(30.00 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblOrderItems] ([Item_Id], [Order_Id], [Product_Name], [Item_Price], [Item_Quantity]) VALUES (133, 56, N'aaaa', CAST(12.00 AS Decimal(8, 2)), CAST(12.00 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblOrderItems] ([Item_Id], [Order_Id], [Product_Name], [Item_Price], [Item_Quantity]) VALUES (134, 56, N'aaaaa', CAST(1.00 AS Decimal(8, 2)), CAST(2.00 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblOrderItems] ([Item_Id], [Order_Id], [Product_Name], [Item_Price], [Item_Quantity]) VALUES (135, 56, N'Fillet', CAST(2.00 AS Decimal(8, 2)), CAST(5.00 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblOrderItems] ([Item_Id], [Order_Id], [Product_Name], [Item_Price], [Item_Quantity]) VALUES (136, 56, N'Ribeye', CAST(3.00 AS Decimal(8, 2)), CAST(4.00 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblOrderItems] ([Item_Id], [Order_Id], [Product_Name], [Item_Price], [Item_Quantity]) VALUES (137, 57, N'Lamb Leg', CAST(19.99 AS Decimal(8, 2)), CAST(50.00 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblOrderItems] ([Item_Id], [Order_Id], [Product_Name], [Item_Price], [Item_Quantity]) VALUES (138, 57, N'Lamb Neck Skewers', CAST(19.99 AS Decimal(8, 2)), CAST(50.00 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblOrderItems] ([Item_Id], [Order_Id], [Product_Name], [Item_Price], [Item_Quantity]) VALUES (139, 57, N'Lamb Rack', CAST(29.99 AS Decimal(8, 2)), CAST(50.00 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblOrderItems] ([Item_Id], [Order_Id], [Product_Name], [Item_Price], [Item_Quantity]) VALUES (140, 57, N'lb', CAST(2134.00 AS Decimal(8, 2)), CAST(50.00 AS Decimal(6, 2)))
GO
SET IDENTITY_INSERT [dbo].[tblOrderItems] OFF
GO
SET IDENTITY_INSERT [dbo].[tblOrderQuotes] ON 
GO
INSERT [dbo].[tblOrderQuotes] ([Quote_Id], [Item_Id], [Supplier_Id], [quote_Price], [quote_DeliveryDate]) VALUES (1, 133, 12, CAST(12.00 AS Decimal(8, 2)), N'2019-03-25')
GO
INSERT [dbo].[tblOrderQuotes] ([Quote_Id], [Item_Id], [Supplier_Id], [quote_Price], [quote_DeliveryDate]) VALUES (2, 134, 9, CAST(1.00 AS Decimal(8, 2)), N'2019-03-25')
GO
INSERT [dbo].[tblOrderQuotes] ([Quote_Id], [Item_Id], [Supplier_Id], [quote_Price], [quote_DeliveryDate]) VALUES (3, 135, 9, CAST(2.00 AS Decimal(8, 2)), N'2019-03-25')
GO
INSERT [dbo].[tblOrderQuotes] ([Quote_Id], [Item_Id], [Supplier_Id], [quote_Price], [quote_DeliveryDate]) VALUES (4, 136, 9, CAST(3.00 AS Decimal(8, 2)), N'2019-03-25')
GO
INSERT [dbo].[tblOrderQuotes] ([Quote_Id], [Item_Id], [Supplier_Id], [quote_Price], [quote_DeliveryDate]) VALUES (8, 137, 12, CAST(8.00 AS Decimal(8, 2)), N'2019-03-26')
GO
SET IDENTITY_INSERT [dbo].[tblOrderQuotes] OFF
GO
SET IDENTITY_INSERT [dbo].[tblOrders] ON 
GO
INSERT [dbo].[tblOrders] ([Order_Id], [Customer_Id], [Address_Id], [order_Date], [order_DeliveryDate], [order_Note], [order_Status], [order_Type]) VALUES (55, 60, 29, N'12/04/2019', N'2019-03-24', NULL, N'Pending', N'Shop                          ')
GO
INSERT [dbo].[tblOrders] ([Order_Id], [Customer_Id], [Address_Id], [order_Date], [order_DeliveryDate], [order_Note], [order_Status], [order_Type]) VALUES (56, 66, 44, N'12/04/2019', N'2019-03-25', N'', N'Complete', N'Bulk                          ')
GO
INSERT [dbo].[tblOrders] ([Order_Id], [Customer_Id], [Address_Id], [order_Date], [order_DeliveryDate], [order_Note], [order_Status], [order_Type]) VALUES (57, 66, 44, N'13/04/2019', N'2019-03-26', N'Test', N'Pending', N'Shop                          ')
GO
SET IDENTITY_INSERT [dbo].[tblOrders] OFF
GO
SET IDENTITY_INSERT [dbo].[tblPayment] ON 
GO
INSERT [dbo].[tblPayment] ([Payment_Id], [Order_Id], [payment_Total], [payment_Supplier], [payment_Profit], [payment_ProfitMargin], [payment_Date]) VALUES (1, 55, CAST(599.70 AS Decimal(9, 2)), CAST(0.00 AS Decimal(9, 2)), CAST(599.70 AS Decimal(9, 2)), 100, N'25/03/2019')
GO
INSERT [dbo].[tblPayment] ([Payment_Id], [Order_Id], [payment_Total], [payment_Supplier], [payment_Profit], [payment_ProfitMargin], [payment_Date]) VALUES (2, 56, CAST(168.00 AS Decimal(9, 2)), CAST(151.20 AS Decimal(9, 2)), CAST(16.80 AS Decimal(9, 2)), 10, N'12/04/2019')
GO
SET IDENTITY_INSERT [dbo].[tblPayment] OFF
GO
SET IDENTITY_INSERT [dbo].[tblPersons] ON 
GO
INSERT [dbo].[tblPersons] ([Person_Id], [person_Title], [person_Name], [person_Surname], [person_Tel], [person_Email], [person_Password]) VALUES (10, N'Mr', N'Majid', N'Joveini', N'0123456985236', N'Majid@Admin.com', N'騳윁쇘跌앻姤ꂤ졢꧋懰ầ쁀炚ⱓ⡌ᚾ䶯䳖ಉ簣䕐鶋쀅ꑑ艹肊삘 ㅿ')
GO
INSERT [dbo].[tblPersons] ([Person_Id], [person_Title], [person_Name], [person_Surname], [person_Tel], [person_Email], [person_Password]) VALUES (97, N'Miss', N'Emily', N'Mahmood', N'078 1500 7743', N'is756co8pw@iffymedia.com', N'঱믳Ⓖ롎䄤纑淐譡࢐ঢ়뺳᯽ݞ䰹橰릋놀磗奞鬄�፟꼦⹚톦ﴃ줇蕓꯿갌蚼')
GO
INSERT [dbo].[tblPersons] ([Person_Id], [person_Title], [person_Name], [person_Surname], [person_Tel], [person_Email], [person_Password]) VALUES (120, N'Mr   ', N'Martin                                                                                              ', N'Safari                                                                                              ', N'01725698324    ', N'Martin@gmail.com', N'঱믳Ⓖ롎䄤纑淐譡࢐ঢ়뺳᯽ݞ䰹橰릋놀磗奞鬄�፟꼦⹚톦ﴃ줇蕓꯿갌蚼')
GO
INSERT [dbo].[tblPersons] ([Person_Id], [person_Title], [person_Name], [person_Surname], [person_Tel], [person_Email], [person_Password]) VALUES (122, N'Mr', N'Tyler', N'Willis', N'079 0640 1117', N'asasd@Asd', N'঱믳Ⓖ롎䄤纑淐譡࢐ঢ়뺳᯽ݞ䰹橰릋놀磗奞鬄�፟꼦⹚톦ﴃ줇蕓꯿갌蚼')
GO
INSERT [dbo].[tblPersons] ([Person_Id], [person_Title], [person_Name], [person_Surname], [person_Tel], [person_Email], [person_Password]) VALUES (125, N'sdf', N'sdfg', N'sdfs', N'23w4', N'asda', N'asd')
GO
INSERT [dbo].[tblPersons] ([Person_Id], [person_Title], [person_Name], [person_Surname], [person_Tel], [person_Email], [person_Password]) VALUES (139, N'sdf', N'sdfg', N'sdfs', N'23w4', N'asdda', N'asd')
GO
SET IDENTITY_INSERT [dbo].[tblPersons] OFF
GO
INSERT [dbo].[tblPosition] ([Position_Name]) VALUES (N'Manager')
GO
INSERT [dbo].[tblPosition] ([Position_Name]) VALUES (N'Staff')
GO
INSERT [dbo].[tblProductCategories] ([ProductCategory], [ProductType], [ProductCategory_imgPath]) VALUES (N'Beef', N'Meat', N'\Images\Category\Beef14.jpg')
GO
INSERT [dbo].[tblProductCategories] ([ProductCategory], [ProductType], [ProductCategory_imgPath]) VALUES (N'Chicken', N'Meat', N'\Images\Category\Chicken76.jpg')
GO
INSERT [dbo].[tblProductCategories] ([ProductCategory], [ProductType], [ProductCategory_imgPath]) VALUES (N'Lamb', N'Meat', N'\Images\Category\Lambs.jpg')
GO
INSERT [dbo].[tblProductCategories] ([ProductCategory], [ProductType], [ProductCategory_imgPath]) VALUES (N'Pork', N'Meat', N'\Images\Category\Pork.jpg')
GO
INSERT [dbo].[tblProducts] ([Product_Name], [productCategory], [product_RetailUnit], [product_RetailPrice]) VALUES (N'aaaa', N'Beef', N'asd', CAST(21.00 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblProducts] ([Product_Name], [productCategory], [product_RetailUnit], [product_RetailPrice]) VALUES (N'aaaaa', N'Beef', N'asd', CAST(21.00 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblProducts] ([Product_Name], [productCategory], [product_RetailUnit], [product_RetailPrice]) VALUES (N'Chicken Breast', N'Chicken', N'Kg', CAST(3.99 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblProducts] ([Product_Name], [productCategory], [product_RetailUnit], [product_RetailPrice]) VALUES (N'Chicken Leg', N'Chicken', N'Kg', CAST(4.99 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblProducts] ([Product_Name], [productCategory], [product_RetailUnit], [product_RetailPrice]) VALUES (N'Fillet', N'Beef', N'Kg', CAST(59.99 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblProducts] ([Product_Name], [productCategory], [product_RetailUnit], [product_RetailPrice]) VALUES (N'Lamb Leg', N'Lamb', N'Kg', CAST(19.99 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblProducts] ([Product_Name], [productCategory], [product_RetailUnit], [product_RetailPrice]) VALUES (N'Lamb Neck Skewers', N'Lamb', N'Kg', CAST(19.99 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblProducts] ([Product_Name], [productCategory], [product_RetailUnit], [product_RetailPrice]) VALUES (N'Lamb Rack', N'Lamb', N'Kg', CAST(29.99 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblProducts] ([Product_Name], [productCategory], [product_RetailUnit], [product_RetailPrice]) VALUES (N'lb', N'Lamb', N'asd', CAST(2134.00 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblProducts] ([Product_Name], [productCategory], [product_RetailUnit], [product_RetailPrice]) VALUES (N'Pork Belly', N'Pork', N'Kg', CAST(9.99 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblProducts] ([Product_Name], [productCategory], [product_RetailUnit], [product_RetailPrice]) VALUES (N'Pork Chops', N'Pork', N'Kg', CAST(11.99 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblProducts] ([Product_Name], [productCategory], [product_RetailUnit], [product_RetailPrice]) VALUES (N'Ribeye', N'Beef', N'Kg', CAST(38.99 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblProducts] ([Product_Name], [productCategory], [product_RetailUnit], [product_RetailPrice]) VALUES (N'Sirlion', N'Beef', N'Kg', CAST(36.99 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblStock] ([Product_Name], [StockLocation_Name], [stock_Quantity]) VALUES (N'aaaa', N'Shop', CAST(100.00 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblStock] ([Product_Name], [StockLocation_Name], [stock_Quantity]) VALUES (N'aaaaa', N'Shop', CAST(0.00 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblStock] ([Product_Name], [StockLocation_Name], [stock_Quantity]) VALUES (N'Chicken Breast', N'Shop', CAST(0.00 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblStock] ([Product_Name], [StockLocation_Name], [stock_Quantity]) VALUES (N'Chicken Leg', N'Shop', CAST(0.00 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblStock] ([Product_Name], [StockLocation_Name], [stock_Quantity]) VALUES (N'Fillet', N'Shop', CAST(0.00 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblStock] ([Product_Name], [StockLocation_Name], [stock_Quantity]) VALUES (N'Lamb Leg', N'Shop', CAST(555.00 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblStock] ([Product_Name], [StockLocation_Name], [stock_Quantity]) VALUES (N'Lamb Neck Skewers', N'Shop', CAST(0.00 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblStock] ([Product_Name], [StockLocation_Name], [stock_Quantity]) VALUES (N'Lamb Rack', N'Shop', CAST(0.00 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblStock] ([Product_Name], [StockLocation_Name], [stock_Quantity]) VALUES (N'lb', N'Shop', CAST(0.00 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblStock] ([Product_Name], [StockLocation_Name], [stock_Quantity]) VALUES (N'Pork Belly', N'Shop', CAST(0.00 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblStock] ([Product_Name], [StockLocation_Name], [stock_Quantity]) VALUES (N'Pork Chops', N'Shop', CAST(0.00 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblStock] ([Product_Name], [StockLocation_Name], [stock_Quantity]) VALUES (N'Ribeye', N'Shop', CAST(0.00 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblStock] ([Product_Name], [StockLocation_Name], [stock_Quantity]) VALUES (N'Sirlion', N'Shop', CAST(0.00 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblStock] ([Product_Name], [StockLocation_Name], [stock_Quantity]) VALUES (N'aaaa', N'Warehouse', CAST(200.00 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblStock] ([Product_Name], [StockLocation_Name], [stock_Quantity]) VALUES (N'aaaaa', N'Warehouse', CAST(0.00 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblStock] ([Product_Name], [StockLocation_Name], [stock_Quantity]) VALUES (N'Chicken Breast', N'Warehouse', CAST(0.00 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblStock] ([Product_Name], [StockLocation_Name], [stock_Quantity]) VALUES (N'Chicken Leg', N'Warehouse', CAST(0.00 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblStock] ([Product_Name], [StockLocation_Name], [stock_Quantity]) VALUES (N'Fillet', N'Warehouse', CAST(0.00 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblStock] ([Product_Name], [StockLocation_Name], [stock_Quantity]) VALUES (N'Lamb Leg', N'Warehouse', CAST(0.00 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblStock] ([Product_Name], [StockLocation_Name], [stock_Quantity]) VALUES (N'Lamb Neck Skewers', N'Warehouse', CAST(0.00 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblStock] ([Product_Name], [StockLocation_Name], [stock_Quantity]) VALUES (N'Lamb Rack', N'Warehouse', CAST(0.00 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblStock] ([Product_Name], [StockLocation_Name], [stock_Quantity]) VALUES (N'lb', N'Warehouse', CAST(0.00 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblStock] ([Product_Name], [StockLocation_Name], [stock_Quantity]) VALUES (N'Pork Belly', N'Warehouse', CAST(0.00 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblStock] ([Product_Name], [StockLocation_Name], [stock_Quantity]) VALUES (N'Pork Chops', N'Warehouse', CAST(0.00 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblStock] ([Product_Name], [StockLocation_Name], [stock_Quantity]) VALUES (N'Ribeye', N'Warehouse', CAST(0.00 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblStock] ([Product_Name], [StockLocation_Name], [stock_Quantity]) VALUES (N'Sirlion', N'Warehouse', CAST(0.00 AS Decimal(6, 2)))
GO
INSERT [dbo].[tblStockLocation] ([StockLocation_Name]) VALUES (N'Shop')
GO
INSERT [dbo].[tblStockLocation] ([StockLocation_Name]) VALUES (N'Warehouse')
GO
INSERT [dbo].[tblSupplierProductCategories] ([Supplier_Id], [ProductCategory]) VALUES (9, N'Chicken')
GO
INSERT [dbo].[tblSupplierProductCategories] ([Supplier_Id], [ProductCategory]) VALUES (12, N'Chicken')
GO
INSERT [dbo].[tblSupplierProductCategories] ([Supplier_Id], [ProductCategory]) VALUES (12, N'Lamb')
GO
SET IDENTITY_INSERT [dbo].[tblSuppliers] ON 
GO
INSERT [dbo].[tblSuppliers] ([Supplier_Id], [supplier_Company], [supplier_FullName], [supplier_Tel], [supplier_Email], [supplier_Description]) VALUES (9, N'Pig farm', N'Mr Piggy', N'012548912311', N'piggy@pig.com', N'Test')
GO
INSERT [dbo].[tblSuppliers] ([Supplier_Id], [supplier_Company], [supplier_FullName], [supplier_Tel], [supplier_Email], [supplier_Description]) VALUES (12, N'Black Hallow Farm.', N'Mr Edward Sasso', N'078 3686 9791', N'm1co6g1239e@iffymedia.com', N'Test Description')
GO
SET IDENTITY_INSERT [dbo].[tblSuppliers] OFF
GO
/****** Object:  Index [PersonIdFK]    Script Date: 17/04/2019 16:05:35 ******/
ALTER TABLE [dbo].[tblCustomers] ADD  CONSTRAINT [PersonIdFK] UNIQUE NONCLUSTERED 
(
	[Person_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [FK_Person_Id]    Script Date: 17/04/2019 16:05:35 ******/
ALTER TABLE [dbo].[tblEmployee] ADD  CONSTRAINT [FK_Person_Id] UNIQUE NONCLUSTERED 
(
	[Person_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_tblMailingList]    Script Date: 17/04/2019 16:05:35 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_tblMailingList] ON [dbo].[tblMailingList]
(
	[MailingList_Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_tblOrders]    Script Date: 17/04/2019 16:05:35 ******/
CREATE NONCLUSTERED INDEX [IX_tblOrders] ON [dbo].[tblOrders]
(
	[Order_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_tblPayment]    Script Date: 17/04/2019 16:05:35 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_tblPayment] ON [dbo].[tblPayment]
(
	[Order_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UniqueEmail]    Script Date: 17/04/2019 16:05:35 ******/
ALTER TABLE [dbo].[tblPersons] ADD  CONSTRAINT [UniqueEmail] UNIQUE NONCLUSTERED 
(
	[person_Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tblCustomers] ADD  CONSTRAINT [DF_tblCustomers_customer_CompanyName]  DEFAULT ('.') FOR [customer_CompanyName]
GO
ALTER TABLE [dbo].[tblOrderItems] ADD  CONSTRAINT [DF_tblOrderItems_Item_Price]  DEFAULT ((0)) FOR [Item_Price]
GO
ALTER TABLE [dbo].[tblOrders] ADD  CONSTRAINT [DF_tblOrders_Address_Id]  DEFAULT ((0)) FOR [Address_Id]
GO
ALTER TABLE [dbo].[tblPayment] ADD  CONSTRAINT [DF_tblPayment_Payment_Total]  DEFAULT ((0.00)) FOR [payment_Total]
GO
ALTER TABLE [dbo].[tblPayment] ADD  CONSTRAINT [DF_tblPayment_payment_Supplier]  DEFAULT ((0.00)) FOR [payment_Supplier]
GO
ALTER TABLE [dbo].[tblPayment] ADD  CONSTRAINT [DF_tblPayment_payment_Profit]  DEFAULT ((0.00)) FOR [payment_Profit]
GO
ALTER TABLE [dbo].[tblPayment] ADD  CONSTRAINT [DF_tblPayment_payment_ProfitMargin]  DEFAULT ((10)) FOR [payment_ProfitMargin]
GO
ALTER TABLE [dbo].[tblSuppliers] ADD  CONSTRAINT [DF_tblSuppliers_supplier_Email]  DEFAULT ('') FOR [supplier_Email]
GO
ALTER TABLE [dbo].[tblSuppliers] ADD  CONSTRAINT [DF_tblSuppliers_supplier_Description]  DEFAULT ('') FOR [supplier_Description]
GO
ALTER TABLE [dbo].[LinkEmployeeOrders]  WITH CHECK ADD  CONSTRAINT [FK_Link_EmployeeOrders_tblEmployee] FOREIGN KEY([Employee_Id])
REFERENCES [dbo].[tblEmployee] ([Employee_Id])
GO
ALTER TABLE [dbo].[LinkEmployeeOrders] CHECK CONSTRAINT [FK_Link_EmployeeOrders_tblEmployee]
GO
ALTER TABLE [dbo].[LinkEmployeeOrders]  WITH CHECK ADD  CONSTRAINT [FK_Link_EmployeeOrders_tblOrders] FOREIGN KEY([Order_Id])
REFERENCES [dbo].[tblOrders] ([Order_Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LinkEmployeeOrders] CHECK CONSTRAINT [FK_Link_EmployeeOrders_tblOrders]
GO
ALTER TABLE [dbo].[tblAddresses]  WITH CHECK ADD  CONSTRAINT [FK_tblAddresses_tblCustomers] FOREIGN KEY([Customer_Id])
REFERENCES [dbo].[tblCustomers] ([Customer_Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblAddresses] CHECK CONSTRAINT [FK_tblAddresses_tblCustomers]
GO
ALTER TABLE [dbo].[tblCustomers]  WITH CHECK ADD  CONSTRAINT [FK_Cusotmer_tblPersons] FOREIGN KEY([Person_Id])
REFERENCES [dbo].[tblPersons] ([Person_Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblCustomers] CHECK CONSTRAINT [FK_Cusotmer_tblPersons]
GO
ALTER TABLE [dbo].[tblEmployee]  WITH CHECK ADD  CONSTRAINT [FK_tblEmployee_tblPersons] FOREIGN KEY([Person_Id])
REFERENCES [dbo].[tblPersons] ([Person_Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblEmployee] CHECK CONSTRAINT [FK_tblEmployee_tblPersons]
GO
ALTER TABLE [dbo].[tblEmployee]  WITH CHECK ADD  CONSTRAINT [FK_tblEmployee_tblPosition] FOREIGN KEY([Position_Name])
REFERENCES [dbo].[tblPosition] ([Position_Name])
GO
ALTER TABLE [dbo].[tblEmployee] CHECK CONSTRAINT [FK_tblEmployee_tblPosition]
GO
ALTER TABLE [dbo].[tblMailingList]  WITH NOCHECK ADD  CONSTRAINT [FK_tblMailingList_tblPersons] FOREIGN KEY([MailingList_Email])
REFERENCES [dbo].[tblPersons] ([person_Email])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblMailingList] NOCHECK CONSTRAINT [FK_tblMailingList_tblPersons]
GO
ALTER TABLE [dbo].[tblOrderItems]  WITH CHECK ADD  CONSTRAINT [FK_LinkOrderProduct_tblOrders] FOREIGN KEY([Order_Id])
REFERENCES [dbo].[tblOrders] ([Order_Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblOrderItems] CHECK CONSTRAINT [FK_LinkOrderProduct_tblOrders]
GO
ALTER TABLE [dbo].[tblOrderItems]  WITH NOCHECK ADD  CONSTRAINT [FK_LinkOrderProduct_tblProducts1] FOREIGN KEY([Product_Name])
REFERENCES [dbo].[tblProducts] ([Product_Name])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[tblOrderItems] CHECK CONSTRAINT [FK_LinkOrderProduct_tblProducts1]
GO
ALTER TABLE [dbo].[tblOrderQuotes]  WITH CHECK ADD  CONSTRAINT [FK_tblQuotes_LinkOrderProduct] FOREIGN KEY([Item_Id])
REFERENCES [dbo].[tblOrderItems] ([Item_Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblOrderQuotes] CHECK CONSTRAINT [FK_tblQuotes_LinkOrderProduct]
GO
ALTER TABLE [dbo].[tblOrderQuotes]  WITH CHECK ADD  CONSTRAINT [FK_tblQuotes_tblSuppliers] FOREIGN KEY([Supplier_Id])
REFERENCES [dbo].[tblSuppliers] ([Supplier_Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblOrderQuotes] CHECK CONSTRAINT [FK_tblQuotes_tblSuppliers]
GO
ALTER TABLE [dbo].[tblOrders]  WITH CHECK ADD  CONSTRAINT [FK_tblOrders_tblAddresses] FOREIGN KEY([Address_Id])
REFERENCES [dbo].[tblAddresses] ([Address_Id])
GO
ALTER TABLE [dbo].[tblOrders] CHECK CONSTRAINT [FK_tblOrders_tblAddresses]
GO
ALTER TABLE [dbo].[tblOrders]  WITH CHECK ADD  CONSTRAINT [FK_tblOrders_tblCustomers] FOREIGN KEY([Customer_Id])
REFERENCES [dbo].[tblCustomers] ([Customer_Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[tblOrders] CHECK CONSTRAINT [FK_tblOrders_tblCustomers]
GO
ALTER TABLE [dbo].[tblPayment]  WITH NOCHECK ADD  CONSTRAINT [FK_tblPayment_tblOrders] FOREIGN KEY([Order_Id])
REFERENCES [dbo].[tblOrders] ([Order_Id])
ON DELETE SET NULL
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[tblPayment] CHECK CONSTRAINT [FK_tblPayment_tblOrders]
GO
ALTER TABLE [dbo].[tblProducts]  WITH CHECK ADD  CONSTRAINT [FK_tblProducts_tblProductCategories] FOREIGN KEY([productCategory])
REFERENCES [dbo].[tblProductCategories] ([ProductCategory])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblProducts] CHECK CONSTRAINT [FK_tblProducts_tblProductCategories]
GO
ALTER TABLE [dbo].[tblStock]  WITH CHECK ADD  CONSTRAINT [FK_tblStock_tbl_StockLocation1] FOREIGN KEY([StockLocation_Name])
REFERENCES [dbo].[tblStockLocation] ([StockLocation_Name])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblStock] CHECK CONSTRAINT [FK_tblStock_tbl_StockLocation1]
GO
ALTER TABLE [dbo].[tblStock]  WITH CHECK ADD  CONSTRAINT [FK_tblStock_tblProducts1] FOREIGN KEY([Product_Name])
REFERENCES [dbo].[tblProducts] ([Product_Name])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblStock] CHECK CONSTRAINT [FK_tblStock_tblProducts1]
GO
ALTER TABLE [dbo].[tblSupplierProductCategories]  WITH CHECK ADD  CONSTRAINT [FK_tblSupplierProductCategories_tblProductCategories] FOREIGN KEY([ProductCategory])
REFERENCES [dbo].[tblProductCategories] ([ProductCategory])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblSupplierProductCategories] CHECK CONSTRAINT [FK_tblSupplierProductCategories_tblProductCategories]
GO
ALTER TABLE [dbo].[tblSupplierProductCategories]  WITH CHECK ADD  CONSTRAINT [FK_tblSupplierProductCategories_tblSuppliers] FOREIGN KEY([Supplier_Id])
REFERENCES [dbo].[tblSuppliers] ([Supplier_Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblSupplierProductCategories] CHECK CONSTRAINT [FK_tblSupplierProductCategories_tblSuppliers]
GO
/****** Object:  StoredProcedure [dbo].[AddCustomer]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[AddCustomer](
		@Title varchar(5), @Name varchar(100),
		@Surname varchar(100), @Tel varchar(15),
		@Email varchar(200), @Password varchar(130)=null,
		@Company varchar(200)=null, @AddressName varchar(100),
		@FirstLineAdd varchar(200),	@SecondLineAdd varchar(200)=null,
		@City varchar(100),	@PostCode varchar(8))
AS
BEGIN
	If EXISTS (SELECT * FROM tblPersons WHERE person_Email = @Email)
	BEGIN RAISERROR('Email Already Registered!',18,4) RETURN END

	-- Add Person Record 
	-- Password is hashed before writing to the databse
	INSERT INTO tblPersons(
		person_Title, person_Name, person_Surname,
		person_Tel,	person_Email, person_Password)
	VALUES (
		@Title,	@Name, @Surname,
		@Tel, @Email, HASHBYTES('SHA2_512', @Password))
	DECLARE @PersonID numeric(18,0)
	SET @PersonID = SCOPE_IDENTITY() -- Receive the Person ID

	-- Add Customer Record
	INSERT INTO tblCustomers(customer_CompanyName, Person_Id)
	VALUES(	@Company, @PersonID)
	DECLARE @CustomerID numeric(18,0)
	SET @CustomerID = SCOPE_IDENTITY() -- Receive the Cusromer ID

	-- Add Address Record by executing the "AddCustomerAddress"
	-- Procedure
	EXEC dbo.AddCustomerAddress
		@CustomerID,
		@AddressName,
		@FirstLineAdd,
		@SecondLineAdd,
		@City,
		@PostCode
	
	-- return the new Customer ID
	SELECT 'Customer_Id' = @CustomerID
END
GO
/****** Object:  StoredProcedure [dbo].[AddCustomerAddress]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[AddCustomerAddress](
		@CustomerID Int, @AddressName varchar(100),
		@FirstLineAdd varchar(200), @SecondLineAdd varchar(200)=null,
		@City varchar(100),	@PostCode varchar(8))
AS
BEGIN
	IF EXISTS(SELECT * FROM tblCustomers 
		WHERE Customer_Id = @CustomerID)
	BEGIN
		INSERT INTO tblAddresses(
			Customer_Id, address_Name,
			address_1stLine, address_2ndLine,
			address_City, address_PostCode)
		VALUES(
			@CustomerID, @AddressName,
			@FirstLineAdd, @SecondLineAdd,
			@City, @PostCode)
	END
	ELSE
	BEGIN
		RAISERROR('No Customer Found!',18,0)
	END
END
GO
/****** Object:  StoredProcedure [dbo].[AddEmployee]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[AddEmployee](
		@Title nchar(5), @Name nchar(100),
		@Surname nchar(100), @Tel nchar(15),
		@Email nchar(200), @Password nchar(130),
		@PositionName nchar(50))
AS
BEGIN
	-- Create a local VarChar(Max) to hold the hash password value
	DECLARE @HashPassword nvarchar(MAX)
	-- Set the "HassPassword" variable to the value returned from
	-- "HASHBYTES" method by passing the name of the has algorithm
	-- and the "Password" value received which returns the a 
	-- "SHA2_512" Hashed value to be kept in database
	SET @HashPassword = HASHBYTES('SHA2_512', @Password)
	BEGIN TRY
		-- Add person details
		INSERT INTO tblPersons(
			person_Title, person_Name, person_Surname,
			person_Tel,	person_Email, person_Password)
		VALUES(
			@Title,	@Name, @Surname,
			@Tel, @Email, @HashPassword)
		DECLARE @PersonID Int
		SET @PersonID = SCOPE_IDENTITY()
		
		-- Add Employee details
		INSERT INTO tblEmployee(
			Position_Name, Person_Id)
		VALUES(
			@PositionName, @PersonID)
		-- return the new Employee ID
		SELECT 'Employee_Id' = SCOPE_IDENTITY()
	END TRY
	BEGIN CATCH -- use "try catch" to raise custom error
		IF( ERROR_NUMBER() = 2627 )
			RAISERROR ('Email address already registered!',18,0)
		ELSE IF (ERROR_NUMBER() = 547)
			BEGIN -- if invalid position is given as parameter
				-- then delete the new person record created
				DELETE FROM tblPersons WHERE Person_Id = @PersonID
				RAISERROR ('Invalid Position!',18,0)
			END
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[AddEmployeeOrderLink]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[AddEmployeeOrderLink](
		@EmployeeID Int, @OrderID Int)
AS
BEGIN
	DECLARE @InitialEmpStatus Bit = 0

	-- If there is no employee linked to the order
	IF NOT EXISTS (SELECT Employee_Id 
		FROM LinkEmployeeOrders
		WHERE Order_Id = @OrderID
		AND Initial_Employee = 1)
	BEGIN -- Set the Initial Employee status to true
		SET @InitialEmpStatus = 1
	END
	INSERT INTO LinkEmployeeOrders(
		Employee_Id, Order_Id, Initial_Employee)
	VALUES (
		@EmployeeID, @OrderID, @InitialEmpStatus)
END
GO
/****** Object:  StoredProcedure [dbo].[AddMailingList]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[AddMailingList] (@Email varchar(200))
AS
BEGIN
	INSERT INTO tblMailingList(
		MailingList_Email) 
	VALUES(
		@Email)
END
GO
/****** Object:  StoredProcedure [dbo].[AddOrder]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[AddOrder](
		@OrderDate varchar(10),	@OrderDeliveryDate varchar(10),
		@Note varchar(500)=null, @Status varchar(30),
		@OrderType varchar(50),	@CustomerID Int=null,
		@AddressID Int)
AS
BEGIN
	INSERT INTO tblOrders(
		order_Date,	order_DeliveryDate,	order_Note,
		order_Status, order_type, Customer_Id, Address_Id)
	OUTPUT INSERTED.Order_Id
	VALUES (
		@OrderDate,	@OrderDeliveryDate,	@Note,
		@Status, @OrderType, @CustomerID, @AddressID)
END
GO
/****** Object:  StoredProcedure [dbo].[AddOrderItem]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[AddOrderItem](
		@OrderID Int, @ProductName varchar(300),
		@ItemPrice decimal(8,2) =null,
		@ItemQuantity decimal(6,2))
AS
BEGIN
	-- If record does not exists then add new record
	IF NOT EXISTS(SELECT * FROM tblOrderItems 
	   WHERE Order_Id = @OrderID 
	   AND Product_Name = @ProductName)
	BEGIN -- Then add the order item
		INSERT INTO tblOrderItems(
			Order_Id, Product_Name,
			Item_Price, Item_Quantity)
		VALUES(
			@OrderID, @ProductName,
			@ItemPrice,	@ItemQuantity)
	END
END
GO
/****** Object:  StoredProcedure [dbo].[AddProduct]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[AddProduct](
	@ProductName varchar(300),
	@RetailUnit varchar(50),
	@RetailPrice decimal(6,2),
	@Category varchar(80))
AS
BEGIN
	INSERT INTO tblProducts(
		Product_Name,
		product_RetailUnit,
		product_RetailPrice,
		productCategory)
	VALUES(
		@ProductName,
		@RetailUnit,
		@RetailPrice,
		@Category)
END
GO
/****** Object:  StoredProcedure [dbo].[AddProductCategory]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[AddProductCategory](
	@CategoryName varchar(80), 
	@CatImagePath varchar(500),
	@TypeName varchar(80))
AS 
BEGIN
	INSERT INTO tblProductCategories(
		productCategory,
		ProductCategory_imgPath,
		ProductType) 
	VALUES(
		@CategoryName,
		@CatImagePath, 
		@TypeName)
END
GO
/****** Object:  StoredProcedure [dbo].[AddStockLocation]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[AddStockLocation](
	@LocationName varchar(30))
AS
BEGIN
	INSERT INTO tblStockLocation(
		stockLocation_Name) 
	VALUES(
		@LocationName)
END
GO
/****** Object:  StoredProcedure [dbo].[AddSupplier]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[AddSupplier](
		@Company varchar(200)=null,
		@FullName varchar(250),
		@Tel varchar(15),
		@Email varchar(200)=null,
		@Description varchar(1000)=null)
AS
BEGIN
	INSERT INTO tblSuppliers(
		supplier_Company, supplier_FullName,
		supplier_Tel, supplier_Email,
		supplier_Description)
	VALUES (
		@Company, @FullName,
		@Tel, @Email,
		@Description)
	SELECT 'Supplier_Id' = SCOPE_IDENTITY()
END
GO
/****** Object:  StoredProcedure [dbo].[AddSupplierProductCategory]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[AddSupplierProductCategory](
	@SupplierId Int,
	@ProductCategory varchar(80))
AS
BEGIN
	INSERT INTO tblSupplierProductCategories(
		Supplier_Id,
		ProductCategory)
	VALUES (
		@SupplierId,
		@ProductCategory)
END
GO
/****** Object:  StoredProcedure [dbo].[AddUpdateOrderQuote]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[AddUpdateOrderQuote](
		@ItemID Int,
		@SupplierID Int,
		@QuotePrice decimal(8,2)=null,
		@DeliveryDate varchar(10)=null )
AS
BEGIN
	-- If the item has not quote from the supplier 
	IF NOT EXISTS(SELECT * FROM tblOrderQuotes
	WHERE Item_Id =@ItemID
	AND Supplier_Id = @SupplierID)
		BEGIN -- Add new quote
			INSERT INTO tblOrderQuotes(
				Item_Id,
				Supplier_Id,
				quote_Price,
				quote_DeliveryDate)
			VALUES (
				@ItemID,
				@SupplierID,
				@QuotePrice,
				@DeliveryDate)
		END
	-- Else Update the existing record
	ELSE 
		BEGIN
			UPDATE tblOrderQuotes SET
				quote_Price = @QuotePrice,
				quote_DeliveryDate = @DeliveryDate
			WHERE Item_Id = @ItemID
			AND Supplier_Id =@SupplierID
		END
END
GO
/****** Object:  StoredProcedure [dbo].[AddUpdatePayment]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[AddUpdatePayment](
	@paymentTotal decimal(9,2),	@paymentSupplier decimal(9,2),
	@paymentProfit decimal(9,2), @paymentProfitMargin int,
	@paymentDate varchar(10), @OrderId int)
AS
BEGIN
	-- If order has not payment amount
	IF NOT EXISTS (SELECT * FROM tblPayment 
		WHERE Order_Id = @OrderId)
	BEGIN
		INSERT INTO tblPayment( -- Add new payment
				payment_Total, payment_Supplier ,
				payment_Profit,	payment_ProfitMargin,
				payment_Date, Order_Id)
		VALUES (
				@paymentTotal, @paymentSupplier,
				@paymentProfit,	@paymentProfitMargin,
				@paymentDate, @OrderId)
	END
	ELSE -- Update existing record
	BEGIN
		UPDATE tblPayment SET
			payment_Total = @paymentTotal,
			payment_Supplier = @paymentSupplier,
			payment_Profit = @paymentProfit,
			payment_ProfitMargin = @paymentProfitMargin,
			Payment_Date = @paymentDate
		WHERE Order_Id = @OrderId
	END
END
GO
/****** Object:  StoredProcedure [dbo].[AddUpdateStock]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[AddUpdateStock](@ProductName varchar(300),
		@Quantity decimal (6,2)=null,
		@StockLocation varchar(30))
AS
BEGIN -- If the record exists then update the record
	IF EXISTS(SELECT * FROM tblStock 
	WHERE tblStock.Product_Name = @ProductName 
	AND tblStock.StockLocation_Name = @StockLocation)
		BEGIN
		UPDATE tblStock 
		SET stock_Quantity = @Quantity
		WHERE 
			Product_Name = @ProductName AND
			StockLocation_Name = @StockLocation
		END
	ELSE -- else create new record
		BEGIN
			INSERT INTO tblStock(
				stock_Quantity,	StockLocation_Name,Product_Name)
			VALUES (@Quantity, @StockLocation, @ProductName)
		END
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteEncryptCookie]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[DeleteEncryptCookie](
		@HashCookieID varchar(5000))
AS
BEGIN
	-- Delete record
	DELETE FROM tblCookieEncrypt 
	WHERE Hash_Id = @HashCookieID
END
GO
/****** Object:  StoredProcedure [dbo].[EncryptCookieRead]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[EncryptCookieRead](
		@HashCookieID varchar(max))
AS
BEGIN
	DECLARE @Base64Value varchar(MAX)
	-- Check if the there is a record with the @HashCookieID
	SELECT @Base64Value = StringBase64Value
	FROM tblCookieEncrypt 
	WHERE Hash_Id = @HashCookieID

	SELECT 'HashCookieID' = @HashCookieID, 'Base64Value' = @Base64Value
END
GO
/****** Object:  StoredProcedure [dbo].[LoginCheck]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================
-- Login Check
-- Returns 
-- Status => true or false
-- ID => Emloyee or Customer
-- PersonType => Staff, Manager or Customer
-- ================================================
CREATE   PROCEDURE [dbo].[LoginCheck](
		@Email nchar(200), @Password varchar(MAX))
AS
BEGIN
	-- Declear Local variables
	DECLARE @RecPass nvarchar(32)
	DECLARE @PersonID int, @ID int, @PersonType nchar(50), @LCheck bit
	
	-- Select the password and person ID of the record where
	-- the email addresses match 
	SELECT @RecPass = person_Password , @PersonID = Person_Id
	FROM tblPersons
	WHERE person_Email = @Email

	-- Check if the person is customer, staff or manager
	IF EXISTS (SELECT * FROM tblCustomers WHERE Person_Id = @PersonID)
		-- If the person is Customer
		BEGIN
			SET @PersonType = 'Customer'
			SELECT @ID = Customer_Id 
			FROM tblCustomers WHERE  Person_Id = @PersonID
		END
	ELSE IF EXISTS (SELECT * FROM tblEmployee WHERE Person_Id = @PersonID)
		-- If the person is Employee
		-- The @PersonType value will be set the the position name of the Employee
		BEGIN
			SELECT @ID = Employee_Id, @PersonType = Position_Name 
			FROM tblEmployee WHERE Person_Id = @PersonID
		END	
	ELSE
		BEGIN
			SELECT 'Status' = 0, 'ID' = 0 , 'PersonType' = 'Wrong Email'
			RETURN
		END

	-- Use if satement to check if the hashed passwords are the same
	IF (HASHBYTES('SHA2_512', @Password) = @RecPass)
		BEGIN
			SET @LCheck = 1
			SELECT 'Status' = @LCheck, 'ID' = @ID , 'PersonType' = @PersonType
		END
	ELSE -- Else password is wrong
		BEGIN
			SET @LCheck = 0
			SELECT 'Status' = @LCheck, 'ID' = 0 , 'PersonType' = 'Wrong Password'
		END
END
GO
/****** Object:  StoredProcedure [dbo].[ReadEncryptCookie]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ReadEncryptCookie](
		@HashCookieID varchar(max))
AS
BEGIN
	DECLARE @Base64Value varchar(MAX)
	-- Check if the there is a record with the @HashCookieID
	SELECT @Base64Value = StringBase64Value
	FROM tblCookieEncrypt 
	WHERE Hash_Id = @HashCookieID

	SELECT 'HashCookieID' = @HashCookieID, 'Base64Value' = @Base64Value
END
GO
/****** Object:  StoredProcedure [dbo].[RemoveCustomer]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[RemoveCustomer](@CustomerID Int)
AS
BEGIN
	-- Delete the Person record to delete all 
	-- the related records for customer
	DELETE FROM tblPersons 
	WHERE Person_Id = (
		SELECT Person_Id FROM tblCustomers
		WHERE Customer_Id = @CustomerID)
END
GO
/****** Object:  StoredProcedure [dbo].[RemoveCustomerAddress]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[RemoveCustomerAddress](
	@AddressID Int)
AS
BEGIN
	DELETE FROM tblAddresses 
	WHERE Address_Id = @AddressID
END
GO
/****** Object:  StoredProcedure [dbo].[RemoveEmployee]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[RemoveEmployee](
		@EmployeeID Int)
AS
BEGIN
	DELETE FROM tblPersons 
	WHERE Person_Id = (
	SELECT Person_Id FROM tblEmployee 
    WHERE Employee_Id = @EmployeeID)
END
GO
/****** Object:  StoredProcedure [dbo].[RemoveEmployeeOrderLink]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[RemoveEmployeeOrderLink](
		@EmployeeID Int,
		@OrderID Int)
AS
BEGIN
	-- get the "InitialEployee" status of the link
	DECLARE @InitialEmployee bit
	SELECT @InitialEmployee = Initial_Employee 
	FROM LinkEmployeeOrders 
	WHERE Employee_Id = @EmployeeID
	AND Order_Id = @OrderID

	-- If the employee is NOT the initial employee
	IF (@InitialEmployee != 1)
		DELETE FROM LinkEmployeeOrders -- Remove the link
		WHERE Employee_Id = @EmployeeID
		AND Order_Id = @OrderID
	ELSE -- Else if it is then raise customer error
		RAISERROR('Initial Employee Cannot Be Removed!',18,0)
END
GO
/****** Object:  StoredProcedure [dbo].[RemoveMailingList]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[RemoveMailingList](@Email varchar(200))
AS
BEGIN
	DELETE FROM tblMailingList 
	WHERE tblMailingList.MailingList_Email = @Email 
END
GO
/****** Object:  StoredProcedure [dbo].[RemoveOrder]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[RemoveOrder](
		@OrderID Int)
AS
BEGIN
	DELETE FROM tblOrders
	WHERE Order_Id = @OrderID
END
GO
/****** Object:  StoredProcedure [dbo].[RemoveOrderItem]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[RemoveOrderItem](
		@ItemID Int)
AS
BEGIN
	DELETE FROM tblOrderItems 
	WHERE Item_Id = @ItemID
END
GO
/****** Object:  StoredProcedure [dbo].[RemoveOrderQuote]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[RemoveOrderQuote](
		@QuoteID Int)
AS
BEGIN
	DELETE FROM tblOrderQuotes
	WHERE Quote_Id = @QuoteID
END
GO
/****** Object:  StoredProcedure [dbo].[RemovePayment]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[RemovePayment](
		@OrderID Int)
AS
BEGIN
	DELETE FROM tblPayment 
		WHERE Order_Id = @OrderID
END
GO
/****** Object:  StoredProcedure [dbo].[RemoveProduct]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[RemoveProduct](
	@ProductName varchar(300))
AS
BEGIN
	DELETE FROM tblProducts 
	WHERE Product_Name = @ProductName
END
GO
/****** Object:  StoredProcedure [dbo].[RemoveProductCategory]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[RemoveProductCategory](
	@CategoryName varchar(80))
AS
BEGIN
	DELETE FROM tblProductCategories 
	WHERE tblProductCategories.productCategory = @CategoryName
END
GO
/****** Object:  StoredProcedure [dbo].[RemoveStockLocation]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[RemoveStockLocation](
	@LocationName varchar(30))
AS
BEGIN
	DELETE FROM tblStockLocation 
	WHERE tblStockLocation.stockLocation_Name = @LocationName
END
GO
/****** Object:  StoredProcedure [dbo].[RemoveSupplier]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[RemoveSupplier](
	@SupplierID Int)
AS
BEGIN
	DELETE FROM tblSuppliers 
	WHERE Supplier_Id = @SupplierID
END
GO
/****** Object:  StoredProcedure [dbo].[RemoveSupplierProductCategory]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[RemoveSupplierProductCategory](
	@SupplierId Int,
	@ProductCategory varchar(80))
AS
BEGIN
	DELETE FROM tblSupplierProductCategories
	WHERE Supplier_Id = @SupplierId
	AND ProductCategory =@ProductCategory
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateCustomer]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[UpdateCustomer](
		@CustomerID Int, @Title varchar(5),
		@Name varchar(100), @Surname varchar(100),
		@Tel varchar(15), @Email varchar(200),
		@Company varchar(200)=null)
AS
BEGIN
	UPDATE tblPersons SET
		person_Title = @Title,
		person_Name = @Name,
		person_Surname = @Surname,
		person_Tel = @Tel,
		person_Email = @Email
	WHERE Person_Id = (
		SELECT Person_Id FROM tblCustomers 
		WHERE Customer_Id = @CustomerID)

	UPDATE tblCustomers SET
		customer_CompanyName = @Company
	WHERE Customer_Id = @CustomerID
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateCustomerAddress]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[UpdateCustomerAddress](
		@AddressID Int,	@AddressName varchar(100),
		@FirstLineAdd varchar(200),	@SecondLineAdd varchar(200)=null,
		@City varchar(100),	@PostCode varchar(8))
AS
BEGIN
	UPDATE tblAddresses SET
		address_Name = @AddressName,
		address_1stLine = @FirstLineAdd,
		address_2ndLine = @SecondLineAdd,
		address_City = @City,
		address_PostCode = @PostCode
	WHERE
		Address_Id = @AddressID
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateEmployee]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[UpdateEmployee](
		@EmployeeID Int, @Title varchar(5),
		@Name varchar(100),	@Surname varchar(100),
		@Tel varchar(15), @Email varchar(200),
		@PositionName varchar(50))
AS
BEGIN
	BEGIN TRY
		-- If the Person_Id exists
		IF EXISTS(SELECT Person_Id FROM tblEmployee
			WHERE Employee_Id = @EmployeeID)
		BEGIN
			-- Update person details
			UPDATE tblPersons SET
				person_Title = @Title, person_Name = @Name,
				person_Surname = @Surname, person_Tel = @Tel,
				person_Email = @Email
			WHERE Person_Id = (
				SELECT Person_Id FROM tblEmployee
				WHERE Employee_Id = @EmployeeID)
			
			-- Update Employee details
			UPDATE tblEmployee SET Position_Name = @PositionName
			WHERE Employee_Id = @EmployeeID
		END
	END TRY
	BEGIN CATCH -- use "try catch" to raise custom error
		IF( ERROR_NUMBER() = 2627 )
			RAISERROR ('Email address already registered!',0,0)
		ELSE IF (ERROR_NUMBER() = 547)
			BEGIN
				RAISERROR ('Invalid Position!',0,0)
			END
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateOrder]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[UpdateOrder](
		@OrderID Int, @OrderDate varchar(10),
		@OrderDeliveryDate varchar(10),	@Note varchar(500)=null,
		@Status varchar(30), @orderType varchar(50),
		@AddressID Int)
AS
BEGIN
	UPDATE tblOrders SET
		order_Date = @OrderDate,
		order_DeliveryDate = @OrderDeliveryDate,
		order_Note = @Note,
		order_Status = @Status,
		order_type = @orderType,
		Address_Id = @AddressID
	WHERE
		Order_Id = @OrderID
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateOrderItem]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[UpdateOrderItem](
		@OrderID Int, @ProductName varchar(300),
		@ItemPrice decimal(8,2), @ItemQuantity decimal(6,2))
AS
BEGIN
	UPDATE tblOrderItems SET
		Item_Price = @ItemPrice,
		Item_Quantity = @ItemQuantity
	WHERE Order_Id = @OrderID
	AND Product_Name = @ProductName
END
GO
/****** Object:  StoredProcedure [dbo].[UpdatePassword]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[UpdatePassword](
		@ID Int,
		@PersonType varchar(10),
		@OldPassword varchar(130),
		@NewPassword varchar(130))
AS
BEGIN
	-- Update passed if the person exists and the old password is correct
	IF (@PersonType = 'Customer' AND EXISTS(SELECT * FROM  tblPersons AS P 
			WHERE P.person_Password = HASHBYTES('SHA2_512', @OldPassword)))
	BEGIN
		UPDATE tblPersons SET 
			person_Password = HASHBYTES('SHA2_512', @NewPassword)
		WHERE Person_Id = (SELECT C.Person_Id 
			FROM tblCustomers AS C, tblPersons AS P 
			WHERE Customer_Id = @ID
			AND P.Person_Id = C.Person_Id
			AND P.person_Password = HASHBYTES('SHA2_512', @OldPassword))
	END
	ELSE IF (@PersonType = 'Employee' AND EXISTS(SELECT * FROM  tblPersons AS P 
			WHERE P.person_Password = HASHBYTES('SHA2_512', @OldPassword)))
	BEGIN
		UPDATE tblPersons SET
			person_Password = HASHBYTES('SHA2_512', @NewPassword)
		WHERE Person_Id = (SELECT E.Person_Id 
			FROM tblEmployee AS E, tblPersons AS P
			WHERE Employee_Id = @ID
			AND P.Person_Id = E.Person_Id
			AND P.person_Password = HASHBYTES('SHA2_512', @OldPassword))
	END
	ELSE -- Else the person is not found
	BEGIN
		RAISERROR('Person cannot be found',18,0)
	END
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateProduct]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[UpdateProduct] (
				@ProductName varchar(300),
				@NewProductName varchar(300),
				@RetailUnit varchar(50),
				@RetailPrice decimal (6,2),
				@productCategory varchar(80))
AS
BEGIN
	Update tblProducts
	SET Product_Name = @NewProductName,
		product_RetailUnit = @RetailUnit,
		product_RetailPrice = @RetailPrice,
		productCategory = @productCategory
	WHERE Product_Name = @ProductName
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateProductCategory]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[UpdateProductCategory](
	@FromCategoryName varchar(80),
	@ToCategoryName varchar(80),
	@CatImagePath varchar(500))
AS
BEGIN 
	UPDATE tblProductCategories 
	SET productCategory = @ToCategoryName, 
		ProductCategory_imgPath = @CatImagePath
	WHERE productCategory = @FromCategoryName 
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateStockLocation]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[UpdateStockLocation] (
	@FromLocation varchar(30), 
	@ToLocation varchar(30))
AS
BEGIN
	UPDATE tblStockLocation 
	SET stockLocation_Name = @ToLocation
	WHERE stockLocation_Name = @FromLocation
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateSupplier]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[UpdateSupplier](
		@SupplierID int,
		@Company varchar(200)=null,
		@FullName varchar(250),
		@Tel varchar(15),
		@Email varchar(200)=null,
		@Description varchar(1000)=null)
AS
BEGIN
	UPDATE  tblSuppliers SET
		supplier_Company = @Company,
		supplier_FullName = @FullName,
		supplier_Tel = @Tel,
		supplier_Email = @Email,
		supplier_Description = @Description
	WHERE Supplier_Id = @SupplierID
END
GO
/****** Object:  StoredProcedure [dbo].[ViewCustomer]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ViewCustomer](
	@CustomerID int,
	@ShowAll bit)
AS
BEGIN
	IF (@ShowAll = 0) -- return specific customer
	BEGIN
		SELECT * FROM tblCustomers, tblPersons
		WHERE tblCustomers.Customer_Id = @CustomerID
		AND tblPersons.Person_Id = tblCustomers.Person_Id
	END
	ELSE IF (@ShowAll = 1) -- Return all customers
	BEGIN
		SELECT * FROM tblCustomers, tblPersons
		WHERE tblPersons.Person_Id = tblCustomers.Person_Id
	END
END
GO
/****** Object:  StoredProcedure [dbo].[ViewCustomerAddress]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ViewCustomerAddress](
		@FilterValue Int, @FilterBy varchar(11))
AS
BEGIN
	IF (@FilterBy = 'CustomerID')
	BEGIN
		SELECT * From tblAddresses
		WHERE Customer_Id = @FilterValue
	END
	ELSE IF (@FilterBy = 'AddressID')
	BEGIN
		SELECT * From tblAddresses
		WHERE Address_Id = @FilterValue
	END
END
GO
/****** Object:  StoredProcedure [dbo].[ViewEmployee]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ViewEmployee](
		@EmployeeID Int, @ShowAll bit)
AS
BEGIN
	IF (@ShowAll = 0)
	BEGIN
		SELECT *
		FROM tblEmployee AS E,tblPersons AS P
		WHERE Employee_Id = @EmployeeID
		AND P.Person_Id = E.Person_Id
	END
	ELSE IF (@ShowAll = 1)
	BEGIN
		SELECT *
		FROM tblEmployee AS E,tblPersons AS P
		WHERE P.Person_Id = E.Person_Id
	END
END
GO
/****** Object:  StoredProcedure [dbo].[ViewEmployeeOrderLink]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ViewEmployeeOrderLink](
		@FilterValue Int,
		@FilterBy varchar(10))
AS
BEGIN
	IF(@FilterBy = 'EmployeeID')
	BEGIN
		SELECT L.Employee_Id, L.Order_Id, L.Initial_Employee,
				P.person_Title, P.person_Name, P.person_Surname
		FROM LinkEmployeeOrders as L, tblPersons as P, tblEmployee as E
		WHERE L.Employee_Id = @FilterValue
		AND  L.Employee_Id = E.Employee_Id
		AND E.Person_Id = P.Person_Id
	END
	ELSE IF(@FilterBy = 'OrderID')
	BEGIN
		SELECT L.Employee_Id, L.Order_Id, L.Initial_Employee,
				P.person_Title, P.person_Name, P.person_Surname
		FROM LinkEmployeeOrders as L, tblPersons as P, tblEmployee as E
		WHERE L.Order_Id = @FilterValue
		AND  L.Employee_Id = E.Employee_Id
		AND E.Person_Id = P.Person_Id
	END
END
GO
/****** Object:  StoredProcedure [dbo].[ViewMailingList]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ViewMailingList]
AS
BEGIN
	SELECT * FROM tblMailingList
END
GO
/****** Object:  StoredProcedure [dbo].[ViewOrder]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ViewOrder](
		@FilterValue varchar(30),
		@FilterBy varchar(30))
AS
BEGIN
	IF (@FilterBy = 'Off')
		SELECT * FROM tblOrders
	IF (@FilterBy = 'CustomerID')
		SELECT * FROM tblOrders WHERE Customer_Id = @FilterValue
	IF (@FilterBy = 'Status')
		SELECT * FROM tblOrders WHERE order_Status = @FilterValue
	IF (@FilterBy = 'OrderID')
		SELECT * FROM tblOrders WHERE Order_Id = @FilterValue
	IF (@FilterBy = 'Type')
		SELECT * FROM tblOrders WHERE order_Type = @FilterValue		
		
END
GO
/****** Object:  StoredProcedure [dbo].[ViewOrderItems]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ViewOrderItems](
		@OrderID Int)
AS
BEGIN
	SELECT I.Item_Id, I.Item_Price, I.Item_Quantity,
			P.Product_Name, P.product_RetailUnit, P.productCategory
	FROM tblOrderItems AS I, tblProducts AS P
	WHERE Order_Id = @OrderID
	AND P.Product_Name = I.Product_Name
END
GO
/****** Object:  StoredProcedure [dbo].[ViewOrderQuote]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ViewOrderQuote](
		@FilterBy varchar(10),
		@IDValue Int )
AS
BEGIN
	IF (@FilterBy = 'ItemID')
	BEGIN
		SELECT * FROM tblOrderQuotes, tblSuppliers
		WHERE Item_Id = @IDValue
		AND tblOrderQuotes.Supplier_Id = tblSuppliers.Supplier_Id
	END
	ELSE IF (@FilterBy = 'QuoteID')
	BEGIN
		SELECT * FROM tblOrderQuotes, tblSuppliers
		WHERE Quote_Id = @IDValue
		AND tblOrderQuotes.Supplier_Id = tblSuppliers.Supplier_Id
	END
END
GO
/****** Object:  StoredProcedure [dbo].[ViewPayment]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ViewPayment](
		@FilterBy varchar(10), @OrderID Int = null,
		@FromDate varchar(10)= null, @ToDate varchar(10)= null)
AS
BEGIN
	IF (@FilterBy = 'OrderID')
	BEGIN
		SELECT * FROM tblPayment
		WHERE Order_Id = @OrderID
	END
	ELSE IF (@FilterBy = 'Date')
	BEGIN
		SELECT * FROM tblPayment
		WHERE Convert(DATE, payment_Date) >= @FromDate
		AND CONVERT(DATE, payment_date) <= @ToDate
	END
	ELSE IF (@FilterBy = 'Off') -- View All payments
	BEGIN
		SELECT * FROM tblPayment
	END
END
GO
/****** Object:  StoredProcedure [dbo].[ViewProductCategories]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ViewProductCategories](
	@CategoryName varchar(80)=null)
AS
BEGIN
	IF (@CategoryName != '')
		SELECT * FROM tblProductCategories 
		WHERE ProductCategory = @CategoryName
	ELSE 
		SELECT * FROM tblProductCategories
END
GO
/****** Object:  StoredProcedure [dbo].[ViewProducts]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ViewProducts](
	@Value varchar(300)=null,
	@FilterBy varchar(30))
AS
BEGIN
	IF (@FilterBy = 'Off') -- View All Products
		SELECT * FROM tblProducts
	ELSE IF (@FilterBy = 'ProductName')
		SELECT * FROM tblProducts WHERE product_Name = @Value
	ELSE IF (@FilterBy = 'Category')
		SELECT * FROM tblProducts WHERE productCategory = @Value
	ELSE
		RAISERROR('ERR_Invalid Filter Value',0,0)
END
GO
/****** Object:  StoredProcedure [dbo].[ViewSockLocations]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ViewSockLocations]
AS
BEGIN
	SELECT * FROM tblStockLocation
END
GO
/****** Object:  StoredProcedure [dbo].[ViewStock]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ViewStock](
	@Value varchar(300)=null, 
	@FilterBy varchar(8))
AS 
BEGIN
	IF (@FilterBy = 'Product')
		SELECT * FROM tblStock 
		WHERE tblStock.Product_Name = @Value 
	IF (@FilterBy = 'Location')
		SELECT * FROM tblStock 
		WHERE tblStock.StockLocation_Name = @Value	
	IF (@FilterBy = 'Off') -- View All Stocks
		SELECT * FROM tblStock
END
GO
/****** Object:  StoredProcedure [dbo].[ViewSupplier]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ViewSupplier](
	@FilterValue Varchar(80),
	@FilterBy Varchar(20))
AS 
BEGIN
	IF (@FilterBy = 'Off') -- View All suppliers
	BEGIN
		SELECT * FROM tblSuppliers
	END
	ELSE IF (@FilterBy = 'SupplierID')
	BEGIN
		SELECT * FROM tblSuppliers
		WHERE tblSuppliers.Supplier_Id = @FilterValue
	END
END
GO
/****** Object:  StoredProcedure [dbo].[ViewSupplierProductCategory]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[ViewSupplierProductCategory](
	@SupplierId Int)
AS
BEGIN
	SELECT * FROM tblSupplierProductCategories
	WHERE Supplier_Id = @SupplierId
END
GO
/****** Object:  StoredProcedure [dbo].[WriteReadEncryptCookie]    Script Date: 17/04/2019 16:05:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[WriteReadEncryptCookie](
		@HashCookieID varchar(max),	@NewBase64Value varchar(max))
AS
BEGIN
	DECLARE @RecID int = 0, @OldBase64Value varchar(max),@SaltedID varchar(Max)

	-- First get the record ID and the OldBase64Value if HashCookieID exists
	SELECT @RecID = Id, @OldBase64Value = StringBase64Value
	FROM tblCookieEncrypt 
	WHERE Hash_Id = @HashCookieID

	-- If the @RecID is more than 0 then a record is found
	-- and if @NewBase64Value is not empty
	IF (@RecID > 0 AND @NewBase64Value != '')
	BEGIN
		-- Delete the old record
		DELETE FROM tblCookieEncrypt WHERE Id = @RecID
		-- insert a new record with the @NewBase64Value
		INSERT INTO tblCookieEncrypt(StringBase64Value)
		VALUES (@NewBase64Value)
		-- Receive the id of the newly created ID
		SET @RecID = SCOPE_IDENTITY()
	END
	-- Else If the @RecID is found and no @NewBase64Value is given
	ELSE IF (@RecID > 0 AND @NewBase64Value = '')
	BEGIN
		-- Then set the @NewBase64Value to the @OldBase64Value
		-- the HashCookieID will be change at the end of the procedure
		SET @NewBase64Value = @OldBase64Value
	END
	-- else @RecID is invalid So if @NewBase64Value is not empty
	ELSE IF (@NewBase64Value != '')
	BEGIN
		-- Add the @NewBase64Value to the database 
		INSERT INTO tblCookieEncrypt(StringBase64Value)
		VALUES (@NewBase64Value)
		--  Receive the id of the newly created ID
		SET @RecID = SCOPE_IDENTITY()
	END
	-- ELSE @RecID is invalid and @NewBase64Value is not empty
	-- So do nothing so stop executing the code further
	ELSE BEGIN RETURN END
	
	-- if the code continues to here create a has id
	DECLARE @HashID varchar(MAX) = HASHBYTES('SHA2_512', 
		cast(@RecID as varchar(25)) + 
		cast(CONVERT(varchar(255), NEWID()) as varchar(255)))
	
	-- Update the record with the hash ID	
	UPDATE tblCookieEncrypt SET Hash_Id = @HashID WHERE Id = @RecID
	
	-- Return Both the HashID and the 
	SELECT 'HashCookieID' = @HashID, 'Base64Value' = @NewBase64Value
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Meat unit is Kilograms 
Condiments is per item' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tblProducts', @level2type=N'COLUMN',@level2name=N'product_RetailPrice'
