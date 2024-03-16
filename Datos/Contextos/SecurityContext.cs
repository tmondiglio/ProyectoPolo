using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Seguridad;
using Entidades.Clientes;
using Microsoft.EntityFrameworkCore;

namespace Datos.Contextos;

public class SecurityContext : DbContext
{
  public DbSet<Usuario> Usuarios { get; set; }

  public DbSet<Perfil> Perfiles { get; set; }

   public DbSet<DireccionCliente> DireccionesClientes { get; set; } // Agregar DbSet para las direcciones de clientes


    public SecurityContext(DbContextOptions<SecurityContext> options) 
    : base(options)
  {
    
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Perfil>(builder =>
    {
      builder.Property(per => per.TipoUsuario).HasColumnName("Tipo_Usuario");
    });

    modelBuilder.Entity<Usuario>(builder =>
    {
      builder.HasKey(usr => usr.Clave);
      builder.Property(usr => usr.Clave).HasColumnName("Identificador");

      builder.Property(usr => usr.TipoUsuario).HasColumnName("Tipo_Usuario");
      builder.Property(usr => usr.FechaAlta).HasColumnName("Fecha_Alta");
      builder.Property(usr => usr.Nacimiento).HasColumnName("Fecha_Nacimiento");
      builder.Property(usr => usr.Correo).HasColumnName("Email");
      builder.Property(usr => usr.LastLogin).HasColumnName("Ultimo_Ingreso");

      //
      //  configuramos la relacion Perfil *----* Usuario
      builder
        .HasMany(usr => usr.Perfiles) // <-- parado en Perfil
        .WithMany()
        .UsingEntity<Dictionary<string, object>>("Relacion_Perfiles_Usuarios",
          right => right.HasOne<Perfil>().WithMany().HasForeignKey("ID_Perfil"),
          left => left.HasOne<Usuario>().WithMany().HasForeignKey("ID_Usuario"))
        .ToTable("Usuarios_Perfiles");
    });
        modelBuilder.Entity<DireccionCliente>(builder =>
        {
            // Configurar propiedades y relaciones de la entidad DireccionCliente
            builder.HasKey(dc => dc.Id);
            builder.Property(dc => dc.Calle).IsRequired();
            builder.Property(dc => dc.Ciudad).IsRequired();
            builder.Property(dc => dc.Pais).IsRequired();
            // Otros campos y relaciones...
            builder.HasOne(dc => dc.Usuario)
                .WithMany(u => u.Direcciones) // Propiedad de navegación en la clase Usuario
                .HasForeignKey(dc => dc.UsuarioId)
                .IsRequired();
        });
    }
}
