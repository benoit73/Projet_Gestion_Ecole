using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace projet_ga_v2.Models;

public partial class Benoit73SymfonyV5Context : DbContext
{
    public Benoit73SymfonyV5Context()
    {
    }

    public Benoit73SymfonyV5Context(DbContextOptions<Benoit73SymfonyV5Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Absence> Absences { get; set; }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Calendrier> Calendriers { get; set; }

    public virtual DbSet<Classe> Classes { get; set; }

    public virtual DbSet<Cour> Cours { get; set; }

    public virtual DbSet<DoctrineMigrationVersion> DoctrineMigrationVersions { get; set; }

    public virtual DbSet<Eleve> Eleves { get; set; }

    public virtual DbSet<Enseignant> Enseignants { get; set; }

    public virtual DbSet<EnseignantMatiereClasse> EnseignantMatiereClasses { get; set; }

    public virtual DbSet<Matiere> Matieres { get; set; }

    public virtual DbSet<Parent> Parents { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=mysql-benoit73.alwaysdata.net;port=3306;database=benoit73_symfony_v5;uid=benoit73_2;pwd=gogolito", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.6.16-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Absence>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("absence")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.EleveId, "IDX_765AE0C9A6CC7B2");

            entity.HasIndex(e => e.CourId, "IDX_765AE0C9B7942F03");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CourId)
                .HasColumnType("int(11)")
                .HasColumnName("cour_id");
            entity.Property(e => e.EleveId)
                .HasColumnType("int(11)")
                .HasColumnName("eleve_id");
            entity.Property(e => e.Justifiee).HasColumnName("justifiee");

            entity.HasOne(d => d.Cour).WithMany(p => p.Absences)
                .HasForeignKey(d => d.CourId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_765AE0C9B7942F03");

            entity.HasOne(d => d.Eleve).WithMany(p => p.Absences)
                .HasForeignKey(d => d.EleveId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_765AE0C9A6CC7B2");
        });

        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("admin")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Calendrier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("calendrier")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Annee)
                .HasColumnType("int(11)")
                .HasColumnName("annee");
            entity.Property(e => e.Date)
                .HasColumnType("int(11)")
                .HasColumnName("date");
            entity.Property(e => e.Jour)
                .HasColumnType("int(11)")
                .HasColumnName("jour");
            entity.Property(e => e.Mois)
                .HasColumnType("int(11)")
                .HasColumnName("mois");
            entity.Property(e => e.Semaine)
                .HasColumnType("int(11)")
                .HasColumnName("semaine");
        });

        modelBuilder.Entity<Classe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("classe")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.NbEleves)
                .HasColumnType("int(11)")
                .HasColumnName("nb_eleves");
            entity.Property(e => e.Niveau)
                .HasMaxLength(255)
                .HasColumnName("niveau");
            entity.Property(e => e.NomClasse)
                .HasMaxLength(255)
                .HasColumnName("nom_classe");
        });

        modelBuilder.Entity<Cour>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("cour")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.EnseignantMatiereClasseId, "IDX_A71F964F33ECEC1F");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Annee)
                .HasColumnType("int(11)")
                .HasColumnName("annee");
            entity.Property(e => e.Debut)
                .HasColumnType("int(11)")
                .HasColumnName("debut");
            entity.Property(e => e.Duree)
                .HasColumnType("int(11)")
                .HasColumnName("duree");
            entity.Property(e => e.EnseignantMatiereClasseId)
                .HasColumnType("int(11)")
                .HasColumnName("enseignant_matiere_classe_id");
            entity.Property(e => e.Jour)
                .HasColumnType("int(11)")
                .HasColumnName("jour");
            entity.Property(e => e.Semaine)
                .HasColumnType("int(11)")
                .HasColumnName("semaine");

            entity.HasOne(d => d.EnseignantMatiereClasse).WithMany(p => p.Cours)
                .HasForeignKey(d => d.EnseignantMatiereClasseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_A71F964F33ECEC1F");
        });

        modelBuilder.Entity<DoctrineMigrationVersion>(entity =>
        {
            entity.HasKey(e => e.Version).HasName("PRIMARY");

            entity
                .ToTable("doctrine_migration_versions")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_unicode_ci");

            entity.Property(e => e.Version)
                .HasMaxLength(191)
                .HasColumnName("version");
            entity.Property(e => e.ExecutedAt)
                .HasColumnType("datetime")
                .HasColumnName("executed_at");
            entity.Property(e => e.ExecutionTime)
                .HasColumnType("int(11)")
                .HasColumnName("execution_time");
        });

        modelBuilder.Entity<Eleve>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("eleve")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.ClasseId, "IDX_ECA105F78F5EA509");

            entity.HasIndex(e => e.ParentsId, "IDX_ECA105F7B706B6D3");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.ClasseId)
                .HasColumnType("int(11)")
                .HasColumnName("classe_id");
            entity.Property(e => e.DateNaissance)
                .HasMaxLength(255)
                .HasColumnName("date_naissance");
            entity.Property(e => e.MailEleve)
                .HasMaxLength(255)
                .HasColumnName("mail_eleve");
            entity.Property(e => e.NomEleve)
                .HasMaxLength(255)
                .HasColumnName("nom_eleve");
            entity.Property(e => e.ParentsId)
                .HasColumnType("int(11)")
                .HasColumnName("parents_id");
            entity.Property(e => e.PrenomEleve)
                .HasMaxLength(255)
                .HasColumnName("prenom_eleve");

            entity.HasOne(d => d.Classe).WithMany(p => p.Eleves)
                .HasForeignKey(d => d.ClasseId)
                .HasConstraintName("FK_ECA105F78F5EA509");

            entity.HasOne(d => d.Parents).WithMany(p => p.Eleves)
                .HasForeignKey(d => d.ParentsId)
                .HasConstraintName("FK_ECA105F7B706B6D3");
        });

        modelBuilder.Entity<Enseignant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("enseignant")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.NomEnseignant)
                .HasMaxLength(255)
                .HasColumnName("nom_enseignant");
            entity.Property(e => e.PrenomEnseignant)
                .HasMaxLength(255)
                .HasColumnName("prenom_enseignant");

            entity.HasMany(d => d.Matieres).WithMany(p => p.Enseignants)
                .UsingEntity<Dictionary<string, object>>(
                    "EnseignantMatiere",
                    r => r.HasOne<Matiere>().WithMany()
                        .HasForeignKey("MatiereId")
                        .HasConstraintName("FK_33D1A024F46CD258"),
                    l => l.HasOne<Enseignant>().WithMany()
                        .HasForeignKey("EnseignantId")
                        .HasConstraintName("FK_33D1A024E455FCC0"),
                    j =>
                    {
                        j.HasKey("EnseignantId", "MatiereId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j
                            .ToTable("enseignant_matiere")
                            .UseCollation("utf8mb4_unicode_ci");
                        j.HasIndex(new[] { "EnseignantId" }, "IDX_33D1A024E455FCC0");
                        j.HasIndex(new[] { "MatiereId" }, "IDX_33D1A024F46CD258");
                        j.IndexerProperty<int>("EnseignantId")
                            .HasColumnType("int(11)")
                            .HasColumnName("enseignant_id");
                        j.IndexerProperty<int>("MatiereId")
                            .HasColumnType("int(11)")
                            .HasColumnName("matiere_id");
                    });
        });

        modelBuilder.Entity<EnseignantMatiereClasse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("enseignant_matiere_classe")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.ClasseId, "IDX_25155D28F5EA509");

            entity.HasIndex(e => e.EnseignantId, "IDX_25155D2E455FCC0");

            entity.HasIndex(e => e.MatiereId, "IDX_25155D2F46CD258");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.ClasseId)
                .HasColumnType("int(11)")
                .HasColumnName("classe_id");
            entity.Property(e => e.EnseignantId)
                .HasColumnType("int(11)")
                .HasColumnName("enseignant_id");
            entity.Property(e => e.MatiereId)
                .HasColumnType("int(11)")
                .HasColumnName("matiere_id");

            entity.HasOne(d => d.Classe).WithMany(p => p.EnseignantMatiereClasses)
                .HasForeignKey(d => d.ClasseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_25155D28F5EA509");

            entity.HasOne(d => d.Enseignant).WithMany(p => p.EnseignantMatiereClasses)
                .HasForeignKey(d => d.EnseignantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_25155D2E455FCC0");

            entity.HasOne(d => d.Matiere).WithMany(p => p.EnseignantMatiereClasses)
                .HasForeignKey(d => d.MatiereId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_25155D2F46CD258");
        });

        modelBuilder.Entity<Matiere>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("matiere")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.NbEnseignants)
                .HasColumnType("int(11)")
                .HasColumnName("nb_enseignants");
            entity.Property(e => e.NomMatiere)
                .HasMaxLength(255)
                .HasColumnName("nom_matiere");
        });

        modelBuilder.Entity<Parent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("parents")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("user")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.EleveId, "UNIQ_8D93D649A6CC7B2").IsUnique();

            entity.HasIndex(e => e.ParentsId, "UNIQ_8D93D649B706B6D3").IsUnique();

            entity.HasIndex(e => e.Email, "UNIQ_8D93D649E7927C74").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.EleveId)
                .HasColumnType("int(11)")
                .HasColumnName("eleve_id");
            entity.Property(e => e.Email)
                .HasMaxLength(180)
                .HasColumnName("email");
            entity.Property(e => e.IsVerified).HasColumnName("is_verified");
            entity.Property(e => e.ParentsId)
                .HasColumnType("int(11)")
                .HasColumnName("parents_id");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Roles)
                .HasComment("(DC2Type:json)")
                .HasColumnType("json")
                .HasColumnName("roles");

            entity.HasOne(d => d.Eleve).WithOne(p => p.User)
                .HasForeignKey<User>(d => d.EleveId)
                .HasConstraintName("FK_8D93D649A6CC7B2");

            entity.HasOne(d => d.Parents).WithOne(p => p.User)
                .HasForeignKey<User>(d => d.ParentsId)
                .HasConstraintName("FK_8D93D649B706B6D3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
