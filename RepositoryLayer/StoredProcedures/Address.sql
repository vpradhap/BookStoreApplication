--Use DB--
use BookStoreDB

--Table for Address type--
create table AddressType(
	TypeId int identity(1,1) primary key,
	AddType varchar(100)
	)

--adding types--
insert into AddressType values('Home');
insert into AddressType values('Work');
insert into AddressType values('Other');

--select table--
select * from AddressType;

--table for Address info--

create table Address(
	AddressId int identity(1,1) primary key,
	Address varchar(max) not null,
	City varchar(100) not null,
	State varchar(100) not null,
	TypeId int not null foreign key (TypeId) references AddressType(TypeId),
	UserId int not null foreign key (UserId) references Users(UserId)
	)

--select table--
select * from Address;

--stored procedure for address--
--add address--
create procedure spAddAddress(
	@Address varchar(max),
	@City varchar(100),
	@State varchar(100),
	@TypeId int,
	@UserId int
	)
as
begin
	insert into Address
	values(@Address,@City,@State,@TypeId,@UserId);
end

--update address--
create procedure spUpdateAddress(
	@AddressId int,
	@Address varchar(max),
	@City varchar(100),
	@State varchar(100),
	@TypeId int,
	@UserId int
	)
as
begin
	update Address set
	Address=@Address,City=@City,State=@State,TypeId=@TypeId where UserId=@UserId and AddressId=@AddressId;
end

--get all address of user--
create procedure spGetAllAddress(
	@UserId int
	)
as
begin
	select * from Address where UserId=@UserId;
end