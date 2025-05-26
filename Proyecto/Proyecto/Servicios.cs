using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabajo_final_Herramientas_1
{
    class Servicios
    {
        public Tienda tienda;


        public Servicios()
        {
            this.tienda = new Tienda();
        }

        internal Tienda Tienda { get => tienda; set => tienda = value; }

        public void registrarLibro(Libro libro)
        {
            if (libroExiste(libro.ISBN))
            {
                Console.WriteLine("El libro ya existe en el catálogo.");
                return;
            }
            tienda.Catalogo.Add(libro);
            Console.WriteLine("Libro registrado con éxito.");

        }

        public void eliminarLibro(Libro libro)
        {
            if (!libroExiste(libro.ISBN))
            {
                Console.WriteLine("El libro no existe en el catálogo.");
                return;
            }
            tienda.Catalogo.Remove(libro);
            Console.WriteLine("Libro eliminado con éxito.");
        }

        public Libro buscarLibroPorTitulo(string titulo)
        {
            foreach (Libro libro in tienda.Catalogo)
            {
                if (libro.Titulo == titulo)
                {
                    return libro;
                }
            }
            Console.WriteLine("El libro no existe en el catálogo.");
            return null;
        }

        public Libro buscarLibroPorISBN(string isbn)
        {
            foreach (Libro libro in tienda.Catalogo)
            {
                if (libro.ISBN == isbn)
                {
                    return libro;
                }
            }
            Console.WriteLine("El libro no existe en el catálogo.");
            return null;
        }

        public void abastecerEjemplaresLibro(string isbn, int cantidad)
        {
            if (!libroExiste(isbn))
            {
                Console.WriteLine("El libro no existe en el catálogo.");
                return;
            }
            Libro libro = buscarLibroPorISBN(isbn);
            libro.CantidadActual += cantidad;
            Transaccion transaccion = new Transaccion("abastecimiento", new DateTime(), cantidad);
            libro.Transacciones.Add(transaccion);
        }

        public void venderEjemplaresLibro(Libro libro, int cantidadVendida)
        {
            if (!libroExiste(libro.ISBN))
            {
                Console.WriteLine("El libro no existe en el catálogo.");
                return;
            }
            if (libro.CantidadActual < cantidadVendida)
            {
                Console.WriteLine("No hay suficientes ejemplares para vender.");
                return;
            }
            libro.CantidadActual -= cantidadVendida;
            tienda.DineroEnCaja += libro.PrecioVenta * cantidadVendida;
            Transaccion transaccion = new Transaccion("venta", new DateTime(), cantidadVendida);
            libro.Transacciones.Add(transaccion);
        }

        private bool libroExiste(string isbn)
        {
            foreach (Libro libro in tienda.Catalogo)
            {
                if (libro.ISBN == isbn)
                {
                    return true;
                }
            }
            return false;
        }

        public int calcularCantAbastecimiento(Libro libro)
        {

            if (!libroExiste(libro.ISBN))
            {
                Console.WriteLine("El libro no existe en el catálogo.");
                return 0;
            }
            int abastecimiento = 0;
            foreach (Transaccion transaccion in libro.Transacciones)
            {
                if (transaccion.Tipo == "abastecimiento")
                {
                    abastecimiento += 1;
                }
            }
            return abastecimiento;

        }

        public int calcularVentasPorLibro(Libro libro)
        {
            if (!libroExiste(libro.ISBN))
            {
                Console.WriteLine("El libro no existe en el catálogo.");
                return 0;
            }
            int ventas = 0;
            foreach (Transaccion transaccion in libro.Transacciones)
            {
                if (transaccion.Tipo == "venta")
                {
                    ventas += 1;
                }
            }
            return ventas;
        }

        public Libro buscarMasCostoso()
        {
            if (tienda.Catalogo.Count == 0)
            {
                Console.WriteLine("No hay libros en el catálogo.");
                return null;
            }
            Libro libroMasCostoso = tienda.Catalogo[0];
            foreach (Libro libro in tienda.Catalogo)
            {
                if (libro.PrecioVenta > libroMasCostoso.PrecioVenta) libroMasCostoso = libro;

            }
            return libroMasCostoso;
        }

        public Libro buscarMenosCostoso()
        {
            if (tienda.Catalogo.Count == 0)
            {
                Console.WriteLine("No hay libros en el catálogo.");
                return null;
            }
            Libro libroMenosCostoso = tienda.Catalogo[0];
            foreach (Libro libro in tienda.Catalogo)
            {
                if (libro.PrecioVenta < libroMenosCostoso.PrecioVenta) libroMenosCostoso = libro;
            }
            return libroMenosCostoso;
        }

        public Libro buscarLibroMasVendido()
        {
            if (tienda.Catalogo.Count == 0)
            {
                Console.WriteLine("No hay libros en el catálogo.");
                return null;
            }
            Libro libroMasVendido = tienda.Catalogo[0];
            int ventasLibroMasVendido = calcularVentasPorLibro(libroMasVendido);

            foreach (Libro libro in tienda.Catalogo)
            {
                int ventas = calcularVentasPorLibro(libro);
                if (ventas > ventasLibroMasVendido)
                {
                    libroMasVendido = libro;
                }
            }
            return libroMasVendido;
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