CREATE TABLE [UserResponse](
	Id INT IDENTITY(1,1) PRIMARY KEY,
	IdUser INT,
	Response INT NOT NULL,
	DateResponse DATETIME NOT NULL
	CONSTRAINT FK_UserId FOREIGN KEY (IdUser)
    REFERENCES [User](Id)
)