using Microsoft.EntityFrameworkCore;
using SO2M.Models;

namespace SO2M.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Axe> Axes { get; set; }
        public DbSet<CritereRecherche> CritereRecherches { get; set; }
        public DbSet<ModelPsycologique> ModelPsycologiques { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Message> Messages { get; set; } // Ajout de la DbSet pour les messages


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Photo>()
                .HasOne(p => p.Utilisateur)
                .WithMany(u => u.Photos)
                .HasForeignKey(p => p.UtilisateurId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Expéditeur)
                .WithMany(u => u.MessagesEnvoyés)
                .HasForeignKey(m => m.ExpéditeurId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Destinataire)
                .WithMany(u => u.MessagesReçus)
                .HasForeignKey(m => m.DestinataireId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
