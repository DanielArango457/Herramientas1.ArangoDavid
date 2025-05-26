using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabajo_final_Herramientas_1
{
    public class Transaccion
    {
        private string tipo;
        private DateTime fecha;
        private int cantidad;

        public Transaccion(string tipo, DateTime fecha, int cantidad)
        {
            this.tipo = tipo;
            this.fecha = fecha;
            this.cantidad = cantidad;
        }

        public string Tipo { get => tipo; set => tipo = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
    }
}