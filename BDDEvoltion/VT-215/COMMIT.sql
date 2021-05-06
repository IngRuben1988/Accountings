/***************************************************
Author: Ruben Magaña Alvarado
Date:   27 Junio 2019
Description: Create payment table
***************************************************/
drop table if exists [vta].[tblpayment]
go
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
	[paymentdeletedby]   [bit] NOT NULL,	 /*borrar registro*/
	[paymentdeletedon]   [datetime] NOT NULL,/*Cancelar registro*/
	[payemntcanceledby]  [bit] NOT NULL,	 /*Cancelar registro*/
	[paymentcanceledon]  [datetime] NOT NULL,/*Cancelar registro*/

 CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED 
(
	[idpayment] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO




ALTER TABLE [vta].[tblpayment]  WITH CHECK ADD  CONSTRAINT [FK_VTA_Payment_tblbankaccount_1] FOREIGN KEY([idbaccount])
REFERENCES [vta].[tblbankaccount] ([idbaccount])
GO
ALTER TABLE [vta].[tblpayment] CHECK CONSTRAINT [FK_VTA_Payment_tblbankaccount_1]
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







/***************************************************
Update: Ruben Magaña Alvarado
Date:   3 Julio 2019
Description: Create payment table
***************************************************/
--------------------------------------------------------------------------------------------------------------------------------
------------------------------- INVOICE / INVOICEITEM / INVOICEATTACHMENTS / INVOICE COMMENTS / LOG's  -------------------------
--------------------------------------------------------------------------------------------------------------------------------
  
 /****** Object:  Table [vta].[tblinvoice]    Script Date: 16/05/2019 09:43:13 a. m. ******/
ALTER TABLE [vta].[tblinvoice]  DROP CONSTRAINT if exists [FK_tblinvoice_tblusers] 
ALTER TABLE [vta].[tblinvoice]  DROP CONSTRAINT if exists[FK_tblinvoice_tblusers2]
ALTER TABLE [vta].[tblinvoice]  drop CONSTRAINT if exists[FK_VTA_tblinvoice_tblccurrency_1] 
ALTER TABLE [vta].[tblinvoice]  drop CONSTRAINT if exists[FK_VTA_tblinvoice_tblcompanies_1] 
ALTER TABLE [vta].[tblinvoiceComments]  drop CONSTRAINT if exists [FK_tblinvoiceComments_tblinvoice]
ALTER TABLE [vta].[tblinvoiceattach]  drop CONSTRAINT if exists [FK_VTA_tblinvoiceattach_tblinvoice_1]
ALTER TABLE [vta].[tblpayment]  drop CONSTRAINT if exists [FK_VTA_tblpayment_tblinvoice_1]
ALTER TABLE [vta].[tblinvoiceditem] DROP CONSTRAINT if exists[FK_VTA_tblinvoiceditem_tblinvoice_1]
go





drop table if exists [vta].[tblinvoice]
go
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
	[invoicedeletedby]	[int]		NOT NULL,
	[invoicedeleteon]	[datetime]  NOT NULL,/*Eliminacion del registro*/
PRIMARY KEY CLUSTERED 
(
	[idinvoice] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO






ALTER TABLE [vta].[tblinvoice]  WITH CHECK ADD  CONSTRAINT [FK_tblinvoice_tblusers] FOREIGN KEY([invoiceCreatedby])
REFERENCES [dbo].[tblusers] ([iduser])
GO
ALTER TABLE [vta].[tblinvoice] CHECK CONSTRAINT [FK_tblinvoice_tblusers]
GO
ALTER TABLE [vta].[tblinvoice]  WITH CHECK ADD  CONSTRAINT [FK_tblinvoice_tblusers2] FOREIGN KEY([invoiceUpdatedby])
REFERENCES [dbo].[tblusers] ([iduser])
GO
ALTER TABLE [vta].[tblinvoice] CHECK CONSTRAINT [FK_tblinvoice_tblusers2]
GO
ALTER TABLE [vta].[tblinvoice]  WITH CHECK ADD  CONSTRAINT [FK_VTA_tblinvoice_tblccurrency_1] FOREIGN KEY([idcurrency])
REFERENCES [dbo].[tblcurrencies] ([idcurrency])
GO
ALTER TABLE [vta].[tblinvoice] CHECK CONSTRAINT [FK_VTA_tblinvoice_tblccurrency_1]
GO
ALTER TABLE [vta].[tblinvoice]  WITH CHECK ADD  CONSTRAINT [FK_VTA_tblinvoice_tblcompanies_1] FOREIGN KEY([idCompany])
REFERENCES [vta].[tblcompanies] ([idcompany])
GO
ALTER TABLE [vta].[tblinvoice] CHECK CONSTRAINT [FK_VTA_tblinvoice_tblcompanies_1]
GO





------------------------------- L O G S ----------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------

/****** Object:  Table [vta].[tblinvoiceLog]    Script Date: 16/05/2019 09:43:13 a. m. ******/
Drop table if exists [vta].[tblinvoiceLog]
go
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
ALTER TABLE [vta].[tblinvoiceditem] drop CONSTRAINT if exists[FK_tblinvoiceditem_tblbugettype]
ALTER TABLE [vta].[tblinvoiceditem] drop CONSTRAINT if exists[FK_tblinvoiceitem_tblsupplier_1]
ALTER TABLE [vta].[tblinvoiceditem] drop CONSTRAINT if exists[FK_VTA_tblinvoiceditem_tblaccountsl4_1]
ALTER TABLE [vta].[tblinvoiceditem] drop CONSTRAINT if exists[FK_VTA_tblinvoiceditem_tblinvoiceitemstatus_1]
ALTER TABLE [vta].[tblinvoiceditem] drop CONSTRAINT if exists[FK_VTA_tblinvoiceditem_tblusers_1]
GO





  
Drop table if exists [vta].[tblinvoiceditem];
go
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
	[itemdeleted]		  [bit]		 NULL,
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
	[itemdeletedby]		  [int]		 NOT NULL,/*Eliminacion del registro*/
	[itemdeleteon]		  [datetime] NOT NULL,/*Eliminacion del registro*/

PRIMARY KEY CLUSTERED 
(
	[idinvoiceitem] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO






ALTER TABLE [vta].[tblinvoiceditem]  WITH CHECK ADD  CONSTRAINT [FK_tblinvoiceditem_tblbugettype] FOREIGN KEY([idbudgettype])
REFERENCES [vta].[tblbugettype] ([idbudgettype]) 
GO
ALTER TABLE [vta].[tblinvoiceditem] CHECK CONSTRAINT [FK_tblinvoiceditem_tblbugettype]
GO
ALTER TABLE [vta].[tblinvoiceditem]  WITH CHECK ADD  CONSTRAINT [FK_tblinvoiceitem_tblsupplier_1] FOREIGN KEY([idsupplier])
REFERENCES [vta].[tblsupplier] ([idsupplier])
GO
ALTER TABLE [vta].[tblinvoiceditem] CHECK CONSTRAINT [FK_tblinvoiceitem_tblsupplier_1]
GO
ALTER TABLE [vta].[tblinvoiceditem]  WITH CHECK ADD  CONSTRAINT [FK_VTA_tblinvoiceditem_tblaccountsl4_1] FOREIGN KEY([idaccountl4])
REFERENCES [vta].[tblaccountsl4] ([idaccountl4])
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








Drop table if exists [vta].[tblinvoiceditemLog]
go
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

--ALTER TABLE [vta].[tbluserpermissions] drop CONSTRAINT[FK_VTA_tbluserpermissions_tblpermissions_1]





Drop Table if exists [vta].[tblpermissions]
go
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





Drop table if exists [vta].[tbluserpermissions]
go
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

drop table if exists[vta].[tblincomemovement]
go
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
	[incomemovcanceled]		   [bit] NOT NULL,
	[incomemovdeleted]		   [bit] NOT NULL,
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






/*************************************insert data**********************************************************/
insert vta.tblpermissions(permissionController,permissionAction,permissionArea,
permissionImageClass,permissionTitle,permissionDescription,permissisonActiveli,permissionEstatus,permissionParentId,
permissionIsParent,permissionHasChild,PermissionMenu,permissionIsHtml)values
('Account','Index','','','Login','Acceso al sistema','1','1','1','1','1','1','1'),
('Home','Index','','','Home','Acceso al menu principal','1','1','1','1','1','1','1'),
('invoice','invoiceapp','','','Invoice','Acceso al modulo de gastos','1','1','1','1','1','1','1'),
('invoice','Sendinvoice','','','Invoice','Acceso a la insersion de nuevos gastos','1','1','1','1','1','1','1'),
('invoice','Sendinvoiceitem','','','InvoiceItem','Acceso a la insersion de nuevos gastos','1','1','1','1','1','1','1'),
('invoice','invoicesearch','','','InvoiceItem','Acceso a la insersion de nuevos gastos','1','1','1','1','1','1','1'),
('invoice','Getinvoice','','','InvoiceItem','Acceso a la insersion de nuevos gastos','1','1','1','1','1','1','1'),
('invoice','Sendinvoiceitemupdate','','','InvoiceItem','Acceso a la insersion de nuevos gastos','1','1','1','1','1','1','1'),
('invoice','Sendinvoiceitemdelete','','','InvoiceItem','Acceso a la insersion de nuevos gastos','1','1','1','1','1','1','1'),
('invoice','GetInvoiceitemsbyId','','','InvoiceItem','Acceso a la insersion de nuevos gastos','1','1','1','1','1','1','1'),
('invoice','sendinvoiceitem','','','InvoiceItem','Acceso a la insersion de nuevos gastos','1','1','1','1','1','1','1')
go





insert into vta.tbluserpermissions(idUser,idPermission,userpermissionActive)values
(1,1,1),
(1,2,1),
(1,3,1),
(2,1,1),
(2,2,1),
(2,3,1),
(3,1,1),
(3,2,1),
(3,3,1),
(1,4,1),
(1,5,1)
go









UPDATE tblusers set passwordHash='AFbNwEQyy4XQOtrh/heXtyd1C9FphddmfTy1JlhUtpw58ggeTKKV/UFGcbN3eGXE0A=='





insert into tblparameters (parameterValue,parameterDescription)values
('16','VTA-Tax'),('1','AccesVTCPermission')
go

--update tblparameters set parameterValue='1' where idParameter=23
--delete from vta.tbluserpermissions
--Delete from vta.tblpermissions
--go
--DBCC CHECKIDENT ('vta.tblpermissions',RESEED, 0)
--delete from vta.tbluserpermissions where IdUserPermission=7822
--update tblusers set idprofileaccount=1 where idUser=1




update dbo.tblusers set idprofileaccount=1 where idUser=1





--select*from vta.tbluserbacount
insert into vta.tbluserbacount
(iduser,idbaccount,userbacountcreatedby,userbacountcreationdate,userbacountactive)
select
iduser,idBAccount,userbacountCreatedby,userbacountCreationDate,userbacountActive
from test_vtrcasasyhotelesdb.vta.tbluserbacount 
where idUser=1 and idBAccount<=7



--select*from vta.tblbankprodttype
insert into vta.tblbankprodttype
(bankprodttypename,bankprodttypeshortname,bankprodttypedescription,bankprodttypeactive)
select bankaccnttypeName,bankaccnttypeShortName,bankaccnttypeDescription,bankaccnttypeActive
from test_vtrcasasyhotelesdb.vta.tblbankaccnttype 




--select*from vta.tblbaccounprodttype
insert into vta.tblbaccounprodttype
(idbaccount,idbankprodttype,baccountprodtypecreatedby,baccountprodtypecreationdate,baccountprodtypeactive,baccountprodtypeallowneg)
select
idBAccount,idBankAccntType,baccounttypetCreatedby,baccounttypeCreationDate,baccounttypeActive,baccounttypeAllowNeg
from test_vtrcasasyhotelesdb.vta.tblbaccounttype where idBAccount<7





--select*from vta.tblpermissions
--insert vta.tblpermissions(permissionController,permissionAction,permissionArea,
--permissionImageClass,permissionTitle,permissionDescription,permissisonActiveli,permissionEstatus,permissionParentId,
--permissionIsParent,permissionHasChild,PermissionMenu,permissionIsHtml)
--select 
--permissionController,permissionAction,permissionArea,
--permissionImageClass,permissionTitle,permissionDescription,permissisonActiveli,permissionEstatus,permissionParentId,
--permissionIsParent,permissionHasChild,PermissionMenu,permissionIsHtml
--from test_vtrcasasyhotelesdb.vta.tblpermissions
 







--insert into vta.tbluseraccl4(iduser,idaccountl4,useraccl4active)values
--(1,1,1),
--(1,2,1),
--(1,3,1),
--(1,4,1),
--(1,5,1)






--insert into vta.tbluserpermissions(idUser,idPermission,userpermissionActive)values
--(1,1,1),(1,2,1),(1,3,1),(1,4,1),(1,5,1),(1,6,1),(1,7,1),(1,8,1),(1,9,1),(1,10,1),(1,11,1),(1,12,1),(1,13,1),
--(1,14,1),(1,15,1),(1,16,1),(1,17,1),(1,18,1),(1,19,1),(1,20,1),(1,21,1),(1,22,1),(1,23,1),(1,24,1),(1,25,1)





--insert into vta.tbluserpermissions(idUser,idPermission,userpermissionActive)
--select idUser,idPermission,userpermissionActive 
--from VTH.vta.tbluserpermissions where idUser=1





--select*from vta.tbluserpermissions
















/****************/

ALTER TABLE [vta].[tblexpensereportsourcedata]  drop  CONSTRAINT [fk_tblexpensereportsourcedata_tblcompanies_1] 
GO

ALTER TABLE [vta].[tblexpensereportsourcedata]  drop  CONSTRAINT [fk_tblexpensereportsourcedata_tblsourcedata_1] 
GO


DROP TABLE if exists [vta].[tblexpensereportsourcedata]
GO



CREATE TABLE [vta].[tblexpensereportsourcedata](
	[idexpensereportsourcedata] [int] IDENTITY(1,1) NOT NULL,
	[idcompany] [int] NOT NULL,
	[idsourcedata] [int] NOT NULL,
	[expensereportsourcedataactive] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idexpensereportsourcedata] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [vta].[tblexpensereportsourcedata]  WITH CHECK ADD  CONSTRAINT [fk_tblexpensereportsourcedata_tblcompanies_1] FOREIGN KEY([idcompany])
REFERENCES [vta].[tblcompanies] ([idcompany])
GO
ALTER TABLE [vta].[tblexpensereportsourcedata] CHECK CONSTRAINT [fk_tblexpensereportsourcedata_tblcompanies_1]
GO
ALTER TABLE [vta].[tblexpensereportsourcedata]  WITH CHECK ADD  CONSTRAINT [fk_tblexpensereportsourcedata_tblsourcedata_1] FOREIGN KEY([idsourcedata])
REFERENCES [vta].[tblsourcedata] ([idsourcedata])
GO
ALTER TABLE [vta].[tblexpensereportsourcedata] CHECK CONSTRAINT [fk_tblexpensereportsourcedata_tblsourcedata_1]
GO



--delete from [vta].[tblexpensereportsourcedata]
--TRUNCATE TABLE [vta].[tblexpensereportsourcedata]
--GO
--DBCC CHECKIDENT ('vta.tblexpensereportsourcedata', RESEED, 0)
--GO  










/****************/



GO



--INSERT INTO [vta].[tblcompaniescurrencies] ([idcompany] ,[idcurrency] ,[companiescurrenciesactive]) VALUES (51,3,1)
--INSERT INTO [vta].[tblcompaniescurrencies] ([idcompany] ,[idcurrency] ,[companiescurrenciesactive]) VALUES (52,3,1)
--INSERT INTO [vta].[tblcompaniescurrencies] ([idcompany] ,[idcurrency] ,[companiescurrenciesactive]) VALUES (48,3,1)
--INSERT INTO [vta].[tblcompaniescurrencies] ([idcompany] ,[idcurrency] ,[companiescurrenciesactive]) VALUES (1,4,1)
--INSERT INTO [vta].[tblcompaniescurrencies] ([idcompany] ,[idcurrency] ,[companiescurrenciesactive]) VALUES (3,4,1)
--INSERT INTO [vta].[tblcompaniescurrencies] ([idcompany] ,[idcurrency] ,[companiescurrenciesactive]) VALUES (48,4,1)



Insert into vta.tblbugettype
(budgettypename,budgettypedescription,budgettypeorder,budgettypeactive)
select
budgettypename,budgettypedescription,budgettypeorder,1
from VTH.vta.tblbugettype






insert into vta.tblsupplier(supplierdescription,supplierRFC,supplieraddress,supplieractive)
select
supplierdescription,supplierRFC,supplieraddress,supplieractive
from VTH.vta.tblsupplier






Update vta.tblsupplier set suppliername=supplierdescription 
 






INSERT INTO vta.tblcompaniesuppliers
(idcompany,idsupplier)
select
idcompany,idsupplier
from VTH.vta.tblcompaniesuppliers where idCompany<4




