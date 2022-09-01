--Use DB--
use BookStoreDB

--Book table--
create table Books(
	BookId int identity(1,1) primary key,
	BookName varchar(100) not null,
	AuthorName varchar(100) not null,
	Rating float,
	ReviewerCount int,
	DiscountPrice int not null,
	OriginalPrice int not null,
	BookDetail varchar(max) not null,
	BookImage varchar(max) not null,
	BookQuantity int not null 
)

--select table--
select * from Books

--stored procedure for Books--
--add books--
create procedure spAddbook(
	@BookName varchar(100),
	@AuthorName varchar(100),
	@Rating float,
	@ReviewerCount int,
	@DiscountPrice int,
	@OriginalPrice int,
	@BookDetail varchar(max),
	@BookImage varchar(max),
	@BookQuantity int
	)
as
begin
	insert into Books
	values(@BookName,@AuthorName,@Rating,@ReviewerCount,@DiscountPrice,@OriginalPrice,@BookDetail,@BookImage,@BookQuantity);
end

--Update book--
create procedure spUpdateBook(
	@BookId int,
	@BookName varchar(100),
	@AuthorName varchar(100),
	@Rating float,
	@ReviewerCount int,
	@DiscountPrice int,
	@OriginalPrice int,
	@BookDetail varchar(max),
	@BookImage varchar(max),
	@BookQuantity int
	)
as 
begin
	update Books set 
	BookName= @BookName,
	AuthorName= @AuthorName,
	Rating = @Rating,
	ReviewerCount= @ReviewerCount,
	DiscountPrice = @DiscountPrice,
	OriginalPrice = @OriginalPrice,
	BookDetail= @BookDetail,
	BookImage = @BookImage,
	BookQuantity = @BookQuantity
	where BookId = @BookId;
end
		
--Delete book--
create procedure spDeleteBook(
	@BookId int
	)
as
begin
	delete from Books where BookId=@BookId;
end	

--Get all books--
create procedure spGetAllBooks
as
begin
	select * from Books;
end

--Get book by id--
create procedure spGetBookById(
	@BookId int
	)
as
begin
	select * from Books where BookId=@BookId;
end	