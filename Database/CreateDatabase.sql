DROP DATABASE IF EXISTS Whib;

CREATE DATABASE IF NOT EXISTS Whib;

USE Whib;

CREATE TABLE IF NOT EXISTS Region (
    Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    UtcCreated DATETIME NOT NULL,
    UtcUpdated DATETIME NOT NULL,
    IsDeleted BIT NOT NULL,
    RegionType TINYINT NOT NULL,
    EnglishName NVARCHAR(200) NOT NULL UNIQUE,
    LocalName NVARCHAR(200),
    IsoCode2 NCHAR(2),
    IsoCode3 NCHAR(3),
    AreaSqKm DECIMAL(15 , 3 ),
    Population BIGINT,
    Capital_CityId INT,
    Largest_CityId INT
);

CREATE TABLE IF NOT EXISTS Organisation (
    Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    UtcCreated DATETIME NOT NULL,
    UtcUpdated DATETIME NOT NULL,
    IsDeleted BIT NOT NULL,
    ShortName NVARCHAR(50) NOT NULL UNIQUE,
    LongName NVARCHAR(200)
);

CREATE TABLE IF NOT EXISTS Traveller (
    Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    UtcCreated DATETIME NOT NULL,
    UtcUpdated DATETIME NOT NULL,
    IsDeleted BIT NOT NULL,    
    Email NVARCHAR(200) NOT NULL UNIQUE,
    OSType TINYINT NOT NULL,
    OSName NVARCHAR(50) NOT NULL,
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    BirthDate DATE,
    Nationality_RegionId INT,
    Residence_RegionId INT
);

CREATE TABLE IF NOT EXISTS City (
    Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    UtcCreated DATETIME NOT NULL,
    UtcUpdated DATETIME NOT NULL,
    IsDeleted BIT NOT NULL,    
    RegionId INT NOT NULL,
    EnglishName NVARCHAR(200) NOT NULL,
    LocalName NVARCHAR(200),
    Population BIGINT
);

CREATE TABLE IF NOT EXISTS Region_Region (
    Parent_RegionId INT NULL,
    Child_RegionId INT NOT NULL
);

CREATE UNIQUE INDEX IX_Region_Region_ParentChild ON Region_Region (Parent_RegionId, Child_RegionId);

CREATE INDEX IX_Region_Region_ChildRegion ON Region_Region (Child_RegionId);

CREATE TABLE IF NOT EXISTS Traveller_Region (
    TravellerId INT NOT NULL,
    RegionId INT NOT NULL
);

CREATE UNIQUE INDEX IX_Traveller_Region ON Traveller_Region (TravellerId, RegionId);

CREATE TABLE IF NOT EXISTS Organisation_Region (
    OrganisationId INT NOT NULL,
    RegionId INT NOT NULL
);

CREATE UNIQUE INDEX IX_Organisation_Region ON Organisation_Region (OrganisationId, RegionId);

SHOW TABLES;