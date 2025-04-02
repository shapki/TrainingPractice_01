-- CREATE DATABASE Shapkin_JewelryAuction;
-- GO

USE Shapkin_JewelryAuction;
GO

CREATE TABLE ItemTypes (
    ItemTypeCode INT PRIMARY KEY,
    ItemTypeName NVARCHAR(255) NOT NULL
);

INSERT INTO ItemTypes (ItemTypeCode, ItemTypeName) VALUES
(1, N'Кольцо'),
(2, N'Серьги'),
(3, N'Браслет'),
(4, N'Ожерелье'),
(5, N'Часы');

CREATE TABLE Items (
    ItemCode INT PRIMARY KEY,
    ItemName NVARCHAR(255) NOT NULL,
    ItemTypeCode INT NOT NULL,
    ProductionYear INT NULL,
    Owner NVARCHAR(255) NULL,
    ReceiptDate DATE NULL,
    EstimatedValue DECIMAL(18, 2) NULL,
    Description NVARCHAR(MAX) NULL,
    FOREIGN KEY (ItemTypeCode) REFERENCES ItemTypes(ItemTypeCode)
);

INSERT INTO Items (ItemCode, ItemName, ItemTypeCode, ProductionYear, Owner, ReceiptDate, EstimatedValue, Description) VALUES
(1, N'Кольцо с бриллиантом "Элиза"', 1, 1920, N'Иванов Иван Иванович', '2023-01-15', 15000.00, N'Старинное кольцо из белого золота с бриллиантом огранки "маркиз".'),
(2, N'Серьги с изумрудами', 2, 1950, N'Петрова Анна Сергеевна', '2023-02-20', 8000.00, N'Серьги из желтого золота с крупными изумрудами.');
INSERT INTO Items (ItemCode, ItemName, ItemTypeCode, ProductionYear, Owner, ReceiptDate, EstimatedValue, Description) VALUES
(3, N'Браслет "Змея"', 3, 2000, N'Сидоров Петр Алексеевич', '2023-03-10', 5000.00, N'Браслет из серебра в форме змеи с гранатами.'),
(4, N'Ожерелье "Клеопатра"', 4, 1880, N'Смирнова Елена Владимировна', '2023-04-05', 25000.00, N'Ожерелье из жемчуга и золотых бусин.'),
(5, N'Часы "Rolex"', 5, 2015, N'Козлов Дмитрий Юрьевич', '2023-05-12', 12000.00, N'Мужские часы Rolex Submariner.');

CREATE TABLE Sales (
    SaleCode INT PRIMARY KEY,
    ItemCode INT NOT NULL,
    AuctionDate DATE NOT NULL,
    StartingPrice DECIMAL(18, 2) NOT NULL,
    FinalPrice DECIMAL(18, 2) NULL,
    Sold BIT NOT NULL,  -- 1 - да, 0 - нет
    BuyerLastName NVARCHAR(255) NULL,
    FOREIGN KEY (ItemCode) REFERENCES Items(ItemCode)
);

INSERT INTO Sales (SaleCode, ItemCode, AuctionDate, StartingPrice, FinalPrice, Sold, BuyerLastName) VALUES
(1, 1, '2023-02-28', 12000.00, 16000.00, 1, N'Кузнецов');
INSERT INTO Sales (SaleCode, ItemCode, AuctionDate, StartingPrice, FinalPrice, Sold, BuyerLastName) VALUES
(2, 2, '2023-03-15', 7000.00, 9000.00, 1, N'Волкова');
INSERT INTO Sales (SaleCode, ItemCode, AuctionDate, StartingPrice, FinalPrice, Sold, BuyerLastName) VALUES
(3, 3, '2023-04-01', 4000.00, 4500.00, 1, N'Лебедев');
INSERT INTO Sales (SaleCode, ItemCode, AuctionDate, StartingPrice, FinalPrice, Sold, BuyerLastName) VALUES
(4, 4, '2023-05-05', 22000.00, 28000.00, 1, N'Морозов');
INSERT INTO Sales (SaleCode, ItemCode, AuctionDate, StartingPrice, FinalPrice, Sold, BuyerLastName) VALUES
(5, 5, '2023-06-10', 10000.00, 11500.00, 1, N'Попов');
INSERT INTO Sales (SaleCode, ItemCode, AuctionDate, StartingPrice, FinalPrice, Sold, BuyerLastName) VALUES
(6, 1, '2023-03-20', 13000.00, NULL, 0, NULL); --пример, когда предмет не был продан


CREATE TABLE [Users] (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    IsAdmin BIT NOT NULL DEFAULT 0  -- 1 - админ, 0 - польз
);

INSERT INTO Users (Username, PasswordHash, IsAdmin) VALUES
(N'admin', N'240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 1), -- SHA256
(N'user', N'e606e38b0d8c19b24cf0ee3808183162ea7cd63ff7912dbb22b5e803286b4446', 0);

ALTER TABLE ItemTypes
ADD Category NVARCHAR(255) NULL;

SELECT * FROM ItemTypes;
SELECT * FROM Items;
SELECT * FROM Sales;
SELECT * FROM [Users];