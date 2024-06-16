using Microsoft.EntityFrameworkCore;
using SO2M.Models;






namespace SO2M.Data
{
    public class So2msContext : DbContext
    {
        public So2msContext()
        {
        }


        public So2msContext(DbContextOptions<So2msContext> options) : base(options) { }


        public DbSet<Utilisateur> Utilisateurs { get; set; }

        public DbSet<CritereRecherche> CritereRecherches { get; set; }
 
        public DbSet<Photo> Photos { get; set; }
        
        public DbSet<Message> Messages { get; set; } // Ajout de la DbSet pour les messages


 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost; Database=SO2Mv4; Trusted_Connection=true; TrustServerCertificate=true;");


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
    }
}
