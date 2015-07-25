-- Create the database Sales if it does not exist

DROP DATABASE IF EXISTS `Sales`;

CREATE DATABASE `Sales`
CHARACTER SET utf8 COLLATE utf8_unicode_ci;

USE `Sales`;

-- Drop all existing Sales tables, so that we can create them
DROP TABLE IF EXISTS `Sales`;
DROP TABLE IF EXISTS `Supermarkets`;
DROP TABLE IF EXISTS `Products`;
DROP TABLE IF EXISTS `Vendors`;
DROP TABLE IF EXISTS `Measures`;
DROP TABLE IF EXISTS `VendorsExpenses`;
DROP TABLE IF EXISTS `ProductTypes`;




  
-- Create tables
CREATE TABLE Measures(
	measureId int AUTO_INCREMENT primary key,
	measureName nvarchar(100) NOT NULL UNIQUE
);

CREATE TABLE Vendors(
	vendorId int NOT NULL AUTO_INCREMENT,
	vendorName nvarchar(200) NOT NULL UNIQUE,
    CONSTRAINT PK_Vendros PRIMARY KEY (vendorId)
);

CREATE TABLE ProductTypes(
	typeId int NOT NULL AUTO_INCREMENT,
	typeName nvarchar(200) NOT NULL UNIQUE,
	CONSTRAINT PK_ProductTypes PRIMARY KEY (typeId)
);

CREATE TABLE Supermarkets(
	supermarketId int NOT NULL AUTO_INCREMENT,
	supermarketName nvarchar(200) NOT NULL UNIQUE,
	CONSTRAINT PK_Supermarkets PRIMARY KEY (supermarketId)
);



CREATE TABLE Products(
	productId int NOT NULL AUTO_INCREMENT,
	productName nvarchar(200) NOT NULL UNIQUE,
	vendorId int NOT NULL,
	measureId int NOT NULL,
    productTypeId int NOT NULL,
	productPrice float NOT NULL,
	CONSTRAINT PK_Products PRIMARY KEY  (productId)
);

CREATE TABLE Sales(
	supermarketId int NOT NULL,
    productId int NOT NULL,
	saledOn datetime NOT NULL,
	quantity int NOT NULL,
    price float NOT NULL

);


CREATE TABLE VendorsExpenses(
	expenseId int NOT NULL AUTO_INCREMENT,
	vendorId int NOT NULL,
	expenseDate datetime NOT NULL,
    total decimal NOT NULL,
	CONSTRAINT PK_VendorsExpenses PRIMARY KEY (expenseId)
);







ALTER TABLE Products  ADD CONSTRAINT FK_Products_Measures
FOREIGN KEY(measureId) REFERENCES Measures (measureId);

ALTER TABLE Products  ADD CONSTRAINT FK_Products_Vendors
FOREIGN KEY(vendorId) REFERENCES Vendors (vendorId);

ALTER TABLE Products  ADD CONSTRAINT FK_Products_ProductTypes
FOREIGN KEY(productTypeId) REFERENCES ProductTypes(typeId);

ALTER TABLE Sales ADD CONSTRAINT FK_Sales_Products
FOREIGN KEY(productId) REFERENCES Products (productId);

ALTER TABLE Sales ADD CONSTRAINT FK_Sales_Supermarkets 
FOREIGN KEY(supermarketId) REFERENCES Supermarkets (supermarketId);

ALTER TABLE VendorsExpenses  ADD CONSTRAINT FK_VendorsExpenses_Vendors
FOREIGN KEY(vendorId) REFERENCES Vendors (vendorId);

