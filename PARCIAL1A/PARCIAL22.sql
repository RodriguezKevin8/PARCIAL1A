CREATE TABLE autores (
    autorid INT IDENTITY(1, 1) PRIMARY KEY,
    nombre VARCHAR(50)
);

CREATE TABLE libros (
    libroid INT IDENTITY(1,1) PRIMARY KEY,
    titulo VARCHAR(50)
);

CREATE TABLE AutorLibro (
    autorid INT ,
    libroid INT ,
    orden INT,
    CONSTRAINT PK_AutorLibro PRIMARY KEY (autorid, libroid),
    CONSTRAINT FK_AutorLibro_autores FOREIGN KEY (autorid) REFERENCES autores (autorid),
    CONSTRAINT FK_AutorLibro_libros FOREIGN KEY (libroid) REFERENCES libros (libroid)
);

CREATE TABLE post (
    postid INT IDENTITY(1,1) PRIMARY KEY,
    titulo VARCHAR(50),
    contenido VARCHAR(500),
    fechapublicacion DATETIME,
    autorid INT,
    CONSTRAINT FK_post_autores FOREIGN KEY (autorid) REFERENCES autores (autorid)
);


-- Insertar datos en la tabla autores
INSERT INTO autores (nombre) VALUES ('Gabriel Garcia Marquez');
INSERT INTO autores (nombre) VALUES ('Julio Cortazar');
INSERT INTO autores (nombre) VALUES ('Mario Vargas Llosa');

-- Insertar datos en la tabla libros
INSERT INTO libros (titulo) VALUES ('Cien años de soledad');
INSERT INTO libros (titulo) VALUES ('Rayuela');
INSERT INTO libros (titulo) VALUES ('La ciudad y los perros');

-- Insertar datos en la tabla AutorLibro
INSERT INTO AutorLibro (autorid, libroid, orden) VALUES (1, 1, 1);
INSERT INTO AutorLibro (autorid, libroid, orden) VALUES (2, 2, 1);
INSERT INTO AutorLibro (autorid, libroid, orden) VALUES (3, 3, 1);

-- Insertar datos en la tabla post
INSERT INTO post (titulo, contenido, fechapublicacion, autorid) 
VALUES ('Nuevo post sobre Cien años de soledad', 'Contenido del post...', GETDATE(), 1);

INSERT INTO post (titulo, contenido, fechapublicacion, autorid) 
VALUES ('Nuevo post sobre Rayuela', 'Contenido del post...', GETDATE(), 2);
INSERT INTO post (titulo, contenido, fechapublicacion, autorid) 
VALUES ('Nuevo post sobre La ciudad y los perros', 'Contenido del post...', GETDATE(), 3);
