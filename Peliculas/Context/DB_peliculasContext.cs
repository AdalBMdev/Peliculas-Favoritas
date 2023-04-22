using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Peliculas.Models;

namespace Peliculas.Context
{
    public partial class DB_peliculasContext : DbContext
    {
        public DB_peliculasContext()
        {
        }

        public DB_peliculasContext(DbContextOptions<DB_peliculasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Pelicula> Peliculas { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=ADALBERTO; Database=DB_peliculas; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pelicula>(entity =>
            {
                entity.HasKey(e => e.Idpeliculas)
                    .HasName("PK__Pelicula__A802669BCF5F8DC8");

                entity.Property(e => e.Idpeliculas).HasColumnName("IDPeliculas");

                entity.Property(e => e.Año).HasColumnType("date");

                entity.Property(e => e.Director)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.id_generos)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Idusuario).HasColumnName("IDUsuario");

                entity.Property(e => e.Poster)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Titulo)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Idusuario)
                    .HasName("PK__Usuario__5231116986630238");

                entity.ToTable("Usuario");

                entity.Property(e => e.Idusuario).HasColumnName("IDUsuario");

                entity.Property(e => e.Clave)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nickname)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Idusuario)
                    .HasName("PK__Usuario__5231116986630238");

                entity.ToTable("Usuario");

                entity.Property(e => e.Idusuario).HasColumnName("IDUsuario");

                entity.Property(e => e.Clave)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nickname)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
