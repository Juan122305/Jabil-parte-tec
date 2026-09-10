IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'MoviesDirectorDB')
BEGIN
    CREATE DATABASE MoviesDirectorDB;
END
GO

USE MoviesDirectorDB;
GO

IF OBJECT_ID('dbo.Movies', 'U') IS NOT NULL
    DROP TABLE dbo.Movies;
GO

IF OBJECT_ID('dbo.Director', 'U') IS NOT NULL
    DROP TABLE dbo.Director;
GO

CREATE TABLE dbo.Director
(
    PKDirector  INT             NOT NULL IDENTITY(1,1),
    Name        VARCHAR(100)    NULL,
    Age         INT             NULL,
    Active      BIT             NULL,
    CONSTRAINT PK_Director PRIMARY KEY (PKDirector)
);
GO

CREATE TABLE dbo.Movies
(
    PKMovies    INT             NOT NULL IDENTITY(1,1),
    Name        VARCHAR(100)    NULL,
    Gender      VARCHAR(50)     NULL,
    Duration    TIME            NULL,
    FKDirector  INT             NOT NULL,
    CONSTRAINT PK_Movies PRIMARY KEY (PKMovies),
    CONSTRAINT FK_Movies_Director FOREIGN KEY (FKDirector)
        REFERENCES dbo.Director (PKDirector)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);
GO

CREATE INDEX IX_Movies_FKDirector ON dbo.Movies (FKDirector);
GO

INSERT INTO dbo.Director (Name, Age, Active) VALUES
    ('Christopher Nolan', 55, 1),
    ('Danny y Michael Philippou', 33, 1),
    ('Shawn Levy', 57, 1);
GO

INSERT INTO dbo.Movies (Name, Gender, Duration, FKDirector) VALUES
    ('The Odyssey', 'Action/Fantasy', '02:53:00', 1),
    ('Haz que Regrese', 'Horror', '01:44:00', 2),
    ('Deadpool y Wolverine', 'Action/Comedy', '02:07:00', 3);
GO