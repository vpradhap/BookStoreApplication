--Use DB--
use BookStoreDB

--order table--
create table Orders(
	OrderId int identity(1,1) primary key,
	OrderQty int not null,
	TotalPrice float not null,
	OrderDate Date not null,
	UserId INT NOT NULL FOREIGN KEY REFERENCES Users(UserId),
	BookId INT NOT NULL FOREIGN KEY REFERENCES Books(BookId),
	AddressId int not null FOREIGN KEY REFERENCES Address(AddressId)
	)

--select table--
select * from Orders

--stored procedures--
--add order--
create procedure spAddOrder(
	@UserId int,
	@BookId int,
	@AddressId int
	)
as
	declare @TotalPrice int;
	declare @OrderQty int;
begin
	set @TotalPrice = (select DiscountPrice from Books where BookId = @BookId); 
	set @OrderQty = (select BookInCart from Cart where BookId = @BookId); 
	if(exists(select * from Books where BookId = @BookId))
	begin
		Begin try
			Begin Transaction
				if((select BookQuantity from Books where BookId = @BookId)>= @OrderQty)
				begin
					insert into Orders values(@OrderQty,@TotalPrice*@OrderQty,GETDATE(),@UserId,@BookId,@AddressId);
					update Books set BookQuantity = (BookQuantity - @OrderQty) where BookId = @BookId;
					delete from Cart where BookId = @BookId and UserId = @UserId; 
				end
			commit Transaction
		End try

		Begin Catch
				rollback;
		End Catch
	end
end

--get all orders--
create procedure spGetAllOrders(@UserId int)
as
begin
	select 
		Orders.OrderId, Orders.UserId, Orders.AddressId, Books.BookId,
		Orders.TotalPrice, Orders.OrderQty, Orders.OrderDate,
		Books.BookName, Books.AuthorName, Books.BookImage
		from Books 
		inner join Orders on Orders.BookId = Books.BookId 
		where Orders.UserId = @UserId; 
end

--delete order--
create procedure spRemoveOrder(@OrderId int)
as
begin
	delete from Orders where OrderId=@OrderId;
end