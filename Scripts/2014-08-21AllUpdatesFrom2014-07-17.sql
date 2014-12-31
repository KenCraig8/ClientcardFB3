------------------------------
Insert FB3 (DBVersion, ExeVersion, Comment, UpdateDate, UpdatedBy)
Values ( '08/21/2014', '2014.07.17'
,'Added Func ZeroFill'
,GetDate(), 'CSDG')
SELECT * FROM FB3 order BY EXEVersion
GO

CREATE FUNCTION [dbo].[ZeroFill] ( @TestValue int, @NbrDigits int)
RETURNS Varchar(4)
AS
BEGIN
DECLARE @result varchar(4)
  IF @NbrDigits = 2 AND @TestValue < 10
     SET @result = '0' + CAST(@TestValue as Varchar(4))
  ELSE IF @NbrDigits = 3 AND  @TestValue < 10
     SET @result = '00' + CAST(@TestValue as Varchar(4))
  ELSE IF @NbrDigits = 3 AND  @TestValue < 100
     SET @result = '0' + CAST(@TestValue as Varchar(4))
  ELSE IF @NbrDigits = 4 AND  @TestValue < 10
     SET @result = '000' + CAST(@TestValue as Varchar(4))
  ELSE IF @NbrDigits = 4 AND  @TestValue < 100
     SET @result = '00' + CAST(@TestValue as Varchar(4))
  ELSE IF @NbrDigits = 4 AND  @TestValue < 1000
     SET @result = '0' + CAST(@TestValue as Varchar(4))
  ELSE
     SET @result = CAST(@TestValue as Varchar(4))
    RETURN @result
END

go

GRANT EXECUTE ON [dbo].[ZeroFill] TO CCFB_User


/*
PRINT dbo.ZeroFill(1,2)
PRINT dbo.ZeroFill(1,3)
PRINT dbo.ZeroFill(1,4)
PRINT dbo.ZeroFill(99,3)
PRINT dbo.ZeroFill(99,4)
PRINT dbo.ZeroFill(997,4)



*/