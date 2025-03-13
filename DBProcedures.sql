USE StoreSample
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetNextOrderPrediction]
AS
BEGIN
    SET NOCOUNT ON;

    WITH OrderDifferences AS (
        SELECT
            custid,
            OrderDate,
            LAG(OrderDate) OVER (PARTITION BY custid ORDER BY OrderDate) AS PreviousOrderDate
        FROM Sales.Orders
    )
    SELECT
        c.custid AS CustomerId,
        c.companyname AS CustomerName,
        MAX(o.OrderDate) AS LastOrderDate,
        DATEADD(DAY, AVG(DATEDIFF(DAY, od.PreviousOrderDate, od.OrderDate)), MAX(o.OrderDate)) AS NextPredictedOrder
    FROM Sales.Orders o
    INNER JOIN Sales.Customers c ON o.custid = c.custid
    INNER JOIN OrderDifferences od ON o.custid = od.custid AND o.OrderDate = od.OrderDate
    WHERE od.PreviousOrderDate IS NOT NULL  -- Se excluyen los clientes con una sola orden
    GROUP BY c.companyname, c.custid;
END;

GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_GetClientOrders]
    @CustomerId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        OrderId,
        RequiredDate,
        ShippedDate,
        ShipName,
        ShipAddress,
        ShipCity
    FROM Sales.Orders
    WHERE CustId = @CustomerId;
END;

GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_GetEmployees]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        EmpId,
        FirstName + ' ' + LastName AS FullName
    FROM HR.Employees;
END;

GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_GetProducts]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Productid,
        Productname AS Productname
    FROM Production.Products;
END;

GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_GetShippers]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ShipperId,
        CompanyName
    FROM Sales.Shippers;
END;
GO

/****** Object:  StoredProcedure [dbo].[sp_InsertOrderWithProduct]    Script Date: 13/03/2025 5:59:40 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_InsertOrderWithProduct]
    @CustomerId INT,
    @Empid INT,
    @Shipperid INT,
    @Shipname NVARCHAR(255),
    @Shipaddress NVARCHAR(255),
    @Shipcity NVARCHAR(100),
    @Orderdate DATE,
    @Requireddate DATE,
    @Shippeddate DATE NULL,
    @Freight MONEY,
    @Shipcountry NVARCHAR(100),
    @Productid INT,
    @Unitprice DECIMAL(10,2),
    @Qty INT,
    @Discount DECIMAL(5,2)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Orderid INT;

    -- Insertar la nueva orden en Orders incluyendo CustomerId
    INSERT INTO Sales.Orders (CustId, Empid, Shipperid, Shipname, Shipaddress, Shipcity, Orderdate, Requireddate, Shippeddate, Freight, Shipcountry)
    VALUES (@CustomerId, @Empid, @Shipperid, @Shipname, @Shipaddress, @Shipcity, @Orderdate, @Requireddate, @Shippeddate, @Freight, @Shipcountry);

    -- Obtener el ID de la orden recién insertada
    SET @Orderid = SCOPE_IDENTITY();

    -- Insertar el detalle de la orden en OrderDetails
    INSERT INTO Sales.OrderDetails (Orderid, Productid, Unitprice, Qty, Discount)
    VALUES (@Orderid, @Productid, @Unitprice, @Qty, @Discount);
END;
GO