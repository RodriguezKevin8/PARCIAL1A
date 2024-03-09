using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PARCIAL1A.Models;

public partial class Parcial1aContext : DbContext
{
    public Parcial1aContext()
    {
    }

    public Parcial1aContext(DbContextOptions<Parcial1aContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AutorLibro> AutorLibros { get; set; }

    public virtual DbSet<Autore> Autores { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-5SK7M5O; Database=PARCIAL1A; Trusted_Connection=True; TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AutorLibro>(entity =>
        {
            entity.HasKey(e => new { e.Autorid, e.Libroid });

            entity.ToTable("AutorLibro");

            entity.Property(e => e.Autorid).HasColumnName("autorid");
            entity.Property(e => e.Libroid).HasColumnName("libroid");
            entity.Property(e => e.Orden).HasColumnName("orden");

            entity.HasOne(d => d.Autor).WithMany(p => p.AutorLibros)
                .HasForeignKey(d => d.Autorid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AutorLibro_autores");

            entity.HasOne(d => d.Libro).WithMany(p => p.AutorLibros)
                .HasForeignKey(d => d.Libroid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AutorLibro_libros");
        });

        modelBuilder.Entity<Autore>(entity =>
        {
            entity.HasKey(e => e.Autorid).HasName("PK__autores__A063869C4432D280");

            entity.ToTable("autores");

            entity.Property(e => e.Autorid).HasColumnName("autorid");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.Libroid).HasName("PK__libros__18C758938F31D97D");

            entity.ToTable("libros");

            entity.Property(e => e.Libroid).HasColumnName("libroid");
            entity.Property(e => e.Titulo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("titulo");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Postid).HasName("PK__post__DD017FD2A31B85E4");

            entity.ToTable("post");

            entity.Property(e => e.Postid).HasColumnName("postid");
            entity.Property(e => e.Autorid).HasColumnName("autorid");
            entity.Property(e => e.Contenido)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("contenido");
            entity.Property(e => e.Fechapublicacion)
                .HasColumnType("datetime")
                .HasColumnName("fechapublicacion");
            entity.Property(e => e.Titulo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("titulo");

            entity.HasOne(d => d.Autor).WithMany(p => p.Posts)
                .HasForeignKey(d => d.Autorid)
                .HasConstraintName("FK_post_autores");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
