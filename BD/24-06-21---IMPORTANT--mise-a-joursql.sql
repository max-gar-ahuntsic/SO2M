/*
   vendredi 21 juin 202416:30:18
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
	Modele1Axe1 int NULL,
	Modele1Axe2 int NULL,
	Modele1Axe3 int NULL
GO
ALTER TABLE dbo.Utilisateurs SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
