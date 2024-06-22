CREATE TABLE Company (
    Id INT Identity(1,1) PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Address VARCHAR(255),
    Country VARCHAR(255)
);

CREATE TABLE Employee (
    Id INT Identity(1,1) PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Age INT NOT NULL,
    Position VARCHAR(255) NOT NULL,
    CompanyId INT NOT NULL
);

ALTER TABLE Employee
ADD CONSTRAINT FK_Employee_Company
FOREIGN KEY (CompanyId) REFERENCES Company(Id);

INSERT INTO Company ( Name, Address, Country) VALUES
('Tech Innovators Inc.', '123 Tech Park', 'USA'),
( 'Global Solutions Ltd.', '456 Business St.', 'Canada'),
( 'Creative Minds', '789 Innovation Blvd', 'UK'),
( 'Future Enterprises', '101 Progress Rd', 'Germany'),
( 'Pioneering Technologies', '202 Discovery Ave', 'Japan');


INSERT INTO Employee (Name, Age, Position, CompanyId) VALUES
('John Doe', 30, 'Software Engineer', 1),
('Jane Smith', 28, 'Data Scientist', 2),
('Emily Johnson', 35, 'Product Manager', 3),
('Michael Brown', 40, 'CTO', 1),
('Sarah Davis', 25, 'UX Designer', 4),
('David Wilson', 32, 'DevOps Engineer', 1),
('Laura Taylor', 27, 'Marketing Specialist', 5),
('James Anderson', 38, 'Sales Manager', 2),
('Sophia Thomas', 29, 'Business Analyst', 3),
('Daniel Moore', 45, 'CEO', 4);


--procedure for getting all the employees
create procedure GetAllEmployees
as 
begin 
	set nocount on
	select *from employee
	set nocount off
end

--procedure for adding employee
create procedure AddEmployee
	@Name varchar(255),
	@Age int,
	@position varchar(255),
	@companyId int

as 
begin 
	set nocount on;
	insert into Employee(Name, Age, Position, CompanyId)
	values (@Name, @Age, @position, @companyId);
set nocount off;
end


alter procedure GetEmployeeByCompany
	@CompanyId int
as 
begin
	set nocount on;
	select *from Employee where CompanyId = @CompanyId
	set nocount off;
end

exec GetEmployeeByCompany @CompanyId = 4

select *from Employee where id = 


--executing the getallemployees sp
exec GetAllEmployees
--executing insert employee sp
EXEC AddEmployee @Name = 'Alice Johnson', @Age = 30, @Position = 'Software Engineer', @CompanyId = 1;


--procedure for getcompany
create procedure GetCompany
as
begin
	set nocount on;
	select *from Company;
	set nocount off;
end

create procedure GetCompanyById
@CompanyId int
as
begin
	set nocount on;
	select *from Company where id = @companyId;
	set nocount off;
end


create procedure GetEmployeeById
@EmployeeId int
as
begin
	set nocount on;
	select *from Company where id = @EmployeeId;
	set nocount off;
end


--exec GetCompany

--procedure for add company
alter procedure AddCompany 
	@Name varchar(255),
	@Address varchar(255),
	@Country varchar(255)
as
begin 
	set nocount on;
	insert into Company(Name,Address,Country)
	values(@Name,@Address,@Country)

	if @@ROWCOUNT = 0
	begin
			SELECT 'FAILURE' AS STATUS
             , '1' AS STATUS_CODE
             , 'Data insertion failure' AS MSG
	end
	else
	begin 
			SELECT 'SUCCESS' AS STATUS
             , '0' AS STATUS_CODE
             , 'Data inserted successfully' AS MSG	
	end

end


exec AddCompany @Name = 'IMark private limited', @Address = 'Kathmandu', @Country = 'Nepal'

exec GetCompany


ALTER PROCEDURE GetCompanyDetail
    @CompanyId INT
AS
BEGIN
    SET NOCOUNT ON;
	
    SELECT 
        c.Id AS CompanyId, 
        c.Name AS CompanyName, 
        c.Address AS CompanyAddress, 
        c.Country AS CompanyCountry, 
        e.Id AS EmployeeId, 
        e.Name AS EmployeeName, 
        e.Age AS EmployeeAge, 
        e.Position AS EmployeePosition
    FROM 
        Company c
    LEFT JOIN 
        Employee e ON c.Id = e.CompanyId
    WHERE 
        c.Id = @CompanyId;

	SET NOCOUNT OFF;
END



EXEC GetCompanyDetail @CompanyId = 1;




create procedure UpdateEmployee
@Id int,
@Name varchar(255),
@Age varchar(255),
@CompanyId varchar(255),
@Position varchar(255)
AS
BEGIN
	set nocount on;
	BEGIN TRY
	update Employee set Name = @Name, Age = @Age, Position= @Position, CompanyId = @CompanyId
	where Id = @Id

	IF @@ROWCOUNT = 0
	BEGIN
		SELECT 'FAILURE' AS STATUS
		, '1' AS STATUS_CODE
		, 'DATA INSERTION FAILED' AS MSG
	END
	ELSE
	BEGIN
		SELECT 'SUCCESS' AS STATUS
             , '0' AS STATUS_CODE
             , 'Data inserted successfully' AS MSG
	END
	END TRY
	BEGIN CATCH
		SELECT 'FAILURE' AS STATUS, ERROR_NUMBER() AS STATUS_CODE, ERROR_MESSAGE() AS MSG;
	END CATCH
	set nocount off;
END

exec UpdateCompany @id = 6 ,@Name = 'Inficare', @Address = 'Naxal, Kathmandu, Nepal' , @Country = 'Nepal'


exec GetCompany


alter PROCEDURE [dbo].[RemoveCompany]
	@CompanyId int
as 
begin
	set nocount on;
	delete from Company where Id = @CompanyId
	
	IF @@ROWCOUNT = 0
	BEGIN
		SELECT 'FAILURE' AS STATUS
		, '1' AS STATUS_CODE
		, 'DATA INSERTION FAILED' AS MSG
	END
	ELSE
	BEGIN
		SELECT 'SUCCESS' AS STATUS
             , '0' AS STATUS_CODE
             , 'Data inserted successfully' AS MSG
	END
	set nocount off;
end


CREATE PROCEDURE [dbo].[RemoveEmployee]
	@EmployeeId int
as 
begin
	set nocount on;
	delete from Employee where Id = @EmployeeId
	
	IF @@ROWCOUNT = 0
	BEGIN
		SELECT 'FAILURE' AS STATUS
		, '1' AS STATUS_CODE
		, 'DATA INSERTION FAILED' AS MSG
	END
	ELSE
	BEGIN
		SELECT 'SUCCESS' AS STATUS
             , '0' AS STATUS_CODE
             , 'Data inserted successfully' AS MSG
	END
	set nocount off;
END



--table valued function -- return whole table(set of rows)
create function [dbo].[GetEmployeesByCompanyId](@CompanyId int)
returns table 
as 
return (
	select *from Employee where CompanyId = @CompanyId
	);

	--function call
	select *from dbo.GetEmployeesByCompanyId(1)


--scaler valued function --returns only a single value









