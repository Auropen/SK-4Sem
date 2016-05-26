USE SKDB


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


CREATE PROCEDURE createOrder  (@OrderID int,
							   @AltDelivery VARCHAR(64),
							   @StartDate DATE,
							   @DeliveryDate DATE,
							   @DeliveryWeek VARCHAR(32),
							   @BluePrintLinks VARCHAR(256),
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
					fldDeliveryWeek,
					fldBluePrintLink,
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

CREATE PROCEDURE createOrderCategory(@CategoryName VARCHAR(64),
								     @OrderID int)

AS
	BEGIN
				INSERT INTO TblOrderCategory
				(
					fldCategoryName,
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
					@categoryID
				)
END
GO

-- Get 

CREATE PROCEDURE getOrder (@OrderID int)
    AS 
	BEGIN
	SELECT  
	   [OrderId]
	  ,[StartDate]
	  ,[DeliveryDate]	  
	  ,[DeliveryWeek]
	  ,[AlternativeDelivery]
	  ,[BluePrinkLink]
      ,[CompanyId]
      ,[CustomerId]
      ,[CompanyAddress]
      ,[CompanyEmail]
      ,[CustomerAddress]
      ,[CustomerEmail]
      ,[CustomerCity]
      ,[CompanyCity]
  FROM [SKDB].[dbo].[OrderView]
  WHERE OrderId = @OrderID
END
GO

CREATE PROCEDURE getCategories(@OrderID int)
	AS
	BEGIN
		SELECT * FROM TblOrderCategory WHERE TblOrderCategory.fldOrderID = @OrderID
END
GO

CREATE PROCEDURE getOrderElements(@CategoryID int)
	AS
	BEGIN
		SELECT * FROM TblOrderElements WHERE TblOrderElements.fldCategoryID = @CategoryID
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


-- View

CREATE VIEW OrderView AS
	SELECT 
	ord.fldOrderID OrderId,
	ord.fldStartDate StartDate,
	ord.fldDeliveryDate DeliveryDate,
	ord.fldDeliveryWeek DeliveryWeek,
	ord.fldAltDelivery AlternativeDelivery,
	ord.fldBluePrintLink BluePrintLink, 
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