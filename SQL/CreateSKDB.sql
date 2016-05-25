USE master
IF EXISTS(Select * from sys.databases where name = 'SKDB')
DROP DATABASE SKDB
GO

CREATE DATABASE SKDB
GO

USE SKDB
GO

-- Creating tables

CREATE TABLE TblZipCodes      (fldZipCode int PRIMARY KEY,
							   fldTown VARCHAR(64) NOT NULL,)

CREATE TABLE TblCompany       (fldCompanyID int PRIMARY KEY,
							   fldCompanyName VARCHAR(64) NOT NULL,
							   fldCompanyAdr VARCHAR(64) NOT NULL,
							   fldCompanyPhone VARCHAR(64)NOT NULL,
							   fldCompanyEmail VARCHAR(64)NOT NULL,
							   fldZipCode INT FOREIGN KEY REFERENCES TblZipCodes(fldZipCode))

CREATE TABLE TblCustomer	  (fldCustomerID int PRIMARY KEY,
							   fldFirstName VARCHAR(64) NOT NULL,
							   fldLastName VARCHAR(64) NOT NULL,
							   fldCustomerPhone VARCHAR(64),
							   fldCustomerEmail VARCHAR(64),
							   fldCustomerAdr VARCHAR(64) NOT NULL,
							   fldZipCode INT FOREIGN KEY REFERENCES TblZipCodes(fldZipCode))

CREATE TABLE TblOrder		  (fldOrderID int PRIMARY KEY,
							   fldAltDelivery VARCHAR(64),
							   fldStartDate date NOT NULL,
							   fldDeliveryDate date NOT NULL,
							   fldCompanyID INT FOREIGN KEY REFERENCES TblCompany(fldCompanyID),
							   fldCustomerID INT FOREIGN KEY REFERENCES TblCustomer(fldCustomerID))

CREATE TABLE TblNotes		  (fldNoteID int IDENTITY(1,1) PRIMARY KEY,
							   fldComment VARCHAR(1024),
							   fldOrderID INT FOREIGN KEY REFERENCES TblOrder(fldOrderID))

CREATE TABLE TblOrderCatagory (fldCatagoryID int IDENTITY(1,1) PRIMARY KEY,
                               fldCatagoryName VARCHAR(64) NOT NULL,
							   fldOrderID INT FOREIGN KEY REFERENCES TblOrder(fldOrderID))

CREATE TABLE TblOrderElements (fldOrderElementID int IDENTITY(1,1) PRIMARY KEY,
							   fldPos VARCHAR(32) NOT NULL,
							   fldHinge VARCHAR(64) NOT NULL,
							   fldFinish VARCHAR(64) NOT NULL,
							   fldAmount VARCHAR(32) NOT NULL,
							   fldUnit VARCHAR(32) NOT NULL,
							   fldText VARCHAR(256) NOT NULL,
							   fldCatagoryID INT FOREIGN KEY REFERENCES TblOrderCatagory(fldCatagoryID))
GO


-- Stored Procedure

CREATE PROCEDURE createCustomer(@CustomerID int,
								@FirstName VARCHAR(64),
								@LastName VARCHAR(64),
								@CustomerPhone VARCHAR(64),
								@CustomerEmail VARCHAR(64),
								@CustomerAdr VARCHAR(64),
								@ZipCode VARCHAR(64))

AS
		BEGIN
				INSERT INTO TblCustomer
				(
					fldCustomerID,
					fldFirstName,
					fldLastName,
					fldCustomerPhone,
					fldCustomerEmail,
					fldCustomerAdr,
					fldZipCode
				)
				Values
				(
					@CustomerID,
					@FirstName,
					@LastName,
					@CustomerPhone,
					@CustomerEmail,
					@CustomerAdr,
					@ZipCode
				)
				
END
GO

CREATE PROCEDURE createCompany(@CompanyID int,
							   @CompanyName VARCHAR(64),
							   @CompanyAdr VARCHAR(64),
							   @CompanyPhone VARCHAR(64),
							   @CompanyEmail VARCHAR(64),
							   @ZipCode int)

AS
	BEGIN
				INSERT INTO TblCompany
				(
					fldCompanyID,
					fldCompanyName,
					fldCompanyAdr,
					fldCompanyPhone,
					fldCompanyEmail,
					fldZipCode
				)
				VALUES
				(
					@CompanyID,
					@CompanyName,
					@companyAdr,
					@CompanyPhone,
					@CompanyEmail,
					@ZipCode
				)
END
GO


CREATE PROCEDURE createOrder  (@OrderID int,
							   @AltDelivery VARCHAR(64),
							   @StartDate DATE,
							   @DeliveryDate DATE,
							   @CompanyID int,
							   @CustomerID int)

AS
	BEGIN
				INSERT INTO TblOrder
				(
					fldOrderID,
					fldAltDelivery,
					fldStartDate,
					fldDeliveryDate,
					fldCompanyID,
					fldCustomerID
				)
				VALUES
				(
					@CompanyID,
					@AltDelivery,
					@StartDate,
					@DeliveryDate,
					@CompanyID,
					@CustomerID
				)
END
GO


CREATE PROCEDURE createNotes  (@CommentContent VARCHAR(1024),
							   @OrderID int)

AS
	BEGIN
				INSERT INTO TblNotes
				(
					fldComment,
					fldOrderID
				)
				VALUES
				(
					@CommentContent,
					@OrderID
				)
END
GO

CREATE PROCEDURE createOrderCategory(@CategoryName VARCHAR(1024),
								     @OrderID int)

AS
	BEGIN
				INSERT INTO TblOrderCatagory
				(
					fldCatagoryName,
					fldOrderID
				)
				VALUES
				(
					@CategoryName,
					@OrderID
				)
END
GO



CREATE PROCEDURE createOrderElements(@Pos VARCHAR(32),
								     @hinge VARCHAR(64),
									 @finish VARCHAR(64),
									 @amount VARCHAR(32),
									 @unit VARCHAR(32),
									 @text VARCHAR(256),
									 @categoryID int)

AS
	BEGIN
				INSERT INTO TblOrderElements
				(
					fldPos,
					fldHinge,
					fldFinish,
					fldAmount,
					fldUnit,
					fldText,
					fldCatagoryID
				)
				VALUES
				(
					@Pos,
					@hinge,
					@finish,
					@amount,
					@unit,
					@text,
					@categoryID
				)
END
GO



CREATE PROCEDURE getOrder (@OrderID int)
    AS 
	BEGIN
		SELECT * FROM TblOrder 
		INNER JOIN TblCompany on TblOrder.fldCompanyID = @OrderID
		INNER JOIN TblCustomer on TblOrder.fldCustomerID = @OrderID 		
		WHERE fldOrderID = @OrderID
END
GO

CREATE PROCEDURE getCategories(@OrderID int)
	AS
	BEGIN
		SELECT * FROM TblOrderCatagory WHERE TblOrderCatagory.fldOrderID = @OrderID
END
GO

CREATE PROCEDURE getOrderElements(@CategoryID int)
	AS
	BEGIN
		SELECT * FROM TblOrderElements WHERE TblOrderElements.fldCatagoryID = @CategoryID
END
GO

CREATE PROCEDURE getNotes(@OrderID int)
	AS
	BEGIN
		SELECT * FROM TblNotes WHERE TblNotes.fldOrderID = @OrderID
END
GO

CREATE PROCEDURE getCustomerZipCode(@CustZip int)
	AS
	BEGIN
		SELECT * FROM TblZipCodes where fldZipCode = @CustZip
END
GO

CREATE PROCEDURE getCompanyZipCode(@CompZip int)
	AS
	BEGIN
		SELECT * FROM TblZipCodes where fldZipCode = @CompZip
END
GO