CREATE PROCEDURE `Region_Merge`(in _IsDeleted BIT, in _Parent_RegionId INT, in _RegionType TINYINT, in _EnglishName NVARCHAR(200), in _LocalName NVARCHAR(200), in _IsoCode2 CHAR(2), in _IsoCode3 CHAR(3), in _AreaSqKm DECIMAL(15 , 3 ), in _Population BIGINT, in _Capital_CityId INT, in _Largest_CityId INT)
BEGIN
  DECLARE _CurrentId INT;
  DECLARE _UtcDateTime DATETIME;

  SELECT 
    Id
  FROM
    Region
  WHERE
    EnglishName = _EnglishName INTO _CurrentId;
  
  SET _UtcDateTime = NOW();

  IF (_CurrentId IS NULL) THEN
    BEGIN
      INSERT INTO Region
      (UtcCreated, UtcUpdated, IsDeleted, RegionType, EnglishName, LocalName, IsoCode2, IsoCode3, AreaSqKm, Population, Capital_CityId, Largest_CityId)
      VALUES
      (_UtcDateTime, _UtcDateTime, _IsDeleted, _RegionType, _EnglishName, _LocalName, _IsoCode2, _IsoCode3, _AreaSqKm, _Population, _Capital_CityId, _Largest_CityId);
    
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
  
  DELETE FROM Region_Region WHERE Child_RegionId = _CurrentId;
  
  INSERT INTO Region_Region
    (Parent_RegionId, Child_RegionId)
  VALUES
    (_Parent_RegionId, _CurrentId);
  
END