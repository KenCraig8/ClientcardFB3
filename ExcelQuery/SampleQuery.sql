/*
update trxlog set ServiceGroup = 1 where lbsCommodity >0 and lbsStd =0
update trxlog set ServiceGroup = 2 where lbsOther >0 and lbsStd =0
update trxlog set ServiceGroup = 3 where lbsStd >0
*/


select sg.Type, tl.FiscalFirstTime, Count(*) NbrHH
, sum(Infants) Infants, sum(Youth + Teens + Eighteen) Children
, sum(Adults) Adults, sum(Seniors) Seniors
, sum(TotalFamily) TotalFamily
, sum(lbsStd + lbsOther + lbsCommodity) TotalLbs
from TrxLog tl
inner join parm_ServiceGroup sg ON tl.ServiceGroup = sg.ID
WHERE TrxDate Between '11/1/2013' and '11/30/2013' and tl.ServiceGroup = 1
GROUP BY sg.Type, tl.FiscalFirstTime
ORDER BY sg.Type, tl.FiscalFirstTime


select sg.Type, Count(*) NbrHH
, sum(Infants) Infants, sum(Youth + Teens + Eighteen) Children
, sum(Adults) Adults, sum(Seniors) Seniors
, sum(TotalFamily) TotalFamily
, sum(lbsStd + lbsOther + lbsCommodity) TotalLbs
from TrxLog tl
inner join parm_ServiceGroup sg ON tl.ServiceGroup = sg.ID
WHERE TrxDate Between '11/1/2013' and '11/30/2013' and tl.ServiceGroup <> 1
GROUP BY sg.Type
ORDER BY sg.Type
