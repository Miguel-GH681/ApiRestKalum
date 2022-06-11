using Microsoft.EntityFrameworkCore;
using WebApiKalum.Entities;

namespace WebApiKalum
{
    public class KalumDbContext : DbContext
    {        
        public KalumDbContext(DbContextOptions options) : base(options)
        {            
        }

        public DbSet<CarreraTecnica> CarreraTecnica {get;set;}
        public DbSet<Aspirante> Aspirante {get;set;}
        public DbSet<Jornada> Jornada {get;set;}
        public DbSet<ExamenAdmision> ExamenAdmision {get; set;}
        public DbSet<Inscripcion> Inscripcion {get;set;}
        public DbSet<Alumno> Alumno {get;set;}
        public DbSet<Cargo> Cargo {get;set;}
        public DbSet<CuentaPorCobrar> CuentaXCobrar {get;set;}
        public DbSet<InversionCarreraTecnica> InversionCarreraTecnica {get;set;}
        public DbSet<InscripcionPago> InscripcionPago {get;set;}
        public DbSet<ResultadoExamenAdmision> ResultadoExamenAdmision {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarreraTecnica>().ToTable("CarreraTecnica").HasKey(ct => new {ct.CarreraId});
            modelBuilder.Entity<Aspirante>().ToTable("Aspirante").HasKey(a => new {a.NoExpediente});
            modelBuilder.Entity<Jornada>().ToTable("Jornada").HasKey(j => new {j.JornadaId});
            modelBuilder.Entity<ExamenAdmision>().ToTable("ExamenAdmision").HasKey(ea => new {ea.ExamenId});
            modelBuilder.Entity<Inscripcion>().ToTable("Inscripcion").HasKey(i => new {i.InscripcionId});
            modelBuilder.Entity<Alumno>().ToTable("Alumno").HasKey(a => new {a.Carne});
            modelBuilder.Entity<Cargo>().ToTable("Cargo").HasKey(c => new {c.CargoId});
            modelBuilder.Entity<CuentaPorCobrar>().ToTable("CuentaPorCobrar").HasKey(cc => new {cc.Anio, cc.Carne, cc.Correlativo});
            modelBuilder.Entity<InversionCarreraTecnica>().ToTable("InversionCarreraTecnica").HasKey(ict => new {ict.InversionId});
            modelBuilder.Entity<InscripcionPago>().ToTable("InscripcionPago").HasKey(ip => new {ip.BoletaPago, ip.NoExpediente, ip.Anio});
            modelBuilder.Entity<ResultadoExamenAdmision>().ToTable("ResultadoExamenAdmision").HasKey(rea => new {rea.NoExpediente, rea.Anio});
            modelBuilder.Entity<Aspirante>()
                .HasOne<CarreraTecnica>( aspirante => aspirante.CarreraTecnica)
                .WithMany(ct => ct.Aspirantes)
                .HasForeignKey(aspirante => aspirante.CarreraId);
            modelBuilder.Entity<Aspirante>()
                .HasOne<Jornada>(aspirante => aspirante.Jornada)
                .WithMany(jornada => jornada.Aspirantes)
                .HasForeignKey(aspirantes => aspirantes.JornadaId);
            modelBuilder.Entity<Aspirante>()
                .HasOne<ExamenAdmision>(aspirante=>aspirante.ExamenAdmision)
                .WithMany(ea => ea.Aspirantes)
                .HasForeignKey(aspirante => aspirante.ExamenId);
            modelBuilder.Entity<Inscripcion>()
                .HasOne<CarreraTecnica>(inscripcion => inscripcion.CarreraTecnica)
                .WithMany(ct => ct.Inscripciones)
                .HasForeignKey(inscripcion => inscripcion.CarreraId);
            modelBuilder.Entity<Inscripcion>()
                .HasOne<Jornada>(inscripcion => inscripcion.Jornada)
                .WithMany(jornada => jornada.Inscripciones)
                .HasForeignKey(inscripcion => inscripcion.JornadaId);
            modelBuilder.Entity<Inscripcion>()
                .HasOne<Alumno>(inscripcion => inscripcion.Alumno)
                .WithOne(a => a.Inscripcion)
                .HasForeignKey<Inscripcion>(inscripcion => inscripcion.Carne);
            modelBuilder.Entity<CuentaPorCobrar>()
                .HasOne<Cargo>(cxc => cxc.Cargo)
                .WithMany(c => c.CuentasPorCobrar)
                .HasForeignKey(cxc => cxc.CargoId);
            modelBuilder.Entity<InversionCarreraTecnica>()
                .HasOne<CarreraTecnica>(ict => ict.CarreraTecnica)
                .WithOne(ct => ct.Inversion)
                .HasForeignKey<InversionCarreraTecnica>(ict => ict.CarreraId);
            modelBuilder.Entity<InscripcionPago>()
                .HasOne<Aspirante>(ip => ip.Aspirante)
                .WithMany(aspirante => aspirante.InscripcionPagos)
                .HasForeignKey(ip => ip.NoExpediente);       
            modelBuilder.Entity<ResultadoExamenAdmision>()
                .HasOne<Aspirante>(rea => rea.Aspirante)
                .WithMany(aspirante => aspirante.Resultados)
                .HasForeignKey(rea => rea.NoExpediente);
            modelBuilder.Entity<CuentaPorCobrar>()
                .HasOne<Alumno>(cpc => cpc.Alumno)
                .WithMany(alumno => alumno.CuentasPorCobrar)
                .HasForeignKey(cpc => cpc.Carne);
        }
    }
}