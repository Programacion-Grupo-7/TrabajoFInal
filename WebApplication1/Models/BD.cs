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
        private static string _connectionString = "Server=localhost;Database=Pagina de Musica;Trusted_Connection=True;";

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
            consulta.CommandText = "SP_RegistrarUsuario";
            consulta.CommandType = System.Data.CommandType.StoredProcedure;
            consulta.Parameters.AddWithValue("@Nombre", c.Nombre);
            consulta.Parameters.AddWithValue("@Apellido", c.Apellido);
            consulta.Parameters.AddWithValue("@NomUsuario", c.NombreUsuario);
            consulta.Parameters.AddWithValue("@mail", c.Correo);
            consulta.Parameters.AddWithValue("@Contraseña", c.Contraseña);
            int cambio = consulta.ExecuteNonQuery();
            Desconectar(conn);
            return cambio;
        }
        public static Usuario LogIn(Usuario c)
        {
            
            SqlConnection conn = Conectar();
            SqlCommand consulta = conn.CreateCommand();
            consulta.CommandText = "SP_Login";
            consulta.CommandType = System.Data.CommandType.StoredProcedure;
            consulta.Parameters.AddWithValue("@Usuario", c.NombreUsuario);
            consulta.Parameters.AddWithValue("@Contraseña", c.Contraseña);
            SqlDataReader dr = consulta.ExecuteReader();
            if(dr.HasRows)
            {
                Desconectar(conn);
                return c;
            }
            else
            {
                Desconectar(conn);
                return null;
            }
           
        }
        public static int IngresarMusica(Cancion c)
        {
            SqlConnection conn = Conectar();
            SqlCommand consulta = conn.CreateCommand();
            consulta.CommandText = "SP_InsertarAudio";
            consulta.CommandType = System.Data.CommandType.StoredProcedure;
            consulta.Parameters.AddWithValue("@Nombre", c.Nombre);
            consulta.Parameters.AddWithValue("@Ubicacion", c.Ubicacion);
            int cambio = consulta.ExecuteNonQuery();
            Desconectar(conn);
            return cambio;
        }
    }
}