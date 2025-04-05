-- PGADMIN
-- User: admin
-- Password: admin
-- Version: PostgreSQL 17

-- ########################################################################################################################################
-- CREATE TABLES
-- ########################################################################################################################################

CREATE TABLE PERFILES(
	CODIGO VARCHAR(50),
	DESCRIPCION VARCHAR(200),
	PRIMARY KEY (CODIGO)
);


CREATE TABLE USUARIOS(
	USUARIO VARCHAR(50),
	NOMBRE VARCHAR(200),
	CLAVE VARCHAR(200),
	ACTIVO VARCHAR(200) DEFAULT 1,
	PERFIL VARCHAR(200),
	EMAIL VARCHAR(200),
	PRIMARY KEY (USUARIO)	
);


ALTER TABLE USUARIOS ADD CONSTRAINT USUARIOS_PERFILES_FK FOREIGN KEY (PERFIL) REFERENCES PERFILES(CODIGO);
ALTER TABLE USUARIOS ADD CONSTRAINT USUARIOS_UNIQUE UNIQUE (EMAIL);


-- ########################################################################################################################################
-- INSERTS
-- ########################################################################################################################################