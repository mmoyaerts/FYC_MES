-- Suppression des tables existantes dans l'ordre inverse de création, en utilisant CASCADE pour gérer les dépendances.
-- On s'assure d'utiliser la casse minuscule pour correspondre au comportement par défaut de PostgreSQL.
DROP TABLE IF EXISTS ofversionhistory CASCADE;
DROP TABLE IF EXISTS ofenproduction CASCADE;
DROP TABLE IF EXISTS of CASCADE;
DROP TABLE IF EXISTS programmegonflage CASCADE;


-- 1. Création de la table programmeGonflage (Gère le contrôle gonflage)
CREATE TABLE programmegonflage (
    idGonflage SMALLINT PRIMARY KEY, -- Remplacé SERIAL par SMALLINT pour des IDs fixes
    labelGonflage VARCHAR(50) NOT NULL
);

-- Insertion des programmes de gonflage requis
-- J'ai remplacé les valeurs pour correspondre au SMALLINT et pour éviter les problèmes de séquences.
INSERT INTO programmegonflage (idGonflage, labelGonflage) VALUES 
(6, 'Programme 6'),
(2, 'contrôle 2');

-- 2. Création de la table Of (Ordre de Fabrication)
CREATE TABLE of (
    numeroOf SMALLINT PRIMARY KEY,
    apiChargementDechargement SMALLINT,
    apiRobotGonflage SMALLINT,
    idGonflage SMALLINT, -- Utiliser SMALLINT pour correspondre à la clé étrangère
    CONSTRAINT fk_idGonflage FOREIGN KEY (idGonflage)
        REFERENCES programmegonflage (idGonflage)
        ON UPDATE CASCADE
        ON DELETE SET NULL
);

-- 3. Création de la table ofEnProduction (OF en cours d'exécution)
CREATE TABLE ofenproduction (
    id SERIAL PRIMARY KEY,
    numeroOf SMALLINT NOT NULL,
    quantite INTEGER NOT NULL,
    dateDeLancement TIMESTAMP DEFAULT NOW(),
    isValid BOOLEAN
);

-- 4. Création de la table ofVersionHistory (Historique des versions)
CREATE TABLE ofversionhistory (
    id SERIAL PRIMARY KEY,
    numeroOf SMALLINT NOT NULL, 
    quantitePrecedente INTEGER NOT NULL,
    dateHistorique TIMESTAMP DEFAULT NOW() 
);


-- Insertion des 2 OF d'exemple
INSERT INTO of (numeroOf, apiChargementDechargement, apiRobotGonflage, idGonflage) 
VALUES (1, 49, 209, 2);

INSERT INTO of (numeroOf, apiChargementDechargement, apiRobotGonflage, idGonflage) 
VALUES (2, 34, 1, NULL);

-- Initialisation : L'OF 1 est considéré en cours
INSERT INTO ofenproduction (numeroOf, quantite, isValid)
VALUES (1, 500, TRUE);