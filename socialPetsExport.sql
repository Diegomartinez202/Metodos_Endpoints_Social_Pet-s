-- MySQL dump 10.13  Distrib 8.0.36, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: socialpets
-- ------------------------------------------------------
-- Server version	8.3.0

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
-- Table structure for table `album`
--

DROP TABLE IF EXISTS `album`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `album` (
  `id_album` int NOT NULL AUTO_INCREMENT,
  `titulo` varchar(200) DEFAULT NULL,
  `contenido` varchar(500) DEFAULT NULL,
  `usuario_id_usuario` int NOT NULL,
  `nivel_id_nivel` int NOT NULL,
  `creado_en` datetime DEFAULT NULL,
  PRIMARY KEY (`id_album`),
  UNIQUE KEY `id_album_UNIQUE` (`id_album`),
  KEY `fk_album_usuario1_idx` (`usuario_id_usuario`),
  KEY `fk_album_nivel1_idx` (`nivel_id_nivel`),
  CONSTRAINT `fk_album_nivel1` FOREIGN KEY (`nivel_id_nivel`) REFERENCES `nivel` (`id_nivel`) ON DELETE RESTRICT,
  CONSTRAINT `fk_album_usuario1` FOREIGN KEY (`usuario_id_usuario`) REFERENCES `usuario` (`id_usuario`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `album`
--

LOCK TABLES `album` WRITE;
/*!40000 ALTER TABLE `album` DISABLE KEYS */;
/*!40000 ALTER TABLE `album` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `amigos`
--

DROP TABLE IF EXISTS `amigos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `amigos` (
  `id_amigos` int NOT NULL AUTO_INCREMENT,
  `aceptado` tinyint NOT NULL,
  `leido` tinyint NOT NULL,
  `creado_en` date NOT NULL,
  `perfil_id_perfil` int DEFAULT NULL,
  `usuario_id_usuario` int NOT NULL,
  PRIMARY KEY (`id_amigos`),
  UNIQUE KEY `id_amigos_UNIQUE` (`id_amigos`),
  KEY `fk_amigos_perfil1_idx` (`perfil_id_perfil`),
  KEY `fk_amigos_usuario1_idx` (`usuario_id_usuario`),
  CONSTRAINT `fk_amigos_perfil1` FOREIGN KEY (`perfil_id_perfil`) REFERENCES `perfil` (`id_perfil`) ON DELETE RESTRICT,
  CONSTRAINT `fk_amigos_usuario1` FOREIGN KEY (`usuario_id_usuario`) REFERENCES `usuario` (`id_usuario`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `amigos`
--

LOCK TABLES `amigos` WRITE;
/*!40000 ALTER TABLE `amigos` DISABLE KEYS */;
/*!40000 ALTER TABLE `amigos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `categoria`
--

DROP TABLE IF EXISTS `categoria`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `categoria` (
  `id_categoria` int NOT NULL,
  `nombre` varchar(100) NOT NULL,
  `publicaciones_id_publicacion` int NOT NULL,
  PRIMARY KEY (`id_categoria`),
  UNIQUE KEY `id_categoria_UNIQUE` (`id_categoria`),
  KEY `fk_categoria_publicaciones1_idx` (`publicaciones_id_publicacion`),
  CONSTRAINT `fk_categoria_publicaciones1` FOREIGN KEY (`publicaciones_id_publicacion`) REFERENCES `publicaciones` (`id_publicacion`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `categoria`
--

LOCK TABLES `categoria` WRITE;
/*!40000 ALTER TABLE `categoria` DISABLE KEYS */;
/*!40000 ALTER TABLE `categoria` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ciudad`
--

DROP TABLE IF EXISTS `ciudad`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ciudad` (
  `id_ciudad` int NOT NULL AUTO_INCREMENT,
  `name` varchar(45) DEFAULT NULL,
  `prefijo` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id_ciudad`),
  UNIQUE KEY `id_ciudad_UNIQUE` (`id_ciudad`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ciudad`
--

LOCK TABLES `ciudad` WRITE;
/*!40000 ALTER TABLE `ciudad` DISABLE KEYS */;
/*!40000 ALTER TABLE `ciudad` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `comentario`
--

DROP TABLE IF EXISTS `comentario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `comentario` (
  `id_comentario` int NOT NULL AUTO_INCREMENT,
  `tipo_id` int NOT NULL,
  `referencia_id` int NOT NULL,
  `usuario_id_usuario` int NOT NULL,
  `contenido` text NOT NULL,
  `comentario_id_comentario` int NOT NULL,
  `creado_en` datetime NOT NULL,
  PRIMARY KEY (`id_comentario`),
  UNIQUE KEY `id_comentario_UNIQUE` (`id_comentario`),
  KEY `fk_comentario_usuario1_idx` (`usuario_id_usuario`),
  KEY `fk_comentario_comentario1_idx` (`comentario_id_comentario`),
  CONSTRAINT `fk_comentario_comentario1` FOREIGN KEY (`comentario_id_comentario`) REFERENCES `comentario` (`id_comentario`) ON DELETE RESTRICT,
  CONSTRAINT `fk_comentario_usuario1` FOREIGN KEY (`usuario_id_usuario`) REFERENCES `usuario` (`id_usuario`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `comentario`
--

LOCK TABLES `comentario` WRITE;
/*!40000 ALTER TABLE `comentario` DISABLE KEYS */;
/*!40000 ALTER TABLE `comentario` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `conversacion`
--

DROP TABLE IF EXISTS `conversacion`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `conversacion` (
  `id_conversacion` int NOT NULL AUTO_INCREMENT,
  `creado_en` datetime NOT NULL,
  `usuario_id_usuario` int NOT NULL,
  PRIMARY KEY (`id_conversacion`),
  UNIQUE KEY `id_conversacion_UNIQUE` (`id_conversacion`),
  KEY `fk_conversacion_usuario1_idx` (`usuario_id_usuario`),
  CONSTRAINT `fk_conversacion_usuario1` FOREIGN KEY (`usuario_id_usuario`) REFERENCES `usuario` (`id_usuario`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `conversacion`
--

LOCK TABLES `conversacion` WRITE;
/*!40000 ALTER TABLE `conversacion` DISABLE KEYS */;
/*!40000 ALTER TABLE `conversacion` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `corazon`
--

DROP TABLE IF EXISTS `corazon`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `corazon` (
  `id_corazon` int NOT NULL AUTO_INCREMENT,
  `tipo_id` int NOT NULL,
  `referencia_id` int NOT NULL,
  `usuario_id_usuario` int NOT NULL,
  `creado_en` datetime NOT NULL,
  PRIMARY KEY (`id_corazon`),
  UNIQUE KEY `id_corazon_UNIQUE` (`id_corazon`),
  KEY `fk_corazon_usuario1_idx` (`usuario_id_usuario`),
  CONSTRAINT `fk_corazon_usuario1` FOREIGN KEY (`usuario_id_usuario`) REFERENCES `usuario` (`id_usuario`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `corazon`
--

LOCK TABLES `corazon` WRITE;
/*!40000 ALTER TABLE `corazon` DISABLE KEYS */;
/*!40000 ALTER TABLE `corazon` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `grupo`
--

DROP TABLE IF EXISTS `grupo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `grupo` (
  `id_grupo` int NOT NULL AUTO_INCREMENT,
  `imagen` varchar(200) NOT NULL,
  `titulo` varchar(200) NOT NULL,
  `descripcion` varchar(500) NOT NULL,
  `usuario_id_usuario` int NOT NULL,
  `estado` int NOT NULL,
  `creado_en` datetime NOT NULL,
  PRIMARY KEY (`id_grupo`),
  UNIQUE KEY `id_grupo_UNIQUE` (`id_grupo`),
  KEY `fk_grupo_usuario1_idx` (`usuario_id_usuario`),
  CONSTRAINT `fk_grupo_usuario1` FOREIGN KEY (`usuario_id_usuario`) REFERENCES `usuario` (`id_usuario`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `grupo`
--

LOCK TABLES `grupo` WRITE;
/*!40000 ALTER TABLE `grupo` DISABLE KEYS */;
/*!40000 ALTER TABLE `grupo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `imagen`
--

DROP TABLE IF EXISTS `imagen`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `imagen` (
  `id_imagen` int NOT NULL AUTO_INCREMENT,
  `fuente` varchar(255) NOT NULL,
  `titulo` varchar(255) NOT NULL,
  `contenido` varchar(500) NOT NULL,
  `creado_en` datetime NOT NULL,
  `usuario_id_usuario` int NOT NULL,
  `nivel_id_nivel` int NOT NULL,
  `album_id_album` int NOT NULL,
  PRIMARY KEY (`id_imagen`),
  UNIQUE KEY `id_imagen_UNIQUE` (`id_imagen`),
  KEY `fk_imagen_usuario1_idx` (`usuario_id_usuario`),
  KEY `fk_imagen_nivel1_idx` (`nivel_id_nivel`),
  KEY `fk_imagen_album1_idx` (`album_id_album`),
  CONSTRAINT `fk_imagen_album1` FOREIGN KEY (`album_id_album`) REFERENCES `album` (`id_album`) ON DELETE RESTRICT,
  CONSTRAINT `fk_imagen_nivel1` FOREIGN KEY (`nivel_id_nivel`) REFERENCES `nivel` (`id_nivel`) ON DELETE RESTRICT,
  CONSTRAINT `fk_imagen_usuario1` FOREIGN KEY (`usuario_id_usuario`) REFERENCES `usuario` (`id_usuario`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `imagen`
--

LOCK TABLES `imagen` WRITE;
/*!40000 ALTER TABLE `imagen` DISABLE KEYS */;
/*!40000 ALTER TABLE `imagen` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `mensage`
--

DROP TABLE IF EXISTS `mensage`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `mensage` (
  `id_mensage` int NOT NULL AUTO_INCREMENT,
  `contenido` text,
  `usuario_id_usuario` int NOT NULL,
  `conversacion_id_conversacion` int NOT NULL,
  `creado_en` datetime DEFAULT NULL,
  `leido` tinyint DEFAULT NULL,
  PRIMARY KEY (`id_mensage`),
  UNIQUE KEY `id_mensage_UNIQUE` (`id_mensage`),
  KEY `fk_mensage_conversacion1_idx` (`conversacion_id_conversacion`),
  KEY `fk_mensage_usuario1_idx` (`usuario_id_usuario`),
  CONSTRAINT `fk_mensage_conversacion1` FOREIGN KEY (`conversacion_id_conversacion`) REFERENCES `conversacion` (`id_conversacion`) ON DELETE RESTRICT,
  CONSTRAINT `fk_mensage_usuario1` FOREIGN KEY (`usuario_id_usuario`) REFERENCES `usuario` (`id_usuario`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `mensage`
--

LOCK TABLES `mensage` WRITE;
/*!40000 ALTER TABLE `mensage` DISABLE KEYS */;
/*!40000 ALTER TABLE `mensage` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `nivel`
--

DROP TABLE IF EXISTS `nivel`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `nivel` (
  `id_nivel` int NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  PRIMARY KEY (`id_nivel`),
  UNIQUE KEY `id_nivel_UNIQUE` (`id_nivel`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `nivel`
--

LOCK TABLES `nivel` WRITE;
/*!40000 ALTER TABLE `nivel` DISABLE KEYS */;
/*!40000 ALTER TABLE `nivel` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `notificacion`
--

DROP TABLE IF EXISTS `notificacion`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `notificacion` (
  `id_notificacion` int NOT NULL AUTO_INCREMENT,
  `tipo_id` int NOT NULL,
  `referencia_id` int NOT NULL,
  `leido` tinyint NOT NULL,
  `creado_en` datetime NOT NULL,
  `usuario_id_usuario` int NOT NULL,
  PRIMARY KEY (`id_notificacion`),
  UNIQUE KEY `id_notificacion_UNIQUE` (`id_notificacion`),
  KEY `fk_notificacion_usuario1_idx` (`usuario_id_usuario`),
  CONSTRAINT `fk_notificacion_usuario1` FOREIGN KEY (`usuario_id_usuario`) REFERENCES `usuario` (`id_usuario`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `notificacion`
--

LOCK TABLES `notificacion` WRITE;
/*!40000 ALTER TABLE `notificacion` DISABLE KEYS */;
/*!40000 ALTER TABLE `notificacion` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `perfil`
--

DROP TABLE IF EXISTS `perfil`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `perfil` (
  `id_perfil` int NOT NULL AUTO_INCREMENT,
  `dia_cumpleaños` date DEFAULT NULL,
  `genero` varchar(45) DEFAULT NULL,
  `ciudad_id_ciudad` int NOT NULL,
  `imagen_perfil` varchar(45) DEFAULT NULL,
  `imagen_portada` varchar(45) DEFAULT NULL,
  `titulo` varchar(45) DEFAULT NULL,
  `biografia` varchar(45) DEFAULT NULL,
  `me_gusta` varchar(45) DEFAULT NULL,
  `no_me_gusta` varchar(45) DEFAULT NULL,
  `direccion` varchar(45) DEFAULT NULL,
  `numero_telefono` int DEFAULT NULL,
  `correo_electronico_id` varchar(45) DEFAULT NULL,
  `nivel_id_nivel` int NOT NULL,
  `sentimiento_id_sentimiento` int NOT NULL,
  `usuario_id_usuario` int NOT NULL,
  PRIMARY KEY (`id_perfil`),
  UNIQUE KEY `id_perfil_UNIQUE` (`id_perfil`),
  KEY `fk_perfil_ciudad1_idx` (`ciudad_id_ciudad`),
  KEY `fk_perfil_nivel1_idx` (`nivel_id_nivel`),
  KEY `fk_perfil_sentimiento1_idx` (`sentimiento_id_sentimiento`),
  KEY `fk_perfil_usuario1_idx` (`usuario_id_usuario`),
  CONSTRAINT `fk_perfil_ciudad1` FOREIGN KEY (`ciudad_id_ciudad`) REFERENCES `ciudad` (`id_ciudad`) ON DELETE RESTRICT,
  CONSTRAINT `fk_perfil_nivel1` FOREIGN KEY (`nivel_id_nivel`) REFERENCES `nivel` (`id_nivel`) ON DELETE RESTRICT,
  CONSTRAINT `fk_perfil_sentimiento1` FOREIGN KEY (`sentimiento_id_sentimiento`) REFERENCES `sentimiento` (`id_sentimiento`) ON DELETE RESTRICT,
  CONSTRAINT `fk_perfil_usuario1` FOREIGN KEY (`usuario_id_usuario`) REFERENCES `usuario` (`id_usuario`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `perfil`
--

LOCK TABLES `perfil` WRITE;
/*!40000 ALTER TABLE `perfil` DISABLE KEYS */;
/*!40000 ALTER TABLE `perfil` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `publicacion imagen`
--

DROP TABLE IF EXISTS `publicacion imagen`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `publicacion imagen` (
  `id_publicacion_imagen` int NOT NULL AUTO_INCREMENT,
  `imagen_id_imagen` int NOT NULL,
  `publicaciones_id_publicacion` int NOT NULL,
  PRIMARY KEY (`id_publicacion_imagen`),
  UNIQUE KEY `id_publicacion_imagen_UNIQUE` (`id_publicacion_imagen`),
  KEY `fk_publicacion imagen_imagen1_idx` (`imagen_id_imagen`),
  KEY `fk_publicacion imagen_publicaciones1_idx` (`publicaciones_id_publicacion`),
  CONSTRAINT `fk_publicacion imagen_imagen1` FOREIGN KEY (`imagen_id_imagen`) REFERENCES `imagen` (`id_imagen`) ON DELETE RESTRICT,
  CONSTRAINT `fk_publicacion imagen_publicaciones1` FOREIGN KEY (`publicaciones_id_publicacion`) REFERENCES `publicaciones` (`id_publicacion`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `publicacion imagen`
--

LOCK TABLES `publicacion imagen` WRITE;
/*!40000 ALTER TABLE `publicacion imagen` DISABLE KEYS */;
/*!40000 ALTER TABLE `publicacion imagen` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `publicaciones`
--

DROP TABLE IF EXISTS `publicaciones`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `publicaciones` (
  `id_publicacion` int NOT NULL AUTO_INCREMENT,
  `titulo` varchar(500) NOT NULL,
  `contenido` text NOT NULL,
  `latitud` double NOT NULL,
  `longitud` double NOT NULL,
  `comenzar_en` datetime NOT NULL,
  `finalizado_en` datetime NOT NULL,
  `tipo_receptor_id` int NOT NULL,
  `referencia_autor_id` int NOT NULL,
  `referencia_receptor_id` int NOT NULL,
  `nivel_id_nivel` int NOT NULL,
  `tipo_publicacion_id` int NOT NULL,
  `creado_en` datetime NOT NULL,
  PRIMARY KEY (`id_publicacion`),
  UNIQUE KEY `id_publicacion_UNIQUE` (`id_publicacion`),
  KEY `fk_publicaciones_nivel1_idx` (`nivel_id_nivel`),
  CONSTRAINT `fk_publicaciones_nivel1` FOREIGN KEY (`nivel_id_nivel`) REFERENCES `nivel` (`id_nivel`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `publicaciones`
--

LOCK TABLES `publicaciones` WRITE;
/*!40000 ALTER TABLE `publicaciones` DISABLE KEYS */;
/*!40000 ALTER TABLE `publicaciones` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `recuperacion`
--

DROP TABLE IF EXISTS `recuperacion`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `recuperacion` (
  `id_recuperacion` int NOT NULL AUTO_INCREMENT,
  `usuario_id_usuario` int NOT NULL,
  `codigo` varchar(45) NOT NULL,
  `en_uso` tinyint DEFAULT NULL,
  `creado_en` datetime DEFAULT NULL,
  PRIMARY KEY (`id_recuperacion`),
  UNIQUE KEY `usuario_id_usuario_UNIQUE` (`usuario_id_usuario`),
  UNIQUE KEY `id_recuperacion_UNIQUE` (`id_recuperacion`),
  KEY `fk_recuperacion_usuario1_idx` (`usuario_id_usuario`),
  CONSTRAINT `fk_recuperacion_usuario1` FOREIGN KEY (`usuario_id_usuario`) REFERENCES `usuario` (`id_usuario`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `recuperacion`
--

LOCK TABLES `recuperacion` WRITE;
/*!40000 ALTER TABLE `recuperacion` DISABLE KEYS */;
/*!40000 ALTER TABLE `recuperacion` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sentimiento`
--

DROP TABLE IF EXISTS `sentimiento`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sentimiento` (
  `id_sentimiento` int NOT NULL AUTO_INCREMENT,
  `name` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id_sentimiento`),
  UNIQUE KEY `id_sentimiento_UNIQUE` (`id_sentimiento`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sentimiento`
--

LOCK TABLES `sentimiento` WRITE;
/*!40000 ALTER TABLE `sentimiento` DISABLE KEYS */;
/*!40000 ALTER TABLE `sentimiento` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usuario`
--

DROP TABLE IF EXISTS `usuario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `usuario` (
  `id_usuario` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(50) NOT NULL,
  `apellido` varchar(50) NOT NULL,
  `nombre_usuario` varchar(50) NOT NULL,
  `correo_electronico` varchar(50) NOT NULL,
  `contrasena_hash` varchar(256) NOT NULL,
  `activo` tinyint NOT NULL,
  `creado_en` datetime NOT NULL,
  PRIMARY KEY (`id_usuario`),
  UNIQUE KEY `idusuario_UNIQUE` (`id_usuario`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuario`
--

LOCK TABLES `usuario` WRITE;
/*!40000 ALTER TABLE `usuario` DISABLE KEYS */;
/*!40000 ALTER TABLE `usuario` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-02-23 21:45:51
