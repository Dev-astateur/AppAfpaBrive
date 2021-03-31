using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using AppAfpaBrive.BOL;

#nullable disable

namespace AppAfpaBrive.DAL
{
    public partial class AFPANADbContext : DbContext
    {
        public AFPANADbContext()
        {
           ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public AFPANADbContext(DbContextOptions<AFPANADbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppelationRome> AppelationRomes { get; set; }
        public virtual DbSet<Beneficiaire> Beneficiaires { get; set; }
        public virtual DbSet<BeneficiaireOffreFormation> BeneficiaireOffreFormations { get; set; }
        public virtual DbSet<CampagneMail> CampagneMails { get; set; }
        public virtual DbSet<CategorieEvenement> CategorieEvenements { get; set; }
        public virtual DbSet<ChampProfessionnnel> ChampProfessionnnels { get; set; }
        public virtual DbSet<CodeResultatCertification> CodeResultatCertifications { get; set; }
        public virtual DbSet<CollaborateurAfpa> CollaborateurAfpas { get; set; }
        public virtual DbSet<Contrat> Contrats { get; set; }
        public virtual DbSet<DestinataireEnquete> DestinataireEnquetes { get; set; }
        public virtual DbSet<DomaineMetierRome> DomaineMetierRomes { get; set; }
        public virtual DbSet<Entreprise> Entreprises { get; set; }
        public virtual DbSet<EntrepriseProfessionnel> EntrepriseProfessionnels { get; set; }
        public virtual DbSet<Etablissement> Etablissements { get; set; }
        public virtual DbSet<Evenement> Evenements { get; set; }
        public virtual DbSet<EvenementDocument> EvenementDocuments { get; set; }
        public virtual DbSet<FamilleMetierRome> FamilleMetierRomes { get; set; }
        public virtual DbSet<OffreFormation> OffreFormations { get; set; }
        public virtual DbSet<Pays> Pays { get; set; }
        public virtual DbSet<Pee> Pees { get; set; }
        public virtual DbSet<PeeDocument> PeeDocuments { get; set; }
        public virtual DbSet<PeriodePee> PeriodePees { get; set; }
        public virtual DbSet<PeriodePeeEvenement> PeriodePeeEvenements { get; set; }
        public virtual DbSet<PlanificationCampagneMail> PlanificationCampagneMails { get; set; }
        public virtual DbSet<ProduitFormation> ProduitFormations { get; set; }
        public virtual DbSet<ProduitFormationAppellationRome> ProduitFormationAppellationRomes { get; set; }
        public virtual DbSet<Professionnel> Professionnels { get; set; }
        public virtual DbSet<Rome> Romes { get; set; }
        public virtual DbSet<TitreCivilite> TitreCivilites { get; set; }
        public virtual DbSet<TypeContrat> TypeContrats { get; set; }
        public virtual DbSet<UniteOrganisationnelle> UniteOrganisationnelles { get; set; }
        public virtual DbSet<UniteOrganisationnelleChampProfessionnel> UniteOrganisationnelleChampProfessionnels { get; set; }
        public virtual DbSet<InsertionsTroisMois> InsertionTroisMois { get; set; }
        public virtual DbSet<InsertionsSixMois> InsertionSixMois { get; set; }
        public virtual DbSet<InsertionsDouzeMois> InsertionDouzeMois { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("data source=localhost;initial catalog=AFPANA;integrated security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "French_CI_AS");

            modelBuilder.Entity<AppelationRome>(entity =>
            {
                entity.HasKey(e => e.CodeAppelationRome);

                entity.ToTable("AppelationRome");

                entity.Property(e => e.CodeAppelationRome).ValueGeneratedNever();

                entity.Property(e => e.CodeRome)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.LibelleAppelationRome)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodeRomeNavigation)
                    .WithMany(p => p.AppelationRomes)
                    .HasForeignKey(d => d.CodeRome)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppelationRome_CodeRome");
            });

            modelBuilder.Entity<Beneficiaire>(entity =>
            {
                entity.HasKey(e => e.MatriculeBeneficiaire);

                entity.ToTable("Beneficiaire");

                entity.HasIndex(e => e.CodeTitreCivilite, "IX_Beneficiaire_CodeTitreCivilite");
                entity.HasIndex(e => e.IdPays2, "IX_Beneficiaire_Idpays2");

                entity.Property(e => e.MatriculeBeneficiaire)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.CodePostal)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DateNaissanceBeneficiaire).HasColumnType("date");

                entity.Property(e => e.IdPays2)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Ligne1Adresse)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Ligne2Adresse)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Ligne3Adresse)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MailBeneficiaire)
                    .HasMaxLength(254)
                    .IsUnicode(false);

                entity.Property(e => e.MailingAutorise)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.NomBeneficiaire)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PathPhoto)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PrenomBeneficiaire)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TelBeneficiaire)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(128)
                    .HasColumnName("UserID");

                entity.Property(e => e.Ville)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodeTitreCiviliteNavigation)
                    .WithMany(p => p.Beneficiaires)
                    .HasForeignKey(d => d.CodeTitreCivilite)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Beneficiaire_TitreCivilite"); 
                
                entity.HasOne(d => d.PaysNavigation)
                      .WithMany(p => p.Beneficiaires)
                      .HasForeignKey(d => d.IdPays2)
                      .HasConstraintName("FK_Beneficiaire_Pays");
           // });
        });
            modelBuilder.Entity<InsertionsTroisMois>(entity =>
            {
                entity.HasKey(e => new { e.IdEtablissement, e.IdOffreFormation, e.Annee });
                entity.ToTable("InsertionsTroisMois");
                entity.Property(e => e.IdEtablissement)
                .HasMaxLength(5)
                .IsFixedLength(true);

                entity.Property(e => e.TotalReponse).HasDefaultValue(0);
                entity.Property(e => e.Cdi).HasDefaultValue(0);
                entity.Property(e => e.Cdd) .HasDefaultValue(0);
                entity.Property(e => e.Alternance).HasDefaultValue(0);
                entity.Property(e => e.SansEmploie).HasDefaultValue(0);
                entity.Property(e => e.Autres).HasDefaultValue(0);
            });

            modelBuilder.Entity<InsertionsSixMois>(entity =>
            {
                entity.HasKey(e => new { e.IdEtablissement, e.IdOffreFormation, e.Annee });
                entity.ToTable("InsertionsSixMois");
                entity.Property(e => e.IdEtablissement)
                .HasMaxLength(5)
                .IsFixedLength(true);

                entity.Property(e => e.TotalReponse).HasDefaultValue(0);
                entity.Property(e => e.Cdi).HasDefaultValue(0);
                entity.Property(e => e.Cdd).HasDefaultValue(0);
                entity.Property(e => e.Alternance).HasDefaultValue(0);
                entity.Property(e => e.SansEmploie).HasDefaultValue(0);
                entity.Property(e => e.Autres).HasDefaultValue(0);
            });

            modelBuilder.Entity<InsertionsDouzeMois>(entity =>
            {
                entity.HasKey(e => new { e.IdEtablissement, e.IdOffreFormation, e.Annee });
                entity.ToTable("InsertionsDouzeMois");
                entity.Property(e => e.IdEtablissement)
                .HasMaxLength(5)
                .IsFixedLength(true);

                entity.Property(e => e.TotalReponse).HasDefaultValue(0);
                entity.Property(e => e.Cdi).HasDefaultValue(0);
                entity.Property(e => e.Cdd).HasDefaultValue(0);
                entity.Property(e => e.Alternance).HasDefaultValue(0);
                entity.Property(e => e.SansEmploie).HasDefaultValue(0);
                entity.Property(e => e.Autres).HasDefaultValue(0);
            });
                

            modelBuilder.Entity<BeneficiaireOffreFormation>(entity =>
            {
                entity.HasKey(e => new { e.MatriculeBeneficiaire, e.IdOffreFormation, e.Idetablissement });

                entity.ToTable("Beneficiaire_OffreFormation");

                entity.Property(e => e.MatriculeBeneficiaire)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Idetablissement)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("IDEtablissement")
                    .IsFixedLength(true);

                entity.Property(e => e.Certifie)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('ANT')")
                    .IsFixedLength(true);

                entity.Property(e => e.Consultable).HasDefaultValueSql("((1))");

                entity.Property(e => e.DateEntreeBeneficiaire).HasColumnType("date");

                entity.Property(e => e.DateSortieBeneficiaire).HasColumnType("date");

                entity.HasOne(d => d.CertifieNavigation)
                    .WithMany(p => p.BeneficiaireOffreFormations)
                    .HasForeignKey(d => d.Certifie)
                    .HasConstraintName("FK_Beneficiaire_OffreFormation_ResultatCertification");

                entity.HasOne(d => d.MatriculeBeneficiaireNavigation)
                    .WithMany(p => p.BeneficiaireOffreFormations)
                    .HasForeignKey(d => d.MatriculeBeneficiaire)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Beneficiaire_OffreFormation_Beneficiaire");

                entity.HasOne(d => d.Id)
                    .WithMany(p => p.BeneficiaireOffreFormations)
                    .HasForeignKey(d => new { d.IdOffreFormation, d.Idetablissement })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Beneficiaire_OffreFormation_OffreFormation");
            });

            modelBuilder.Entity<CampagneMail>(entity =>
            {
                entity.HasKey(e => e.IdCampagneMail);

                entity.ToTable("CampagneMail");

                entity.Property(e => e.DateCreation).HasColumnType("date");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.IdEtablissement)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Id)
                    .WithMany(p => p.CampagneMails)
                    .HasForeignKey(d => new { d.IdOffreFormation, d.IdEtablissement })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CampagneMail_OffreFormation");
            });

            modelBuilder.Entity<CategorieEvenement>(entity =>
            {
                entity.HasKey(e => e.IdCatEvent);

                entity.ToTable("CategorieEvenement");

                entity.Property(e => e.IdCatEvent)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.LibelleEvent)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ChampProfessionnnel>(entity =>
            {
                entity.HasKey(e => e.IdChampProfessionnel);

                entity.ToTable("ChampProfessionnnel");

                entity.Property(e => e.IdChampProfessionnel)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.LibelleChampProfessionnel)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CodeResultatCertification>(entity =>
            {
                entity.HasKey(e => e.CodeResultatCertification1)
                    .HasName("PK_ResultatCertification");

                entity.ToTable("CodeResultatCertification");

                entity.Property(e => e.CodeResultatCertification1)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CodeResultatCertification")
                    .IsFixedLength(true);

                entity.Property(e => e.LibelleResultatCertification)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<CollaborateurAfpa>(entity =>
            {
                entity.HasKey(e => e.MatriculeCollaborateurAfpa)
                    .HasName("PK_CollaborateurAfpa_1");

                entity.ToTable("CollaborateurAfpa");

                entity.Property(e => e.MatriculeCollaborateurAfpa)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.IdEtablissement)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.MailCollaborateurAfpa)
                    .HasMaxLength(254)
                    .IsUnicode(false);

                entity.Property(e => e.NomCollaborateur)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PrenomCollaborateur)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.TelCollaborateurAfpa)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Uo)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("UO")
                    .IsFixedLength(true);

                entity.Property(e => e.UserId)
                    .HasMaxLength(128)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.CodeTitreCiviliteNavigation)
                    .WithMany(p => p.CollaborateurAfpas)
                    .HasForeignKey(d => d.CodeTitreCivilite)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CollaborateurAfpa_TitreCivilite");

                entity.HasOne(d => d.IdEtablissementNavigation)
                    .WithMany(p => p.CollaborateurAfpas)
                    .HasForeignKey(d => d.IdEtablissement)
                    .HasConstraintName("FK_CollaborateurAfpa_Etablissement");

                entity.HasOne(d => d.UoNavigation)
                    .WithMany(p => p.CollaborateurAfpas)
                    .HasForeignKey(d => d.Uo)
                    .HasConstraintName("FK_CollaborateurAfpa_UniteOrganisationnelle");
            });

            modelBuilder.Entity<Contrat>(entity =>
            {
                entity.HasKey(e => e.IdContrat);

                entity.ToTable("Contrat");

                entity.Property(e => e.DateEntreeFonction).HasColumnType("date");

                entity.Property(e => e.DateSortieFonction).HasColumnType("date");

                entity.Property(e => e.LibelleFonction)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.MatriculeBeneficiaire)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.IdEntrepriseNavigation)
                    .WithMany(p => p.Contrats)
                    .HasForeignKey(d => d.IdEntreprise)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contrat_Entreprise");

                entity.HasOne(d => d.MatriculeBeneficiaireNavigation)
                    .WithMany(p => p.Contrats)
                    .HasForeignKey(d => d.MatriculeBeneficiaire)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contrat_Beneficiaire");

                entity.HasOne(d => d.TypeContratNavigation)
                    .WithMany(p => p.Contrats)
                    .HasForeignKey(d => d.TypeContrat)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contrat_TypeContrat");
            });

            modelBuilder.Entity<DestinataireEnquete>(entity =>
            {
                entity.HasKey(e => e.IdSoumissionnaire);

                entity.ToTable("DestinataireEnquete");

                entity.Property(e => e.IdSoumissionnaire).ValueGeneratedNever();

                entity.Property(e => e.Agrege).HasDefaultValueSql("('0')");

                entity.Property(e => e.DateEcheanceEnquete).HasColumnType("date");

                entity.Property(e => e.DateRealisationEnquete).HasColumnType("datetime");

                entity.Property(e => e.DateRelance1).HasColumnType("date");

                entity.Property(e => e.DateRelance2).HasColumnType("date");

                entity.Property(e => e.Repondu).HasDefaultValue(false);

                entity.Property(e => e.IdEtablissement)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.MatriculeBeneficiaire)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.IdContratNavigation)
                    .WithMany(p => p.DestinataireEnquetes)
                    .HasForeignKey(d => d.IdContrat)
                    .HasConstraintName("FK_DestinataireEnquete_Contrat");

                entity.HasOne(d => d.IdPlanificationCampagneMailNavigation)
                    .WithMany(p => p.DestinataireEnquetes)
                    .HasForeignKey(d => d.IdPlanificationCampagneMail)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DestinataireEnquete_PlanificationCampagneMail");

                entity.HasOne(d => d.MatriculeBeneficiaireNavigation)
                    .WithMany(p => p.DestinataireEnquetes)
                    .HasForeignKey(d => d.MatriculeBeneficiaire)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DestinataireEnquete_Beneficiaire");
            });

            modelBuilder.Entity<DomaineMetierRome>(entity =>
            {
                entity.HasKey(e => e.CodeDomaineRome);

                entity.ToTable("DomaineMetierRome");

                entity.Property(e => e.CodeDomaineRome)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.CodeFamilleRome)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.IntituleDomaineRome)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodeFamilleRomeNavigation)
                    .WithMany(p => p.DomaineMetierRomes)
                    .HasForeignKey(d => d.CodeFamilleRome)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DomaineMetierRome_FamilleMetierRome");
            });

            modelBuilder.Entity<Entreprise>(entity =>
            {
                entity.HasKey(e => e.IdEntreprise);

                entity.ToTable("Entreprise");

                entity.Property(e => e.CodePostal)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Idpays2)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("IDPays2")
                    .IsFixedLength(true);

                entity.Property(e => e.Ligne1Adresse)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Ligne2Adresse)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Ligne3Adresse)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MailEntreprise)
                    .HasMaxLength(254)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroSiret)
                    .IsRequired()
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("NumeroSIRET");

                entity.Property(e => e.RaisonSociale)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TelEntreprise)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Ville)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Idpays2Navigation)
                    .WithMany(p => p.Entreprises)
                    .HasForeignKey(d => d.Idpays2)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Entreprise_Pays");
            });

            modelBuilder.Entity<EntrepriseProfessionnel>(entity =>
            {
                entity.HasKey(e => new { e.IdEntreprise, e.IdProfessionnel });

                entity.ToTable("Entreprise_Professionnel");

                entity.Property(e => e.Actif)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.AdresseMailPro)
                    .HasMaxLength(254)
                    .IsUnicode(false);

                entity.Property(e => e.Fonction).HasMaxLength(255);

                entity.Property(e => e.TelephonePro)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Etablissement>(entity =>
            {
                entity.HasKey(e => e.IdEtablissement)
                    .HasName("PK_Etablissement_1");

                entity.ToTable("Etablissement");

                entity.Property(e => e.IdEtablissement)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.CodePostal)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.IdEtablissementRattachement)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Ligne1Adresse)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Ligne2Adresse)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Ligne3Adresse)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MailEtablissement)
                    .HasMaxLength(254)
                    .IsUnicode(false);

                entity.Property(e => e.NomEtablissement)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TelEtablissement)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Ville)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEtablissementRattachementNavigation)
                    .WithMany(p => p.InverseIdEtablissementRattachementNavigation)
                    .HasForeignKey(d => d.IdEtablissementRattachement)
                    .HasConstraintName("FK_Etablissement_Etablissement1");
            });

            modelBuilder.Entity<Evenement>(entity =>
            {
                entity.HasKey(e => e.IdEvent);

                entity.ToTable("Evenement");

                entity.Property(e => e.IdEvent)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.DateEvent).HasColumnType("datetime");

                entity.Property(e => e.DétailsEvent)
                    .IsRequired()
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.IdCategorieEvent)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.IdEtablissement)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.IdCategorieEventNavigation)
                    .WithMany(p => p.Evenements)
                    .HasForeignKey(d => d.IdCategorieEvent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Evenement_CategorieEvenement");

                entity.HasOne(d => d.IdEtablissementNavigation)
                    .WithMany(p => p.Evenements)
                    .HasForeignKey(d => d.IdEtablissement)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Evenement_Etablissement");
            });

            modelBuilder.Entity<EvenementDocument>(entity =>
            {
                entity.HasKey(e => new { e.IdEvent, e.NumOrdre });

                entity.ToTable("Evenement_Document");

                entity.Property(e => e.IdEvent).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.PathDocument)
                    .IsRequired()
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEventNavigation)
                    .WithMany(p => p.EvenementDocuments)
                    .HasForeignKey(d => d.IdEvent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Evenement_Document_Evenement");
            });

            modelBuilder.Entity<FamilleMetierRome>(entity =>
            {
                entity.HasKey(e => e.CodeFamilleMetierRome);

                entity.ToTable("FamilleMetierRome");

                entity.Property(e => e.CodeFamilleMetierRome)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.IntituleFamilleMetierRome)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<OffreFormation>(entity =>
            {
                entity.HasKey(e => new { e.IdOffreFormation, e.IdEtablissement });

                entity.ToTable("OffreFormation");

                entity.Property(e => e.IdEtablissement)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.DateDebutOffreFormation).HasColumnType("date");

                entity.Property(e => e.DateFinOffreFormation).HasColumnType("date");

                entity.Property(e => e.LibelleOffreFormation)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.LibelleReduitOffreFormation)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.MatriculeCollaborateurAfpa)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.CodeProduitFormationNavigation)
                    .WithMany(p => p.OffreFormations)
                    .HasForeignKey(d => d.CodeProduitFormation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OffreFormation_ProduitDeFormation");

                entity.HasOne(d => d.IdEtablissementNavigation)
                    .WithMany(p => p.OffreFormations)
                    .HasForeignKey(d => d.IdEtablissement)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OffreFormation_Etablissement");

                entity.HasOne(d => d.MatriculeCollaborateurAfpaNavigation)
                    .WithMany(p => p.OffreFormations)
                    .HasForeignKey(d => d.MatriculeCollaborateurAfpa)
                    .HasConstraintName("FK_OffreFormation_CollaborateurAfpa");
            });

            modelBuilder.Entity<Pays>(entity =>
            {
                entity.HasKey(e => e.Idpays2);

                entity.Property(e => e.Idpays2)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("IDPays2")
                    .IsFixedLength(true);

                entity.Property(e => e.Idpays3)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("IDPays3")
                    .IsFixedLength(true);

                entity.Property(e => e.IdpaysNum).HasColumnName("IDPaysNum");

                entity.Property(e => e.LibellePays)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Pee>(entity =>
            {
                entity.HasKey(e => e.IdPee);

                entity.ToTable("Pee");

                entity.Property(e => e.IdPee)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IdEtablissement)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.MatriculeBeneficiaire)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.IdEntrepriseNavigation)
                    .WithMany(p => p.Pees)
                    .HasForeignKey(d => d.IdEntreprise)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pee_Entreprise");

                entity.HasOne(d => d.IdResponsableJuridiqueNavigation)
                    .WithMany(p => p.PeeIdResponsableJuridiqueNavigations)
                    .HasForeignKey(d => d.IdResponsableJuridique)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pee_ResponsableJuridique");

                entity.HasOne(d => d.IdTuteurNavigation)
                    .WithMany(p => p.PeeIdTuteurNavigations)
                    .HasForeignKey(d => d.IdTuteur)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pee_Tuteur");

                entity.HasOne(d => d.MatriculeBeneficiaireNavigation)
                    .WithMany(p => p.Pees)
                    .HasForeignKey(d => d.MatriculeBeneficiaire)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pee_Beneficiaire");

                entity.HasOne(d => d.Id)
                    .WithMany(p => p.Pees)
                    .HasForeignKey(d => new { d.IdOffreFormation, d.IdEtablissement })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pee_OffreFormation");
            });

            modelBuilder.Entity<PeeDocument>(entity =>
            {
                entity.HasKey(e => new { e.IdPee, e.NumOrdre });

                entity.ToTable("Pee_Document");

                entity.Property(e => e.IdPee)
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.PathDocument)
                    .IsRequired()
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.HasOne(d => d.idPeeNavigation)
                    .WithMany(p => p.PeeDocument)
                    .HasForeignKey(d => d.IdPee)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pee_Document_Pee");
            });

            modelBuilder.Entity<PeriodePee>(entity =>
            {
                entity.HasKey(e => new { e.IdPee, e.NumOrdre })
                    .HasName("PK_Periode_Pee_1");

                entity.ToTable("Periode_Pee");

                entity.Property(e => e.IdPee).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.DateDebutPeriodePee).HasColumnType("date");

                entity.Property(e => e.DateFinPeriodePee).HasColumnType("date");

                entity.HasOne(d => d.IdPeeNavigation)
                    .WithMany(p => p.PeriodePees)
                    .HasForeignKey(d => d.IdPee)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Periode_Pee_Pee");
            });

            modelBuilder.Entity<PeriodePeeEvenement>(entity =>
            {
                entity.HasKey(e => new { e.IdPee, e.NumOrdre, e.IdEvent });

                entity.ToTable("Periode_Pee_Evenement");

                entity.Property(e => e.IdPee).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.IdEvent).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<PlanificationCampagneMail>(entity =>
            {
                entity.HasKey(e => e.IdPlanificationCampagneMail)
                    .HasName("PK_PlanificationCampagne");

                entity.ToTable("PlanificationCampagneMail");

                entity.Property(e => e.DateRealisation).HasColumnType("date");

                entity.Property(e => e.Echeance).HasColumnType("date");

                entity.Property(e => e.Type)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.IdCampagneMailNavigation)
                    .WithMany(p => p.PlanificationCampagneMails)
                    .HasForeignKey(d => d.IdCampagneMail)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlanificationCampagneMail_CampagneMail");
            });

            modelBuilder.Entity<ProduitFormation>(entity =>
            {
                entity.HasKey(e => e.CodeProduitFormation)
                    .HasName("PK_ProduitDeFormation");

                entity.ToTable("ProduitFormation");

                entity.Property(e => e.CodeProduitFormation).ValueGeneratedNever();

                entity.Property(e => e.LibelleCourtFormation)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LibelleProduitFormation)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.NiveauFormation)
                    .HasMaxLength(5)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<ProduitFormationAppellationRome>(entity =>
            {
                entity.HasKey(e => new { e.CodeProduitFormation, e.CodeRome });

                entity.ToTable("ProduitFormation_AppellationRome");

                entity.Property(e => e.CodeRome)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.CodeProduitFormationNavigation)
                    .WithMany(p => p.ProduitFormationAppellationRomes)
                    .HasForeignKey(d => d.CodeProduitFormation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProduitFormation_AppellationRome_ProduitFormation");

                entity.HasOne(d => d.CodeRomeNavigation)
                    .WithMany(p => p.ProduitFormationAppellationRomes)
                    .HasForeignKey(d => d.CodeRome)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProduitFormation_AppellationRome_Rome");
            });

            modelBuilder.Entity<Professionnel>(entity =>
            {
                entity.HasKey(e => e.IdProfessionnel);

                entity.ToTable("Professionnel");

                entity.Property(e => e.NomProfessionnel)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PrenomProfessionnel)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.HasOne(d => d.TitreCiviliteNavigation)
                   .WithMany(p => p.Professionnels)
                   .HasForeignKey(d => d.CodeTitreCiviliteProfessionnel)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("CodeTitreCiviliteProfessionnel");
            });

            modelBuilder.Entity<Rome>(entity =>
            {
                entity.HasKey(e => e.CodeRome)
                    .HasName("PK_CodeRome");

                entity.ToTable("Rome");

                entity.Property(e => e.CodeRome)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.CodeDomaineRome)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.IntituleCodeRome)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodeDomaineRomeNavigation)
                    .WithMany(p => p.Romes)
                    .HasForeignKey(d => d.CodeDomaineRome)
                    .HasConstraintName("FK_Rome_DomaineMetierRome");
            });

            modelBuilder.Entity<TitreCivilite>(entity =>
            {
                entity.HasKey(e => e.CodeTitreCivilite);

                entity.ToTable("TitreCivilite");

                entity.Property(e => e.CodeTitreCivilite).ValueGeneratedNever();

                entity.Property(e => e.TitreCiviliteAbrege)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.TitreCiviliteComplet)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TypeContrat>(entity =>
            {
                entity.HasKey(e => e.IdTypeContrat);

                entity.ToTable("TypeContrat");

                entity.Property(e => e.DesignationTypeContrat)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("designationTypeContrat");
            });

            modelBuilder.Entity<UniteOrganisationnelle>(entity =>
            {
                entity.HasKey(e => e.Uo);

                entity.ToTable("UniteOrganisationnelle");

                entity.Property(e => e.Uo)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("UO")
                    .IsFixedLength(true);

                entity.Property(e => e.LibelleUo)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("LibelleUO");
            });

            modelBuilder.Entity<UniteOrganisationnelleChampProfessionnel>(entity =>
            {
                entity.HasKey(e => new { e.Uo, e.IdChampProfessionnel })
                    .HasName("PK_Uo_ChampProfessionnel");

                entity.ToTable("UniteOrganisationnelle_ChampProfessionnel");

                entity.Property(e => e.Uo)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("UO")
                    .IsFixedLength(true);

                entity.Property(e => e.IdChampProfessionnel)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.IdChampProfessionnelNavigation)
                    .WithMany(p => p.UniteOrganisationnelleChampProfessionnels)
                    .HasForeignKey(d => d.IdChampProfessionnel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Uo_ChampProfessionnel_ChampProfessionnnel");

                entity.HasOne(d => d.UoNavigation)
                    .WithMany(p => p.UniteOrganisationnelleChampProfessionnels)
                    .HasForeignKey(d => d.Uo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Uo_ChampProfessionnel_UniteOrganisationnelle");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
