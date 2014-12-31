select * from preferences where fldname IN  ('DefaultCity', 'FoodbankName', 'FiscalYearStartMonth','DefaultFMIDType')

SELECT 'TL', min(TrxDate), max(trxDate), Count(*) from TrxLog
UNION
SELECT 'VL', min(TrxDate), max(trxDate), Count(*) from VoucherLog
UNION
SELECT 'FD', min(TrxDate), Max(TrxDate), Count(*) From FoodDonations
UNION
SELECT 'VH', min(TrxDate), Max(TrxDate), Count(*) From VolunteerHours

SELECT * from FB3 ORDER BY EXEVersion
SELECT * FROM [MonthlyReports] ORDER BY ReportName
