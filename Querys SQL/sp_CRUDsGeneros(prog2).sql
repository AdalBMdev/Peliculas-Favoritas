use DB_Peliculas

CREATE PROCEDURE sp_AgregarGenero--POST
(
    @Genero varchar(33),
    @GeneroAgregado bit output,
    @Mensaje varchar(100) output
)
AS
BEGIN
    IF NOT EXISTS (SELECT * FROM generos WHERE generos = @Genero)
    BEGIN
        INSERT INTO generos (generos) VALUES (@Genero)
        SET @GeneroAgregado = 1
        SET @Mensaje = 'G�nero agregado correctamente'
    END
    ELSE
    BEGIN
        SET @GeneroAgregado = 0
        SET @Mensaje = 'El g�nero ya existe'
    END
END

CREATE PROCEDURE sp_MostrarGeneros--GET
AS
BEGIN
    SELECT * FROM generos
END


CREATE PROCEDURE sp_EditarGenero--PUT
(
    @IdGenero int,
    @NuevoGenero varchar(33),
    @GeneroEditado bit output,
    @Mensaje varchar(100) output
)
AS
BEGIN
    IF NOT EXISTS (SELECT * FROM generos WHERE generos = @NuevoGenero)
    BEGIN
        IF EXISTS (SELECT * FROM generos WHERE id_generos = @IdGenero)
        BEGIN
            UPDATE generos SET generos = @NuevoGenero WHERE id_generos = @IdGenero
            SET @GeneroEditado = 1
            SET @Mensaje = 'G�nero editado correctamente'
        END
        ELSE
        BEGIN
            SET @GeneroEditado = 0
            SET @Mensaje = 'No se encontr� el g�nero a editar'
        END
    END
    ELSE
    BEGIN
        SET @GeneroEditado = 0
        SET @Mensaje = 'Ya existe un g�nero con ese nombre'
    END
END

CREATE PROCEDURE sp_EliminarGenero--DELETE
(
    @IdGenero int,
    @GeneroEliminado bit output,
    @Mensaje varchar(100) output
)
AS
BEGIN
    IF EXISTS (SELECT * FROM generos WHERE id_generos = @IdGenero)
    BEGIN
        DELETE FROM generos WHERE id_generos = @IdGenero
        SET @GeneroEliminado = 1
        SET @Mensaje = 'G�nero eliminado correctamente'
    END
    ELSE
    BEGIN
        SET @GeneroEliminado = 0
        SET @Mensaje = 'No se encontr� el g�nero a eliminar'
    END
END
