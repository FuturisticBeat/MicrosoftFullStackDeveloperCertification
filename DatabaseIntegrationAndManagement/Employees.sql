USE employeedb;

SHOW TABLES;

CREATE TABLE Employees
(
    ID              INT PRIMARY KEY AUTO_INCREMENT,
    FirstName       VARCHAR(50),
    LastName        VARCHAR(50),
    Department      VARCHAR(50),
    Salary          DECIMAL(10, 2),
    YearsExperience INT,
    HireDate        DATE
);

DESCRIBE Employees;

INSERT INTO Employees (FirstName, LastName, Department, Salary, YearsExperience)
VALUES ('John', 'Doe', 'HR', 50000.00, 5),
       ('Jane', 'Smith', 'IT', 60000.00, 7),
       ('Mike', 'Johnson', 'HR', 55000.00, 6),
       ('Sara', 'Williams', 'IT', 65000.00, 8),
       ('David', 'Brown', 'HR', 52000.00, 5),
       ('Mary', 'Jones', 'IT', 62000.00, 7),
       ('James', 'Garcia', 'HR', 53000.00, 6),
       ('Patricia', 'Martinez', 'IT', 64000.00, 8),
       ('Robert', 'Robinson', 'HR', 51000.00, 5),
       ('Linda', 'Hernandez', 'IT', 61000.00, 7),
       ('Michael', 'Gonzalez', 'HR', 54000.00, 6),
       ('Barbara', 'Moore', 'IT', 63000.00, 8);

SELECT *
FROM Employees;

SELECT FirstName, LastName
FROM Employees;

-- The DISTINCT keyword is used to return only distinct (different) values
SELECT DISTINCT Department
FROM Employees;

SELECT *
FROM Employees
WHERE Department = 'HR';

SELECT *
FROM Employees
WHERE Department = 'Finance'
  AND Salary > 60000;

SELECT *
FROM Employees
WHERE YearsExperience > 5
  AND Salary < 70000;

SELECT *
FROM Employees
ORDER BY LastName;

SELECT *
FROM Employees
WHERE Department = 'HR'
ORDER BY Salary DESC;

SELECT *
FROM Employees
ORDER BY Salary DESC LIMIT 3;

SELECT *
FROM Employees
WHERE Department = 'IT'
  AND YearsExperience > 3
ORDER BY YearsExperience DESC;

SELECT *
FROM Employees
WHERE Salary BETWEEN 50000 AND 75000
ORDER BY FirstName;

# SQL Functions

-- The CONCAT function is used to combine two or more strings into one string 
SELECT CONCAT(FirstName, ' ', LastName)
AS FullName
FROM Employees;

-- The UPPER function is used to convert all characters in a string to uppercase
SELECT UPPER(Department)
AS UpperDepartment
FROM Employees;

-- The LOWER function is used to convert all characters in a string to lowercase
SELECT LOWER(LastName)
AS LowerLastName
FROM Employees;

-- The LENGTH function is used to return the length of a string
SELECT LENGTH(FirstName)
AS FirstNameLength
FROM Employees;

-- The SUBSTRING function is used to extract a substring from a string 
-- (1, 3) means start at position 1 and get 3 characters
SELECT SUBSTRING(LastName, 1, 3)
AS LastNameSnippet
FROM Employees;

# Aggregate Functions

-- The COUNT function is used to return the number of rows that match a specified condition
SELECT COUNT(*)
AS TotalEmployees
FROM Employees;

-- The SUM function is used to return the total sum of a numeric column
SELECT SUM(Salary)
AS TotalSalary
FROM Employees;

-- The AVG function is used to return the average value of a numeric column
SELECT AVG(Salary)
AS AvgEngineeringSalary
FROM Employees
WHERE Department = 'Engineering';

-- The MIN function is used to return the minimum value of a numeric column
SELECT MIN(Salary)
AS MinSalary
FROM Employees;

-- The MAX function is used to return the maximum value of a numeric column
SELECT MAX(Salary)
AS MaxSalesSalary
FROM Employees
WHERE Department = 'Sales';

-- The GROUP BY clause is used to group rows that have the same values into summary rows
SELECT Department, SUM(Salary)
AS TotalSalary
FROM Employees
GROUP BY Department;

SELECT Department, AVG(Salary)
AS AvgSalary
FROM Employees
GROUP BY Department;

SELECT Department, COUNT(*)
AS EmployeeCount
FROM Employees
GROUP BY Department;

# Advanced Functions
SELECT CONCAT(FirstName, ' ', LastName)
AS FullName,
LENGTH(CONCAT(FirstName, ' ', LastName))
AS FullNameLength
FROM Employees;

SELECT YEAR(HireDate) 
AS HireYear, 
COUNT(*)
AS EmployeeCount
FROM Employees
GROUP BY HireYear;

SELECT YEAR(HireDate) 
AS HireYear,
SUM(Salary)
AS TotalSalary
FROM Employees
GROUP BY HireYear;