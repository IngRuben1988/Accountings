

CREATE TABLE [vta].[tbluserbacount](
[iduserbacount] [int] IDENTITY(1,1) NOT NULL,
[iduser] [int] NOT NULL,
[idbaccount] [int] NOT NULL,
[userbacountcreatedby] [int] NOT NULL,
[userbacountcreationdate] [datetime] NOT NULL,
[userbacountactive] [bit] NOT NULL,
CONSTRAINT [tbluserbacount_PK] PRIMARY KEY CLUSTERED 
(
[idUserbacount] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [vta].[tbluserbacount] WITH CHECK ADD CONSTRAINT [FK_tbluserbacount_tblbankaccount] FOREIGN KEY([idBAccount])
REFERENCES [vta].[tblbankaccount] ([idBAccount])
GO

ALTER TABLE [vta].[tbluserbacount] CHECK CONSTRAINT [FK_tbluserbacount_tblbankaccount]
GO

ALTER TABLE [vta].[tbluserbacount] WITH CHECK ADD CONSTRAINT [FK_tbluserbacount_tblusers] FOREIGN KEY([idUser])
REFERENCES [dbo].[tblusers] ([idUser])
GO

ALTER TABLE [vta].[tbluserbacount] CHECK CONSTRAINT [FK_tbluserbacount_tblusers]
GO




CREATE TABLE [vta].[tblcompaniesuppliers](
	[idcompanysupplier] [int] IDENTITY(1,1) NOT NULL,
	[idcompany] [int] NOT NULL,
	[idsupplier] [int] NOT NULL,
 CONSTRAINT [PK_idCompanySupplier] PRIMARY KEY CLUSTERED 
(
	[idcompanysupplier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [vta].[tblcompaniesuppliers]  WITH CHECK ADD  CONSTRAINT [fk_tblcompaniessupplier_tblsupplier_1] FOREIGN KEY([idsupplier])
REFERENCES [vta].[tblsupplier] ([idsupplier])
GO

ALTER TABLE [vta].[tblcompaniesuppliers] CHECK CONSTRAINT [fk_tblcompaniessupplier_tblsupplier_1]
GO

ALTER TABLE [vta].[tblcompaniesuppliers]  WITH CHECK ADD  CONSTRAINT [fk_tblcompinessupplier_tblcompanies_1] FOREIGN KEY([idcompany])
REFERENCES [vta].[tblcompanies] ([idcompany])
GO

ALTER TABLE [vta].[tblcompaniesuppliers] CHECK CONSTRAINT [fk_tblcompinessupplier_tblcompanies_1]
GO
