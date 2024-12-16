-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Dec 15, 2024 at 07:55 PM
-- Server version: 10.4.28-MariaDB
-- PHP Version: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `asrama_puni`
--

-- --------------------------------------------------------

--
-- Table structure for table `backuphelpdesk`
--

CREATE TABLE `backuphelpdesk` (
  `HelpDeskID` int(11) NOT NULL DEFAULT 0,
  `NamaHelpDesk` varchar(100) NOT NULL,
  `Email` varchar(100) DEFAULT NULL,
  `Telepon` varchar(15) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `backuphelpdesk`
--

INSERT INTO `backuphelpdesk` (`HelpDeskID`, `NamaHelpDesk`, `Email`, `Telepon`) VALUES
(1, 'Giselma ', 'Gisel@asrama.com', '08123456789'),
(2, 'Aicel', 'Aicel@asrama.com', '08198765432');

-- --------------------------------------------------------

--
-- Table structure for table `backuppengaduanhelpdesk`
--

CREATE TABLE `backuppengaduanhelpdesk` (
  `PengaduanID` int(11) NOT NULL DEFAULT 0,
  `MahasiswaID` int(11) NOT NULL,
  `TanggalPengaduan` date NOT NULL,
  `Deskripsi` text NOT NULL,
  `StatusPengaduan` enum('Diajukan','Diproses','Selesai') DEFAULT 'Diajukan',
  `HelpDeskID` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `backuppengaduanhelpdesk`
--

INSERT INTO `backuppengaduanhelpdesk` (`PengaduanID`, `MahasiswaID`, `TanggalPengaduan`, `Deskripsi`, `StatusPengaduan`, `HelpDeskID`) VALUES
(3, 1, '2024-11-10', 'Kipas angin di kamar tidak berfungsi', 'Diajukan', NULL),
(4, 2, '2024-11-12', 'WiFi asrama lambat', 'Diajukan', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `helpdesk_0405`
--

CREATE TABLE `helpdesk_0405` (
  `HelpDeskID` int(11) NOT NULL,
  `NamaHelpDesk` varchar(100) NOT NULL,
  `Email` varchar(100) DEFAULT NULL,
  `Telepon` varchar(15) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `helpdesk_0405`
--

INSERT INTO `helpdesk_0405` (`HelpDeskID`, `NamaHelpDesk`, `Email`, `Telepon`) VALUES
(1, 'Giselma', 'giselma@asrama.com', '08123456789'),
(2, 'Aicel', 'aicel@asrama.com', '08198765432'),
(3, 'sdads', 'adsad', '332133');

-- --------------------------------------------------------

--
-- Table structure for table `kamarasrama_0405`
--

CREATE TABLE `kamarasrama_0405` (
  `KamarID` int(11) NOT NULL,
  `NamaKamar` varchar(50) NOT NULL,
  `Kapasitas` int(11) NOT NULL,
  `Tersedia` tinyint(1) DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `kamarasrama_0405`
--

INSERT INTO `kamarasrama_0405` (`KamarID`, `NamaKamar`, `Kapasitas`, `Tersedia`) VALUES
(1, 'Kamar A111', 2, 1),
(2, 'Kamar B222', 3, 1),
(3, 'Kamar C333', 4, 1);

-- --------------------------------------------------------

--
-- Table structure for table `mahasiswa_0405`
--

CREATE TABLE `mahasiswa_0405` (
  `MahasiswaID` int(11) NOT NULL,
  `NamaMahasiswa` varchar(100) NOT NULL,
  `NIM` varchar(15) NOT NULL,
  `Email` varchar(100) DEFAULT NULL,
  `Telepon` varchar(15) DEFAULT NULL,
  `Alamat` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `mahasiswa_0405`
--

INSERT INTO `mahasiswa_0405` (`MahasiswaID`, `NamaMahasiswa`, `NIM`, `Email`, `Telepon`, `Alamat`) VALUES
(2, 'Lukmin Alkhairi', '987654321', 'siti@student.telkom.ac.id', '08198765432', 'Jl. Merdeka No. 5'),
(3, 'ariq', '123', 'ariq@gmail.com', '123', 'disini'),
(5, 'Mas Funy', '800808080', 'puni@gmail.com', '902801928', 'jalan disana'),
(6, 'Tes ID', '123123123', 'asdawas', '12313323', 'asdasd');

-- --------------------------------------------------------

--
-- Table structure for table `pengaduanhelpdesk_0405`
--

CREATE TABLE `pengaduanhelpdesk_0405` (
  `PengaduanID` int(11) NOT NULL,
  `MahasiswaID` int(11) NOT NULL,
  `TanggalPengaduan` date NOT NULL,
  `Deskripsi` text NOT NULL,
  `StatusPengaduan` varchar(20) DEFAULT 'Diajukan',
  `HelpDeskID` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `pengaduanhelpdesk_0405`
--

INSERT INTO `pengaduanhelpdesk_0405` (`PengaduanID`, `MahasiswaID`, `TanggalPengaduan`, `Deskripsi`, `StatusPengaduan`, `HelpDeskID`) VALUES
(1, 2, '2024-12-16', 'sdadsaasa', 'Diajukan', 2),
(2, 2, '2024-11-12', 'WiFi lambat', 'Diajukan', 2),
(3, 5, '2024-10-10', 'kasksakads', 'kkakadk', 2),
(6, 2, '2024-10-10', 'kkass', 'kaksda', 1),
(7, 2, '2024-10-10', 'asads', 'dsads', 1);

-- --------------------------------------------------------

--
-- Table structure for table `reservasikamar_0405`
--

CREATE TABLE `reservasikamar_0405` (
  `ReservasiID` int(11) NOT NULL,
  `MahasiswaID` int(11) NOT NULL,
  `KamarID` int(11) NOT NULL,
  `TanggalMasuk` date NOT NULL,
  `TanggalKeluar` date DEFAULT NULL,
  `StatusReservasi` enum('Aktif','Selesai','Dibatalkan') DEFAULT 'Aktif'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `reservasikamar_0405`
--

INSERT INTO `reservasikamar_0405` (`ReservasiID`, `MahasiswaID`, `KamarID`, `TanggalMasuk`, `TanggalKeluar`, `StatusReservasi`) VALUES
(4, 2, 2, '2024-07-01', '2024-08-01', 'Aktif'),
(5, 3, 1, '2023-10-10', '2923-10-10', 'Aktif');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `helpdesk_0405`
--
ALTER TABLE `helpdesk_0405`
  ADD PRIMARY KEY (`HelpDeskID`);

--
-- Indexes for table `kamarasrama_0405`
--
ALTER TABLE `kamarasrama_0405`
  ADD PRIMARY KEY (`KamarID`);

--
-- Indexes for table `mahasiswa_0405`
--
ALTER TABLE `mahasiswa_0405`
  ADD PRIMARY KEY (`MahasiswaID`),
  ADD UNIQUE KEY `NIM` (`NIM`);

--
-- Indexes for table `pengaduanhelpdesk_0405`
--
ALTER TABLE `pengaduanhelpdesk_0405`
  ADD PRIMARY KEY (`PengaduanID`),
  ADD KEY `HelpDeskID` (`HelpDeskID`);

--
-- Indexes for table `reservasikamar_0405`
--
ALTER TABLE `reservasikamar_0405`
  ADD PRIMARY KEY (`ReservasiID`),
  ADD KEY `MahasiswaID` (`MahasiswaID`),
  ADD KEY `KamarID` (`KamarID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `helpdesk_0405`
--
ALTER TABLE `helpdesk_0405`
  MODIFY `HelpDeskID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `kamarasrama_0405`
--
ALTER TABLE `kamarasrama_0405`
  MODIFY `KamarID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `mahasiswa_0405`
--
ALTER TABLE `mahasiswa_0405`
  MODIFY `MahasiswaID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `pengaduanhelpdesk_0405`
--
ALTER TABLE `pengaduanhelpdesk_0405`
  MODIFY `PengaduanID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `reservasikamar_0405`
--
ALTER TABLE `reservasikamar_0405`
  MODIFY `ReservasiID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `pengaduanhelpdesk_0405`
--
ALTER TABLE `pengaduanhelpdesk_0405`
  ADD CONSTRAINT `pengaduanhelpdesk_0405_ibfk_1` FOREIGN KEY (`HelpDeskID`) REFERENCES `helpdesk_0405` (`HelpDeskID`) ON DELETE SET NULL ON UPDATE CASCADE;

--
-- Constraints for table `reservasikamar_0405`
--
ALTER TABLE `reservasikamar_0405`
  ADD CONSTRAINT `reservasikamar_0405_ibfk_1` FOREIGN KEY (`MahasiswaID`) REFERENCES `mahasiswa_0405` (`MahasiswaID`) ON DELETE CASCADE,
  ADD CONSTRAINT `reservasikamar_0405_ibfk_2` FOREIGN KEY (`KamarID`) REFERENCES `kamarasrama_0405` (`KamarID`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
