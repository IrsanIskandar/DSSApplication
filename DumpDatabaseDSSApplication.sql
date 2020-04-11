
USE `mysql`;

CREATE USER 'admin'@'%' IDENTIFIED BY 'abc123*';

GRANT USAGE ON *.* TO 'admin'@'%' WITH MAX_CONNECTIONS_PER_HOUR 100 MAX_USER_CONNECTIONS 300;

FLUSH PRIVILEGES;

 GRANT ALTER, ALTER ROUTINE, CREATE, CREATE ROUTINE, CREATE TABLESPACE,
	CREATE TEMPORARY TABLES, CREATE USER, CREATE VIEW, DELETE, DROP,
	EVENT, EXECUTE, FILE, INDEX, INSERT, LOCK TABLES, PROCESS, REFERENCES,
	RELOAD, REPLICATION CLIENT, REPLICATION SLAVE, SELECT, SHOW DATABASES,
	SHOW VIEW, SHUTDOWN, SUPER, TRIGGER, UPDATE ON *.*
TO 'admin'@'%' WITH GRANT OPTION;

DROP DATABASE IF EXISTS Dss_Application;

CREATE DATABASE IF NOT EXISTS Dss_Application;

USE Dss_Application;

-- Reset Auto Increment
ALTER TABLE video_content AUTO_INCREMENT = 1;

DROP TABLE IF EXISTS Core_User;

CREATE TABLE Core_User
(
	ID INT AUTO_INCREMENT NOT NULL,
	Username VARCHAR(50) NOT NULL,
	PASSWORD VARCHAR(255) NOT NULL,
	Email VARCHAR(100) NOT NULL,
	LoginDate DATETIME NULL,
	IsVerified VARCHAR(255) NULL,
	CreateDate DATETIME NOT NULL DEFAULT(NOW()),
	IsActive TINYINT NOT NULL DEFAULT(1),

	CONSTRAINT PK_User__Core_User PRIMARY KEY (ID),
	CONSTRAINT UQ_Core_User__Username UNIQUE (Username)
);

DROP TABLE IF EXISTS Core_User_Details;

CREATE TABLE Core_User_Details
(
	ID INT AUTO_INCREMENT NOT NULL,
	UserID INT NOT NULL,
	FirstName VARCHAR(100) NOT NULL,
	LastName VARCHAR(100) NOT NULL,
	PreferredName VARCHAR(255) NULL,
	BirthDate DATE NULL,
	PhoneNumber VARCHAR(32) NOT NULL,
	CreatedByID INT NULL,
	IsActive TINYINT NOT NULL DEFAULT(1),
	CreateDate DATETIME NOT NULL DEFAULT(NOW()),

	CONSTRAINT PK_UserDetails__Core_User_Details PRIMARY KEY (ID),
	CONSTRAINT FK_UserID_Core_User_Details__Core_User FOREIGN KEY (UserID)
		REFERENCES Core_User (ID),
	CONSTRAINT FK_CreatedByID_Core_User_Details__Core_User FOREIGN KEY (CreatedByID)
		REFERENCES Core_User (ID)
);

DROP TABLE IF EXISTS Carousel_Slider;

CREATE OR REPLACE TABLE Carousel_Slider
(
	ID INT AUTO_INCREMENT NOT NULL,
	FileName VARCHAR(255) NOT NULL,
	FileSize BIGINT NULL,
	FilePath VARCHAR(255) NOT NULL,
	IsShow TINYINT DEFAULT(0),
	UserCode INT NULL,
	CreateDate DATETIME DEFAULT(NOW()),

	CONSTRAINT PK_Carousel_Slider PRIMARY KEY(ID),
	CONSTRAINT FK_Carousel_Slider__UserCode FOREIGN KEY(UserCode)
		REFERENCES Core_User(ID)
);

DROP TABLE IF EXISTS Video_Content;

CREATE OR REPLACE TABLE Video_Content
(
	ID INT AUTO_INCREMENT NOT NULL,
	FileName VARCHAR(50) NOT NULL,
	FileSize BIGINT NULL,
	FilePath VARCHAR(255) NOT NULL,
	IsShow TINYINT DEFAULT(0),
	UserCode INT NULL,
	CreateDate DATETIME DEFAULT(NOW()),

	CONSTRAINT PK_Video_Content PRIMARY KEY(ID),
	CONSTRAINT FK_Video_Content__UserCode FOREIGN KEY(UserCode)
		REFERENCES Core_User(ID)
);

DROP TABLE IF EXISTS Text_Running;

CREATE OR REPLACE TABLE Text_Running
(
	ID INT AUTO_INCREMENT NOT NULL,
	TextName VARCHAR(1024) NULL,
	UserCode INT NULL,
	IsShow TINYINT DEFAULT(0),
	CreateDate DATETIME DEFAULT(NOW()),

	CONSTRAINT PK_Text_Running PRIMARY KEY(ID),
	CONSTRAINT FK_Text_Running__UserCode FOREIGN KEY(UserCode)
		REFERENCES core_user(ID)
);

DROP TABLE IF EXISTS Server_Directory;

CREATE TABLE Server_Directory
(
	ID INT AUTO_INCREMENT NOT NULL,
	ParentPathDirectory VARCHAR(150) NOT NULL,
	directoryPath VARCHAR(255) NULL,
	IsActive TINYINT DEFAULT(1),
	CreateDate DATETIME DEFAULT(NOW()),

	CONSTRAINT PK_Server_Directory PRIMARY KEY(ID)
);

-- Create View
DROP VIEW IF EXISTS UserDetails;

CREATE VIEW UserDetails
AS
(
SELECT CRUser.ID AS UserID,
	CRUser.Username AS UserName,
	CRUserDetails.FirstName AS FirstName,
	CRUserDetails.PreferredName AS PreferredName,
	CRUserDetails.LastName AS LastName,
	CRUserDetails.BirthDate AS BirthDay,
	CRUserDetails.PhoneNumber AS PhoneNumber,
	CRUser.Email AS Email,
	CASE WHEN CRUser.IsVerified IS NOT NULL THEN 'Account Verify'
		WHEN CRUser.IsVerified IS NULL THEN 'Account Is Not Verify'
		ELSE NULL
	END AS IsVerified,
	CRUser.Password AS PASSWORD,
	CASE CRUser.IsActive
		WHEN 1 THEN 'Active'
		WHEN 0 THEN 'Not Active'
		ELSE NULL
	END AS UserActive
FROM Core_User CRUser
JOIN Core_User_Details CRUserDetails ON CRUserDetails.UserID = CRUser.ID
WHERE CRUser.IsActive = 1
);

-- Create Stored Procedure
DROP PROCEDURE IF EXISTS sp_AddUser;
DELIMITER $$
CREATE PROCEDURE sp_AddUser
(
IN p_FirstName VARCHAR(100),
IN p_LastName VARCHAR(100),
IN p_PreferredName VARCHAR(255),
IN p_PhoneNumber VARCHAR(32),
IN p_BirthDate DATE,
IN p_Username VARCHAR(50),
IN p_Email VARCHAR(100),
IN p_Password VARCHAR(128)
)
BEGIN
	INSERT INTO Core_User
	(Username, PASSWORD, Email, CreateDate)
	VALUES
	(p_Username, p_Password, p_Email, NOW());

	INSERT INTO Core_User_Details
	(UserID, FirstName, LastName, PreferredName, PhoneNumber, BirthDate, CreateDate)
	VALUES
	(LAST_INSERT_ID(), p_FirstName, p_LastName, p_PreferredName, p_PhoneNumber, p_BirthDate, NOW());

	CALL sp_GetUserDetail(LAST_INSERT_ID());
END $$
DELIMITER ;

DROP PROCEDURE IF EXISTS sp_GetUserDetail;
DELIMITER $$
CREATE PROCEDURE sp_GetUserDetail
(
	IN p_UserID INT
)
BEGIN
	SELECT UserID,
		UserName,
		FirstName,
		PreferredName,
		LastName,
		BirthDay,
		PhoneNumber,
		Email,
		IsVerified,
		PASSWORD,
		UserActive
	FROM userdetails
	WHERE UserID = p_UserID;
END $$
DELIMITER ;


DROP PROCEDURE IF EXISTS sp_CheckAccount;
DELIMITER $$
CREATE PROCEDURE sp_CheckAccount
(
	IN p_UserId INT
)
BEGIN
	SELECT COUNT(CRUser.IsVerified) IsVerified
	FROM Core_User CRUser
	WHERE ID = p_UserId AND CRUser.IsActive = 1;
END $$
DELIMITER ;

DROP PROCEDURE IF EXISTS sp_UpdateAccountActivate;
DELIMITER $$
CREATE PROCEDURE sp_UpdateAccountActivate
(
	IN p_UserId INT,
	IN p_IsVerified VARCHAR(255)
)
BEGIN
	UPDATE Core_User
		SET IsVerified = p_IsVerified
	WHERE ID = p_UserId AND IsActive = 1;
END $$
DELIMITER ;

DROP PROCEDURE IF EXISTS sp_CheckAccountVerify;
DELIMITER $$
CREATE PROCEDURE sp_CheckAccountVerify
(
	IN p_Username VARCHAR(50),
	IN p_EmailAddress VARCHAR(50),
	IN p_Password VARCHAR(255)
)
BEGIN
	SELECT COUNT(CRUser.IsVerified) IsVerified
	FROM Core_User CRUser
	WHERE UserName = COALESCE(p_Username, '') OR COALESCE(p_EmailAddress, '')
    AND PASSWORD = p_Password AND CRUser.IsActive = 1;
END $$
DELIMITER ;

DROP PROCEDURE IF EXISTS sp_CheckAccountVerifyUsername;
DELIMITER $$
CREATE PROCEDURE sp_CheckAccountVerifyUsername
(
	IN p_Username VARCHAR(50),
	IN p_Password VARCHAR(255)
)
BEGIN
	SELECT COUNT(CRUser.IsVerified) IsVerified
	FROM Core_User CRUser
	WHERE UserName = p_Username AND PASSWORD = p_Password AND CRUser.IsActive = 1;
END $$
DELIMITER ;

DROP PROCEDURE IF EXISTS sp_CheckAccountVerifyEmailAdress;
DELIMITER $$
CREATE PROCEDURE sp_CheckAccountVerifyEmailAdress
(
	IN p_EmailAddress VARCHAR(50),
	IN p_Password VARCHAR(255)
)
BEGIN
	SELECT COUNT(CRUser.IsVerified) IsVerified
	FROM Core_User CRUser
	WHERE Email = p_EmailAddress AND PASSWORD = p_Password AND CRUser.IsActive = 1;
END $$
DELIMITER ;

DROP PROCEDURE IF EXISTS sp_LoginUser;
DELIMITER $$
CREATE PROCEDURE sp_LoginUser
(
	IN p_Username VARCHAR(50),
	IN p_EmailAddress VARCHAR(50),
	IN p_Password VARCHAR(255)
)
BEGIN
	SELECT UserID,
		UserName,
		FirstName,
		PreferredName,
		LastName,
		BirthDay,
		PhoneNumber,
		Email,
		IsVerified,
		PASSWORD,
		UserActive
	FROM userdetails
	WHERE UserName = COALESCE(p_Username, '') OR Email = COALESCE(p_EmailAddress, '') AND PASSWORD = p_Password;
END $$
DELIMITER ;


DROP PROCEDURE IF EXISTS sp_UploadImages;
DELIMITER $$
CREATE PROCEDURE sp_UploadImages
(
	IN p_FileName VARCHAR(50),
	IN p_FileSize DECIMAL(18,5),
	IN p_FilePath VARCHAR(255),
	IN p_UserCode VARCHAR(255)
)
BEGIN
	INSERT INTO carousel_slider
	(FileName, FileSize, FilePath, UserCode)
	VALUES
	(p_FileName, p_FileSize, p_FilePath, p_UserCode);
END$$
DELIMITER ;


DROP PROCEDURE IF EXISTS sp_UploadVideos;
DELIMITER $$
CREATE PROCEDURE sp_UploadVideos
(
	IN p_FileName VARCHAR(50),
	IN p_FileSize BIGINT,
	IN p_FilePath VARCHAR(255),
	IN p_UserCode VARCHAR(255)
)
BEGIN
	INSERT INTO Video_Content
	(FileName, FileSize, FilePath, UserCode)
	VALUES
	(p_FileName, p_FileSize, p_FilePath, p_UserCode);
END$$
DELIMITER ;


DROP PROCEDURE IF EXISTS sp_GetListImages;
DELIMITER $$
CREATE PROCEDURE sp_GetListImages()
BEGIN
	SELECT cs.ID AS NumberImage,
		cs.FileName AS ImageName,
		cs.FileSize AS ImageSize,
		cs.FilePath AS ImagePath,
		CASE cs.IsShow
			WHEN 0 THEN 'Not Displayed'
			WHEN 1 THEN 'Displayed'
			WHEN 2 THEN 'Live Displayed'
			ELSE 'Not Configure'
		END AS IsShow,
		usrDet.PreferredName AS CreateBy,
		cs.CreateDate AS CreateDate
	FROM carousel_slider cs
	LEFT JOIN Core_User_Details usrDet ON cs.UserCode = usrDet.UserID
	ORDER BY cs.ID, cs.CreateDate DESC;
END $$
DELIMITER ;


DROP PROCEDURE IF EXISTS sp_GetListVideos;
DELIMITER $$
CREATE PROCEDURE sp_GetListVideos()
BEGIN
	SELECT vc.ID AS NumberVideo,
		vc.FileName AS VideoName,
		vc.FileSize AS VideoSize,
		vc.FilePath AS VideoPath,
		CASE vc.IsShow
			WHEN 0 THEN 'Not Displayed'
			WHEN 1 THEN 'Displayed'
		    WHEN 2 THEN 'Live Displayed'
			ELSE 'Not Configure'
		END AS IsShow,
		usrDet.PreferredName AS CreateBy,
		vc.CreateDate AS CreateDate
	FROM video_content vc
	LEFT JOIN Core_User_Details usrDet ON vc.UserCode = usrDet.UserID
	ORDER BY vc.ID, vc.CreateDate DESC;
END $$
DELIMITER ;


DROP PROCEDURE IF EXISTS sp_GetListTextRunning;
DELIMITER $$
CREATE PROCEDURE sp_GetListTextRunning()
BEGIN
	SELECT txtRun.ID AS NumberText,
		txtRun.TextName AS TextName,
		usrDet.PreferredName AS CreateBy,
		CASE txtRun.IsShow
			WHEN 0 THEN 'Not Displayed'
			WHEN 1 THEN 'Displayed'
			WHEN 2 THEN 'Live Displayed'
			ELSE 'Not Configure'
		END AS IsShow,
		txtRun.CreateDate AS CreateDate
	FROM text_running txtRun
	LEFT JOIN Core_User_Details usrDet ON txtRun.UserCode = usrDet.UserID
	ORDER BY txtRun.ID, txtRun.CreateDate DESC;
END $$
DELIMITER ;


DROP PROCEDURE IF EXISTS sp_AddNewTextRunning;
DELIMITER $$
CREATE PROCEDURE sp_AddNewTextRunning
(
	IN p_TextName VARCHAR(1024),
	IN p_UserCode INT,
	IN p_IsDisplay TINYINT,
	IN p_CreateDate datetime
)
BEGIN
	INSERT INTO text_running
	(TextName, UserCode, IsShow, CreateDate)
	VALUES
	(p_TextName, p_UserCode, p_IsDisplay, p_CreateDate);

	CALL sp_GetListTextRunning();
END $$
DELIMITER ;

DROP PROCEDURE IF EXISTS sp_LiveDisplayImage;
DELIMITER $$
CREATE PROCEDURE sp_LiveDisplayImage
(
	IN p_LiveDisplayImage TINYINT,
	IN p_UserCode INT,
	IN p_UpdateDate datetime,
	IN p_ImageID INT
)
BEGIN
	UPDATE carousel_slider
		SET IsShow = p_LiveDisplayImage,
		    UserCode = p_UserCode,
		    CreateDate = p_UpdateDate
	WHERE ID = p_ImageID;

	CALL sp_GetListImages();
END $$
DELIMITER ;

DROP PROCEDURE IF EXISTS sp_LiveDisplayVideo;
DELIMITER $$
CREATE PROCEDURE sp_LiveDisplayVideo
(
	IN p_LiveDisplayVideo TINYINT,
	IN p_UserCode INT,
	IN p_UpdateDate datetime,
	IN p_VideoID INT
)
BEGIN
	UPDATE video_content
		SET IsShow = p_LiveDisplayVideo,
		    UserCode = p_UserCode,
		    CreateDate = p_UpdateDate
	WHERE ID = p_VideoID;

	CALL sp_GetListVideos();
END $$
DELIMITER ;

DROP PROCEDURE IF EXISTS sp_LiveDisplayTextRunning;
DELIMITER $$
CREATE PROCEDURE sp_LiveDisplayTextRunning
(
	IN p_TextDisplay TINYINT,
	IN p_UserCode INT,
	IN p_UpdateDate datetime,
	IN p_TextRunID INT
)
BEGIN
	UPDATE text_running
		SET IsShow = p_TextDisplay,
		    UserCode = p_UserCode,
		    CreateDate = p_UpdateDate
	WHERE ID = p_TextRunID;

	CALL sp_GetListTextRunning();
END $$
DELIMITER ;

DROP PROCEDURE IF EXISTS sp_GetMediaContentImage;
DELIMITER $$
CREATE PROCEDURE sp_GetMediaContentImage()
BEGIN
	SELECT ID AS NumberImage,
		FileName AS ImageName,
		FileSize AS ImageSize,
		FilePath AS ImagePath,
		CASE IsShow
			WHEN 0 THEN 'Not Displayed'
			WHEN 1 THEN 'Displayed'
			WHEN 2 THEN 'Live Displayed'
			ELSE 'Not Configure'
		END AS IsShowImage
	FROM carousel_slider
	WHERE IsShow = 2
	ORDER BY CreateDate DESC LIMIT 15;
END $$
DELIMITER ;

DROP PROCEDURE IF EXISTS sp_GetMediaContentVideo;
DELIMITER $$
CREATE PROCEDURE sp_GetMediaContentVideo()
BEGIN
	SELECT ID AS NumberVideo,
		FileName AS VideoName,
		FileSize AS VideoSize,
		FilePath AS VideoPath,
		CASE IsShow
			WHEN 0 THEN 'Not Displayed'
			WHEN 1 THEN 'Displayed'
			WHEN 2 THEN 'Live Displayed'
			ELSE 'Not Configure'
		END AS IsShowVideo
	FROM video_content
	WHERE IsShow = 2
	ORDER BY CreateDate DESC LIMIT 1;
END $$
DELIMITER ;

DROP PROCEDURE IF EXISTS sp_GetMediaContentTextRun;
DELIMITER $$
CREATE PROCEDURE sp_GetMediaContentTextRun()
BEGIN
	SELECT ID AS NumberText,
		TextName AS TextName,
		CASE IsShow
			WHEN 0 THEN 'Not Displayed'
			WHEN 1 THEN 'Displayed'
			WHEN 2 THEN 'Live Displayed'
			ELSE 'Not Configure'
		END AS IsShowText
	FROM text_running
    WHERE IsShow = 2
	ORDER BY CreateDate DESC LIMIT 1;
END$$
DELIMITER ;

DROP PROCEDURE IF EXISTS sp_GetCountUsers;
DELIMITER $$
CREATE PROCEDURE sp_GetCountUsers()
BEGIN
	SELECT COUNT(cu.ID) AS CountUsers
	FROM Core_User cu
	JOIN Core_User_Details cud on cud.UserID = cu.ID
	ORDER BY cu.CreateDate DESC;
END $$
DELIMITER ;

DROP PROCEDURE IF EXISTS sp_GetCountMediaContentImage;
DELIMITER $$
CREATE PROCEDURE sp_GetCountMediaContentImage()
BEGIN
	SELECT COUNT(ID) AS CountImage
	FROM carousel_slider
	ORDER BY CreateDate DESC;
END $$
DELIMITER ;

DROP PROCEDURE IF EXISTS sp_GetCountMediaContentVideo;
DELIMITER $$
CREATE PROCEDURE sp_GetCountMediaContentVideo()
BEGIN
	SELECT COUNT(ID) AS CountVideo
	FROM video_content
	ORDER BY CreateDate DESC;
END $$
DELIMITER ;

DROP PROCEDURE IF EXISTS sp_GetCountMediaContentTextRun;
DELIMITER $$
CREATE PROCEDURE sp_GetCountMediaContentTextRun()
BEGIN
	SELECT COUNT(ID) AS CounTextRunning
	FROM text_running
	ORDER BY CreateDate DESC;
END $$
DELIMITER ;

DROP PROCEDURE IF EXISTS sp_CheckForgotPassword;
DELIMITER $$
CREATE PROCEDURE sp_CheckForgotPassword
(
    IN p_Username varchar(50),
    IN p_EmailAddress varchar(100)
)
BEGIN
    SELECT COUNT(cu.ID) AS UserId,
           COUNT(cu.Username) Username
    FROM core_user cu
    LEFT JOIN Core_User_Details cud on cu.ID = cud.UserID
    WHERE cu.Username = p_Username AND cu.Email = p_EmailAddress AND cu.IsActive = 1 AND cud.IsActive = 1;
END $$
DELIMITER ;

DROP PROCEDURE IF EXISTS sp_CreateNewPassword;
DELIMITER $$
CREATE PROCEDURE sp_CreateNewPassword
(
    IN p_NewPassword varchar(100),
    IN p_UserCode int
)
BEGIN
    UPDATE Core_User
        SET PASSWORD = p_NewPassword,
            CreateDate = NOW()
    WHERE ID = p_UserCode;
END $$
DELIMITER ;




