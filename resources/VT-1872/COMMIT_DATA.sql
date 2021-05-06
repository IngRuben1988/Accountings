USE test_vtclubdb_wp_201906111221
GO

INSERT INTO [vta].[tblsegment] ([segmentName] ,[SegmentShortName] ,[segmentDescription] ,[segmentActive]) VALUES  ('Agencia','Agencia','',1)
-- INSERT INTO [vta].[tblsegment] ([segmentName] ,[SegmentShortName] ,[segmentDescription] ,[segmentActive]) VALUES  ('PROVEEDORES','PROV','',1)
-- INSERT INTO [vta].[tblsegment] ([segmentName] ,[SegmentShortName] ,[segmentDescription] ,[segmentActive]) VALUES  ('CONSTRUCCION','CONST','',1)
-- INSERT INTO [vta].[tblsegment] ([segmentName] ,[SegmentShortName] ,[segmentDescription] ,[segmentActive]) VALUES  ('EXCELLENT','EX','',1)
-- INSERT INTO [vta].[tblsegment] ([segmentName] ,[SegmentShortName] ,[segmentDescription] ,[segmentActive]) VALUES  ('FUNDACIÓN','FUND','',1)
-- INSERT INTO [vta].[tblsegment] ([segmentName] ,[SegmentShortName] ,[segmentDescription] ,[segmentActive]) VALUES  ('WORLDPASS','WP','',1)
-- INSERT INTO [vta].[tblsegment] ([segmentName] ,[SegmentShortName] ,[segmentDescription] ,[segmentActive]) VALUES  ('VACATIONS TIMES','VT','',1)
-- INSERT INTO [vta].[tblsegment] ([segmentName] ,[SegmentShortName] ,[segmentDescription] ,[segmentActive]) VALUES  ('NEXT PROPERTY ADVISOR','NPA','',1)
-- INSERT INTO [vta].[tblsegment] ([segmentName] ,[SegmentShortName] ,[segmentDescription] ,[segmentActive]) VALUES  ('CORPORATIVO','CORP','',1)
-- INSERT INTO [vta].[tblsegment] ([segmentName] ,[SegmentShortName] ,[segmentDescription] ,[segmentActive]) VALUES  ('A&B','A&B','',1)
GO


INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('WORLDPASS Mexico','WPM','','',1,1,1)
INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('WORLDPASS Internacional','WPI','','',1,2,1)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('Las Escaleras by Inmense','HLE','','',1,2,1)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('Hacienda las Nubes','HLB','','',0,6,1)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('Xoxula by Inmense','HXO','','',1,8,1)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('Serenity Sailing Co','SSC','','',0,4,1)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('Barlovento 102','Barlovento 102','','',0,9,1)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('Eleven Palms by Inmense','HEP','','',1,13,1)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('RD68 by Inmense','RD68','','',1,12,1)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('Serenity Sailing Co','SSC-PTC','','',0,5,1)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('Grand Las Nubes by Inmense','HGB','','',1,7,1)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('Santosi by Inmense','HST','','',1,11,1)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('Sin compañia','','s','s',1,100,1)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('Corporativo','CORP','','',1,12,9)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('Corporativo WORLDSPASS','WP','','',1,13,6)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('ACF','AC','','',1,14,9)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('Servicios Hoteleros de Prestigio','SHP','','',1,15,1)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('Loreto','LORETO','','',1,16,1)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('VT8','VT8','','',1,19,1)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('Catamarán','CAT','','',1,17,1)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('Mala Vecindad','HMV','','',1,21,1)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('Myko','HMK','','',1,22,1)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('Pagadora CIE','PAG-CIE','','',1,23,2)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('Pagadora CCI','PAG-CCI','','',1,24,2)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('Pagadora ELVIS','PAG-ELVIS','','',1,25,2)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('PAGADORA SOLGLOSER','PAG-SOLGLOSER','','',1,26,2)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('ESPERANZA RINCON ZUNIGA ','E RINCON','','',1,27,2)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('RAUL PINA RAMIREZ','RA PINA','','',1,28,2)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('CELIA AYALA RUIZ','CE AYALA','','',1,29,2)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('JUAN GONZALES LOPEZ','J GONZ','','',1,30,2)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('MA DE LOS A MATEHUALA RODRIGUEZ','MA MATEHUALA','','',1,31,2)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('DISTRIBUIDORA VALLARTA ','DIST VALLARTA','','',1,32,2)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('ERIKA LOPEZ RENDON ','ERIKA LOPEZ','','',1,33,2)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('AGUA VICTORIA','AGUA VIC','','',1,34,2)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('FABRICA DE HIELO EL POLO SA DE CV ','EL POLO','','',1,35,2)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('GAS COM','GAS COM','','',1,36,2)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('SONIGAS (HLB)','SONIGAS HLB','','',1,37,2)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('GAS MENGUC SA DE CV','GAS MENGUC','','',1,38,2)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('GAS SOLIDO SA DE CV','GAS SOLIDO','','',1,39,2)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('GASOLINERA HUITEPEC','GAS HUITEPEC','','',1,40,2)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('GASSPEED','GASSPEED','','',1,41,2)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('SERVICIOS FAE','SERV FAE','','',1,42,2)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('MUSEO KAKAW','MUSEO KAKAW','','',1,43,2)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('turistica colonia','TUR COLONIA','','',1,44,2)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('TRANVIA ( CHIAPAS )','TRANVIA CHS','','',1,45,2)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('Dacia Villaseñor Robledo','DACIA VILLASEÑOR','','',1,46,2)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('La Embajada by Inmense','HEM','','',1,47,1)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('Casa Gutierrez','RCG','','',1,1,10)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('Restaurante 1 by HMV','R1-HMV','','',1,49,10)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('Axkan Riviera Maya','AX-RM','','',1,1,5)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('Axkan Playa del Carmen','AX-PDC','','',1,1,5)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('Axkan Chunhuhub','AX-CHUN','','',1,1,5)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('Axkan Ayotla','AX-AYO','','',1,1,5)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('Axkan Casa de la Cultura Texcoco','AX-CCTEX','','',1,1,5)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('Dunas Radio','DUN','','',1,1,5)
--INSERT INTO [vta].[tblcompanies] ([companyName] ,[companyShortName] ,[companyAddress] ,[companyTelephone] ,[companyActive] ,[companyOrder] ,[idSegment]) VALUES ('Restaurante 2 by HMV','R2-HMV','','',1,56,10)
GO


INSERT INTO [vta].[tblusercompanies] ([idCompany] ,[IdUser] ,[usercompanyUserLastChange] ,[usercompanyDateLastChange] ,[usercompanyActive]) VALUES (1,1,1,'2019-01-01 00:00:00',1)
INSERT INTO [vta].[tblusercompanies] ([idCompany] ,[IdUser] ,[usercompanyUserLastChange] ,[usercompanyDateLastChange] ,[usercompanyActive]) VALUES (2,1,1,'2019-01-01 00:00:00',1)
--INSERT INTO [vta].[tblusercompanies] ([idCompany] ,[IdUser] ,[usercompanyUserLastChange] ,[usercompanyDateLastChange] ,[usercompanyActive]) VALUES (3,1,1,'2019-01-01 00:00:00',1)
--INSERT INTO [vta].[tblusercompanies] ([idCompany] ,[IdUser] ,[usercompanyUserLastChange] ,[usercompanyDateLastChange] ,[usercompanyActive]) VALUES (51,1,1,'2019-01-01 00:00:00',1)
--INSERT INTO [vta].[tblusercompanies] ([idCompany] ,[IdUser] ,[usercompanyUserLastChange] ,[usercompanyDateLastChange] ,[usercompanyActive]) VALUES (52,1,1,'2019-01-01 00:00:00',1)
--INSERT INTO [vta].[tblusercompanies] ([idCompany] ,[IdUser] ,[usercompanyUserLastChange] ,[usercompanyDateLastChange] ,[usercompanyActive]) VALUES (48,1,1,'2019-01-01 00:00:00',1)
GO


INSERT INTO [vta].[tblcompaniescurrencies] ([idcompany] ,[idcurrency] ,[companiescurrenciesactive]) VALUES (1,3,1)
INSERT INTO [vta].[tblcompaniescurrencies] ([idcompany] ,[idcurrency] ,[companiescurrenciesactive]) VALUES (2,3,1)
INSERT INTO [vta].[tblcompaniescurrencies] ([idcompany] ,[idcurrency] ,[companiescurrenciesactive]) VALUES (2,4,1)
--INSERT INTO [vta].[tblcompaniescurrencies] ([idcompany] ,[idcurrency] ,[companiescurrenciesactive]) VALUES (51,3,1)
--INSERT INTO [vta].[tblcompaniescurrencies] ([idcompany] ,[idcurrency] ,[companiescurrenciesactive]) VALUES (52,3,1)
--INSERT INTO [vta].[tblcompaniescurrencies] ([idcompany] ,[idcurrency] ,[companiescurrenciesactive]) VALUES (48,3,1)
--INSERT INTO [vta].[tblcompaniescurrencies] ([idcompany] ,[idcurrency] ,[companiescurrenciesactive]) VALUES (1,4,1)
--INSERT INTO [vta].[tblcompaniescurrencies] ([idcompany] ,[idcurrency] ,[companiescurrenciesactive]) VALUES (3,4,1)
--INSERT INTO [vta].[tblcompaniescurrencies] ([idcompany] ,[idcurrency] ,[companiescurrenciesactive]) VALUES (48,4,1)
GO

-------------------------------------------------------------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------------------------------------------------

INSERT INTO [vta].[tblaccountsl1] ([accountl1name] ,[accountl1description] ,[accountl1active] ,[accountl1order]) VALUES ('Ingresos','Ingresos',1,1)
INSERT INTO [vta].[tblaccountsl1] ([accountl1name] ,[accountl1description] ,[accountl1active] ,[accountl1order]) VALUES ('Egresos','Egresos',1,2)
GO

INSERT INTO [vta].[tblaccountsl2] ([idaccountl1] ,[accountl2name] ,[accountl2description] ,[accountl2active] ,[accountl2order]) VALUES (1,'400','Ingresos',1,1)
GO

INSERT INTO [vta].[tblaccountsl3] ([idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (1,'401','Tipo de Pago',1,1)
INSERT INTO [vta].[tblaccountsl3] ([idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (1,'402','Reservaciones',1,2)

GO

INSERT INTO [vta].[tblaccountsl4] ([idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (1,'1','Ingresos en TC',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (1,'2','Ingresos en Efectivo',1,2)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (1,'3','Transferencias',1,3)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (1,'4','Paypal/medios electrónicos',1,4)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (2,'6','RCI,RTC',1,1)
GO

INSERT INTO [vta].[tblsegmentaccl4] ([idsegment] ,[idaccountl4] ,[segmentaccl4active]) VALUES (1,1,1)
INSERT INTO [vta].[tblsegmentaccl4] ([idsegment] ,[idaccountl4] ,[segmentaccl4active]) VALUES (1,2,1)
INSERT INTO [vta].[tblsegmentaccl4] ([idsegment] ,[idaccountl4] ,[segmentaccl4active]) VALUES (1,3,1)
INSERT INTO [vta].[tblsegmentaccl4] ([idsegment] ,[idaccountl4] ,[segmentaccl4active]) VALUES (1,4,1)
INSERT INTO [vta].[tblsegmentaccl4] ([idsegment] ,[idaccountl4] ,[segmentaccl4active]) VALUES (1,5,1)
GO

INSERT INTO [vta].[tblprofilesaccounts] ([profileaccountname] ,[profileaccountshortame] ,[profileaccountactive]) VALUES ('WORLDPASS Manager','WPM',1)
INSERT INTO [vta].[tblprofilesaccounts] ([profileaccountname] ,[profileaccountshortame] ,[profileaccountactive]) VALUES ('WORLDPASS Agent','WPA',1)
GO

INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (1,1,1,'2019-01-01 00:00:00.000')
GO

INSERT INTO [vta].[tblaccounttypereport] ([accounttypereportname] ,[accounttypereportdescription]) VALUES ('E-R','Estado de Resultados')
INSERT INTO [vta].[tblaccounttypereport] ([accounttypereportname] ,[accounttypereportdescription]) VALUES ('B-G','Balance General')
GO

INSERT INTO [vta].[tblaccountl4accounttype] ([idaccountl4] ,[idaccounttypereport]) VALUES (1,1)
INSERT INTO [vta].[tblaccountl4accounttype] ([idaccountl4] ,[idaccounttypereport]) VALUES (2,1)
INSERT INTO [vta].[tblaccountl4accounttype] ([idaccountl4] ,[idaccounttypereport]) VALUES (3,1)
INSERT INTO [vta].[tblaccountl4accounttype] ([idaccountl4] ,[idaccounttypereport]) VALUES (4,1)
INSERT INTO [vta].[tblaccountl4accounttype] ([idaccountl4] ,[idaccounttypereport]) VALUES (5,1)
GO


INSERT INTO [vta].[tbluseraccl4] ([iduser] ,[idaccountl4] ,[useraccl4active]) VALUES (1,5,1)
GO

INSERT INTO [vta].[tblcurrencyexchange] ([currencyexchangefrom] ,[currencyexchangeto] ,[currencyexchangerate] ,[currencyexchangedate]) VALUES ( 4,3,18,'2019-05-01 00:00:00')
GO

INSERT INTO [vta].[tblsourcedata] ([sourcedataname] ,[sourcedataactive]) VALUES ('Ingresos',1)
INSERT INTO [vta].[tblsourcedata] ([sourcedataname] ,[sourcedataactive]) VALUES ('Ingresos Movimientos',1)
INSERT INTO [vta].[tblsourcedata] ([sourcedataname] ,[sourcedataactive]) VALUES ('Facturas',1)
INSERT INTO [vta].[tblsourcedata] ([sourcedataname] ,[sourcedataactive]) VALUES ('Pagos',1)
INSERT INTO [vta].[tblsourcedata] ([sourcedataname] ,[sourcedataactive]) VALUES ('Fondos',1)
INSERT INTO [vta].[tblsourcedata] ([sourcedataname] ,[sourcedataactive]) VALUES ('Conciliaciones',1)
INSERT INTO [vta].[tblsourcedata] ([sourcedataname] ,[sourcedataactive]) VALUES ('Reservas',1)
GO

INSERT INTO [vta].[tblexpensereportsourcedata] ([idcompany] ,[idsourcedata] ,[expensereportsourcedataactive]) VALUES (2,1,1)
INSERT INTO [vta].[tblexpensereportsourcedata] ([idcompany] ,[idsourcedata] ,[expensereportsourcedataactive]) VALUES (2,7,1)
GO
  
  
INSERT INTO [vta].[tblinvoiceitemstatus] ([invoicestatusname], [invoicestatusshortname] ,[invoicestatusactive]) VALUES ('Sin Revisar','SR',1)
INSERT INTO [vta].[tblinvoiceitemstatus] ([invoicestatusname], [invoicestatusshortname] ,[invoicestatusactive]) VALUES ('Aprobado','A+',1)
INSERT INTO [vta].[tblinvoiceitemstatus] ([invoicestatusname], [invoicestatusshortname] ,[invoicestatusactive]) VALUES ('Rechazado','R-',1)
INSERT INTO [vta].[tblinvoiceitemstatus] ([invoicestatusname], [invoicestatusshortname] ,[invoicestatusactive]) VALUES ('Extemporaneo','EXT-',1)
GO

INSERT INTO [vta].[tblexpensereportsourcedatatypes] ([idexpensereportsourcedata] ,[idtype] ,[expensereportsourcedatatypesactive]) VALUES (2,2,1)
INSERT INTO [vta].[tblexpensereportsourcedatatypes] ([idexpensereportsourcedata] ,[idtype] ,[expensereportsourcedatatypesactive]) VALUES (2,3,1)
INSERT INTO [vta].[tblexpensereportsourcedatatypes] ([idexpensereportsourcedata] ,[idtype] ,[expensereportsourcedatatypesactive]) VALUES (2,4,1)
INSERT INTO [vta].[tblexpensereportsourcedatatypes] ([idexpensereportsourcedata] ,[idtype] ,[expensereportsourcedatatypesactive]) VALUES (2,11,1)
INSERT INTO [vta].[tblexpensereportsourcedatatypes] ([idexpensereportsourcedata] ,[idtype] ,[expensereportsourcedatatypesactive]) VALUES (2,12,1)
INSERT INTO [vta].[tblexpensereportsourcedatatypes] ([idexpensereportsourcedata] ,[idtype] ,[expensereportsourcedatatypesactive]) VALUES (2,13,1)
INSERT INTO [vta].[tblexpensereportsourcedatatypes] ([idexpensereportsourcedata] ,[idtype] ,[expensereportsourcedatatypesactive]) VALUES (2,16,1)
INSERT INTO [vta].[tblexpensereportsourcedatatypes] ([idexpensereportsourcedata] ,[idtype] ,[expensereportsourcedatatypesactive]) VALUES (2,23,1)
INSERT INTO [vta].[tblexpensereportsourcedatatypes] ([idexpensereportsourcedata] ,[idtype] ,[expensereportsourcedatatypesactive]) VALUES (2,25,1)
GO

INSERT INTO [vta].[tblaccl4paymenmethods] ([idpaymentmethod] ,[idaccountl4]) VALUES (2,1)
INSERT INTO [vta].[tblaccl4paymenmethods] ([idpaymentmethod] ,[idaccountl4]) VALUES (3,1)
INSERT INTO [vta].[tblaccl4paymenmethods] ([idpaymentmethod] ,[idaccountl4]) VALUES (4,1)
INSERT INTO [vta].[tblaccl4paymenmethods] ([idpaymentmethod] ,[idaccountl4]) VALUES (11,3)
INSERT INTO [vta].[tblaccl4paymenmethods] ([idpaymentmethod] ,[idaccountl4]) VALUES (12,2)
INSERT INTO [vta].[tblaccl4paymenmethods] ([idpaymentmethod] ,[idaccountl4]) VALUES (25,4)
INSERT INTO [vta].[tblaccl4paymenmethods] ([idpaymentmethod] ,[idaccountl4]) VALUES (13,5)
INSERT INTO [vta].[tblaccl4paymenmethods] ([idpaymentmethod] ,[idaccountl4]) VALUES (16,5)
INSERT INTO [vta].[tblaccl4paymenmethods] ([idpaymentmethod] ,[idaccountl4]) VALUES (23,5)
GO

