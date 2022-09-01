--Use DB--
use BookStoreDB

--table for wishlist--
create table Wishlist(
	WishlistId int identity (1,1) primary key,
	UserId int not null foreign key (UserId) references Users(UserId),
	BookId int not null foreign key (BookId) references Books(BookId)
	)

--select table--
select * from Wishlist;

--stored procedure for wishlist--
--add to wishlist--
create procedure spAddToWishlist(
	@BookId int,
	@UserId int
	)
as
begin
	if(not exists(select * from Wishlist where BookId=@BookId and UserId=@UserId))
	begin
		insert into Wishlist
		values(@UserId,@BookId);
	end
end

--remove from wishlist--
create procedure spRemoveFromWishlist(
	@WishlistId int
	)
as
begin
	delete from Wishlist where WishlistId = @WishlistId;
end

--get wishlist item--
create procedure spGetAllWishlistItem(
	@UserId int
	)
as
begin
	select wish.WishlistId,wish.BookId,wish.UserId,
		book.BookName,book.BookImage,book.AuthorName,book.DiscountPrice,book.OriginalPrice		
		from WishList wish inner join Books book
		on wish.BookId = book.BookId
		where wish.UserId = @UserId;
end