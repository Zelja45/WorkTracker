CREATE DATABASE  IF NOT EXISTS `worktracker` /*!40100 DEFAULT CHARACTER SET utf8mb3 */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `worktracker`;
-- MySQL dump 10.13  Distrib 8.0.36, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: worktracker
-- ------------------------------------------------------
-- Server version	8.0.36

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `pauselog`
--

DROP TABLE IF EXISTS `pauselog`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `pauselog` (
  `StartTime` datetime NOT NULL,
  `EndTime` datetime DEFAULT NULL,
  `idWorkSession` int NOT NULL,
  PRIMARY KEY (`idWorkSession`,`StartTime`),
  CONSTRAINT `fk_PauseLog_WorkSession1` FOREIGN KEY (`idWorkSession`) REFERENCES `worksession` (`idSession`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pauselog`
--

LOCK TABLES `pauselog` WRITE;
/*!40000 ALTER TABLE `pauselog` DISABLE KEYS */;
INSERT INTO `pauselog` VALUES ('2025-01-30 18:41:24','2025-01-30 18:41:25',3),('2025-01-30 18:41:29','2025-01-30 18:41:31',3),('2025-01-30 18:41:36','2025-01-30 18:41:39',3),('2025-01-30 18:44:50','2025-01-30 18:44:52',4);
/*!40000 ALTER TABLE `pauselog` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sector`
--

DROP TABLE IF EXISTS `sector`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sector` (
  `idSector` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) NOT NULL,
  `Description` text,
  `HourlyRate` decimal(10,0) NOT NULL,
  `OvertimeHourlyRate` decimal(10,0) NOT NULL,
  `DailyHoursNorm` int NOT NULL,
  `WeeklyHoursNorm` int NOT NULL,
  PRIMARY KEY (`idSector`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sector`
--

LOCK TABLES `sector` WRITE;
/*!40000 ALTER TABLE `sector` DISABLE KEYS */;
INSERT INTO `sector` VALUES (1,'Sektor1','Opis sektora 1 za radnike koji rade u njemu',20,25,8,40),(2,'Sektor2','Opis sektora 2 za radnike',30,40,7,35);
/*!40000 ALTER TABLE `sector` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sectormanager`
--

DROP TABLE IF EXISTS `sectormanager`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sectormanager` (
  `idSector` int NOT NULL,
  `ManagerUsername` varchar(45) NOT NULL,
  PRIMARY KEY (`idSector`,`ManagerUsername`),
  KEY `fk_Sector_has_Manager_Sector1_idx` (`idSector`),
  KEY `fk_SectorManager_User1_idx` (`ManagerUsername`),
  CONSTRAINT `fk_Sector_has_Manager_Sector1` FOREIGN KEY (`idSector`) REFERENCES `sector` (`idSector`),
  CONSTRAINT `fk_SectorManager_User1` FOREIGN KEY (`ManagerUsername`) REFERENCES `user` (`Username`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sectormanager`
--

LOCK TABLES `sectormanager` WRITE;
/*!40000 ALTER TABLE `sectormanager` DISABLE KEYS */;
INSERT INTO `sectormanager` VALUES (1,'jovan'),(2,'jovan'),(2,'marko');
/*!40000 ALTER TABLE `sectormanager` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `task`
--

DROP TABLE IF EXISTS `task`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `task` (
  `idTask` int NOT NULL AUTO_INCREMENT,
  `Title` varchar(100) NOT NULL,
  `Description` text,
  `Status` tinyint NOT NULL,
  `Priority` int NOT NULL,
  `CreatedAt` datetime NOT NULL,
  `DueDate` datetime NOT NULL,
  `Progress` int NOT NULL DEFAULT '0',
  `WorkerUsername` varchar(45) NOT NULL,
  `Pinned` tinyint NOT NULL,
  PRIMARY KEY (`idTask`),
  KEY `fk_Task_User1_idx` (`WorkerUsername`),
  CONSTRAINT `fk_Task_User1` FOREIGN KEY (`WorkerUsername`) REFERENCES `user` (`Username`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `task`
--

LOCK TABLES `task` WRITE;
/*!40000 ALTER TABLE `task` DISABLE KEYS */;
INSERT INTO `task` VALUES (1,'Zadatak1','Opis zadatka1',0,1,'2025-01-30 18:28:01','2025-02-02 16:59:00',0,'radnik',0),(2,'Zadatak2','Opis zadatka 2',0,2,'2025-01-30 18:28:23','2025-01-31 16:00:00',0,'radnik',0),(3,'Zadatak 3','Opis zadatka3',0,0,'2025-01-30 18:28:47','2025-02-01 16:01:00',0,'nikola',0),(4,'Zadatak4','Opis zadatka4',0,2,'2025-01-30 18:29:12','2025-02-28 00:00:00',0,'nikola',0),(5,'Zadatak 5','Opis zadatka 5',1,1,'2025-01-30 18:29:33','2025-02-03 17:25:00',33,'radnik',1);
/*!40000 ALTER TABLE `task` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `todolist`
--

DROP TABLE IF EXISTS `todolist`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `todolist` (
  `idTODOList` int NOT NULL AUTO_INCREMENT,
  `Title` varchar(100) NOT NULL,
  `IsSelected` tinyint NOT NULL,
  `WorkerUsername` varchar(45) NOT NULL,
  PRIMARY KEY (`idTODOList`),
  KEY `fk_TODOList_User1_idx` (`WorkerUsername`),
  CONSTRAINT `fk_TODOList_User1` FOREIGN KEY (`WorkerUsername`) REFERENCES `user` (`Username`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `todolist`
--

LOCK TABLES `todolist` WRITE;
/*!40000 ALTER TABLE `todolist` DISABLE KEYS */;
INSERT INTO `todolist` VALUES (1,'TODO lista 1',0,'radnik'),(2,'TODO lista 2',1,'radnik');
/*!40000 ALTER TABLE `todolist` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `todolistitem`
--

DROP TABLE IF EXISTS `todolistitem`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `todolistitem` (
  `idTODOListItem` int NOT NULL AUTO_INCREMENT,
  `idTODOList` int NOT NULL,
  `Content` text NOT NULL,
  `Checked` tinyint NOT NULL,
  PRIMARY KEY (`idTODOListItem`,`idTODOList`),
  KEY `fk_TODOListItem_TODOList1_idx` (`idTODOList`),
  CONSTRAINT `fk_TODOListItem_TODOList1` FOREIGN KEY (`idTODOList`) REFERENCES `todolist` (`idTODOList`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `todolistitem`
--

LOCK TABLES `todolistitem` WRITE;
/*!40000 ALTER TABLE `todolistitem` DISABLE KEYS */;
INSERT INTO `todolistitem` VALUES (1,1,'Element 1',1),(2,1,'Element 2',0),(3,1,'Element 3',1),(4,2,'Element 1',0);
/*!40000 ALTER TABLE `todolistitem` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user` (
  `Username` varchar(45) NOT NULL,
  `Password` varchar(255) NOT NULL,
  `Name` varchar(45) NOT NULL,
  `Surname` varchar(45) NOT NULL,
  `Email` varchar(45) NOT NULL,
  `PhoneNumber` varchar(45) NOT NULL,
  `IsActive` tinyint NOT NULL,
  `CreatedAt` date NOT NULL,
  `Image` blob,
  `AccountType` varchar(45) NOT NULL,
  `OvertimeRateWorkerSpecific` decimal(10,0) DEFAULT NULL,
  `HourlyRateWorkerSpecific` decimal(10,0) DEFAULT NULL,
  `idSector` int DEFAULT NULL,
  PRIMARY KEY (`Username`),
  UNIQUE KEY `Username_UNIQUE` (`Username`),
  KEY `fk_User_Sector1_idx` (`idSector`),
  CONSTRAINT `fk_User_Sector1` FOREIGN KEY (`idSector`) REFERENCES `sector` (`idSector`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES ('admin','admin123@','Admin','Adminkovic','admin@gmail.com','065786543',1,'2025-01-30',NULL,'admin',NULL,NULL,NULL),('jovan','lozinka123@','Jovan','Jovanovic','jovan@gmail.com','065432211',1,'2025-01-30',NULL,'manager',NULL,NULL,NULL),('marko','lozinka123@','Marko','Marković','marko@gmil.com','065432112',1,'2025-01-30',NULL,'manager',NULL,NULL,NULL),('nikola','lozinka123@','Nikola','Nikolić','nikola@gmail.com','065432123',1,'2025-01-30',NULL,'worker',NULL,NULL,1),('radnik','lozinka123@','Radnik','Radić','radnik@gmail.com','065212345',1,'2025-01-30',NULL,'worker',NULL,NULL,2);
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `worksession`
--

DROP TABLE IF EXISTS `worksession`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `worksession` (
  `idSession` int NOT NULL AUTO_INCREMENT,
  `StartTime` datetime NOT NULL,
  `EndTime` datetime DEFAULT NULL,
  `WorkedHours` time DEFAULT NULL,
  `Status` tinyint NOT NULL,
  `WorkerUsername` varchar(45) NOT NULL,
  PRIMARY KEY (`idSession`),
  KEY `fk_WorkSession_User1_idx` (`WorkerUsername`),
  CONSTRAINT `fk_WorkSession_User1` FOREIGN KEY (`WorkerUsername`) REFERENCES `user` (`Username`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `worksession`
--

LOCK TABLES `worksession` WRITE;
/*!40000 ALTER TABLE `worksession` DISABLE KEYS */;
INSERT INTO `worksession` VALUES (3,'2025-01-30 18:41:16','2025-01-30 18:42:34','00:01:07',2,'radnik'),(4,'2025-01-30 18:44:23','2025-01-30 18:45:32','00:01:05',2,'radnik');
/*!40000 ALTER TABLE `worksession` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `worksessionreport`
--

DROP TABLE IF EXISTS `worksessionreport`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `worksessionreport` (
  `WorkSession_idSession` int NOT NULL,
  `WorkedHours` time NOT NULL,
  `OvertimeHours` time NOT NULL,
  `HourlyRate` decimal(10,0) NOT NULL,
  `OvertimeHourlyRate` decimal(10,0) NOT NULL,
  PRIMARY KEY (`WorkSession_idSession`),
  CONSTRAINT `fk_WorkSessionReport_WorkSession1` FOREIGN KEY (`WorkSession_idSession`) REFERENCES `worksession` (`idSession`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `worksessionreport`
--

LOCK TABLES `worksessionreport` WRITE;
/*!40000 ALTER TABLE `worksessionreport` DISABLE KEYS */;
INSERT INTO `worksessionreport` VALUES (3,'00:01:07','00:00:00',30,40),(4,'00:01:05','00:00:00',30,40);
/*!40000 ALTER TABLE `worksessionreport` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-01-30 18:53:06
