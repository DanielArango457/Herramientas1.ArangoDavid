using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabajo_final_Herramientas_1
{
    class Tienda
    {
        private List<Libro> catalogo;
        private int dineroEnCaja;
        private Fabrica fabrica;

        public Tienda()
        {
            this.fabrica = new Fabrica();
            this.catalogo = new List<Libro>();
            fabrica.CrearLibros(catalogo);
            this.DineroEnCaja = 1000000;
        }

        public int DineroEnCaja { get => dineroEnCaja; set => dineroEnCaja = value; }
        internal List<Libro> Catalogo { get => catalogo; set => catalogo = value; }
    }
}
