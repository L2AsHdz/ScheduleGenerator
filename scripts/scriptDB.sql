
DROP DATABASE IF EXISTS ScheduleDB;
CREATE DATABASE ScheduleDB;
USE ScheduleDB;

CREATE TABLE IF NOT EXISTS Carrera (
    codigoCarrera INT NOT NULL AUTO_INCREMENT,
    nombre VARCHAR(100) NOT NULL,
    cantidadSemestres INT NOT NULL,
    presupuesto DECIMAL(10,2) NOT NULL,
    color VARCHAR(50) NOT NULL,
    PRIMARY KEY (codigoCarrera)
);

INSERT INTO Carrera (nombre, cantidadSemestres, presupuesto, color)
VALUES
    ('Carrera 1', 4, 100000, '#D4B27D'),
    ('Carrera 2', 4, 120000, '#BBBAE8'),
    ('Carrera 3', 4, 160000, '#6ECF9E')
;

CREATE TABLE IF NOT EXISTS Salon (
    noSalon INT NOT NULL AUTO_INCREMENT,
    nombre VARCHAR(100) NOT NULL,
    capacidadRecomendada INT NOT NULL,
    capacidadMaxima INT NOT NULL,
    codigoCarreraPreferida INT NULL,
    ubicacion VARCHAR(60) NOT NULL,
    PRIMARY KEY (noSalon),
    CONSTRAINT FOREIGN KEY (codigoCarreraPreferida) REFERENCES Carrera(codigoCarrera) ON UPDATE RESTRICT ON DELETE RESTRICT
);

INSERT INTO Salon (nombre, capacidadRecomendada, capacidadMaxima, codigoCarreraPreferida, ubicacion)
VALUES
    ('Salon 1', 25, 35, null, 'Ubicacion 1'),
    ('Salon 2', 20, 30, null, 'Ubicacion 2'),
    ('Salon 3', 15, 20, 1, 'Ubicacion 3'),
    ('Salon 4', 30, 40, null, 'Ubicacion 4'),
    ('Salon 5', 40, 50, null, 'Ubicacion 5'),
    ('Salon 6', 15, 20, null, 'Ubicacion 6'),
    ('Salon 7', 20, 30, 2, 'Ubicacion 7'),
    ('Salon 8', 25, 35, null, 'Ubicacion 8'),
    ('Salon 9', 35, 45, null, 'Ubicacion 9'),
    ('Salon 10', 35, 50, null, 'Ubicacion 10')
;

CREATE TABLE IF NOT EXISTS Curso (
    codigoCurso INT NOT NULL AUTO_INCREMENT,
    nombre VARCHAR(45) NOT NULL,
    PRIMARY KEY (codigoCurso)
);

INSERT INTO Curso (nombre)
VALUES
    ('Curso 1'),
    ('Curso 2'),
    ('Curso 3'),
    ('Curso 4'),
    ('Curso 5'),
    ('Curso 6'),
    ('Curso 7'),
    ('Curso 8'),
    ('Curso 9'),
    ('Curso 10'),
    ('Curso 11'),
    ('Curso 12'),
    ('Curso 13'),
    ('Curso 14'),
    ('Curso 15'),
    ('Curso 16'),
    ('Curso 17'),
    ('Curso 18'),
    ('Curso 19'),
    ('Curso 20'),
    ('Curso 21'),
    ('Curso 22'),
    ('Curso 23'),
    ('Curso 24'),
    ('Curso 25'),
    ('Curso 26'),
    ('Curso 27'),
    ('Curso 28'),
    ('Curso 29'),
    ('Curso 30'),
    ('Curso 31'),
    ('Curso 32'),
    ('Curso 33'),
    ('Curso 34'),
    ('Curso 35'),
    ('Curso 36'),
    ('Curso 37'),
    ('Curso 38'),
    ('Curso 39'),
    ('Curso 40')
;

CREATE TABLE IF NOT EXISTS CursoSalon (
    noSalon INT NOT NULL,
    codigoCurso INT NOT NULL,
    PRIMARY KEY (noSalon, codigoCurso),
    CONSTRAINT FOREIGN KEY (noSalon) REFERENCES Salon(noSalon) ON UPDATE RESTRICT ON DELETE RESTRICT,
    CONSTRAINT FOREIGN KEY (codigoCurso) REFERENCES Curso(codigoCurso) ON UPDATE RESTRICT ON DELETE RESTRICT
);

CREATE TABLE IF NOT EXISTS CursoCarrera (
    codigoCarrera INT NOT NULL,
    codigoCurso INT NOT NULL,
    cantidadAsignaciones INT NOT NULL,
    semestre INT NOT NULL,
    creditos INT NOT NULL,
    costo DECIMAL(10,2) NOT NULL,
    esObligatorio TINYINT NOT NULL,
    PRIMARY KEY (codigoCarrera, codigoCurso),
    CONSTRAINT FOREIGN KEY (codigoCurso) REFERENCES Curso(codigoCurso) ON UPDATE RESTRICT ON DELETE RESTRICT,
    CONSTRAINT FOREIGN KEY (codigoCarrera) REFERENCES Carrera(codigoCarrera) ON UPDATE RESTRICT ON DELETE RESTRICT
);

INSERT INTO CursoCarrera (codigoCarrera, codigoCurso, cantidadAsignaciones, semestre, creditos, costo, esObligatorio)
VALUES
    -- Carrera 1
    (1, 1, 20, 1, 4, 10000, 1),
    (1, 2, 15, 1, 4, 10000, 1),
    (1, 3, 15, 1, 4, 10000, 1),
    (1, 4, 0, 1, 4, 10000, 0),

    (1, 5, 15, 2, 4, 12000, 1),
    (1, 6, 20, 2, 4, 12000, 1),
    (1, 7, 18, 2, 4, 12000, 1),

    (1, 8, 20, 3, 4, 15000, 1),
    (1, 9, 17, 3, 4, 15000, 1),
    (1, 10, 25, 3, 4, 15000, 1),
    (1, 11, 10, 3, 4, 15000, 0),

    (1, 12, 21, 4, 4, 15000, 1),
    (1, 13, 25, 4, 4, 15000, 1),
    (1, 14, 19, 4, 4, 15000, 1),
    (1, 15, 0, 4, 4, 15000, 0),

    -- Carrera 2
    (2, 1, 10, 1, 4, 10000, 1),
    (2, 2, 15, 1, 4, 10000, 1),
    (2, 3, 15, 1, 4, 10000, 1),
    (2, 16, 15, 1, 4, 10000, 0),

    (2, 17, 8, 2, 4, 12000, 1),
    (2, 18, 20, 2, 4, 12000, 1),
    (2, 19, 25, 2, 4, 12000, 1),
    (2, 10, 7, 2, 4, 12000, 0),

    (2, 20, 18, 3, 4, 15000, 1),
    (2, 21, 30, 3, 4, 15000, 1),
    (2, 22, 17, 3, 4, 15000, 1),
    (2, 23, 23, 3, 4, 15000, 0),

    (2, 24, 28, 4, 4, 18000, 1),
    (2, 25, 12, 4, 4, 18000, 1),
    (2, 26, 16, 4, 4, 18000, 1),
    (2, 27, 21, 4, 4, 18000, 1),

    -- Carrera 3
    (3, 1, 17, 1, 4, 10000, 1),
    (3, 2, 10, 1, 4, 10000, 1),
    (3, 3, 12, 1, 4, 10000, 1),
    (3, 28, 30, 1, 4, 10000, 0),

    (3, 29, 18, 2, 4, 12000, 1),
    (3, 30, 20, 2, 4, 12000, 1),
    (3, 31, 24, 2, 4, 12000, 1),
    (3, 32, 19, 2, 4, 12000, 1),

    (3, 33, 23, 3, 4, 14000, 1),
    (3, 34, 27, 3, 4, 14000, 1),
    (3, 35, 15, 3, 4, 14000, 1),
    (3, 18, 12, 3, 4, 14000, 0),

    (3, 36, 24, 4, 4, 16000, 1),
    (3, 37, 37, 4, 4, 16000, 1),
    (3, 38, 29, 4, 4, 16000, 1),
    (3, 39, 21, 4, 4, 16000, 1),
    (3, 40, 39, 4, 4, 16000, 1)
;

CREATE TABLE IF NOT EXISTS Catedratico (
    codigoCatedratico INT NOT NULL AUTO_INCREMENT,
    nombre VARCHAR(100) NOT NULL,
    noColegiado INT NOT NULL,
    horaEntrada TIME NOT NULL,
    horaSalida TIME NOT NULL,
    PRIMARY KEY (codigoCatedratico)
);

INSERT INTO Catedratico (nombre, noColegiado, horaEntrada, horaSalida)
VALUES
    ('Catedratico 1', 12345, '13:00', '18:00'),
    ('Catedratico 2', 12345, '13:00', '18:00'),
    ('Catedratico 3', 12345, '13:00', '18:00'),
    ('Catedratico 4', 12345, '15:00', '19:00'),
    ('Catedratico 5', 12345, '18:00', '21:00'),
    ('Catedratico 6', 12345, '15:00', '19:00'),
    ('Catedratico 7', 12345, '14:00', '19:00'),
    ('Catedratico 8', 12345, '13:00', '18:00'),
    ('Catedratico 9', 12345, '14:00', '19:00'),
    ('Catedratico 10', 12345, '15:00', '19:00'),
    ('Catedratico 11', 12345, '18:00', '21:00'),
    ('Catedratico 12', 12345, '15:00', '19:00'),
    ('Catedratico 13', 12345, '18:00', '21:00'),
    ('Catedratico 14', 12345, '13:00', '18:00'),
    ('Catedratico 15', 12345, '16:00', '20:00'),
    ('Catedratico 16', 12345, '18:00', '21:00'),
    ('Catedratico 17', 12345, '15:00', '19:00'),
    ('Catedratico 18', 12345, '13:00', '18:00'),
    ('Catedratico 19', 12345, '15:00', '19:00'),
    ('Catedratico 20', 12345, '18:00', '21:00')
;

CREATE TABLE IF NOT EXISTS CursoCatedratico (
    codigoCatedratico INT NOT NULL,
    codigoCurso INT NOT NULL,
    prioridad TINYINT NOT NULL,
    PRIMARY KEY (codigoCatedratico, codigoCurso),
    CONSTRAINT FOREIGN KEY (codigoCurso) REFERENCES Curso(codigoCurso) ON UPDATE RESTRICT ON DELETE RESTRICT,
    CONSTRAINT FOREIGN KEY (codigoCatedratico) REFERENCES Catedratico(codigoCatedratico) ON UPDATE RESTRICT ON DELETE RESTRICT
);

INSERT INTO CursoCatedratico (codigoCatedratico, codigoCurso, prioridad)
VALUES
    (1, 1, 1),
    (1, 2, 1),
    (2, 3, 1),
    (3, 4, 1),
    (4, 5, 1),
    (5, 6, 1),
    (6, 7, 1),
    (7, 8, 1),
    (8, 9, 1),
    (9, 10, 1),
    (10, 11, 1),
    (4, 12, 1),
    (3, 13, 1),
    (2, 14, 1),
    (1, 15, 1),
    (6, 16, 1),
    (7, 17, 1),
    (8, 18, 1),
    (9, 19, 1),
    (10, 20, 1),
    (11, 21, 1),
    (12, 22, 1),
    (13, 23, 1),
    (14, 24, 1),
    (15, 25, 1),
    (16, 26, 1),
    (17, 27, 1),
    (18, 28, 1),
    (19, 29, 1),
    (20, 30, 1),
    (15, 31, 1),
    (14, 32, 1),
    (13, 33, 1),
    (20, 34, 1),
    (18, 35, 1),
    (12, 36, 1),
    (13, 37, 1),
    (7, 38, 1),
    (4, 39, 1),
    (9, 40, 1),
    (1, 5, 0),
    (14, 17, 0),
    (16, 22, 0),
    (1, 33, 0),
    (13, 9, 0),
    (20, 11, 0),
    (8, 25, 0),
    (15, 3, 0),
    (13, 13, 0),
    (17, 29, 0),
    (11, 18, 0),
    (15, 21, 0)
;

-- Parametro                            Tipo            Default
-- Horario default                      int
-- Duracion de periodo                  int                60
-- Hora inicio de clases                time               13:00:00
-- Hora fin de clases                   time               21:00:00
-- Asignacion minima                    int                15

-- Prioridad salon                     tinyint             1                   Prioridad a los cursos indicados en el salon
-- *Prioridad carrera                   tinyint             1                   Prioridad a los cursos de una carrera en especifico (Prioridad salon y priodidad carrera no pueden estar actvas al mismo tiempo)
-- *Prioridad catedratico               tinyint             1                   Prioridad a los catedraticos titulares
-- Prioridad presupuesto                tinyint             1                   Prioridad a los cursos que cuesten menos
-- Prioridad capacidad                  tinyint             1                   Prioridad a los cursos que ocupen mejor el espacio
-- *Priodidad ultimos semestres         tinyint             1                   Primeros o ultimos semestres
-- Prioridad asignaciones               tinyint             1                   Prioridad a los cursos con mas asignaciones

-- Ignorar asignacion minima            tinyint             0
-- Crear secciones                      tinyint             1
-- Mostrar cursos con advertencia       tinyint             0
CREATE TABLE IF NOT EXISTS Parametro (
    codigoParametro INT NOT NULL AUTO_INCREMENT,
    nombre VARCHAR(60) NOT NULL,
    valor varchar(100) NOT NULL,
    tipo ENUM('CONFIGURACION', 'PRIORIDAD', 'COMPORTAMIENTO') NOT NULL,
    descripcion varchar(250) NOT NULL,
    PRIMARY KEY (codigoParametro)
);

INSERT INTO Parametro (nombre, valor, tipo, descripcion)
VALUES
    ('config.defaultSchedule', 1, 'CONFIGURACION', 'Horario por defecto'),
    ('config.durationPeriod', 60, 'CONFIGURACION', 'Duracion de periodo'),
    ('config.startHour', '13:00:00', 'CONFIGURACION', 'Hora inicio de clases'),
    ('config.endHour', '21:00:00', 'CONFIGURACION', 'Hora fin de clases'),
    ('config.minAssignment', 15, 'CONFIGURACION', 'Asignacion minima'),

    ('priority.room', 1, 'PRIORIDAD', 'Prioridad salon'),
    ('priority.career', 1, 'PRIORIDAD', 'Prioridad carrera'),
    ('priority.teacher', 1, 'PRIORIDAD', 'Prioridad catedratico'),
    ('priority.budget', 1, 'PRIORIDAD', 'Prioridad presupuesto'),
    ('priority.capacity', 1, 'PRIORIDAD', 'Prioridad al salon que este mejor ocupado'),
    ('priority.lastSemester', 1, 'PRIORIDAD', 'Priodidad ultimos semestres'),
    ('priority.assignment', 1, 'PRIORIDAD', 'Prioridad a los cursos con mas asignaciones'),

    ('behavior.joinCourses', 0, 'COMPORTAMIENTO', 'Unir mismos cursos de distintas carreras'),
    ('behavior.ignoreMinAssignment', 0, 'COMPORTAMIENTO', 'Ignorar asignacion minima'),
    ('behavior.createSections', 0, 'COMPORTAMIENTO', 'Crear secciones'),
    ('behavior.showWarning', 0, 'COMPORTAMIENTO', 'Mostrar cursos con advertencia'),
    ('behavior.evenSemester', 1, 'COMPORTAMIENTO', 'Filtra por los cursos de semestre par'),
    ('priority.obligatory', 1, 'PRIORIDAD', 'Prioridad a los cursos obligatorios'),
    ('behavior.ignoreMaxCapacity', 0, 'COMPORTAMIENTO', 'Ignorar la capacidad maxima del salon')
;

CREATE TABLE IF NOT EXISTS  CursoHorario (
    idRegistro INT NOT NULL AUTO_INCREMENT,
    codigoHorario INT NOT NULL,
    codigoCurso INT NOT NULL,
    codigoCarrera INT NOT NULL,
    codigoCatedratico INT NOT NULL,
    noSalon INT NOT NULL,
    horaInicio TIME NOT NULL,
    horaFin TIME NOT NULL,
    PRIMARY KEY (idRegistro),
    CONSTRAINT FOREIGN KEY (codigoCurso) REFERENCES Curso(codigoCurso) ON UPDATE RESTRICT ON DELETE RESTRICT,
    CONSTRAINT FOREIGN KEY (codigoCarrera) REFERENCES Carrera(codigoCarrera) ON UPDATE RESTRICT ON DELETE RESTRICT,
    CONSTRAINT FOREIGN KEY (codigoCatedratico) REFERENCES Catedratico(codigoCatedratico) ON UPDATE RESTRICT ON DELETE RESTRICT,
    CONSTRAINT FOREIGN KEY (noSalon) REFERENCES Salon(noSalon) ON UPDATE RESTRICT ON DELETE RESTRICT,
    UNIQUE (codigoHorario, codigoCurso, codigoCatedratico, noSalon)
);

CREATE TABLE IF NOT EXISTS CursoAdvertencia (
    idRegistro INT NOT NULL AUTO_INCREMENT,
    codigoHorario INT NOT NULL,
    codigoCurso INT NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    advertencia VARCHAR(250) NOT NULL,
    PRIMARY KEY (idRegistro),
    CONSTRAINT FOREIGN KEY (codigoCurso) REFERENCES Curso(codigoCurso) ON UPDATE RESTRICT ON DELETE RESTRICT
);
