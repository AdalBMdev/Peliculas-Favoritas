using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Peliculas.Models;


namespace Peliculas.Controllers
{
    public class FiltroController : Controller
    {

        private readonly string cadena = @"Server=ADALBERTO; Database=DB_peliculas; Trusted_Connection=True;"; //Conexion a bdd

        [HttpGet("filtroTitulo/{filtro}/{id}/{busqueda}")]
        public IActionResult FiltroTitulo(int filtro,int id, string busqueda)
        {
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand();

               
                if (filtro == 2)
                {
                    cmd.CommandText = "sp_FiltrarPorTitulo";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDUsuario", id);
                    cmd.Parameters.AddWithValue("@Titulo", busqueda);
                }
                if (filtro == 3)
                {

                    string fechaParse = $"{busqueda}-01-01";
                    DateTime fecha = DateTime.Parse(fechaParse);
                        cmd.CommandText = "sp_FiltrarPorAño";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IDUsuario", id);
                        cmd.Parameters.AddWithValue("@Año", fecha);
                }
                if (filtro == 4)
                {
                    cmd.CommandText = "sp_FiltrarPorDirector";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDUsuario", id);
                    cmd.Parameters.AddWithValue("@Director", busqueda);
                }
                if (filtro == 5)
                {
                    cmd.CommandText = "sp_MostrarPeliculasPorGenero";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDUsuario", id);
                    cmd.Parameters.AddWithValue("@Genero", busqueda);
                }

                cmd.Connection = cn;
                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                List<Pelicula> peliculas = new List<Pelicula>();

                while (dr.Read())
                {
                    Pelicula pelicula = new Pelicula();

                    pelicula.Idpeliculas = Convert.ToInt32(dr["IDPeliculas"]);
                    pelicula.Idusuario = Convert.ToInt32(dr["IDUsuario"]);
                    pelicula.Titulo = dr["Titulo"].ToString();
                    pelicula.Año = Convert.ToDateTime(dr["Año"]);
                    pelicula.Director = dr["Director"].ToString();
                    pelicula.Genero = dr["Genero"].ToString(); 
                    pelicula.Poster = dr["Poster"].ToString();

                    peliculas.Add(pelicula);
                }

                cn.Close();

                return Ok(peliculas);
            }
        }

    }
}
