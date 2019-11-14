using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Cancion
    {
        private string _Nombre;
        private int _id;
        private string _Artista;
        private string _Ubicacion;
        private string _Album;
        private string _Genero;
        private string _Imagen;

        
        public Cancion() { }

        public Cancion(string Nombre, int id, string Artista, string Ubicacion, string Album, string Genero, string Imagen)
        {
            _Nombre = Nombre;
            _id = id;
            _Artista = Artista;
            _Ubicacion = Ubicacion;
            _Album = Album;
            _Genero = Genero;
            _Imagen = Imagen;
        }

        public string Ubicacion {
            get
            {
                return _Ubicacion;
            }
            set
            {
                _Ubicacion = value;
            }
        }
        public string Album
        {
            get
            {
                return _Album;
            }
            set
            {
                _Album = value;
            }
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
        public int id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        public string Artista
        {
            get
            {
                return _Artista;
            }
            set
            {
                _Artista = value;
            }
        }
        public string Genero
        {
            get
            {
                return _Genero;
            }
            set
            {
                _Genero = value;
            }
        }
        public string Imagen
        {
            get
            {
                return _Imagen;
            }
            set
            {
                _Imagen = value;
            }
        }
    }
}