using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SO2M.Models;

public partial class So2mContext : DbContext
{
    public So2mContext()
    {
    }

    public So2mContext(DbContextOptions<So2mContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Axe> Axes { get; set; }

    public virtual DbSet<CritereRecherche> CritereRecherches { get; set; }

    public virtual DbSet<ModelPsycologique> ModelPsycologiques { get; set; }

    public virtual DbSet<Photo> Photos { get; set; }

    public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost; Database=SO2M; Trusted_Connection=true; TrustServerCertificate=true;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Axe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Axes__3214EC2789E8112F");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ModelPsycologiqueId).HasColumnName("ModelPsycologiqueID");
            entity.Property(e => e.TitreAxe).HasMaxLength(255);

            entity.HasOne(d => d.ModelPsycologique).WithMany(p => p.Axes)
                .HasForeignKey(d => d.ModelPsycologiqueId)
                .HasConstraintName("FK__Axes__ModelPsyco__4316F928");
        });

        modelBuilder.Entity<CritereRecherche>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CritereR__3214EC27E9363A20");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Genre).HasMaxLength(50);
            entity.Property(e => e.NiveauAcademique).HasMaxLength(255);
            entity.Property(e => e.OrientationS).HasMaxLength(50);
            entity.Property(e => e.Ville).HasMaxLength(255);
        });

        modelBuilder.Entity<ModelPsycologique>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ModelPsy__3214EC273BB82D1C");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AuteurModel).HasMaxLength(255);
            entity.Property(e => e.TitreModel).HasMaxLength(255);
        });

        modelBuilder.Entity<Photo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Photos__3214EC275C8334B4");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Url)
                .HasMaxLength(255)
                .HasColumnName("URL");
            entity.Property(e => e.UtilisateurId).HasColumnName("UtilisateurID");

            entity.HasOne(d => d.Utilisateur).WithMany(p => p.Photos)
                .HasForeignKey(d => d.UtilisateurId)
                .HasConstraintName("FK__Photos__Utilisat__403A8C7D");
        });

        modelBuilder.Entity<Utilisateur>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Utilisat__3214EC2700ED213F");

            entity.HasIndex(e => e.Username, "UQ__Utilisat__536C85E4A6FFDB1E").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Courriel).HasMaxLength(255);
            entity.Property(e => e.CritereRechercheId).HasColumnName("CritereRechercheID");
            entity.Property(e => e.Est_Active).HasColumnName("Est_Active");
            entity.Property(e => e.Genre).HasMaxLength(50);
            entity.Property(e => e.ModelPsycologiqueId).HasColumnName("ModelPsycologiqueID");
            entity.Property(e => e.MotDePasse).HasMaxLength(255);
            entity.Property(e => e.NiveauAcademique).HasMaxLength(255);
            entity.Property(e => e.Nom).HasMaxLength(255);
            entity.Property(e => e.OrientationS).HasMaxLength(50);
            entity.Property(e => e.Prenom).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(255);

            entity.HasOne(d => d.CritereRecherche).WithMany(p => p.Utilisateurs)
                .HasForeignKey(d => d.CritereRechercheId)
                .HasConstraintName("FK__Utilisate__Crite__3C69FB99");

            entity.HasOne(d => d.ModelPsycologique).WithMany(p => p.Utilisateurs)
                .HasForeignKey(d => d.ModelPsycologiqueId)
                .HasConstraintName("FK__Utilisate__Model__3D5E1FD2");

            entity.HasMany(d => d.Favoris).WithMany(p => p.Utilisateurs)
                .UsingEntity<Dictionary<string, object>>(
                    "UtilisateurFavoriss",
                    r => r.HasOne<Utilisateur>().WithMany()
                        .HasForeignKey("FavoriId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Utilisate__Favor__4AB81AF0"),
                    l => l.HasOne<Utilisateur>().WithMany()
                        .HasForeignKey("UtilisateurId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Utilisate__Utili__49C3F6B7"),
                    j =>
                    {
                        j.HasKey("UtilisateurId", "FavoriId").HasName("PK__Utilisat__39F97FE357DA6AB7");
                        j.ToTable("UtilisateurFavoriss");
                        j.IndexerProperty<int>("UtilisateurId").HasColumnName("UtilisateurID");
                        j.IndexerProperty<int>("FavoriId").HasColumnName("FavoriID");
                    });

            entity.HasMany(d => d.Matches).WithMany(p => p.UtilisateursNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "UtilisateurMatchss",
                    r => r.HasOne<Utilisateur>().WithMany()
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Utilisate__Match__4E88ABD4"),
                    l => l.HasOne<Utilisateur>().WithMany()
                        .HasForeignKey("UtilisateurId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Utilisate__Utili__4D94879B"),
                    j =>
                    {
                        j.HasKey("UtilisateurId", "MatchId").HasName("PK__Utilisat__0897229C59FCAEBD");
                        j.ToTable("UtilisateurMatchss");
                        j.IndexerProperty<int>("UtilisateurId").HasColumnName("UtilisateurID");
                        j.IndexerProperty<int>("MatchId").HasColumnName("MatchID");
                    });

            entity.HasMany(d => d.PhotosNavigation).WithMany(p => p.Utilisateurs)
                .UsingEntity<Dictionary<string, object>>(
                    "UtilisateurPhoto",
                    r => r.HasOne<Photo>().WithMany()
                        .HasForeignKey("PhotoId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Utilisate__Photo__46E78A0C"),
                    l => l.HasOne<Utilisateur>().WithMany()
                        .HasForeignKey("UtilisateurId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Utilisate__Utili__45F365D3"),
                    j =>
                    {
                        j.HasKey("UtilisateurId", "PhotoId").HasName("PK__Utilisat__5EADD547253098FC");
                        j.ToTable("UtilisateurPhotos");
                        j.IndexerProperty<int>("UtilisateurId").HasColumnName("UtilisateurID");
                        j.IndexerProperty<int>("PhotoId").HasColumnName("PhotoID");
                    });

            entity.HasMany(d => d.Utilisateurs).WithMany(p => p.Favoris)
                .UsingEntity<Dictionary<string, object>>(
                    "UtilisateurFavoriss",
                    r => r.HasOne<Utilisateur>().WithMany()
                        .HasForeignKey("UtilisateurId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Utilisate__Utili__49C3F6B7"),
                    l => l.HasOne<Utilisateur>().WithMany()
                        .HasForeignKey("FavoriId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Utilisate__Favor__4AB81AF0"),
                    j =>
                    {
                        j.HasKey("UtilisateurId", "FavoriId").HasName("PK__Utilisat__39F97FE357DA6AB7");
                        j.ToTable("UtilisateurFavoriss");
                        j.IndexerProperty<int>("UtilisateurId").HasColumnName("UtilisateurID");
                        j.IndexerProperty<int>("FavoriId").HasColumnName("FavoriID");
                    });

            entity.HasMany(d => d.UtilisateursNavigation).WithMany(p => p.Matches)
                .UsingEntity<Dictionary<string, object>>(
                    "UtilisateurMatchss",
                    r => r.HasOne<Utilisateur>().WithMany()
                        .HasForeignKey("UtilisateurId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Utilisate__Utili__4D94879B"),
                    l => l.HasOne<Utilisateur>().WithMany()
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Utilisate__Match__4E88ABD4"),
                    j =>
                    {
                        j.HasKey("UtilisateurId", "MatchId").HasName("PK__Utilisat__0897229C59FCAEBD");
                        j.ToTable("UtilisateurMatchss");
                        j.IndexerProperty<int>("UtilisateurId").HasColumnName("UtilisateurID");
                        j.IndexerProperty<int>("MatchId").HasColumnName("MatchID");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
