-- Create tablespace
-- not obligatory part
create tablespace teamwork_tabspace
datafile 'teamwork_tabspace.dat'
size 50M autoextend on;

-- Create temporary tablespace
-- not obligatory part

create temporary tablespace teamwork_tabspace_temp
tempfile 'teamwork_tabspace_temp.dat'
size 10M autoextend on;

--Cretae User    [username] ... [password]
create user teamwork IDENTIFIED BY teamwork
default tablespace teamwork_tabspace
temporary tablespace teamwork_tabspace_temp;

--Grant privileges
grant connect, resource to teamwork

--Assign SYSTEM privileges to new user in Oracle

GRANT create session TO teamwork;
GRANT create table TO teamwork;
GRANT create view TO teamwork;
GRANT create any trigger TO teamwork;
GRANT create any procedure TO teamwork;
GRANT create sequence TO teamwork;
GRANT create synonym TO teamwork;
GRANT unlimited tablespace TO teamwork;


--Change the default db schema
ALTER SESSION SET CURRENT_SCHEMA = teamwork;




CREATE TABLE MEASURES
(
MEASURE_ID INT NOT NULL,
MEASURENAME nvarchar2(255) NOT NULL CONSTRAINT MEASURENAME_UQ UNIQUE,
CONSTRAINT PK_MEASURES PRIMARY KEY (MEASURE_ID)
);

-- This makes auto-increment sequence for primary key in table "Measures"
CREATE SEQUENCE measures_sequence
START WITH 1
INCREMENT BY 1
MINVALUE 1
MAXVALUE 1000000;



CREATE TABLE VENDORS
(
VENDOR_ID INT NOT NULL,
VENDORNAME nvarchar2(255) NOT NULL CONSTRAINT VENDORNAME_UQ UNIQUE,
CONSTRAINT PK_VENDORS PRIMARY KEY (VENDOR_ID)
);

-- This makes auto-increment sequence for primary key in table "Vendors"
CREATE SEQUENCE vendors_sequence
START WITH 1
INCREMENT BY 1
MINVALUE 1
MAXVALUE 1000000;


CREATE TABLE PRODUCTSTYPES
(
TYPE_ID INT NOT NULL,
TYPENAME nvarchar2(255) NOT NULL CONSTRAINT TYPENAME_UQ UNIQUE,
CONSTRAINT PK_PRODUCTSTYPES PRIMARY KEY (TYPE_ID)
);

-- This makes auto-increment sequence for primary key in table "Productstypes"
CREATE SEQUENCE productstypes_sequence
START WITH 1
INCREMENT BY 1
MINVALUE 1
MAXVALUE 1000000;




CREATE TABLE PRODUCTS
(
PRODUCT_ID INT NOT NULL ,
VENDOR_ID INT NOT NULL,
PRODUCTNAME nvarchar2(255) NOT NULL,
MEASURE_ID INT NOT NULL,
TYPE_ID INT NOT NULL,
PRICE FLOAT NOT NULL,
CONSTRAINT PK_PRODUCTS PRIMARY KEY (PRODUCT_ID)
);


-- This makes auto-increment sequence for primary key in table "Products"
CREATE SEQUENCE products_sequence
START WITH 1
INCREMENT BY 1
MINVALUE 1
MAXVALUE 1000000;


--Add foreign key constraints in table "Products"
ALTER TABLE PRODUCTS ADD CONSTRAINT products_ms_fk FOREIGN KEY 
(MEASURE_ID) REFERENCES MEASURES(MEASURE_ID);

ALTER TABLE PRODUCTS ADD CONSTRAINT products_vd_fk FOREIGN KEY 
(VENDOR_ID) REFERENCES VENDORS(VENDOR_ID);

ALTER TABLE PRODUCTS ADD CONSTRAINT products_tps_fk FOREIGN KEY 
(TYPE_ID) REFERENCES PRODUCTSTYPES(TYPE_ID);


--Insert some data
insert into MEASURES(MEASURE_ID, MEASURENAME)
values (measures_sequence.NEXTVAL,'liters');
insert into MEASURES(MEASURE_ID, MEASURENAME)
values (measures_sequence.NEXTVAL,'pieces');
insert into MEASURES(MEASURE_ID, MEASURENAME)
values (measures_sequence.NEXTVAL,'kg');


insert into VENDORS(VENDOR_ID, VENDORNAME)
values (vendors_sequence.NEXTVAL,'Nestle Sofia Corp.');
insert into VENDORS(VENDOR_ID, VENDORNAME)
values (vendors_sequence.NEXTVAL,'Zagorka Corp.');
insert into VENDORS(VENDOR_ID, VENDORNAME)
values (vendors_sequence.NEXTVAL,'Targovishte Bottling Company Ltd.');
insert into VENDORS(VENDOR_ID, VENDORNAME)
values (vendors_sequence.NEXTVAL,'Coca-Cola HBC AG');


insert into PRODUCTSTYPES(TYPE_ID, TYPENAME)
values (productstypes_sequence.NEXTVAL,'Alcohol');
insert into PRODUCTSTYPES(TYPE_ID, TYPENAME)
values (productstypes_sequence.NEXTVAL,'Sweets');
insert into PRODUCTSTYPES(TYPE_ID, TYPENAME)
values (productstypes_sequence.NEXTVAL,'Non-alcohol');


insert into PRODUCTS(PRODUCT_ID, VENDOR_ID, PRODUCTNAME, MEASURE_ID, TYPE_ID, PRICE)
values (products_sequence.NEXTVAL, 2, 'Beer "Zagorka"', 1, 1 , 0.86);
insert into PRODUCTS(PRODUCT_ID, VENDOR_ID, PRODUCTNAME, MEASURE_ID, TYPE_ID, PRICE)
values (products_sequence.NEXTVAL, 3, 'Vodka "Targovishte"', 1, 1 , 7.56);
insert into PRODUCTS(PRODUCT_ID, VENDOR_ID, PRODUCTNAME, MEASURE_ID, TYPE_ID, PRICE)
values (products_sequence.NEXTVAL, 2, 'Beer "Beck`s"', 1, 1 , 1.03);
insert into PRODUCTS(PRODUCT_ID, VENDOR_ID, PRODUCTNAME, MEASURE_ID, TYPE_ID, PRICE)
values (products_sequence.NEXTVAL, 1, 'Chocolate "Milka"', 2, 2 , 2.80);
insert into PRODUCTS(PRODUCT_ID, VENDOR_ID, PRODUCTNAME, MEASURE_ID, TYPE_ID, PRICE)
values (products_sequence.NEXTVAL, 4, 'Coca-Cola', 1, 3 , 1.60);





-- Test if everything is ok
select * from MEASURES;

select * from VENDORS;

select * from PRODUCTSTYPES;

select * from PRODUCTS;



select 
  p.PRODUCTNAME,
  p.price,
  v.VENDORNAME,
  pt.TYPENAME as "Type",
  m.MEASURENAME as "Measure"
from products p
join VENDORS v
  on v.VENDOR_ID = p.VENDOR_ID
join MEASURES m
  on m.MEASURE_ID = p.MEASURE_ID
join PRODUCTSTYPES pt
  on pt.TYPE_ID = p.TYPE_ID;


