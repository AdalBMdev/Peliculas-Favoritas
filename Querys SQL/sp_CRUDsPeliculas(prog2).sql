use DB_Peliculas

CREATE PROCEDURE sp_RegistrarPeliculas--POST
(
    @IDUsuario int,
    @Titulo varchar(100),
    @A�o date,
    @Director varchar(100),
    @id_generos int,
    @Poster varchar(300),
    @Registrado bit output,
    @Mensaje varchar(100) output
)
AS
BEGIN
    IF NOT EXISTS (SELECT * FROM Peliculas WHERE Titulo = @Titulo AND IDUsuario = @IDUsuario)
    BEGIN
        INSERT INTO Peliculas (IDUsuario, Titulo, A�o, Director, id_generos, Poster)
        VALUES (@IDUsuario, @Titulo, @A�o, @Director, @id_generos, @Poster)
        
        SET @Registrado = 1
        SET @Mensaje = 'Pel�cula registrada'
    END
    ELSE
    BEGIN
        SET @Registrado = 0
        SET @Mensaje = 'El t�tulo ya existe'
    END
END

CREATE PROCEDURE sp_MostrarPeliculas--GET
(
    @IDUsuario int
)
AS
BEGIN
    SELECT P.IDPeliculas,P.IDUsuario, P.Titulo, P.A�o, P.Director, G.generos AS Genero, P.Poster
    FROM Peliculas P
    INNER JOIN generos G ON P.id_generos = G.id_generos
    WHERE P.IDUsuario = @IDUsuario
END

CREATE PROCEDURE sp_editarPeliculas--PUT
(
    @IDPeliculas int,
    @Titulo varchar(100),
    @A�o date,
    @Director varchar(100),
    @id_generos int,
    @Poster varchar(300),
    @Registrado bit output,
    @Mensaje varchar(100) output
)
AS
BEGIN
    IF EXISTS (SELECT * FROM Peliculas WHERE IDPeliculas = @IDPeliculas)
    BEGIN
        UPDATE Peliculas 
        SET Titulo = @Titulo, A�o = @A�o, Director = @Director, id_generos = @id_generos, Poster = @Poster
        WHERE IDPeliculas = @IDPeliculas

        SET @Registrado = 1
        SET @Mensaje = 'Pelicula editada'
    END
    ELSE
    BEGIN 
        SET @Registrado = 0
        SET @Mensaje = 'No se pudo editar la pelicula'
    END
END


create procedure sp_eliminarPeliculas(--DELETE
@IDPeliculas int,
@Registrado bit output,
@Mensaje varchar(100) output
)
as
begin
		if(exists(select * from Peliculas where IDPeliculas = @IDPeliculas))
BEGIN
    DELETE FROM Peliculas WHERE IDPeliculas = @IDPeliculas
    SET @Registrado = 1;
    SET @Mensaje = 'La pelicula ha sido eliminada correctamente';
END
ELSE
BEGIN
    SET @Registrado = 0;
    SET @Mensaje = 'La pelicula que intentas eliminar no existe';
END
END
