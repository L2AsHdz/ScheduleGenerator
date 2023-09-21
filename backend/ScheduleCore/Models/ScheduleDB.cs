using Microsoft.EntityFrameworkCore;

namespace ScheduleCore.Models;

public class ScheduleDB: DbContext
{
    public ScheduleDB(DbContextOptions options) : base(options) { }
    
    public DbSet<Carrera> Carrera { get; set; }
    public DbSet<Catedratico> Catedratico { get; set; }
    public DbSet<Curso> Curso { get; set; }
    public DbSet<Horario> Horario { get; set; }
    public DbSet<CursoAdvertencia> CursoAdvertencia { get; set; }
    public DbSet<CursoCarrera> CursoCarrera { get; set; }
    public DbSet<CursoCatedratico> CursoCatedratico { get; set; }
    public DbSet<CursoHorario> CursoHorario { get; set; }
    public DbSet<CursoSalon> CursoSalon { get; set; }
    public DbSet<Parametro> Parametro { get; set; }
    public DbSet<Salon> Salon { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<CursoCarrera>()
            .HasKey(e => new { e.CodigoCarrera, e.CodigoCurso });
        
        modelBuilder.Entity<CursoSalon>()
            .HasKey(e => new { e.NoSalon, e.CodigoCurso });
        
        modelBuilder.Entity<CursoCatedratico>()
            .HasKey(e => new { e.CodigoCatedratico, e.CodigoCurso });
    }
}