--CREATE DATABASE Shapkin_Shop;
--GO

USE Shapkin_Shop;
GO

CREATE TABLE Category (
    CategoryId INT PRIMARY KEY IDENTITY(1,1),
    CategoryName NVARCHAR(30)
);
GO

INSERT INTO Category (CategoryName) VALUES
('Шапки'),
('Перчатки'),
('Шарфы');
GO

CREATE TABLE Good (
    GoodId INT PRIMARY KEY IDENTITY(1,1),
    GoodName NVARCHAR(255),
    Price FLOAT,
    Picture NVARCHAR(50) NULL,
    Description NVARCHAR(255) NULL,
    CountGood INT,
    CategoryId INT,
    FOREIGN KEY (CategoryId) REFERENCES Category(CategoryId)
);
GO

INSERT INTO Good (GoodName, Price, Picture, Description, CountGood, CategoryId) VALUES
('Шапка вязаная', 500.00, 'hat1.jpg', 'Теплая вязаная шапка', 10, 1),
('Перчатки кожаные', 800.00, 'gloves1.jpg', 'Кожаные перчатки высокого качества', 5, 2),
('Шарф шерстяной', 600.00, 'scarf1.jpg', 'Шерстяной шарф для защиты от холода', 8, 3),
('Шапка ушанка', 750.00, 'hat2.jpg', 'Шапка ушанка с мехом', 7, 1),
('Перчатки трикотажные', 350.00, 'gloves2.jpg', 'Трикотажные перчатки для повседневной носки', 12, 2),
('Шарф шелковый', 900.00, 'scarf2.jpg', 'Шелковый шарф для элегантного образа', 4, 3);
GO

CREATE TABLE [User] (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    UserName NVARCHAR(30),
    Password NVARCHAR(30),
    Role NVARCHAR(30)
);
GO

INSERT INTO [User] (UserName, Password, Role) VALUES
('admin', 'admin123', 'Admin'),
('user1', 'user123', 'User'),
('user2', 'user456', 'User');
GO

CREATE TABLE Sell (
    SellId INT PRIMARY KEY IDENTITY(1,1),
    GoodId INT,
    DateSell DATETIME,
    SellCount INT,
    FOREIGN KEY (GoodId) REFERENCES Good(GoodId)
);
GO

INSERT INTO Sell (GoodId, DateSell, SellCount) VALUES
(1, '2023-10-26 10:00:00', 2),
(2, '2023-10-26 11:30:00', 1),
(3, '2023-10-26 14:00:00', 3),
(1, '2023-10-27 09:00:00', 1),
(4, '2023-10-27 12:00:00', 2),
(5, '2023-10-27 15:00:00', 4);
GO

SELECT * FROM Good;
SELECT * From [User];
SELECT * FROM Sell;