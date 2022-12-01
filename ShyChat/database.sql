CREATE DATABASE aachat COLLATE utf8_general_ci;
USE aachat;

CREATE TABLE comments (
	cid			BIGINT NOT NULL auto_increment,
	comment		VARCHAR(500) NOT NULL,
	user		VARCHAR(100) NOT NULL,
	created		TIMESTAMP DEFAULT CURRENT_TIMESTAMP(),
	PRIMARY KEY(cid)
);