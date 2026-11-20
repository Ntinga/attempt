IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'RaceDayDB')
BEGIN
    CREATE DATABASE RaceDayDB;
END;
GO
USE RaceDayDB;
GO

CREATE TABLE Roles (
    RoleId INT IDENTITY(1,1) PRIMARY KEY,
    RoleName VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    FullName VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL,
    RoleId INT NOT NULL,
    CONSTRAINT FK_Users_Roles FOREIGN KEY (RoleId) REFERENCES Roles(RoleId)
);

CREATE TABLE Events (
    EventId INT IDENTITY(1,1) PRIMARY KEY,
    Title VARCHAR(100) NOT NULL,
    EventDate DATETIME NOT NULL,
    Location VARCHAR(150) NOT NULL,
    OrganiserId INT NOT NULL,
    CONSTRAINT FK_Events_Organiser FOREIGN KEY (OrganiserId) REFERENCES Users(UserId)
);

CREATE TABLE Categories (
    CategoryId INT IDENTITY(1,1) PRIMARY KEY,
    EventId INT NOT NULL,
    CategoryName VARCHAR(50) NOT NULL,
    DistanceKM DECIMAL(5,2) NOT NULL,
    CONSTRAINT FK_Categories_Events FOREIGN KEY (EventId) REFERENCES Events(EventId)
);

CREATE TABLE Enrolments (
    EnrolmentId INT IDENTITY(1,1) PRIMARY KEY,
    CategoryId INT NOT NULL,
    ParticipantId INT NOT NULL,
    EnrolmentDate DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_Enrolments_Categories FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId),
    CONSTRAINT FK_Enrolments_Users FOREIGN KEY (ParticipantId) REFERENCES Users(UserId)
);

CREATE TABLE Results (
    ResultId INT IDENTITY(1,1) PRIMARY KEY,
    EnrolmentId INT NOT NULL UNIQUE,
    FinishTime VARCHAR(20) NOT NULL,
    Position INT NOT NULL,
    CONSTRAINT FK_Results_Enrolments FOREIGN KEY (EnrolmentId) REFERENCES Enrolments(EnrolmentId)
);

INSERT INTO Roles (RoleName) VALUES ('Organiser'), ('Participant');

INSERT INTO Users (FullName, Email, PasswordHash, RoleId) VALUES 
('Alice Smith', 'alice@race.com', 'hash123', 1),
('Bob Jones', 'bob@race.com', 'hash123', 1),
('Charlie Brown', 'charlie@runner.com', 'hash123', 2),
('Diana Prince', 'diana@runner.com', 'hash123', 2);

INSERT INTO Events (Title, EventDate, Location, OrganiserId) VALUES
('City Marathon', '2026-10-10', 'Downtown Main St', 1),
('Trail Run Challenge', '2026-11-05', 'Mountain Park', 1),
('Coastal Cycling 50K', '2026-12-01', 'Beachfront Promenade', 2);

INSERT INTO Categories (EventId, CategoryName, DistanceKM) VALUES
(1, 'Full Marathon', 42.2),
(2, 'Trail 10K', 10.0),
(3, '50K Cycle', 50.0);

INSERT INTO Enrolments (CategoryId, ParticipantId) VALUES
(1, 3),
(2, 4);

INSERT INTO Results (EnrolmentId, FinishTime, Position) VALUES
(1, '03:45:12', 12);
