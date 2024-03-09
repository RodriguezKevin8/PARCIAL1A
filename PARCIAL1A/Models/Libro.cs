using System;
using System.Collections.Generic;

namespace PARCIAL1A.Models;

public partial class Libro
{
    public int Libroid { get; set; }

    public string? Titulo { get; set; }

    public virtual ICollection<AutorLibro> AutorLibros { get; set; } = new List<AutorLibro>();
}
