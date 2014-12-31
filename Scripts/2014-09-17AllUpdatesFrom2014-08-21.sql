------------------------------
Insert FB3 (DBVersion, ExeVersion, Comment, UpdateDate, UpdatedBy)
Values ( '09/17/2014', '2014.09.22'
,'Preferences Add EnableFTscale, LbsIncludeCommodityWt, AllowLbsManualEntry, EnableSchSupply, DefaultFMIDType
'
,GetDate(), 'CSDG')

SELECT * FROM FB3 ORDER BY EXEVersion, ID
GO
------------------------------
IF NOT EXISTS(SELECT * FROM Preferences WHERE FldName = 'EnableFTScale')
BEGIN
	print 'Add Preferences EnableFTScale'
	INSERT INTO Preferences (FldName,FldVal,BoolVal,EditForm,EditLabel,EditTip,FldType,ControlType,ControlWidth)
	VALUES ('EnableFTScale','0',0,'Features','Enable Fast Track Scale Interface','Enable Fast Track Scale Interface','Yes|No','combo',80)
END
GO
------------------------------
IF NOT EXISTS(SELECT * FROM Preferences WHERE FldName = 'LbsIncludeCommodityWt')
BEGIN
	print 'Add Preferences LbsIncludeCommodityWt'
	INSERT INTO Preferences (FldName,FldVal,BoolVal,EditForm,EditLabel,EditTip,FldType,ControlType,ControlWidth)
	VALUES ('LbsIncludeCommodityWt','0',0,'Features','Enable Scale Lbs Include TEFAP','Enable Scale Lbs Include TEFAP','Yes|No','combo',80)
END
GO
------------------------------
IF NOT EXISTS(SELECT * FROM Preferences WHERE FldName = 'AllowLbsManualEntry')
BEGIN
	print 'Add Preferences AllowLbsManualEntry'
	INSERT INTO Preferences (FldName,FldVal,BoolVal,EditForm,EditLabel,EditTip,FldType,ControlType,ControlWidth)
	VALUES ('AllowLbsManualEntry','0',0,'Features','Allow Manual Entry of Pounds Served','Allow Manual Entry of Pounds Served','Yes|No','combo',80)
END
GO
------------------------------
IF NOT EXISTS(SELECT * FROM Preferences WHERE FldName = 'EnableSchSupply')
BEGIN
	print 'Add Preferences EnableSchSupply'
	INSERT INTO Preferences (FldName,FldVal,BoolVal,EditForm,EditLabel,EditTip,FldType,ControlType,ControlWidth)
	VALUES ('EnableSchSupply','0',0,'Features','Enable School Supply Module','Enable School Supply Module','Yes|No','combo',80)
END
GO
------------------------------
IF NOT EXISTS(SELECT * FROM Preferences WHERE FldName = 'DefaultFMIDType')
BEGIN
	print 'Add Preferences DefaultFMIDType'
	INSERT INTO Preferences (FldName,FldVal,BoolVal,EditForm,EditLabel,EditTip,FldType,ControlType,ControlWidth)
	VALUES ('DefaultFMIDType','0',0,'ClientOptions','Default Family Member ID Type','Default Family Member ID Type','Yes|No','combo',80)
END
GO
------------------------------
