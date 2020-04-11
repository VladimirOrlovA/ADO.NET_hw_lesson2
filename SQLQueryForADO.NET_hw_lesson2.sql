USE MCS1

-----------------------------------------
 CREATE TABLE TablesModel (intModelID int, strName nvarchar(50), intManufacturerID int, intSMCSFamilyID int, strImage nvarchar(50), )

 -----------------------------------------

 CREATE TABLE TablesManufacturer (intManufacturerID int, strName nvarchar(50), )

 -----------------------------------------

 CREATE TABLE TablesStopReason (intStopReason int,strReason nvarchar(50),bitDowntime bit,bitUnscheduled bit,bitPMStoppages bit,bitScheduledRepairsAndOther bit,intLocationId int, )

 -----------------------------------------

 -----------------------------------------
 INSERT INTO TablesModel
 VALUES
(1, 'Toyota', 1, 1,'imageToyota')

 -----------------------------------------

 INSERT INTO TablesManufacturer 
 VALUES
(1, 'strName' )

 -----------------------------------------

 INSERT INTO TablesStopReason
 VALUES 
(1, 'Found Deffect', 1, 1, 1, 1, 123 )

 -----------------------------------------
 -----------------------------------------

 SELECT * FROM TablesModel
 SELECT * FROM TablesManufacturer
 SELECT * FROM TablesStopReason

  -----------------------------------------
 ------------------------------------------

 DELETE TablesModel WHERE intModelID=1;
 DELETE TablesManufacturer WHERE intManufacturerID=1
 DELETE TablesStopReason WHERE intStopReason=1