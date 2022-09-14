--Use DB--
use BookStoreDB

--Admin table--
create table Admin(
	AdminId int identity (1,1) primary key,
	FullName varchar(100) not null,
	EmailId varchar(100) not null,
	Password varchar(100) not null,
	MobileNumber bigint not null
);

--select--
select * from Admin

--inserting admin details--
insert into Admin 
values('Admin','adminlogin@gmail.com','Admin@123',1234567890);

--admin login--
create procedure spAdminLogin(
	@EmailId varchar(100),
	@Password varchar(100)
	)
as
begin
	select * from Admin where EmailId=@EmailId and Password = @Password;
end
