/*
   lundi 24 juin 202409:02:43
   User: 
   Server: DESKTOP-2271T3V\SQLEXPRESS
   Database: SO2M
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Utilisateurs ADD
	Photo1_data varchar(MAX) NULL
GO
ALTER TABLE dbo.Utilisateurs SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
