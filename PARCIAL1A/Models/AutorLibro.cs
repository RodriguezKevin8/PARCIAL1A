using System;
using System.Collections.Generic;

namespace PARCIAL1A.Models;

public partial class AutorLibro
{
    public int Id { get; set; }

    public int? Autorid { get; set; }

    public int? Libroid { get; set; }

    public int? Orden { get; set; }

    public virtual Autore? Autor { get; set; }

    public virtual Libro? Libro { get; set; }
}
