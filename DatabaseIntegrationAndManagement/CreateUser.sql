use companydb;

CREATE user 'manager'@'localhost' IDENTIFIED BY 'StrongPassword123';
GRANT ALL PRIVILEGES ON CompanyDB.* TO 'manager'@'localhost';