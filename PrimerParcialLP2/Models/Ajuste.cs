using System;
using System.Collections.Generic;

namespace PrimerParcialLP2.Models;

public partial class Ajuste
{
    public int AjusteId { get; set; }

    public int ProductoId { get; set; }

    public int AlmacenId { get; set; }

    public int Cantidad { get; set; }

    public DateTime Fecha { get; set; }

    public string Tipo { get; set; } = null!;

    public virtual Almacen Almacen { get; set; } = null!;

    public virtual Producto Producto { get; set; } = null!;
}
