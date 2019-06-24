CREATE DATABASE  IF NOT EXISTS `nodejs_api` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `nodejs_api`;
-- MySQL dump 10.13  Distrib 8.0.15, for Win64 (x86_64)
--
-- Host: localhost    Database: nodejs_api
-- ------------------------------------------------------
-- Server version	8.0.15

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
 SET NAMES utf8 ;
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
 SET character_set_client = utf8mb4 ;
CREATE TABLE `faculties` (
  `facultyid` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `facultyname` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `facultyroom` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `facultyemail` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `facultyphone` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  PRIMARY KEY (`facultyid`),
  UNIQUE KEY `facultyname_UNIQUE` (`facultyname`),
  UNIQUE KEY `facultyroom_UNIQUE` (`facultyroom`),
  UNIQUE KEY `facultyphone_UNIQUE` (`facultyphone`),
  UNIQUE KEY `facultyemail_UNIQUE` (`facultyemail`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `faculties`
--

LOCK TABLES `faculties` WRITE;
/*!40000 ALTER TABLE `faculties` DISABLE KEYS */;
INSERT INTO `faculties` VALUES ('CNTT','Công nghệ thông tin','I52','info@fit.hcmus.edu.vn','088354266 '),('DC','Địa chất học','C14','nkhoang@hcmus.edu.vn','0838355271'),('DTVT','Điện tử viễn thông','E106','tnhvy@hcmus.edu.vn','0838356464'),('HH','Hóa học','B101','chemistry@hcmus.edu.vn','02838355270'),('KHCNVL','Khoa học và Công nghệ Vật liệu','B202','mst.hcmus@gmail.com','02838350831'),('MT','Môi trường và tự nhiên','C16','environment@hcmus.edu.vn','0838304379'),('SHCNSH','Sinh học - Công nghệ Sinh học','C12','fbb@hcmus.edu.vn','0838355273'),('TTH','Toán - Tin học','F009','nhhai@hcmus.edu.vn','02873089899'),('VLVLKT','Vật lý - Vật lý Kỹ thuật','C11','htnhan@hcmus.edu.vn','08 8355272');
/*!40000 ALTER TABLE `faculties` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `registers`
--

DROP TABLE IF EXISTS `registers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `registers` (
  `userid` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `subjectid` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `status` varchar(30) DEFAULT NULL,
  PRIMARY KEY (`userid`,`subjectid`),
  KEY `FK_REGISTERS_SUBJECTS_idx` (`subjectid`),
  CONSTRAINT `FK_REGISTERS_SUBJECTS` FOREIGN KEY (`subjectid`) REFERENCES `subjects` (`subjectid`),
  CONSTRAINT `FK_REGISTERS_USERS` FOREIGN KEY (`userid`) REFERENCES `users` (`userid`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `registers`
--

LOCK TABLES `registers` WRITE;
/*!40000 ALTER TABLE `registers` DISABLE KEYS */;
INSERT INTO `registers` VALUES ('1612431','CSDL',NULL),('1612431','HDH',NULL),('1612431','LTUDW',NULL),('1612431','MMT',NULL),('1612431','PPT',NULL),('1612431','PTTKPM',NULL),('1612431','THTH',NULL),('1612431','TRR',NULL);
/*!40000 ALTER TABLE `registers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `schedules`
--

DROP TABLE IF EXISTS `schedules`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `schedules` (
  `subjectid` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `day` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `room` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `starttime` time DEFAULT NULL,
  `finishtime` time DEFAULT NULL,
  PRIMARY KEY (`subjectid`,`day`),
  CONSTRAINT `FK_SCHEDULE_SUBJECT` FOREIGN KEY (`subjectid`) REFERENCES `subjects` (`subjectid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `schedules`
--

LOCK TABLES `schedules` WRITE;
/*!40000 ALTER TABLE `schedules` DISABLE KEYS */;
INSERT INTO `schedules` VALUES ('CSDL','Thứ hai','E302','07:00:00','09:20:00'),('CSDL','Thứ năm','C201','15:10:00','17:00:00'),('HDH','Thứ tư','C42','09:20:00','11:50:00'),('KNM','Thứ hai','C33','06:20:00','09:00:00'),('KNM','Thứ tư','E201','12:30:00','16:00:00'),('LTUDJ','Thứ năm','C32','14:00:00','17:50:00'),('LTUDW','Thứ hai','C22','09:20:00','11:50:00'),('MMT','Thứ bảy','C42','12:30:00','15:10:00'),('NMLT','Thứ hai','D203','07:30:00','09:20:00'),('NMLT','Thứ tư','C201','06:20:00','08:50:00'),('PPT','Thứ năm','C32','12:30:00','15:00:00'),('PPT','Thứ sáu','TH-Toan','09:20:00','11:50:00'),('PTTKPM','Thứ tư','C32','09:20:00','11:50:00'),('THTH','Thứ ba','C33','15:10:00','17:00:00'),('TRR','Thứ ba','E304','06:20:00','08:50:00'),('XSTK','Thứ tư','F203','07:30:00','11:00:00');
/*!40000 ALTER TABLE `schedules` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `subjects`
--

DROP TABLE IF EXISTS `subjects`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `subjects` (
  `subjectid` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `subjectname` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `credit` int(11) DEFAULT NULL,
  `teachername` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `termindex` varchar(3) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `termyear` varchar(9) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `faculty` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  PRIMARY KEY (`subjectid`),
  KEY `FK_SUBJECTS_FACULTIES_idx` (`faculty`),
  KEY `FK_SUBJECTS_TERMS_idx` (`termindex`,`termyear`),
  CONSTRAINT `FK_SUBJECTS_FACULTIES` FOREIGN KEY (`faculty`) REFERENCES `faculties` (`facultyid`),
  CONSTRAINT `FK_SUBJECTS_TERMS` FOREIGN KEY (`termindex`, `termyear`) REFERENCES `terms` (`termindex`, `termyear`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `subjects`
--

LOCK TABLES `subjects` WRITE;
/*!40000 ALTER TABLE `subjects` DISABLE KEYS */;
INSERT INTO `subjects` VALUES ('CSDL','Cơ sở dữ liệu',4,'Phạm Thị Bạch Huệ','I','2018-2019','CNTT'),('HDH','Hệ điều hành Linux',4,'Trần Thái Sơn','I','2018-2019','CNTT'),('KNM','Kỹ năng mềm',3,'Huỳnh Thị Bảo Trân','III','2018-2019','DC'),('LTUDJ','Lập trình ứng dụng Java',4,'Trần Văn Lượng','II','2018-2019','CNTT'),('LTUDW','Lập trình ứng dụng Web',4,'Ngô Ngọc Đăng Khoa','II','2018-2019','CNTT'),('MMT','Mạng máy tính',4,'Trần Văn Tâm','I','2018-2019','CNTT'),('NMLT','Nhập môn lập trình',3,'Lê Văn Nghĩa','I','2018-2019','CNTT'),('PPT','Phương pháp tính',4,'Lê Văn Kiệt','II','2018-2019','TTH'),('PTTKPM','Phân tích thiết kế phần mềm',4,'Trương Văn Khiết','II','2018-2019','CNTT'),('THTH','Toán học tổ hợp',4,'Lê Văn Hợp','I','2018-2019','TTH'),('TRR','Toán rời rạc',4,'Lê Văn Luyện','I','2018-2019','TTH'),('XSTK','Xác suất thống kê',4,'Trần Văn Huệ','I','2018-2019','TTH');
/*!40000 ALTER TABLE `subjects` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tasks`
--

DROP TABLE IF EXISTS `tasks`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `tasks` (
  `taskid` varchar(30) NOT NULL,
  `taskname` varchar(45) DEFAULT NULL,
  `userid` varchar(30) DEFAULT NULL,
  `subjectid` varchar(30) DEFAULT NULL,
  `progress` int(11) DEFAULT NULL,
  `deadline` datetime DEFAULT NULL,
  `description` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`taskid`),
  KEY `FK_TASKS_USERS_idx` (`userid`),
  KEY `FK_TASKS_SUBJECTS_idx` (`subjectid`),
  CONSTRAINT `FK_TASKS_SUBJECTS` FOREIGN KEY (`subjectid`) REFERENCES `subjects` (`subjectid`),
  CONSTRAINT `FK_TASKS_USERS` FOREIGN KEY (`userid`) REFERENCES `users` (`userid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tasks`
--

LOCK TABLES `tasks` WRITE;
/*!40000 ALTER TABLE `tasks` DISABLE KEYS */;
INSERT INTO `tasks` VALUES ('LTWF','Lập trình web cuối kì','1612431','LTUDW',33,'2019-06-24 00:00:00','Bài tập làm trên máy, có video demo, sử dụng github'),('PPT1','Phương pháp lặp','1612431','PPT',14,'2019-06-24 00:00:00','Bài tập làm trên giấy'),('PTTKP','Đồ án PTTKPM','1612431','PTTKPM',42,'2019-06-25 00:00:00',''),('TTH','Bài tập toán tổ hợp','1612431','THTH',20,'2019-06-25 23:55:00','Bài tập làm trên giấy');
/*!40000 ALTER TABLE `tasks` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `terms`
--

DROP TABLE IF EXISTS `terms`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `terms` (
  `termindex` varchar(3) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `termyear` varchar(9) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `begindate` date NOT NULL,
  `enddate` date NOT NULL,
  PRIMARY KEY (`termindex`,`termyear`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `terms`
--

LOCK TABLES `terms` WRITE;
/*!40000 ALTER TABLE `terms` DISABLE KEYS */;
INSERT INTO `terms` VALUES ('I','2018-2019','2018-08-18','2018-12-25'),('II','2018-2019','2019-01-11','2019-05-24'),('III','2018-2019','2019-06-24','2019-08-30');
/*!40000 ALTER TABLE `terms` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `users` (
  `userid` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `password` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `firstname` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `lastname` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `email` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `faculty` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `userType` varchar(30) CHARACTER SET utf8 COLLATE utf8_bin DEFAULT NULL,
  PRIMARY KEY (`userid`),
  KEY `faculty_idx` (`faculty`),
  CONSTRAINT `users_faculties` FOREIGN KEY (`faculty`) REFERENCES `faculties` (`facultyid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES ('1612422','123456','Trịnh Quang','Nghĩa','tonystrinh@gmail.com','CNTT','student'),('1612431','123456','Trần Bá','Ngọc','tonystrinh@gmail.com','CNTT','student'),('admin','admin','Ngô Ngọc Đăng','Khoa','tonystrinh@gmail.com','CNTT','admin');
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

-- Dump completed on 2019-06-24 18:01:38
