//Se importan los espacios de nombres necesarios. DbContext y otros tipos relacionados a Entity Framework Core están en el espacio
//de nombres Microsoft.EntityFrameworkCore.

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

//Se define un espacio de nombres llamado MiPrimerAPI.Models. Este es el espacio de nombres en el que se encuentra la clase AlmacenamientoContext.

namespace MiPrimerAPI.Models;


//Se define la clase AlmacenamientoContext que hereda de DbContext. La clase representa el contexto de la base de datos y se utiliza para interactuar
//con la base de datos mediante Entity Framework Core.
public partial class AlmacenamientoContext : DbContext
{
    //Se definen dos constructores. Uno es el constructor por defecto, y el otro toma DbContextOptions como parámetro. El segundo constructor es
    //útil cuando se desea configurar el contexto con opciones específicas, como la cadena de conexión.
    public AlmacenamientoContext()
    {
    }

    public AlmacenamientoContext(DbContextOptions<AlmacenamientoContext> options)
        : base(options)
    {
    }


    //Se definen dos propiedades del tipo DbSet<> que representan las tablas en la base de datos. RespuestaApis representa la tabla "RespuestaAPI" y Results representa la
    //tabla "results".
    public virtual DbSet<RespuestaApi> RespuestaApis { get; set; }

    public virtual DbSet<Result> Results { get; set; }


    //Se anula el método OnConfiguring, que se utiliza para configurar el proveedor de la base de datos y la cadena de conexión. Aquí,
    //se está utilizando SQL Server como proveedor y se proporciona la cadena de conexión directamente en el código (se recomienda evitar esto en entornos de producción).


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-GLVK46U;Database=Almacenamiento;Integrated Security=True;TrustServerCertificate=True;");


    //Se anula el método OnModelCreating, que se utiliza para configurar el modelo de datos, como definir relaciones, restricciones, etc.

    //Dentro de OnModelCreating, se utiliza el objeto modelBuilder para configurar las entidades y sus propiedades. Aquí se configuran las entidades
    //RespuestaApi y Result con sus respectivas propiedades y configuraciones de columna en la base de datos.

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RespuestaApi>(entity =>
        {
            entity.ToTable("RespuestaAPI");

            entity.Property(e => e.NextPage)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("next_page");
            entity.Property(e => e.PageCount).HasColumnName("page_count");
            entity.Property(e => e.PageNbr).HasColumnName("page_nbr");
            entity.Property(e => e.ResultCount).HasColumnName("result_count");
        });

        modelBuilder.Entity<Result>(entity =>
        {
            entity.ToTable("results");

            entity.Property(e => e.CreateTs)
                .HasColumnType("datetime")
                .HasColumnName("create_ts");
            entity.Property(e => e.Description)
                .HasMaxLength(350)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.ItemAlternateCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("item_alternate_code");
        });

        OnModelCreatingPartial(modelBuilder);
    }


    //Se proporciona un método parcial (OnModelCreatingPartial) que puede ser implementado en otro archivo parcial de la clase para extender la
    //lógica de configuración del modelo.

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

//En resumen, este código define un contexto de base de datos (AlmacenamientoContext) utilizando Entity Framework Core para interactuar con una base de datos.
//Configura las tablas y sus propiedades, así como la cadena de conexión y otras configuraciones relacionadas con la base de datos.