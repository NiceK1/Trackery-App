drop database if exists TrackeryAppDB
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
	Email  nvarchar (100) unique not null,
	Role NVARCHAR(50) NOT NULL DEFAULT 'Employee',
	Status BIT NOT NULL DEFAULT 1
)
go
insert into [User] values (NEWID(), 'admin', 'admin','Daniil','Serov', 'cdase001@edu.xamk.fi', 'admin', 1)
insert into [User] values (NEWID(), 'Rclick', 'kone','Sasha','Shlyapa', 'sashko@gmail.com', 'boss', 1)
insert into [User] values (NEWID(), 'Petu4', 'ursa','Nikita','Tunika', 'dota.ruin@gmail.com', 'employee', 0)
go
create table [Delivery]
(
	Id UNIQUEIDENTIFIER primary key default NEWID(),
	DeliveryEstimate DATETIME2 NOT NULL,
	Sender NVARCHAR(50) NOT NULL, 
	IsSent BIT NOT NULL DEFAULT 0,
	IsReceived BIT NOT NULL DEFAULT 0
)
go

insert into [Delivery] values (NEWID(), '2025-05-17 14:30:00', 'Nokia', 1, 0)
insert into [Delivery] values (NEWID(), '2025-03-01 12:20:00', 'KONE', 1, 1)
insert into [Delivery] values (NEWID(), '2025-05-27 13:20:00', 'Rightware', 0, 0)
insert into [Delivery] values (NEWID(), GETDATE(), 'Apple', 1, 1)
create table [Stock]
(
	Id UNIQUEIDENTIFIER primary key default NEWID(),
	Name NVARCHAR(100) NOT NULL,
	SKU NVARCHAR(50) UNIQUE NOT NULL,
	EAN NVARCHAR(20) UNIQUE NOT NULL,
	Quantity INT NOT NULL DEFAULT 0,
	Location NVARCHAR(100),
	LastUpdated DATETIME2 NOT NULL DEFAULT GETDATE(),
	AdditionalInfo NVARCHAR(255)
)
go

insert into [Stock] (Name, SKU, EAN, Quantity, Location, LastUpdated, AdditionalInfo)
values 
('Industrial Fan', 'FAN-IND-001', '5901234123457', 100, 'Warehouse A1', GETDATE(), 'High-power fan unit'),

('Conveyor Belt Motor', 'MTR-CB-004', '4006381333931', 25, 'Shelf B3', '2025-04-01 10:00:00', 'Used for heavy-load conveyor belts'),

('Welding Machine', 'WLD-MCH-789', '5012345678900', 5, 'Zone C2', '2025-04-05 12:30:00', 'Requires regular maintenance'),

('Safety Helmets', 'HLM-SFT-012', '9780201379624', 200, 'Safety Rack', GETDATE(), 'Batch includes yellow and orange colors')
go

select * from [User]
select * from [Delivery]
select * from [Stock]