using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Usuario
    {
        private int _IdUsuario;
        private string _Nombre;
        private string _Apellido;
        private string _NombreUsuario;
        private string _Contraseña;
        private string _Correo;
        private string _Telefono;


        public Usuario() { }

        public Usuario(int IdUsuario, string Nombre, string Apellido, string NombreUsuario, string Contraseña, string Correo, string Telefono)
        {
            _IdUsuario = IdUsuario;
            _Nombre = Nombre;
            _Apellido = Apellido;
            _NombreUsuario = NombreUsuario;
            _Contraseña = Contraseña;
            _Correo = Correo;
            _Telefono = Telefono;
        }

        public string Nombre
        {
            get
            {
                return _Nombre;
            }
            set
            {

                _Nombre = value;
            }
        }
        public int IdUsuario
        {
            get
            {
                return _IdUsuario;
            }
            set
            {

                _IdUsuario = value;
            }
        }

        public string Apellido
        {
            get
            {
                return _Apellido;
            }
            set
            {

                _Apellido = value;
            }
        }

        public string NombreUsuario
        {
            get
            {
                return _NombreUsuario;
            }
            set
            {
                _NombreUsuario = value;
            }
        }
        public string Contraseña
        {
            get
            {
                return _Contraseña;
            }
            set
            {
                _Contraseña  = value;
            }
        }
        public string Correo
        {
            get
            {
                return _Correo;
            }
            set
            {
                _Correo = value;
            }
        }
        public string Musica { get; set; }
        private Cancion NombreCan { get; set; }
    }
}