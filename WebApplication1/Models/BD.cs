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
        private static string _connectionString = "Server=localhost;Database=Pagina de Musica;uid=alumno; pwd=alumno1;";

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
        public static Usuario ObtenerUsuario(int id)
        {
            Usuario unUsuario = null;
            SqlConnection conn = Conectar();
            SqlCommand consulta = conn.CreateCommand();
            consulta.CommandText = "SELECT Id, Nombre, Apellido, Nombre_Usuario, Contraseña, Correo FROM Usuarios where Id= " + id;
            consulta.CommandType = System.Data.CommandType.Text;
            consulta.Parameters.AddWithValue("@ID", id);
            SqlDataReader dr = consulta.ExecuteReader();
            if (dr.Read())
            {
                int Id = Convert.ToInt32(dr["id"]);
                string Nombre = dr["Nombre"].ToString();
                string Apellido = dr["Apellido"].ToString();
                string NombreUsuario = dr["Nombre_Usuario"].ToString();
                string Contraseña = dr["Contraseña"].ToString();
                string Correo = dr["Correo"].ToString();
                unUsuario = new Usuario(Id, Nombre, Apellido, NombreUsuario, Contraseña, Correo);
            }
            Desconectar(conn);
            return unUsuario;
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
            int u = consulta.ExecuteNonQuery();
            Desconectar(conn);
            return u;
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
            if (dr.Read())
            {

                int id = Convert.ToInt32(dr["id"]);
                string Nombre = dr["Nombre"].ToString();
                string Apellido = dr["Apellido"].ToString();
                string NombreUsuario = dr["Nombre_Usuario"].ToString();
                string Contraseña = dr["Contraseña"].ToString();
                string Correo = dr["Correo"].ToString();

                Usuario unusuario = new Usuario(id, Nombre, Apellido, NombreUsuario, Contraseña, Correo);
                Desconectar(conn);
                return unusuario;
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
            consulta.CommandText = "SP_InsertarCancion";
            consulta.CommandType = System.Data.CommandType.StoredProcedure;
            consulta.Parameters.AddWithValue("@Nombre", c.Nombre);
            consulta.Parameters.AddWithValue("@Ubicacion", c.Ubicacion);
            consulta.Parameters.AddWithValue("@Artista", c.Artista);
            int cambio = consulta.ExecuteNonQuery();
            Desconectar(conn);
            return cambio;
        }
        public static List<Cancion> TraerMusica()
        {
            List<Cancion> Lista = new List<Cancion>();
            SqlConnection conn = Conectar();
            SqlCommand consulta = conn.CreateCommand();
            consulta.CommandText = "SP_TraerMusica";
            consulta.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader dr = consulta.ExecuteReader();
            while (dr.Read())
            {
                int id = Convert.ToInt32(dr["id"]);
                string Nombre = dr["Nombre"].ToString();
                string Artista = dr["Artista"].ToString();
                string Ubicacion = dr["UbicacionCancion"].ToString();
                string Album = dr["IdAlbum"].ToString();
                string Genero = dr["Genero"].ToString();
                string Imagen = dr["UbicacionImagen"].ToString();
                Cancion unaCancion = new Cancion(Nombre, id, Artista, Ubicacion, Album, Genero, Imagen);
                Lista.Add(unaCancion);
            }
            Desconectar(conn);
            return Lista;
        }
        public static List<Cancion> TOP50()
        {
            List<Cancion> Lista = new List<Cancion>();
            SqlConnection conn = Conectar();
            SqlCommand consulta = conn.CreateCommand();
            consulta.CommandText = "SELECT TOP 50 Nombre,IdAlbum,UbicacionCancion,Artista,id,Genero,UbicacionImagen FROM CANCIONES";
            consulta.CommandType = System.Data.CommandType.Text;
            SqlDataReader dr = consulta.ExecuteReader();
            while (dr.Read())
            {
                int id = Convert.ToInt32(dr["id"]);
                string Nombre = dr["Nombre"].ToString();
                string Artista = dr["Artista"].ToString();
                string Ubicacion = dr["UbicacionCancion"].ToString();
                string Album = dr["IdAlbum"].ToString();
                string Genero = dr["Genero"].ToString();
                string Imagen = dr["UbicacionImagen"].ToString();
                Cancion unaCancion = new Cancion(Nombre, id, Artista, Ubicacion, Album, Genero, Imagen);
                Lista.Add(unaCancion);
            }
            Desconectar(conn);
            return Lista;
        }
        public static int EliminarUsuario(int id,Usuario u)
        {
            SqlConnection conn = Conectar();
            SqlCommand consulta = conn.CreateCommand();
            consulta.CommandText = "SP_EliminarUsuario";
            consulta.CommandType = System.Data.CommandType.StoredProcedure;
            consulta.Parameters.AddWithValue("@id", id);
            consulta.Parameters.AddWithValue("@NombreU", u.NombreUsuario);
           int cambio = consulta.ExecuteNonQuery();
            Desconectar(conn);
            return cambio;
        }
        public static int EliminarCancion(int id)
        {
            SqlConnection conn = Conectar();
            SqlCommand consulta = conn.CreateCommand();
            consulta.CommandText = "SP_EliminarCancion";
            consulta.CommandType = System.Data.CommandType.StoredProcedure;
            consulta.Parameters.AddWithValue("@id", id);
            int cambio = consulta.ExecuteNonQuery();
            Desconectar(conn);
            return cambio;
        }
        public static int EliminarFavorito(Cancion c,Usuario u)
        {
            SqlConnection conn = Conectar();
            SqlCommand consulta = conn.CreateCommand();
            consulta.CommandText = "DeleteFav";
            consulta.CommandType = System.Data.CommandType.StoredProcedure;
            consulta.Parameters.AddWithValue("@idCancion", c.id);
            consulta.Parameters.AddWithValue("@idUsuario", u.IdUsuario);
            int cambio = consulta.ExecuteNonQuery();
            Desconectar(conn);
            return cambio;
        }
        public static int EditarPerfil(Usuario c)
        {
            SqlConnection conn = Conectar();
            SqlCommand consulta = conn.CreateCommand();
            consulta.CommandText = "SP_EditarPerfil";
            consulta.CommandType = System.Data.CommandType.StoredProcedure;
            consulta.Parameters.AddWithValue("@id", c.IdUsuario);
            consulta.Parameters.AddWithValue("@Nombre", c.Nombre);
            consulta.Parameters.AddWithValue("@Apellido", c.Apellido);
            consulta.Parameters.AddWithValue("@Nombre_U", c.NombreUsuario);
            consulta.Parameters.AddWithValue("@correo", c.Correo);
            consulta.Parameters.AddWithValue("@Contraseña", c.Contraseña);
            int cambio = consulta.ExecuteNonQuery();
            Desconectar(conn); 
            return cambio;
            
        }
        public static List<Cancion> TraerMiMusica(Usuario u)
        {
            List<Cancion> Lista = new List<Cancion>();
            SqlConnection conn = Conectar();
            SqlCommand consulta = conn.CreateCommand();
            consulta.CommandText = "SP_TraerMiMusica";
            consulta.CommandType = System.Data.CommandType.StoredProcedure;
            consulta.Parameters.AddWithValue("@NombreU", u.NombreUsuario);
            SqlDataReader dr = consulta.ExecuteReader();
            while (dr.Read())
            {
                int id = Convert.ToInt32(dr["id"]);
                string Nombre = dr["Nombre"].ToString();
                string Artista = dr["Artista"].ToString();
                string Ubicacion = dr["UbicacionCancion"].ToString();
                string Album = dr["IdAlbum"].ToString();
                string Genero = dr["Genero"].ToString();
                string Imagen = dr["UbicacionImagen"].ToString();
                Cancion unaCancion = new Cancion(Nombre, id, Artista, Ubicacion, Album, Genero, Imagen);
                Lista.Add(unaCancion);
            }
            Desconectar(conn);
            return Lista;
        }
        public static void Favorito(Cancion c,Usuario u)
        {
            SqlConnection conn = Conectar();
            SqlCommand consulta = conn.CreateCommand();
            consulta.CommandText = "SP_Favoritos";
            consulta.CommandType = System.Data.CommandType.StoredProcedure;
            consulta.Parameters.AddWithValue("@id", c.id);
            consulta.Parameters.AddWithValue("@idUsuario", u.IdUsuario);
            consulta.ExecuteNonQuery();
            Desconectar(conn);
            
            
        }
        public static List<Cancion> Filtrar(string c)
        {
            List<Cancion> Lista = new List<Cancion>();
            SqlConnection conn = Conectar();
            SqlCommand consulta = conn.CreateCommand();
            consulta.CommandText = "SP_BuscarxNombre";
            consulta.CommandType = System.Data.CommandType.StoredProcedure;
            consulta.Parameters.AddWithValue("@Nombre", c);
            SqlDataReader dr = consulta.ExecuteReader();
            while (dr.Read())
            {
                int id = Convert.ToInt32(dr["Id"]);
                string Nombre = dr["Nombre"].ToString();
                string Artista = dr["Artista"].ToString();
                string Ubicacion = dr["UbicacionCancion"].ToString();
                string Album = dr["IdAlbum"].ToString();
                string Genero = dr["Genero"].ToString();
                string Imagen = dr["UbicacionImagen"].ToString();
                Cancion unaCancion = new Cancion(Nombre, id, Artista, Ubicacion, Album, Genero, Imagen);
                Lista.Add(unaCancion);
            }
            Desconectar(conn);
            return Lista;

        }
        public static List<Cancion> TraerFav(Usuario u)
        {
            List<Cancion> Lista = new List<Cancion>();
            SqlConnection conn = Conectar();
            SqlCommand consulta = conn.CreateCommand();
            consulta.CommandText = "Select * from Canciones inner join Favorito on Id_Canciones = Id where Id_Usuarios = " + u.IdUsuario;
            consulta.CommandType = System.Data.CommandType.Text;
            SqlDataReader dr = consulta.ExecuteReader();
            while (dr.Read())
            {
                int id = Convert.ToInt32(dr["Id"]);
                string Nombre = dr["Nombre"].ToString();
                string Artista = dr["Artista"].ToString();
                string Ubicacion = dr["UbicacionCancion"].ToString();
                string Album = dr["IdAlbum"].ToString();
                string Genero = dr["Genero"].ToString();
                string Imagen = dr["UbicacionImagen"].ToString();
                Cancion unaCancion = new Cancion(Nombre, id, Artista, Ubicacion, Album, Genero, Imagen);
                Lista.Add(unaCancion);
            }
            Desconectar(conn);
            return Lista;
        }
    }
}

    