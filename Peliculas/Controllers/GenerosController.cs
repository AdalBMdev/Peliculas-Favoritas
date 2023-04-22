using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Peliculas.Models;


namespace Peliculas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenerosController : Controller
    {

        private readonly string cadena = @"Server=ADALBERTO; Database=DB_peliculas; Trusted_Connection=True;"; //Conexion a bdd

        [HttpPost("agregarGenero")]
        public IActionResult registrarGeneros(Generos oGeneros) //Obtiene un objeto tipo genero
        {
            using (SqlConnection cn = new SqlConnection(cadena)) //Usa la conexion
            {
                SqlCommand cmd = new SqlCommand("sp_AgregarGenero", cn);//Usa el store procedure de la base de datos "sp_AgregarGenero"
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Genero", oGeneros.generos); //obtiene el parametro genero



                SqlParameter Registrado = new SqlParameter("@GeneroAgregado", SqlDbType.Bit);
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

        [HttpGet("mostrarGeneros")]
        public IActionResult mostrarGeneros() 
        {
            using (SqlConnection cn = new SqlConnection(cadena)) //Usa la conexion
            {
                SqlCommand cmd = new SqlCommand("sp_MostrarGeneros", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                

                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                List<Generos> generos = new List<Generos>();

                while (dr.Read())
                {
                    Generos genero = new Generos();


                    genero.id_generos = Convert.ToInt32(dr["id_generos"]);
                    genero.generos = dr["generos"].ToString();

                    generos.Add(genero);
                }

                cn.Close();
                return Ok(generos);
            }
        }

        [HttpPut("editarGenero")]
        public IActionResult editarGenero(Generos oGenero)
        {
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_editarGenero", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdGenero", oGenero.id_generos);
                cmd.Parameters.AddWithValue("@NuevoGenero", oGenero.generos);

                SqlParameter Editado = new SqlParameter("@GeneroEditado", SqlDbType.Bit);
                Editado.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(Editado);

                SqlParameter Mensaje = new SqlParameter("@Mensaje", SqlDbType.VarChar, 100);
                Mensaje.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(Mensaje);

                cn.Open();

                cmd.ExecuteNonQuery();

                bool editado = Convert.ToBoolean(Editado.Value);
                string mensaje = Mensaje.Value.ToString();

                if (editado)
                {
                    return Ok(mensaje);
                }
                else
                {
                    return BadRequest(mensaje);
                }
            }
        }

        [HttpDelete("eliminarGenero/{id}")]
        public IActionResult eliminarGenero(int id)
        {
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_eliminarGenero", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdGenero", id);

                SqlParameter Eliminado = new SqlParameter("@GeneroEliminado", SqlDbType.Bit);
                Eliminado.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(Eliminado);

                SqlParameter Mensaje = new SqlParameter("@Mensaje", SqlDbType.VarChar, 100);
                Mensaje.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(Mensaje);

                cn.Open();

                cmd.ExecuteNonQuery();

                bool eliminado = Convert.ToBoolean(Eliminado.Value);
                string mensaje = Mensaje.Value.ToString();

                if (eliminado)
                {
                    return Ok(mensaje);
                }
                else
                {
                    return BadRequest(mensaje);
                }
            }
        }


    }
}
