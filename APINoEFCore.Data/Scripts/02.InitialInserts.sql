USE APINoEFCore
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*Create Table Product*/
CREATE TABLE [Product](
	Id uniqueidentifier NOT NULL,
	Name nvarchar(max) NOT NULL,
	Price decimal(5, 2) NOT NULL,
	CreatedAt datetime2(7) NOT NULL,
 CONSTRAINT PK_Product PRIMARY KEY CLUSTERED 
(
	Id ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/*Insert on Product*/
INSERT INTO [Product]
           (Id,Name,Price,CreatedAt)
     VALUES
           ('6F8E9522-2693-4517-AB05-5814FEF799F9','Product 1',2.00,GETDATE()),
		   ('36B119C3-55FA-42FC-948D-F7F314CBDA60','Product 2',5.00,GETDATE())
GO

/*Create Table Persons*/
CREATE TABLE [Person](
	Id uniqueidentifier NOT NULL,
	Name nvarchar(max) NOT NULL,
    Email nvarchar(max) NOT NULL,
    Password nvarchar(max) NOT NULL,
	IsAdmin bit NOT NULL,
	CreatedAt datetime2(7) NOT NULL,
 CONSTRAINT PK_Persons PRIMARY KEY CLUSTERED 
(
	Id ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/*Insert on Persons*/
INSERT INTO [Persons]
			(Id, Name, CreatedAt)
     VALUES
           ('690D5EEE-EF40-40A2-9BE4-CD8610C2692C', 'Person 1', 'person1@api.com', '123456', 1, GETDATE()),
           ('CEB0B506-A565-4C9F-9F92-ED08B949F23B', 'Person 2', 'person2@api.com', '123456', 0, GETDATE()),
GO

/*Create Table Order*/
CREATE TABLE [Order](
	Id uniqueidentifier NOT NULL,
	PersonId uniqueidentifier NOT NULL,
	ProductId uniqueidentifier NOT NULL,
	Qty int NOT NULL,
	Total decimal(5, 2) NOT NULL,
	CreatedAt datetime2(7) NOT NULL,
 CONSTRAINT PK_Order PRIMARY KEY CLUSTERED 
(
	Id ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [Order]  WITH CHECK ADD  CONSTRAINT FK_Order_Persons_PersonId FOREIGN KEY(PersonId)
REFERENCES dbo.Persons (Id)
ON DELETE CASCADE
GO

ALTER TABLE [Order] CHECK CONSTRAINT FK_Order_Persons_PersonId
GO

ALTER TABLE [Order]  WITH CHECK ADD  CONSTRAINT FK_Order_Product_ProductId FOREIGN KEY(ProductId)
REFERENCES dbo.Product (Id)
ON DELETE CASCADE
GO

ALTER TABLE [Order] CHECK CONSTRAINT FK_Order_Product_ProductId
GO

/*Insert on Orders*/
INSERT INTO [Order]
           (Id, PersonId, ProductId, Qty, Total, CreatedAt)
     VALUES
           ('98DBDAA4-6C3F-47F4-B293-EE68ADE2B102', '690D5EEE-EF40-40A2-9BE4-CD8610C2692C', '6F8E9522-2693-4517-AB05-5814FEF799F9', 10, 20.00, GETDATE())
GO

CREATE PROCEDURE [CreateOrder]
@PersonId UNIQUEIDENTIFIER,
@ProductId UNIQUEIDENTIFIER,
@Qty INT
AS
BEGIN
    DECLARE @Price DECIMAL(5, 2)
    DECLARE @NewGuid UNIQUEIDENTIFIER
    DECLARE @CurrentDateTime DATETIME2(7)

    -- Get Price from Product table
    SELECT @Price = Price
    FROM Product
    WHERE Id = @ProductId

    -- Calculate Total
    DECLARE @Total DECIMAL(5, 2)
    SET @Total = @Price * @Qty

    -- Generate new Guid and current DateTime
    SET @NewGuid = NEWID()
    SET @CurrentDateTime = SYSDATETIME()

    -- Insert into Order table
    INSERT INTO [Order] (Id, PersonId, ProductId, Qty, Total, CreatedAt)
    VALUES (@NewGuid, @PersonId, @ProductId, @Qty, @Total, @CurrentDateTime)                                       
END
GO





