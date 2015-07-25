DROP DATABASE IF EXISTS `marketsystem`;

CREATE DATABASE `marketsystem`
CHARACTER SET utf8 COLLATE utf8_unicode_ci;

USE `marketsystem`;

DROP TABLE IF EXISTS `sales`;
DROP TABLE IF EXISTS `supermarkets`;
DROP TABLE IF EXISTS `products`;
DROP TABLE IF EXISTS `vendors`;
DROP TABLE IF EXISTS `measures`;
DROP TABLE IF EXISTS `vendorsexpenses`;
DROP TABLE IF EXISTS `producttypes`;

CREATE TABLE `measures` (
    measureId INT PRIMARY KEY,
    measureName NVARCHAR(50) NOT NULL UNIQUE); 
    

CREATE TABLE `vendors`(
	vendorId INT PRIMARY KEY,
    vendorName NVARCHAR(50) NOT NULL UNIQUE);
    
CREATE TABLE `productTypes`(
	typeId INT PRIMARY KEY,
    typeName NVARCHAR(50) NOT NULL UNIQUE);


CREATE TABLE `supermarkets`(
	supermarketId INT PRIMARY KEY,
    supermarketName NVARCHAR(50) NOT NULL UNIQUE);



CREATE TABLE `products`(
	productId INT PRIMARY KEY,
    productName NVARCHAR(200) NOT NULL UNIQUE,
    vendorId INT NOT NULL,
    measureId INT NOT NULL,
    productTypeId INT NOT NULL,
    productPrice DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY(vendorId) REFERENCES vendors(vendorId),
	FOREIGN KEY(measureId) REFERENCES measures(measureId));
 
 
 
CREATE TABLE `sales`(
	id INT PRIMARY KEY,
    saledOn datetime NOT NULL,
    supermarketId INT NOT NULL ,
    productId INT NOT NULL,
    quantity INT NOT NULL,
    price DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY(supermarketId) REFERENCES supermarkets(supermarketId),
    FOREIGN KEY(productId) REFERENCES products(productId));
    
CREATE TABLE `vendor_expenses` (
	vendorId INT NOT NULL,
    expenseDate datetime NOT NULL,
    total DECIMAL(10, 2) NOT NULL,
    PRIMARY KEY (vendorId, expenseDate),
    FOREIGN KEY(vendorId) REFERENCES vendors(vendorId));
