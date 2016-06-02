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
							   fldTown VARCHAR(64) NOT NULL)

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

CREATE TABLE TblOrder		  (fldOrderNumber VARCHAR(64) PRIMARY KEY,
							   fldAltDelivery VARCHAR(64),
							   fldStartDate date NOT NULL,
							   fldDeliveryDate date NOT NULL,
							   fldDeliveryWeek VARCHAR(32) NOT NULL,
							   fldBluePrintLink VARCHAR(256),
							   fldProgressStatus VARCHAR(16) NOT NULL,
							   fldCompanyID INT FOREIGN KEY REFERENCES TblCompany(fldCompanyID),
							   fldCustomerID INT FOREIGN KEY REFERENCES TblCustomer(fldCustomerID))

CREATE TABLE TblNotes		  (fldNoteID int IDENTITY(1,1) PRIMARY KEY,
							   fldComment VARCHAR(1024),
							   fldOrderNumber VARCHAR(64) FOREIGN KEY REFERENCES TblOrder(fldOrderNumber))

CREATE TABLE TblOrderCategory (fldCategoryID int IDENTITY(1,1) PRIMARY KEY,
                               fldCategoryName VARCHAR(64) NOT NULL,
							   fldOrderNumber VARCHAR(64) FOREIGN KEY REFERENCES TblOrder(fldOrderNumber))

CREATE TABLE TblOrderElements (fldOrderElementID int IDENTITY(1,1) PRIMARY KEY,
							   fldPos VARCHAR(32) NOT NULL,
							   fldHinge VARCHAR(64) NOT NULL,
							   fldFinish VARCHAR(64) NOT NULL,
							   fldAmount VARCHAR(32) NOT NULL,
							   fldUnit VARCHAR(32) NOT NULL,
							   fldText VARCHAR(256) NOT NULL,
							   fldStation4Status BIT NOT NULL,
							   fldStation5Status BIT NOT NULL,
							   fldStation6Status BIT NOT NULL,
							   fldStation7Status BIT NOT NULL,
							   fldStation8Status BIT NOT NULL,
							   fldOverallStatus BIT NOT NULL,
							   fldCategoryID INT FOREIGN KEY REFERENCES TblOrderCategory(fldCategoryID))
GO

-- Fill Zip and Company

INSERT INTO TblZipCodes (fldZipCode, fldTown) VALUES(6400, 'Sønderborg'),(6440, 'Nordborg'),(6300, 'Gråsten'),(6200, 'Aabenraa'),(6230, 'Rødekro')


INSERT INTO TblCompany (fldCompanyID,
						fldCompanyName, 
						fldCompanyAdr, 
						fldCompanyPhone, 
						fldCompanyEmail, 
						fldZipCode) 
						
						VALUES(934185,
						      'Sønderborg Køkken',
							  ' Ellegårdvej 23B',
							  '74 42 92 20',
							  'info@sonderborg-kokken.dk',
							   6400)

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
				VALUES
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

CREATE PROCEDURE createOrder  (@OrderNumber VARCHAR(64),
							   @AltDelivery VARCHAR(64),
							   @StartDate DATE,
							   @DeliveryDate DATE,
							   @DeliveryWeek VARCHAR(32),
							   @BluePrintLinks VARCHAR(256),
							   @progressStatus VARCHAR(16),
							   @CompanyID int,
							   @CustomerID int)

AS
	BEGIN
				INSERT INTO TblOrder
				(
					fldOrderNumber,
					fldAltDelivery,
					fldStartDate,
					fldDeliveryDate,
					fldDeliveryWeek,
					fldBluePrintLink,
					fldProgressStatus,
					fldCompanyID,
					fldCustomerID
				)
				VALUES
				(
					@CompanyID,
					@AltDelivery,
					@StartDate,
					@DeliveryDate,
					@DeliveryWeek,
					@BluePrintLinks,
					@progressStatus,
					@CompanyID,
					@CustomerID
				)
END
GO

CREATE PROCEDURE createNotes  (@CommentContent VARCHAR(1024),
							   @OrderNumber VARCHAR(64))

AS
	BEGIN
				INSERT INTO TblNotes
				(
					fldComment,
					fldOrderNumber
				)
				VALUES
				(
					@CommentContent,
					@OrderNumber
				)
END
GO

CREATE PROCEDURE createOrderCategory(@CategoryName VARCHAR(64),
								     @OrderNumber VARCHAR(64))

AS
	BEGIN
				INSERT INTO TblOrderCategory
				(
					fldCategoryName,
					fldOrderNumber
				)
				VALUES
				(
					@CategoryName,
					@OrderNumber
				)
END
GO

CREATE PROCEDURE createOrderElements(@Pos VARCHAR(32),
								     @hinge VARCHAR(64),
									 @finish VARCHAR(64),
									 @amount VARCHAR(32),
									 @unit VARCHAR(32),
									 @text VARCHAR(256),
									 @station4Status BIT,
									 @station5Status BIT,
									 @station6Status BIT,
									 @station7Status BIT,
									 @station8Status BIT,
									 @OverallStatus BIT,
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
					fldStation4Status,
					fldStation5Status,
					fldStation6Status,
					fldStation7Status,
					fldStation8Status,
					fldOverallStatus,
					fldCategoryID
				)
				VALUES
				(
					@Pos,
					@hinge,
					@finish,
					@amount,
					@unit,
					@text,
					@station4Status,
					@station5Status,
					@station6Status,
					@station7Status,
					@station8Status,
					@OverallStatus,
					@categoryID
				)
END
GO

-- Get 

CREATE PROCEDURE getOrder (@OrderNumber VARCHAR(64))
    AS 
	BEGIN
	SELECT  
	   [OrderNumber]
	  ,[StartDate]
	  ,[DeliveryDate]	  
	  ,[DeliveryWeek]
	  ,[AlternativeDelivery]
	  ,[BluePrinkLink]
	  ,[ProgessStatus]
      ,[CompanyId]
      ,[CustomerId]
      ,[CompanyAddress]
      ,[CompanyEmail]
      ,[CustomerAddress]
      ,[CustomerEmail]
      ,[CustomerCity]
      ,[CompanyCity]
  FROM [SKDB].[dbo].[OrderView]
  WHERE OrderNumber = @OrderNumber
END
GO

CREATE PROCEDURE getAllOrders(@ProgressStatus VARCHAR(16))
	AS
	BEGIN
	SELECT
		*
	FROM
		TblOrder
	WHERE
		fldProgressStatus = @ProgressStatus
END
GO

CREATE PROCEDURE getCategories(@OrderNumber VARCHAR(64))
	AS
	BEGIN
		SELECT * FROM TblOrderCategory WHERE TblOrderCategory.fldOrderNumber = @OrderNumber
END
GO

CREATE PROCEDURE getOrderElements(@CategoryID int)
	AS
	BEGIN
		SELECT * FROM TblOrderElements WHERE TblOrderElements.fldCategoryID = @CategoryID
END
GO

CREATE PROCEDURE getNotes(@OrderNumber VARCHAR(64))
	AS
	BEGIN
		SELECT * FROM TblNotes WHERE TblNotes.fldOrderNumber = @OrderNumber
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

-- View

CREATE VIEW OrderView AS
	SELECT 
	ord.fldOrderNumber OrderNumber,
	ord.fldStartDate StartDate,
	ord.fldDeliveryDate DeliveryDate,
	ord.fldDeliveryWeek DeliveryWeek,
	ord.fldAltDelivery AlternativeDelivery,
	ord.fldBluePrintLink BluePrintLink, 
	ord.fldProgressStatus progressStatus,
	ord.fldCompanyID CompanyId, 
	ord.fldCustomerID CustomerId,
	comp.fldCompanyAdr CompanyAddress,
	comp.fldCompanyEmail CompanyEmail,
	cust.fldCustomerAdr CustomerAddress,
	cust.fldCustomerEmail CustomerEmail,
	custZip.fldTown CustomerCity,
	compZip.fldTown CompanyCity
	FROM dbo.TblOrder ord
	INNER JOIN dbo.TblCompany comp ON ord.fldCompanyID=comp.fldCompanyID
	INNER JOIN dbo.TblCustomer cust ON ord.fldCustomerID=cust.fldCustomerID
	INNER JOIN dbo.TblZipCodes custZip ON cust.fldZipCode=custZip.fldZipCode
	INNER JOIN dbo.TblZipCodes compZip ON comp.fldZipCode=compZip.fldZipCode
GO

