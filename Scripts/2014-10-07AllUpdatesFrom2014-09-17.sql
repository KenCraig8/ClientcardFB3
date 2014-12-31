------------------------------
Insert FB3 (DBVersion, ExeVersion, Comment, UpdateDate, UpdatedBy)
Values ( '10/07/2014', '2014.10.07'
,'Update SP CSFPListByPeriod
'
,GetDate(), 'CSDG')
------------------------------------------
/****** Object:  StoredProcedure [dbo].[CSFPListByPeriod]    Script Date: 10/07/2014 10:13:13 PM ******/
DROP PROCEDURE [dbo].[CSFPListByPeriod]
GO

/****** Object:  StoredProcedure [dbo].[CSFPListByPeriod]    Script Date: 10/07/2014 10:13:13 PM ******/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CSFPListByPeriod] (	@Period char(6) = '', @SortBy varchar(200) )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON
DECLARE @sql varchar(5000)
SELECT @sql = '
		SELECT hhm.HouseholdID
			 , hhm.CSFP
			 , hh.Inactive
			 , hh.Address
			 , hh.AptNbr
			 , hh.City
			 , hhm.CSFPExpiration
			 , CASE WHEN cl.DistributionMethod IS NULL THEN CASE WHEN hhm.CSFPRoute is NULL THEN ''PkUp'' ELSE DfltRoute.Type END ELSE ActRoute.Type END [Route]
			 , CASE WHEN cl.DistributionMethod IS NULL THEN CASE WHEN hhm.CSFPRoute is NULL THEN 0 ELSE hhm.CSFPRoute END ELSE cl.DistributionMethod END [RouteAsInt]
			 , cl.TrxDate
			 , cl.Lbs
			 , hhm.ID hhmID
			 , cl.ID LogId
			 , ltrim(rtrim(hhm.LastName)) + '', '' + ltrim(rtrim(hhm.FirstName)) colNameLF
			 , ltrim(rtrim(hhm.FirstName)) + '' '' + ltrim(rtrim(hhm.LastName)) colNameFL
			 , CASE WHEN hhm.Phone IS NULL THEN hh.Phone 
                    WHEN hhm.Phone = '''' THEN hh.Phone
                    ELSE hhm.Phone
               END Phone 
             , (SELECT MAX(TrxDate) FROM CSFPLog WHERE MemID = hhm.ID AND Period < ' + @Period + ') PrevService
		  FROM HouseholdMembers hhm 
		 INNER JOIN Household hh on hhm.HouseholdID = hh.ID
		  LEFT JOIN CSFPLog cl ON hhm.ID = cl.MemId AND cl.Period = ' + @Period + '
		  LEFT JOIN parm_CSFPRoutes DfltRoute ON hhm.CSFPRoute = DfltRoute.Id
		  LEFT JOIN parm_CSFPRoutes ActRoute ON cl.DistributionMethod = ActRoute.Id
		 WHERE hhm.ID IN (SELECT hhm.ID hhmId FROM HouseholdMembers hhm WHERE CSFP = 1 AND hhm.Inactive = 0 
		            UNION SELECT MemId hhmId FROM CSFPLog WHERE Period = ' + @Period + ')
		   AND hh.Inactive = 0 '
		 + @SortBy  

EXECUTE(@sql)
END
GO
GRANT EXECUTE ON [dbo].[CSFPListByPeriod] TO [CCFB_User]
GO
----------------------------

/*
DECLARE	@return_value int
 EXEC	@return_value = [dbo].[CSFPListByPeriod]
		@Period = N'201410',
		@SortBy = N'ORDER BY colNameLF'
*/