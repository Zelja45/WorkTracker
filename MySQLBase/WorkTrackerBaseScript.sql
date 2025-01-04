-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema worktracker
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema worktracker
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `worktracker` DEFAULT CHARACTER SET utf8 ;
USE `worktracker` ;

-- -----------------------------------------------------
-- Table `worktracker`.`User`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `worktracker`.`User` (
  `Username` VARCHAR(45) NOT NULL,
  `Password` VARCHAR(255) NOT NULL,
  `Name` VARCHAR(45) NOT NULL,
  `Surname` VARCHAR(45) NOT NULL,
  `Email` VARCHAR(45) NOT NULL,
  `PhoneNumber` VARCHAR(45) NOT NULL,
  `IsActive` TINYINT NOT NULL,
  `CreatedAt` DATE NOT NULL,
  `Image` BLOB NULL,
  PRIMARY KEY (`Username`),
  UNIQUE INDEX `Username_UNIQUE` (`Username` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `worktracker`.`Sector`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `worktracker`.`Sector` (
  `idSector` INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(45) NOT NULL,
  `Description` VARCHAR(256) NULL,
  `HourlyRate` VARCHAR(45) NOT NULL,
  `OvertimeHourlyRate` VARCHAR(45) NOT NULL,
  `DailyHoursNorm` VARCHAR(45) NOT NULL,
  `WeeklyHoursNorm` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idSector`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `worktracker`.`Worker`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `worktracker`.`Worker` (
  `Username` VARCHAR(45) NOT NULL,
  `OvertimeRateWorkerSpecific` DECIMAL(5,2) NULL,
  `HourlyRateWorkerSpecific` DECIMAL(5,2) NULL,
  `idSector` INT NOT NULL,
  PRIMARY KEY (`Username`),
  INDEX `fk_Worker_Sector1_idx` (`idSector` ASC) VISIBLE,
  INDEX `fk_Worker_User1_idx` (`Username` ASC) VISIBLE,
  CONSTRAINT `fk_Worker_Sector1`
    FOREIGN KEY (`idSector`)
    REFERENCES `worktracker`.`Sector` (`idSector`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Worker_User1`
    FOREIGN KEY (`Username`)
    REFERENCES `worktracker`.`User` (`Username`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `worktracker`.`WorkSession`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `worktracker`.`WorkSession` (
  `idSession` INT NOT NULL AUTO_INCREMENT,
  `StartTime` DATETIME NOT NULL,
  `EndTime` DATETIME NULL,
  `WorkedHours` TIME NULL,
  `Status` TINYINT NOT NULL,
  `WorkerUsername` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idSession`),
  INDEX `fk_WorkSession_Worker1_idx` (`WorkerUsername` ASC) VISIBLE,
  CONSTRAINT `fk_WorkSession_Worker1`
    FOREIGN KEY (`WorkerUsername`)
    REFERENCES `worktracker`.`Worker` (`Username`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `worktracker`.`Manager`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `worktracker`.`Manager` (
  `Username` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`Username`),
  INDEX `fk_Manager_User1_idx` (`Username` ASC) VISIBLE,
  CONSTRAINT `fk_Manager_User1`
    FOREIGN KEY (`Username`)
    REFERENCES `worktracker`.`User` (`Username`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `worktracker`.`Admin`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `worktracker`.`Admin` (
  `Username` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`Username`),
  CONSTRAINT `fk_Admin_User1`
    FOREIGN KEY (`Username`)
    REFERENCES `worktracker`.`User` (`Username`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `worktracker`.`SectorManager`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `worktracker`.`SectorManager` (
  `idSector` INT NOT NULL,
  `ManagerUsername` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idSector`, `ManagerUsername`),
  INDEX `fk_Sector_has_Manager_Manager1_idx` (`ManagerUsername` ASC) VISIBLE,
  INDEX `fk_Sector_has_Manager_Sector1_idx` (`idSector` ASC) VISIBLE,
  CONSTRAINT `fk_Sector_has_Manager_Sector1`
    FOREIGN KEY (`idSector`)
    REFERENCES `worktracker`.`Sector` (`idSector`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Sector_has_Manager_Manager1`
    FOREIGN KEY (`ManagerUsername`)
    REFERENCES `worktracker`.`Manager` (`Username`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `worktracker`.`Task`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `worktracker`.`Task` (
  `idTask` INT NOT NULL,
  `Title` VARCHAR(100) NOT NULL,
  `Description` TEXT NULL,
  `Status` TINYINT NOT NULL,
  `Priority` INT NOT NULL,
  `CreatedAt` DATETIME NOT NULL,
  `DueDate` DATETIME NOT NULL,
  `WorkerUsername` VARCHAR(45) NOT NULL,
  `Progress` INT NOT NULL DEFAULT 0,
  PRIMARY KEY (`idTask`),
  INDEX `fk_Task_Worker1_idx` (`WorkerUsername` ASC) VISIBLE,
  CONSTRAINT `fk_Task_Worker1`
    FOREIGN KEY (`WorkerUsername`)
    REFERENCES `worktracker`.`Worker` (`Username`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `worktracker`.`TODOList`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `worktracker`.`TODOList` (
  `idTODOList` INT NOT NULL AUTO_INCREMENT,
  `Title` VARCHAR(100) NOT NULL,
  `Description` TEXT NULL,
  `Worker_Username` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idTODOList`),
  INDEX `fk_TODOList_Worker1_idx` (`Worker_Username` ASC) VISIBLE,
  CONSTRAINT `fk_TODOList_Worker1`
    FOREIGN KEY (`Worker_Username`)
    REFERENCES `worktracker`.`Worker` (`Username`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `worktracker`.`TODOListItem`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `worktracker`.`TODOListItem` (
  `idTODOListItem` INT NOT NULL AUTO_INCREMENT,
  `idTODOList` INT NOT NULL,
  `Content` TEXT NOT NULL,
  `Checked` TINYINT NOT NULL,
  PRIMARY KEY (`idTODOListItem`, `idTODOList`),
  INDEX `fk_TODOListItem_TODOList1_idx` (`idTODOList` ASC) VISIBLE,
  CONSTRAINT `fk_TODOListItem_TODOList1`
    FOREIGN KEY (`idTODOList`)
    REFERENCES `worktracker`.`TODOList` (`idTODOList`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `worktracker`.`PauseLog`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `worktracker`.`PauseLog` (
  `StartTime` DATETIME NOT NULL,
  `EndTime` DATETIME NULL,
  `idWorkSession` INT NOT NULL,
  PRIMARY KEY (`idWorkSession`),
  CONSTRAINT `fk_PauseLog_WorkSession1`
    FOREIGN KEY (`idWorkSession`)
    REFERENCES `worktracker`.`WorkSession` (`idSession`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `worktracker`.`WorkSessionReport`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `worktracker`.`WorkSessionReport` (
  `WorkSession_idSession` INT NOT NULL,
  `WorkedHours` TIME NOT NULL,
  `OvertimeHours` TIME NOT NULL,
  `HourlyRate` DECIMAL(5,2) NOT NULL,
  `OvertimeHourlyRate` DECIMAL(5,2) NOT NULL,
  PRIMARY KEY (`WorkSession_idSession`),
  CONSTRAINT `fk_WorkSessionReport_WorkSession1`
    FOREIGN KEY (`WorkSession_idSession`)
    REFERENCES `worktracker`.`WorkSession` (`idSession`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
