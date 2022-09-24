Create procedure GetUserWithNameAndEmail 
(  
@username varchar (100),  
@email varchar (100),
@createdat datetime,
@updatedat datetime
)  
as  
begin  
select * from Users where Username = @username and Email = @email
End
Go

Create procedure GetUserDetailUsingUserId 
(  
@userid int
)  
as  
begin  
select * from UserDetails where UserID = @userid
End
Go

Create procedure GetTokenUsingUserIdAndRecoveryToken
(  
@userid int,
@recoverytoken nvarchar(200)
)  
as  
begin  
select * from Tokens where UserId = @userid and RecoveryToken = @recoverytoken
End
Go

Create procedure DeleteTokenUsingUserId
(  
@userid int
)  
as  
begin  
delete from Tokens where UserId = @userid
End
Go

create procedure InsertRecoveryToken
(
@userid int,
@recoverytoken nvarchar(200),
@expiredat datetime
)
as
begin
IF EXISTS (SELECT * FROM Tokens WHERE UserId = @userid)
begin
update Tokens set RecoveryToken = @recoverytoken, ExpiredAt = @expiredat where UserId = @userid
end
else
begin
insert into Tokens values(@userid, @recoverytoken, @expiredat)
end
end
go

alter table Tokens
add unique(UserId)

alter table UserDetails
add unique(UserId)

create table Tokens (
Id int identity not null,
UserId int not null,
RecoveryToken nvarchar(200) not null,
ExpiredAt datetime not null,
primary key(Id),
foreign key(UserId) references Users(Id)
)

create table UserDetails (
Id int identity not null,
UserId int not null,
PhoneNumber nvarchar(10) not null,
Address nvarchar(200) not null,
Pincode nvarchar(10) not null,
State nvarchar(10) not null,
primary key(Id),
foreign key(UserId) references Users(Id)
)

create table Items (
Id int identity not null,
Name nvarchar(50) null,
Description nvarchar(200) not null,
Amount float(53) not null,
ExpiredAt datetime not null,
CreatedAt datetime not null,
UpdatedAt datetime not null,
primary key(Id)
)

Create procedure InsertAnItem
(  
@name nvarchar (50),  
@description nvarchar (200),
@amount FLOAT(53),
@expiredat datetime,
@createdat datetime,
@updatedat datetime
)  
as  
begin  
insert into Items values(@name, @description, @amount, @expiredat, @createdat, @updatedat)
End
Go



Create procedure UpdatedAnItemUsingId
(  
@id int,
@name nvarchar (50),  
@description nvarchar (200),
@amount FLOAT(53),
@expiredat datetime,
@updatedat datetime
)  
as  
begin  
update Items set Name = @name, Description = @description, Amount = @amount, UpdatedAt = @updatedat, ExpiredAt = @expiredat where Id = @id
End
Go

Create procedure GetAnItemUsingId
(  
@id int
)  
as  
begin  
Select * from Items where Id = @id
End
Go

Create procedure GetActiveItems
(  
@currentdate datetime
)  
as  
begin  
Select * from Items where ExpiredAt > @currentdate
End
Go

Create procedure GetInactiveItems
(  
@currentdate datetime
)  
as  
begin  
Select * from Items where ExpiredAt <= @currentdate and Id in (select ItemId from Winners)
End
Go

Create procedure GetItemsToChooseWinner
(  
@currentdate datetime
)  
as  
begin  
Select * from Items where ExpiredAt <= @currentdate and Id not in (select ItemId from Winners)
End
Go

Create procedure DeleteAnItemUsingId
(  
@id int
)  
as  
begin  
delete from Items where Id = @id
End
Go

create table Investments (
Id int identity not null,
UserId int not null,
ItemId int not null,
CryptId nvarchar(100),
CreatedAt datetime not null,
primary key(Id),
foreign key(UserId) references Users(Id),
foreign key(ItemId) references Items(Id)
)

Create procedure InsertAnInvestment
(  
@userid int,  
@itemid int,
@cryptid nvarchar(100),
@createdat datetime
)  
as  
begin  
insert into Investments values(@userid, @itemid, @cryptid, @createdat)
End
Go

Create procedure GetInvestmentUsingUserIdAndItemId
(  
@userid int,
@itemid int
)  
as  
begin  
Select * from Investments where UserId = @userid and ItemId = @itemid
End
Go

Create procedure CountInvestmentsUsingItemId
(  
@itemid int
)  
as  
begin  
Select count(*) as Count from Investments where ItemId = @itemid
End
Go

Create procedure GetInvestmentsOfUniqueItemId
(  
@itemid int
)  
as  
begin  
Select * from Investments where ItemId = @itemid
End
Go

create table Winners (
Id int identity not null,
UserId int not null,
ItemId int not null,
CreatedAt datetime not null,
primary key(Id),
foreign key(UserId) references Users(Id),
foreign key(ItemId) references Items(Id)
)

Create procedure InsertAWinner
(  
@userid int,  
@itemid int,
@createdat datetime
)  
as  
begin  
insert into Winners values(@userid, @itemid, @createdat)
End
Go

Create procedure GetWinnerUsingItemId
(    
@itemid int
)  
as  
begin  
select * from Winners where ItemId = @itemid
End
Go