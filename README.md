# SO2M - site de rencontre 

## Introduction

Ce projet vise à développer une application web permettant aux utilisateurs de s'inscrire, de se connecter, 
de publier des statuts, et de modifier leur profil,gestion de la messagerie entre les matchs, gestion de payement, 
gestion de recherche partenaire par critère de recherche, Gestion pour répondre au questionnaire
Le projet est développé en utilisant ASP.NET Core pour le backend et HTML/CSS/JavaScript pour le frontend.

## Contexte, Objectifs & Portée

### Contexte

Avec l'évolution des réseaux sociaux et des plateformes de communication, il est crucial de fournir une application permettant
aux utilisateurs de gérer leur profil et de partager des statuts avec d'autres utilisateurs.

### Objectifs

1. Permettre aux utilisateurs de s'inscrire et de se connecter.
2. Permettre aux utilisateurs de publier et de supprimer des statuts.
3. Permettre aux utilisateurs de modifier leur profil, y compris leur photo de profil.
4. Permettre aux utilisateurs de rechercher un partenaire.
5. Permettre aux utilisateurs d'envoie et recevoir des messages.
6. Assurer la sécurité et la gestion des sessions utilisateur.

### Portée

Le projet se concentre sur les fonctionnalités de base nécessaires pour une plateforme de communication sociale.

## Méthodologie

### Description des méthodes de travail

- **Backend**: Utilisation de ASP.NET Core pour développer l'API RESTful.
- **Frontend**: Utilisation de HTML, CSS, et JavaScript pour l'interface utilisateur.
- **Base de données**: Utilisation de SQL Server pour gérer les données utilisateur et les statuts.

### Technologies et outils utilisés

- **ASP.NET Core**: Framework pour le développement de l'API backend.
- **Entity Framework Core**: ORM pour l'interaction avec la base de données SQL Server.
- **HTML/CSS/JavaScript**: Technologies pour le développement frontend.
- **Visual Studio Code**: Éditeur de code pour le développement Backend.
- **Visual Studio **: Éditeur de code pour le développement Frontend.
- **SQL Server**: Système de gestion de base de données relationnelle.

### Répartition des tâches

- **Développement Backend**: Gestion des utilisateurs, sessions, s'inscrire, de se connecter, 
de publier des statuts et de modifier leur profil, recherche un partenaire, envoie et recevoir des messages et API pour les statuts.
- **Développement Frontend**: Conception de l'interface utilisateur et intégration avec l'API backend.
- **Base de données**: Conception et gestion des schémas de base de données.


### Analyse de la performance du projet

Le projet a atteint les objectifs principaux en permettant aux utilisateurs de s'inscrire, de se connecter, 
de publier des statuts et de modifier leur profil, recherche un partenaire, envoie et recevoir des messages
La gestion des sessions assure que seules les utilisateurs
authentifiés peuvent accéder aux fonctionnalités protégées.

## Conclusion

Le projet SO2M a permis de développer une application web fonctionnelle répondant aux besoins de gestion de profil 
et de publication de statuts. Les leçons apprises incluent l'importance de la gestion des sessions et de la sécurité
des données utilisateur. Des améliorations futures pourraient inclure des fonctionnalités de notification.

## Installation

### Prérequis

- .NET Core SDK
- SQL Server
- Visual Studio Code
- Visual Studio 

### Étapes d'installation

1. Clonez le dépôt : `git clone https://github.com/max-gar-ahuntsic/SO2M.git`
2. Naviguez dans le répertoire du projet : `cd so2m`
3. Configurez la base de données dans `appsettings.json`
4. Créer les tables via le script "BD/creation-des-tables.sql" (la BD 'SO2M' pourrait devoir être créée manuellement avant)
5. Lancez l'application : `dotnet run`


### Utilisation

- Accédez à l'application via `http://localhost:5160/view/wwwroot/login.html`
- Inscrivez-vous, connectez-vous et commencez à utiliser les fonctionnalités de l'application.


