DROP DATABASE IF EXISTS Whib;

CREATE DATABASE IF NOT EXISTS Whib;

USE Whib;

CREATE TABLE IF NOT EXISTS Region (
    Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    UtcCreated DATETIME NOT NULL,
    UtcUpdated DATETIME NOT NULL,
    IsDeleted BIT NOT NULL,
    ParentId INT,
    RegionType TINYINT NOT NULL,
    EnglishName NVARCHAR(200) NOT NULL UNIQUE,
    LocalName NVARCHAR(200),
    IsoCode2 NCHAR(2),
    IsoCode3 NCHAR(3),
    AreaSqKm DECIMAL(15 , 3 ) NOT NULL,
    Population BIGINT NOT NULL,
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

CREATE UNIQUE INDEX IX_City ON City (RegionId, EnglishName);

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

DELIMITER |

DROP FUNCTION IF EXISTS `Region_GetNameFromId`|

CREATE DEFINER=`root`@`localhost` FUNCTION `Region_GetNameFromId`(_Id INT) RETURNS NVARCHAR(200)
BEGIN

DECLARE _Name NVARCHAR(200);
SELECT 
  EnglishName
FROM
  Region
WHERE
  Id = _Id INTO _Name;

RETURN _Name;
END|

DROP FUNCTION IF EXISTS `Region_GetIdFromName`|

CREATE DEFINER=`root`@`localhost` FUNCTION `Region_GetIdFromName`(_Name NVARCHAR(200)) RETURNS INT
BEGIN

DECLARE _Id INT;
SELECT 
  Id
FROM
  Region
WHERE
  EnglishName = _Name INTO _Id;

RETURN _Id;
END|

DROP PROCEDURE IF EXISTS `Region_Merge`|

CREATE DEFINER=`root`@`localhost` PROCEDURE `Region_Merge`(in _IsDeleted BIT, in _ParentName NVARCHAR(200), in _RegionType TINYINT, in _EnglishName NVARCHAR(200), in _LocalName NVARCHAR(200), in _IsoCode2 CHAR(2), in _IsoCode3 CHAR(3), in _AreaSqKm DECIMAL(15 , 3 ), in _Population BIGINT, in _Capital_CityId INT, in _Largest_CityId INT)
BEGIN
  DECLARE _CurrentId INT;
  DECLARE _UtcDateTime DATETIME;

  SELECT 
    Id
  FROM
    Region
  WHERE
    EnglishName = _EnglishName INTO _CurrentId;
  
  SET _UtcDateTime = UTC_TIMESTAMP();

  IF (_CurrentId IS NULL) THEN
    BEGIN
      INSERT INTO Region
      (UtcCreated, UtcUpdated, IsDeleted, ParentId, RegionType, EnglishName, LocalName, IsoCode2, IsoCode3, AreaSqKm, Population, Capital_CityId, Largest_CityId)
      VALUES
      (_UtcDateTime, _UtcDateTime, _IsDeleted, Region_GetIdFromName(_ParentName), _RegionType, _EnglishName, _LocalName, _IsoCode2, _IsoCode3, _AreaSqKm, _Population, _Capital_CityId, _Largest_CityId);
    
      SELECT 
        Id
      FROM
        Region
      WHERE
        EnglishName = _EnglishName INTO _CurrentId;
    END;
  ELSE
    UPDATE Region
    SET 
      UtcUpdated = _UtcDateTime,
      IsDeleted = _IsDeleted,
      ParentId = Region_GetIdFromName(_ParentName),
      RegionType = _RegionType,
      EnglishName = _EnglishName,
      LocalName = _LocalName,
      IsoCode2 = _IsoCode2,
      IsoCode3 = _IsoCode3,
      AreaSqKm = _AreaSqKm,
      Population = _Population,
      Capital_CityId = _Capital_CityId,
      Largest_CityId = _Largest_CityId
    WHERE
      Id = _CurrentId;
  END IF;
  
END|

DROP PROCEDURE IF EXISTS `City_Merge`|

CREATE DEFINER=`root`@`localhost` PROCEDURE `City_Merge`(in _IsDeleted BIT, in _RegionName NVARCHAR(200), in _EnglishName NVARCHAR(200), in _LocalName NVARCHAR(200), in _Population BIGINT)
BEGIN
  DECLARE _CurrentId INT;
  DECLARE _UtcDateTime DATETIME;
  DECLARE _RegionId INT;
  
  SELECT Region_GetIdFromName(_RegionName) INTO _RegionId;

  SELECT 
    Id
  FROM
    City
  WHERE
    RegionId = _RegionId AND EnglishName = _EnglishName INTO _CurrentId;
  
  SET _UtcDateTime = UTC_TIMESTAMP();

  IF (_CurrentId IS NULL) THEN
    BEGIN
      INSERT INTO City
      (UtcCreated, UtcUpdated, IsDeleted, RegionId, EnglishName, LocalName, Population)
      VALUES
      (_UtcDateTime, _UtcDateTime, _IsDeleted, _RegionId, _EnglishName, _LocalName, _Population);
    
      SELECT 
        Id
      FROM
        City
      WHERE
        RegionId = _RegionId AND EnglishName = _EnglishName INTO _CurrentId;
    END;
  ELSE
    UPDATE City
    SET 
      UtcUpdated = _UtcDateTime,
      IsDeleted = _IsDeleted,
      RegionId = _RegionId,
      EnglishName = _EnglishName,
      LocalName = _LocalName,
      Population = _Population
    WHERE
      Id = _CurrentId;
  END IF;
  
END|