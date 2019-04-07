CREATE DATABASE  IF NOT EXISTS `nodejs_api` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `nodejs_api`;
-- MySQL dump 10.13  Distrib 5.7.12, for Win32 (AMD64)
--
-- Host: localhost    Database: nodejs_api
-- ------------------------------------------------------
-- Server version	5.7.25-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `faculties`
--

DROP TABLE IF EXISTS `faculties`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `faculties` (
  `facultyid` varchar(30) CHARACTER SET utf8 NOT NULL,
  `facultyname` varchar(30) CHARACTER SET utf8 DEFAULT NULL,
  `facultyroom` varchar(30) CHARACTER SET utf8 DEFAULT NULL,
  `facultyemail` varchar(30) DEFAULT NULL,
  `facultyphone` varchar(30) CHARACTER SET utf8 DEFAULT NULL,
  PRIMARY KEY (`facultyid`),
  UNIQUE KEY `facultyname_UNIQUE` (`facultyname`),
  UNIQUE KEY `facultyroom_UNIQUE` (`facultyroom`),
  UNIQUE KEY `facultyphone_UNIQUE` (`facultyphone`),
  UNIQUE KEY `facultyemail_UNIQUE` (`facultyemail`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `faculties`
--

LOCK TABLES `faculties` WRITE;
/*!40000 ALTER TABLE `faculties` DISABLE KEYS */;
INSERT INTO `faculties` VALUES ('CNTT','Công nghệ thông tin','I52','info@fit.hcmus.edu.vn','088354266 '),('DC','Địa chất','C14','nkhoang@hcmus.edu.vn','0838355271'),('DTVT','Điện tử viễn thông','E106','tnhvy@hcmus.edu.vn','0838356464'),('HH','Hóa học','B101','chemistry@hcmus.edu.vn','02838355270'),('KHCNVL','Khoa học và Công nghệ Vật liệu','B202','mst.hcmus@gmail.com','02838350831'),('MT','Môi trường','C16','environment@hcmus.edu.vn','0838304379'),('SHCNSH','Sinh học - Công nghệ Sinh học','C12','fbb@hcmus.edu.vn','0838355273'),('TTH','Toán - Tin học','F009','nhhai@hcmus.edu.vn','02873089899'),('VLVLKT','Vật lý - Vật lý Kỹ thuật','C11','htnhan@hcmus.edu.vn','08 8355272');
/*!40000 ALTER TABLE `faculties` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `products`
--

DROP TABLE IF EXISTS `products`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `products` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) DEFAULT NULL,
  `color` varchar(255) DEFAULT NULL,
  `price` decimal(10,0) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `products`
--

LOCK TABLES `products` WRITE;
/*!40000 ALTER TABLE `products` DISABLE KEYS */;
INSERT INTO `products` VALUES (1,'Iphone XS','Black',30000000),(2,'Samsung S9','White',24000000),(3,'Oppo F5','Red',7000000),(4,'HTC','yellow',999999),(5,'Oppo F5','Red',7000000),(6,'Oppo F5','Red',7000000),(7,'Oppdso F5','Red',7000000);
/*!40000 ALTER TABLE `products` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `users` (
  `userid` varchar(30) CHARACTER SET utf8 NOT NULL,
  `password` varchar(30) CHARACTER SET utf8 NOT NULL,
  `firstname` varchar(30) CHARACTER SET utf8 NOT NULL,
  `lastname` varchar(30) CHARACTER SET utf8 NOT NULL,
  `email` varchar(30) CHARACTER SET utf8 DEFAULT NULL,
  `faculty` varchar(30) CHARACTER SET utf8 DEFAULT NULL,
  PRIMARY KEY (`userid`),
  KEY `faculty_idx` (`faculty`),
  CONSTRAINT `users_faculties` FOREIGN KEY (`faculty`) REFERENCES `faculties` (`facultyid`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES ('1612421','tqnghia','Nguyễn Ngọc','Nghĩa','tonystrinh@gmail.com','DC'),('1612422','221455338','Trịnh Quang','Nghĩa','tonystrinh@gmail.com','CNTT'),('1612423','tqnghia','Trịnh Quang ','Nghĩa','tonystrinh@gmail.com','DTVT'),('1612431','tqnghia','Trần Bá','Ngọc','tonystrinh@gmail.com','DC'),('1612555','tqnghia','trịnh','nghĩa','eaer@gad.com','SHCNSH'),('admin','admin','Trịnh Quang','Nghĩa','tonystrinh@gmail.com','CNTT');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-04-07 19:26:58
