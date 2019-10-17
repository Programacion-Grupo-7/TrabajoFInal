using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WebApplication1.Models
{
    public static class BD
    {
        private static string _connectionString = "Server=.;Database=Pagina de Musica;Trusted_Connection=True;";

        public static string ConnectionString
        {
            get
            {
                return _connectionString;
            }
        }

        private static SqlConnection Conectar()
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            return conn;
        }

        public static void Desconectar(SqlConnection conn)
        {
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
        }

        public static List<Usuario> Obtener_Todos_Usuarios()
        {
            List<Usuario> lista = new List<Usuario>();
            SqlConnection conn = Conectar();
            SqlCommand consulta = conn.CreateCommand();
            consulta.CommandText = "SELECT Id, Nombre, Apellido, Nombre_Usuario, Contraseña, Correo FROM Usuarios";
            consulta.CommandType = System.Data.CommandType.Text;
            SqlDataReader dr = consulta.ExecuteReader();
            while (dr.Read())
            {
                int IdUsuario = Convert.ToInt32(dr["Id"]);
                string Nombre = dr["Nombre"].ToString();
                string Apellido = dr["Apellido"].ToString();
                string NombreUsuario = dr["Nombre_Usuario"].ToString();
                string Contraseña = dr["Contraseña"].ToString();
                string Correo = dr["Correo"].ToString();
                Usuario unUsuario = new Usuario(IdUsuario, Nombre, Apellido, NombreUsuario, Contraseña, Correo);
                lista.Add(unUsuario);
            }
            Desconectar(conn);
            return lista;
        }
        public static int Registrarse(Usuario c)
        {
            SqlConnection conn = Conectar();
            SqlCommand consulta = conn.CreateCommand();
            consulta.CommandText = "Registarse";
            consulta.CommandType = System.Data.CommandType.StoredProcedure;
            consulta.Parameters.AddWithValue("@Nombre", c.Nombre);
            consulta.Parameters.AddWithValue("@Apellido", c.Apellido);
            consulta.Parameters.AddWithValue("@Usuario", c.NombreUsuario);
            consulta.Parameters.AddWithValue("@Correo", c.Correo);
            consulta.Parameters.AddWithValue("@Contraseña", c.Contraseña);
            int cambio = consulta.ExecuteNonQuery();
            Desconectar(conn);
            return cambio;
        }
        public static int LogIn(Usuario c)
        {
            SqlConnection conn = Conectar();
            SqlCommand consulta = conn.CreateCommand();
            consulta.CommandText = "LogIn";
            consulta.CommandType = System.Data.CommandType.StoredProcedure;
            consulta.Parameters.AddWithValue("@Usuario", c.NombreUsuario);    
            consulta.Parameters.AddWithValue("@Contraseña", c.Contraseña);
            int cambio = consulta.ExecuteNonQuery();
            Desconectar(conn);
            return cambio;
        }
        public static int IngresarMusica(Cancion c)
        {
            SqlConnection conn = Conectar();
            SqlCommand consulta = conn.CreateCommand();
            consulta.CommandText = "InsertarMusica";
            consulta.CommandType = System.Data.CommandType.StoredProcedure;
            consulta.Parameters.AddWithValue("@Nombre", c.Nombre);
            consulta.Parameters.AddWithValue("@Artista", c.Artista);
            consulta.Parameters.AddWithValue("@Album", c.Album);
            consulta.Parameters.AddWithValue("@Ubicacion", c.Ubicacion);
            consulta.Parameters.AddWithValue("@id", c.id);
            int cambio = consulta.ExecuteNonQuery();
            Desconectar(conn);
            return cambio;
        }
    }
}