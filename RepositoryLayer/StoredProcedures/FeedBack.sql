--Use DB--
use BookStoreDB

--Table--
create table Feedback(
	FeedbackId int identity (1,1) primary key,
	Rating float not null,
	Comment varchar(max) not null,
	BookId int not null foreign key (BookId) references Books(BookId),
	UserId int not null foreign key (UserId) references Users(UserId)
	)

--select table--
select * from Feedback

--Stored procedures--
--add feedback--
create procedure spAddFeedback(
	@Rating float,
	@Comment varchar(max),
	@BookId int,
	@UserId int
	)
as
	declare @TotalRating float;
begin
	if(not exists(select * from Feedback where BookId=@BookId and UserId=@UserId))
	begin
		insert into Feedback values(@Rating,@Comment,@BookId,@UserId);

		select @TotalRating = avg(@Rating) from Books where BookId = @BookId;

		Update Books set Rating = @TotalRating, ReviewerCount = (ReviewerCount+1) where BookId=@BookId;
	end
end

--get feedback--
create procedure spGetFeedback(
	@BookId int
	)
as
begin
	select Feedback.FeedbackId,Feedback.Comment,Feedback.BookId,Feedback.Rating,Feedback.UserId,Users.FullName
	from Users
	inner join Feedback
	on Feedback.UserId = Users.UserId where BookId=@BookId;
end

truncate table Feedback