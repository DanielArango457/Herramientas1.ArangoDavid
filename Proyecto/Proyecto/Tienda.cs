using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabajo_final_Herramientas_1
{
    public class Tienda
    {
        private List<Libro> catalogo;
        private decimal dineroEnCaja;
        private Fabrica fabrica;

        public Tienda()
        {
            this.fabrica = new Fabrica();
            this.catalogo = new List<Libro>();
            fabrica.CrearLibros(catalogo);
            this.dineroEnCaja = 1000000M; // Inversión inicial
        }

        public List<Libro> Catalogo { get => catalogo; set => catalogo = value; }
        public decimal DineroEnCaja { get => dineroEnCaja; set => dineroEnCaja = value; }
    }
}
