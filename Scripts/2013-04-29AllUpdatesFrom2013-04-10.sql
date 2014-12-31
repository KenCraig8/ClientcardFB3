------------------------------

Insert FB3 (DBVersion, ExeVersion, Comment, UpdateDate, UpdatedBy)
Values ( '04/29/2013', '2013.04.28'
,' Prefs: CaptureTEFAPSignature
 ,  Func: GetFiscalYear'
,GetDate(), 'CSDG')

SELECT * FROM FB3 ORDER BY EXEVersion
GO
IF NOT EXISTS(SELECT * FROM Preferences WHERE FldName = 'CaptureTEFAPSignature')
BEGIN
	Print 'Preference CaptureTEFAPSignature'
	INSERT INTO Preferences (FldName,FldVal,BoolVal,EditForm,EditLabel,EditTip,FldType,ControlType,ControlWidth)
	VALUES ('CaptureTEFAPSignature','NO',0,'Features','Capture Signature','Always Capture TEFAP Signatures with Electronic Signature Pad','Yes|No','checkbox',80)
END
GO
------------------------------
------------------------------------------------
--Updated 02/2012
--Updated 03/11/2013
--Updated 04/25/2013
/****** Object:  UserDefinedFunction [dbo].[GetFiscalYear]    Script Date: 03/01/2011 14:43:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetFiscalYear]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetFiscalYear]
GO

/****** Object:  UserDefinedFunction [dbo].[GetFiscalYear]    Script Date: 03/01/2011 14:43:40 ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[GetFiscalYear] (@Period char(6))
RETURNS char(4)
AS
BEGIN
DECLARE @FiscalYear CHAR(4), @FiscalMonthStart int, @PeriodMonth int
SELECT @FiscalMonthStart = CAST(FldVal AS int) FROM Preferences WHERE FldName = 'FiscalYearStartMonth'
IF @FiscalMonthStart = 1 
    SELECT @FiscalYear = LEFT(@Period,4)
ELSE
BEGIN
	SELECT @PeriodMonth = CAST(RIGHT(@Period,2) as int)
	SELECT @FiscalYear = 
          CASE WHEN @PeriodMonth >= @FiscalMonthStart 
                    THEN CAST(CAST(LEFT(@Period,4) AS INT) + 1 AS CHAR(4)) 
               ELSE LEFT(@Period,4) 
          END
END
RETURN @FiscalYear

END

GO

GRANT EXECUTE ON [dbo].[GetFiscalYear] TO [CCFB_User]