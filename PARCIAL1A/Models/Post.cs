using System;
using System.Collections.Generic;

namespace PARCIAL1A.Models;

public partial class Post
{
    public int Postid { get; set; }

    public string? Titulo { get; set; }

    public string? Contenido { get; set; }

    public DateTime? Fechapublicacion { get; set; }

    public int? Autorid { get; set; }

    public virtual Autore? Autor { get; set; }
}
