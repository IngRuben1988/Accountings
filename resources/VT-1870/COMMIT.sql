

CREATE SCHEMA vta AUTHORIZATION dbo
GO

--------------------------------------------------------------------------------------------------------------------------------
------------------------------- S E G M E N T S --------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [vta].[tblsegment](
	[idsegment] [int] IDENTITY(1,1) NOT NULL,
	[segmentname] [varchar](50) NULL,
	[Segmentshortname] [varchar](50) NULL,
	[segmentdescription] [varchar](50) NULL,
	[segmentactive] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idsegment] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

--------------------------------------------------------------------------------------------------------------------------------
------------------------------- C O M P A N I E S ------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE [vta].[tblcompanies](
	[idcompany] [int] IDENTITY(1,1) NOT NULL,
	[companyname] [varchar](72) NOT NULL,
	[companyshortname] [varchar](64) NOT NULL,
	[companyaddress] [varchar](255) NOT NULL,
	[companytelephone] [varchar](32) NOT NULL,
	[companyactive] [bit] NOT NULL,
	[companyorder] [int] NULL,
	[idsegment] [int] NULL,
 CONSTRAINT [tblcompanies_PK] PRIMARY KEY CLUSTERED 
(
	[idcompany] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [vta].[tblcompanies]  WITH CHECK ADD  CONSTRAINT [FK_tblcompanies_tblsegment] FOREIGN KEY([idsegment])
REFERENCES [vta].[tblsegment] ([idSegment])
GO

ALTER TABLE [vta].[tblcompanies] CHECK CONSTRAINT [FK_tblcompanies_tblsegment]
GO

--------------------------------------------------------------------------------------------------------------------------------
------------------------------- C O M P A N I E S     C U R R E N C I E S  -----------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE [vta].[tblcompaniescurrencies](
	[idcompaniescurrencies] [int] IDENTITY(1,1) NOT NULL,
	[idcompany] [int] NOT NULL,
	[idcurrency] [int] NOT NULL,
	[companiescurrenciesactive] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idcompaniescurrencies] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [vta].[tblcompaniescurrencies]  WITH CHECK ADD  CONSTRAINT [FK_VTA_tblcompaniescurrencies_tblcurrencies_1] FOREIGN KEY([idcompany])
REFERENCES [vta].[tblcompanies] ([idcompany])
GO

ALTER TABLE [vta].[tblcompaniescurrencies] CHECK CONSTRAINT [FK_VTA_tblcompaniescurrencies_tblcurrencies_1]
GO

ALTER TABLE [vta].[tblcompaniescurrencies]  WITH CHECK ADD  CONSTRAINT [FK_VTA_tblcompaniescurrencies_tblcurrencies_2] FOREIGN KEY([idcurrency])
REFERENCES [dbo].[tblcurrencies] ([idcurrency])
GO

ALTER TABLE [vta].[tblcompaniescurrencies] CHECK CONSTRAINT [FK_VTA_tblcompaniescurrencies_tblcurrencies_2]
GO

--------------------------------------------------------------------------------------------------------------------------------
------------------------------- U S E R - C O M P A N I E S --------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE [vta].[tblusercompanies](
	[idusercompany] [int] IDENTITY(1,1) NOT NULL,
	[idcompany] [int] NOT NULL,
	[iduser] [int] NOT NULL,
	[usercompanyuserlastchange] [int] NOT NULL,
	[usercompanydatelastchange] [datetime] NULL,
	[usercompanyactive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[idusercompany] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [vta].[tblusercompanies]  WITH CHECK ADD  CONSTRAINT [FK_VTA_tblusercompanies_tblcompanies_1] FOREIGN KEY([idcompany])
REFERENCES [vta].[tblcompanies] ([idCompany])
GO

ALTER TABLE [vta].[tblusercompanies] CHECK CONSTRAINT [FK_VTA_tblusercompanies_tblcompanies_1]
GO

ALTER TABLE [vta].[tblusercompanies]  WITH CHECK ADD  CONSTRAINT [FK_VTA_tblusercompanies_tblusers_1] FOREIGN KEY([IdUser])
REFERENCES [dbo].[tblusers] ([idUser])
GO

ALTER TABLE [vta].[tblusercompanies] CHECK CONSTRAINT [FK_VTA_tblusercompanies_tblusers_1]
GO


--------------------------------------------------------------------------------------------------------------------------------
------------------------------- ACCOUNT L1 L2 L3 L4 ----------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------

/****** Object:  Table [vta].[tblaccountsl1]    Script Date: 16/05/2019 09:43:11 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vta].[tblaccountsl1](
	[idaccountl1] [int] IDENTITY(1,1) NOT NULL,
	[accountl1name] [varchar](60) NULL,
	[accountl1description] [varchar](60) NULL,
	[accountl1active] [bit] NULL,
	[accountl1order] [int] NOT NULL,
 CONSTRAINT [PK_idaccountsl1_tblaccountsl1] PRIMARY KEY NONCLUSTERED 
(
	[idaccountl1] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [vta].[tblaccountsl2]    Script Date: 16/05/2019 09:43:11 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vta].[tblaccountsl2](
	[idaccountl2] [int] IDENTITY(1,1) NOT NULL,
	[idaccountl1] [int] NOT NULL,
	[accountl2name] [varchar](60) NULL,
	[accountl2description] [varchar](60) NULL,
	[accountl2active] [bit] NULL,
	[accountl2order] [int] NOT NULL,
 CONSTRAINT [PK_idaccountsl2_tblaccountsl2] PRIMARY KEY NONCLUSTERED 
(
	[idaccountl2] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [vta].[tblaccountsl3]    Script Date: 16/05/2019 09:43:11 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vta].[tblaccountsl3](
	[idaccountl3] [int] IDENTITY(1,1) NOT NULL,
	[idaccountl2] [int] NOT NULL,
	[accountl3name] [varchar](60) NULL,
	[accountl3description] [varchar](60) NULL,
	[accountl3active] [bit] NULL,
	[accountl3order] [int] NOT NULL,
 CONSTRAINT [PK_idaccountsl3_tblaccountsl3] PRIMARY KEY NONCLUSTERED 
(
	[idAccountl3] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [vta].[tblaccountsl4]    Script Date: 16/05/2019 09:43:11 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vta].[tblaccountsl4](
	[idAccountl4] [int] IDENTITY(1,1) NOT NULL,
	[idAccountl3] [int] NOT NULL,
	[accountl4name] [varchar](60) NULL,
	[accountl4description] [varchar](60) NULL,
	[accountl4active] [bit] NULL,
	[accountl4order] [int] NOT NULL,
 CONSTRAINT [PK_idaccountsl4_tblaccountsl4] PRIMARY KEY NONCLUSTERED 
(
	[idaccountl4] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

---
--- Orders Default
---
ALTER TABLE [vta].[tblaccountsl1] ADD  DEFAULT ((0)) FOR [accountl1order]
GO
ALTER TABLE [vta].[tblaccountsl2] ADD  DEFAULT ((0)) FOR [accountl2order]
GO
ALTER TABLE [vta].[tblaccountsl3] ADD  DEFAULT ((0)) FOR [accountl3order]
GO
ALTER TABLE [vta].[tblaccountsl4] ADD  DEFAULT ((0)) FOR [accountl4order]


---
--- Foreing Keys
---
ALTER TABLE [vta].[tblaccountsl2]  WITH CHECK ADD  CONSTRAINT [FK_VTA_tblaccountsl2_tblaccountsl1_1] FOREIGN KEY([idaccountl1])
REFERENCES [vta].[tblaccountsl1] ([idaccountl1])
GO
ALTER TABLE [vta].[tblaccountsl2] CHECK CONSTRAINT [FK_VTA_tblaccountsl2_tblaccountsl1_1]
GO
ALTER TABLE [vta].[tblaccountsl3]  WITH CHECK ADD  CONSTRAINT [FK_VTA_tblaccountsl3_tblaccountsl2_1] FOREIGN KEY([idaccountl2])
REFERENCES [vta].[tblaccountsl2] ([idaccountl2])
GO
ALTER TABLE [vta].[tblaccountsl3] CHECK CONSTRAINT [FK_VTA_tblaccountsl3_tblaccountsl2_1]
GO
ALTER TABLE [vta].[tblaccountsl4]  WITH CHECK ADD  CONSTRAINT [FK_VTA_tblaccountsl4_tblaccountsl3_1] FOREIGN KEY([idaccountl3])
REFERENCES [vta].[tblaccountsl3] ([idaccountl3])
GO
ALTER TABLE [vta].[tblaccountsl4] CHECK CONSTRAINT [FK_VTA_tblaccountsl4_tblaccountsl3_1]
GO

--------------------------------------------------------------------------------------------------------------------------------
------------------------------- PROFILES ---------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------

/****** Object:  Table [vta].[tblprofilesaccounts]    Script Date: 16/05/2019 09:43:14 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vta].[tblprofilesaccounts](
	[idprofileaccount] [int] IDENTITY(1,1) NOT NULL,
	[profileaccountname] [varchar](255) NULL,
	[profileaccountshortame] [varchar](255) NULL,
	[profileaccountactive] [bit] NULL,
 CONSTRAINT [PK_tblprofilesaccounts] PRIMARY KEY CLUSTERED 
(
	[idprofileaccount] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

--------------------------------------------------------------------------------------------------------------------------------
------------------------------- PROFILES ACCOUNT -------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------

/****** Object:  Table [vta].[tblprofaccclass3]    Script Date: 16/05/2019 09:43:14 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vta].[tblprofaccclass3](
	[idprofaccclass3] [int] IDENTITY(1,1) NOT NULL,
	[idaccountl3] [int] NOT NULL,
	[idprofileaccount] [int] NOT NULL,
	[profaccclassuserlastchange] [int] NOT NULL,
	[profaccclassdatelastchange] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idprofaccclass3] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


ALTER TABLE [vta].[tblprofaccclass3]  WITH CHECK ADD  CONSTRAINT [FK_tblprofaccsclass3_tblaccountsl3] FOREIGN KEY([idaccountl3])
REFERENCES [vta].[tblaccountsl3] ([idaccountl3])
GO
ALTER TABLE [vta].[tblprofaccclass3] CHECK CONSTRAINT [FK_tblprofaccsclass3_tblaccountsl3]
GO
ALTER TABLE [vta].[tblprofaccclass3]  WITH CHECK ADD  CONSTRAINT [FK_tblprofaccsclass3_tblprofilesaccounts] FOREIGN KEY([idprofileaccount])
REFERENCES [vta].[tblprofilesaccounts] ([idprofileaccount])
GO
ALTER TABLE [vta].[tblprofaccclass3] CHECK CONSTRAINT [FK_tblprofaccsclass3_tblprofilesaccounts]
GO

--------------------------------------------------------------------------------------------------------------------------------
------------------------------- TBL TYPE REPORT --------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------

/****** Object:  Table [vta].[tblaccounttype]    Script Date: 16/05/2019 09:43:11 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vta].[tblaccounttypereport](
	[idaccounttypereport] [int] IDENTITY(1,1) NOT NULL,
	[accounttypereportname] [varchar](100) NOT NULL,
	[accounttypereportdescription] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[idaccounttypereport] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


--------------------------------------------------------------------------------------------------------------------------------
------------------------------- TBLACCOUNTSL4 - TYPE REPORT --------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------

/****** Object:  Table [vta].[tblaccountl4accounttype]    Script Date: 16/05/2019 09:43:11 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vta].[tblaccountl4accounttype](
	[idaccL4acctypereport] [int] IDENTITY(1,1) NOT NULL,
	[idaccountl4] [int] NOT NULL,
	[idaccounttypereport] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idaccL4acctypereport] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [vta].[tblaccountl4accounttype]  WITH CHECK ADD  CONSTRAINT [FK_tblaccountl4accounttype_tblaccountsl4] FOREIGN KEY([idaccountl4])
REFERENCES [vta].[tblaccountsl4] ([idaccountl4])
GO
ALTER TABLE [vta].[tblaccountl4accounttype] CHECK CONSTRAINT [FK_tblaccountl4accounttype_tblaccountsl4]
GO
ALTER TABLE [vta].[tblaccountl4accounttype]  WITH CHECK ADD  CONSTRAINT [FK_tblaccountl4accounttype_tblaccounttype] FOREIGN KEY([idaccounttypereport])
REFERENCES [vta].[tblaccounttype] ([idaccounttypereport])
GO
ALTER TABLE [vta].[tblaccountl4accounttype] CHECK CONSTRAINT [FK_tblaccountl4accounttype_tblaccounttype]
GO

--------------------------------------------------------------------------------------------------------------------------------
------------------------------- TBLSEGMENTS - ACCOUNTL4 ------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------

/****** Object:  Table [vta].[tblsegmentaccl4]    Script Date: 16/05/2019 09:43:14 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vta].[tblsegmentaccl4](
	[idsegmentaccl4] [int] IDENTITY(1,1) NOT NULL,
	[idsegment] [int] NOT NULL,
	[idaccountl4] [int] NOT NULL,
	[segmentaccl4active] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idsegmentaccl4] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [vta].[tblsegmentaccl4]  WITH CHECK ADD  CONSTRAINT [fk_tblsegmentaccl4_tblaccountsl4_1] FOREIGN KEY([idaccountl4])
REFERENCES [vta].[tblaccountsl4] ([idaccountl4])
GO
ALTER TABLE [vta].[tblsegmentaccl4] CHECK CONSTRAINT [fk_tblsegmentaccl4_tblaccountsl4_1]
GO
ALTER TABLE [vta].[tblsegmentaccl4]  WITH CHECK ADD  CONSTRAINT [fk_tblsegmentaccl4_tblsegment_1] FOREIGN KEY([idsegment])
REFERENCES [vta].[tblsegment] ([idsegment])
GO
ALTER TABLE [vta].[tblsegmentaccl4] CHECK CONSTRAINT [fk_tblsegmentaccl4_tblsegment_1]
GO

--------------------------------------------------------------------------------------------------------------------------------
------------------------------- TBLUSER - ACCOUNTL4 ----------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------

/****** Object:  Table [vta].[tbluseraccl4]    Script Date: 16/05/2019 09:43:14 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vta].[tbluseraccl4](
	[iduseraccl4] [int] IDENTITY(1,1) NOT NULL,
	[iduser] [int] NOT NULL,
	[idaccountl4] [int] NOT NULL,
	[useraccl4active] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[iduseraccl4] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


ALTER TABLE [vta].[tbluseraccl4]  WITH CHECK ADD  CONSTRAINT [fk_tbluseraccl4_tblaccountsl4_1] FOREIGN KEY([idaccountl4])
REFERENCES [vta].[tblaccountsl4] ([idaccountl4])
GO
ALTER TABLE [vta].[tbluseraccl4] CHECK CONSTRAINT [fk_tbluseraccl4_tblaccountsl4_1]
GO
ALTER TABLE [vta].[tbluseraccl4]  WITH CHECK ADD  CONSTRAINT [fk_tbluseraccl4_tblusers_1] FOREIGN KEY([iduser])
REFERENCES [dbo].[tblusers] ([idUser])
GO
ALTER TABLE [vta].[tbluseraccl4] CHECK CONSTRAINT [fk_tbluseraccl4_tblusers_1]
GO


--------------------------------------------------------------------------------------------------------------------------------
------------------------------- INCOME /INVOICE STATUS -------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------

/****** Object:  Table [vta].[tblinvoiceitemstatus]    Script Date: 16/05/2019 09:43:13 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vta].[tblinvoiceitemstatus](
	[idinvoiceItemstatus] [int] IDENTITY(1,1) NOT NULL,
	[invoicestatusname] [nvarchar](250) NOT NULL,
	[invoicestatusshortname] [nvarchar](4) NOT NULL,
	[invoicestatusactive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[idInvoiceItemStatus] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

--------------------------------------------------------------------------------------------------------------------------------
------------------------------- A T A C H M E N T S ----------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------

/****** Object:  Table [vta].[tblattachments]    Script Date: 16/05/2019 09:43:11 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vta].[tblattachments](
	[idattachment] [int] IDENTITY(1,1) NOT NULL,
	[attachmentname] [nvarchar](70) NULL,
	[attachmentactive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[idAttachment] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


--------------------------------------------------------------------------------------------------------------------------------
------------------------------- I N C O M E , I T E M S , C O M M E N T S , L O G ' s ------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------

/****** Object:  Table [vta].[tblincome]    Script Date: 16/05/2019 09:43:12 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vta].[tblincome](
	[idincome] [bigint] IDENTITY(1,1) NOT NULL,
	[idcompany] [int] NOT NULL,
	[idcurrency] [int] NOT NULL,
	[incomeapplicationdate] [datetime] NOT NULL,
	[incomenumber] [int] NOT NULL,
	[incomecreatedby] [int] NOT NULL,
	[incomecreactiondate] [datetime] NOT NULL,
	[incomeupdatedby] [int] NOT NULL,
	[incometupdateon] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idincome] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [vta].[tblincomecomments]    Script Date: 16/05/2019 09:43:13 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vta].[tblincomecomments](
	[idincomecomment] [int] IDENTITY(1,1) NOT NULL,
	[idincome] [bigint] NOT NULL,
	[iduser] [int] NOT NULL,
	[invcomeCommentdescription] [varchar](500) NULL,
	[incomeCommentcreactiondate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idincomecomment] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [vta].[tblincomeattach]    Script Date: 16/05/2019 09:43:13 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vta].[tblincomeattach](
	[idincomeattach] [int] IDENTITY(1,1) NOT NULL,
	[idincome] [bigint] NOT NULL,
	[idattach] [int] NOT NULL,
	[incomeattachname] [nvarchar](500) NOT NULL,
	[incomeattachshortname] [nvarchar](500) NOT NULL,
	[incomeattachdirectory] [nvarchar](max) NOT NULL,
	[incomeattachcontenttype] [nvarchar](500) NOT NULL,
	[incomeattachuserlastchange] [int] NOT NULL,
	[incomeattachdatelastchange] [datetime] NULL,
	[incomeattachactive] [bit] NOT NULL,

PRIMARY KEY CLUSTERED 
(
	[idincomeattach] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


/****** Object:  Table [vta].[tblincomeitem]    Script Date: 16/05/2019 09:43:13 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vta].[tblincomeitem](
	[idincomeitem] [bigint] IDENTITY(1,1) NOT NULL,
	[idincome] [bigint] NOT NULL,
	[idAccountl4] [int] NOT NULL,
	[idincomeitemstatus] [int] NOT NULL,
	[iduser] [int] NOT NULL,
	[incomeitemdate] [datetime] NOT NULL,
	[incomeitemsubtotal] [numeric](14, 2) NOT NULL,
	[incomedescription] [nvarchar](250) NULL,
PRIMARY KEY CLUSTERED 
(
	[idincomeitem] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [vta].[tblincomeitemLog]    Script Date: 16/05/2019 09:43:13 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vta].[tblincomeitemLog](
	[LogId] [int] IDENTITY(1,1) NOT NULL,
	[LogDate] [datetime] NULL,
	[LogUser] [int] NULL,
	[LogObs] [nvarchar](max) NULL,
	[idincomeitem] [bigint] NOT NULL,
	[idincome] [bigint] NOT NULL,
	[idaccountl4] [int] NOT NULL,
	[idincomeitemstatus] [int] NOT NULL,
	[iduser] [int] NOT NULL,
	[incomeitemdate] [datetime] NOT NULL,
	[incomeitemsubtotal] [numeric](14, 2) NOT NULL,
	[incomedescription] [nvarchar](250) NULL,
PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [vta].[tblincomeLog]    Script Date: 16/05/2019 09:43:13 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vta].[tblincomeLog](
	[LogId] [bigint] IDENTITY(1,1) NOT NULL,
	[LogDate] [datetime] NULL,
	[LogUser] [int] NULL,
	[LogObs] [nvarchar](max) NULL,
	[idincome] [bigint] NOT NULL,
	[idcompany] [int] NOT NULL,
	[idcurrency] [int] NOT NULL,
	[incomeapplicationdate] [datetime] NOT NULL,
	[incomenumber] [int] NOT NULL,
	[incomecreatedby] [int] NOT NULL,
	[incomecreactionDate] [datetime] NOT NULL,
	[incomeupdatedby] [int] NOT NULL,
	[incometupdateon] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


ALTER TABLE [vta].[tblincomeLog] ADD  DEFAULT (getdate()) FOR [incomeapplicationdate]
GO

ALTER TABLE [vta].[tblincomeLog] ADD  DEFAULT ((0)) FOR [incomenumber]
GO

ALTER TABLE [vta].[tblincomeitem] ADD  DEFAULT ((0)) FOR [idincomeitemstatus]
GO

ALTER TABLE [vta].[tblincome]  WITH CHECK ADD  CONSTRAINT [FK_tblincome_tblccurrency_1] FOREIGN KEY([idCurrency])
REFERENCES [dbo].[tblcurrencies] ([idcurrency])
GO
ALTER TABLE [vta].[tblincome] CHECK CONSTRAINT [FK_tblincome_tblccurrency_1]
GO
ALTER TABLE [vta].[tblincome]  WITH CHECK ADD  CONSTRAINT [FK_tblincome_tblcompanies_1] FOREIGN KEY([idcompany])
REFERENCES [vta].[tblcompanies] ([idcompany])
GO
ALTER TABLE [vta].[tblincome] CHECK CONSTRAINT [FK_tblincome_tblcompanies_1]
GO
ALTER TABLE [vta].[tblincome]  WITH CHECK ADD  CONSTRAINT [FK_tblincome_tblusers] FOREIGN KEY([incomecreatedby])
REFERENCES [dbo].[tblusers] ([idUser])
GO
ALTER TABLE [vta].[tblincome] CHECK CONSTRAINT [FK_tblincome_tblusers]
GO
ALTER TABLE [vta].[tblincome]  WITH CHECK ADD  CONSTRAINT [FK_tblincome_tblusers2] FOREIGN KEY([incomeupdatedby])
REFERENCES [dbo].[tblusers] ([idUser])
GO
ALTER TABLE [vta].[tblincome] CHECK CONSTRAINT [FK_tblincome_tblusers2]
GO
ALTER TABLE [vta].[tblincomecomments]  WITH CHECK ADD  CONSTRAINT [FK_tblincomeComments_tblincome] FOREIGN KEY([idincome])
REFERENCES [vta].[tblincome] ([idincome])
GO
ALTER TABLE [vta].[tblincomecomments]  WITH CHECK ADD  CONSTRAINT [FK_tblincomeComments_tblusers] FOREIGN KEY([iduser])
REFERENCES [dbo].[tblusers] ([idUser])
GO
ALTER TABLE [vta].[tblincomecomments] CHECK CONSTRAINT [FK_tblincomeComments_tblusers]
GO
ALTER TABLE [vta].[tblincomeitem]  WITH CHECK ADD  CONSTRAINT [FK_tblincomeitem_tblaccountsl4_1] FOREIGN KEY([idaccountl4])
REFERENCES [vta].[tblaccountsl4] ([idaccountl4])
GO
ALTER TABLE [vta].[tblincomeitem] CHECK CONSTRAINT [FK_tblincomeitem_tblaccountsl4_1]
GO
ALTER TABLE [vta].[tblincomeitem]  WITH CHECK ADD  CONSTRAINT [FK_tblincomeitem_tblincome_1] FOREIGN KEY([idincome])
REFERENCES [vta].[tblincome] ([idincome])
GO
ALTER TABLE [vta].[tblincomeitem] CHECK CONSTRAINT [FK_tblincomeitem_tblincome_1]
GO
ALTER TABLE [vta].[tblincomeitem]  WITH CHECK ADD  CONSTRAINT [FK_tblincomeitem_tblusers_1] FOREIGN KEY([iduser])
REFERENCES [dbo].[tblusers] ([idUser])
GO
ALTER TABLE [vta].[tblincomeitem] CHECK CONSTRAINT [FK_tblincomeitem_tblusers_1]
GO
ALTER TABLE [vta].[tblincomeitem]  WITH CHECK ADD  CONSTRAINT [FK_VTA_tblincomeitem_tblinvoiceitemstatus_1] FOREIGN KEY([idIncomeitemstatus])
REFERENCES [vta].[tblinvoiceitemstatus] ([idinvoiceitemstatus])
GO
ALTER TABLE [vta].[tblincomeitem] CHECK CONSTRAINT [FK_VTA_tblincomeitem_tblinvoiceitemstatus_1]
GO

ALTER TABLE [vta].[tblincomeattach]  WITH CHECK ADD  CONSTRAINT [FK_VTA_tblincomeattach_tblincome_1] FOREIGN KEY([idincome])
REFERENCES [vta].[tblincome] ([idincome])
GO
ALTER TABLE [vta].[tblincomeattach] CHECK CONSTRAINT [FK_VTA_tblincomeattach_tblincome_1]
GO
ALTER TABLE [vta].[tblincomeattach]  WITH CHECK ADD  CONSTRAINT [FK_VTA_tblincomeattach_tblusers_1] FOREIGN KEY([incomeattachuserlastchange])
REFERENCES [dbo].[tblusers] ([idUser])
GO
ALTER TABLE [vta].[tblincomeattach] CHECK CONSTRAINT [FK_VTA_tblincomeattach_tblusers_1]
GO
ALTER TABLE [vta].[tblincomeattach]  WITH CHECK ADD  CONSTRAINT [FK_VTA_tblincomeattach_tblattachments_1] FOREIGN KEY([idattach])
REFERENCES [vta].[tblattachments] ([idattachment])
GO
ALTER TABLE [vta].[tblincomeattach] CHECK CONSTRAINT [FK_VTA_tblincomeattach_tblattachments_1]
GO

--------------------------------------------------------------------------------------------------------------------------------
------------------------------- T B L U S E R S    T Y P E A C C O U N T -------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------

ALTER TABLE [tblusers] ADD [idprofileaccount] int NULL DEFAULT 0
GO

-- ALTER TABLE [dbo].[tblusers] ADD  DEFAULT ((0)) FOR [idprofileaccount]
-- GO

ALTER TABLE [dbo].[tblusers]  WITH CHECK ADD  CONSTRAINT [FK_VTA_tblusers_tblprofilesaccounts_1] FOREIGN KEY([idprofileaccount])
REFERENCES [vta].[tblprofilesaccounts] ([idprofileaccount])
GO





--------------------------------------------------------------------------------------------------------------------------------
------------------------------- S O U R C E D A T A  ---------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE [vta].[tblsourcedata](
	[idsourcedata] [int] IDENTITY(1,1) NOT NULL,
	[sourcedataname] [varchar](100) NOT NULL,
	[sourcedataactive] [bit] NOT NULL,
 CONSTRAINT [PK_tblsourcedata] PRIMARY KEY CLUSTERED 
(
	[idsourcedata] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

--------------------------------------------------------------------------------------------------------------------------------
------------------------------- B U D G E T   T Y P E  -------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------

/****** Object:  Table [vta].[tblbugettype]    Script Date: 16/05/2019 09:43:12 a. m. ******/
CREATE TABLE [vta].[tblbugettype](
	[idbudgettype] [int] IDENTITY(1,1) NOT NULL,
	[budgettypename] [varchar](100) NOT NULL,
	[budgettypedescription] [varchar](150) NOT NULL,
	[budgettypeorder] [int] NOT NULL,
	[budgettypeactive] [bit] NOT NULL,
 CONSTRAINT [PK_tblbugettype] PRIMARY KEY CLUSTERED 
(
	[idbudgettype] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

--------------------------------------------------------------------------------------------------------------------------------
------------------------------- S U P P L I E R --------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------

/****** Object:  Table [vta].[tblsupplier]    Script Date: 16/05/2019 09:43:14 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [vta].[tblsupplier](
	[idsupplier] [int] IDENTITY(1,1) NOT NULL,
	[suppliername] [varchar](255) NULL,
	[supplierdescription] [varchar](255) NULL,
	[supplierRFC] [varchar](15) NULL,
	[supplieraddress] [varchar](500) NULL,
	[supplieractive] [bit] NULL,
 CONSTRAINT [PK_idSupplier] PRIMARY KEY CLUSTERED 
(
	[idsupplier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

--------------------------------------------------------------------------------------------------------------------------------
------------------------------- S U P P L I E R --------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------


CREATE TABLE [vta].[tblattachments](
	[idattachment] [int] IDENTITY(1,1) NOT NULL,
	[attachmentname] [nvarchar](70) NULL,
	[attachmentactive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[idattachment] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

--------------------------------------------------------------------------------------------------------------------------------
------------------------------- EXPENSE REPORT SOURCEDATA / EXPENSE REPORT SOURCEDATA TYPE -------------------------------------
--------------------------------------------------------------------------------------------------------------------------------

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

CREATE TABLE [vta].[tblexpensereportsourcedatatypes](
	[idexpensereportsourcedatatypes] [int] IDENTITY(1,1) NOT NULL,
	[idexpensereportsourcedata] [int] NOT NULL,
	[idtype] [int] NOT NULL,
	[expensereportsourcedatatypesactive] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idexpensereportsourcedatatypes] ASC
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

ALTER TABLE [vta].[tblexpensereportsourcedatatypes]  WITH CHECK ADD  CONSTRAINT [fk_tblexpensereportsourcedatatypes_tblexpensereportsourcedata_1] FOREIGN KEY([idExpenseReportSourcedata])
REFERENCES [vta].[tblexpensereportsourcedata] ([idexpensereportsourcedata])
GO

 
--------------------------------------------------------------------------------------------------------------------------------
------------------------------- INVOICE / INVOICEITEM / INVOICEATTACHMENTS / INVOICE COMMENTS / LOG's  -------------------------
--------------------------------------------------------------------------------------------------------------------------------
  
 /****** Object:  Table [vta].[tblinvoice]    Script Date: 16/05/2019 09:43:13 a. m. ******/
CREATE TABLE [vta].[tblinvoice](
	[idinvoice] [int] IDENTITY(1,1) NOT NULL,
	[idcompany] [int] NOT NULL,
	[invoiceapplicationdate] [datetime] NOT NULL,
	[invoicenumber] [int] NOT NULL,
	[invoicecreatedby] [int] NOT NULL,
	[invoicecreationdate] [datetime] NOT NULL,
	[invoiceupdatedby] [int] NOT NULL,
	[invoicetupdateon] [datetime] NOT NULL,
	[idcurrency] [int] NULL,
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

------------------------------- I N V O I C E  I T E M S -----------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------

/****** Object:  Table [vta].[tblinvoiceditem]    Script Date: 16/05/2019 09:43:13 a. m. ******/
CREATE TABLE [vta].[tblinvoiceditem](
	[idinvoiceitem] [int] IDENTITY(1,1) NOT NULL,
	[idinvoice] [int] NOT NULL,
	[idaccountl4] [int] NOT NULL,
	[idinvoiceitemstatus] [int] NOT NULL,
	[iduser] [int] NOT NULL,
	[invoiceditemsubtotal] [numeric](14, 2) NULL,
	[description] [nvarchar](250) NULL,
	[deleted] [bit] NULL,
	[invoiceditemistax] [bit] NOT NULL,
	[invoiceditemtaxes] [numeric](14, 2) NOT NULL,
	[invoiceditembillidentifier] [nvarchar](50) NULL,
	[idsupplier] [int] NOT NULL,
	[invoiceditemsupplierother] [nvarchar](250) NULL,
	[invoiceditemothertaxesammount] [numeric](14, 2) NOT NULL,
	[invoiceditemsingleexibitionpayment] [bit] NOT NULL,
	[idbudgettype] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idinvoiceitem] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [vta].[tblinvoiceditem] ADD  DEFAULT ((0)) FOR [invoiceditemistax]
GO
ALTER TABLE [vta].[tblinvoiceditem] ADD  DEFAULT ((0)) FOR [invoiceditemtaxes]
GO
ALTER TABLE [vta].[tblinvoiceditem] ADD  DEFAULT ((1)) FOR [idsupplier]
GO
ALTER TABLE [vta].[tblinvoiceditem] ADD  DEFAULT ((0)) FOR [invoiceditemothertaxesammount]
GO
ALTER TABLE [vta].[tblinvoiceditem] ADD  DEFAULT ((1)) FOR [invoiceditemsingleexibitionpayment]
GO
ALTER TABLE [vta].[tblinvoiceditem] ADD  DEFAULT ((1)) FOR [idbudgettype]
GO
ALTER TABLE [vta].[tblinvoiceditem]  WITH CHECK ADD  CONSTRAINT [FK_tblinvoiceditem_tblbugettype] FOREIGN KEY([idbudgettype])
REFERENCES [vta].[tblbugettype] ([idbudgettype]) 
GO
ALTER TABLE [vta].[tblinvoiceditem] CHECK CONSTRAINT [FK_tblinvoiceditem_tblbugettype]
GO
ALTER TABLE [vta].[tblinvoiceditem]  WITH CHECK ADD  CONSTRAINT [fk_tblinvoiceitem_tblsupplier_1] FOREIGN KEY([idsupplier])
REFERENCES [vta].[tblsupplier] ([idsupplier])
GO
ALTER TABLE [vta].[tblinvoiceditem] CHECK CONSTRAINT [fk_tblinvoiceitem_tblsupplier_1]
GO
ALTER TABLE [vta].[tblinvoiceditem]  WITH CHECK ADD  CONSTRAINT [FK_VTA_tblinvoiceditem_tblaccountsl4_1] FOREIGN KEY([idaccountl4])
REFERENCES [vta].[tblaccountsl4] ([idaccountl4])
GO
ALTER TABLE [vta].[tblinvoiceditem] CHECK CONSTRAINT [FK_VTA_tblinvoiceditem_tblaccountsl4_1]
GO
ALTER TABLE [vta].[tblinvoiceditem]  WITH CHECK ADD  CONSTRAINT [FK_VTA_tblinvoiceditem_tblinvoice_1] FOREIGN KEY([idinvoice])
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
------------------------------- A T T A C H M E N T S --------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------

/****** Object:  Table [vta].[tblinvoiceattach]    Script Date: 16/05/2019 09:43:13 a. m. ******/
CREATE TABLE [vta].[tblinvoiceattach](
	[idinvoiceattach] [int] IDENTITY(1,1) NOT NULL,
	[idinvoice] [int] NOT NULL,
	[idattach] [int] NOT NULL,
	[invoiceattachname] [nvarchar](500) NOT NULL,
	[invoiceattachdirectory] [nvarchar](max) NOT NULL,
	[invoiceattachcontenttype] [nvarchar](500) NOT NULL,
	[invoiceattachuserlastchange] [int] NOT NULL,
	[invoiceattachdatelastchange] [datetime] NULL,
	[invoiceattachactive] [bit] NOT NULL,
	[invoiceattachshortname] [nvarchar](500) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idinvoiceattach] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [vta].[tblinvoiceattach] ADD  DEFAULT ('') FOR [invoiceattachshortname]
GO

ALTER TABLE [vta].[tblinvoiceattach]  WITH CHECK ADD  CONSTRAINT [FK_VTA_tblinvoiceattach_tblinvoice_1] FOREIGN KEY([idinvoice])
REFERENCES [vta].[tblinvoice] ([idinvoice])
GO
ALTER TABLE [vta].[tblinvoiceattach] CHECK CONSTRAINT [FK_VTA_tblinvoiceattach_tblinvoice_1]
GO
ALTER TABLE [vta].[tblinvoiceattach]  WITH CHECK ADD  CONSTRAINT [FK_VTA_tblinvoiceattach_tblusers_1] FOREIGN KEY([invoiceattachuserlastchange])
REFERENCES [dbo].[tblusers] ([idUser])
GO
ALTER TABLE [vta].[tblinvoiceattach] CHECK CONSTRAINT [FK_VTA_tblinvoiceattach_tblusers_1]
GO
ALTER TABLE [vta].[tblinvoiceattach]  WITH CHECK ADD  CONSTRAINT [FK_VTA_tblpayattach_tblattachments_1] FOREIGN KEY([idattach])
REFERENCES [vta].[tblattachments] ([idattachment])
GO
ALTER TABLE [vta].[tblinvoiceattach] CHECK CONSTRAINT [FK_VTA_tblpayattach_tblattachments_1]
GO

------------------------------- C O M M E N T S --------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE [vta].[tblinvoicecomments](
	[idinvoicecomment] [int] IDENTITY(1,1) NOT NULL,
	[idinvoice] [int] NOT NULL,
	[iduser] [int] NOT NULL,
	[invoicecommentdescription] [varchar](500) NULL,
	[invoicecommentcreactiondate] [datetime] NOT NULL,
 CONSTRAINT [PK_tblinvoiceComments] PRIMARY KEY CLUSTERED 
(
	[idinvoicecomment] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [vta].[tblinvoicecomments]  WITH CHECK ADD  CONSTRAINT [FK_tblinvoiceComments_tblinvoice] FOREIGN KEY([idinvoice])
REFERENCES [vta].[tblinvoice] ([idinvoice])
GO
ALTER TABLE [vta].[tblinvoicecomments] CHECK CONSTRAINT [FK_tblinvoiceComments_tblinvoice]
GO
ALTER TABLE [vta].[tblinvoicecomments]  WITH CHECK ADD  CONSTRAINT [FK_tblinvoiceComments_tblusers] FOREIGN KEY([iduser])
REFERENCES [dbo].[tblusers] ([idUser])
GO
ALTER TABLE [vta].[tblinvoicecomments] CHECK CONSTRAINT [FK_tblinvoiceComments_tblusers]
GO


------------------------------- L O G S ----------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------

/****** Object:  Table [vta].[tblinvoiceLog]    Script Date: 16/05/2019 09:43:13 a. m. ******/

CREATE TABLE [vta].[tblinvoiceLog](
	[LogId] [int] IDENTITY(1,1) NOT NULL,
	[LogDate] [datetime] NULL,
	[LogUser] [int] NULL,
	[LogObs] [nvarchar](max) NULL,
	[idinvoice] [int] NOT NULL,
	[idcurrency] [int] NOT NULL,
	[idcompany] [int] NOT NULL,
	[invoicedescription] [varchar](max) NULL,
	[invoicecreatedby] [int] NOT NULL,
	[invoicecreactiondate] [datetime] NOT NULL,
	[invoiceupdatedby] [int] NOT NULL,
	[invoicetupdateon] [datetime] NOT NULL,
	[invoiceapplicationdate] [datetime] NOT NULL,
	[invoicenumber] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


/****** Object:  Table [vta].[tblinvoiceditemLog]    Script Date: 16/05/2019 09:43:13 a. m. ******/

CREATE TABLE [vta].[tblinvoiceditemLog](
	[LogId] [int] IDENTITY(1,1) NOT NULL,
	[LogDate] [datetime] NULL,
	[LogUser] [int] NULL,
	[LogObs] [nvarchar](max) NULL,
	[idinvoiceitem] [int] NOT NULL,
	[idinvoice] [int] NOT NULL,
	[idaccl4] [int] NOT NULL,
	[idinvoiceitemstatus] [int] NOT NULL,
	[Iduser] [int] NOT NULL,
	[invoiceditemsubtotal] [numeric](14, 2) NULL,
	[description] [nvarchar](250) NULL,
	[deleted] [bit] NULL,
	[invoiceditemistax] [bit] NOT NULL,
	[invoiceditemtaxes] [numeric](14, 2) NOT NULL,
	[invoiceditembillidentifier] [nvarchar](50) NULL,
	[idsupplier] [int] NOT NULL,
	[invoiceditemsupplierother] [nvarchar](250) NULL,
	[invoiceditemothertaxesammount] [numeric](14, 2) NOT NULL,
	[invoiceditemsingleexibitionpayment] [bit] NOT NULL,
	[idbudgettype] [int] NOT NULL,
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


--------------------------------------------------------------------------------------------------------------------------------
------------------------------- C U R R E N C Y   E X C H A N G E --------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE [vta].[tblcurrencyexchange](
	[idcurrencyexchange] [int] IDENTITY(1,1) NOT NULL,
	[currencyexchangefrom] [int] NOT NULL,
	[currencyexchangeto] [int] NOT NULL,
	[currencyexchangerate] [numeric](14, 2) NOT NULL,
	[currencyexchangedate] [datetime] NOT NULL,
 CONSTRAINT [PK_CURRENCY_EXCHANGE] PRIMARY KEY CLUSTERED 
(
	[idcurrencyexchange] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

--------------------------------------------------------------------------------------------------------------------------------
------------------------------- ACCOUNTL4 METHODSPAYMENT ------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------

/****** Object:  Table [vta].[tblaccl4paymenmethods]    Script Date: 16/05/2019 09:43:10 a. m. ******/

CREATE TABLE [vta].[tblaccl4paymenmethods](
	[idaccl4paymenmethods] [int] IDENTITY(1,1) NOT NULL,
	[idpaymentmethod] [int] NULL,
	[idaccountl4] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idAccl4Paymenmethods] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


ALTER TABLE [vta].[tblaccl4paymenmethods]  WITH CHECK ADD  CONSTRAINT [fk.tblaccl4paymenmethods_tblaccountsl4_1] FOREIGN KEY([idaccountl4])
REFERENCES [vta].[tblaccountsl4] ([idaccountl4])
GO
ALTER TABLE [vta].[tblaccl4paymenmethods] CHECK CONSTRAINT [fk.tblaccl4paymenmethods_tblaccountsl4_1]
GO
ALTER TABLE [vta].[tblaccl4paymenmethods]  WITH CHECK ADD  CONSTRAINT [fk.tblaccl4paymenmethods_tblPaymentForms_1] FOREIGN KEY([idPaymentMethod])
REFERENCES [dbo].[tblPaymentForms] ([idPaymentForm])
GO
ALTER TABLE [vta].[tblaccl4paymenmethods] CHECK CONSTRAINT [fk.tblaccl4paymenmethods_tblPaymentForms_1]
GO