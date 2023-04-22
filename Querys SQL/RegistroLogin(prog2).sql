use DB_Peliculas

----------------------------------------------------Registro y Login------------------------------------------------------------------------

create procedure sp_Registrar(
@Nickname varchar(100),
@Correo varchar(100),
@Clave varchar(500),
@Registrado bit output,
@Mensaje varchar(100) output
)
as
begin

	if(not exists(select * from Usuario where Correo = @Correo and Nickname = @Nickname))--Validar correo y nicknames unicos
	begin
		insert into Usuario(Nickname,Correo,Clave) values(@Nickname,@Correo,@Clave)-- insertar
		set @Registrado = 1
		set @Mensaje = 'Usuario registrado'
	end
	else
	begin 
		set @Registrado = 0
		set @Mensaje = 'Correo/Nick ya existe'
	end
end


create procedure sp_Validar(
@Correo varchar(100),
@Clave varchar(500)
)
as
begin
	if(exists(select * from Usuario where Correo = @Correo and Clave = @Clave))
		select IDUsuario from Usuario where Correo = @Correo and Clave = @Clave
	else
		select '0'
end
