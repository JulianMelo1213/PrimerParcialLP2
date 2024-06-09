using System;
using System.Collections.Generic;

namespace PrimerParcialLP2.Models;

public partial class Salidum
{
    public int SalidaId { get; set; }

    public int ProductoId { get; set; }

    public int Cantidad { get; set; }

    public DateTime Fecha { get; set; }

    public virtual Producto Producto { get; set; } = null!;
}
