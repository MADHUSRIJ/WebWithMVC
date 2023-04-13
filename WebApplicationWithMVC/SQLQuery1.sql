SELECT *  FROM LMS_MEMBERS;

GO

  CREATE OR ALTER PROCEDURE FETCH_MEMBERS 
  AS
  SELECT * FROM LMS_MEMBERS;

  GO

 CREATE OR ALTER PROCEDURE ADD_MEMBER 
  @memberId varchar(10),
  @memberName varchar(30),
  @city varchar(20),
  @membershipStatus varchar(15)
AS
BEGIN
  INSERT INTO LMS_MEMBERS 
    VALUES( @memberId,
            @memberName,
            @city,
            GETDATE(),
            DATEADD(YEAR, 1, GETDATE()),
            @membershipStatus
          )
END

  GO

  CREATE OR ALTER PROCEDURE EDIT_MEMBER 
  @memberId varchar(10),
  @memberName varchar(30),
  @city varchar(20),
  @membershipStatus varchar(15)
  AS
  BEGIN
	UPDATE LMS_MEMBERS SET MEMBER_NAME = @memberName, CITY = @city, MEMBERSHIP_STATUS = @membershipStatus 
	WHERE MEMBER_ID = @memberId;
  END

	GO

  CREATE OR ALTER PROCEDURE GET_MEMBER @memberId varchar(10)
  AS
  SELECT * FROM LMS_MEMBERS WHERE MEMBER_ID = @memberId

  GO


CREATE OR ALTER PROCEDURE DELETE_MEMBER @memberId varchar(10)
AS
DELETE FROM LMS_MEMBERS WHERE MEMBER_ID = @memberId

GO

