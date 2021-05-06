/***************************************************
Author: Ruben Magaña Alvarado
Date:   22 Agosto 2019
Description: 
	1.Face dos, 
	2.cambios profundos a VTA subir archivos, 
	3.logs complementarios 1,  
	4.conciliaciones 
***************************************************/


Insert into vta.tblattachments (attachmentname,attachmentactive)
select
attachmentname,attachmentactive
from VTH.vta.tblattachments



select*from vta.tblinvoicecomments
select*from vta.tblincomecomments

select*from vta.tblinvoiceattach
select*from vta.tblincomeattach
select*from VTH.vta.tblinvoiceattach


select*from tblUploadedFileSources
select*from tbluploadedfilesparent
select*from tbluploadedfilesUsers

select*from tblworkrole

select*from tblusers

select*from vta.tblprofilesaccounts

ALTER TABLE [dbo].[tblusers]  drop  CONSTRAINT [fk_tblprofilesaccounts_tblusers_1]

ALTER TABLE [dbo].[tblusers]  WITH CHECK ADD  CONSTRAINT [FK_VTA_tblusers_tblprofilesaccounts_1] FOREIGN KEY([idprofileaccount])
REFERENCES [vta].[tblprofilesaccounts] ([idprofileaccount])
GO
ALTER TABLE [dbo].[tblusers] CHECK CONSTRAINT[FK_VTA_tblusers_tblprofilesaccounts_1]
GO

