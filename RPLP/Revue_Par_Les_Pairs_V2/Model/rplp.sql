
IF EXISTS (
		SELECT *
		FROM sys.databases
		WHERE name = 'rplp'
		)
	BEGIN
		use master;
		ALTER DATABASE rplp SET OFFLINE WITH ROLLBACK IMMEDIATE;
		ALTER DATABASE rplp SET ONLINE;
		DROP DATABASE rplp;
	END;
	GO

IF NOT EXISTS (
		SELECT *
		FROM sys.databases
		WHERE name = 'rplp'
		)
BEGIN
	CREATE DATABASE rplp;
END;
GO

USE rplp;

IF EXISTS (
		SELECT *
		FROM sysobjects
		WHERE name = 'cours'
		)
BEGIN
	DROP TABLE cours;
END;
GO

IF EXISTS (
		SELECT *
		FROM sysobjects
		WHERE name = 'professeur'
		)
BEGIN
	DROP TABLE professeur;
END;
GO

IF EXISTS (
		SELECT *
		FROM sysobjects
		WHERE name = 'etudiant'
		)
BEGIN
	DROP TABLE etudiant;
END;
GO

IF EXISTS (
		SELECT *
		FROM sysobjects
		WHERE name = 'inscription'
		)
BEGIN
	DROP TABLE inscription;
END;
GO
CREATE TABLE solution (
	Solution_id INT IDENTITY(1,1) primary key ,
	Liens VARCHAR(255)
);

CREATE TABLE commentaire (
	Commentaire_id INT IDENTITY(1,1) PRIMARY KEY,
	Texte VARCHAR(255),
	Numero_ligne INT not null,
	Severite VARCHAR(255)
);

CREATE TABLE etudiant(
	Etudiant_id VARCHAR(255) PRIMARY KEY
	,Nom VARCHAR(255) NOT NULL
	,Prenom VARCHAR(255) NOT NULL
	);

CREATE TABLE professeur (
	Professeur_id INT IDENTITY(1,1) PRIMARY KEY
	,Nom VARCHAR(255) NOT NULL
	,Prenom VARCHAR(255) NOT NULL
	);

CREATE TABLE correction (
	Correction_id INT IDENTITY(1,1) PRIMARY KEY
	,Solution_id INT NOT NULL FOREIGN KEY REFERENCES solution(Solution_id)
	,Etudiant_id VARCHAR(255) NOT NULL FOREIGN KEY REFERENCES etudiant(Etudiant_id)
	,Finaliser BIT NOT NULL
	);

CREATE TABLE cours (
	Cours_id INT IDENTITY(1,1) PRIMARY KEY
	,Nom VARCHAR(255) NOT NULL
	);

CREATE TABLE travail (
	Travail_id INT IDENTITY(1,1) PRIMARY KEY,
	Nom VARCHAR(255),
	DateDeRemise DATE,
	NombresDeRevues INT NOT NULL
);

ALTER TABLE solution
ADD Travail_id INT NOT NULL FOREIGN KEY REFERENCES travail(Travail_id);

ALTER TABLE commentaire
ADD Etudiant_id VARCHAR(255) NOT NULL FOREIGN KEY REFERENCES etudiant(Etudiant_id),
	Solution_id int not null foreign key references solution(Solution_id);

ALTER TABLE cours
ADD Professeur_id int not null foreign key references professeur(Professeur_id);

ALTER TABLE travail
ADD Cours_id int not null foreign key references cours(Cours_id);

create table inscription (
	Inscription_id int IDENTITY(1,1) primary key,
	Etudiant_id VARCHAR(255) not null foreign key references etudiant(Etudiant_id),
	Cours_id int not null foreign key references cours(Cours_id)
);