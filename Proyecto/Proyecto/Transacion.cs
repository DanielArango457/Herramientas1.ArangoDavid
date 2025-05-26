using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabajo_final_Herramientas_1
{
    class Transaccion
    {
        private string tipo;
        private DateTime fechaRealizacion;
        private int cantidadEjemplares;

        public Transaccion(string tipo, DateTime fechaRealizacion, int cantidadEjemplares)
        {
            this.tipo = tipo;
            this.fechaRealizacion = fechaRealizacion;
            this.cantidadEjemplares = cantidadEjemplares;
        }

        public string Tipo { get => tipo; set => tipo = value; }
        public DateTime FechaRealizacion { get => fechaRealizacion; set => fechaRealizacion = value; }
        public int CantidadEjemplares { get => cantidadEjemplares; set => cantidadEjemplares = value; }
    }
}