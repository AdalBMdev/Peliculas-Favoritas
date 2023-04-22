use DB_Peliculas

----------------------------------------------------FILTRO CATEGORIA------------------------------------------------------------------------
CREATE PROCEDURE sp_FiltrarPorTitulo--titulo
(
    @IDUsuario int,
    @Titulo nvarchar(100)
)
AS
BEGIN
    SELECT P.IDPeliculas, P.IDUsuario, P.Titulo, P.A�o, P.Director, G.generos AS Genero, P.Poster
    FROM Peliculas P
    INNER JOIN generos G ON P.id_generos = G.id_generos
    WHERE P.IDUsuario = @IDUsuario AND P.Titulo LIKE '%' + @Titulo + '%'
END


CREATE PROCEDURE sp_FiltrarPorA�o--A�o
(
    @IDUsuario int,
    @A�o date
)
AS
BEGIN
    SELECT P.IDPeliculas, P.IDUsuario, P.Titulo, P.A�o, P.Director, G.generos AS Genero, P.Poster
    FROM Peliculas P
    INNER JOIN generos G ON P.id_generos = G.id_generos
    WHERE P.IDUsuario = @IDUsuario AND YEAR(P.A�o) = YEAR(@A�o)
END

DROP PROCEDURE dbo.sp_FiltrarPorA�o;


CREATE PROCEDURE sp_FiltrarPorDirector--Director
(
    @IDUsuario int,
    @Director nvarchar(100)
)
AS
BEGIN
    SELECT P.IDPeliculas, P.IDUsuario, P.Titulo, P.A�o, P.Director, G.generos AS Genero, P.Poster
    FROM Peliculas P
    INNER JOIN generos G ON P.id_generos = G.id_generos
    WHERE P.IDUsuario = @IDUsuario AND P.Director LIKE '%' + @Director + '%'
END



CREATE PROCEDURE sp_MostrarPeliculasPorGenero--Genero
(
    @IDUsuario int,
    @Genero varchar(50)
)
AS
BEGIN
    SELECT P.IDPeliculas, P.IDUsuario, P.Titulo, P.A�o, P.Director, G.generos AS Genero, P.Poster
    FROM Peliculas P
    INNER JOIN generos G ON P.id_generos = G.id_generos
    WHERE P.IDUsuario = @IDUsuario AND G.generos LIKE '%' + @Genero + '%'
END