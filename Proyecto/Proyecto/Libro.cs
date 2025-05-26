using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabajo_final_Herramientas_1
{
    class Libro
    {
        private string isbn;
        private string titulo;
        private int precioCompra;
        private int precioVenta;
        private int cantidadActual;
        private List<Transaccion> transacciones;

        public Libro(string iSBN, string titulo, int precioCompra, int precioVenta, int cantidadActual, List<Transaccion> transacciones)
        {
            this.isbn = iSBN;
            this.titulo = titulo;
            this.precioCompra = precioCompra;
            this.precioVenta = precioVenta;
            this.cantidadActual = cantidadActual;
            this.transacciones = transacciones;
        }

        public string ISBN { get => isbn; set => ISBN = value; }
        public string Titulo { get => titulo; set => titulo = value; }
        public int PrecioCompra { get => precioCompra; set => precioCompra = value; }
        public int PrecioVenta { get => precioVenta; set => precioVenta = value; }
        public int CantidadActual { get => cantidadActual; set => cantidadActual = value; }
        internal List<Transaccion> Transacciones { get => transacciones; set => transacciones = value; }
    }
}
