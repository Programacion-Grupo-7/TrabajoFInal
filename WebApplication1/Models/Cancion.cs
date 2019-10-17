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

        public Cancion(){}
        public Cancion(string Nombre, int id, string Artista, string Ubicacion, string Album)
        {
            _Nombre = Nombre;
            _id = id;
            _Artista = Artista;
            _Ubicacion = Ubicacion;
            _Album = Album;
        }

        public string Nombre { get; set; }
        public int id { get; set; }
        public string Artista { get; set; }
        public string Ubicacion { get; set; }
        public string Album { get; set; }

    }
}