go
/*
COMMIT DE VTACCOUNT PARA VTClub
*/



 /****** Object:  Table [vta].[tblinvoice]    Script Date: 16/05/2019 09:43:13 a. m. ******/
 IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[vta].[FK_tblinvoice_tblusers]') AND parent_object_id = OBJECT_ID(N'[vta].[tblinvoice]') )
ALTER TABLE [vta].[tblinvoice]  DROP CONSTRAINT [FK_tblinvoice_tblusers] 
GO
 IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[vta].[FK_tblinvoice_tblusers2]') AND parent_object_id = OBJECT_ID(N'[vta].[tblinvoice]') )
ALTER TABLE [vta].[tblinvoice]  DROP CONSTRAINT [FK_tblinvoice_tblusers2]
GO
 IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[vta].[FK_VTA_tblinvoice_tblccurrency_1]') AND parent_object_id = OBJECT_ID(N'[vta].[tblinvoice]') )
ALTER TABLE [vta].[tblinvoice]  DROP CONSTRAINT [FK_VTA_tblinvoice_tblccurrency_1] 
GO
 IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[vta].[FK_VTA_tblinvoice_tblcompanies_1]') AND parent_object_id = OBJECT_ID(N'[vta].[tblinvoice]') )
ALTER TABLE [vta].[tblinvoice]  DROP CONSTRAINT [FK_VTA_tblinvoice_tblcompanies_1] 
GO
 IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[vta].[FK_tblinvoiceComments_tblinvoice]') AND parent_object_id = OBJECT_ID(N'[vta].[tblinvoicecomments]') )
ALTER TABLE [vta].[tblinvoicecomments]  DROP CONSTRAINT  [FK_tblinvoiceComments_tblinvoice]
GO
 IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[vta].[FK_VTA_tblinvoiceattach_tblinvoice_1]') AND parent_object_id = OBJECT_ID(N'[vta].[tblinvoiceattach]') )
ALTER TABLE [vta].[tblinvoiceattach]  DROP CONSTRAINT  [FK_VTA_tblinvoiceattach_tblinvoice_1]
GO
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[vta].[FK_VTA_tblpayment_tblinvoice_1]') AND parent_object_id = OBJECT_ID(N'[vta].[tblpayment]') )
ALTER TABLE [vta].[tblpayment] DROP CONSTRAINT [FK_VTA_tblpayment_tblinvoice_1]
GO
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[vta].[FK_VTA_tblinvoiceditem_tblinvoice_1]') AND parent_object_id = OBJECT_ID(N'[vta].[tblinvoiceditem]') )
ALTER TABLE [vta].[tblinvoiceditem] DROP CONSTRAINT [FK_VTA_tblinvoiceditem_tblinvoice_1]
GO
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[vta].[FK_tblincome_tblccurrency_1]') AND parent_object_id = OBJECT_ID(N'[vta].[tblincome]') )
ALTER TABLE [vta].[tblincome] DROP CONSTRAINT [FK_tblincome_tblccurrency_1]
GO
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[vta].[FK_tblinvoiceitem_tblsupplier_1]') AND parent_object_id = OBJECT_ID(N'[vta].[tblinvoiceditem]') )
ALTER TABLE [vta].[tblinvoiceditem] DROP CONSTRAINT [FK_tblinvoiceitem_tblsupplier_1]
GO
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[vta].[fk_tblcompaniessupplier_tblsupplier_1]') AND parent_object_id = OBJECT_ID(N'[vta].[tblcompaniesuppliers]') )
ALTER TABLE [vta].[tblcompaniesuppliers] DROP CONSTRAINT [fk_tblcompaniessupplier_tblsupplier_1]
GO
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[vta].[FK_tblinvoiceditem_tblbugettype]') AND parent_object_id = OBJECT_ID(N'[vta].[tblinvoiceditem]') )
ALTER TABLE [vta].[tblinvoiceditem] DROP CONSTRAINT [FK_tblinvoiceditem_tblbugettype]
GO
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[vta].[FK_VTA_tblinvoiceditem_tblaccountsl4_1]') AND parent_object_id = OBJECT_ID(N'[vta].[tblinvoiceditem]') )
ALTER TABLE [vta].[tblinvoiceditem] DROP CONSTRAINT [FK_VTA_tblinvoiceditem_tblaccountsl4_1]
GO
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[vta].[FK_VTA_tblinvoiceditem_tblinvoiceitemstatus_1]') AND parent_object_id = OBJECT_ID(N'[vta].[tblinvoiceditem]') )
ALTER TABLE [vta].[tblinvoiceditem] DROP CONSTRAINT [FK_VTA_tblinvoiceditem_tblinvoiceitemstatus_1]
GO
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[vta].[FK_VTA_tblinvoiceditem_tblusers_1]') AND parent_object_id = OBJECT_ID(N'[vta].[tblinvoiceditem]') )
ALTER TABLE [vta].[tblinvoiceditem] DROP CONSTRAINT [FK_VTA_tblinvoiceditem_tblusers_1]
GO




GO
/*****INSERCION DE DATOS A LAS TABLAS EXISTENTES *****/
--INSERT INTO [vta].[tblsegment] ([segmentname] ,[segmentshortname] ,[segmentdescription] ,[segmentactive]) VALUES  ('Agencia','Agencia','',1)
--GO

INSERT INTO [vta].[tblusercompanies] ([idcompany] ,[iduser] ,[usercompanyuserlastchange] ,[usercompanydatelastchange] ,[usercompanyactive]) VALUES (1,1,1,'2019-01-01 00:00:00',1)
INSERT INTO [vta].[tblusercompanies] ([idcompany] ,[iduser] ,[usercompanyuserlastchange] ,[usercompanydatelastchange] ,[usercompanyactive]) VALUES (2,1,1,'2019-01-01 00:00:00',1)
GO

INSERT INTO [vta].[tblcompaniescurrencies] ([idcompany] ,[idcurrency] ,[companiescurrenciesactive]) VALUES (1,3,1)
INSERT INTO [vta].[tblcompaniescurrencies] ([idcompany] ,[idcurrency] ,[companiescurrenciesactive]) VALUES (2,3,1)
INSERT INTO [vta].[tblcompaniescurrencies] ([idcompany] ,[idcurrency] ,[companiescurrenciesactive]) VALUES (2,4,1)
GO

-------------------------------------------------------------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------------------------------------------------
SET IDENTITY_INSERT [vta].[tblaccountsl1] ON
GO
INSERT INTO [vta].[tblaccountsl1] ([idaccountl1] ,[accountl1name] ,[accountl1description] ,[accountl1active] ,[accountl1order]) VALUES (1,'Ingresos','Ingresos',1,1)
GO
INSERT INTO [vta].[tblaccountsl1] ([idaccountl1] ,[accountl1name] ,[accountl1description] ,[accountl1active] ,[accountl1order]) VALUES (2,'Egresos','Egresos',1,2)
GO
SET IDENTITY_INSERT [vta].[tblaccountsl1] OFF
GO
SET IDENTITY_INSERT [vta].[tblaccountsl2] ON
GO
INSERT INTO [vta].[tblaccountsl2] ([idaccountl2] ,[idaccountl1] ,[accountl2name] ,[accountl2description] ,[accountl2active] ,[accountl2order]) VALUES (1,1,'100','Ingresos',1,1)
GO
INSERT INTO [vta].[tblaccountsl2] ([idaccountl2] ,[idaccountl1] ,[accountl2name] ,[accountl2description] ,[accountl2active] ,[accountl2order]) VALUES (5,1,'200','Ventas de Hospedaje',1,2)
GO
INSERT INTO [vta].[tblaccountsl2] ([idaccountl2] ,[idaccountl1] ,[accountl2name] ,[accountl2description] ,[accountl2active] ,[accountl2order]) VALUES (6,1,'300','Vuelos',1,3)
GO
INSERT INTO [vta].[tblaccountsl2] ([idaccountl2] ,[idaccountl1] ,[accountl2name] ,[accountl2description] ,[accountl2active] ,[accountl2order]) VALUES (7,1,'400','Vuelos con Descuento',1,4)
GO
INSERT INTO [vta].[tblaccountsl2] ([idaccountl2] ,[idaccountl1] ,[accountl2name] ,[accountl2description] ,[accountl2active] ,[accountl2order]) VALUES (8,1,'500','Intercambios',1,5)
GO
INSERT INTO [vta].[tblaccountsl2] ([idaccountl2] ,[idaccountl1] ,[accountl2name] ,[accountl2description] ,[accountl2active] ,[accountl2order]) VALUES (9,1,'600','Ventas Renovaciones',1,6)
GO
INSERT INTO [vta].[tblaccountsl2] ([idaccountl2] ,[idaccountl1] ,[accountl2name] ,[accountl2description] ,[accountl2active] ,[accountl2order]) VALUES (10,1,'700','Otros Ingresos',1,7)
GO
INSERT INTO [vta].[tblaccountsl2] ([idaccountl2] ,[idaccountl1] ,[accountl2name] ,[accountl2description] ,[accountl2active] ,[accountl2order]) VALUES (11,1,'800','Ventas Insurance',1,8)
GO
INSERT INTO [vta].[tblaccountsl2] ([idaccountl2] ,[idaccountl1] ,[accountl2name] ,[accountl2description] ,[accountl2active] ,[accountl2order]) VALUES (12,1,'900','Ventas de Membresias Desarrollo',1,9)
GO
INSERT INTO [vta].[tblaccountsl2] ([idaccountl2] ,[idaccountl1] ,[accountl2name] ,[accountl2description] ,[accountl2active] ,[accountl2order]) VALUES (13,1,'1000','Ventas Desarrollo Certificados',1,10)
GO

INSERT INTO [vta].[tblaccountsl2] ([idaccountl2] ,[idaccountl1] ,[accountl2name] ,[accountl2description] ,[accountl2active] ,[accountl2order]) VALUES (2,2,'1500','Costos por Operación',1,5)
GO
INSERT INTO [vta].[tblaccountsl2] ([idaccountl2] ,[idaccountl1] ,[accountl2name] ,[accountl2description] ,[accountl2active] ,[accountl2order]) VALUES (3,2,'1600','Gastos de Ventas',1,6)
GO
INSERT INTO [vta].[tblaccountsl2] ([idaccountl2] ,[idaccountl1] ,[accountl2name] ,[accountl2description] ,[accountl2active] ,[accountl2order]) VALUES (4,2,'1100','Gastos de Personal',1,1)
GO
INSERT INTO [vta].[tblaccountsl2] ([idaccountl2] ,[idaccountl1] ,[accountl2name] ,[accountl2description] ,[accountl2active] ,[accountl2order]) VALUES (14,2,'1200','Gastos de Servicio',1,2)
GO
INSERT INTO [vta].[tblaccountsl2] ([idaccountl2] ,[idaccountl1] ,[accountl2name] ,[accountl2description] ,[accountl2active] ,[accountl2order]) VALUES (15,2,'1300','Materiales y Suministros Operaciones',1,3)
GO
INSERT INTO [vta].[tblaccountsl2] ([idaccountl2] ,[idaccountl1] ,[accountl2name] ,[accountl2description] ,[accountl2active] ,[accountl2order]) VALUES (16,2,'1400','Alimentos y Utensilios',1,4)
GO
INSERT INTO [vta].[tblaccountsl2] ([idaccountl2] ,[idaccountl1] ,[accountl2name] ,[accountl2description] ,[accountl2active] ,[accountl2order]) VALUES (17,2,'1700','Gastos Generales',1,7)
GO
INSERT INTO [vta].[tblaccountsl2] ([idaccountl2] ,[idaccountl1] ,[accountl2name] ,[accountl2description] ,[accountl2active] ,[accountl2order]) VALUES (18,2,'1800','Otros Gastos',1,8)
GO

SET IDENTITY_INSERT [vta].[tblaccountsl2] OFF
GO
SET IDENTITY_INSERT [vta].[tblaccountsl3] ON
GO
INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (1,1,'101','Tipo de Pago',1,1)

INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (2,5,'201','Hoteles Socios',1,1)
INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (3,5,'202','Hoteles Directivos',1,2)
INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (4,5,'203','Hoteles Uso Empresa',1,3)
INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (5,5,'204','Hoteles Web',1,4)

INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (6,6,'301','Vuelos Socios',1,1)
INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (7,6,'302','Vuelos Directivos',1,2)
INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (8,6,'303','Vuelos Uso Empresa',1,3)
INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (9,6,'303','Vuelos Web',1,4)

INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (10,7,'401','15%',1,1)
INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (11,7,'402','25%',1,2)
INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (12,7,'403','2x1',1,3)

INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (13,8,'501','Intercambios',1,1)
INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (14,9,'601','Ventas Renovaciones',1,1)
INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (15,10,'701','Otros Ingresos',1,1)
INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (16,11,'801','Ventas Insurance',1,1)

INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (17,12,'901','Palladium',1,1)
INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (18,12,'902','H10',1,2)
INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (19,12,'903','Fiesta CV / Costa Rica',1,3)
INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (20,12,'904','Certificados',1,4)
INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (21,12,'904','Regulares',1,5)
INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (22,12,'905','HD Honduras',1,6)
INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (23,12,'906','Premier',1,7)
INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (24,12,'907','Unique by WORLDPASS',1,8)
INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (25,12,'908','Circle One',1,9)
INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (26,12,'909','Inmense',1,10)
INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (27,12,'910','CMD',1,11)
INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (28,12,'911','ORBIS',1,12)
INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (29,12,'912','Punta Pacifica',1,13)

INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (30,13,'1001','Ventas Desarrollo Certificados',1,1)

INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (31,4,'1101','Nómina Personal',1,1)
INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (32,4,'1102','Compensaciones Personal',1,2)
INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (33,4,'1103','Estímulos, Premios u Otros Personal',1,3)
INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (34,4,'1104','Otros',1,4)

INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (35,14,'1201','Gastos de Servicio',1,1)

INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (36,15,'1301','Materiales y Suministros Operaciones',1,1)

INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (37,16,'1401','Alimentos y Utensilios',1,1)

INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (38,2,'1501','Costo por Hospedaje',1,1)
INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (39,2,'1502','Costo por Vuelos',1,2)
INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (40,2,'1503','Costo por Vuelos Air',1,3)
INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (41,2,'1504','Costo por Exchange',1,4)
INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (42,2,'1505','Costo Otros',1,5)
INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (43,2,'1506','Costo Insurance',1,6)

INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (44,3,'1601','Gastos de Venta',1,1)

INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (45,17,'1701','Marketing',1,1)
INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (46,17,'1702','Folleteria e Impresiones',1,2)
INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (47,17,'1703','Publicidad',1,3)
INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (48,17,'1704','Materiales Promocionales',1,4)
INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (49,17,'1705','Eventos',1,5)

INSERT INTO [vta].[tblaccountsl3] ([idaccountl3] ,[idaccountl2] ,[accountl3name] ,[accountl3description] ,[accountl3active] ,[accountl3order]) VALUES (50,18,'1801','Servicios Financieros, Bancarios y Comerciales',1,1)


SET IDENTITY_INSERT [vta].[tblaccountsl3] OFF
GO
SET IDENTITY_INSERT [vta].[tblaccountsl4] ON
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (1,1,'1','Ingresos en TC',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (2,1,'2','Ingresos en Efectivo',1,2)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (3,1,'3','Transferencias',1,3)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (4,1,'4','Paypal/medios Electrónicos',1,4)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (5,1,'5','Filiales',1,5)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (6,1,'6','Cortesias',1,6)

INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (7,2,'7','Ingresos en TC',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (8,2,'8','Ingresos en Efectivo',1,2)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (9,2,'9','Transferencias',1,3)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (10,2,'10','Paypal/medios Electrónicos',1,4)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (11,2,'11','Filiales',1,5)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (12,2,'12','Cortesias',1,6)

INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (13,3,'13','Ingresos en TC',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (14,3,'14','Ingresos en Efectivo',1,2)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (15,3,'15','Transferencias',1,3)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (16,3,'16','Paypal/medios Electrónicos',1,4)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (17,3,'17','Filiales',1,5)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (18,3,'18','Cortesias',1,6)

INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (19,4,'19','Ingresos en TC',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (20,4,'20','Ingresos en Efectivo',1,2)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (21,4,'21','Transferencias',1,3)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (22,4,'22','Paypal/medios Electrónicos',1,4)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (23,4,'23','Filiales',1,5)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (24,4,'24','Cortesias',1,6)

INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (25,5,'25','Ingresos en TC',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (26,5,'26','Ingresos en Efectivo',1,2)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (27,5,'27','Transferencias',1,3)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (28,5,'28','Paypal/medios Electrónicos',1,4)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (29,5,'29','Filiales',1,5)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (30,5,'30','Cortesias',1,6)

INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (31,6,'31','Ingresos en TC',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (32,6,'32','Ingresos en Efectivo',1,2)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (33,6,'33','Transferencias',1,3)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (34,6,'34','Paypal/medios Electrónicos',1,4)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (35,6,'35','Filiales',1,5)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (36,6,'36','Cortesias',1,6)

INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (37,7,'37','Ingresos en TC',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (38,7,'38','Ingresos en Efectivo',1,2)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (39,7,'39','Transferencias',1,3)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (40,7,'40','Paypal/medios Electrónicos',1,4)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (41,7,'41','Filiales',1,5)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (42,7,'42','Cortesias',1,6)

INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (43,8,'43','Ingresos en TC',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (44,8,'44','Ingresos en Efectivo',1,2)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (45,8,'45','Transferencias',1,3)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (46,8,'46','Paypal/medios Electrónicos',1,4)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (47,8,'47','Filiales',1,5)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (48,8,'48','Cortesias',1,6)

INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (49,9,'49','Ingresos en TC',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (50,9,'50','Ingresos en Efectivo',1,2)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (51,9,'51','Transferencias',1,3)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (52,9,'52','Paypal/medios Electrónicos',1,4)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (53,9,'53','Filiales',1,5)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (54,9,'54','Cortesias',1,6)

INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (55,10,'55','Ingresos en TC',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (56,10,'56','Ingresos en Efectivo',1,2)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (57,10,'57','Transferencias',1,3)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (58,10,'58','Paypal/medios Electrónicos',1,4)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (59,10,'59','Filiales',1,5)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (60,10,'60','Cortesias',1,6)

INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (61,11,'55','Ingresos en TC',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (62,11,'56','Ingresos en Efectivo',1,2)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (63,11,'57','Transferencias',1,3)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (64,11,'58','Paypal/medios Electrónicos',1,4)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (65,11,'59','Filiales',1,5)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (66,11,'60','Cortesias',1,6)

INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (67,12,'67','Ingresos en TC',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (68,12,'68','Ingresos en Efectivo',1,2)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (69,12,'69','Transferencias',1,3)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (70,12,'70','Paypal/medios Electrónicos',1,4)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (71,12,'71','Filiales',1,5)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (72,12,'72','Cortesias',1,6)

INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (73,13,'73','Exchange Socios',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (74,13,'74','Ingresos en TC',1,2)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (75,13,'75','Ingresos en Efectivo',1,3)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (76,13,'76','Transferencias',1,4)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (77,13,'77','Paypal/medios Electrónicos',1,5)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (78,13,'78','Filiales',1,6)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (79,13,'79','Cortesias',1,7)

INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (80,14,'80','Ingresos en TC',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (81,14,'81','Ingresos en Efectivo',1,2)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (82,14,'82','Transferencias',1,3)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (83,14,'83','Paypal/medios Electrónicos',1,4)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (84,14,'84','Filiales',1,5)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (85,14,'85','Cortesias',1,6)

INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (86,15,'86','Ingresos en TC',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (87,15,'87','Ingresos en Efectivo',1,2)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (88,15,'88','Transferencias',1,3)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (89,15,'89','Paypal/medios Electrónicos',1,4)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (90,15,'90','Filiales',1,5)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (91,15,'91','Cortesias',1,6)

INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (92,16,'92','Ingresos en TC',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (93,16,'93','Ingresos en Efectivo',1,2)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (94,16,'94','Transferencias',1,3)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (95,16,'95','Paypal/medios Electrónicos',1,4)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (96,16,'96','Filiales',1,5)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (97,16,'97','Cortesias',1,6)

INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (98,17,'98','Palladium RM',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (99,17,'99','Palladium JAM',1,2)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (100,17,'100','Palladium DOM',1,3)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (101,17,'101','Palladium VAL',1,4)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (102,17,'102','Palladium CMU',1,5)

INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (103,18,'103','OBS',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (104,18,'104','ORP',1,2)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (105,18,'105','OCT',1,3)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (106,18,'106','OMR',1,4)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (107,18,'107','OEF',1,5)

INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (108,19,'108','Fiesta CV / Costa Rica',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (109,20,'109','Certificados',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (110,21,'110','Regulares',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (111,22,'111','HD Honduras',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (112,23,'112','Premier',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (113,24,'113','Unique by WORLDPASS',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (114,25,'114','Circle One',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (115,26,'115','Inmense',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (116,27,'116','CMD',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (117,28,'117','ORBIS',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (118,29,'118','Punta Pacifica',1,1)

INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (119,30,'119','Ventas Desarrollo Certificados',1,5)

INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (120,31,'120','Reservaciones',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (121,31,'121','Renovaciones',1,2)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (122,31,'122','Customer Service',1,3)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (123,31,'123','Dirección',1,4)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (124,31,'124','Administración y Finanzas',1,5)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (125,31,'125','We Care',1,6)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (126,31,'126','Nuevos Productos',1,7)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (127,31,'127','WORLDPASS',1,8)

INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (128,32,'128','Reservaciones',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (129,32,'129','Renovaciones',1,2)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (130,32,'130','Customer Service',1,3)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (131,32,'131','Dirección',1,4)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (132,32,'132','Administración y Finanzas',1,5)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (133,32,'133','We Care',1,6)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (134,32,'134','Nuevos Productos',1,7)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (135,32,'135','WORLDPASS',1,8)

INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (136,33,'136','Reservaciones',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (137,33,'137','Renovaciones',1,2)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (138,33,'138','Customer Service',1,3)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (139,33,'139','Dirección',1,4)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (140,33,'140','Administración y Finanzas',1,5)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (141,33,'141','We Care',1,6)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (142,33,'142','Nuevos Productos',1,7)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (143,33,'143','WORLDPASS',1,8)

INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (144,34,'144','Otros Bonos y Premios',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (145,34,'145','Aguinaldos',1,2)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (146,34,'146','Costo Social: IMSS + INFONAVIT',1,3)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (147,34,'147','Indemnizaciones y/o Finiquitos y/o Prestamos',1,4)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (148,34,'148','Apoyo para Transportación',1,5)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (149,34,'149','Uniformes',1,6)

INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (150,35,'150','Servicio de Energía Eléctrica',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (151,35,'151','Servicio de Gas Estacionario',1,2)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (152,35,'152','Servicio de Agua',1,3)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (153,35,'153','Servicio de Telefonía',1,4)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (154,35,'154','Servicios de Acceso a Internet',1,5)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (155,35,'155','Servicio de Paqueteria',1,6)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (156,35,'156','Servicios de Arrendamiento Oficinas / Bodegas',1,7)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (157,35,'157','Servicios de Protección (pólizas de seguro)',1,8)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (158,35,'158','Servicios de Mantenimiento',1,9)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (159,35,'159','Servicios de Fumigación',1,10)

INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (160,36,'160','Arículos de limpieza',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (161,36,'161','Gasolina',1,2)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (162,36,'162','Papeleria y artículos menores de oficina',1,3)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (163,36,'163','Renta de Copiadora',1,4)

INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (164,37,'164','Insumos para Oficina',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (165,37,'165','Insumos alimenticios para staff',1,2)

INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (166,38,'166','Hoteles Socios',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (167,38,'167','Hoteles Directivos',1,2)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (168,38,'168','Hoteles Premios y Cortesias',1,3)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (169,38,'169','Hoteles Web',1,4)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (170,38,'170','Hoteles Uso Empresa',1,5)

INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (171,39,'171','Vuelos Socios',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (172,39,'172','Vuelos Directivos',1,2)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (173,39,'173','Vuelos Premios y Cortesias',1,3)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (174,39,'174','Vuelos Uso Casa',1,4)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (175,39,'175','Vuelos Web',1,5)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (176,39,'176','2x1',1,6)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (177,39,'177','15%',1,7)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (178,39,'178','25%',1,8)

INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (179,40,'179','Pasaporte',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (180,40,'180','Certificado',1,2)

INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (181,41,'181','Exchange Socios',1,1)

INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (182,42,'182','Otros Costos',1,1)

INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (183,43,'183','Costos insurance',1,1)

INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (184,44,'184','Viaticos',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (185,44,'185','Tarjetas',1,2)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (186,44,'186','Folletos',1,3)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (187,44,'187','Pasaportes',1,4)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (188,44,'188','WORLDPASS EXIT',1,5)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (189,44,'189','Promocionales',1,6)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (190,44,'190','Gasolina',1,7)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (191,44,'191','Viaticos',1,8)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (192,44,'192','Cortesias',1,9)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (193,44,'193','Certificados',1,10)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (194,44,'194','Impresión de Certificados',1,11)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (195,44,'195','Certificados MKT',1,12)

INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (196,45,'196','Pagos a Asociaciones, Empresas y Expos',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (197,45,'197','Campañas FB',1,2)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (198,45,'198','Otros Medios',1,3)

INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (199,46,'199','Tarjetas para Socios',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (200,46,'200','Folletos Negros',1,2)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (201,46,'201','Certificados Air-Rebate',1,3)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (202,46,'202','Pasaportes',1,4)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (203,46,'203','Otros Certificados',1,5)

INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (204,47,'204','Espectaculares',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (205,47,'205','Promociones y Anuncios en Revistas y Medios',1,2)

INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (206,48,'206','Tazas',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (207,48,'207','Ositos',1,2)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (208,48,'208','Bolsas Grandes',1,3)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (209,48,'209','Bolsas Pequeñas',1,4)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (210,48,'210','Globos',1,5)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (211,48,'211','Rotulación de Autos',1,6)

INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (212,49,'212','Ambientación, Iluminación y Otros Servicios ',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (213,49,'213','Bebidas',1,2)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (214,49,'214','Insumos para Alimentación',1,3)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (215,49,'215','VT',1,4)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (216,49,'216','Palladium RM',1,5)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (217,49,'217','Inmense Hotels',1,6)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (218,49,'218','Inmense Club',1,7)

INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (219,50,'219','Comisiones Bancarias',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (220,50,'220','Comisiones de Pagadora',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (221,50,'221','Licencia de Funcionamiento',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (222,50,'222','Cuotas de Inscripción',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (223,50,'223','Impuestos',1,1)
INSERT INTO [vta].[tblaccountsl4] ([idAccountl4] ,[idAccountl3] ,[accountl4name] ,[accountl4description] ,[accountl4active] ,[accountl4order]) VALUES (224,50,'224','Servicios de Administración',1,1)


SET IDENTITY_INSERT [vta].[tblaccountsl4] OFF
GO


INSERT INTO [vta].[tblsegmentaccl4] ([idsegment] ,[idaccountl4] ,[segmentaccl4active]) VALUES (1,1,1)
INSERT INTO [vta].[tblsegmentaccl4] ([idsegment] ,[idaccountl4] ,[segmentaccl4active]) VALUES (1,2,1)
INSERT INTO [vta].[tblsegmentaccl4] ([idsegment] ,[idaccountl4] ,[segmentaccl4active]) VALUES (1,3,1)
INSERT INTO [vta].[tblsegmentaccl4] ([idsegment] ,[idaccountl4] ,[segmentaccl4active]) VALUES (1,4,1)
INSERT INTO [vta].[tblsegmentaccl4] ([idsegment] ,[idaccountl4] ,[segmentaccl4active]) VALUES (1,5,1)
INSERT INTO [vta].[tblsegmentaccl4] ([idsegment] ,[idaccountl4] ,[segmentaccl4active]) VALUES (1,6,1)
INSERT INTO [vta].[tblsegmentaccl4] ([idsegment] ,[idaccountl4] ,[segmentaccl4active]) VALUES (1,7,1)
INSERT INTO [vta].[tblsegmentaccl4] ([idsegment] ,[idaccountl4] ,[segmentaccl4active]) VALUES (1,8,1)
INSERT INTO [vta].[tblsegmentaccl4] ([idsegment] ,[idaccountl4] ,[segmentaccl4active]) VALUES (1,9,1)
INSERT INTO [vta].[tblsegmentaccl4] ([idsegment] ,[idaccountl4] ,[segmentaccl4active]) VALUES (1,10,1)
INSERT INTO [vta].[tblsegmentaccl4] ([idsegment] ,[idaccountl4] ,[segmentaccl4active]) VALUES (1,11,1)
INSERT INTO [vta].[tblsegmentaccl4] ([idsegment] ,[idaccountl4] ,[segmentaccl4active]) VALUES (1,12,1)
INSERT INTO [vta].[tblsegmentaccl4] ([idsegment] ,[idaccountl4] ,[segmentaccl4active]) VALUES (1,13,1)
INSERT INTO [vta].[tblsegmentaccl4] ([idsegment] ,[idaccountl4] ,[segmentaccl4active]) VALUES (1,14,1)
INSERT INTO [vta].[tblsegmentaccl4] ([idsegment] ,[idaccountl4] ,[segmentaccl4active]) VALUES (1,15,1)
INSERT INTO [vta].[tblsegmentaccl4] ([idsegment] ,[idaccountl4] ,[segmentaccl4active]) VALUES (1,16,1)
INSERT INTO [vta].[tblsegmentaccl4] ([idsegment] ,[idaccountl4] ,[segmentaccl4active]) VALUES (1,17,1)
INSERT INTO [vta].[tblsegmentaccl4] ([idsegment] ,[idaccountl4] ,[segmentaccl4active]) VALUES (1,18,1)
INSERT INTO [vta].[tblsegmentaccl4] ([idsegment] ,[idaccountl4] ,[segmentaccl4active]) VALUES (1,19,1)
INSERT INTO [vta].[tblsegmentaccl4] ([idsegment] ,[idaccountl4] ,[segmentaccl4active]) VALUES (1,20,1)
INSERT INTO [vta].[tblsegmentaccl4] ([idsegment] ,[idaccountl4] ,[segmentaccl4active]) VALUES (1,21,1)
INSERT INTO [vta].[tblsegmentaccl4] ([idsegment] ,[idaccountl4] ,[segmentaccl4active]) VALUES (1,22,1)
INSERT INTO [vta].[tblsegmentaccl4] ([idsegment] ,[idaccountl4] ,[segmentaccl4active]) VALUES (1,23,1)
INSERT INTO [vta].[tblsegmentaccl4] ([idsegment] ,[idaccountl4] ,[segmentaccl4active]) VALUES (1,24,1)
INSERT INTO [vta].[tblsegmentaccl4] ([idsegment] ,[idaccountl4] ,[segmentaccl4active]) VALUES (1,25,1)
INSERT INTO [vta].[tblsegmentaccl4] ([idsegment] ,[idaccountl4] ,[segmentaccl4active]) VALUES (1,26,1)
INSERT INTO [vta].[tblsegmentaccl4] ([idsegment] ,[idaccountl4] ,[segmentaccl4active]) VALUES (1,27,1)
INSERT INTO [vta].[tblsegmentaccl4] ([idsegment] ,[idaccountl4] ,[segmentaccl4active]) VALUES (1,28,1)
INSERT INTO [vta].[tblsegmentaccl4] ([idsegment] ,[idaccountl4] ,[segmentaccl4active]) VALUES (1,29,1)
INSERT INTO [vta].[tblsegmentaccl4] ([idsegment] ,[idaccountl4] ,[segmentaccl4active]) VALUES (1,30,1)


GO
SET IDENTITY_INSERT [vta].[tblprofilesaccounts]  ON
GO
INSERT INTO [vta].[tblprofilesaccounts] ([idprofileaccount],[profileaccountname] ,[profileaccountshortame] ,[profileaccountactive]) VALUES (1,'WORLDPASS Manager','WPM',1)
INSERT INTO [vta].[tblprofilesaccounts] ([idprofileaccount],[profileaccountname] ,[profileaccountshortame] ,[profileaccountactive]) VALUES (2,'WORLDPASS Agent','WPA',1)
GO
SET IDENTITY_INSERT [vta].[tblprofilesaccounts]  OFF
GO

-- Perfil WORLDPASS Manager
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (1,1,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (2,1,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (3,1,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (4,1,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (5,1,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (6,1,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (7,1,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (8,1,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (9,1,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (10,1,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (11,1,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (12,1,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (13,1,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (14,1,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (15,1,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (16,1,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (17,1,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (18,1,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (19,1,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (20,1,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (21,1,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (22,1,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (23,1,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (24,1,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (25,1,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (26,1,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (27,1,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (28,1,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (29,1,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (40,1,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (41,1,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (42,1,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (43,1,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (44,1,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (45,1,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (46,1,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (47,1,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (48,1,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (49,1,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (50,1,1,'2019-01-01 00:00:00.000')
-- Perfil WORLDPASS Agent
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (1,2,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (2,2,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (3,2,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (4,2,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (5,2,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (6,2,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (7,2,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (8,2,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (9,2,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (10,2,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (11,2,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (12,2,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (13,2,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (14,2,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (15,2,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (16,2,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (17,2,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (18,2,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (19,2,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (20,2,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (21,2,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (22,2,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (23,2,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (24,2,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (25,2,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (26,2,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (27,2,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (28,2,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (29,2,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (40,2,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (41,2,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (42,2,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (43,2,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (44,2,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (45,2,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (46,2,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (47,2,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (48,2,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (49,2,1,'2019-01-01 00:00:00.000')
INSERT INTO [vta].[tblprofaccclass3] ([idaccountl3] ,[idprofileaccount] ,[profaccclassuserlastchange] ,[profaccclassdatelastchange]) VALUES (50,2,1,'2019-01-01 00:00:00.000')

GO

INSERT INTO [vta].[tblaccounttypereport] ([accounttypereportname] ,[accounttypereportdescription]) VALUES ('E-R','Estado de Resultados')
INSERT INTO [vta].[tblaccounttypereport] ([accounttypereportname] ,[accounttypereportdescription]) VALUES ('B-G','Balance General')
GO

INSERT INTO [vta].[tblaccountl4accounttype] ([idaccountl4] ,[idaccounttypereport]) VALUES (1,1)
INSERT INTO [vta].[tblaccountl4accounttype] ([idaccountl4] ,[idaccounttypereport]) VALUES (2,1)
INSERT INTO [vta].[tblaccountl4accounttype] ([idaccountl4] ,[idaccounttypereport]) VALUES (3,1)
INSERT INTO [vta].[tblaccountl4accounttype] ([idaccountl4] ,[idaccounttypereport]) VALUES (4,1)
--INSERT INTO [vta].[tblaccountl4accounttype] ([idaccountl4] ,[idaccounttypereport]) VALUES (5,1)
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

INSERT INTO [vta].[tblaccl4paymenmethods] ([idpaymentmethod] ,[idaccountl4]) VALUES (2,1)--Visa
INSERT INTO [vta].[tblaccl4paymenmethods] ([idpaymentmethod] ,[idaccountl4]) VALUES (3,1)--MasterCard
INSERT INTO [vta].[tblaccl4paymenmethods] ([idpaymentmethod] ,[idaccountl4]) VALUES (4,1)--American Express
--INSERT INTO [vta].[tblaccl4paymenmethods] ([idpaymentmethod] ,[idaccountl4]) VALUES (16,1) /*Hay duda en este metodó de pago Discover con respecto a si debe ir en la cuenta contable Ingresos en TC*/
INSERT INTO [vta].[tblaccl4paymenmethods] ([idpaymentmethod] ,[idaccountl4]) VALUES (11,3)--Wire transfer
INSERT INTO [vta].[tblaccl4paymenmethods] ([idpaymentmethod] ,[idaccountl4]) VALUES (12,2)--Cash
INSERT INTO [vta].[tblaccl4paymenmethods] ([idpaymentmethod] ,[idaccountl4]) VALUES (25,4)--PayPal
INSERT INTO [vta].[tblaccl4paymenmethods] ([idpaymentmethod] ,[idaccountl4]) VALUES (23,5)--CXC
INSERT INTO [vta].[tblaccl4paymenmethods] ([idpaymentmethod] ,[idaccountl4]) VALUES (13,6)--Courtesy

GO



/*******SEGUNDA PARTE DE EJECUCIÓN*******/

/***************************************************
Author: Ruben Magaña Alvarado
Date:   27 Junio 2019
Description: Create payment table
***************************************************/
IF EXISTS ( SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[vta].[tblpayment]') AND type in (N'U') )
 DROP TABLE [vta].[tblpayment]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [vta].[tblpayment](
	[idpayment]			 [bigint] IDENTITY(1,1) NOT NULL,
	[idinvoice]			 [bigint] NOT NULL,
	[idbaccount]		 [int] NOT NULL,
	[idbankprodttype]	 [int] NOT NULL,
	[paymentdate]		 [datetime] NOT NULL,/*Fecha del insercion que establece el usuario*/
	[paymentamount]	     [numeric](14, 2) NOT NULL,
	[paymentauthref]	 [nvarchar](250) NULL,
	[paymentcreatedby]   [int] NOT NULL,     /*creacion de registro*/
	[paymentcreateon] 	 [datetime] NOT NULL,/*creacion de registro*/
	[paymentupdatedby]   [int] NOT NULL,	 /*actualizacion de registro*/
	[paymentupdatedon]   [datetime] NOT NULL,/*actualizacion de registro*/

 CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED 
(
	[idpayment] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



/***************************************************
Update: Ruben Magaña Alvarado
Date:   3 Julio 2019
Description: Create payment table
***************************************************/
--------------------------------------------------------------------------------------------------------------------------------
------------------------------- INVOICE / INVOICEITEM / INVOICEATTACHMENTS / INVOICE COMMENTS / LOG's  -------------------------
--------------------------------------------------------------------------------------------------------------------------------
  


IF EXISTS ( SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[vta].[tblinvoice]') AND type in (N'U') )
	DROP TABLE [vta].[tblinvoice]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vta].[tblinvoice](
	[idinvoice]			[bigint]	IDENTITY(1,1) NOT NULL,/*llave primaria*/
	[idcompany]			[int]		NOT NULL,
	[idcurrency]		[int]		NULL,
	[invoicedate]		[datetime]  NOT NULL,
	[invoicenumber]		[int]		NOT NULL,
	[invoicecreatedby]	[int]		NOT NULL,/*Creacion del registro*/
	[invoicecreateon]	[datetime]  NOT NULL,
	[invoiceupdatedby]	[int]		NOT NULL,
	[invoiceupdateon]	[datetime]  NOT NULL,/*Actualizacion del registro*/
PRIMARY KEY CLUSTERED 
(
	[idinvoice] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO




------------------------------- L O G S ----------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------

/****** Object:  Table [vta].[tblinvoiceLog]    Script Date: 16/05/2019 09:43:13 a. m. ******/
IF EXISTS ( SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[vta].[tblinvoiceLog]') AND type in (N'U') )
	Drop table  [vta].[tblinvoiceLog]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vta].[tblinvoiceLog](
	[LogId]				[int] IDENTITY(1,1) NOT NULL,
	[LogDate]			[datetime]  NULL,
	[LogUser]			[int]		NULL,
	[LogObs]			[nvarchar] (max) NULL,
	[idinvoice]			[bigint]    NOT NULL,
	[idcompany]			[int]		NOT NULL,
	[idcurrency]		[int]		NULL,
	[invoicedate]		[datetime]  NOT NULL,
	[invoicenumber]		[int]		NOT NULL,
	[invoicecreatedby]	[int]		NOT NULL,/*Creacion del registro*/
	[invoicecreateon]	[datetime]  NOT NULL,
	[invoiceupdatedby]	[int]		NOT NULL,
	[invoiceupdateon]	[datetime]  NOT NULL,/*Actualizacion del registro*/
	[invoicedeletedby]	[int]		NOT NULL,
	[invoicedeleteon]	[datetime]  NOT NULL,/*Eliminacion del registro*/
PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


------------------------------- I N V O I C E  I T E M S -----------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------

/****** Object:  Table [vta].[tblinvoiceditem]    Script Date: 16/05/2019 09:43:13 a. m. ******/

IF EXISTS ( SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[vta].[tblinvoiceditem]') AND type in (N'U') )
	Drop table [vta].[tblinvoiceditem];
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vta].[tblinvoiceditem](
	[idinvoiceitem]		  [bigint]  IDENTITY(1,1) NOT NULL,
	[idinvoice]			  [bigint]	NOT NULL,
	[iduser]			  [int]		NOT NULL,
	[idaccountl4]		  [int]		NOT NULL,
	[idinvoiceitemstatus] [int]		NOT NULL,
	[idbudgettype]		  [int]		NOT NULL,
	[idsupplier]		  [int]		NOT NULL,
	[itemsubtotal]		  [numeric]	(14, 2) NULL,
	[itemdescription]	  [nvarchar](250) NULL,
	[ditemistax]		  [bit]		 NOT NULL,
	[itemtax]			  [numeric]  (14, 2) NOT NULL,/*[invoiceditemtaxes]	cambio*/
	[itemidentifier]	  [nvarchar] (50) NULL,
	[itemsupplierother]	  [nvarchar] (250) NULL,
	[itemothertax]		  [numeric]  (14, 2) NOT NULL,
	[itemsinglepayment]	  [bit]		 NOT NULL,
	[itemcreatedby]		  [int]		 NOT NULL,/*Creacion del registro*/
	[itemcreateon]	      [datetime] NOT NULL,/*Creacion del registro*/
	[itemupdatedby]		  [int]		 NOT NULL,/*Actualizacion del registro*/
	[itemupdateon]		  [datetime] NOT NULL,/*Actualizacion del registro*/

PRIMARY KEY CLUSTERED 
(
	[idinvoiceitem] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO





IF EXISTS ( SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[vta].[tblinvoiceditemLog]') AND type in (N'U') )
	Drop table [vta].[tblinvoiceditemLog]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vta].[tblinvoiceditemLog](
	[LogId]								 [int] IDENTITY(1,1) NOT NULL,
	[LogDate]							 [datetime] NULL,
	[LogUser]							 [int] NULL,
	[LogObs]							 [nvarchar](max) NULL,
	[idinvoiceitem]						 [int] NOT NULL,
	[idinvoice]							 [int] NOT NULL,
	[idaccl4]							 [int] NOT NULL,
	[idinvoiceitemstatus]				 [int] NOT NULL,
	[Iduser]							 [int] NOT NULL,
	[invoiceditemsubtotal]				 [numeric](14, 2) NULL,
	[description]						 [nvarchar](250) NULL,
	[deleted]							 [bit] NULL,
	[invoiceditemistax]					 [bit] NOT NULL,
	[invoiceditemtaxes]					 [numeric](14, 2) NOT NULL,
	[invoiceditembillidentifier]		 [nvarchar](50) NULL,
	[idsupplier]						 [int] NOT NULL,
	[invoiceditemsupplierother]			 [nvarchar](250) NULL,
	[invoiceditemothertaxesammount]		 [numeric](14, 2) NOT NULL,
	[invoiceditemsingleexibitionpayment] [bit] NOT NULL,
	[idbudgettype]						 [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO







ALTER TABLE [vta].[tblinvoiceditemLog] ADD  DEFAULT ((0)) FOR [invoiceditemistax]
GO
ALTER TABLE [vta].[tblinvoiceditemLog] ADD  DEFAULT ((0)) FOR [invoiceditemtaxes]
GO
ALTER TABLE [vta].[tblinvoiceditemLog] ADD  DEFAULT ((0)) FOR [idsupplier]
GO
ALTER TABLE [vta].[tblinvoiceditemLog] ADD  DEFAULT ((0)) FOR [invoiceditemothertaxesammount]
GO
ALTER TABLE [vta].[tblinvoiceditemLog] ADD  DEFAULT ((1)) FOR [invoiceditemsingleexibitionpayment]
GO
ALTER TABLE [vta].[tblinvoiceditemLog] ADD  DEFAULT ((1)) FOR [idbudgettype]
GO



------------------------------- P E R M I S S I O N S  U S E R S ---------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------

IF EXISTS ( SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[vta].[tblpermissions]') AND type in (N'U') )
	Drop Table  [vta].[tblpermissions]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vta].[tblpermissions](
	[idPermission]			[int] IDENTITY(1,1) NOT NULL,
	[permissionController]	[nvarchar](50) NOT NULL,
	[permissionAction]		[nvarchar](60) NOT NULL,
	[permissionArea]		[nvarchar](50) NULL,
	[permissionImageClass]	[nvarchar](50) NOT NULL,
	[permissionTitle]		[nvarchar](150) NOT NULL,
	[permissionDescription] [nvarchar](250) NULL,
	[permissisonActiveli]	[nvarchar](50) NULL,
	[permissionEstatus]		[bit] NOT NULL,
	[permissionParentId]	[int] NOT NULL,
	[permissionIsParent]	[bit] NOT NULL,
	[permissionHasChild]	[bit] NULL,
	[PermissionMenu]		[bit] NOT NULL,
	[permissionIsHtml]		[bit] NOT NULL,
 CONSTRAINT [PK__tblpermi__A08B06815C06032B] PRIMARY KEY CLUSTERED 
(
	[idPermission] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



IF EXISTS ( SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[vta].[tbluserpermissions]') AND type in (N'U') )
	Drop table  [vta].[tbluserpermissions]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vta].[tbluserpermissions](
	[IdUserPermission]	   [int] IDENTITY(1,1) NOT NULL,
	[idUser]			   [int] NULL,
	[idPermission]		   [int] NULL,
	[userpermissionActive] [bit] NULL,
 CONSTRAINT [PK__tbluserp__106F9019DADDCE40] PRIMARY KEY CLUSTERED 
(
	[IdUserPermission] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO




ALTER TABLE [vta].[tbluserpermissions]  WITH CHECK ADD  CONSTRAINT [FK_VTA_tbluserpermissions_tblpermissions_1] FOREIGN KEY([idPermission])
REFERENCES [vta].[tblpermissions] ([idPermission])
GO
ALTER TABLE [vta].[tbluserpermissions] CHECK CONSTRAINT [FK_VTA_tbluserpermissions_tblpermissions_1]
GO
ALTER TABLE [vta].[tbluserpermissions]  WITH CHECK ADD  CONSTRAINT [FK_VTA_tbluserpermissions_tblusers_1] FOREIGN KEY([idUser])
REFERENCES [dbo].[tblusers] ([idUser])
GO
ALTER TABLE [vta].[tbluserpermissions] CHECK CONSTRAINT [FK_VTA_tbluserpermissions_tblusers_1]
GO


/**********************************************************************************************************/
IF EXISTS ( SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[vta].[tblincomemovement]') AND type in (N'U') )
	drop table  [vta].[tblincomemovement]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vta].[tblincomemovement](
	[idincomeMovement]		   [bigint] IDENTITY(1,1) NOT NULL,
	[idincome]				   [bigint] NOT NULL,
	[idbaccount]			   [int] NOT NULL,
	[idbankaccnttype]		   [int] NOT NULL,
	[idtpv]					   [int] NULL,
	[incomemovcard]            [nvarchar](50) NULL,
	[incomemovapplicationdate] [datetime] NOT NULL,
	[incomemovchargedamount]   [numeric](14, 2) NOT NULL,
	[incomemovauthref]		   [nvarchar](250) NULL,
	[incomemovcreationdate]    [datetime] NOT NULL,
	[incomemovcreatedby]       [int] NOT NULL,
	[incomemovupdatedon]	   [datetime] NOT NULL,
	[incomemovupdatedby]	   [int] NOT NULL,
	[incomemovauthcode]		[nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[idincomemovement] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO




ALTER TABLE [vta].[tblincomemovement]  WITH CHECK ADD  CONSTRAINT [FK_tblincomemovement_tbltpv] FOREIGN KEY([idtpv])
REFERENCES [vta].[tbltpv] ([idtpv])
GO
ALTER TABLE [vta].[tblincomemovement] CHECK CONSTRAINT [FK_tblincomemovement_tbltpv]
GO
ALTER TABLE [vta].[tblincomemovement]  WITH CHECK ADD  CONSTRAINT [FK_VTA_IncomeMovement_tblbaccounprodttype_1] FOREIGN KEY([idbankaccnttype])
REFERENCES [vta].[tblbankprodttype] ([idbankprodttype])
GO
ALTER TABLE [vta].[tblincomemovement] CHECK CONSTRAINT [FK_VTA_IncomeMovement_tblbaccounprodttype_1]
GO
ALTER TABLE [vta].[tblincomemovement]  WITH CHECK ADD  CONSTRAINT [FK_VTA_IncomeMovement_tblbankaccount_1] FOREIGN KEY([idbaccount])
REFERENCES [vta].[tblbankaccount] ([idbaccount])
GO
ALTER TABLE [vta].[tblincomemovement] CHECK CONSTRAINT [FK_VTA_IncomeMovement_tblbankaccount_1]
GO
ALTER TABLE [vta].[tblincomemovement]  WITH CHECK ADD  CONSTRAINT [FK_VTA_tblincomemovement_tblincome_1] FOREIGN KEY([idincome])
REFERENCES [vta].[tblincome] ([idincome])
GO
ALTER TABLE [vta].[tblincomemovement] CHECK CONSTRAINT [FK_VTA_tblincomemovement_tblincome_1]
GO



ALTER TABLE [vta].[tblincome] WITH CHECK ADD  CONSTRAINT [FK_tblincome_tblccurrency_1] FOREIGN KEY([idcurrency])
REFERENCES [dbo].[tblcurrencies] ([idCurrency])
GO
ALTER TABLE [vta].[tblincome] CHECK CONSTRAINT [FK_tblincome_tblccurrency_1]
GO

UPDATE tblusers set passwordHash='AFbNwEQyy4XQOtrh/heXtyd1C9FphddmfTy1JlhUtpw58ggeTKKV/UFGcbN3eGXE0A=='

GO


insert into dbo.tblparameters (parameterValue,parameterDescription)values
('16','VTA-Tax'),('1','AccesVTCPermission')
go





update dbo.tblusers set idprofileaccount=1 where idUser=1

GO


SET IDENTITY_INSERT [vta].[tbluserbacount] ON 
GO
INSERT [vta].[tbluserbacount] ([iduserbacount], [iduser], [idbaccount], [userbacountcreatedby], [userbacountcreationdate], [userbacountactive]) VALUES (55, 1, 1, 1, CAST(N'2018-01-01T00:00:00.000' AS DateTime), 1)
GO
INSERT [vta].[tbluserbacount] ([iduserbacount], [iduser], [idbaccount], [userbacountcreatedby], [userbacountcreationdate], [userbacountactive]) VALUES (56, 1, 2, 1, CAST(N'2018-01-01T00:00:00.000' AS DateTime), 1)
GO
INSERT [vta].[tbluserbacount] ([iduserbacount], [iduser], [idbaccount], [userbacountcreatedby], [userbacountcreationdate], [userbacountactive]) VALUES (57, 1, 3, 0, CAST(N'2018-01-01T00:00:00.000' AS DateTime), 1)
GO
INSERT [vta].[tbluserbacount] ([iduserbacount], [iduser], [idbaccount], [userbacountcreatedby], [userbacountcreationdate], [userbacountactive]) VALUES (58, 1, 4, 1, CAST(N'2018-01-01T00:00:00.000' AS DateTime), 1)
GO
INSERT [vta].[tbluserbacount] ([iduserbacount], [iduser], [idbaccount], [userbacountcreatedby], [userbacountcreationdate], [userbacountactive]) VALUES (59, 1, 5, 0, CAST(N'2018-01-01T00:00:00.000' AS DateTime), 1)
GO
INSERT [vta].[tbluserbacount] ([iduserbacount], [iduser], [idbaccount], [userbacountcreatedby], [userbacountcreationdate], [userbacountactive]) VALUES (60, 1, 6, 1, CAST(N'2018-01-01T00:00:00.000' AS DateTime), 0)
GO
INSERT [vta].[tbluserbacount] ([iduserbacount], [iduser], [idbaccount], [userbacountcreatedby], [userbacountcreationdate], [userbacountactive]) VALUES (61, 1, 7, 1, CAST(N'2018-01-01T00:00:00.000' AS DateTime), 1)
GO
SET IDENTITY_INSERT [vta].[tbluserbacount] OFF
GO

GO

SET IDENTITY_INSERT [vta].[tblbankprodttype] ON 
GO
INSERT [vta].[tblbankprodttype] ([idbankprodttype], [bankprodttypename], [bankprodttypeshortname], [bankprodttypedescription], [bankprodttypeactive]) VALUES (1, N'Sin tipo', N'', N'Sin tipo', 1)
GO
INSERT [vta].[tblbankprodttype] ([idbankprodttype], [bankprodttypename], [bankprodttypeshortname], [bankprodttypedescription], [bankprodttypeactive]) VALUES (2, N'Débito', N'TDD', N'Tarjeta débito', 1)
GO
INSERT [vta].[tblbankprodttype] ([idbankprodttype], [bankprodttypename], [bankprodttypeshortname], [bankprodttypedescription], [bankprodttypeactive]) VALUES (3, N'Crédito', N'TDC', N'Tarjeta de crédito', 1)
GO
INSERT [vta].[tblbankprodttype] ([idbankprodttype], [bankprodttypename], [bankprodttypeshortname], [bankprodttypedescription], [bankprodttypeactive]) VALUES (4, N'Cheques', N'Cheques', N'Cheques', 1)
GO
INSERT [vta].[tblbankprodttype] ([idbankprodttype], [bankprodttypename], [bankprodttypeshortname], [bankprodttypedescription], [bankprodttypeactive]) VALUES (5, N'Efectivo', N'EFVO', N'Efectivo', 1)
GO
INSERT [vta].[tblbankprodttype] ([idbankprodttype], [bankprodttypename], [bankprodttypeshortname], [bankprodttypedescription], [bankprodttypeactive]) VALUES (6, N'Transferencias', N'TRANS', N'Transferencias', 1)
GO
INSERT [vta].[tblbankprodttype] ([idbankprodttype], [bankprodttypename], [bankprodttypeshortname], [bankprodttypedescription], [bankprodttypeactive]) VALUES (7, N'CXC', N'CXC', N'Cuentas por cobrar', 1)
GO
SET IDENTITY_INSERT [vta].[tblbankprodttype] OFF
GO




SET IDENTITY_INSERT [vta].[tblbaccounprodttype] ON 
GO
INSERT [vta].[tblbaccounprodttype] ([idbaccountprodtype], [idbaccount], [idbankprodttype], [baccountprodtypecreatedby], [baccountprodtypecreationdate], [baccountprodtypeactive], [baccountprodtypeallowneg]) VALUES (1, 1, 4, 1, CAST(N'2018-08-05T00:00:00.000' AS DateTime), 1, 0)
GO
INSERT [vta].[tblbaccounprodttype] ([idbaccountprodtype], [idbaccount], [idbankprodttype], [baccountprodtypecreatedby], [baccountprodtypecreationdate], [baccountprodtypeactive], [baccountprodtypeallowneg]) VALUES (2, 1, 6, 1, CAST(N'2018-08-05T00:00:00.000' AS DateTime), 1, 0)
GO
INSERT [vta].[tblbaccounprodttype] ([idbaccountprodtype], [idbaccount], [idbankprodttype], [baccountprodtypecreatedby], [baccountprodtypecreationdate], [baccountprodtypeactive], [baccountprodtypeallowneg]) VALUES (3, 2, 4, 1, CAST(N'2018-08-05T00:00:00.000' AS DateTime), 1, 0)
GO
INSERT [vta].[tblbaccounprodttype] ([idbaccountprodtype], [idbaccount], [idbankprodttype], [baccountprodtypecreatedby], [baccountprodtypecreationdate], [baccountprodtypeactive], [baccountprodtypeallowneg]) VALUES (4, 2, 6, 1, CAST(N'2018-08-05T00:00:00.000' AS DateTime), 1, 0)
GO
INSERT [vta].[tblbaccounprodttype] ([idbaccountprodtype], [idbaccount], [idbankprodttype], [baccountprodtypecreatedby], [baccountprodtypecreationdate], [baccountprodtypeactive], [baccountprodtypeallowneg]) VALUES (5, 4, 6, 1, CAST(N'2018-08-05T00:00:00.000' AS DateTime), 1, 0)
GO
INSERT [vta].[tblbaccounprodttype] ([idbaccountprodtype], [idbaccount], [idbankprodttype], [baccountprodtypecreatedby], [baccountprodtypecreationdate], [baccountprodtypeactive], [baccountprodtypeallowneg]) VALUES (6, 5, 3, 1, CAST(N'2018-08-05T00:00:00.000' AS DateTime), 1, 0)
GO
INSERT [vta].[tblbaccounprodttype] ([idbaccountprodtype], [idbaccount], [idbankprodttype], [baccountprodtypecreatedby], [baccountprodtypecreationdate], [baccountprodtypeactive], [baccountprodtypeallowneg]) VALUES (7, 6, 6, 1, CAST(N'2018-08-05T00:00:00.000' AS DateTime), 1, 0)
GO
INSERT [vta].[tblbaccounprodttype] ([idbaccountprodtype], [idbaccount], [idbankprodttype], [baccountprodtypecreatedby], [baccountprodtypecreationdate], [baccountprodtypeactive], [baccountprodtypeallowneg]) VALUES (8, 1, 2, 1, CAST(N'2019-08-04T00:00:00.000' AS DateTime), 1, 0)
GO
INSERT [vta].[tblbaccounprodttype] ([idbaccountprodtype], [idbaccount], [idbankprodttype], [baccountprodtypecreatedby], [baccountprodtypecreationdate], [baccountprodtypeactive], [baccountprodtypeallowneg]) VALUES (9, 2, 2, 1, CAST(N'2019-08-04T00:00:00.000' AS DateTime), 1, 0)
GO
INSERT [vta].[tblbaccounprodttype] ([idbaccountprodtype], [idbaccount], [idbankprodttype], [baccountprodtypecreatedby], [baccountprodtypecreationdate], [baccountprodtypeactive], [baccountprodtypeallowneg]) VALUES (10, 3, 2, 1, CAST(N'2019-08-04T00:00:00.000' AS DateTime), 1, 0)
GO
INSERT [vta].[tblbaccounprodttype] ([idbaccountprodtype], [idbaccount], [idbankprodttype], [baccountprodtypecreatedby], [baccountprodtypecreationdate], [baccountprodtypeactive], [baccountprodtypeallowneg]) VALUES (11, 4, 2, 1, CAST(N'2019-08-04T00:00:00.000' AS DateTime), 1, 0)
GO
INSERT [vta].[tblbaccounprodttype] ([idbaccountprodtype], [idbaccount], [idbankprodttype], [baccountprodtypecreatedby], [baccountprodtypecreationdate], [baccountprodtypeactive], [baccountprodtypeallowneg]) VALUES (12, 5, 2, 1, CAST(N'2019-08-04T00:00:00.000' AS DateTime), 1, 0)
GO
INSERT [vta].[tblbaccounprodttype] ([idbaccountprodtype], [idbaccount], [idbankprodttype], [baccountprodtypecreatedby], [baccountprodtypecreationdate], [baccountprodtypeactive], [baccountprodtypeallowneg]) VALUES (13, 6, 2, 1, CAST(N'2019-08-04T00:00:00.000' AS DateTime), 1, 0)
GO
INSERT [vta].[tblbaccounprodttype] ([idbaccountprodtype], [idbaccount], [idbankprodttype], [baccountprodtypecreatedby], [baccountprodtypecreationdate], [baccountprodtypeactive], [baccountprodtypeallowneg]) VALUES (14, 1, 3, 1, CAST(N'2019-08-04T00:00:00.000' AS DateTime), 1, 0)
GO
INSERT [vta].[tblbaccounprodttype] ([idbaccountprodtype], [idbaccount], [idbankprodttype], [baccountprodtypecreatedby], [baccountprodtypecreationdate], [baccountprodtypeactive], [baccountprodtypeallowneg]) VALUES (15, 2, 3, 1, CAST(N'2019-08-04T00:00:00.000' AS DateTime), 1, 0)
GO
INSERT [vta].[tblbaccounprodttype] ([idbaccountprodtype], [idbaccount], [idbankprodttype], [baccountprodtypecreatedby], [baccountprodtypecreationdate], [baccountprodtypeactive], [baccountprodtypeallowneg]) VALUES (16, 3, 3, 1, CAST(N'2019-08-04T00:00:00.000' AS DateTime), 1, 0)
GO
INSERT [vta].[tblbaccounprodttype] ([idbaccountprodtype], [idbaccount], [idbankprodttype], [baccountprodtypecreatedby], [baccountprodtypecreationdate], [baccountprodtypeactive], [baccountprodtypeallowneg]) VALUES (17, 4, 3, 1, CAST(N'2019-08-04T00:00:00.000' AS DateTime), 1, 0)
GO
INSERT [vta].[tblbaccounprodttype] ([idbaccountprodtype], [idbaccount], [idbankprodttype], [baccountprodtypecreatedby], [baccountprodtypecreationdate], [baccountprodtypeactive], [baccountprodtypeallowneg]) VALUES (18, 5, 3, 1, CAST(N'2019-08-04T00:00:00.000' AS DateTime), 1, 0)
GO
INSERT [vta].[tblbaccounprodttype] ([idbaccountprodtype], [idbaccount], [idbankprodttype], [baccountprodtypecreatedby], [baccountprodtypecreationdate], [baccountprodtypeactive], [baccountprodtypeallowneg]) VALUES (19, 6, 3, 1, CAST(N'2019-08-04T00:00:00.000' AS DateTime), 1, 0)
GO
INSERT [vta].[tblbaccounprodttype] ([idbaccountprodtype], [idbaccount], [idbankprodttype], [baccountprodtypecreatedby], [baccountprodtypecreationdate], [baccountprodtypeactive], [baccountprodtypeallowneg]) VALUES (20, 1, 5, 1, CAST(N'2019-08-04T00:00:00.000' AS DateTime), 1, 0)
GO
INSERT [vta].[tblbaccounprodttype] ([idbaccountprodtype], [idbaccount], [idbankprodttype], [baccountprodtypecreatedby], [baccountprodtypecreationdate], [baccountprodtypeactive], [baccountprodtypeallowneg]) VALUES (21, 2, 5, 1, CAST(N'2019-08-04T00:00:00.000' AS DateTime), 1, 0)
GO
INSERT [vta].[tblbaccounprodttype] ([idbaccountprodtype], [idbaccount], [idbankprodttype], [baccountprodtypecreatedby], [baccountprodtypecreationdate], [baccountprodtypeactive], [baccountprodtypeallowneg]) VALUES (22, 3, 5, 1, CAST(N'2019-08-04T00:00:00.000' AS DateTime), 1, 0)
GO
INSERT [vta].[tblbaccounprodttype] ([idbaccountprodtype], [idbaccount], [idbankprodttype], [baccountprodtypecreatedby], [baccountprodtypecreationdate], [baccountprodtypeactive], [baccountprodtypeallowneg]) VALUES (23, 4, 5, 1, CAST(N'2019-08-04T00:00:00.000' AS DateTime), 1, 0)
GO
INSERT [vta].[tblbaccounprodttype] ([idbaccountprodtype], [idbaccount], [idbankprodttype], [baccountprodtypecreatedby], [baccountprodtypecreationdate], [baccountprodtypeactive], [baccountprodtypeallowneg]) VALUES (24, 5, 5, 1, CAST(N'2019-08-04T00:00:00.000' AS DateTime), 1, 0)
GO
INSERT [vta].[tblbaccounprodttype] ([idbaccountprodtype], [idbaccount], [idbankprodttype], [baccountprodtypecreatedby], [baccountprodtypecreationdate], [baccountprodtypeactive], [baccountprodtypeallowneg]) VALUES (25, 6, 5, 1, CAST(N'2019-08-04T00:00:00.000' AS DateTime), 1, 0)
GO
SET IDENTITY_INSERT [vta].[tblbaccounprodttype] OFF
GO




SET IDENTITY_INSERT [vta].[tblbugettype] ON 
GO
INSERT [vta].[tblbugettype] ([idbudgettype], [budgettypename], [budgettypedescription], [budgettypeorder],[budgettypeactive]) VALUES (1, N'No aplica', N'No aplica', 100,1)
GO
INSERT [vta].[tblbugettype] ([idbudgettype], [budgettypename], [budgettypedescription], [budgettypeorder],[budgettypeactive]) VALUES (2, N'Presup. Operativo', N'Presupuesto Operativo', 1,1)
GO
INSERT [vta].[tblbugettype] ([idbudgettype], [budgettypename], [budgettypedescription], [budgettypeorder],[budgettypeactive]) VALUES (3, N'Presup. Staff', N'Presupuesto Staff', 2,0)
GO
INSERT [vta].[tblbugettype] ([idbudgettype], [budgettypename], [budgettypedescription], [budgettypeorder],[budgettypeactive]) VALUES (4, N'Presup. Restaurante', N'Presupuesto Operativo', 3,0)
GO
INSERT [vta].[tblbugettype] ([idbudgettype], [budgettypename], [budgettypedescription], [budgettypeorder],[budgettypeactive]) VALUES (5, N'Presup. Especial', N'Presupuesto Operativo', 4,1)
GO
SET IDENTITY_INSERT [vta].[tblbugettype] OFF
GO



SET IDENTITY_INSERT [vta].[tblsupplier] ON 
GO
INSERT [vta].[tblsupplier] ([idsupplier], [suppliername], [supplierdescription], [supplierRFC], [supplieraddress], [supplieractive]) VALUES (1, N'Otros', N'Otros', N'DEHY270856ER3', N'Calle cerrada', 1)
GO
SET IDENTITY_INSERT [vta].[tblsupplier] OFF







SET IDENTITY_INSERT [vta].[tblcompaniesuppliers] ON 
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (1, 1, 1)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (2, 1, 2)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (3, 1, 3)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (4, 1, 4)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (5, 1, 5)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (6, 1, 6)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (7, 1, 7)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (8, 1, 8)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (9, 1, 9)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (10, 1, 10)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (11, 1, 11)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (12, 1, 12)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (13, 1, 13)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (14, 1, 14)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (15, 1, 15)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (16, 1, 16)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (17, 1, 17)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (18, 1, 18)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (19, 1, 19)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (20, 1, 20)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (21, 1, 21)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (22, 1, 22)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (23, 1, 23)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (24, 1, 24)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (25, 1, 25)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (26, 2, 1)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (27, 2, 2)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (28, 2, 3)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (29, 2, 4)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (30, 2, 5)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (31, 2, 6)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (32, 2, 7)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (33, 2, 8)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (34, 2, 9)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (35, 2, 10)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (36, 2, 11)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (37, 2, 12)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (38, 2, 13)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (39, 2, 14)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (40, 2, 15)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (41, 2, 16)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (42, 2, 17)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (43, 2, 18)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (44, 2, 19)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (45, 2, 20)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (46, 2, 21)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (47, 2, 22)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (48, 2, 23)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (49, 2, 24)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (50, 2, 25)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (51, 3, 1)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (52, 3, 2)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (53, 3, 3)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (54, 3, 4)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (55, 3, 5)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (56, 3, 6)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (57, 3, 7)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (58, 3, 8)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (59, 3, 9)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (60, 3, 10)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (61, 3, 11)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (62, 3, 12)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (63, 3, 13)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (64, 3, 14)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (65, 3, 15)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (66, 3, 16)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (67, 3, 17)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (68, 3, 18)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (69, 3, 19)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (70, 3, 20)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (71, 3, 21)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (72, 3, 22)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (73, 3, 23)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (74, 3, 24)
GO
INSERT [vta].[tblcompaniesuppliers] ([idCompanySupplier], [idCompany], [idSupplier]) VALUES (75, 3, 25)
GO
SET IDENTITY_INSERT [vta].[tblcompaniesuppliers] OFF
GO



/****TERCERA PARTE DE EJECUCION ******/



ALTER TABLE [dbo].[tblcurrencies]
ADD currencyActive BIT NULL
GO

ALTER TABLE [vta].[tbltpv] ADD iddeposittype INT, idphysicallyat INT NULL
GO

UPDATE [vta].[tbltpv] SET iddeposittype = 3,idphysicallyat = 1 WHERE idtpv = 1
GO

UPDATE [vta].[tbltpv] SET iddeposittype = 1,idphysicallyat = 1 WHERE idtpv IN (2,3,4,5,7)
GO

UPDATE [vta].[tbltpv] SET iddeposittype = 1,idphysicallyat = 2 WHERE idtpv = 6
GO

UPDATE [vta].[tbltpv] SET iddeposittype = 1,idphysicallyat = 3 WHERE idtpv IN (8,9,10,11,12,13,14,15)
GO

UPDATE [dbo].[tblcurrencies] SET
currencyActive = 1
GO

ALTER TABLE [vta].[tblcompanies]
ADD fundsGive BIT
  , fundsReceive BIT
GO

UPDATE [vta].[tblcompanies] SET fundsGive = 1, fundsReceive = 1 WHERE idcompany in(1,2,3) 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vta].[tblpaymentLog](
	[LogId] [int] IDENTITY(1,1) NOT NULL,
	[LogDate] [datetime] NULL,
	[LogUser] [int] NULL,
	[LogObs] [nvarchar](max) NULL,
	[idPayment] [bigint] NOT NULL,
	[idInvoice] [int] NOT NULL,
	[idBAccount] [int] NOT NULL,
	[idBankAccntType] [int] NOT NULL,
	[applicationDate] [datetime] NOT NULL,
	[chargedAmount] [numeric](14, 2) NOT NULL,
	[authRef] [nvarchar](250) NULL,
	[creationDate] [datetime] NOT NULL,
	[createdBy] [int] NOT NULL,
	[updatedOn] [datetime] NOT NULL,
	[updatedBy] [int] NOT NULL,
	[paymentdeletedby] [int] NULL,
	[paymentdeletedon] [datetime] NULL,
 CONSTRAINT [PK_Payment_log] PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vta].[tblparameters](
	[idParameter] [int] IDENTITY(1,1) NOT NULL,
	[parameterValue] [varchar](50) NOT NULL,
	[parameterShortName] [text] NOT NULL,
	[parameterDescription] [text] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idParameter] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


SET IDENTITY_INSERT [vta].[tblparameters] ON
GO
INSERT INTO [vta].[tblparameters]([idParameter],[parameterValue],[parameterShortName],[parameterDescription]) VALUES (1,'87','accl4TaxbyBudget','Cuenta contable con la cual se realizará cargo en el estado de resultados.')
GO
INSERT INTO [vta].[tblparameters]([idParameter],[parameterValue],[parameterShortName],[parameterDescription]) VALUES (2,'0.5','maxDiffThreshold','Tolerancia máxima a la diferencia para las comparaciones de montos en las conciliaciones.')
GO
SET IDENTITY_INSERT [vta].[tblparameters] OFF
GO

CREATE TABLE [vta].[tblfinancetype](
	[idFinanceType] [int] IDENTITY(1,1) NOT NULL,
	[financeTypeName] [nvarchar](70) NULL,
	[financeTypeCode] [nvarchar](10) NULL,
	[financeTypeActive] [bit] NULL,
 CONSTRAINT [PK_tblfinancetype] PRIMARY KEY CLUSTERED 
(
	[idFinanceType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vta].[tblbankstatementmethod](
	[idBankStatementMethod] [int] IDENTITY(1,1) NOT NULL,
	[bankstatementmethodName] [varchar](60) NOT NULL,
	[bankstatementmethodShortName] [varchar](60) NOT NULL,
	[bankstatementmethodDescription] [varchar](60) NULL,
	[bankstatementmethodActive] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idBankStatementMethod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vta].[tblbankstatements](
	[idBankStatements] [bigint] IDENTITY(1,1) NOT NULL,
	[idBAccount] [int] NOT NULL,
	[idTPV] [int] NOT NULL,
	[idCompany] [int] NOT NULL,
	[idBankStatementMethod] [int] NULL,
	[bankstatementAplicationDate] [datetime] NOT NULL,
	[bankstatementAppliedAmmount] [numeric](14, 2) NOT NULL,
	[bankstatementBankFee] [numeric](14, 2) NULL,
	[bankstatementAppliedAmmountFinal] [numeric](14, 2) NULL,
	[bankStatementsTC] [nvarchar](20) NULL,
	[bankStatementsAuthCode] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[idBankStatements] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vta].[tblbankstatementsLog](
	[Log_Id] [int] IDENTITY(1,1) NOT NULL,
	[Log_Date] [datetime] NULL,
	[Log_User] [int] NULL,
	[Log_Obs] [nvarchar](max) NULL,
	[idBankStatements] [bigint] NOT NULL,
	[idBAccount] [int] NOT NULL,
	[idTPV] [int] NOT NULL,
	[idCompany] [int] NOT NULL,
	[idBankStatementMethod] [int] NULL,
	[bankstatementAplicationDate] [datetime] NOT NULL,
	[bankstatementAppliedAmmount] [numeric](14, 2) NOT NULL,
	[bankstatementBankFee] [numeric](14, 2) NULL,
	[bankstatementAppliedAmmountFinal] [numeric](14, 2) NULL,
	[bankStatementsTC] [nvarchar](20) NULL,
	[bankStatementsAuthCode] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[Log_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vta].[tblbankstatementsdet](
	[idBankStatementsDet] [bigint] IDENTITY(1,1) NOT NULL,
	[idBankStatements] [bigint] NULL,
	[idTPV] [int] NOT NULL,
	[bankStatementsDetSaleDate] [datetime] NULL,
	[bankStatementsDetSaleAmnt] [numeric](14, 2) NULL,
	[bankStatementsDetTC] [nvarchar](20) NULL,
	[bankStatementsDetAuthCode] [nvarchar](20) NULL,
 CONSTRAINT [PK_tblbankstatementsdet] PRIMARY KEY CLUSTERED 
(
	[idBankStatementsDet] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vta].[tblbankstatements2](
	[idBankStatements2] [bigint] IDENTITY(1,1) NOT NULL,
	[idBAccount] [int] NOT NULL,
	[idMovementType] [int] NOT NULL,
	[bankstatements2AplicationDate] [datetime] NOT NULL,
	[bankstatements2Concept] [nvarchar](500) NOT NULL,
	[bankstatements2Charge] [numeric](14, 2) NULL,
	[bankstatements2Pay] [numeric](14, 2) NULL,
	[bankstatements2CreatedBy] [int] NULL,
	[bankstatements2CreatedOn] [datetime] NULL,
	[bankstatements2UpdatedBy] [int] NULL,
	[bankstatements2UpdatedOn] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idBankStatements2] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vta].[tblmovementtype](
	[idMovementType] [int] IDENTITY(1,1) NOT NULL,
	[movementTypeName] [nvarchar](50) NULL,
	[movementTypeActive] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idMovementType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vta].[tblbankstatements2Log](
	[Log_Id] [int] IDENTITY(1,1) NOT NULL,
	[Log_Date] [datetime] NULL,
	[Log_User] [int] NULL,
	[Log_Obs] [nvarchar](max) NULL,
	[idBankStatements2] [bigint] NOT NULL,
	[idBAccount] [int] NOT NULL,
	[idMovementType] [int] NOT NULL,
	[bankstatements2AplicationDate] [datetime] NOT NULL,
	[bankstatements2Concept] [nvarchar](500) NOT NULL,
	[bankstatements2Charge] [numeric](14, 2) NULL,
	[bankstatements2Pay] [numeric](14, 2) NULL,
	[bankstatements2CreatedBy] [int] NULL,
	[bankstatements2CreatedOn] [datetime] NULL,
	[bankstatements2UpdatedBy] [int] NULL,
	[bankstatements2UpdatedOn] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Log_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vta].[tblbankstatementsgap](
	[idBankStatementsGap] [int] IDENTITY(1,1) NOT NULL,
	[bankstatementsgapValue] [numeric](14, 2) NOT NULL,
	[bankstatementsgapDate] [datetime] NOT NULL,
	[bankstatementsgapDescription] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idBankStatementsGap] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vta].[tblbankstatereserv](
	[idBankstatReserv] [bigint] IDENTITY(1,1) NOT NULL,
	[idBankStatements] [bigint] NOT NULL,
	[idReservationPayment] [int] NOT NULL,
	[authCode] [nvarchar](50) NULL,
	[cardNumber] [nvarchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[idBankstatReserv] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [vta].[tblfondos](
	[idFondos] [int] IDENTITY(1,1) NOT NULL,
	[idPaymentMethod] [int] NOT NULL,
	[idFinancialMethod] [int] NOT NULL,
	[fondofechaEntrega] [datetime] NOT NULL,
	[fondoFechaInicio] [datetime] NOT NULL,
	[fondoFechaFin] [datetime] NOT NULL,
	[fondoMonto] [numeric](14, 2) NOT NULL,
	[fondoCreatedby] [int] NOT NULL,
	[fondoCreationDate] [datetime] NOT NULL,
	[fondoComments] [nvarchar](255) NULL,
	[fondoFee] [numeric](14, 2) NOT NULL,
	[fondoInvoice] [bigint] NULL,
	[idFinanceType] [int] NULL,
 CONSTRAINT [tblfondos_PK] PRIMARY KEY CLUSTERED 
(
	[idFondos] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [vta].[tblfondosLog](
	[LogId] [int] IDENTITY(1,1) NOT NULL,
	[LogDate] [datetime] NULL,
	[LogUser] [int] NULL,
	[LogObs] [nvarchar](max) NULL,
	[idFondos] [int] NULL,
	[idPaymentMethod] [int] NOT NULL,
	[idFinancialMethod] [int] NOT NULL,
	[fondofechaEntrega] [datetime] NOT NULL,
	[fondoFechaInicio] [datetime] NOT NULL,
	[fondoFechaFin] [datetime] NOT NULL,
	[fondoMonto] [numeric](14, 2) NOT NULL,
	[fondoCreatedby] [int] NOT NULL,
	[fondoCreationDate] [datetime] NOT NULL,
	[fondoComments] [nvarchar](255) NULL,
	[fondoFee] [numeric](14, 2) NOT NULL,
	[fondoInvoice] [bigint] NULL,
	[idFinanceType] [int] NULL,
 CONSTRAINT [tblfondos_Log_PK] PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


CREATE TABLE [vta].[tblfondosmaxammount](
	[idFondosMax] [int] IDENTITY(1,1) NOT NULL,
	[idBAccount] [int] NOT NULL,
	[fondosmaxAmmount] [numeric](14, 2) NOT NULL,
	[fondosmaxDescription] [varchar](255) NOT NULL,
	[fondosmaxCreatedby] [int] NOT NULL,
	[fondosmaxCreationDate] [datetime] NOT NULL,
 CONSTRAINT [idFondosMax_PK] PRIMARY KEY CLUSTERED 
(
	[idFondosMax] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vta].[tblfondosmaxammountLog](
	[LogId] [int] IDENTITY(1,1) NOT NULL,
	[LogDate] [datetime] NULL,
	[LogUser] [int] NULL,
	[LogObs] [nvarchar](500) NULL,
	[idFondosMax] [int] NOT NULL,
	[idBAccount] [int] NOT NULL,
	[fondosmaxAmmount] [numeric](14, 2) NOT NULL,
	[fondosmaxCreatedby] [int] NOT NULL,
	[fondosmaxCreationDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [vta].[tblconciliationsegmentaccl4fee](
	[idConciliationsegmentaccl4] [int] IDENTITY(1,1) NOT NULL,
	[idSegment] [int] NOT NULL,
	[idAccountl4] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idConciliationsegmentaccl4] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vta].[tblbankstatepurchase](
	[idBankstatPurchase] [bigint] IDENTITY(1,1) NOT NULL,
	[idBankStatements] [bigint] NOT NULL,
	[idPaymentPurchase] [int] NOT NULL,
	[authCode] [nvarchar](50) NULL,
	[cardNumber] [nvarchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[idBankstatPurchase] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vta].[tblbankstateparentreserv](
	[idBankstatParentReserv] [bigint] IDENTITY(1,1) NOT NULL,
	[idBankStatements] [bigint] NOT NULL,
	[idReservationParentPayment] [int] NOT NULL,
	[authCode] [nvarchar](50) NULL,
	[cardNumber] [nvarchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[idBankstatParentReserv] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vta].[tblexternalgroup](
	[IdExternalGroup] [int] IDENTITY(1,1) NOT NULL,
	[externalgroupName] [varchar](100) NOT NULL,
	[externalgroupShortName] [varchar](50) NOT NULL,
	[externalgroupActive] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdExternalGroup] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vta].[tblextgroupcompanies](
	[IdExtGroupCompanies] [int] IDENTITY(1,1) NOT NULL,
	[idCompany] [int] NOT NULL,
	[IdExternalGroup] [int] NOT NULL,
	[externalgroupcompanyActive] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdExtGroupCompanies] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [vta].[tblcompanydevelopment](
idCompanyDevelopment INT IDENTITY(1,1) NOT NULL,
idCompany INT NOT NULL,
idHotelChain INT NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idCompanyDevelopment] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [vta].[tblcompanygroupdevelopment](
idCompanyGroupDevelop INT IDENTITY(1,1) NOT NULL,
idCompanyParent INT NOT NULL,
idCompanyChild INT NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idCompanyGroupDevelop] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [vta].[tblbankstateincome](
idBankstatIncome BIGINT IDENTITY(1,1) NOT NULL,
idBankStatements BIGINT NOT NULL,
idincomeMovement BIGINT NOT NULL,
authCode NVARCHAR(30) NULL,
cardNumber NVARCHAR(30) NULL,
 CONSTRAINT [PK_tblbankstateincome] PRIMARY KEY CLUSTERED 
(
	[idBankstatIncome] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [vta].[tbldeposittypes](
	[idDepositType] [int] IDENTITY(1,1) NOT NULL,
	[depositTypeDescription] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_tbldeposittypes] PRIMARY KEY CLUSTERED 
(
	[idDepositType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [vta].[tblbankstatements] ADD  DEFAULT ((0)) FOR [bankstatementBankFee]
GO

ALTER TABLE [vta].[tblbankstatements] ADD  DEFAULT ((0)) FOR [bankstatementAppliedAmmountFinal]
GO

ALTER TABLE [vta].[tblfondosLog] ADD  DEFAULT ((0)) FOR [fondoFee]
GO

ALTER TABLE [vta].[tblbankstatements]  WITH CHECK ADD  CONSTRAINT [fk_tblbankstatementmethod_tblbankstatementmethod_1] FOREIGN KEY([idBankStatementMethod])
REFERENCES [vta].[tblbankstatementmethod] ([idBankStatementMethod])
GO

ALTER TABLE [vta].[tblbankstatements] CHECK CONSTRAINT [fk_tblbankstatementmethod_tblbankstatementmethod_1]
GO

ALTER TABLE [vta].[tblbankstatements]  WITH CHECK ADD  CONSTRAINT [fk_tblbankstatements_tblbankaccount_1] FOREIGN KEY([idBAccount])
REFERENCES [vta].[tblbankaccount] ([idbaccount])
GO

ALTER TABLE [vta].[tblbankstatements] CHECK CONSTRAINT [fk_tblbankstatements_tblbankaccount_1]
GO

ALTER TABLE [vta].[tblbankstatements]  WITH CHECK ADD  CONSTRAINT [fk_tblbankstatements_tblcompany_1] FOREIGN KEY([idCompany])
REFERENCES [vta].[tblcompanies] ([idcompany])
GO

ALTER TABLE [vta].[tblbankstatements] CHECK CONSTRAINT [fk_tblbankstatements_tblcompany_1]
GO

ALTER TABLE [vta].[tblbankstatements]  WITH CHECK ADD  CONSTRAINT [fk_tblbankstatements_tbltpv_1] FOREIGN KEY([idTPV])
REFERENCES [vta].[tbltpv] ([idtpv])
GO

ALTER TABLE [vta].[tblbankstatements] CHECK CONSTRAINT [fk_tblbankstatements_tbltpv_1]
GO

ALTER TABLE [vta].[tblbankstatereserv]  WITH CHECK ADD  CONSTRAINT [fk_tblbankstatereserv_tblbankstatements_1] FOREIGN KEY([idBankStatements])
REFERENCES [vta].[tblbankstatements] ([idBankStatements])
GO

ALTER TABLE [vta].[tblbankstatereserv] CHECK CONSTRAINT [fk_tblbankstatereserv_tblbankstatements_1]
GO

ALTER TABLE [vta].[tblfondos] ADD  DEFAULT ((0)) FOR [fondoFee]
GO

ALTER TABLE [vta].[tblfondos]  WITH CHECK ADD  CONSTRAINT [fk_tblfondos_tblbankaccount_1] FOREIGN KEY([idPaymentMethod])
REFERENCES [vta].[tblbankaccount] ([idbaccount])
GO

ALTER TABLE [vta].[tblfondos] CHECK CONSTRAINT [fk_tblfondos_tblbankaccount_1]
GO

ALTER TABLE [vta].[tblfondos]  WITH CHECK ADD  CONSTRAINT [fk_tblfondos_tblbankaccount_2] FOREIGN KEY([idFinancialMethod])
REFERENCES [vta].[tblbankaccount] ([idbaccount])
GO

ALTER TABLE [vta].[tblfondos] CHECK CONSTRAINT [fk_tblfondos_tblbankaccount_2]
GO

ALTER TABLE [vta].[tblfondos]  WITH CHECK ADD  CONSTRAINT [fk_tblfondos_tbluser_1] FOREIGN KEY([fondoCreatedby])
REFERENCES [dbo].[tblusers] ([idUser])
GO

ALTER TABLE [vta].[tblfondos] CHECK CONSTRAINT [fk_tblfondos_tbluser_1]
GO

ALTER TABLE [vta].[tblfondosmaxammount]  WITH CHECK ADD  CONSTRAINT [FK_tblfondosmaxammount_tblbankaccount] FOREIGN KEY([idBAccount])
REFERENCES [vta].[tblbankaccount] ([idbaccount])
GO

ALTER TABLE [vta].[tblfondosmaxammount] CHECK CONSTRAINT [FK_tblfondosmaxammount_tblbankaccount]
GO

ALTER TABLE [vta].[tblfondosmaxammount]  WITH CHECK ADD  CONSTRAINT [fk_tblfondosmaxammount_tbluser_1] FOREIGN KEY([fondosmaxCreatedby])
REFERENCES [dbo].[tblusers] ([idUser])
GO

ALTER TABLE [vta].[tblfondosmaxammount] CHECK CONSTRAINT [fk_tblfondosmaxammount_tbluser_1]
GO

ALTER TABLE [vta].[tblconciliationsegmentaccl4fee]  WITH CHECK ADD  CONSTRAINT [fk_tblconciliationsegmentaccl4fee_tblaccountsl4_1] FOREIGN KEY([idAccountl4])
REFERENCES [vta].[tblaccountsl4] ([idAccountl4])
GO

ALTER TABLE [vta].[tblconciliationsegmentaccl4fee] CHECK CONSTRAINT [fk_tblconciliationsegmentaccl4fee_tblaccountsl4_1]
GO

ALTER TABLE [vta].[tblconciliationsegmentaccl4fee]  WITH CHECK ADD  CONSTRAINT [fk_tblconciliationsegmentaccl4fee_tblsegment_1] FOREIGN KEY([idSegment])
REFERENCES [vta].[tblsegment] ([idsegment])
GO

ALTER TABLE [vta].[tblconciliationsegmentaccl4fee] CHECK CONSTRAINT [fk_tblconciliationsegmentaccl4fee_tblsegment_1]
GO

ALTER TABLE [vta].[tblfondos]  WITH CHECK ADD  CONSTRAINT [FK_VTA_tblfondos_tblfinancetype_1] FOREIGN KEY([idFinanceType])
REFERENCES [vta].[tblfinancetype] ([idFinanceType])
GO

ALTER TABLE [vta].[tblfondos] CHECK CONSTRAINT [FK_VTA_tblfondos_tblfinancetype_1]
GO

ALTER TABLE [vta].[tblbankstatereserv]  WITH CHECK ADD  CONSTRAINT [fk_tblbankstatereserv_tblreservationspayment_1] FOREIGN KEY([idReservationPayment])
REFERENCES [dbo].[tblreservationspayment] ([idReservationPayment])
GO

ALTER TABLE [vta].[tblbankstatereserv] CHECK CONSTRAINT [fk_tblbankstatereserv_tblreservationspayment_1]
GO




ALTER TABLE [dbo].[tblpaymentspurchases] ADD
 CONSTRAINT [PK_tblpaymentspurchases] PRIMARY KEY CLUSTERED 
(
	[idPaymentPurchase] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO

ALTER TABLE [vta].[tblbankstatepurchase]  WITH CHECK ADD  CONSTRAINT [fk_tblbankstatepurchase_tblbankstatements_1] FOREIGN KEY([idBankStatements])
REFERENCES [vta].[tblbankstatements] ([idBankStatements])
GO

ALTER TABLE [vta].[tblbankstatepurchase] CHECK CONSTRAINT [fk_tblbankstatepurchase_tblbankstatements_1]
GO

ALTER TABLE [vta].[tblbankstatepurchase]  WITH CHECK ADD  CONSTRAINT [fk_tblbankstatepurchase_tblpaymentspurchases_1] FOREIGN KEY([idPaymentPurchase])
REFERENCES [dbo].[tblpaymentspurchases] ([idPaymentPurchase])
GO

ALTER TABLE [vta].[tblbankstatepurchase] CHECK CONSTRAINT [fk_tblbankstatepurchase_tblpaymentspurchases_1]
GO

ALTER TABLE [vta].[tblbankstateparentreserv]  WITH CHECK ADD  CONSTRAINT [fk_tblbankstateparentreserv_tblbankstatements_1] FOREIGN KEY([idBankStatements])
REFERENCES [vta].[tblbankstatements] ([idBankStatements])
GO

ALTER TABLE [vta].[tblbankstateparentreserv] CHECK CONSTRAINT [fk_tblbankstateparentreserv_tblbankstatements_1]
GO

ALTER TABLE [vta].[tblbankstateparentreserv]  WITH CHECK ADD  CONSTRAINT [fk_tblbankstatepurchase_tblreservationsparentpayment_1] FOREIGN KEY([idReservationParentPayment])
REFERENCES [dbo].[tblreservationsparentpayment] ([idReservationParentPayment])
GO

ALTER TABLE [vta].[tblbankstateparentreserv] CHECK CONSTRAINT [fk_tblbankstatepurchase_tblreservationsparentpayment_1]
GO



ALTER TABLE [vta].[tblinvoicecomments]  WITH CHECK ADD  CONSTRAINT  [FK_tblinvoiceComments_tblinvoice] FOREIGN KEY([idinvoice])
REFERENCES [vta].[tblinvoice] ([idinvoice])
GO

ALTER TABLE [vta].[tblinvoicecomments] CHECK CONSTRAINT [FK_tblinvoiceComments_tblinvoice]
GO


ALTER TABLE [vta].[tblinvoiceattach]  WITH CHECK ADD  CONSTRAINT [FK_VTA_tblinvoiceattach_tblinvoice_1] FOREIGN KEY([idinvoice])
REFERENCES [vta].[tblinvoice] ([idinvoice])
GO

ALTER TABLE [vta].[tblinvoiceattach] CHECK CONSTRAINT [FK_VTA_tblinvoiceattach_tblinvoice_1]
GO


ALTER TABLE [vta].[tblextgroupcompanies]  WITH CHECK ADD  CONSTRAINT [fk_tblextgroupcompanies_tblexternalgroup_1] FOREIGN KEY([IdExternalGroup])
REFERENCES [vta].[tblexternalgroup] ([IdExternalGroup])
GO
ALTER TABLE [vta].[tblextgroupcompanies] CHECK CONSTRAINT [fk_tblextgroupcompanies_tblexternalgroup_1]
GO

ALTER TABLE [vta].[tblextgroupcompanies]  WITH CHECK ADD  CONSTRAINT [fk_tblextgroupcompanies_tblcompanies_1] FOREIGN KEY([idCompany])
REFERENCES [vta].[tblcompanies] ([idCompany])
GO
ALTER TABLE [vta].[tblextgroupcompanies] CHECK CONSTRAINT [fk_tblextgroupcompanies_tblcompanies_1]
GO


ALTER TABLE [vta].[tblcompaniesuppliers]  WITH CHECK ADD  CONSTRAINT [fk_tblcompaniessupplier_tblsupplier_1] FOREIGN KEY([idsupplier])
REFERENCES [dbo].[tblSuppliers] ([idSupplier])
GO

ALTER TABLE [vta].[tblcompaniesuppliers] CHECK CONSTRAINT [fk_tblcompaniessupplier_tblsupplier_1]
GO

ALTER TABLE [vta].[tblcompanydevelopment]  WITH CHECK ADD  CONSTRAINT [fk_tblcompanydevelopment_tblcompanies_1] FOREIGN KEY([idCompany])
REFERENCES [vta].[tblcompanies] ([idcompany])
GO

ALTER TABLE [vta].[tblcompanydevelopment] CHECK CONSTRAINT [fk_tblcompanydevelopment_tblcompanies_1]
GO

ALTER TABLE [vta].[tblcompanydevelopment]  WITH CHECK ADD  CONSTRAINT [fk_tblcompanydevelopment_tblhotelchains_1] FOREIGN KEY([idHotelChain])
REFERENCES [dbo].[tblhotelchains] ([idHotelChain])
GO

ALTER TABLE [vta].[tblcompanydevelopment] CHECK CONSTRAINT [fk_tblcompanydevelopment_tblhotelchains_1]
GO

ALTER TABLE [dbo].[tblreservations]  WITH CHECK ADD  CONSTRAINT [fk_tblreservations_tblpartners_1] FOREIGN KEY([idPartner])
REFERENCES [dbo].[tblpartners] ([idPartner])
GO

ALTER TABLE [dbo].[tblreservations] CHECK CONSTRAINT [fk_tblreservations_tblpartners_1]
GO

ALTER TABLE [vta].[tblcompanygroupdevelopment]  WITH CHECK ADD  CONSTRAINT [fk_tblcompanygroupdevelopment_tblcompanies_1] FOREIGN KEY([idCompanyParent])
REFERENCES [vta].[tblcompanies] ([idcompany])
GO

ALTER TABLE [vta].[tblcompanygroupdevelopment] CHECK CONSTRAINT [fk_tblcompanygroupdevelopment_tblcompanies_1]
GO

ALTER TABLE [vta].[tblcompanygroupdevelopment]  WITH CHECK ADD  CONSTRAINT [fk_tblcompanygroupdevelopment_tblcompanies_2] FOREIGN KEY([idCompanyChild])
REFERENCES [vta].[tblcompanies] ([idcompany])
GO

ALTER TABLE [vta].[tblcompanygroupdevelopment] CHECK CONSTRAINT [fk_tblcompanygroupdevelopment_tblcompanies_2]
GO

ALTER TABLE [vta].[tblbankstatements2]  WITH CHECK ADD  CONSTRAINT [fk_tblbankstatements2_tblbankaccount_1] FOREIGN KEY([idBAccount])
REFERENCES [vta].[tblbankaccount] ([idBAccount])
GO

ALTER TABLE [vta].[tblbankstatements2] CHECK CONSTRAINT [fk_tblbankstatements2_tblbankaccount_1]
GO

--ALTER TABLE [vta].[tblbankstatements2]  WITH CHECK ADD  CONSTRAINT [fk_tblbankstatements2_tblmovementtype_1] FOREIGN KEY([idMovementType])
--REFERENCES [vta].[tblmovementtype] ([idMovementType])
--GO

--ALTER TABLE [vta].[tblbankstatements2] CHECK CONSTRAINT [fk_tblbankstatements2_tblmovementtype_1]
--GO

ALTER TABLE [vta].[tblbankstatements2]  WITH CHECK ADD  CONSTRAINT [fk_tblbankstatements2_tblusers_1] FOREIGN KEY([bankstatements2CreatedBy])
REFERENCES [dbo].[tblusers] ([idUser])
GO

ALTER TABLE [vta].[tblbankstatements2] CHECK CONSTRAINT [fk_tblbankstatements2_tblusers_1]
GO

ALTER TABLE [vta].[tblbankstatements2]  WITH CHECK ADD  CONSTRAINT [fk_tblbankstatements2_tblusers_2] FOREIGN KEY([bankstatements2UpdatedBy])
REFERENCES [dbo].[tblusers] ([idUser])
GO

ALTER TABLE [vta].[tblbankstatements2] CHECK CONSTRAINT [fk_tblbankstatements2_tblusers_2]
GO

ALTER TABLE [vta].[tblbankstatementsdet]  WITH CHECK ADD  CONSTRAINT [FK_tblbankstatementsdet_tblbankstatements] FOREIGN KEY([idBankStatements])
REFERENCES [vta].[tblbankstatements] ([idBankStatements])
GO

ALTER TABLE [vta].[tblbankstatementsdet] CHECK CONSTRAINT [FK_tblbankstatementsdet_tblbankstatements]
GO

ALTER TABLE [vta].[tblbankstatementsdet]  WITH CHECK ADD  CONSTRAINT [FK_tblbankstatementsdet_tbltpv] FOREIGN KEY([idTPV])
REFERENCES [vta].[tbltpv] ([idTPV])
GO

ALTER TABLE [vta].[tblbankstatementsdet] CHECK CONSTRAINT [FK_tblbankstatementsdet_tbltpv]
GO

ALTER TABLE [vta].[tblbankstateincome]  WITH CHECK ADD  CONSTRAINT [fk_tblbankstateincome_tblbankstatements_1] FOREIGN KEY([idBankStatements])
REFERENCES [vta].[tblbankstatements] ([idBankStatements])
GO

ALTER TABLE [vta].[tblbankstateincome] CHECK CONSTRAINT [fk_tblbankstateincome_tblbankstatements_1]
GO

ALTER TABLE [vta].[tblbankstateincome]  WITH CHECK ADD  CONSTRAINT [fk_tblbankstateincome_tblincomemovement_1] FOREIGN KEY([idincomeMovement])
REFERENCES [vta].[tblincomemovement] ([idincomeMovement])
GO

ALTER TABLE [vta].[tblbankstateincome] CHECK CONSTRAINT [fk_tblbankstateincome_tblincomemovement_1]
GO


ALTER TABLE [vta].[tbltpv]  WITH CHECK ADD  CONSTRAINT [FK_tbltpv_tblcompanies] FOREIGN KEY([idphysicallyat])
REFERENCES [vta].[tblcompanies] ([idcompany])
GO

ALTER TABLE [vta].[tbltpv] CHECK CONSTRAINT [FK_tbltpv_tblcompanies]
GO

/*** SE INSERTA INFORMACIÓN A LAS TABLAS ***/
SET IDENTITY_INSERT [vta].[tblconciliationsegmentaccl4fee] ON 
GO
INSERT INTO [vta].[tblconciliationsegmentaccl4fee]([idConciliationsegmentaccl4], [idSegment], [idAccountl4]) VALUES(1,1,219)
GO
SET IDENTITY_INSERT [vta].[tblconciliationsegmentaccl4fee] OFF
GO

SET IDENTITY_INSERT [vta].[tblattachments] ON 
GO
INSERT [vta].[tblattachments] ([idattachment], [attachmentname], [attachmentactive]) VALUES (1, N'Factura', 1)
GO
INSERT [vta].[tblattachments] ([idattachment], [attachmentname], [attachmentactive]) VALUES (2, N'Comprobante simple', 1)
GO
INSERT [vta].[tblattachments] ([idattachment], [attachmentname], [attachmentactive]) VALUES (3, N'Nota de venta', 1)
GO
INSERT [vta].[tblattachments] ([idattachment], [attachmentname], [attachmentactive]) VALUES (4, N'Nota de remisión', 1)
GO
INSERT [vta].[tblattachments] ([idattachment], [attachmentname], [attachmentactive]) VALUES (5, N'Voucher', 1)
GO
SET IDENTITY_INSERT [vta].[tblattachments] OFF
GO

SET IDENTITY_INSERT [vta].[tblbankstatementmethod] ON 
GO
INSERT [vta].[tblbankstatementmethod] ([idBankStatementMethod], [bankstatementmethodName], [bankstatementmethodShortName], [bankstatementmethodDescription], [bankstatementmethodActive]) VALUES (1, N'Sin conciliar', N'Sin conciliar', N'No se ha iniciado o finalizado el proceso de conciliación', 1)
GO
INSERT [vta].[tblbankstatementmethod] ([idBankStatementMethod], [bankstatementmethodName], [bankstatementmethodShortName], [bankstatementmethodDescription], [bankstatementmethodActive]) VALUES (2, N'Manual', N'Manual', N'Se realiza conciliación de manera manual', 1)
GO
INSERT [vta].[tblbankstatementmethod] ([idBankStatementMethod], [bankstatementmethodName], [bankstatementmethodShortName], [bankstatementmethodDescription], [bankstatementmethodActive]) VALUES (3, N'Sistema', N'Sistema', N'Se realiza conciliación por sistema', 1)
GO
SET IDENTITY_INSERT [vta].[tblbankstatementmethod] OFF
GO

SET IDENTITY_INSERT [vta].[tblsourcedata] ON 
GO
INSERT [vta].[tblsourcedata] ([idsourcedata], [sourcedataname], [sourcedataactive]) VALUES (8, N'Pagos Membership', 1)
GO
SET IDENTITY_INSERT [vta].[tblsourcedata] OFF
GO

SET IDENTITY_INSERT [vta].[tblbankaccountsourcedata] ON 
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (1, 1, 3, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (2, 1, 4, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (3, 1, 5, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (4, 2, 3, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (5, 2, 4, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (6, 2, 5, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (9, 3, 3, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (10, 3, 4, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (11, 3, 5, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (12, 4, 3, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (13, 4, 4, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (14, 4, 5, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (15, 1, 2, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (16, 1, 6, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (17, 1, 7, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (18, 1, 8, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (19, 2, 2, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (20, 2, 6, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (21, 2, 7, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (22, 2, 8, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (23, 3, 2, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (24, 3, 6, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (25, 4, 2, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (26, 5, 2, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (27, 5, 3, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (28, 5, 4, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (29, 5, 5, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (30, 5, 6, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (31, 5, 7, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (32, 5, 8, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (33, 6, 2, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (34, 6, 3, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (35, 6, 4, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (36, 6, 5, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (37, 6, 6, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (38, 6, 7, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (39, 6, 8, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (40, 7, 2, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (41, 7, 3, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (42, 7, 4, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (43, 7, 5, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (44, 7, 6, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (45, 7, 7, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (46, 7, 8, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (47, 3, 7, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (48, 3, 8, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (49, 4, 7, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
INSERT [vta].[tblbankaccountsourcedata] ([idbankaccountsourcedata], [idbaccount], [idsourcedata], [sourcedatadatestart]) VALUES (50, 4, 8, CAST(N'2019-01-01T00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [vta].[tblbankaccountsourcedata] OFF
GO
SET IDENTITY_INSERT [vta].[tblbankaccountsourcedatatypes] ON 
GO
INSERT [vta].[tblbankaccountsourcedatatypes] ([idbankaccountsourcedatatypes], [idbankaccountsourcedata], [idtype]) VALUES (1, 17, 11)
GO
INSERT [vta].[tblbankaccountsourcedatatypes] ([idbankaccountsourcedatatypes], [idbankaccountsourcedata], [idtype]) VALUES (2, 17, 12)
GO
INSERT [vta].[tblbankaccountsourcedatatypes] ([idbankaccountsourcedatatypes], [idbankaccountsourcedata], [idtype]) VALUES (3, 17, 25)
GO
INSERT [vta].[tblbankaccountsourcedatatypes] ([idbankaccountsourcedatatypes], [idbankaccountsourcedata], [idtype]) VALUES (4, 18, 11)
GO
INSERT [vta].[tblbankaccountsourcedatatypes] ([idbankaccountsourcedatatypes], [idbankaccountsourcedata], [idtype]) VALUES (5, 18, 12)
GO
INSERT [vta].[tblbankaccountsourcedatatypes] ([idbankaccountsourcedatatypes], [idbankaccountsourcedata], [idtype]) VALUES (6, 18, 25)
GO
INSERT [vta].[tblbankaccountsourcedatatypes] ([idbankaccountsourcedatatypes], [idbankaccountsourcedata], [idtype]) VALUES (7, 21, 11)
GO
INSERT [vta].[tblbankaccountsourcedatatypes] ([idbankaccountsourcedatatypes], [idbankaccountsourcedata], [idtype]) VALUES (8, 21, 12)
GO
INSERT [vta].[tblbankaccountsourcedatatypes] ([idbankaccountsourcedatatypes], [idbankaccountsourcedata], [idtype]) VALUES (9, 21, 25)
GO
INSERT [vta].[tblbankaccountsourcedatatypes] ([idbankaccountsourcedatatypes], [idbankaccountsourcedata], [idtype]) VALUES (10, 22, 11)
GO
INSERT [vta].[tblbankaccountsourcedatatypes] ([idbankaccountsourcedatatypes], [idbankaccountsourcedata], [idtype]) VALUES (11, 22, 12)
GO
INSERT [vta].[tblbankaccountsourcedatatypes] ([idbankaccountsourcedatatypes], [idbankaccountsourcedata], [idtype]) VALUES (12, 22, 25)
GO
INSERT [vta].[tblbankaccountsourcedatatypes] ([idbankaccountsourcedatatypes], [idbankaccountsourcedata], [idtype]) VALUES (13, 47, 11)
GO
INSERT [vta].[tblbankaccountsourcedatatypes] ([idbankaccountsourcedatatypes], [idbankaccountsourcedata], [idtype]) VALUES (14, 47, 12)
GO
INSERT [vta].[tblbankaccountsourcedatatypes] ([idbankaccountsourcedatatypes], [idbankaccountsourcedata], [idtype]) VALUES (15, 47, 25)
GO
INSERT [vta].[tblbankaccountsourcedatatypes] ([idbankaccountsourcedatatypes], [idbankaccountsourcedata], [idtype]) VALUES (16, 48, 11)
GO
INSERT [vta].[tblbankaccountsourcedatatypes] ([idbankaccountsourcedatatypes], [idbankaccountsourcedata], [idtype]) VALUES (17, 48, 12)
GO
INSERT [vta].[tblbankaccountsourcedatatypes] ([idbankaccountsourcedatatypes], [idbankaccountsourcedata], [idtype]) VALUES (18, 48, 25)
GO
INSERT [vta].[tblbankaccountsourcedatatypes] ([idbankaccountsourcedatatypes], [idbankaccountsourcedata], [idtype]) VALUES (19, 49, 11)
GO
INSERT [vta].[tblbankaccountsourcedatatypes] ([idbankaccountsourcedatatypes], [idbankaccountsourcedata], [idtype]) VALUES (20, 49, 12)
GO
INSERT [vta].[tblbankaccountsourcedatatypes] ([idbankaccountsourcedatatypes], [idbankaccountsourcedata], [idtype]) VALUES (21, 49, 25)
GO
INSERT [vta].[tblbankaccountsourcedatatypes] ([idbankaccountsourcedatatypes], [idbankaccountsourcedata], [idtype]) VALUES (22, 50, 11)
GO
INSERT [vta].[tblbankaccountsourcedatatypes] ([idbankaccountsourcedatatypes], [idbankaccountsourcedata], [idtype]) VALUES (23, 50, 12)
GO
INSERT [vta].[tblbankaccountsourcedatatypes] ([idbankaccountsourcedatatypes], [idbankaccountsourcedata], [idtype]) VALUES (24, 50, 25)
GO
INSERT [vta].[tblbankaccountsourcedatatypes] ([idbankaccountsourcedatatypes], [idbankaccountsourcedata], [idtype]) VALUES (25, 31, 25)
GO
INSERT [vta].[tblbankaccountsourcedatatypes] ([idbankaccountsourcedatatypes], [idbankaccountsourcedata], [idtype]) VALUES (26, 32, 25)
GO
INSERT [vta].[tblbankaccountsourcedatatypes] ([idbankaccountsourcedatatypes], [idbankaccountsourcedata], [idtype]) VALUES (27, 38, 25)
GO
INSERT [vta].[tblbankaccountsourcedatatypes] ([idbankaccountsourcedatatypes], [idbankaccountsourcedata], [idtype]) VALUES (28, 39, 25)
GO
INSERT [vta].[tblbankaccountsourcedatatypes] ([idbankaccountsourcedatatypes], [idbankaccountsourcedata], [idtype]) VALUES (29, 45, 25)
GO
INSERT [vta].[tblbankaccountsourcedatatypes] ([idbankaccountsourcedatatypes], [idbankaccountsourcedata], [idtype]) VALUES (30, 46, 25)
GO
SET IDENTITY_INSERT [vta].[tblbankaccountsourcedatatypes] OFF
GO
SET IDENTITY_INSERT [vta].[tblsegment] ON 
GO
INSERT [vta].[tblsegment] ([idsegment], [segmentname], [Segmentshortname], [segmentdescription], [segmentactive]) VALUES (3, N'DESARROLLOS', N'DES', N'Son cadenas hoteleras', 0)
GO
SET IDENTITY_INSERT [vta].[tblsegment] OFF
GO
SET IDENTITY_INSERT [vta].[tblcompanies] ON 
GO
INSERT [vta].[tblcompanies] ([idcompany], [companyname], [companyshortname], [companyaddress], [companytelephone], [companyactive], [companyorder], [idsegment], [fundsGive], [fundsReceive]) VALUES (10, N'Palladium', N'Palladium', N'', N'', 0, 4, 3, NULL, NULL)
GO
INSERT [vta].[tblcompanies] ([idcompany], [companyname], [companyshortname], [companyaddress], [companytelephone], [companyactive], [companyorder], [idsegment], [fundsGive], [fundsReceive]) VALUES (11, N'Circle One', N'Circle One', N'', N'', 0, 5, 3, NULL, NULL)
GO
INSERT [vta].[tblcompanies] ([idcompany], [companyname], [companyshortname], [companyaddress], [companytelephone], [companyactive], [companyorder], [idsegment], [fundsGive], [fundsReceive]) VALUES (12, N'Catalonia', N'Catalonia', N'', N'', 0, 6, 3, NULL, NULL)
GO
INSERT [vta].[tblcompanies] ([idcompany], [companyname], [companyshortname], [companyaddress], [companytelephone], [companyactive], [companyorder], [idsegment], [fundsGive], [fundsReceive]) VALUES (13, N'Original', N'Original', N'', N'', 0, 7, 3, NULL, NULL)
GO
INSERT [vta].[tblcompanies] ([idcompany], [companyname], [companyshortname], [companyaddress], [companytelephone], [companyactive], [companyorder], [idsegment], [fundsGive], [fundsReceive]) VALUES (14, N'Lifestyle VC', N'Lifestyle VC', N'', N'', 0, 8, 3, NULL, NULL)
GO
INSERT [vta].[tblcompanies] ([idcompany], [companyname], [companyshortname], [companyaddress], [companytelephone], [companyactive], [companyorder], [idsegment], [fundsGive], [fundsReceive]) VALUES (15, N'Sirenis', N'Sirenis', N'', N'', 0, 9, 3, NULL, NULL)
GO
INSERT [vta].[tblcompanies] ([idcompany], [companyname], [companyshortname], [companyaddress], [companytelephone], [companyactive], [companyorder], [idsegment], [fundsGive], [fundsReceive]) VALUES (16, N'Essential', N'Essential', N'', N'', 0, 12, 3, NULL, NULL)
GO
INSERT [vta].[tblcompanies] ([idcompany], [companyname], [companyshortname], [companyaddress], [companytelephone], [companyactive], [companyorder], [idsegment], [fundsGive], [fundsReceive]) VALUES (17, N'H10 Hotels', N'H10', N'', N'', 0, 13, 3, NULL, NULL)
GO
INSERT [vta].[tblcompanies] ([idcompany], [companyname], [companyshortname], [companyaddress], [companytelephone], [companyactive], [companyorder], [idsegment], [fundsGive], [fundsReceive]) VALUES (18, N'Fiesta Vacation Club', N'Fiesta Vacation Club', N'', N'', 0, 16, 3, NULL, NULL)
GO
INSERT [vta].[tblcompanies] ([idcompany], [companyname], [companyshortname], [companyaddress], [companytelephone], [companyactive], [companyorder], [idsegment], [fundsGive], [fundsReceive]) VALUES (19, N'HD Club Vacacional', N'HD Club Vacacional', N'', N'', 0, 19, 3, NULL, NULL)
GO
INSERT [vta].[tblcompanies] ([idcompany], [companyname], [companyshortname], [companyaddress], [companytelephone], [companyactive], [companyorder], [idsegment], [fundsGive], [fundsReceive]) VALUES (20, N'Comercializadora multidestinos', N'Comercializadora multidestinos', N'', N'', 0, 20, 3, NULL, NULL)
GO
SET IDENTITY_INSERT [vta].[tblcompanies] OFF
GO

SET IDENTITY_INSERT [vta].[tblcompaniescurrencies] ON 
GO
INSERT [vta].[tblcompaniescurrencies] ([idcompaniescurrencies], [idcompany], [idcurrency], [companiescurrenciesactive]) VALUES (4, 3, 3, 1)
GO
INSERT [vta].[tblcompaniescurrencies] ([idcompaniescurrencies], [idcompany], [idcurrency], [companiescurrenciesactive]) VALUES (5, 1, 4, 1)
GO
SET IDENTITY_INSERT [vta].[tblcompaniescurrencies] OFF
GO
SET IDENTITY_INSERT [vta].[tblcompanydevelopment] ON 
GO
INSERT [vta].[tblcompanydevelopment] ([idCompanyDevelopment], [idCompany], [idHotelChain]) VALUES (1, 10, 2)
GO
INSERT [vta].[tblcompanydevelopment] ([idCompanyDevelopment], [idCompany], [idHotelChain]) VALUES (2, 11, 3)
GO
INSERT [vta].[tblcompanydevelopment] ([idCompanyDevelopment], [idCompany], [idHotelChain]) VALUES (3, 12, 4)
GO
INSERT [vta].[tblcompanydevelopment] ([idCompanyDevelopment], [idCompany], [idHotelChain]) VALUES (4, 13, 5)
GO
INSERT [vta].[tblcompanydevelopment] ([idCompanyDevelopment], [idCompany], [idHotelChain]) VALUES (5, 14, 6)
GO
INSERT [vta].[tblcompanydevelopment] ([idCompanyDevelopment], [idCompany], [idHotelChain]) VALUES (6, 15, 7)
GO
INSERT [vta].[tblcompanydevelopment] ([idCompanyDevelopment], [idCompany], [idHotelChain]) VALUES (7, 16, 11)
GO
INSERT [vta].[tblcompanydevelopment] ([idCompanyDevelopment], [idCompany], [idHotelChain]) VALUES (8, 17, 12)
GO
INSERT [vta].[tblcompanydevelopment] ([idCompanyDevelopment], [idCompany], [idHotelChain]) VALUES (9, 18, 15)
GO
INSERT [vta].[tblcompanydevelopment] ([idCompanyDevelopment], [idCompany], [idHotelChain]) VALUES (10, 19, 18)
GO
INSERT [vta].[tblcompanydevelopment] ([idCompanyDevelopment], [idCompany], [idHotelChain]) VALUES (11, 20, 19)
GO
SET IDENTITY_INSERT [vta].[tblcompanydevelopment] OFF
GO
SET IDENTITY_INSERT [vta].[tblcompanygroupdevelopment] ON 
GO
INSERT [vta].[tblcompanygroupdevelopment] ([idCompanyGroupDevelop], [idCompanyParent], [idCompanyChild]) VALUES (1, 1, 10)
GO
INSERT [vta].[tblcompanygroupdevelopment] ([idCompanyGroupDevelop], [idCompanyParent], [idCompanyChild]) VALUES (2, 1, 11)
GO
INSERT [vta].[tblcompanygroupdevelopment] ([idCompanyGroupDevelop], [idCompanyParent], [idCompanyChild]) VALUES (3, 1, 12)
GO
INSERT [vta].[tblcompanygroupdevelopment] ([idCompanyGroupDevelop], [idCompanyParent], [idCompanyChild]) VALUES (4, 1, 13)
GO
INSERT [vta].[tblcompanygroupdevelopment] ([idCompanyGroupDevelop], [idCompanyParent], [idCompanyChild]) VALUES (5, 1, 14)
GO
INSERT [vta].[tblcompanygroupdevelopment] ([idCompanyGroupDevelop], [idCompanyParent], [idCompanyChild]) VALUES (6, 1, 15)
GO
INSERT [vta].[tblcompanygroupdevelopment] ([idCompanyGroupDevelop], [idCompanyParent], [idCompanyChild]) VALUES (7, 1, 16)
GO
INSERT [vta].[tblcompanygroupdevelopment] ([idCompanyGroupDevelop], [idCompanyParent], [idCompanyChild]) VALUES (8, 1, 17)
GO
INSERT [vta].[tblcompanygroupdevelopment] ([idCompanyGroupDevelop], [idCompanyParent], [idCompanyChild]) VALUES (9, 1, 18)
GO
INSERT [vta].[tblcompanygroupdevelopment] ([idCompanyGroupDevelop], [idCompanyParent], [idCompanyChild]) VALUES (10, 1, 19)
GO
INSERT [vta].[tblcompanygroupdevelopment] ([idCompanyGroupDevelop], [idCompanyParent], [idCompanyChild]) VALUES (11, 1, 20)
GO
INSERT [vta].[tblcompanygroupdevelopment] ([idCompanyGroupDevelop], [idCompanyParent], [idCompanyChild]) VALUES (12, 2, 10)
GO
INSERT [vta].[tblcompanygroupdevelopment] ([idCompanyGroupDevelop], [idCompanyParent], [idCompanyChild]) VALUES (13, 2, 11)
GO
INSERT [vta].[tblcompanygroupdevelopment] ([idCompanyGroupDevelop], [idCompanyParent], [idCompanyChild]) VALUES (14, 2, 12)
GO
INSERT [vta].[tblcompanygroupdevelopment] ([idCompanyGroupDevelop], [idCompanyParent], [idCompanyChild]) VALUES (15, 2, 13)
GO
INSERT [vta].[tblcompanygroupdevelopment] ([idCompanyGroupDevelop], [idCompanyParent], [idCompanyChild]) VALUES (16, 2, 14)
GO
INSERT [vta].[tblcompanygroupdevelopment] ([idCompanyGroupDevelop], [idCompanyParent], [idCompanyChild]) VALUES (17, 2, 15)
GO
INSERT [vta].[tblcompanygroupdevelopment] ([idCompanyGroupDevelop], [idCompanyParent], [idCompanyChild]) VALUES (18, 2, 16)
GO
INSERT [vta].[tblcompanygroupdevelopment] ([idCompanyGroupDevelop], [idCompanyParent], [idCompanyChild]) VALUES (19, 2, 17)
GO
INSERT [vta].[tblcompanygroupdevelopment] ([idCompanyGroupDevelop], [idCompanyParent], [idCompanyChild]) VALUES (20, 2, 18)
GO
INSERT [vta].[tblcompanygroupdevelopment] ([idCompanyGroupDevelop], [idCompanyParent], [idCompanyChild]) VALUES (21, 2, 19)
GO
INSERT [vta].[tblcompanygroupdevelopment] ([idCompanyGroupDevelop], [idCompanyParent], [idCompanyChild]) VALUES (22, 2, 20)
GO
INSERT [vta].[tblcompanygroupdevelopment] ([idCompanyGroupDevelop], [idCompanyParent], [idCompanyChild]) VALUES (23, 3, 11)
GO
INSERT [vta].[tblcompanygroupdevelopment] ([idCompanyGroupDevelop], [idCompanyParent], [idCompanyChild]) VALUES (24, 3, 12)
GO
INSERT [vta].[tblcompanygroupdevelopment] ([idCompanyGroupDevelop], [idCompanyParent], [idCompanyChild]) VALUES (25, 3, 13)
GO
INSERT [vta].[tblcompanygroupdevelopment] ([idCompanyGroupDevelop], [idCompanyParent], [idCompanyChild]) VALUES (26, 3, 14)
GO
INSERT [vta].[tblcompanygroupdevelopment] ([idCompanyGroupDevelop], [idCompanyParent], [idCompanyChild]) VALUES (27, 3, 15)
GO
INSERT [vta].[tblcompanygroupdevelopment] ([idCompanyGroupDevelop], [idCompanyParent], [idCompanyChild]) VALUES (28, 3, 16)
GO
INSERT [vta].[tblcompanygroupdevelopment] ([idCompanyGroupDevelop], [idCompanyParent], [idCompanyChild]) VALUES (29, 3, 17)
GO
INSERT [vta].[tblcompanygroupdevelopment] ([idCompanyGroupDevelop], [idCompanyParent], [idCompanyChild]) VALUES (30, 3, 18)
GO
INSERT [vta].[tblcompanygroupdevelopment] ([idCompanyGroupDevelop], [idCompanyParent], [idCompanyChild]) VALUES (31, 3, 19)
GO
INSERT [vta].[tblcompanygroupdevelopment] ([idCompanyGroupDevelop], [idCompanyParent], [idCompanyChild]) VALUES (32, 3, 20)
GO
SET IDENTITY_INSERT [vta].[tblcompanygroupdevelopment] OFF
GO
SET IDENTITY_INSERT [vta].[tbldeposittypes] ON 
GO
INSERT [vta].[tbldeposittypes] ([idDepositType], [depositTypeDescription]) VALUES (1, N'Desconocido/No aplica')
GO
INSERT [vta].[tbldeposittypes] ([idDepositType], [depositTypeDescription]) VALUES (2, N'Por movimiento')
GO
INSERT [vta].[tbldeposittypes] ([idDepositType], [depositTypeDescription]) VALUES (3, N'Por período')
GO
SET IDENTITY_INSERT [vta].[tbldeposittypes] OFF
GO
SET IDENTITY_INSERT [vta].[tblexpensereportsourcedata] ON 
GO
--INSERT [vta].[tblexpensereportsourcedata] ([idexpensereportsourcedata], [idcompany], [idsourcedata], [expensereportsourcedataactive]) VALUES (3, 10, 7, 1)
--GO
--INSERT [vta].[tblexpensereportsourcedata] ([idexpensereportsourcedata], [idcompany], [idsourcedata], [expensereportsourcedataactive]) VALUES (4, 10, 8, 1)
--GO
INSERT [vta].[tblexpensereportsourcedata] ([idexpensereportsourcedata], [idcompany], [idsourcedata], [expensereportsourcedataactive]) VALUES (3, 2, 8, 1)
GO
INSERT [vta].[tblexpensereportsourcedata] ([idexpensereportsourcedata], [idcompany], [idsourcedata], [expensereportsourcedataactive]) VALUES (4, 2, 3, 1)
GO
INSERT [vta].[tblexpensereportsourcedata] ([idexpensereportsourcedata], [idcompany], [idsourcedata], [expensereportsourcedataactive]) VALUES (5, 2, 4, 1)
GO
INSERT [vta].[tblexpensereportsourcedata] ([idexpensereportsourcedata], [idcompany], [idsourcedata], [expensereportsourcedataactive]) VALUES (6, 1, 1, 1)
GO
INSERT [vta].[tblexpensereportsourcedata] ([idexpensereportsourcedata], [idcompany], [idsourcedata], [expensereportsourcedataactive]) VALUES (7, 1, 3, 1)
GO
INSERT [vta].[tblexpensereportsourcedata] ([idexpensereportsourcedata], [idcompany], [idsourcedata], [expensereportsourcedataactive]) VALUES (8, 1, 4, 1)
GO
INSERT [vta].[tblexpensereportsourcedata] ([idexpensereportsourcedata], [idcompany], [idsourcedata], [expensereportsourcedataactive]) VALUES (9, 1, 7, 1)
GO
INSERT [vta].[tblexpensereportsourcedata] ([idexpensereportsourcedata], [idcompany], [idsourcedata], [expensereportsourcedataactive]) VALUES (10, 1, 8, 1)
GO
SET IDENTITY_INSERT [vta].[tblexpensereportsourcedata] OFF
GO
SET IDENTITY_INSERT [vta].[tblexpensereportsourcedatatypes] ON 
GO
INSERT [vta].[tblexpensereportsourcedatatypes] ([idexpensereportsourcedatatypes], [idexpensereportsourcedata], [idtype], [expensereportsourcedatatypesactive]) VALUES (10, 3, 2, 1)
GO
INSERT [vta].[tblexpensereportsourcedatatypes] ([idexpensereportsourcedatatypes], [idexpensereportsourcedata], [idtype], [expensereportsourcedatatypesactive]) VALUES (11, 3, 3, 1)
GO
INSERT [vta].[tblexpensereportsourcedatatypes] ([idexpensereportsourcedatatypes], [idexpensereportsourcedata], [idtype], [expensereportsourcedatatypesactive]) VALUES (12, 3, 4, 1)
GO
INSERT [vta].[tblexpensereportsourcedatatypes] ([idexpensereportsourcedatatypes], [idexpensereportsourcedata], [idtype], [expensereportsourcedatatypesactive]) VALUES (13, 3, 11, 1)
GO
INSERT [vta].[tblexpensereportsourcedatatypes] ([idexpensereportsourcedatatypes], [idexpensereportsourcedata], [idtype], [expensereportsourcedatatypesactive]) VALUES (14, 3, 12, 1)
GO
INSERT [vta].[tblexpensereportsourcedatatypes] ([idexpensereportsourcedatatypes], [idexpensereportsourcedata], [idtype], [expensereportsourcedatatypesactive]) VALUES (15, 3, 13, 1)
GO
INSERT [vta].[tblexpensereportsourcedatatypes] ([idexpensereportsourcedatatypes], [idexpensereportsourcedata], [idtype], [expensereportsourcedatatypesactive]) VALUES (16, 3, 16, 1)
GO
INSERT [vta].[tblexpensereportsourcedatatypes] ([idexpensereportsourcedatatypes], [idexpensereportsourcedata], [idtype], [expensereportsourcedatatypesactive]) VALUES (17, 3, 23, 1)
GO
INSERT [vta].[tblexpensereportsourcedatatypes] ([idexpensereportsourcedatatypes], [idexpensereportsourcedata], [idtype], [expensereportsourcedatatypesactive]) VALUES (18, 3, 25, 1)
GO
INSERT [vta].[tblexpensereportsourcedatatypes] ([idexpensereportsourcedatatypes], [idexpensereportsourcedata], [idtype], [expensereportsourcedatatypesactive]) VALUES (19, 9, 2, 1)
GO
INSERT [vta].[tblexpensereportsourcedatatypes] ([idexpensereportsourcedatatypes], [idexpensereportsourcedata], [idtype], [expensereportsourcedatatypesactive]) VALUES (20, 9, 3, 1)
GO
INSERT [vta].[tblexpensereportsourcedatatypes] ([idexpensereportsourcedatatypes], [idexpensereportsourcedata], [idtype], [expensereportsourcedatatypesactive]) VALUES (21, 9, 4, 1)
GO
INSERT [vta].[tblexpensereportsourcedatatypes] ([idexpensereportsourcedatatypes], [idexpensereportsourcedata], [idtype], [expensereportsourcedatatypesactive]) VALUES (22, 9, 11, 1)
GO
INSERT [vta].[tblexpensereportsourcedatatypes] ([idexpensereportsourcedatatypes], [idexpensereportsourcedata], [idtype], [expensereportsourcedatatypesactive]) VALUES (23, 9, 12, 1)
GO
INSERT [vta].[tblexpensereportsourcedatatypes] ([idexpensereportsourcedatatypes], [idexpensereportsourcedata], [idtype], [expensereportsourcedatatypesactive]) VALUES (24, 9, 13, 1)
GO
INSERT [vta].[tblexpensereportsourcedatatypes] ([idexpensereportsourcedatatypes], [idexpensereportsourcedata], [idtype], [expensereportsourcedatatypesactive]) VALUES (25, 9, 16, 1)
GO
INSERT [vta].[tblexpensereportsourcedatatypes] ([idexpensereportsourcedatatypes], [idexpensereportsourcedata], [idtype], [expensereportsourcedatatypesactive]) VALUES (26, 9, 23, 1)
GO
INSERT [vta].[tblexpensereportsourcedatatypes] ([idexpensereportsourcedatatypes], [idexpensereportsourcedata], [idtype], [expensereportsourcedatatypesactive]) VALUES (27, 9, 25, 1)
GO
INSERT [vta].[tblexpensereportsourcedatatypes] ([idexpensereportsourcedatatypes], [idexpensereportsourcedata], [idtype], [expensereportsourcedatatypesactive]) VALUES (28, 10, 2, 1)
GO
INSERT [vta].[tblexpensereportsourcedatatypes] ([idexpensereportsourcedatatypes], [idexpensereportsourcedata], [idtype], [expensereportsourcedatatypesactive]) VALUES (29, 10, 3, 1)
GO
INSERT [vta].[tblexpensereportsourcedatatypes] ([idexpensereportsourcedatatypes], [idexpensereportsourcedata], [idtype], [expensereportsourcedatatypesactive]) VALUES (30, 10, 4, 1)
GO
INSERT [vta].[tblexpensereportsourcedatatypes] ([idexpensereportsourcedatatypes], [idexpensereportsourcedata], [idtype], [expensereportsourcedatatypesactive]) VALUES (31, 10, 11, 1)
GO
INSERT [vta].[tblexpensereportsourcedatatypes] ([idexpensereportsourcedatatypes], [idexpensereportsourcedata], [idtype], [expensereportsourcedatatypesactive]) VALUES (32, 10, 12, 1)
GO
INSERT [vta].[tblexpensereportsourcedatatypes] ([idexpensereportsourcedatatypes], [idexpensereportsourcedata], [idtype], [expensereportsourcedatatypesactive]) VALUES (33, 10, 13, 1)
GO
INSERT [vta].[tblexpensereportsourcedatatypes] ([idexpensereportsourcedatatypes], [idexpensereportsourcedata], [idtype], [expensereportsourcedatatypesactive]) VALUES (34, 10, 16, 1)
GO
INSERT [vta].[tblexpensereportsourcedatatypes] ([idexpensereportsourcedatatypes], [idexpensereportsourcedata], [idtype], [expensereportsourcedatatypesactive]) VALUES (35, 10, 23, 1)
GO
INSERT [vta].[tblexpensereportsourcedatatypes] ([idexpensereportsourcedatatypes], [idexpensereportsourcedata], [idtype], [expensereportsourcedatatypesactive]) VALUES (36, 10, 25, 1)
GO
SET IDENTITY_INSERT [vta].[tblexpensereportsourcedatatypes] OFF
GO
SET IDENTITY_INSERT [vta].[tblexternalgroup] ON 
GO
INSERT [vta].[tblexternalgroup] ([IdExternalGroup], [externalgroupName], [externalgroupShortName], [externalgroupActive]) VALUES (1, N'Empresa', N'Empresa', 1)
GO
SET IDENTITY_INSERT [vta].[tblexternalgroup] OFF
GO
SET IDENTITY_INSERT [vta].[tblextgroupcompanies] ON 
GO
INSERT [vta].[tblextgroupcompanies] ([IdExtGroupCompanies], [idCompany], [IdExternalGroup], [externalgroupcompanyActive]) VALUES (1, 1, 1, 1)
GO
INSERT [vta].[tblextgroupcompanies] ([IdExtGroupCompanies], [idCompany], [IdExternalGroup], [externalgroupcompanyActive]) VALUES (2, 2, 1, 1)
GO
SET IDENTITY_INSERT [vta].[tblextgroupcompanies] OFF
GO
SET IDENTITY_INSERT [vta].[tblfinancetype] ON 
GO
INSERT [vta].[tblfinancetype] ([idFinanceType], [financeTypeName], [financeTypeCode], [financeTypeActive]) VALUES (1, N'Financiamiento entre compañias', N'FNC', 1)
GO
SET IDENTITY_INSERT [vta].[tblfinancetype] OFF
GO

--SET IDENTITY_INSERT [vta].[tblmovementtype] ON 
--GO
--INSERT [vta].[tblmovementtype] ([idMovementType], [movementTypeName], [movementTypeActive]) VALUES (1, N'Transferencias', 1)
--GO
--INSERT [vta].[tblmovementtype] ([idMovementType], [movementTypeName], [movementTypeActive]) VALUES (2, N'Depositos', 1)
--GO
--INSERT [vta].[tblmovementtype] ([idMovementType], [movementTypeName], [movementTypeActive]) VALUES (3, N'Traspasos', 1)
--GO
--INSERT [vta].[tblmovementtype] ([idMovementType], [movementTypeName], [movementTypeActive]) VALUES (4, N'Otros', 1)
--GO
--SET IDENTITY_INSERT [vta].[tblmovementtype] OFF
--GO
GO
DELETE FROM [vta].[tbluserpermissions]
GO
DELETE FROM [vta].[tblpermissions]
GO
SET IDENTITY_INSERT [vta].[tblpermissions] ON 
GO
INSERT [vta].[tblpermissions] ([idPermission], [permissionController], [permissionAction], [permissionArea], [permissionImageClass], [permissionTitle], [permissionDescription], [permissisonActiveli], [permissionEstatus], [permissionParentId], [permissionIsParent], [permissionHasChild], [PermissionMenu], [permissionIsHtml]) VALUES (1, N'Account', N'Index', N'', N'', N'Login', N'Acceso al sistema', N'1', 1, 0, 1, 1, 1, 1)
GO
INSERT [vta].[tblpermissions] ([idPermission], [permissionController], [permissionAction], [permissionArea], [permissionImageClass], [permissionTitle], [permissionDescription], [permissisonActiveli], [permissionEstatus], [permissionParentId], [permissionIsParent], [permissionHasChild], [PermissionMenu], [permissionIsHtml]) VALUES (2, N'Home', N'Index', N'', N'', N'Home', N'Acceso al menu principal', N'1', 1, 0, 1, 1, 1, 1)
GO
INSERT [vta].[tblpermissions] ([idPermission], [permissionController], [permissionAction], [permissionArea], [permissionImageClass], [permissionTitle], [permissionDescription], [permissisonActiveli], [permissionEstatus], [permissionParentId], [permissionIsParent], [permissionHasChild], [PermissionMenu], [permissionIsHtml]) VALUES (3, N'Invoice', N'invoiceapp', N'', N'', N'Nuevo Gasto', N'Vista para capturar gastos', N'1', 1, 32, 1, 1, 1, 1)
GO
INSERT [vta].[tblpermissions] ([idPermission], [permissionController], [permissionAction], [permissionArea], [permissionImageClass], [permissionTitle], [permissionDescription], [permissisonActiveli], [permissionEstatus], [permissionParentId], [permissionIsParent], [permissionHasChild], [PermissionMenu], [permissionIsHtml]) VALUES (4, N'Invoice', N'SendInvoice', N'', N'', N'Guardar factura', N'Permite guardar la factura', N'1', 1, 32, 1, 1, 1, 1)
GO
INSERT [vta].[tblpermissions] ([idPermission], [permissionController], [permissionAction], [permissionArea], [permissionImageClass], [permissionTitle], [permissionDescription], [permissisonActiveli], [permissionEstatus], [permissionParentId], [permissionIsParent], [permissionHasChild], [PermissionMenu], [permissionIsHtml]) VALUES (6, N'Invoice', N'invoicesearch', N'', N'', N'Detalles de Gastos', N'Vista para busqueda y edición de gastos', N'1', 1, 32, 1, 1, 1, 1)
GO
INSERT [vta].[tblpermissions] ([idPermission], [permissionController], [permissionAction], [permissionArea], [permissionImageClass], [permissionTitle], [permissionDescription], [permissisonActiveli], [permissionEstatus], [permissionParentId], [permissionIsParent], [permissionHasChild], [PermissionMenu], [permissionIsHtml]) VALUES (7, N'Invoice', N'GetInvoice', N'', N'', N'Obtiener factura', N'Acción que permite obtener factura', N'1', 1, 32, 1, 1, 1, 1)
GO
INSERT [vta].[tblpermissions] ([idPermission], [permissionController], [permissionAction], [permissionArea], [permissionImageClass], [permissionTitle], [permissionDescription], [permissisonActiveli], [permissionEstatus], [permissionParentId], [permissionIsParent], [permissionHasChild], [PermissionMenu], [permissionIsHtml]) VALUES (10, N'Bankreconciliation', N'Index', N'', N'', N'Conciliaciones', N'Vista de conciliaciones', N'1', 1, 35, 1, 1, 1, 1)
GO
INSERT [vta].[tblpermissions] ([idPermission], [permissionController], [permissionAction], [permissionArea], [permissionImageClass], [permissionTitle], [permissionDescription], [permissisonActiveli], [permissionEstatus], [permissionParentId], [permissionIsParent], [permissionHasChild], [PermissionMenu], [permissionIsHtml]) VALUES (13, N'Invoice', N'UpdateInvoice', N'', N'', N'Actualizar factura', N'Actualiza la información de la factura', N'1', 1, 32, 1, 1, 1, 1)
GO
INSERT [vta].[tblpermissions] ([idPermission], [permissionController], [permissionAction], [permissionArea], [permissionImageClass], [permissionTitle], [permissionDescription], [permissisonActiveli], [permissionEstatus], [permissionParentId], [permissionIsParent], [permissionHasChild], [PermissionMenu], [permissionIsHtml]) VALUES (15, N'Invoice', N'DeleteInvoice', N'', N'', N'Eliminar factura', N'Elimina la factura de la base de datos', N'1', 1, 32, 1, 1, 1, 1)
GO
INSERT [vta].[tblpermissions] ([idPermission], [permissionController], [permissionAction], [permissionArea], [permissionImageClass], [permissionTitle], [permissionDescription], [permissisonActiveli], [permissionEstatus], [permissionParentId], [permissionIsParent], [permissionHasChild], [PermissionMenu], [permissionIsHtml]) VALUES (16, N'Invoice', N'AttachFileInvoiceAjax', N'', N'', N'Guardar archivos', N'Acción que permite guardar archivos', N'1', 1, 32, 1, 1, 1, 1)
GO
INSERT [vta].[tblpermissions] ([idPermission], [permissionController], [permissionAction], [permissionArea], [permissionImageClass], [permissionTitle], [permissionDescription], [permissisonActiveli], [permissionEstatus], [permissionParentId], [permissionIsParent], [permissionHasChild], [PermissionMenu], [permissionIsHtml]) VALUES (17, N'Invoice', N'DeleteAttachment', N'', N'', N'Eliminar archivo', N'Acción para eliminar un archivo', N'1', 1, 32, 1, 1, 1, 1)
GO
INSERT [vta].[tblpermissions] ([idPermission], [permissionController], [permissionAction], [permissionArea], [permissionImageClass], [permissionTitle], [permissionDescription], [permissisonActiveli], [permissionEstatus], [permissionParentId], [permissionIsParent], [permissionHasChild], [PermissionMenu], [permissionIsHtml]) VALUES (18, N'Income', N'incomeaddvw', N'', N'', N'Nuevo ingreso', N'Vista para captura de ingreso', N'1', 1, 33, 1, 1, 1, 1)
GO
INSERT [vta].[tblpermissions] ([idPermission], [permissionController], [permissionAction], [permissionArea], [permissionImageClass], [permissionTitle], [permissionDescription], [permissisonActiveli], [permissionEstatus], [permissionParentId], [permissionIsParent], [permissionHasChild], [PermissionMenu], [permissionIsHtml]) VALUES (19, N'Income', N'incomeeditvw', N'', N'', N'Editar ingreso', N'Vista para editar ingreso', N'1', 1, 33, 1, 1, 1, 1)
GO
INSERT [vta].[tblpermissions] ([idPermission], [permissionController], [permissionAction], [permissionArea], [permissionImageClass], [permissionTitle], [permissionDescription], [permissisonActiveli], [permissionEstatus], [permissionParentId], [permissionIsParent], [permissionHasChild], [PermissionMenu], [permissionIsHtml]) VALUES (21, N'Income', N'incomedeletevw', N'', N'', N'Eliminar ingreso', N'Vista para eliminar ingreso', N'1', 1, 33, 1, 1, 1, 1)
GO
INSERT [vta].[tblpermissions] ([idPermission], [permissionController], [permissionAction], [permissionArea], [permissionImageClass], [permissionTitle], [permissionDescription], [permissisonActiveli], [permissionEstatus], [permissionParentId], [permissionIsParent], [permissionHasChild], [PermissionMenu], [permissionIsHtml]) VALUES (22, N'Config', N'Users', N'', N'', N'Configuración  usuarios', N'Vista de Usuarios', N'1', 1, 37, 1, 1, 1, 1)
GO
INSERT [vta].[tblpermissions] ([idPermission], [permissionController], [permissionAction], [permissionArea], [permissionImageClass], [permissionTitle], [permissionDescription], [permissisonActiveli], [permissionEstatus], [permissionParentId], [permissionIsParent], [permissionHasChild], [PermissionMenu], [permissionIsHtml]) VALUES (23, N'Income', N'SaveIncome', N'', N'', N'Guardar ingreso', N'Acciín para guardar ingreso', N'1', 1, 33, 1, 1, 1, 1)
GO
INSERT [vta].[tblpermissions] ([idPermission], [permissionController], [permissionAction], [permissionArea], [permissionImageClass], [permissionTitle], [permissionDescription], [permissisonActiveli], [permissionEstatus], [permissionParentId], [permissionIsParent], [permissionHasChild], [PermissionMenu], [permissionIsHtml]) VALUES (25, N'Income', N'GetIncomes', N'', N'', N'Obtener ingreso', N'Acción que permite obtener ingresos', N'1', 1, 33, 1, 1, 1, 1)
GO
INSERT [vta].[tblpermissions] ([idPermission], [permissionController], [permissionAction], [permissionArea], [permissionImageClass], [permissionTitle], [permissionDescription], [permissisonActiveli], [permissionEstatus], [permissionParentId], [permissionIsParent], [permissionHasChild], [PermissionMenu], [permissionIsHtml]) VALUES (26, N'Income', N'UpdateIncome', N'', N'', N'Actualiza ingreso', N'Actualiza la información del ingreso', N'1', 1, 33, 1, 1, 1, 1)
GO
INSERT [vta].[tblpermissions] ([idPermission], [permissionController], [permissionAction], [permissionArea], [permissionImageClass], [permissionTitle], [permissionDescription], [permissisonActiveli], [permissionEstatus], [permissionParentId], [permissionIsParent], [permissionHasChild], [PermissionMenu], [permissionIsHtml]) VALUES (27, N'Income', N'DeleteIncome', N'', N'', N'Eliminar ingreso', N'Elimina el ingreso de la base de datos', N'1', 1, 33, 1, 1, 1, 1)
GO
INSERT [vta].[tblpermissions] ([idPermission], [permissionController], [permissionAction], [permissionArea], [permissionImageClass], [permissionTitle], [permissionDescription], [permissisonActiveli], [permissionEstatus], [permissionParentId], [permissionIsParent], [permissionHasChild], [PermissionMenu], [permissionIsHtml]) VALUES (29, N'Budget', N'Index', N'', N'', N'Movimientos', N'Vista de movimientos', N'1', 1, 34, 1, 1, 1, 1)
GO
INSERT [vta].[tblpermissions] ([idPermission], [permissionController], [permissionAction], [permissionArea], [permissionImageClass], [permissionTitle], [permissionDescription], [permissisonActiveli], [permissionEstatus], [permissionParentId], [permissionIsParent], [permissionHasChild], [PermissionMenu], [permissionIsHtml]) VALUES (30, N'Budget', N'Budgetautorization', N'', N'', N'Límites de cuentas', N'Vista limite de cuantas', N'1', 1, 34, 1, 1, 1, 1)
GO
INSERT [vta].[tblpermissions] ([idPermission], [permissionController], [permissionAction], [permissionArea], [permissionImageClass], [permissionTitle], [permissionDescription], [permissisonActiveli], [permissionEstatus], [permissionParentId], [permissionIsParent], [permissionHasChild], [PermissionMenu], [permissionIsHtml]) VALUES (31, N'Budget', N'Finance', N'', N'', N'Financiamiento', N'Vista de financiamiento', N'1', 1, 34, 1, 1, 1, 1)
GO
INSERT [vta].[tblpermissions] ([idPermission], [permissionController], [permissionAction], [permissionArea], [permissionImageClass], [permissionTitle], [permissionDescription], [permissisonActiveli], [permissionEstatus], [permissionParentId], [permissionIsParent], [permissionHasChild], [PermissionMenu], [permissionIsHtml]) VALUES (32, N'#', N'#', N'', N'', N'Gastos', N'Menu principal de gastos', N'1', 1, 0, 1, 1, 1, 1)
GO
INSERT [vta].[tblpermissions] ([idPermission], [permissionController], [permissionAction], [permissionArea], [permissionImageClass], [permissionTitle], [permissionDescription], [permissisonActiveli], [permissionEstatus], [permissionParentId], [permissionIsParent], [permissionHasChild], [PermissionMenu], [permissionIsHtml]) VALUES (33, N'#', N'#', N'', N'', N'Ingresos', N'Manu principal de ingresos', N'1', 1, 0, 1, 1, 1, 1)
GO
INSERT [vta].[tblpermissions] ([idPermission], [permissionController], [permissionAction], [permissionArea], [permissionImageClass], [permissionTitle], [permissionDescription], [permissisonActiveli], [permissionEstatus], [permissionParentId], [permissionIsParent], [permissionHasChild], [PermissionMenu], [permissionIsHtml]) VALUES (34, N'#', N'#', N'', N'', N'Tesorería', N'Menu principal de tesorería', N'1', 1, 0, 1, 1, 1, 1)
GO
INSERT [vta].[tblpermissions] ([idPermission], [permissionController], [permissionAction], [permissionArea], [permissionImageClass], [permissionTitle], [permissionDescription], [permissisonActiveli], [permissionEstatus], [permissionParentId], [permissionIsParent], [permissionHasChild], [PermissionMenu], [permissionIsHtml]) VALUES (35, N'#', N'#', N'', N'', N'Conciliaciones', N'Menu principal de conciliaciones', N'1', 1, 0, 1, 1, 1, 1)
GO
INSERT [vta].[tblpermissions] ([idPermission], [permissionController], [permissionAction], [permissionArea], [permissionImageClass], [permissionTitle], [permissionDescription], [permissisonActiveli], [permissionEstatus], [permissionParentId], [permissionIsParent], [permissionHasChild], [PermissionMenu], [permissionIsHtml]) VALUES (36, N'#', N'#', N'', N'', N'Reportes', N'Menu principal de reportes', N'1', 1, 0, 1, 1, 1, 1)
GO
INSERT [vta].[tblpermissions] ([idPermission], [permissionController], [permissionAction], [permissionArea], [permissionImageClass], [permissionTitle], [permissionDescription], [permissisonActiveli], [permissionEstatus], [permissionParentId], [permissionIsParent], [permissionHasChild], [PermissionMenu], [permissionIsHtml]) VALUES (37, N'#', N'#', N'', N'', N'Usuarios', N'Menu principal de usuario', N'1', 1, 0, 1, 1, 1, 1)
GO
INSERT [vta].[tblpermissions] ([idPermission], [permissionController], [permissionAction], [permissionArea], [permissionImageClass], [permissionTitle], [permissionDescription], [permissisonActiveli], [permissionEstatus], [permissionParentId], [permissionIsParent], [permissionHasChild], [PermissionMenu], [permissionIsHtml]) VALUES (38,'Bankreconciliation','baccountstatements','','[V] - Conciliaciones movimientos bancarios','Conciliaciones movimientos bancarios','Vista para conciliar movimientos bancarios',N'1',1,35,1,1,1,1)
GO
INSERT [vta].[tblpermissions] ([idPermission], [permissionController], [permissionAction], [permissionArea], [permissionImageClass], [permissionTitle], [permissionDescription], [permissisonActiveli], [permissionEstatus], [permissionParentId], [permissionIsParent], [permissionHasChild], [PermissionMenu], [permissionIsHtml]) VALUES (39,'Reports','monthlyexpenses','','[V] - Reporte gastos mensuales','Reporte gastos mensuales','Vista reporte gastos mensuales',N'1',1,36,1,1,1,1)
GO
INSERT [vta].[tblpermissions] ([idPermission], [permissionController], [permissionAction], [permissionArea], [permissionImageClass], [permissionTitle], [permissionDescription], [permissisonActiveli], [permissionEstatus], [permissionParentId], [permissionIsParent], [permissionHasChild], [PermissionMenu], [permissionIsHtml]) VALUES (40,'Reports','monthlyexpensesdetails','','[V] - Reporte gastos mensuales detallado','Reporte gastos mensuales detallado','Vista reporte gastos mensuales detallado',N'1',1,36,1,1,1,1)
GO
INSERT [vta].[tblpermissions] ([idPermission], [permissionController], [permissionAction], [permissionArea], [permissionImageClass], [permissionTitle], [permissionDescription], [permissisonActiveli], [permissionEstatus], [permissionParentId], [permissionIsParent], [permissionHasChild], [PermissionMenu], [permissionIsHtml]) VALUES (41,'Reports','reportexpenseconcentrated','','[V] - Reporte concentrado gastos mensuales','Reporte concentrado gastos mensuales','Vista reporte concentrado gastos mensuales',N'1',1,36,1,1,1,1)
GO
INSERT [vta].[tblpermissions] ([idPermission], [permissionController], [permissionAction], [permissionArea], [permissionImageClass], [permissionTitle], [permissionDescription], [permissisonActiveli], [permissionEstatus], [permissionParentId], [permissionIsParent], [permissionHasChild], [PermissionMenu], [permissionIsHtml]) VALUES (42,'Reports','cashclosings','','[V] - Reporte corte de cajas','Reporte corte de cajas','Vista reporte corte de cajas',N'1',1,36,1,1,1,1)
GO
INSERT [vta].[tblpermissions] ([idPermission], [permissionController], [permissionAction], [permissionArea], [permissionImageClass], [permissionTitle], [permissionDescription], [permissisonActiveli], [permissionEstatus], [permissionParentId], [permissionIsParent], [permissionHasChild], [PermissionMenu], [permissionIsHtml]) VALUES (43,'Reports','bankaccountcash','','[V] - Reporte saldo en banco','Reporte saldo en banco','Vista reporte saldo en banco',N'1',1,36,1,1,1,1)
GO
INSERT [vta].[tblpermissions] ([idPermission], [permissionController], [permissionAction], [permissionArea], [permissionImageClass], [permissionTitle], [permissionDescription], [permissisonActiveli], [permissionEstatus], [permissionParentId], [permissionIsParent], [permissionHasChild], [PermissionMenu], [permissionIsHtml]) VALUES (44,'Reports','accountsclosingreconciliations','','[V] - Reporte conciliaciones','Reporte de conciliaciones','Vista reporte de conciliaciones',N'1',1,36,1,1,1,1)
GO
SET IDENTITY_INSERT [vta].[tblpermissions] OFF
GO

SET IDENTITY_INSERT [vta].[tblusercompanies] ON 
GO
INSERT [vta].[tblusercompanies] ([idusercompany], [idcompany], [iduser], [usercompanyuserlastchange], [usercompanydatelastchange], [usercompanyactive]) VALUES (3, 3, 3, 3, CAST(N'2019-01-01T00:00:00.000' AS DateTime), 1)
GO
INSERT [vta].[tblusercompanies] ([idusercompany], [idcompany], [iduser], [usercompanyuserlastchange], [usercompanydatelastchange], [usercompanyactive]) VALUES (6, 3, 1, 1, CAST(N'2019-10-15T10:21:33.147' AS DateTime), 1)
GO
INSERT [vta].[tblusercompanies] ([idusercompany], [idcompany], [iduser], [usercompanyuserlastchange], [usercompanydatelastchange], [usercompanyactive]) VALUES (7, 10, 1, 1, CAST(N'2019-10-15T10:21:33.153' AS DateTime), 1)
GO
INSERT [vta].[tblusercompanies] ([idusercompany], [idcompany], [iduser], [usercompanyuserlastchange], [usercompanydatelastchange], [usercompanyactive]) VALUES (8, 11, 1, 1, CAST(N'2019-10-15T10:21:33.227' AS DateTime), 1)
GO
INSERT [vta].[tblusercompanies] ([idusercompany], [idcompany], [iduser], [usercompanyuserlastchange], [usercompanydatelastchange], [usercompanyactive]) VALUES (9, 12, 1, 1, CAST(N'2019-10-15T10:21:33.273' AS DateTime), 1)
GO
INSERT [vta].[tblusercompanies] ([idusercompany], [idcompany], [iduser], [usercompanyuserlastchange], [usercompanydatelastchange], [usercompanyactive]) VALUES (10, 13, 1, 1, CAST(N'2019-10-15T10:21:33.280' AS DateTime), 1)
GO
INSERT [vta].[tblusercompanies] ([idusercompany], [idcompany], [iduser], [usercompanyuserlastchange], [usercompanydatelastchange], [usercompanyactive]) VALUES (11, 14, 1, 1, CAST(N'2019-10-15T10:21:33.287' AS DateTime), 1)
GO
INSERT [vta].[tblusercompanies] ([idusercompany], [idcompany], [iduser], [usercompanyuserlastchange], [usercompanydatelastchange], [usercompanyactive]) VALUES (12, 15, 1, 1, CAST(N'2019-10-15T10:21:33.293' AS DateTime), 1)
GO
INSERT [vta].[tblusercompanies] ([idusercompany], [idcompany], [iduser], [usercompanyuserlastchange], [usercompanydatelastchange], [usercompanyactive]) VALUES (13, 16, 1, 1, CAST(N'2019-10-15T10:21:33.320' AS DateTime), 1)
GO
INSERT [vta].[tblusercompanies] ([idusercompany], [idcompany], [iduser], [usercompanyuserlastchange], [usercompanydatelastchange], [usercompanyactive]) VALUES (14, 17, 1, 1, CAST(N'2019-10-15T10:21:33.323' AS DateTime), 1)
GO
INSERT [vta].[tblusercompanies] ([idusercompany], [idcompany], [iduser], [usercompanyuserlastchange], [usercompanydatelastchange], [usercompanyactive]) VALUES (15, 18, 1, 1, CAST(N'2019-10-15T10:21:33.333' AS DateTime), 1)
GO
INSERT [vta].[tblusercompanies] ([idusercompany], [idcompany], [iduser], [usercompanyuserlastchange], [usercompanydatelastchange], [usercompanyactive]) VALUES (16, 19, 1, 1, CAST(N'2019-10-15T10:21:33.337' AS DateTime), 1)
GO
INSERT [vta].[tblusercompanies] ([idusercompany], [idcompany], [iduser], [usercompanyuserlastchange], [usercompanydatelastchange], [usercompanyactive]) VALUES (17, 20, 1, 1, CAST(N'2019-10-15T10:21:33.363' AS DateTime), 1)
GO
SET IDENTITY_INSERT [vta].[tblusercompanies] OFF
GO
SET IDENTITY_INSERT [vta].[tbluserpermissions] ON 
GO
INSERT [vta].[tbluserpermissions] ([IdUserPermission], [idUser], [idPermission], [userpermissionActive]) VALUES (1, 1, 1, 1)
GO
INSERT [vta].[tbluserpermissions] ([IdUserPermission], [idUser], [idPermission], [userpermissionActive]) VALUES (2, 1, 2, 1)
GO
INSERT [vta].[tbluserpermissions] ([IdUserPermission], [idUser], [idPermission], [userpermissionActive]) VALUES (3, 1, 3, 1)
GO
INSERT [vta].[tbluserpermissions] ([IdUserPermission], [idUser], [idPermission], [userpermissionActive]) VALUES (4, 2, 1, 1)
GO
INSERT [vta].[tbluserpermissions] ([IdUserPermission], [idUser], [idPermission], [userpermissionActive]) VALUES (5, 2, 2, 1)
GO
INSERT [vta].[tbluserpermissions] ([IdUserPermission], [idUser], [idPermission], [userpermissionActive]) VALUES (6, 2, 3, 1)
GO
INSERT [vta].[tbluserpermissions] ([IdUserPermission], [idUser], [idPermission], [userpermissionActive]) VALUES (7, 3, 1, 1)
GO
INSERT [vta].[tbluserpermissions] ([IdUserPermission], [idUser], [idPermission], [userpermissionActive]) VALUES (8, 3, 2, 1)
GO
INSERT [vta].[tbluserpermissions] ([IdUserPermission], [idUser], [idPermission], [userpermissionActive]) VALUES (9, 3, 3, 1)
GO
INSERT [vta].[tbluserpermissions] ([IdUserPermission], [idUser], [idPermission], [userpermissionActive]) VALUES (10, 1, 4, 1)
GO
INSERT [vta].[tbluserpermissions] ([IdUserPermission], [idUser], [idPermission], [userpermissionActive]) VALUES (12, 1, 6, 1)
GO
INSERT [vta].[tbluserpermissions] ([IdUserPermission], [idUser], [idPermission], [userpermissionActive]) VALUES (13, 1, 18, 1)
GO
INSERT [vta].[tbluserpermissions] ([IdUserPermission], [idUser], [idPermission], [userpermissionActive]) VALUES (14, 1, 19, 1)
GO
INSERT [vta].[tbluserpermissions] ([IdUserPermission], [idUser], [idPermission], [userpermissionActive]) VALUES (18, 1, 21, 1)
GO
INSERT [vta].[tbluserpermissions] ([IdUserPermission], [idUser], [idPermission], [userpermissionActive]) VALUES (19, 1, 7, 1)
GO
INSERT [vta].[tbluserpermissions] ([IdUserPermission], [idUser], [idPermission], [userpermissionActive]) VALUES (20, 1, 23, 1)
GO
INSERT [vta].[tbluserpermissions] ([IdUserPermission], [idUser], [idPermission], [userpermissionActive]) VALUES (21, 1, 25, 1)
GO
INSERT [vta].[tbluserpermissions] ([IdUserPermission], [idUser], [idPermission], [userpermissionActive]) VALUES (23, 1, 22, 1)
GO
INSERT [vta].[tbluserpermissions] ([IdUserPermission], [idUser], [idPermission], [userpermissionActive]) VALUES (25, 1, 32, 1)
GO
INSERT [vta].[tbluserpermissions] ([IdUserPermission], [idUser], [idPermission], [userpermissionActive]) VALUES (26, 1, 10, 1)
GO
INSERT [vta].[tbluserpermissions] ([IdUserPermission], [idUser], [idPermission], [userpermissionActive]) VALUES (27, 1, 13, 1)
GO
INSERT [vta].[tbluserpermissions] ([IdUserPermission], [idUser], [idPermission], [userpermissionActive]) VALUES (28, 1, 15, 1)
GO
INSERT [vta].[tbluserpermissions] ([IdUserPermission], [idUser], [idPermission], [userpermissionActive]) VALUES (29, 1, 16, 1)
GO
INSERT [vta].[tbluserpermissions] ([IdUserPermission], [idUser], [idPermission], [userpermissionActive]) VALUES (30, 1, 17, 1)
GO
INSERT [vta].[tbluserpermissions] ([IdUserPermission], [idUser], [idPermission], [userpermissionActive]) VALUES (31, 1, 33, 1)
GO
INSERT [vta].[tbluserpermissions] ([IdUserPermission], [idUser], [idPermission], [userpermissionActive]) VALUES (32, 1, 26, 1)
GO
INSERT [vta].[tbluserpermissions] ([IdUserPermission], [idUser], [idPermission], [userpermissionActive]) VALUES (33, 1, 27, 1)
GO
INSERT [vta].[tbluserpermissions] ([IdUserPermission], [idUser], [idPermission], [userpermissionActive]) VALUES (34, 1, 34, 1)
GO
INSERT [vta].[tbluserpermissions] ([IdUserPermission], [idUser], [idPermission], [userpermissionActive]) VALUES (35, 1, 29, 1)
GO
INSERT [vta].[tbluserpermissions] ([IdUserPermission], [idUser], [idPermission], [userpermissionActive]) VALUES (36, 1, 30, 1)
GO
INSERT [vta].[tbluserpermissions] ([IdUserPermission], [idUser], [idPermission], [userpermissionActive]) VALUES (37, 1, 31, 1)
GO
INSERT [vta].[tbluserpermissions] ([IdUserPermission], [idUser], [idPermission], [userpermissionActive]) VALUES (38, 1, 37, 1)
GO
SET IDENTITY_INSERT [vta].[tbluserpermissions] OFF
GO


ALTER TABLE [vta].[tblsourcedata] ADD sourcedatadescription NVARCHAR(MAX) NULL



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vta].[tblincomemovementLog](
	[LogId] [bigint] IDENTITY(1,1) NOT NULL,
	[LogDate] [datetime] NULL,
	[LogUser] [int] NULL,
	[LogObs] [nvarchar](max) NULL,
	[idincomeMovement] [bigint] NOT NULL,
	[idincome] [bigint] NOT NULL,
	[idbaccount] [int] NOT NULL,
	[idBankAccntType] [int] NOT NULL,
	[idtpv] [int] NULL,
	[incomemovcard] [nvarchar](50) NULL,
	[incomemovapplicationdate] [datetime] NOT NULL,
	[incomemovchargedamount] [numeric](14, 2) NOT NULL,
	[incomemovauthref] [nvarchar](250) NULL,
	[incomemovcreationdate] [datetime] NOT NULL,
	[incomemovcreatedby] [int] NOT NULL,
	[incomemovupdatedon] [datetime] NOT NULL,
	[incomemovupdatedby] [int] NOT NULL,
	[incomemovcanceled] [bit] NOT NULL,
	[incomemovdeleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


ALTER TABLE [vta].[tblpayment]  WITH CHECK ADD  CONSTRAINT [FK_VTA_tblpayment_tblbankaccount_1] FOREIGN KEY([idbaccount])
REFERENCES [vta].[tblbankaccount] ([idbaccount])
GO
ALTER TABLE [vta].[tblpayment] CHECK CONSTRAINT [FK_VTA_tblpayment_tblbankaccount_1]
GO
ALTER TABLE [vta].[tblpayment]  WITH CHECK ADD  CONSTRAINT [FK_VTA_tblpayment_idBankAccontProdType_1] FOREIGN KEY([idbankprodttype])
REFERENCES [vta].[tblbankprodttype] ([idbankprodttype])
GO
ALTER TABLE [vta].[tblpayment] CHECK CONSTRAINT [FK_VTA_tblpayment_idBankAccontProdType_1]
GO
ALTER TABLE [vta].[tblpayment]  WITH CHECK ADD  CONSTRAINT [FK_VTA_tblpayment_tblinvoice_1] FOREIGN KEY([idinvoice])
REFERENCES [vta].[tblinvoice] ([idinvoice])
GO
ALTER TABLE [vta].[tblpayment] CHECK CONSTRAINT [FK_VTA_tblpayment_tblinvoice_1]
GO



ALTER TABLE [vta].[tblinvoice]  WITH CHECK ADD  CONSTRAINT [FK_tblinvoice_tblusers] FOREIGN KEY([invoicecreatedby])
REFERENCES [dbo].[tblusers] ([iduser])
GO
ALTER TABLE [vta].[tblinvoice] CHECK CONSTRAINT [FK_tblinvoice_tblusers]
GO
ALTER TABLE [vta].[tblinvoice]  WITH CHECK ADD  CONSTRAINT [FK_tblinvoice_tblusers2] FOREIGN KEY([invoiceupdatedby])
REFERENCES [dbo].[tblusers] ([iduser])
GO
ALTER TABLE [vta].[tblinvoice] CHECK CONSTRAINT [FK_tblinvoice_tblusers2]
GO
ALTER TABLE [vta].[tblinvoice]  WITH CHECK ADD  CONSTRAINT [FK_VTA_tblinvoice_tblcurrency_1] FOREIGN KEY([idcurrency])
REFERENCES [dbo].[tblcurrencies] ([idcurrency])
GO
ALTER TABLE [vta].[tblinvoice] CHECK CONSTRAINT [FK_VTA_tblinvoice_tblcurrency_1]
GO
ALTER TABLE [vta].[tblinvoice]  WITH CHECK ADD  CONSTRAINT [FK_VTA_tblinvoice_tblcompanies_1] FOREIGN KEY([idcompany])
REFERENCES [vta].[tblcompanies] ([idcompany])
GO
ALTER TABLE [vta].[tblinvoice] CHECK CONSTRAINT [FK_VTA_tblinvoice_tblcompanies_1]
GO


ALTER TABLE [vta].[tblinvoiceditem]  WITH CHECK ADD  CONSTRAINT [FK_tblinvoiceditem_tblbugettype] FOREIGN KEY([idbudgettype])
REFERENCES [vta].[tblbugettype] ([idbudgettype]) 
GO
ALTER TABLE [vta].[tblinvoiceditem] CHECK CONSTRAINT [FK_tblinvoiceditem_tblbugettype]
GO
ALTER TABLE [vta].[tblinvoiceditem]  WITH CHECK ADD  CONSTRAINT [FK_tblinvoiceitem_tblsupplier_1] FOREIGN KEY([idsupplier])
REFERENCES [dbo].[tblSuppliers] ([idSupplier])
GO
ALTER TABLE [vta].[tblinvoiceditem] CHECK CONSTRAINT [FK_tblinvoiceitem_tblsupplier_1]
GO
ALTER TABLE [vta].[tblinvoiceditem]  WITH CHECK ADD  CONSTRAINT [FK_VTA_tblinvoiceditem_tblaccountsl4_1] FOREIGN KEY([idaccountl4])
REFERENCES [vta].[tblaccountsl4] ([idAccountl4])
GO
ALTER TABLE [vta].[tblinvoiceditem] CHECK CONSTRAINT [FK_VTA_tblinvoiceditem_tblaccountsl4_1]
GO
ALTER TABLE [vta].[tblinvoiceditem] WITH CHECK ADD  CONSTRAINT [FK_VTA_tblinvoiceditem_tblinvoice_1] FOREIGN KEY([idinvoice])
REFERENCES [vta].[tblinvoice] ([idinvoice])
GO
ALTER TABLE [vta].[tblinvoiceditem] CHECK CONSTRAINT [FK_VTA_tblinvoiceditem_tblinvoice_1]
GO
ALTER TABLE [vta].[tblinvoiceditem]  WITH CHECK ADD  CONSTRAINT [FK_VTA_tblinvoiceditem_tblinvoiceitemstatus_1] FOREIGN KEY([idinvoiceitemstatus])
REFERENCES [vta].[tblinvoiceitemstatus] ([idinvoiceitemstatus])
GO
ALTER TABLE [vta].[tblinvoiceditem] CHECK CONSTRAINT [FK_VTA_tblinvoiceditem_tblinvoiceitemstatus_1]
GO
ALTER TABLE [vta].[tblinvoiceditem]  WITH CHECK ADD  CONSTRAINT [FK_VTA_tblinvoiceditem_tblusers_1] FOREIGN KEY([iduser])
REFERENCES [dbo].[tblusers] ([idUser])
GO
ALTER TABLE [vta].[tblinvoiceditem] CHECK CONSTRAINT [FK_VTA_tblinvoiceditem_tblusers_1]
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [vta].[tblerroraction](
	[idErrorAction] [int] IDENTITY(1,1) NOT NULL,
	[errorActionName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_tblerroraction] PRIMARY KEY CLUSTERED 
(
	[idErrorAction] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [vta].[tblerrorseverity](
	[idErrorSeverity] [int] IDENTITY(1,1) NOT NULL,
	[errorSeverityName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_tblerrorseverity] PRIMARY KEY CLUSTERED 
(
	[idErrorSeverity] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


SET IDENTITY_INSERT [vta].[tblerroraction] ON
GO
INSERT INTO [vta].[tblerroraction]([idErrorAction],[errorActionName]) VALUES(1,'Ignored')
GO
INSERT INTO [vta].[tblerroraction]([idErrorAction],[errorActionName]) VALUES(2,'Fixed')
GO
INSERT INTO [vta].[tblerroraction]([idErrorAction],[errorActionName]) VALUES(3,'Skipped')
GO
INSERT INTO [vta].[tblerroraction]([idErrorAction],[errorActionName]) VALUES(4,'Halted')
GO
SET IDENTITY_INSERT [vta].[tblerroraction] OFF
GO
SET IDENTITY_INSERT [vta].[tblerrorseverity] ON
GO
INSERT INTO [vta].[tblerrorseverity]([idErrorSeverity],[errorSeverityName]) VALUES(1,'Information')
GO
INSERT INTO [vta].[tblerrorseverity]([idErrorSeverity],[errorSeverityName]) VALUES(2,'Warning')
GO
INSERT INTO [vta].[tblerrorseverity]([idErrorSeverity],[errorSeverityName]) VALUES(3,'Critical')
GO
SET IDENTITY_INSERT [vta].[tblerrorseverity] OFF



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [vta].[tblbstaterrors](
	[idBStatError] [int] IDENTITY(1,1) NOT NULL,
	[bStatErrorOperUuid] [nvarchar](50) NOT NULL,
	[bStatErrorDatetime] [datetime] NOT NULL,
	[bStatErrorConsec] [int] NOT NULL,
	[bStatErrorTotal] [int] NOT NULL,
	[bStatErrorMessage] [nvarchar](250) NOT NULL,
	[bStatErrorDetails] [text] NULL,
	[idErrorSeverity] [int] NOT NULL,
	[idErrorAction] [int] NOT NULL,
	[bStatErrorHasExcep] [bit] NOT NULL,
	[bStatErrorExcepMsg] [nvarchar](250) NULL,
	[bStatErrorExcepStack] [text] NULL,
	[idUser] [int] NOT NULL,
 CONSTRAINT [PK_tblbstaterrors] PRIMARY KEY CLUSTERED 
(
	[idBStatError] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [vta].[tblbstaterrors] ADD  CONSTRAINT [DF_Table_1_bStatErrorHasException]  DEFAULT ((0)) FOR [bStatErrorHasExcep]
GO

ALTER TABLE [vta].[tblbstaterrors]  WITH CHECK ADD  CONSTRAINT [FK_tblbstaterrors_tblerroraction] FOREIGN KEY([idErrorAction])
REFERENCES [vta].[tblerroraction] ([idErrorAction])
GO

ALTER TABLE [vta].[tblbstaterrors] CHECK CONSTRAINT [FK_tblbstaterrors_tblerroraction]
GO

ALTER TABLE [vta].[tblbstaterrors]  WITH CHECK ADD  CONSTRAINT [FK_tblbstaterrors_tblerrorseverity] FOREIGN KEY([idErrorSeverity])
REFERENCES [vta].[tblerrorseverity] ([idErrorSeverity])
GO

ALTER TABLE [vta].[tblbstaterrors] CHECK CONSTRAINT [FK_tblbstaterrors_tblerrorseverity]
GO

ALTER TABLE [vta].[tblbstaterrors]  WITH CHECK ADD  CONSTRAINT [FK_tblbstaterrors_tblusers] FOREIGN KEY([idUser])
REFERENCES [dbo].[tblusers] ([idUser])
GO

ALTER TABLE [vta].[tblbstaterrors] CHECK CONSTRAINT [FK_tblbstaterrors_tblusers]
GO


/** Tabla tbloperationtype **/
CREATE TABLE [vta].[tbloperationtype]
	(
		[idOperationType] int NOT NULL IDENTITY (1, 1),
		[operationTypeName] nvarchar(100) NOT NULL
	) ON [PRIMARY];

ALTER TABLE [vta].[tbloperationtype] ADD CONSTRAINT PK_tbloperationtype PRIMARY KEY CLUSTERED ([idOperationType]) WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];
GO

SET IDENTITY_INSERT [vta].[tbloperationtype] ON

INSERT INTO [vta].[tbloperationtype] ([idOperationType], [operationTypeName])
	VALUES
	(1, 'Entradas'),
	(2, 'Salidas'),
	(3, 'Ambos sentidos');

SET IDENTITY_INSERT [vta].[tbloperationtype] OFF
------------------------------------------------

/** Tabla tblmovementtypes **/
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[vta].[fk_tblbankstatements2_tblmovementtype_1]') AND parent_object_id = OBJECT_ID(N'[vta].[tblbankstatements2]') )
ALTER TABLE [vta].[tblbankstatements2] DROP CONSTRAINT fk_tblbankstatements2_tblmovementtype_1;
TRUNCATE TABLE [vta].[tblmovementtype];
GO

ALTER TABLE [vta].[tblmovementtype] ADD
	[idOperationType] int NOT NULL;
GO

ALTER TABLE [vta].[tblbankstatements2] ADD CONSTRAINT FK_tblbankstatements2_tblmovementtype_1 FOREIGN KEY ([idMovementType]) REFERENCES [vta].[tblmovementtype] ([idMovementType])
GO

SET IDENTITY_INSERT [vta].[tblmovementtype] ON

INSERT INTO [vta].[tblmovementtype] ([idMovementType], [movementTypeName], [movementTypeActive], [idOperationType])
	VALUES
	(1, 'Fondeo', 1, 3),
	(2, 'Transferencia saliente', 1, 2),
	(3, 'Transferencia entrante', 1, 1),
	(4, 'Depósito', 1, 1),
	(5, 'Retiro', 1, 2),
	(6, 'Pago', 1, 2),
	(7, 'Comisiones', 1, 2),
	(8, 'Domiciliación', 1, 2),
	(9, 'Rendimientos', 1, 1),
	(10, 'Cheque cobrado', 1, 2),
	(11, 'Compra', 1, 2),
	(12, 'Cuota', 1, 2),
	(13, 'Traspasos', 1, 3),
	(14, 'Otros', 1, 3);

SET IDENTITY_INSERT [vta].[tblmovementtype] OFF;
GO
-------------------------------------------------


/** Tabla tblbankstatements2 **/

ALTER TABLE [vta].[tblbankstatements2] ADD
	[bankstatements2Reconciled] bit NOT NULL CONSTRAINT DF_tblbankstatements2_bankstatements2Conciled DEFAULT 0;
GO

ALTER TABLE [vta].[tblbankstatements2] ADD
	[idBankStatementMethod] int NOT NULL;
GO
------------------------------------------------

/** Tabla tblbankstat2fondos **/
CREATE TABLE [vta].[tblbankstat2fondo]
	(
		[idBankStat2Fondo] bigint NOT NULL IDENTITY (1, 1),
		[idFondos] int NOT NULL,
		[idBankStatements2In] bigint NULL,
		[idBankStatements2Out] bigint NULL
	) ON [PRIMARY];

ALTER TABLE [vta].[tblbankstat2fondo] ADD CONSTRAINT PK_tblbankstat2fondo PRIMARY KEY CLUSTERED ([idBankStat2Fondo]) WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];
GO
------------------------------------------------

/** Tabla tblbankstat2incomes **/
CREATE TABLE [vta].[tblbankstat2income]
	(
		[idBankStat2Income] bigint NOT NULL IDENTITY (1, 1),
		[idincomeMovement] bigint NOT NULL,
		[idBankStatements2] bigint NOT NULL
	) ON [PRIMARY];

ALTER TABLE [vta].[tblbankstat2income] ADD CONSTRAINT PK_tblbankstat2income PRIMARY KEY CLUSTERED ([idBankStat2Income]) WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];
GO
------------------------------------------------

/** Tabla tblbankstat2invoices **/
CREATE TABLE [vta].[tblbankstat2invoice]
	(
		[idBankStat2Invoice] bigint NOT NULL IDENTITY (1, 1),
		[idpayment] bigint NOT NULL,
		[idBankStatements2] bigint NOT NULL
	) ON [PRIMARY];

ALTER TABLE [vta].[tblbankstat2invoice] ADD CONSTRAINT PK_tblbankstat2invoice PRIMARY KEY CLUSTERED ([idBankStat2Invoice]) WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];
GO
------------------------------------------------

/** Tabla tblbankstat2purchase **/
CREATE TABLE [vta].[tblbankstat2purchase]
	(
		[idBankStat2Purchase] bigint NOT NULL IDENTITY (1, 1),
		[idPaymentPurchase] int NOT NULL,
		[idBankStatements2] bigint NOT NULL
	) ON [PRIMARY];

ALTER TABLE [vta].[tblbankstat2purchase] ADD CONSTRAINT PK_tblbankstat2purchase PRIMARY KEY CLUSTERED ([idBankStat2Purchase]) WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];
GO
------------------------------------------------

/** Tabla tblbankstat2reserv **/
CREATE TABLE [vta].[tblbankstat2reserv]
	(
		[idBankStat2Reserv] bigint NOT NULL IDENTITY (1, 1),
		[idReservationPayment] int NOT NULL,
		[idBankStatements2] bigint NOT NULL
	) ON [PRIMARY];

ALTER TABLE [vta].[tblbankstat2reserv] ADD CONSTRAINT PK_tblbankstat2reserv PRIMARY KEY CLUSTERED ([idBankStat2Reserv]) WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];
GO
------------------------------------------------


/** Tabla tblbankstat2parentreserv **/
CREATE TABLE [vta].[tblbankstat2parentreserv]
	(
		[idBankStat2ParentReserv] bigint NOT NULL IDENTITY (1, 1),
		[idReservationParentPayment] int NOT NULL,
		[idBankStatements2] bigint NOT NULL
	) ON [PRIMARY];

ALTER TABLE [vta].[tblbankstat2parentreserv] ADD CONSTRAINT PK_tblbankstat2parentreserv PRIMARY KEY CLUSTERED ([idBankStat2ParentReserv]) WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];
GO
------------------------------------------------

ALTER TABLE vta.[tblbankstatements2] ADD CONSTRAINT
	FK_tblbankstatements2_tblbankstatementmethod FOREIGN KEY
	(
	idBankStatementMethod
	) REFERENCES vta.tblbankstatementmethod
	(
	idBankStatementMethod
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

ALTER TABLE vta.tblmovementtype ADD CONSTRAINT
	FK_tblmovementtype_tbloperationtype FOREIGN KEY
	(
	idOperationType
	) REFERENCES vta.tbloperationtype
	(
	idOperationType
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

ALTER TABLE vta.tblbankstat2purchase ADD CONSTRAINT
	FK_tblbankstat2purchase_tblpaymentspurchases FOREIGN KEY
	(
	idPaymentPurchase
	) REFERENCES dbo.tblpaymentspurchases
	(
	idPaymentPurchase
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE vta.tblbankstat2purchase ADD CONSTRAINT
	FK_tblbankstat2purchase_tblbankstatements2 FOREIGN KEY
	(
	idBankStatements2
	) REFERENCES vta.tblbankstatements2
	(
	idBankStatements2
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE vta.tblbankstat2reserv ADD CONSTRAINT
	FK_tblbankstat2reserv_tblreservationspayment FOREIGN KEY
	(
	idReservationPayment
	) REFERENCES dbo.tblreservationspayment
	(
	idReservationPayment
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE vta.tblbankstat2reserv ADD CONSTRAINT
	FK_tblbankstat2reserv_tblbankstatements2 FOREIGN KEY
	(
	idBankStatements2
	) REFERENCES vta.tblbankstatements2
	(
	idBankStatements2
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

ALTER TABLE vta.tblbankstat2parentreserv ADD CONSTRAINT
	FK_tblbankstat2parentreserv_tblreservationsparentpayment FOREIGN KEY
	(
	idReservationParentPayment
	) REFERENCES dbo.tblreservationsparentpayment
	(
	idReservationParentPayment
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE vta.tblbankstat2parentreserv ADD CONSTRAINT
	FK_tblbankstat2parentreserv_tblbankstatements2 FOREIGN KEY
	(
	idBankStatements2
	) REFERENCES vta.tblbankstatements2
	(
	idBankStatements2
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

ALTER TABLE vta.tblbankstat2invoice ADD CONSTRAINT
	FK_tblbankstat2invoice_tblpayment FOREIGN KEY
	(
	idpayment
	) REFERENCES vta.tblpayment
	(
	idpayment
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE vta.tblbankstat2invoice ADD CONSTRAINT
	FK_tblbankstat2invoice_tblbankstatements2 FOREIGN KEY
	(
	idBankStatements2
	) REFERENCES vta.tblbankstatements2
	(
	idBankStatements2
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

ALTER TABLE vta.tblbankstat2income ADD CONSTRAINT
	FK_tblbankstat2income_tblincomemovement FOREIGN KEY
	(
	idincomeMovement
	) REFERENCES vta.tblincomemovement
	(
	idincomeMovement
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

ALTER TABLE vta.tblbankstat2income ADD CONSTRAINT
	FK_tblbankstat2income_tblbankstatements2 FOREIGN KEY
	(
	idBankStatements2
	) REFERENCES vta.tblbankstatements2
	(
	idBankStatements2
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

ALTER TABLE vta.tblbankstat2fondo ADD CONSTRAINT
	FK_tblbankstat2fondo_tblfondos FOREIGN KEY
	(
	idFondos
	) REFERENCES vta.tblfondos
	(
	idFondos
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE vta.tblbankstat2fondo ADD CONSTRAINT
	FK_tblbankstat2fondo_tblbankstatements2_1 FOREIGN KEY
	(
	idBankStatements2In
	) REFERENCES vta.tblbankstatements2
	(
	idBankStatements2
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

ALTER TABLE vta.tblbankstat2fondo ADD CONSTRAINT
	FK_tblbankstat2fondo_tblbankstatements2_2 FOREIGN KEY
	(
	idBankStatements2Out
	) REFERENCES vta.tblbankstatements2
	(
	idBankStatements2
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO



ALTER TABLE [vta].[tblsourcedata] ADD [sourcedatadescription] nvarchar(500) NULL
GO

GO
UPDATE [vta].[tblsourcedata] SET [sourcedatadescription] = 'INGRESOS\CONCEPTOS. Movimientos contables de ingresos, almacenados en la tabla vta.tblincomeitems.' WHERE [idsourcedata] = 1
GO
UPDATE [vta].[tblsourcedata] SET [sourcedatadescription] = 'INGRESOS\MOVIMIENTOS. Movimientos de entrada de dinero, almacenados en el tabla vta.tblincomemovement.' WHERE [idsourcedata] = 2
GO
UPDATE [vta].[tblsourcedata] SET [sourcedatadescription] = 'GASTOS\CONCEPTOS. Movimientos contables de gastos, almacenados en la tabla vta.tblinvoiceditems.' WHERE [idsourcedata] = 3
GO
UPDATE [vta].[tblsourcedata] SET [sourcedatadescription] = 'GASTOS\PAGOS. Movimientos de salida de dinero, almacenados en la tabla vta.tblpayment.' WHERE [idsourcedata] = 4
GO
UPDATE [vta].[tblsourcedata] SET [sourcedatadescription] = 'FONDEOS. Transferencias entre cuentas propias, almacenados en la tabla vta.tblfondos.' WHERE [idsourcedata] = 5
GO
UPDATE [vta].[tblsourcedata] SET [sourcedatadescription] = 'CONCILIACIONES. Movimientos de ventas por medio de Terminales de Punto de Venta. Almacenados en la tabla vta.tblbankstatements.' WHERE [idsourcedata] = 6
GO
UPDATE [vta].[tblsourcedata] SET [sourcedatadescription] = 'RESERVACIONES. Pagos de reservaciones de VTClub, almacenados en la tabla dbo.tblreservationspayment.' WHERE [idsourcedata] = 7
GO
UPDATE [vta].[tblsourcedata] SET [sourcedatadescription] = 'PAGOS MEMBERSHIPS. Pagos de membresias de VTClub, almacenados en la tabla dbo.tblpaymentspurchases.' WHERE [idsourcedata] = 8
GO


ALTER TABLE [vta].[tbltpv]  WITH CHECK ADD  CONSTRAINT [FK_tbltpv_tbldeposittypes] FOREIGN KEY([iddeposittype])
REFERENCES [vta].[tbldeposittypes] ([idDepositType])
GO

ALTER TABLE [vta].[tbltpv] CHECK CONSTRAINT [FK_tbltpv_tbldeposittypes]
GO

-- Se elimina el id 18 porque no tiene factura en la tabla tblbatch
DELETE FROM [dbo].[tblbatchdetailpre] where id = 18 and idBatch = 87
GO


ALTER TABLE [dbo].[tblbatch] WITH CHECK ADD CONSTRAINT [FK_tblbatch_tblbankaccount] FOREIGN KEY ([idbaccount])  
REFERENCES [vta].[tblbankaccount] ([idbaccount])
GO

ALTER TABLE [dbo].[tblbatch] CHECK CONSTRAINT [FK_tblbatch_tblbankaccount]
GO

-- se crean las relaciones entre la factura y los registros
ALTER TABLE [dbo].[tblbatchdetail] WITH CHECK ADD CONSTRAINT [FK_tblbatchdetail_tblbatch] FOREIGN KEY ([idBatch])  
REFERENCES [dbo].[tblbatch] ([idBatch])
GO

ALTER TABLE [dbo].[tblbatchdetail] CHECK CONSTRAINT [FK_tblbatchdetail_tblbatch]
GO

ALTER TABLE [dbo].[tblbatchdetail] WITH CHECK ADD CONSTRAINT [FK_tblbatchdetail_tblpurchases] FOREIGN KEY ([idPurchase])  
REFERENCES [dbo].[tblpurchases] ([idPurchase])
GO

ALTER TABLE [dbo].[tblbatchdetail] CHECK CONSTRAINT [FK_tblbatchdetail_tblpurchases]
GO


ALTER TABLE [dbo].[tblbatchdetailpre] WITH CHECK ADD CONSTRAINT [FK_tblbatchdetailpre_tblbatch] FOREIGN KEY ([idBatch])  
REFERENCES [dbo].[tblbatch] ([idBatch])
GO

ALTER TABLE [dbo].[tblbatchdetailpre] CHECK CONSTRAINT [FK_tblbatchdetailpre_tblbatch]
GO

ALTER TABLE [dbo].[tblbatchdetailpre] WITH CHECK ADD CONSTRAINT [FK_tblbatchdetailpre_tblpartners] FOREIGN KEY ([idPartner])  
REFERENCES [dbo].[tblpartners] ([idPartner])
GO

ALTER TABLE [dbo].[tblbatchdetailpre] CHECK CONSTRAINT [FK_tblbatchdetailpre_tblpartners]
GO

ALTER TABLE [dbo].[tblbatchdetailpre] WITH CHECK ADD CONSTRAINT [FK_tblbatchdetailpre_tblprefixes] FOREIGN KEY ([idPrefix])  
REFERENCES [dbo].[tblprefixes] ([idPrefix])
GO

ALTER TABLE [dbo].[tblbatchdetailpre] CHECK CONSTRAINT [FK_tblbatchdetailpre_tblprefixes]
GO

UPDATE [vta].[tblbankaccount] SET
[idbankaccntclasification] = 1
WHERE [idbaccount] = 6
GO

UPDATE [vta].[tblsegment] SET
segmentname = 'AGENCIA WP'
,Segmentshortname = 'AGN WP'
WHERE idsegment = 1