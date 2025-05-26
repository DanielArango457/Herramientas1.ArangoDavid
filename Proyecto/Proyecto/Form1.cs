using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Drawing;

namespace Trabajo_final_Herramientas_1
{
    public partial class Form1 : Form
    {
        private Servicios servicios;
        private decimal cajaActual = 1000000M; // Inversión inicial
        private Panel contentPanel;

        public Form1()
        {
            InitializeComponent();
            servicios = new Servicios();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ConfigurarInterfaz();
        }

        private void ConfigurarInterfaz()
        {
            this.MinimumSize = new Size(1000, 600);

            // Panel principal
            TableLayoutPanel mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Color.WhiteSmoke
            };

            mainPanel.ColumnStyles.Clear();
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 250F));
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            // Panel de botones
            Panel leftPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(45, 45, 48),
                Padding = new Padding(10)
            };

            FlowLayoutPanel buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoSize = true,
                Padding = new Padding(5),
                BackColor = Color.FromArgb(45, 45, 48)
            };

            // Panel de contenido
            contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BackColor = Color.White
            };

            // Agregar botones
            string[] opciones = new string[]
            {
                "Registrar Libro",
                "Eliminar Libro",
                "Buscar por Título",
                "Buscar por ISBN",
                "Abastecer Ejemplares",
                "Vender Ejemplares",
                "Ver Transacciones de Abastecimiento",
                "Buscar Libro más Costoso",
                "Buscar Libro menos Costoso",
                "Buscar Libro más Vendido",
                "Ver Caja Actual"
            };

            foreach (string opcion in opciones)
            {
                Button btn = new Button
                {
                    Text = opcion,
                    Width = 220,
                    Height = 45,
                    Margin = new Padding(0, 0, 0, 10),
                    BackColor = Color.FromArgb(0, 122, 204),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                    TextAlign = ContentAlignment.MiddleLeft,
                    Padding = new Padding(10, 0, 0, 0)
                };

                btn.FlatAppearance.BorderSize = 0;
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 151, 251);
                btn.Click += (s, e) => ManejarClick(opcion, contentPanel);
                buttonPanel.Controls.Add(btn);
            }

            leftPanel.Controls.Add(buttonPanel);
            mainPanel.Controls.Add(leftPanel, 0, 0);
            mainPanel.Controls.Add(contentPanel, 1, 0);
            this.Controls.Add(mainPanel);
        }

        private void ManejarClick(string opcion, Panel contentPanel)
        {
            contentPanel.Controls.Clear();

            switch (opcion)
            {
                case "Registrar Libro":
                    MostrarFormularioRegistro(contentPanel);
                    break;
                case "Eliminar Libro":
                    MostrarFormularioEliminar(contentPanel);
                    break;
                case "Buscar por Título":
                    MostrarFormularioBusquedaTitulo(contentPanel);
                    break;
                case "Buscar por ISBN":
                    MostrarFormularioBusquedaISBN(contentPanel);
                    break;
                case "Abastecer Ejemplares":
                    MostrarFormularioAbastecer(contentPanel);
                    break;
                case "Vender Ejemplares":
                    MostrarFormularioVender(contentPanel);
                    break;
                case "Ver Transacciones de Abastecimiento":
                    MostrarTransaccionesAbastecimiento(contentPanel);
                    break;
                case "Buscar Libro más Costoso":
                    MostrarLibroMasCostoso(contentPanel);
                    break;
                case "Buscar Libro menos Costoso":
                    MostrarLibroMenosCostoso(contentPanel);
                    break;
                case "Buscar Libro más Vendido":
                    MostrarLibroMasVendido(contentPanel);
                    break;
                case "Ver Caja Actual":
                    MostrarCajaActual(contentPanel);
                    break;
            }
        }

        private void MostrarFormularioRegistro(Panel panel)
        {
            TableLayoutPanel form = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                ColumnCount = 2,
                RowCount = 6,
                Height = 300
            };

            // Campos del formulario
            string[] labels = { "ISBN:", "Título:", "Precio Compra:", "Precio Venta:", "Cantidad:" };
            TextBox[] textBoxes = new TextBox[5];

            for (int i = 0; i < labels.Length; i++)
            {
                form.Controls.Add(new Label { Text = labels[i] }, 0, i);
                textBoxes[i] = new TextBox { Width = 200 };
                form.Controls.Add(textBoxes[i], 1, i);
            }

            Button btnRegistrar = new Button
            {
                Text = "Registrar Libro",
                Width = 120,
                Height = 30
            };
            btnRegistrar.Click += (s, e) =>
            {
                try
                {
                    string isbn = textBoxes[0].Text;
                    string titulo = textBoxes[1].Text;
                    int precioCompra = int.Parse(textBoxes[2].Text);
                    int precioVenta = int.Parse(textBoxes[3].Text);
                    int cantidad = int.Parse(textBoxes[4].Text);

                    Libro nuevoLibro = new Libro(isbn, titulo, precioCompra, precioVenta, cantidad, new List<Transaccion>());
                    servicios.AgregarLibro(nuevoLibro);
                    MessageBox.Show("Libro registrado exitosamente");

                    // Actualizar caja
                    cajaActual -= precioCompra * cantidad;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al registrar libro: " + ex.Message);
                }
            };

            form.Controls.Add(btnRegistrar, 1, 5);
            panel.Controls.Add(form);
        }

        private void MostrarFormularioEliminar(Panel panel)
        {
            TableLayoutPanel form = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                ColumnCount = 2,
                RowCount = 2,
                Height = 100
            };

            form.Controls.Add(new Label { Text = "ISBN:" }, 0, 0);
            TextBox txtISBN = new TextBox { Width = 200 };
            form.Controls.Add(txtISBN, 1, 0);

            Button btnEliminar = new Button
            {
                Text = "Eliminar Libro",
                Width = 120,
                Height = 30
            };
            btnEliminar.Click += (s, e) =>
            {
                try
                {
                    servicios.EliminarLibro(txtISBN.Text);
                    MessageBox.Show("Libro eliminado exitosamente");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar libro: " + ex.Message);
                }
            };

            form.Controls.Add(btnEliminar, 1, 1);
            panel.Controls.Add(form);
        }

        private void MostrarFormularioBusquedaTitulo(Panel panel)
        {
            TableLayoutPanel form = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                ColumnCount = 2,
                RowCount = 2,
                Height = 100
            };

            form.Controls.Add(new Label { Text = "Título:" }, 0, 0);
            TextBox txtTitulo = new TextBox { Width = 200 };
            form.Controls.Add(txtTitulo, 1, 0);

            Button btnBuscar = new Button
            {
                Text = "Buscar",
                Width = 120,
                Height = 30
            };
            btnBuscar.Click += (s, e) =>
            {
                try
                {
                    var libro = servicios.BuscarPorTitulo(txtTitulo.Text);
                    if (libro != null)
                    {
                        MostrarInformacionLibro(libro, panel);
                    }
                    else
                    {
                        MessageBox.Show("Libro no encontrado");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error en la búsqueda: " + ex.Message);
                }
            };

            form.Controls.Add(btnBuscar, 1, 1);
            panel.Controls.Add(form);
        }

        private void MostrarFormularioBusquedaISBN(Panel panel)
        {
            TableLayoutPanel form = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                ColumnCount = 2,
                RowCount = 2,
                Height = 100
            };

            form.Controls.Add(new Label { Text = "ISBN:" }, 0, 0);
            TextBox txtISBN = new TextBox { Width = 200 };
            form.Controls.Add(txtISBN, 1, 0);

            Button btnBuscar = new Button
            {
                Text = "Buscar",
                Width = 120,
                Height = 30
            };
            btnBuscar.Click += (s, e) =>
            {
                try
                {
                    var libro = servicios.BuscarPorISBN(txtISBN.Text);
                    if (libro != null)
                    {
                        MostrarInformacionLibro(libro, panel);
                    }
                    else
                    {
                        MessageBox.Show("Libro no encontrado");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error en la búsqueda: " + ex.Message);
                }
            };

            form.Controls.Add(btnBuscar, 1, 1);
            panel.Controls.Add(form);
        }

        private void MostrarFormularioAbastecer(Panel panel)
        {
            TableLayoutPanel form = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                ColumnCount = 2,
                RowCount = 3,
                Height = 150
            };

            form.Controls.Add(new Label { Text = "ISBN:" }, 0, 0);
            TextBox txtISBN = new TextBox { Width = 200 };
            form.Controls.Add(txtISBN, 1, 0);

            form.Controls.Add(new Label { Text = "Cantidad:" }, 0, 1);
            TextBox txtCantidad = new TextBox { Width = 200 };
            form.Controls.Add(txtCantidad, 1, 1);

            Button btnAbastecer = new Button
            {
                Text = "Abastecer",
                Width = 120,
                Height = 30
            };
            btnAbastecer.Click += (s, e) =>
            {
                try
                {
                    string isbn = txtISBN.Text;
                    int cantidad = int.Parse(txtCantidad.Text);
                    var libro = servicios.BuscarPorISBN(isbn);

                    if (libro != null)
                    {
                        decimal costoTotal = libro.PrecioCompra * cantidad;
                        if (costoTotal <= cajaActual)
                        {
                            servicios.AbastecerLibro(isbn, cantidad);
                            cajaActual -= costoTotal;
                            MessageBox.Show("Abastecimiento realizado exitosamente");
                        }
                        else
                        {
                            MessageBox.Show("No hay suficiente dinero en caja para realizar el abastecimiento");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Libro no encontrado");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al abastecer: " + ex.Message);
                }
            };

            form.Controls.Add(btnAbastecer, 1, 2);
            panel.Controls.Add(form);
        }

        private void MostrarFormularioVender(Panel panel)
        {
            TableLayoutPanel form = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                ColumnCount = 2,
                RowCount = 3,
                Height = 150
            };

            form.Controls.Add(new Label { Text = "ISBN:" }, 0, 0);
            TextBox txtISBN = new TextBox { Width = 200 };
            form.Controls.Add(txtISBN, 1, 0);

            form.Controls.Add(new Label { Text = "Cantidad:" }, 0, 1);
            TextBox txtCantidad = new TextBox { Width = 200 };
            form.Controls.Add(txtCantidad, 1, 1);

            Button btnVender = new Button
            {
                Text = "Vender",
                Width = 120,
                Height = 30
            };
            btnVender.Click += (s, e) =>
            {
                try
                {
                    string isbn = txtISBN.Text;
                    int cantidad = int.Parse(txtCantidad.Text);
                    var libro = servicios.BuscarPorISBN(isbn);

                    if (libro != null && libro.CantidadActual >= cantidad)
                    {
                        servicios.VenderLibro(isbn, cantidad);
                        cajaActual += libro.PrecioVenta * cantidad;
                        MessageBox.Show("Venta realizada exitosamente");
                    }
                    else
                    {
                        MessageBox.Show("No hay suficientes ejemplares disponibles");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al vender: " + ex.Message);
                }
            };

            form.Controls.Add(btnVender, 1, 2);
            panel.Controls.Add(form);
        }

        private void MostrarTransaccionesAbastecimiento(Panel panel)
        {
            TableLayoutPanel form = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                ColumnCount = 2,
                RowCount = 2,
                Height = 100
            };

            form.Controls.Add(new Label { Text = "ISBN:" }, 0, 0);
            TextBox txtISBN = new TextBox { Width = 200 };
            form.Controls.Add(txtISBN, 1, 0);

            Button btnBuscar = new Button
            {
                Text = "Ver Transacciones",
                Width = 120,
                Height = 30
            };
            btnBuscar.Click += (s, e) =>
            {
                try
                {
                    var libro = servicios.BuscarPorISBN(txtISBN.Text);
                    if (libro != null)
                    {
                        int cantidadTransacciones = libro.Transacciones.Count(t => t.Tipo == "Abastecimiento");
                        MessageBox.Show($"Cantidad de transacciones de abastecimiento: {cantidadTransacciones}");
                    }
                    else
                    {
                        MessageBox.Show("Libro no encontrado");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            };

            form.Controls.Add(btnBuscar, 1, 1);
            panel.Controls.Add(form);
        }

        private void MostrarLibroMasCostoso(Panel panel)
        {
            try
            {
                var libro = servicios.BuscarLibroMasCostoso();
                if (libro != null)
                {
                    MostrarInformacionLibro(libro, panel);
                }
                else
                {
                    MessageBox.Show("No hay libros en el catálogo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void MostrarLibroMenosCostoso(Panel panel)
        {
            try
            {
                var libro = servicios.BuscarLibroMenosCostoso();
                if (libro != null)
                {
                    MostrarInformacionLibro(libro, panel);
                }
                else
                {
                    MessageBox.Show("No hay libros en el catálogo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void MostrarLibroMasVendido(Panel panel)
        {
            try
            {
                var libro = servicios.BuscarLibroMasVendido();
                if (libro != null)
                {
                    MostrarInformacionLibro(libro, panel);
                }
                else
                {
                    MessageBox.Show("No hay libros en el catálogo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void MostrarCajaActual(Panel panel)
        {
            Label lblCaja = new Label
            {
                Text = $"Dinero en caja: ${cajaActual:N2}",
                Dock = DockStyle.Top,
                Height = 30,
                Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold)
            };
            panel.Controls.Add(lblCaja);
        }

        private void MostrarInformacionLibro(Libro libro, Panel panel)
        {
            TableLayoutPanel info = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                ColumnCount = 2,
                RowCount = 6,
                Height = 200
            };

            string[] labels = {
                "ISBN:",
                "Título:",
                "Precio Compra:",
                "Precio Venta:",
                "Cantidad Actual:",
                "Transacciones:"
            };

            string[] valores = {
                libro.ISBN,
                libro.Titulo,
                $"${libro.PrecioCompra:N2}",
                $"${libro.PrecioVenta:N2}",
                libro.CantidadActual.ToString(),
                libro.Transacciones.Count.ToString()
            };

            for (int i = 0; i < labels.Length; i++)
            {
                info.Controls.Add(new Label { Text = labels[i] }, 0, i);
                info.Controls.Add(new Label { Text = valores[i] }, 1, i);
            }

            panel.Controls.Add(info);
        }
    }
}