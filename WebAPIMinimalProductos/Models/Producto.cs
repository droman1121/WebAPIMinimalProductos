﻿namespace WebAPIMinimalProductos.Models
{
    public class Producto
    {
        public int ID { get; set; }
        public string Nombre { get; set; }

        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
    }
}
