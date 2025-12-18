-- Schema SQL gerado para LH_PET (MySQL)
-- Ajuste nomes de database/charset conforme necessário

CREATE DATABASE IF NOT EXISTS `lh_pet_db` CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE `lh_pet_db`;

-- Tabela Cliente
CREATE TABLE IF NOT EXISTS `Cliente` (
  `ClienteID` int NOT NULL AUTO_INCREMENT,
  `Nome` varchar(80) NOT NULL,
  `CPF` varchar(80) NOT NULL,
  `Email` varchar(80) NOT NULL,
  `DataCadastro` datetime NOT NULL,
  PRIMARY KEY (`ClienteID`),
  UNIQUE KEY `UX_Cliente_CPF` (`CPF`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Tabela Animal
CREATE TABLE IF NOT EXISTS `Animal` (
  `AnimalID` int NOT NULL AUTO_INCREMENT,
  `Nome` varchar(80) NOT NULL,
  `Tipo` varchar(80) NOT NULL,
  `Sexo` varchar(80) NOT NULL,
  `Raca` varchar(80) NOT NULL,
  `Idade` varchar(80) NOT NULL,
  `DataCadastro` datetime NOT NULL,
  `ClienteID` int NOT NULL,
  PRIMARY KEY (`AnimalID`),
  KEY `FK_Animal_Cliente` (`ClienteID`),
  CONSTRAINT `FK_Animal_Cliente` FOREIGN KEY (`ClienteID`) REFERENCES `Cliente` (`ClienteID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Tabela Consulta
CREATE TABLE IF NOT EXISTS `Consulta` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ClienteID` int NOT NULL,
  `AnimalID` int NOT NULL,
  `DataHora` datetime NOT NULL,
  `Descricao` text,
  PRIMARY KEY (`Id`),
  KEY `FK_Consulta_Cliente` (`ClienteID`),
  KEY `FK_Consulta_Animal` (`AnimalID`),
  CONSTRAINT `FK_Consulta_Cliente` FOREIGN KEY (`ClienteID`) REFERENCES `Cliente` (`ClienteID`) ON DELETE CASCADE,
  CONSTRAINT `FK_Consulta_Animal` FOREIGN KEY (`AnimalID`) REFERENCES `Animal` (`AnimalID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Tabela Fornecedor
CREATE TABLE IF NOT EXISTS `Fornecedor` (
  `FornecedorID` int NOT NULL AUTO_INCREMENT,
  `Nome` varchar(120) DEFAULT NULL,
  `CNPJ` varchar(80) DEFAULT NULL,
  `Email` varchar(120) DEFAULT NULL,
  PRIMARY KEY (`FornecedorID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Tabela Users
CREATE TABLE IF NOT EXISTS `User` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Username` varchar(100) NOT NULL,
  `Email` varchar(100) NOT NULL,
  `PasswordHash` varchar(255) NOT NULL,
  `DataCadastro` datetime NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UX_User_Username` (`Username`),
  UNIQUE KEY `UX_User_Email` (`Email`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Tabelas adicionais do projeto (se houver) podem ser adicionadas aqui

-- Seed inicial (opcional)
INSERT INTO `User` (`Username`, `Email`, `PasswordHash`, `DataCadastro`)
VALUES ('admin', 'admin@local', '$2y$10$abcdefghijklmnopqrstuv', NOW())
ON DUPLICATE KEY UPDATE `Username` = `Username`;
