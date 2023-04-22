using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Peliculas.Models;
using System.Text.Json;
using Newtonsoft.Json;

namespace Peliculas.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    private readonly string cadena = @"Server=ADALBERTO; Database=DB_peliculas; Trusted_Connection=True;"; //Conexion a bdd

    [HttpPost("registrar")]
    public IActionResult Registrar(Usuario oUsuario) //Obtiene un objeto tipo usuario
    {
        oUsuario.Clave = ConvertirSha256(oUsuario.Clave); //Encripta la contrase;a en SHA256

        using (SqlConnection cn = new SqlConnection(cadena)) //Usa la conexion
        {
            SqlCommand cmd = new SqlCommand("sp_Registrar", cn);//Usa el store procedure de la base de datos "sp_Registrar"
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Nickname", oUsuario.Nickname); //Obtiene los datos de la consulta
            cmd.Parameters.AddWithValue("@Correo", oUsuario.Correo);
            cmd.Parameters.AddWithValue("@Clave", oUsuario.Clave);

            SqlParameter Registrado = new SqlParameter("@Registrado", SqlDbType.Bit);
            Registrado.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(Registrado);

            SqlParameter Mensaje = new SqlParameter("@Mensaje", SqlDbType.VarChar, 100);
            Mensaje.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(Mensaje);

            cn.Open();

            cmd.ExecuteNonQuery();

            bool registrado = Convert.ToBoolean(Registrado.Value);
            string mensaje = Mensaje.Value.ToString();

            if (registrado)
            {
                return Ok(mensaje);
            }
            else
            {
                return BadRequest(mensaje);
            }
        }
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] JsonElement body)
    {
        // Obtener los valores de correo y clave del objeto JSON
        string correo = body.GetProperty("correo").GetString();
        var claveEncriptada = ConvertirSha256(body.GetProperty("clave").GetString()); //Vuelve a encriptar la clave introducida por el usuario

        using (SqlConnection cn = new SqlConnection(cadena))
        {
            SqlCommand cmd = new SqlCommand("sp_Validar", cn); //Usa el store procedure "sp_Validar"
            cmd.Parameters.AddWithValue("@Correo", correo);//Envia los datos necesarios
            cmd.Parameters.AddWithValue("@Clave", claveEncriptada);
            cmd.CommandType = CommandType.StoredProcedure;

            cn.Open();

            int idUsuario = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);


            if (idUsuario != 0) //Si encuentra un usuario 
            {

                HttpContext.Session.SetInt32("IdUsuario", idUsuario);

                //Obtener nickname
                SqlCommand cmd2 = new SqlCommand("SELECT Nickname FROM Usuario WHERE IDUsuario = @IDUsuario", cn);
                cmd2.Parameters.AddWithValue("@IDUsuario", idUsuario);
                string nicknamelog = cmd2.ExecuteScalar().ToString();

                var usuario = new
                {
                    id = idUsuario,
                    nickname = nicknamelog,
                    mensaje = "Usuario encontrado"
                };

                // Serializar objeto en formato JSON y enviar como respuesta
                var jsonUsuario = JsonConvert.SerializeObject(usuario);
                return Ok(jsonUsuario);


            }
            else //Si no encuentra un usuario 
            {

                // Crear objeto con mensaje de error
                var error = new
                {
                    mensaje = "Usuario no encontrado"
                };

                // Serializar objeto en formato JSON y enviar como respuesta
                var jsonError = JsonConvert.SerializeObject(error);
                return BadRequest(jsonError);
            }
        }
    }


    private string ConvertirSha256(string inputString) //Encriptacion
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(inputString));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }

    [HttpGet("logout")]
    public IActionResult Logout() //Desloguear-Cerrar sesion
    {
        HttpContext.Session.Clear();

        var usuario = new
        {
            id = 0,
            mensaje = "Sesion de Usuario finalizada"
        };

        // Serializar objeto en formato JSON y enviar como respuesta
        var jsonUsuario = JsonConvert.SerializeObject(usuario);
        return Ok(jsonUsuario);
    }

}



