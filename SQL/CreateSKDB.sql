USE master
IF EXISTS(Select * from sys.databases where name = 'SKDB')
DROP DATABASE SKDB
GO

CREATE DATABASE SKDB
GO

USE SKDB
GO

-- Creating tables

CREATE TABLE TblCompany       (fldID int IDENTITY(1,1) PRIMARY KEY,
							   fldName VARCHAR(64) NOT NULL,
							   fldAdr VARCHAR(64) NOT NULL,
							   fldPhone VARCHAR(64) NOT NULL,
							   fldFaxPhone VARCHAR(64),
							   fldCVR VARCHAR(64),
							   fldWebsite VARCHAR(64),
							   fldEmail VARCHAR(64),
							   fldTown VARCHAR(64) NOT NULL,
							   fldZipCode INT NOT NULL

CREATE TABLE TblOrder		  (fldOrderNumber VARCHAR(64) PRIMARY KEY,
							   fldDelivery VARCHAR(64),
							   fldAltDelivery VARCHAR(64),
							   fldHousingAssociation VARCHAR(64),
							   fldStartDate date NOT NULL,
							   fldDeliveryDate date NOT NULL,
							   fldDeliveryWeek VARCHAR(32) NOT NULL,
							   fldBluePrintLink VARCHAR(256),
							   fldRequisitionLink VARCHAR(256),
							   fldProgressStatus VARCHAR(16) NOT NULL,
							   fldCompanyID INT FOREIGN KEY REFERENCES TblCompany(fldCompanyID))

CREATE TABLE TblNotes		  (fldNoteID int IDENTITY(1,1) PRIMARY KEY,
							   fldComment VARCHAR(1024),
							   fldOrderNumber VARCHAR(64) FOREIGN KEY REFERENCES TblOrder(fldOrderNumber))

CREATE TABLE TblOrderCategory (fldCategoryID int PRIMARY KEY,
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

INSERT INTO TblCompany (fldName,
						fldAdr,
						fldPhone,
						fldFaxPhone,
						fldCVR,
						fldWebsite,
						fldEmail,
						fldTown,
						fldZipCode) 
						
	VALUES('Sønderborg Køkken',
		  'Ellegårdvej 23B',
		  '74 42 92 20',
		  '74 42 92 21',
		  '',
		  'www.sonderborg-kokken.dk',
		  'info@sonderborg-kokken.dk',
		  'Sønderborg',
		   6400)

GO

-- Stored Procedure

CREATE PROCEDURE createCompany(@CompanyID int,
							   @CompanyName VARCHAR(64),
							   @Adr VARCHAR(64),
							   @Phone VARCHAR(64),
							   @FaxPhone VARCHAR(64),
							   @CVR VARCHAR(64),
							   @Email VARCHAR(64),
							   @Town VARCHAR(64),
							   @ZipCode int)

AS
	BEGIN
		INSERT INTO TblCompany
		(
			fldCompanyID,
			fldName,
			fldAdr,
			fldPhone,
			fldFaxPhone,
			fldCVR,
			fldWebsite,
			fldEmail,
			fldTown,
			fldZipCode
		)
		VALUES
		(
			@CompanyID
			@Name,
			@Adr,
			@Phone,
			@FaxPhone,
			@CVR,
			@fldWebsite,
			@Email,
			@Town,
			@ZipCode
		)
END
GO

CREATE PROCEDURE createOrder  (@OrderNumber VARCHAR(64),
							   @Delivery VARCHAR(64),
							   @AltDelivery VARCHAR(64),
							   @StartDate DATE,
							   @DeliveryDate DATE,
							   @DeliveryWeek VARCHAR(32),
							   @BluePrintLinks VARCHAR(256),
							   @ProgressStatus VARCHAR(16),
							   @CompanyID int)

AS
	BEGIN
		INSERT INTO TblOrder
		(
			fldOrderNumber,
			fldDelivery,
			fldAltDelivery,
			fldStartDate,
			fldDeliveryDate,
			fldDeliveryWeek,
			fldBluePrintLink,
			fldProgressStatus,
			fldCompanyID
		)
		VALUES
		(
			@CompanyID,
			@Delivery,
			@AltDelivery,
			@StartDate,
			@DeliveryDate,
			@DeliveryWeek,
			@BluePrintLinks,
			@ProgressStatus,
			@CompanyID
		)
END
GO

CREATE PROCEDURE createNotes  (@OrderNumber VARCHAR(64),
							   @Content VARCHAR(1024))

AS
	BEGIN
		INSERT INTO TblNotes
		(
			fldComment,
			fldOrderNumber
		)
		VALUES
		(
			@Content,
			@OrderNumber
		)
END
GO

CREATE PROCEDURE createOrderCategory(@OrderNumber VARCHAR(64), 
								     @CategoryName VARCHAR(64),
								     @new_id INT OUTPUT)

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
		SET @new_id = SCOPE_IDENTITY()
END
GO

CREATE PROCEDURE createOrderElements(@Pos VARCHAR(32),
								     @Hinge VARCHAR(64),
									 @Finish VARCHAR(64),
									 @Amount VARCHAR(32),
									 @Unit VARCHAR(32),
									 @Text VARCHAR(256),
									 @Station4Status BIT,
									 @Station5Status BIT,
									 @Station6Status BIT,
									 @Station7Status BIT,
									 @Station8Status BIT,
									 @OverallStatus BIT,
									 @CategoryID int)

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
			@Hinge,
			@Finish,
			@Amount,
			@Unit,
			@Text,
			@Station4Status,
			@Station5Status,
			@Station6Status,
			@Station7Status,
			@Station8Status,
			@OverallStatus,
			@CategoryID
		)
END
GO

-- Get 

CREATE PROCEDURE getOrder (@OrderNumber VARCHAR(64))
    AS 
	BEGIN
	SELECT
	   [OrderNumber],
	   [StartDate],
	   [DeliveryDate],	  
	   [DeliveryWeek],
	   [Delivery],
	   [AlternativeDelivery],
	   [HousingAssociation],
	   [BlueprinkLink],
	   [RequisitionLink],
	   [ProgressStatus],
       [CompanyName],
       [CompanyAddress],
       [CompanyZipCode],
       [CompanyPhone],
       [CompanyFaxPhone],
       [CompanyCVR],
       [CompanyEmail]
  FROM [SKDB].[dbo].[OrderView]
  WHERE OrderNumber = @OrderNumber
END
GO

CREATE PROCEDURE getAllOrdersOfStatus(@ProgressStatus VARCHAR(16))
    AS 
	BEGIN
	SELECT
	   [OrderNumber],
	   [StartDate],
	   [DeliveryDate],	  
	   [DeliveryWeek],
	   [Delivery],
	   [AlternativeDelivery],
	   [HousingAssociation],
	   [BlueprinkLink],
	   [RequisitionLink],
	   [ProgessStatus],
       [CompanyName],
       [CompanyAddress],
       [CompanyZipCode],
       [CompanyPhone],
       [CompanyFaxPhone],
       [CompanyCVR],
       [CompanyEmail]
  FROM [SKDB].[dbo].[OrderView]
  WHERE ProgressStatus = @ProgressStatus
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

-- View

CREATE VIEW OrderView AS
	SELECT 
	ord.fldOrderNumber OrderNumber,
	ord.fldStartDate StartDate,
	ord.fldDeliveryDate DeliveryDate,
	ord.fldDeliveryWeek DeliveryWeek,
	ord.fldDelivery Delivery,
	ord.fldAltDelivery AlternativeDelivery,
	ord.fldHousingAssociation HousingAssociation,
	ord.fldBluePrintLink BlueprintLink, 
	ord.fldRequisitionLink RequisitionLink, 
	ord.fldProgressStatus ProgressStatus,
	comp.fldName CompanyName,
	comp.fldAdr CompanyAddress, 
	comp.fldZipCode CompanyZipCode,
	comp.fldTown CompanyTown,
	comp.fldPhone CompanyPhone,
	comp.fldFaxPhone CompanyFaxPhone,
	comp.fldCVR CompanyCVR,
	comp.fldEmail CompanyEmail
	FROM dbo.TblOrder ord
	INNER JOIN dbo.TblCompany comp ON ord.fldCompanyID=comp.fldCompanyID
GO

