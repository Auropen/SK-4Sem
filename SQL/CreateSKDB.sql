USE master
IF EXISTS(Select * from sys.databases where name = 'SKDB')
DROP DATABASE SKDB
GO

CREATE DATABASE SKDB
GO

USE SKDB
GO

-- Creating tables

CREATE TABLE TblZipCodes      (fldZipCode int IDENTITY(1,1) PRIMARY KEY,
							   fldTown VARCHAR(64) NOT NULL,)

CREATE TABLE TblCompany       (fldCompanyID int IDENTITY(1,1) PRIMARY KEY,
							   fldCompanyName VARCHAR(64) NOT NULL,
							   fldAddress VARCHAR(64) NOT NULL,
							   fldTelefoneNumber VARCHAR(64)NOT NULL,
							   fldEmail VARCHAR(64)NOT NULL,
							   fldZipCode INT FOREIGN KEY REFERENCES TblZipCodes(fldZipCode))

CREATE TABLE TblCustomer	  (fldCustomerID int IDENTITY(1,1) PRIMARY KEY,
							   fldFirstName VARCHAR(64) NOT NULL,
							   fldLastName VARCHAR(64) NOT NULL,
							   fldTelefoneNumber VARCHAR(64),
							   fldEmail VARCHAR(64),
							   fldAddrress VARCHAR(64) NOT NULL,
							   fldZipCode INT FOREIGN KEY REFERENCES TblZipCodes(fldZipCode))

CREATE TABLE TblOrder		  (fldOrderID int IDENTITY(1,1) PRIMARY KEY,
							   fldAltDelivery VARCHAR(64), 
							   fldCompanyID INT FOREIGN KEY REFERENCES TblCompany(fldCompanyID),
							   fldCustomerID INT FOREIGN KEY REFERENCES TblCustomer(fldCustomerID))

CREATE TABLE TblNotes		  (fldNoteID int IDENTITY(1,1) PRIMARY KEY,
							   fldComment VARCHAR(1024),
							   fldOrderID INT FOREIGN KEY REFERENCES TblOrder(fldOrderID))

CREATE TABLE TblOrderCatagory (fldCatagoryID int IDENTITY(1,1) PRIMARY KEY,
                               fldCatagoryName VARCHAR(64) NOT NULL,
							   fldOrderID INT FOREIGN KEY REFERENCES TblOrder(fldOrderID))

CREATE TABLE TblOrderElements (fldOrderElementID int IDENTITY(1,1) PRIMARY KEY,
							   fldPosID VARCHAR(32) NOT NULL,
							   fldHinge VARCHAR(64) NOT NULL,
							   fldFinish VARCHAR(64) NOT NULL,
							   fldAmount VARCHAR(32) NOT NULL,
							   fldUnit VARCHAR(32) NOT NULL,
							   fldText VARCHAR(256) NOT NULL,
							   fldCatagoryID INT FOREIGN KEY REFERENCES TblOrderCatagory(fldCatagoryID))
GO


-- Stored Procedure

CREATE PROCEDURE createCustomer(@FirstName VARCHAR(64),
								@LastName VARCHAR(64),
								@TelefoneNumber VARCHAR(64),
								@Email VARCHAR(64),
								@Address VARCHAR(64),
								@ZipCode VARCHAR(64),
								@new_ID int OUTPUT)

AS
		BEGIN
				INSERT INTO TblCustomer
				(
					fldFirstName,
					fldLastName,
					fldTelefoneNumber,
					fldEmail,
					fldAddrress,
					fldZipCode
				)
				Values
				(
					@FirstName,
					@LastName,
					@TelefoneNumber,
					@Email,
					@Address,
					@ZipCode
				)
				SET @new_ID = SCOPE_IDENTITY()
END
GO

