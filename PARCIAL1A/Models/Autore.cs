using System;
using System.Collections.Generic;

namespace PARCIAL1A.Models;

public partial class Autore
{
    public int Autorid { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<AutorLibro> AutorLibros { get; set; } = new List<AutorLibro>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
