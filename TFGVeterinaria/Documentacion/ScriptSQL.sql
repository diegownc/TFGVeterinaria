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
	CLAVE TEXT NOT NULL,
	SALT TEXT NOT NULL,
	ACTIVO VARCHAR(200) DEFAULT 1,
	PERFIL VARCHAR(200),
	EMAIL VARCHAR(200),
	VERIFICADO INTEGER,
	PRIMARY KEY (USUARIO)	
)


ALTER TABLE USUARIOS ADD CONSTRAINT USUARIOS_PERFILES_FK FOREIGN KEY (PERFIL) REFERENCES PERFILES(CODIGO);
ALTER TABLE USUARIOS ADD CONSTRAINT USUARIOS_UNIQUE UNIQUE (EMAIL);


CREATE TABLE DIALOGOS(
	CODIGO VARCHAR(50),
	DESCRIPCION VARCHAR(200),
	PRIMARY KEY (CODIGO)
);

CREATE TABLE PERMISOS(
	PERFIL VARCHAR(50),
	DIALOGO VARCHAR(50),
	ALTA INTEGER DEFAULT 0,
	BAJA INTEGER DEFAULT 0,
	CONSULTA INTEGER DEFAULT 0,
	MODIF INTEGER DEFAULT 0,
	PRIMARY KEY (PERFIL , DIALOGO)
);


CREATE TABLE LOG_PROCESOS(
	ID SERIAL PRIMARY KEY,
    UBICACION VARCHAR(200),
    STACKTRACE TEXT,
    ERROR_MESSAGE TEXT,
    FECHA TIMESTAMP
);

-- ########################################################################################################################################
-- INSERTS
-- ########################################################################################################################################


INSERT INTO PERFILES (CODIGO, DESCRIPCION) VALUES ('GENERICO', 'Perfil Genérico');
INSERT INTO PERFILES (CODIGO, DESCRIPCION) VALUES ('VETERINARIO', 'Perfil Veterinario');
INSERT INTO PERFILES (CODIGO, DESCRIPCION) VALUES ('ADMINISTRADOR', 'Perfil Administrador');

