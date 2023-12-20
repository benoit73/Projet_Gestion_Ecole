using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace projet_ga_v2.BDD;

public partial class Benoit73TestContext : DbContext
{
    public Benoit73TestContext()
    {
    }

    public Benoit73TestContext(DbContextOptions<Benoit73TestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Absence> Absences { get; set; }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Compte> Comptes { get; set; }

    public virtual DbSet<Cour> Cours { get; set; }

    public virtual DbSet<DoctrineMigrationVersion> DoctrineMigrationVersions { get; set; }

    public virtual DbSet<Edt> Edts { get; set; }

    public virtual DbSet<Elefe> Eleves { get; set; }

    public virtual DbSet<Enseignant> Enseignants { get; set; }

    public virtual DbSet<EnseignantClasse> EnseignantClasses { get; set; }

    public virtual DbSet<Matiere> Matieres { get; set; }

    public virtual DbSet<MessengerMessage> MessengerMessages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=mysql-benoit73.alwaysdata.net;database=benoit73_test;uid=benoit73;pwd=gogolito", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.6.14-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Absence>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("absences");

            entity.HasIndex(e => e.IdEleve, "IDX_F9C0EFFF22444C7");

            entity.HasIndex(e => e.IdMatiere, "IDX_F9C0EFFF4E89FE3A");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Heure)
                .HasColumnType("int(11)")
                .HasColumnName("heure");
            entity.Property(e => e.IdEleve)
                .HasColumnType("int(11)")
                .HasColumnName("id_eleve");
            entity.Property(e => e.IdMatiere)
                .HasColumnType("int(11)")
                .HasColumnName("id_matiere");
            entity.Property(e => e.Jour)
                .HasColumnType("int(11)")
                .HasColumnName("jour");

            entity.HasOne(d => d.IdEleveNavigation).WithMany(p => p.Absences)
                .HasForeignKey(d => d.IdEleve)
                .HasConstraintName("FK_F9C0EFFF22444C7");

            entity.HasOne(d => d.IdMatiereNavigation).WithMany(p => p.Absences)
                .HasForeignKey(d => d.IdMatiere)
                .HasConstraintName("FK_F9C0EFFF4E89FE3A");
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
            entity.Property(e => e.User)
                .HasMaxLength(255)
                .HasColumnName("user");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("classes")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.NomClasse)
                .HasMaxLength(255)
                .HasColumnName("nom_classe");
        });

        modelBuilder.Entity<Compte>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("comptes")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.EleveId, "UNIQ_56735801A6CC7B2").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.AccountType)
                .HasMaxLength(1)
                .HasColumnName("account_type");
            entity.Property(e => e.EleveId)
                .HasColumnType("int(11)")
                .HasColumnName("eleve_id");
            entity.Property(e => e.IdEnseignant)
                .HasMaxLength(255)
                .HasColumnName("id_enseignant");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.UserName)
                .HasMaxLength(255)
                .HasColumnName("user_name");

            entity.HasOne(d => d.Eleve).WithOne(p => p.Compte)
                .HasForeignKey<Compte>(d => d.EleveId)
                .HasConstraintName("FK_56735801A6CC7B2");
        });

        modelBuilder.Entity<Cour>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("cours")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.EnseignantId, "IDX_FDCA8C9CE455FCC0");

            entity.HasIndex(e => e.MatiereId, "IDX_FDCA8C9CF46CD258");

            entity.HasIndex(e => e.EdtId, "IDX_FDCA8C9CF814C52E");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Debut)
                .HasColumnType("int(11)")
                .HasColumnName("debut");
            entity.Property(e => e.Duree)
                .HasColumnType("int(11)")
                .HasColumnName("duree");
            entity.Property(e => e.EdtId)
                .HasColumnType("int(11)")
                .HasColumnName("edt_id");
            entity.Property(e => e.EnseignantId)
                .HasColumnType("int(11)")
                .HasColumnName("enseignant_id");
            entity.Property(e => e.Jour)
                .HasColumnType("int(11)")
                .HasColumnName("jour");
            entity.Property(e => e.MatiereId)
                .HasColumnType("int(11)")
                .HasColumnName("matiere_id");

            entity.HasOne(d => d.Edt).WithMany(p => p.Cours)
                .HasForeignKey(d => d.EdtId)
                .HasConstraintName("FK_FDCA8C9CF814C52E");

            entity.HasOne(d => d.Enseignant).WithMany(p => p.Cours)
                .HasForeignKey(d => d.EnseignantId)
                .HasConstraintName("FK_FDCA8C9CE455FCC0");

            entity.HasOne(d => d.Matiere).WithMany(p => p.Cours)
                .HasForeignKey(d => d.MatiereId)
                .HasConstraintName("FK_FDCA8C9CF46CD258");
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

        modelBuilder.Entity<Edt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("edt")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.ClasseId, "IDX_E7A4CB5F8F5EA509");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.ClasseId)
                .HasColumnType("int(11)")
                .HasColumnName("classe_id");
            entity.Property(e => e.EnseignantClasse)
                .HasMaxLength(255)
                .HasColumnName("enseignant_classe");

            entity.HasOne(d => d.Classe).WithMany(p => p.Edts)
                .HasForeignKey(d => d.ClasseId)
                .HasConstraintName("FK_E7A4CB5F8F5EA509");
        });

        modelBuilder.Entity<Elefe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("eleves")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.ClasseId, "IDX_383B09B18F5EA509");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.ClasseId)
                .HasColumnType("int(11)")
                .HasColumnName("classe_id");
            entity.Property(e => e.MailEleve)
                .HasMaxLength(255)
                .HasColumnName("mail_eleve");
            entity.Property(e => e.NomEleve)
                .HasMaxLength(255)
                .HasColumnName("nom_eleve");
            entity.Property(e => e.PrenomEleve)
                .HasMaxLength(255)
                .HasColumnName("prenom_eleve");

            entity.HasOne(d => d.Classe).WithMany(p => p.Eleves)
                .HasForeignKey(d => d.ClasseId)
                .HasConstraintName("FK_383B09B18F5EA509");
        });

        modelBuilder.Entity<Enseignant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("enseignants")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.MatiereId, "IDX_BA5EFB5AF46CD258");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.MatiereId)
                .HasColumnType("int(11)")
                .HasColumnName("matiere_id");
            entity.Property(e => e.NomEnseignant)
                .HasMaxLength(255)
                .HasColumnName("nom_enseignant");
            entity.Property(e => e.PrenomEnseignant)
                .HasMaxLength(255)
                .HasColumnName("prenom_enseignant");

            entity.HasOne(d => d.Matiere).WithMany(p => p.Enseignants)
                .HasForeignKey(d => d.MatiereId)
                .HasConstraintName("FK_BA5EFB5AF46CD258");
        });

        modelBuilder.Entity<EnseignantClasse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("enseignant_classe")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.ClasseId, "UNIQ_F670A5F48F5EA509").IsUnique();

            entity.HasIndex(e => e.EnseignantId, "UNIQ_F670A5F4E455FCC0").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.ClasseId)
                .HasColumnType("int(11)")
                .HasColumnName("classe_id");
            entity.Property(e => e.EnseignantId)
                .HasColumnType("int(11)")
                .HasColumnName("enseignant_id");

            entity.HasOne(d => d.Classe).WithOne(p => p.EnseignantClasse)
                .HasForeignKey<EnseignantClasse>(d => d.ClasseId)
                .HasConstraintName("FK_F670A5F48F5EA509");

            entity.HasOne(d => d.Enseignant).WithOne(p => p.EnseignantClasse)
                .HasForeignKey<EnseignantClasse>(d => d.EnseignantId)
                .HasConstraintName("FK_F670A5F4E455FCC0");
        });

        modelBuilder.Entity<Matiere>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("matieres")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.NomMatiere)
                .HasMaxLength(255)
                .HasColumnName("nom_matiere");
        });

        modelBuilder.Entity<MessengerMessage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("messenger_messages")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.DeliveredAt, "IDX_75EA56E016BA31DB");

            entity.HasIndex(e => e.AvailableAt, "IDX_75EA56E0E3BD61CE");

            entity.HasIndex(e => e.QueueName, "IDX_75EA56E0FB7336F0");

            entity.Property(e => e.Id)
                .HasColumnType("bigint(20)")
                .HasColumnName("id");
            entity.Property(e => e.AvailableAt)
                .HasComment("(DC2Type:datetime_immutable)")
                .HasColumnType("datetime")
                .HasColumnName("available_at");
            entity.Property(e => e.Body).HasColumnName("body");
            entity.Property(e => e.CreatedAt)
                .HasComment("(DC2Type:datetime_immutable)")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DeliveredAt)
                .HasComment("(DC2Type:datetime_immutable)")
                .HasColumnType("datetime")
                .HasColumnName("delivered_at");
            entity.Property(e => e.Headers).HasColumnName("headers");
            entity.Property(e => e.QueueName)
                .HasMaxLength(190)
                .HasColumnName("queue_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
