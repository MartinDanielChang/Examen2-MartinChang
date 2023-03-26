using System;
using System.Collections.Generic;
using Entidades;
using BaseDatos;
using System.Windows.Forms;

namespace Examen2
{
    public partial class TicketForm : Form
    {
        public TicketForm()
        {
            InitializeComponent();
        }

        Cliente miCliente = null;
        ClienteBD clienteDB = new ClienteBD();
        List<DetalleTicket> listaDetalles = new List<DetalleTicket>();
        TicketBD facturaDB = new TicketBD();
        decimal subTotal = 0;
        decimal isv = 0;
        decimal totalAPagar = 0;
        decimal descuento = 0;
        private object miFactura;

        private void IdentidadTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && !string.IsNullOrEmpty(IdentidadTextBox.Text))
            {
                miCliente = new Cliente();
                miCliente = clienteDB.DevolverClientePorIdentidad(IdentidadTextBox.Text);
                NombreClienteTextBox.Text = miCliente.Nombre;
            }
            else
            {
                miCliente = null;
                NombreClienteTextBox.Clear();
            }
        }



        private void TicketForm_Load(object sender, EventArgs e)
        {
            {
                UsuarioTextBox.Text = System.Threading.Thread.CurrentPrincipal.Identity.Name;
            }
        }

        private void DescripcionRespuestaTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && !string.IsNullOrEmpty(DescripcionRespuestaTextBox.Text))
            {
                DetalleTicket detalle = new DetalleTicket();
                Ticket Precio = new Ticket();
                detalle.TipoSoporte = TipoSoporteTextBox.Text;
                detalle.DescripcionSolicitud = DescripcionSolicitudTextBox.Text;
                detalle.DescripcionSolicitud = DescripcionRespuestaTextBox.Text;
                detalle.Precio = Convert.ToDecimal(PrecioTextBox.Text);

                subTotal += Precio.Total;
                isv = subTotal * 0.15M;
                totalAPagar = subTotal + isv;

                listaDetalles.Add(detalle);
                DetalleDataGridView.DataSource = null;
                DetalleDataGridView.DataSource = listaDetalles;

                SubTotalTextBox.Text = subTotal.ToString("N2");
                ISVTextBox.Text = isv.ToString("N2");
                TotalTextBox.Text = totalAPagar.ToString("N2");


            }
        }

        private void GuardarButton_Click(object sender, EventArgs e)
        {
            Ticket miTickect = new Ticket();
            miTickect.Fecha = FechaDateTimePicker1.Value;
            miTickect.CodigoUsuaurio = System.Threading.Thread.CurrentPrincipal.Identity.Name;
            miTickect.IdentidadCliente = miCliente.Identidad;
            miTickect.SubTotal = subTotal;
            miTickect.ISV = isv;
            miTickect.Descuento = descuento;
            miTickect.Total = totalAPagar;

            bool inserto = facturaDB.Guardar(miTickect, listaDetalles);

            if (inserto)
            {
                IdentidadTextBox.Focus();
                MessageBox.Show("Factura registrada exitosamente");
                LimpiarControles();
            }
            else
                MessageBox.Show("No se pudo registrar la factura");
        }

        private void DescuentoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && e.KeyChar != '\b')
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }

            if (e.KeyChar == (char)Keys.Enter && !string.IsNullOrEmpty(DescuentoTextBox.Text))
            {
                descuento = Convert.ToDecimal(DescuentoTextBox.Text);
                totalAPagar = totalAPagar - descuento;
                TotalTextBox.Text = totalAPagar.ToString();
            }
        }

        private void LimpiarControles()
        {
            miCliente = null;
            listaDetalles = null;
            FechaDateTimePicker1.Value = DateTime.Now;
            IdentidadTextBox.Clear();
            NombreClienteTextBox.Clear();
            DetalleDataGridView.DataSource = null;
            subTotal = 0;
            SubTotalTextBox.Clear();
            isv = 0;
            ISVTextBox.Clear();
            descuento = 0;
            DescuentoTextBox.Clear();
            totalAPagar = 0;
            TotalTextBox.Clear();
        }

        private void CancelarButton_Click(object sender, EventArgs e)
        {
            LimpiarControles();
        }
    }
}
    

