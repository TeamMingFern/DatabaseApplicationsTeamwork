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


  
-- Create tables
CREATE TABLE Measures(
	measureId int NOT NULL AUTO_INCREMENT,
	measureName nvarchar(200) NOT NULL UNIQUE,
	CONSTRAINT PK_Measures PRIMARY KEY (measureId)
);

CREATE TABLE Products(
	productId int NOT NULL AUTO_INCREMENT,
	productName nvarchar(200) NOT NULL UNIQUE,
	vendorId int NOT NULL,
	measureId int NOT NULL,
	productPrice float NOT NULL,
	CONSTRAINT PK_Products PRIMARY KEY  (productId)
);

CREATE TABLE Sales(
	saleId int NOT NULL AUTO_INCREMENT,
	saledOn datetime NOT NULL,
	supermarketId int NOT NULL,
	productId int NOT NULL,
	quantity int NOT NULL,
	CONSTRAINT PK_Sales PRIMARY KEY (saleId)
);


CREATE TABLE Supermarkets(
	supermarketId int NOT NULL AUTO_INCREMENT,
	supermarketName nvarchar(200) NOT NULL UNIQUE,
	CONSTRAINT PK_Supermarkets PRIMARY KEY (supermarketId)
);


CREATE TABLE Vendors(
	vendorId int NOT NULL AUTO_INCREMENT,
	vendorName nvarchar(200) NOT NULL UNIQUE,
	CONSTRAINT PK_Vendors PRIMARY KEY (vendorId)
);


-- Insert table data
INSERT INTO Measures(measureId, measureName) VALUES
(1,'liter'),
(2,'kilograms'),
(3,'packs');


INSERT INTO Products(productId, productName, vendorId, measureId, productPrice) VALUES
(1,'Chai',1,3,18),
(2,'Chang',1,1,19),
(3,'Aniseed Syrup',1,1,10),
(4,'Chef Anton''s Cajun Seasoning',2,2,22),
(5,'Chef Anton''s Gumbo Mix',2,3,21.35),
(6,'Grandma''s Boysenberry Spread',3,2,25),
(7,'Uncle Bob''s Organic Dried Pears',3,3,30),
(8,'Northwoods Cranberry Sauce',3,3,40),
(9,'Mishi Kobe Niku',4,3,97),
(10,'Ikura',4,2,31),
(11,'Queso Cabrales',5,2,21),
(12,'Queso Manchego La Pastora',5,2,38),
(13,'Konbu',6,2,6),
(14,'Tofu',6,2,23.25),
(15,'Genen Shouyu',6,1,15.5),
(16,'Pavlova',7,2,17.45),
(17,'Alice Mutton',7,2,39),
(18,'Carnarvon Tigers',7,2,62.5),
(19,'Teatime Chocolate Biscuits',8,3,9.2),
(20,'Sir Rodney''s Marmalade',8,3,81),
(21,'Sir Rodney''s Scones',8,3,10),
(22,'Gustaf''s Knackebrod',9,2,21),
(23,'Tunnbrod',9,2,9),
(24,'Guarana Fantastica',10,1,4.5),
(25,'NuNuCa Nu?-Nougat-Creme',11,2,14),
(26,'Gumbar Gummibarchen',11,2,31.23),
(27,'Schoggi Schokolade',11,1,43.9),
(28,'Rossle Sauerkraut',12,3,45.6),
(29,'Thuringer Rostbratwurst',12,3,123.79),
(30,'Nord-Ost Matjeshering',13,1,25.89),
(31,'Gorgonzola Telino',14,1,12.5),
(32,'Mascarpone Fabioli',14,1,32),
(33,'Geitost',15,2,2.5),
(34,'Sasquatch Ale',16,3,14),
(35,'Steeleye Stout',16,3,18),
(36,'Inlagd Sill',17,2,19),
(37,'Gravad lax',17,1,26),
(38,'Cote de Blaye',18,1,263.5),
(39,'Chartreuse verte',18,3,18),
(40,'Boston Crab Meat',19,3,18.4),
(41,'Jack''s New England Clam Chowder',19,1,9.65),
(42,'Singaporean Hokkien Fried Mee',20,3,14),
(43,'Ipoh Coffee',20,2,46),
(44,'Gula Malacca',20,2,19.45),
(45,'Rogede sild',21,3,9.5),
(46,'Spegesild',21,1,12),
(47,'Zaanse koeken',22,1,9.5),
(48,'Chocolade',22,3,12.75),
(49,'Maxilaku',23,1,20),
(50,'Valkoinen suklaa',23,2,16.25),
(51,'Manjimup Dried Apples',24,2,53),
(52,'Filo Mix',24,2,7),
(53,'Perth Pasties',24,2,32.8),
(54,'Tourtiere',25,1,7.45),
(55,'Pate chinois',25,1,24),
(56,'Gnocchi di nonna Alice',26,1,38),
(57,'Ravioli Angelo',26,3,19.5),
(58,'Escargots de Bourgogne',27,1,13.25),
(59,'Raclette Courdavault',28,1,55),
(60,'Camembert Pierrot',28,2,34),
(61,'Sirop d''erable',29,2,28.5),
(62,'Tarte au sucre',29,2,49.3),
(63,'Vegie-spread',7,2,43.9),
(64,'Wimmers gute Semmelknodel',12,2,33.25),
(65,'Louisiana Fiery Hot Pepper Sauce',2,3,21.05),
(66,'Louisiana Hot Spiced Okra',2,3,17),
(67,'Laughing Lumberjack Lager',16,3,14),
(68,'Scottish Longbreads',8,1,12.5),
(69,'Gudbrandsdalsost',15,1,36),
(70,'Outback Lager',7,3,15),
(71,'Flotemysost',15,1,21.5),
(72,'Mozzarella di Giovanni',14,1,34.8),
(73,'Rod Kaviar',17,3,15),
(74,'Longlife Tofu',4,1,10),
(75,'Rhonbrau Klosterbier',12,2,7.75),
(76,'Lakkalikoori',23,2,18),
(77,'Original Frankfurter grune So?e',12,2,13);


INSERT Sales(saleId, saledOn, supermarketId, productId, quantity) VALUES
(1,'2015-02-05',3,33,139),
(2,'2015-02-02',2,30,128),
(3,'2015-01-22',1,14,65),
(4,'2015-03-03',4,68,278),
(5,'2015-02-22',1,55,228),
(6,'2015-03-04',1,69,282),
(7,'2015-03-06',2,72,296),
(8,'2015-01-15',2,5,29),
(9,'2015-01-22',2,14,65),
(10,'2015-03-11',2,17,75),
(11,'2015-01-24',3,25,107),
(12,'2015-01-30',4,32,136),
(13,'2015-02-04',1,22,94),
(14,'2015-01-27',3,68,279),
(15,'2015-03-03',3,43,180),
(16,'2015-02-12',2,57,234),
(17,'2015-02-23',4,8,39),
(18,'2015-01-17',4,42,176),
(19,'2015-02-12',3,36,153),
(20,'2015-02-07',4,18,80),
(21,'2015-01-24',1,54,225),
(22,'2015-02-21',1,49,202),
(23,'2015-02-17',2,48,200),
(24,'2015-02-16',4,66,270),
(25,'2015-03-02',1,58,240),
(26,'2015-02-24',3,2,16),
(27,'2015-01-12',1,1,11),
(28,'2015-01-11',4,18,78),
(29,'2015-01-24',1,60,249),
(30,'2015-02-25',3,14,62),
(31,'2015-01-21',1,36,151),
(32,'2015-02-07',2,14,64),
(33,'2015-01-21',1,56,233),
(34,'2015-02-22',2,4,23),
(35,'2015-01-14',1,43,180),
(36,'2015-02-12',3,8,39),
(37,'2015-03-11',4,29,123),
(38,'2015-01-17',1,6,32),
(39,'2015-02-02',2,17,74),
(40,'2015-01-15',2,12,54);

INSERT supermarkets(supermarketId, supermarketName) VALUES
(1,'Supermarket Bay Ivan Zmeyovo'),
(2,'Supermarket Plovdiv Stolipinovo'),
(3,'Supermarket Kaspichan Center'),
(4,'Supermarket Bourgas Plaza');


INSERT vendors(vendorId, vendorName) VALUES
(1,'Exotic Liquids'),
(2,'New Orleans Cajun Delights'),
(3,'Grandma Kelly''s Homestead'),
(4,'Tokyo Traders'),
(5,'Cooperativa de Quesos ''Las Cabras'''),
(6,'Mayumi''s'),
(7,'Pavlova, Ltd.'),
(8,'Specialty Biscuits, Ltd.'),
(9,'PB Knackebrod AB'),
(10,'Refrescos Americanas LTDA'),
(11,'Heli Su?waren GmbH & Co. KG'),
(12,'Plutzer Lebensmittelgro?markte AG'),
(13,'Nord-Ost-Fisch Handelsgesellschaft mbH'),
(14,'Formaggi Fortini s.r.l.'),
(15,'Norske Meierier'),
(16,'Bigfoot Breweries'),
(17,'Svensk Sjofoda AB'),
(18,'Aux joyeux ecclesiastiques'),
(19,'New England Seafood Cannery'),
(20,'Leka Trading'),
(21,'Lyngbysild'),
(22,'Zaanse Snoepfabriek'),
(23,'Karkki Oy'),
(24,'G''day, Mate'),
(25,'Ma Maison'),
(26,'Pasta Buttini s.r.l.'),
(27,'Escargots Nouveaux'),
(28,'Gai paturage'),
(29,'Forets d''erables');

ALTER TABLE Products  ADD CONSTRAINT FK_Products_Measures
FOREIGN KEY(measureId) REFERENCES Measures (measureId);

ALTER TABLE Products  ADD CONSTRAINT FK_Products_Vendors
FOREIGN KEY(vendorId) REFERENCES Vendors (vendorId);

ALTER TABLE Sales ADD CONSTRAINT FK_Sales_Products
FOREIGN KEY(productId) REFERENCES Products (productId);

ALTER TABLE Sales ADD CONSTRAINT FK_Sales_Supermarkets 
FOREIGN KEY(supermarketId) REFERENCES Supermarkets (supermarketId);

