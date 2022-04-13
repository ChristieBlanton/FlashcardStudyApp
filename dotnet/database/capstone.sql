USE master
GO

--drop database if it exists
IF DB_ID('final_capstone') IS NOT NULL
BEGIN
	ALTER DATABASE final_capstone SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE final_capstone;
END

CREATE DATABASE final_capstone
GO

USE final_capstone
GO

--create tables
CREATE TABLE users (
	user_id int IDENTITY(1,1) NOT NULL,
	username varchar(50) NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	user_role varchar(50) NOT NULL
	CONSTRAINT PK_user PRIMARY KEY (user_id)
)

CREATE TABLE deck (
	deck_id int IDENTITY (101, 1) NOT NULL,
	user_id int NOT NULL,
	deck_name varchar(50) NOT NULL,
	deck_description varchar(100) NOT NULL,
	isPublic bit DEFAULT(0) NOT NULL,
	CONSTRAINT PK_deck PRIMARY KEY (deck_id),
	CONSTRAINT FK_deck FOREIGN KEY (user_id) REFERENCES users(user_id)
)

CREATE TABLE card (
	card_id int IDENTITY (1001,1) NOT NULL,
	card_front varchar(200) NOT NULL,
	card_back varchar(200) NOT NULL,
	user_id int NOT NULL,
	isPublic bit DEFAULT(0) NOT NULL,
	CONSTRAINT FK_card FOREIGN KEY (user_id) REFERENCES users(user_id),
	CONSTRAINT PK_card PRIMARY KEY (card_id)
)

CREATE TABLE tag(
	tag_id int IDENTITY(2001,1) NOT NULL,
	tag_name varchar (50) NOT NULL,
	CONSTRAINT PK_tag PRIMARY KEY (tag_id)
)

CREATE TABLE card_tag(
	tag_id int NOT NULL,
	card_id int NOT NULL,
	CONSTRAINT PK_card_tag PRIMARY KEY (tag_id, card_id),
	CONSTRAINT FK_ct_card FOREIGN KEY (card_id) REFERENCES card(card_id),
	CONSTRAINT FK_ct_tag FOREIGN KEY (tag_id) REFERENCES tag(tag_id)
	
)

CREATE TABLE card_deck(
	card_id int NOT NULL,
	deck_id int NOT NULL,
	CONSTRAINT PK_card_deck PRIMARY KEY (card_id, deck_id),
	CONSTRAINT FK_card_id FOREIGN KEY (card_id) REFERENCES card(card_id),
	CONSTRAINT FK_deck_id FOREIGN KEY (deck_id) REFERENCES deck(deck_id)
)


--populate default data
-- user/password
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user@email.com','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');

-- admin/password
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('admin@email.com','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');

INSERT INTO deck (user_id, deck_name, deck_description) VALUES (1, 'Test Deck', 'Test Description');

INSERT INTO card (card_front, card_back, user_id) VALUES ('Test', 'TestAnswer', 1);

INSERT INTO card_deck (card_id, deck_id) VALUES (1001, 101);



GO