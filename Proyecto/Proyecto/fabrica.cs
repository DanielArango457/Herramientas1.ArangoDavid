using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabajo_final_Herramientas_1
{
    class Fabrica
    {

        List<string> titulos = new List<string>
        {
            "Cien años de soledad",
            "Don Quijote de la Mancha",
            "La sombra del viento",
            "Rayuela",
            "El amor en los tiempos del cólera",
            "Pedro Páramo",
            "Los detectives salvajes",
            "Como agua para chocolate",
            "Crónica de una muerte anunciada",
            "Fervor de Buenos Aires",
            "La ciudad y los perros",
            "El túnel",
            "La casa de los espíritus",
            "Sobre héroes y tumbas",
            "La tregua",
            "Pantaleón y las visitadoras",
            "Ensayo sobre la ceguera",
            "El coronel no tiene quien le escriba",
            "Aura",
            "La región más transparente"
        };



        public void CrearLibros(List<Libro> libros)
        {
            Random rnd = new Random();


            for (int i = 0; i < titulos.Count; i++)
            {
                string isbn = $"ISBN{i + 1:D3}";
                int precioCompra = rnd.Next(20000, 40001);
                int precioVenta = rnd.Next(45000, 70001);
                int cantidadActual = rnd.Next(5, 21);

                libros.Add(new Libro(isbn, titulos[i], precioCompra, precioVenta, cantidadActual, new List<Transaccion>()));
            }


        }


    }

}
