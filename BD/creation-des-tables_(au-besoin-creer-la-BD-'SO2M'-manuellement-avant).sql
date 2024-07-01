-- Vérifier si la base de données existe avant de la supprimer
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'SO2M') DROP DATABASE SO2M;
-- Créer la base de données SO2M
CREATE DATABASE SO2M;
-- Utiliser la base de données SO2M
USE SO2M;

-- Créer la table CritereRecherche
CREATE TABLE CritereRecherches (
    ID INT PRIMARY KEY IDENTITY,
    AgeMin INT,
    AgeMax INT,
    NiveauAcademique NVARCHAR(255),
    Genre NVARCHAR(50),
    OrientationS NVARCHAR(50),
    Ville NVARCHAR(255),
    RayonRecherche INT
);

-- Créer la table ModelPsycologique
CREATE TABLE ModelPsycologiques (
    ID INT PRIMARY KEY IDENTITY,
    TitreModel NVARCHAR(255),
    AuteurModel NVARCHAR(255)
);

-- Créer la table Utilisateur
CREATE TABLE Utilisateurs (
    ID INT PRIMARY KEY IDENTITY,
    Nom NVARCHAR(255),
    Prenom NVARCHAR(255),
    Genre NVARCHAR(50),
    NiveauAcademique NVARCHAR(255),
    Age INT,
    OrientationS NVARCHAR(50),
    Courriel NVARCHAR(255),
    Username NVARCHAR(255) UNIQUE,
    MotDePasse NVARCHAR(255),
    Est_Active BIT,
    DateCreationProfil DATE,
    CritereRechercheID INT,
    ModelPsycologiqueID INT,
    FOREIGN KEY (CritereRechercheID) REFERENCES CritereRecherches(ID),
    FOREIGN KEY (ModelPsycologiqueID) REFERENCES ModelPsycologiques(ID)
);




-- Créer la table Photo
CREATE TABLE Photos (
    ID INT PRIMARY KEY IDENTITY,
    URL NVARCHAR(255),
    DateAjout DATE,
    PhotoProfil BIT,
    UtilisateurID INT,
    FOREIGN KEY (UtilisateurID) REFERENCES Utilisateurs(ID)
);

-- Créer la table Axe
CREATE TABLE Axes (
    ID INT PRIMARY KEY IDENTITY,
    TitreAxe NVARCHAR(255),
    Score INT,
    ModelPsycologiqueID INT,
    FOREIGN KEY (ModelPsycologiqueID) REFERENCES ModelPsycologiques(ID)
);

-- Créer la table intermédiaire UtilisateurPhoto pour la relation Utilisateur-Photo
CREATE TABLE UtilisateurPhotos (
    UtilisateurID INT,
    PhotoID INT,
    PRIMARY KEY (UtilisateurID, PhotoID),
    FOREIGN KEY (UtilisateurID) REFERENCES Utilisateurs(ID),
    FOREIGN KEY (PhotoID) REFERENCES Photos(ID)
);

-- Créer la table intermédiaire UtilisateurFavoris pour la relation Utilisateur-Favoris
CREATE TABLE UtilisateurFavoriss (
    UtilisateurID INT,
    FavoriID INT,
    PRIMARY KEY (UtilisateurID, FavoriID),
    FOREIGN KEY (UtilisateurID) REFERENCES Utilisateurs(ID),
    FOREIGN KEY (FavoriID) REFERENCES Utilisateurs(ID)
);

-- Créer la table intermédiaire UtilisateurMatchs pour la relation Utilisateur-Matchs
CREATE TABLE UtilisateurMatchss (
    UtilisateurID INT,
    MatchID INT,
    PRIMARY KEY (UtilisateurID, MatchID),
    FOREIGN KEY (UtilisateurID) REFERENCES Utilisateurs(ID),
    FOREIGN KEY (MatchID) REFERENCES Utilisateurs(ID)
);



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



CREATE TABLE Posts (
    Id INT PRIMARY KEY IDENTITY,
    UserId INT,
    Username NVARCHAR(50),
    Content NVARCHAR(MAX),
    DateCreated DATETIME,
    FOREIGN KEY (UserId) REFERENCES Utilisateurs(Id)
 )












