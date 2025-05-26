using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabajo_final_Herramientas_1
{
    public class Servicios
    {
        public Tienda tienda;

        public Servicios()
        {
            this.tienda = new Tienda();
        }

        internal Tienda Tienda { get => tienda; set => tienda = value; }

        public void AgregarLibro(Libro libro)
        {
            if (libroExiste(libro.ISBN))
            {
                throw new Exception("El libro ya existe en el catálogo.");
            }
            tienda.Catalogo.Add(libro);
        }

        public void EliminarLibro(string isbn)
        {
            var libro = BuscarPorISBN(isbn);
            if (libro == null)
            {
                throw new Exception("El libro no existe en el catálogo.");
            }
            tienda.Catalogo.Remove(libro);
        }

        public Libro BuscarPorTitulo(string titulo)
        {
            return tienda.Catalogo.FirstOrDefault(l => l.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase));
        }

        public Libro BuscarPorISBN(string isbn)
        {
            return tienda.Catalogo.FirstOrDefault(l => l.ISBN == isbn);
        }

        public void AbastecerLibro(string isbn, int cantidad)
        {
            var libro = BuscarPorISBN(isbn);
            if (libro == null)
            {
                throw new Exception("El libro no existe en el catálogo.");
            }
            libro.CantidadActual += cantidad;
            libro.Transacciones.Add(new Transaccion("Abastecimiento", DateTime.Now, cantidad));
        }

        public void VenderLibro(string isbn, int cantidadVendida)
        {
            var libro = BuscarPorISBN(isbn);
            if (libro == null)
            {
                throw new Exception("El libro no existe en el catálogo.");
            }
            if (libro.CantidadActual < cantidadVendida)
            {
                throw new Exception("No hay suficientes ejemplares para vender.");
            }
            libro.CantidadActual -= cantidadVendida;
            tienda.DineroEnCaja += libro.PrecioVenta * cantidadVendida;
            libro.Transacciones.Add(new Transaccion("Venta", DateTime.Now, cantidadVendida));
        }

        private bool libroExiste(string isbn)
        {
            return tienda.Catalogo.Any(l => l.ISBN == isbn);
        }

        public int CalcularTransaccionesAbastecimiento(string isbn)
        {
            var libro = BuscarPorISBN(isbn);
            if (libro == null)
            {
                throw new Exception("El libro no existe en el catálogo.");
            }
            return libro.Transacciones.Count(t => t.Tipo == "Abastecimiento");
        }

        public Libro BuscarLibroMasCostoso()
        {
            if (!tienda.Catalogo.Any())
            {
                return null;
            }
            return tienda.Catalogo.OrderByDescending(l => l.PrecioVenta).First();
        }

        public Libro BuscarLibroMenosCostoso()
        {
            if (!tienda.Catalogo.Any())
            {
                return null;
            }
            return tienda.Catalogo.OrderBy(l => l.PrecioVenta).First();
        }

        public Libro BuscarLibroMasVendido()
        {
            if (!tienda.Catalogo.Any())
            {
                return null;
            }
            return tienda.Catalogo
                .OrderByDescending(l => l.Transacciones.Count(t => t.Tipo == "Venta"))
                .First();
        }

        public void mostrarLibros()
        {
            if (tienda.Catalogo.Count == 0)
            {
                Console.WriteLine("No hay libros en el catálogo.");
                return;
            }
            foreach (Libro libro in tienda.Catalogo)
            {
                Console.WriteLine("ISBN: " + libro.ISBN);
                Console.WriteLine("Título: " + libro.Titulo);
                Console.WriteLine("Precio de compra: " + libro.PrecioCompra);
                Console.WriteLine("Precio de venta: " + libro.PrecioVenta);
                Console.WriteLine("Cantidad actual: " + libro.CantidadActual);
                Console.WriteLine();
            }
        }
    }
}