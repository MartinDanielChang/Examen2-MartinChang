using System;


namespace Entidades
{
    public class Ticket
    {
        public int Id{ get; set; }
        public DateTime Fecha { get; set; }
        public string CodigoUsuaurio { get; set; }
        public string IdentidadCliente { get; set; }
        public decimal ISV { get; set; }
        public decimal Descuento { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }

        public Ticket()
        {
        }

        public Ticket(int id, DateTime fecha, string codigoUsuaurio, string identidadCliente,  decimal iSV, decimal descuento, decimal subTotal, decimal total)
        {
            Id = id;
            Fecha = fecha;
            CodigoUsuaurio = codigoUsuaurio;
            IdentidadCliente = identidadCliente;
            ISV = iSV;
            Descuento = descuento;
            SubTotal = subTotal;
            Total = total;
        }
    }
}
