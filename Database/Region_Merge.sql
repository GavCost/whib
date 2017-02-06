CREATE PROCEDURE `Region_Merge`(in _IsDeleted BIT, in _ParentId INT, in _RegionType TINYINT, in _EnglishName NVARCHAR(200), in _LocalName NVARCHAR(200), in _IsoCode2 CHAR(2), in _IsoCode3 CHAR(3), in _AreaSqKm DECIMAL(15 , 3 ), in _Population BIGINT, in _Capital_CityId INT, in _Largest_CityId INT)
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
      (_UtcDateTime, _UtcDateTime, _IsDeleted, _ParentId, _RegionType, _EnglishName, _LocalName, _IsoCode2, _IsoCode3, _AreaSqKm, _Population, _Capital_CityId, _Largest_CityId);
    
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
      ParentId = _ParentId,
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
  
END