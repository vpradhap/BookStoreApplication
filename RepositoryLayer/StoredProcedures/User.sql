--Create DB
create database BookStoreDB

--Use DB--
use BookStoreDB

--Table for User--
create table Users (
	UserId int identity (1,1) primary key,
	FullName varchar(100) not null,
	EmailId varchar(100) not null,
	Password varchar(100) not null,
	MobileNumber varchar(100) not null
);

--select table--
select * from Users

--Stored procedures for user--

--Register--
create procedure spRegister(
	@FullName varchar(100),
	@EmailId varchar(100),
	@Password varchar(100),
	@MobileNumber varchar(100)
	)
as
begin
	if(not exists(select EmailId from Users where EmailId=@EmailId))
	begin
		insert into Users
		values(@FullName,@EmailId,@Password,@MobileNumber);
	end
end

--Login--
create procedure spLogin(
	@EmailId varchar(100)
	)
as
begin
	select * from Users where EmailId=@EmailId;
end

--Forget Password--
create procedure spForget(
	@EmailId varchar(100)
	)
as
begin
	select * from Users where EmailId=@EmailId;
end

--reset password--
create procedure spResetPassword(
	@EmailId varchar(100),
	@Password varchar(100)
	)
as
begin
	update Users set Password = @Password where EmailId = @EmailId;
end