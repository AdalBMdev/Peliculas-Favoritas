use DB_Peliculas
----------------------------------------------------Crear las tablas principales------------------------------------------------------------------------
create table Usuario(
IDUsuario int primary key identity (1,1),
Nickname varchar(100),
Correo varchar(100),
Clave varchar(500)
)

create table Peliculas(
IDPeliculas int primary key identity(1,1),
IDUsuario int,
Titulo varchar(100),
Año date,
Director varchar(100),
id_generos int,
Poster varchar(300)
)

create table generos
(
id_generos int primary key identity(1,1),
generos varchar(33)
)