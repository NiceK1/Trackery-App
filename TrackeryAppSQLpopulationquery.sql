create database TrackeryAppDB
go
use TrackeryAppDB
go
create table [User]
(
	Id UNIQUEIDENTIFIER primary Key default NEWID(),
	Username nvarchar (50) unique not null,
	[Password] nvarchar (100) not null,
	[Name]nvarchar (50) not null,
	LastName nvarchar (50) not null,
	Email  nvarchar (100) unique not null
)
go
insert into [User] values (NEWID(), 'admin', 'admin','Daniil','Serov', 'cdase001@edu.xamk.fi')
insert into [User] values (NEWID(), 'Rclick', 'kone','Sasha','Shlyapa', 'sashko@gmail.com')
insert into [User] values (NEWID(), 'Petu4', 'ursa','Nikita','Tunika', 'dota.ruin@gmail.com')
go

select *from [User]