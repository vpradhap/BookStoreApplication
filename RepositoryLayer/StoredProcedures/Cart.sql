--Use DB--
use BookStoreDB

--cart table--
create table Cart(
	CartId int identity(1,1) primary key,
	BookInCart int default 1,
	UserId int not null foreign key (UserId) references Users(UserId),
	BookId int not null foreign key (BookId) references Books(BookId)
)

--select table--
select * from Cart

--stored procedure for cart--
--Add to cart--
create procedure spAddToCart(
	@BookId int,
	@BookInCart int,
	@UserId int
	)
as
begin
	if(not exists(select * from Cart where BookId=@BookId and UserId=@UserId))
	begin
		insert into Cart(BookId,UserId)
		values(@BookId,@UserId);
	end
end

--update cart--
create procedure spUpdateCart(
	@CartId int,
	@BookInCart int
	)
as
begin
	update Cart set BookInCart=@BookInCart where CartId=@CartId;
end

--remove from cart--
create procedure spRemoveFromCart(
	@CartId int
	)
as
begin
	delete from Cart where CartId=@CartId;
end

--get all cart items--
create procedure spGetAllCartItem(
	@UserId int
	)
as
begin
	select cart.CartId,cart.BookId,cart.BookInCart,cart.UserId,
		book.BookName,book.BookImage,book.AuthorName,book.DiscountPrice,book.OriginalPrice from Cart cart inner join Books book 
		on book.BookId=cart.BookId where cart.UserId = @UserId;
end