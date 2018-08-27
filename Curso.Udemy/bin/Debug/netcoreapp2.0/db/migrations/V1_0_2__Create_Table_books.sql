CREATE TABLE cursoUdemy.dbo.Books (
	Id int NOT NULL IDENTITY(1,1),
	Author varchar(50) NOT NULL,
	LaunchDate datetime NOT NULL,
	Price decimal(18,0) NOT NULL,
	Title varchar(500) NOT NULL
)
