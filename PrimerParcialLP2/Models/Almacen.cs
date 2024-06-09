using System;
using System.Collections.Generic;

namespace PrimerParcialLP2.Models;

public partial class Almacen
{
    public int AlmacenId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Ubicacion { get; set; }

    public virtual ICollection<Ajuste> Ajustes { get; set; } = new List<Ajuste>();

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();
}
