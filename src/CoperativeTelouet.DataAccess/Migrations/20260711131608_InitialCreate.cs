using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoperativeTelouet.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SocieteNom = table.Column<string>(type: "TEXT", nullable: true),
                    SocieteAdresse = table.Column<string>(type: "TEXT", nullable: true),
                    SocieteICE = table.Column<string>(type: "TEXT", nullable: true),
                    SocieteMentionsLegales = table.Column<string>(type: "TEXT", nullable: true),
                    SocieteLogoPath = table.Column<string>(type: "TEXT", nullable: true),
                    TauxTVAJson = table.Column<string>(type: "TEXT", nullable: true),
                    BlocageSiStockInsuffisant = table.Column<bool>(type: "INTEGER", nullable: false),
                    DevisValiditeJoursDefaut = table.Column<int>(type: "INTEGER", nullable: false),
                    Devise = table.Column<string>(type: "TEXT", nullable: true),
                    UiLanguage = table.Column<string>(type: "TEXT", nullable: true),
                    BackupEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    BackupIntervalHours = table.Column<int>(type: "INTEGER", nullable: false),
                    BackupIntervalUnit = table.Column<string>(type: "TEXT", nullable: true),
                    BackupRetentionDays = table.Column<int>(type: "INTEGER", nullable: false),
                    BackupDirectory = table.Column<string>(type: "TEXT", nullable: true),
                    LastBackupDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    PrixStockageParBacParJour = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nom = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChambresFroides",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nom = table.Column<string>(type: "TEXT", nullable: false),
                    CapaciteBacs = table.Column<int>(type: "INTEGER", nullable: false),
                    Actif = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChambresFroides", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Reference = table.Column<string>(type: "TEXT", nullable: false),
                    Designation = table.Column<string>(type: "TEXT", nullable: false),
                    Unite = table.Column<string>(type: "TEXT", nullable: true),
                    PrixVenteHT = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    CoutHT = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    TauxTVA = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Actif = table.Column<bool>(type: "INTEGER", nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    ImageData = table.Column<byte[]>(type: "BLOB", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockBacsSociete",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BacsVides = table.Column<int>(type: "INTEGER", nullable: false),
                    BacsPleins = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalBacsOriginal = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockBacsSociete", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tiers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Nom = table.Column<string>(type: "TEXT", nullable: false),
                    ICE = table.Column<string>(type: "TEXT", nullable: true),
                    Adresse = table.Column<string>(type: "TEXT", nullable: true),
                    Ville = table.Column<string>(type: "TEXT", nullable: true),
                    Telephone = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    ConditionsPaiement = table.Column<string>(type: "TEXT", nullable: true),
                    Actif = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tiers", x => x.Id);
                    table.CheckConstraint("CK_Tiers_Type", "Type IN ('Client','Fournisseur','LesDeux')");
                });

            migrationBuilder.CreateTable(
                name: "TypesCharge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nom = table.Column<string>(type: "TEXT", nullable: false),
                    Actif = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesCharge", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VarietesPomme",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nom = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VarietesPomme", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Reference = table.Column<string>(type: "TEXT", nullable: false),
                    CodeBarre = table.Column<string>(type: "TEXT", nullable: true),
                    Designation = table.Column<string>(type: "TEXT", nullable: false),
                    Unite = table.Column<string>(type: "TEXT", nullable: true),
                    PrixAchatHT = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    PrixVenteHT = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    TauxTVA = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    StockActuel = table.Column<decimal>(type: "TEXT", precision: 18, scale: 3, nullable: false),
                    StockMinimum = table.Column<decimal>(type: "TEXT", precision: 18, scale: 3, nullable: false),
                    CategorieId = table.Column<int>(type: "INTEGER", nullable: true),
                    Actif = table.Column<bool>(type: "INTEGER", nullable: false),
                    ImageData = table.Column<byte[]>(type: "BLOB", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produits_Categories_CategorieId",
                        column: x => x.CategorieId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DevisClient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Numero = table.Column<string>(type: "TEXT", nullable: false),
                    ClientId = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateValidite = table.Column<DateTime>(type: "TEXT", nullable: false),
                    RemiseGlobale = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DevisClient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DevisClient_Tiers_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Tiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DevisFournisseur",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Numero = table.Column<string>(type: "TEXT", nullable: false),
                    FournisseurId = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateValidite = table.Column<DateTime>(type: "TEXT", nullable: false),
                    RemiseGlobale = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DevisFournisseur", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DevisFournisseur_Tiers_FournisseurId",
                        column: x => x.FournisseurId,
                        principalTable: "Tiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Charges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TypeChargeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Libelle = table.Column<string>(type: "TEXT", nullable: false),
                    FournisseurId = table.Column<int>(type: "INTEGER", nullable: true),
                    BeneficiaireLibre = table.Column<string>(type: "TEXT", nullable: true),
                    MontantTtc = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Charges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Charges_Tiers_FournisseurId",
                        column: x => x.FournisseurId,
                        principalTable: "Tiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Charges_TypesCharge_TypeChargeId",
                        column: x => x.TypeChargeId,
                        principalTable: "TypesCharge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BonsEntreeStockage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Numero = table.Column<string>(type: "TEXT", nullable: false),
                    ClientId = table.Column<int>(type: "INTEGER", nullable: false),
                    DateEntree = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EtatBac = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    ChambreFroideId = table.Column<int>(type: "INTEGER", nullable: true),
                    VarieteId = table.Column<int>(type: "INTEGER", nullable: true),
                    NumeroLot = table.Column<string>(type: "TEXT", nullable: true),
                    NombreBacs = table.Column<int>(type: "INTEGER", nullable: false),
                    PrixParBacParJourApplique = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: true),
                    BacsVidesAvant = table.Column<int>(type: "INTEGER", nullable: false),
                    BacsVidesApres = table.Column<int>(type: "INTEGER", nullable: false),
                    BacsPleinsAvant = table.Column<int>(type: "INTEGER", nullable: false),
                    BacsPleinsApres = table.Column<int>(type: "INTEGER", nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonsEntreeStockage", x => x.Id);
                    table.CheckConstraint("CK_BonsEntreeStockage_EtatBac", "EtatBac IN ('Vide','Plein')");
                    table.CheckConstraint("CK_BonsEntreeStockage_EtatBac_Fields", "(EtatBac = 'Vide' AND ChambreFroideId IS NULL AND VarieteId IS NULL AND NumeroLot IS NULL AND PrixParBacParJourApplique IS NULL) OR (EtatBac = 'Plein' AND ChambreFroideId IS NOT NULL AND VarieteId IS NOT NULL AND NumeroLot IS NOT NULL AND PrixParBacParJourApplique IS NOT NULL)");
                    table.ForeignKey(
                        name: "FK_BonsEntreeStockage_ChambresFroides_ChambreFroideId",
                        column: x => x.ChambreFroideId,
                        principalTable: "ChambresFroides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BonsEntreeStockage_Tiers_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Tiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BonsEntreeStockage_VarietesPomme_VarieteId",
                        column: x => x.VarieteId,
                        principalTable: "VarietesPomme",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MouvementsStock",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProduitId = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    StockAvant = table.Column<decimal>(type: "TEXT", precision: 18, scale: 3, nullable: false),
                    Quantite = table.Column<decimal>(type: "TEXT", precision: 18, scale: 3, nullable: false),
                    OrigineType = table.Column<string>(type: "TEXT", nullable: true),
                    OrigineId = table.Column<int>(type: "INTEGER", nullable: true),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MouvementsStock", x => x.Id);
                    table.CheckConstraint("CK_MouvementsStock_Type", "Type IN ('Entree','Sortie','Ajustement')");
                    table.ForeignKey(
                        name: "FK_MouvementsStock_Produits_ProduitId",
                        column: x => x.ProduitId,
                        principalTable: "Produits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DevisClientConditions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DevisClientId = table.Column<int>(type: "INTEGER", nullable: false),
                    Titre = table.Column<string>(type: "TEXT", nullable: false),
                    Valeur = table.Column<string>(type: "TEXT", nullable: false),
                    Ordre = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DevisClientConditions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DevisClientConditions_DevisClient_DevisClientId",
                        column: x => x.DevisClientId,
                        principalTable: "DevisClient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DevisClientLignes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DevisClientId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProduitId = table.Column<int>(type: "INTEGER", nullable: true),
                    ServiceId = table.Column<int>(type: "INTEGER", nullable: true),
                    Designation = table.Column<string>(type: "TEXT", nullable: false),
                    Quantite = table.Column<decimal>(type: "TEXT", precision: 18, scale: 3, nullable: false),
                    PrixUnitaireHT = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Remise = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    TauxTVA = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Conditionnement = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DevisClientLignes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DevisClientLignes_DevisClient_DevisClientId",
                        column: x => x.DevisClientId,
                        principalTable: "DevisClient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DevisClientLignes_Produits_ProduitId",
                        column: x => x.ProduitId,
                        principalTable: "Produits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DevisClientLignes_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FacturesClient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Numero = table.Column<string>(type: "TEXT", nullable: false),
                    ClientId = table.Column<int>(type: "INTEGER", nullable: false),
                    DevisClientId = table.Column<int>(type: "INTEGER", nullable: true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateEcheance = table.Column<DateTime>(type: "TEXT", nullable: true),
                    EstPayee = table.Column<bool>(type: "INTEGER", nullable: false),
                    RemiseGlobale = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    TotalTtc = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    BonCommandeReference = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacturesClient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacturesClient_DevisClient_DevisClientId",
                        column: x => x.DevisClientId,
                        principalTable: "DevisClient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_FacturesClient_Tiers_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Tiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DevisFournisseurConditions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DevisFournisseurId = table.Column<int>(type: "INTEGER", nullable: false),
                    Titre = table.Column<string>(type: "TEXT", nullable: false),
                    Valeur = table.Column<string>(type: "TEXT", nullable: false),
                    Ordre = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DevisFournisseurConditions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DevisFournisseurConditions_DevisFournisseur_DevisFournisseurId",
                        column: x => x.DevisFournisseurId,
                        principalTable: "DevisFournisseur",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DevisFournisseurLignes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DevisFournisseurId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProduitId = table.Column<int>(type: "INTEGER", nullable: true),
                    ServiceId = table.Column<int>(type: "INTEGER", nullable: true),
                    Designation = table.Column<string>(type: "TEXT", nullable: false),
                    Quantite = table.Column<decimal>(type: "TEXT", precision: 18, scale: 3, nullable: false),
                    PrixUnitaireHT = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Remise = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    TauxTVA = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Conditionnement = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DevisFournisseurLignes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DevisFournisseurLignes_DevisFournisseur_DevisFournisseurId",
                        column: x => x.DevisFournisseurId,
                        principalTable: "DevisFournisseur",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DevisFournisseurLignes_Produits_ProduitId",
                        column: x => x.ProduitId,
                        principalTable: "Produits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DevisFournisseurLignes_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FacturesFournisseur",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Numero = table.Column<string>(type: "TEXT", nullable: false),
                    FournisseurId = table.Column<int>(type: "INTEGER", nullable: false),
                    DevisFournisseurId = table.Column<int>(type: "INTEGER", nullable: true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateEcheance = table.Column<DateTime>(type: "TEXT", nullable: true),
                    EstPayee = table.Column<bool>(type: "INTEGER", nullable: false),
                    RemiseGlobale = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    TotalTtc = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacturesFournisseur", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacturesFournisseur_DevisFournisseur_DevisFournisseurId",
                        column: x => x.DevisFournisseurId,
                        principalTable: "DevisFournisseur",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_FacturesFournisseur_Tiers_FournisseurId",
                        column: x => x.FournisseurId,
                        principalTable: "Tiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AvoirsClient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Numero = table.Column<string>(type: "TEXT", nullable: false),
                    FactureClientId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClientId = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Motif = table.Column<string>(type: "TEXT", nullable: true),
                    RetourMarchandise = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvoirsClient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AvoirsClient_FacturesClient_FactureClientId",
                        column: x => x.FactureClientId,
                        principalTable: "FacturesClient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AvoirsClient_Tiers_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Tiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BonsCommandeClient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Numero = table.Column<string>(type: "TEXT", nullable: false),
                    ClientId = table.Column<int>(type: "INTEGER", nullable: false),
                    DevisClientId = table.Column<int>(type: "INTEGER", nullable: true),
                    FactureClientId = table.Column<int>(type: "INTEGER", nullable: true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonsCommandeClient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BonsCommandeClient_DevisClient_DevisClientId",
                        column: x => x.DevisClientId,
                        principalTable: "DevisClient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_BonsCommandeClient_FacturesClient_FactureClientId",
                        column: x => x.FactureClientId,
                        principalTable: "FacturesClient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_BonsCommandeClient_Tiers_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Tiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BonsSortieStockage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Numero = table.Column<string>(type: "TEXT", nullable: false),
                    ClientId = table.Column<int>(type: "INTEGER", nullable: false),
                    DateSortie = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EtatBac = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    NombreBacs = table.Column<int>(type: "INTEGER", nullable: false),
                    BonEntreeStockageId = table.Column<int>(type: "INTEGER", nullable: true),
                    FactureClientId = table.Column<int>(type: "INTEGER", nullable: true),
                    BacsVidesAvant = table.Column<int>(type: "INTEGER", nullable: false),
                    BacsVidesApres = table.Column<int>(type: "INTEGER", nullable: false),
                    BacsPleinsAvant = table.Column<int>(type: "INTEGER", nullable: false),
                    BacsPleinsApres = table.Column<int>(type: "INTEGER", nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonsSortieStockage", x => x.Id);
                    table.CheckConstraint("CK_BonsSortieStockage_EtatBac", "EtatBac IN ('Vide','Plein')");
                    table.CheckConstraint("CK_BonsSortieStockage_EtatBac_Fields", "(EtatBac = 'Vide' AND BonEntreeStockageId IS NULL AND FactureClientId IS NULL) OR (EtatBac = 'Plein' AND BonEntreeStockageId IS NOT NULL)");
                    table.ForeignKey(
                        name: "FK_BonsSortieStockage_BonsEntreeStockage_BonEntreeStockageId",
                        column: x => x.BonEntreeStockageId,
                        principalTable: "BonsEntreeStockage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_BonsSortieStockage_FacturesClient_FactureClientId",
                        column: x => x.FactureClientId,
                        principalTable: "FacturesClient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_BonsSortieStockage_Tiers_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Tiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaiementsClient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FactureClientId = table.Column<int>(type: "INTEGER", nullable: false),
                    Mode = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Montant = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Reference = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaiementsClient", x => x.Id);
                    table.CheckConstraint("CK_PaiementsClient_Mode", "Mode IN ('Credit','Cheque','Especes','TPE','Virement','Effet')");
                    table.ForeignKey(
                        name: "FK_PaiementsClient_FacturesClient_FactureClientId",
                        column: x => x.FactureClientId,
                        principalTable: "FacturesClient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AvoirsFournisseur",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Numero = table.Column<string>(type: "TEXT", nullable: false),
                    FactureFournisseurId = table.Column<int>(type: "INTEGER", nullable: false),
                    FournisseurId = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Motif = table.Column<string>(type: "TEXT", nullable: true),
                    RetourMarchandise = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvoirsFournisseur", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AvoirsFournisseur_FacturesFournisseur_FactureFournisseurId",
                        column: x => x.FactureFournisseurId,
                        principalTable: "FacturesFournisseur",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AvoirsFournisseur_Tiers_FournisseurId",
                        column: x => x.FournisseurId,
                        principalTable: "Tiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BonsCommandeFournisseur",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Numero = table.Column<string>(type: "TEXT", nullable: false),
                    FournisseurId = table.Column<int>(type: "INTEGER", nullable: false),
                    DevisFournisseurId = table.Column<int>(type: "INTEGER", nullable: true),
                    FactureFournisseurId = table.Column<int>(type: "INTEGER", nullable: true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonsCommandeFournisseur", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BonsCommandeFournisseur_DevisFournisseur_DevisFournisseurId",
                        column: x => x.DevisFournisseurId,
                        principalTable: "DevisFournisseur",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_BonsCommandeFournisseur_FacturesFournisseur_FactureFournisseurId",
                        column: x => x.FactureFournisseurId,
                        principalTable: "FacturesFournisseur",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_BonsCommandeFournisseur_Tiers_FournisseurId",
                        column: x => x.FournisseurId,
                        principalTable: "Tiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaiementsFournisseur",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FactureFournisseurId = table.Column<int>(type: "INTEGER", nullable: false),
                    Mode = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Montant = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Reference = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaiementsFournisseur", x => x.Id);
                    table.CheckConstraint("CK_PaiementsFournisseur_Mode", "Mode IN ('Credit','Cheque','Especes','TPE','Virement','Effet')");
                    table.ForeignKey(
                        name: "FK_PaiementsFournisseur_FacturesFournisseur_FactureFournisseurId",
                        column: x => x.FactureFournisseurId,
                        principalTable: "FacturesFournisseur",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AvoirClientLignes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AvoirClientId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProduitId = table.Column<int>(type: "INTEGER", nullable: true),
                    ServiceId = table.Column<int>(type: "INTEGER", nullable: true),
                    Designation = table.Column<string>(type: "TEXT", nullable: false),
                    Quantite = table.Column<decimal>(type: "TEXT", precision: 18, scale: 3, nullable: false),
                    PrixUnitaireHT = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Remise = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    TauxTVA = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Conditionnement = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvoirClientLignes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AvoirClientLignes_AvoirsClient_AvoirClientId",
                        column: x => x.AvoirClientId,
                        principalTable: "AvoirsClient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AvoirClientLignes_Produits_ProduitId",
                        column: x => x.ProduitId,
                        principalTable: "Produits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AvoirClientLignes_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BonCommandeClientLignes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BonCommandeClientId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProduitId = table.Column<int>(type: "INTEGER", nullable: true),
                    ServiceId = table.Column<int>(type: "INTEGER", nullable: true),
                    Designation = table.Column<string>(type: "TEXT", nullable: false),
                    QuantiteCommandee = table.Column<decimal>(type: "TEXT", precision: 18, scale: 3, nullable: false),
                    PrixUnitaireHT = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Remise = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    TauxTVA = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Conditionnement = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonCommandeClientLignes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BonCommandeClientLignes_BonsCommandeClient_BonCommandeClientId",
                        column: x => x.BonCommandeClientId,
                        principalTable: "BonsCommandeClient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BonCommandeClientLignes_Produits_ProduitId",
                        column: x => x.ProduitId,
                        principalTable: "Produits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BonCommandeClientLignes_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BonsLivraisonClient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Numero = table.Column<string>(type: "TEXT", nullable: false),
                    ClientId = table.Column<int>(type: "INTEGER", nullable: false),
                    DevisClientId = table.Column<int>(type: "INTEGER", nullable: true),
                    BonCommandeClientId = table.Column<int>(type: "INTEGER", nullable: true),
                    FactureClientId = table.Column<int>(type: "INTEGER", nullable: true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonsLivraisonClient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BonsLivraisonClient_BonsCommandeClient_BonCommandeClientId",
                        column: x => x.BonCommandeClientId,
                        principalTable: "BonsCommandeClient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_BonsLivraisonClient_DevisClient_DevisClientId",
                        column: x => x.DevisClientId,
                        principalTable: "DevisClient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_BonsLivraisonClient_FacturesClient_FactureClientId",
                        column: x => x.FactureClientId,
                        principalTable: "FacturesClient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_BonsLivraisonClient_Tiers_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Tiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AvoirFournisseurLignes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AvoirFournisseurId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProduitId = table.Column<int>(type: "INTEGER", nullable: true),
                    ServiceId = table.Column<int>(type: "INTEGER", nullable: true),
                    Designation = table.Column<string>(type: "TEXT", nullable: false),
                    Quantite = table.Column<decimal>(type: "TEXT", precision: 18, scale: 3, nullable: false),
                    PrixUnitaireHT = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Remise = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    TauxTVA = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Conditionnement = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvoirFournisseurLignes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AvoirFournisseurLignes_AvoirsFournisseur_AvoirFournisseurId",
                        column: x => x.AvoirFournisseurId,
                        principalTable: "AvoirsFournisseur",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AvoirFournisseurLignes_Produits_ProduitId",
                        column: x => x.ProduitId,
                        principalTable: "Produits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AvoirFournisseurLignes_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BonCommandeFournisseurLignes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BonCommandeFournisseurId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProduitId = table.Column<int>(type: "INTEGER", nullable: true),
                    ServiceId = table.Column<int>(type: "INTEGER", nullable: true),
                    Designation = table.Column<string>(type: "TEXT", nullable: false),
                    QuantiteCommandee = table.Column<decimal>(type: "TEXT", precision: 18, scale: 3, nullable: false),
                    PrixUnitaireHT = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Remise = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    TauxTVA = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Conditionnement = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonCommandeFournisseurLignes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BonCommandeFournisseurLignes_BonsCommandeFournisseur_BonCommandeFournisseurId",
                        column: x => x.BonCommandeFournisseurId,
                        principalTable: "BonsCommandeFournisseur",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BonCommandeFournisseurLignes_Produits_ProduitId",
                        column: x => x.ProduitId,
                        principalTable: "Produits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BonCommandeFournisseurLignes_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BonsReceptionFournisseur",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Numero = table.Column<string>(type: "TEXT", nullable: false),
                    BonCommandeFournisseurId = table.Column<int>(type: "INTEGER", nullable: false),
                    FournisseurId = table.Column<int>(type: "INTEGER", nullable: false),
                    DevisFournisseurId = table.Column<int>(type: "INTEGER", nullable: true),
                    FactureFournisseurId = table.Column<int>(type: "INTEGER", nullable: true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TotalTtc = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonsReceptionFournisseur", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BonsReceptionFournisseur_BonsCommandeFournisseur_BonCommandeFournisseurId",
                        column: x => x.BonCommandeFournisseurId,
                        principalTable: "BonsCommandeFournisseur",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BonsReceptionFournisseur_DevisFournisseur_DevisFournisseurId",
                        column: x => x.DevisFournisseurId,
                        principalTable: "DevisFournisseur",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_BonsReceptionFournisseur_FacturesFournisseur_FactureFournisseurId",
                        column: x => x.FactureFournisseurId,
                        principalTable: "FacturesFournisseur",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_BonsReceptionFournisseur_Tiers_FournisseurId",
                        column: x => x.FournisseurId,
                        principalTable: "Tiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BonLivraisonClientLignes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BonLivraisonClientId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProduitId = table.Column<int>(type: "INTEGER", nullable: true),
                    ServiceId = table.Column<int>(type: "INTEGER", nullable: true),
                    Designation = table.Column<string>(type: "TEXT", nullable: false),
                    QuantiteCommandee = table.Column<decimal>(type: "TEXT", precision: 18, scale: 3, nullable: false),
                    QuantiteLivree = table.Column<decimal>(type: "TEXT", precision: 18, scale: 3, nullable: false),
                    PrixUnitaireHT = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Remise = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    TauxTVA = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonLivraisonClientLignes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BonLivraisonClientLignes_BonsLivraisonClient_BonLivraisonClientId",
                        column: x => x.BonLivraisonClientId,
                        principalTable: "BonsLivraisonClient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BonLivraisonClientLignes_Produits_ProduitId",
                        column: x => x.ProduitId,
                        principalTable: "Produits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BonLivraisonClientLignes_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FactureClientLignes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FactureClientId = table.Column<int>(type: "INTEGER", nullable: false),
                    BonLivraisonClientId = table.Column<int>(type: "INTEGER", nullable: true),
                    ProduitId = table.Column<int>(type: "INTEGER", nullable: true),
                    ServiceId = table.Column<int>(type: "INTEGER", nullable: true),
                    Designation = table.Column<string>(type: "TEXT", nullable: false),
                    Quantite = table.Column<decimal>(type: "TEXT", precision: 18, scale: 3, nullable: false),
                    PrixUnitaireHT = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Remise = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    TauxTVA = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Conditionnement = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactureClientLignes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FactureClientLignes_BonsLivraisonClient_BonLivraisonClientId",
                        column: x => x.BonLivraisonClientId,
                        principalTable: "BonsLivraisonClient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_FactureClientLignes_FacturesClient_FactureClientId",
                        column: x => x.FactureClientId,
                        principalTable: "FacturesClient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FactureClientLignes_Produits_ProduitId",
                        column: x => x.ProduitId,
                        principalTable: "Produits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FactureClientLignes_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BonReceptionFournisseurLignes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BonReceptionFournisseurId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProduitId = table.Column<int>(type: "INTEGER", nullable: true),
                    ServiceId = table.Column<int>(type: "INTEGER", nullable: true),
                    Designation = table.Column<string>(type: "TEXT", nullable: false),
                    QuantiteRecue = table.Column<decimal>(type: "TEXT", precision: 18, scale: 3, nullable: false),
                    PrixUnitaireHT = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    TauxTVA = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonReceptionFournisseurLignes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BonReceptionFournisseurLignes_BonsReceptionFournisseur_BonReceptionFournisseurId",
                        column: x => x.BonReceptionFournisseurId,
                        principalTable: "BonsReceptionFournisseur",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BonReceptionFournisseurLignes_Produits_ProduitId",
                        column: x => x.ProduitId,
                        principalTable: "Produits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BonReceptionFournisseurLignes_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FactureFournisseurLignes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FactureFournisseurId = table.Column<int>(type: "INTEGER", nullable: false),
                    BonReceptionFournisseurId = table.Column<int>(type: "INTEGER", nullable: true),
                    ProduitId = table.Column<int>(type: "INTEGER", nullable: true),
                    ServiceId = table.Column<int>(type: "INTEGER", nullable: true),
                    Designation = table.Column<string>(type: "TEXT", nullable: false),
                    Quantite = table.Column<decimal>(type: "TEXT", precision: 18, scale: 3, nullable: false),
                    PrixUnitaireHT = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Remise = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    TauxTVA = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Conditionnement = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactureFournisseurLignes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FactureFournisseurLignes_BonsReceptionFournisseur_BonReceptionFournisseurId",
                        column: x => x.BonReceptionFournisseurId,
                        principalTable: "BonsReceptionFournisseur",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_FactureFournisseurLignes_FacturesFournisseur_FactureFournisseurId",
                        column: x => x.FactureFournisseurId,
                        principalTable: "FacturesFournisseur",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FactureFournisseurLignes_Produits_ProduitId",
                        column: x => x.ProduitId,
                        principalTable: "Produits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FactureFournisseurLignes_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AvoirClientLignes_AvoirClientId",
                table: "AvoirClientLignes",
                column: "AvoirClientId");

            migrationBuilder.CreateIndex(
                name: "IX_AvoirClientLignes_ProduitId",
                table: "AvoirClientLignes",
                column: "ProduitId");

            migrationBuilder.CreateIndex(
                name: "IX_AvoirClientLignes_ServiceId",
                table: "AvoirClientLignes",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_AvoirFournisseurLignes_AvoirFournisseurId",
                table: "AvoirFournisseurLignes",
                column: "AvoirFournisseurId");

            migrationBuilder.CreateIndex(
                name: "IX_AvoirFournisseurLignes_ProduitId",
                table: "AvoirFournisseurLignes",
                column: "ProduitId");

            migrationBuilder.CreateIndex(
                name: "IX_AvoirFournisseurLignes_ServiceId",
                table: "AvoirFournisseurLignes",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_AvoirsClient_ClientId",
                table: "AvoirsClient",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_AvoirsClient_FactureClientId",
                table: "AvoirsClient",
                column: "FactureClientId");

            migrationBuilder.CreateIndex(
                name: "IX_AvoirsClient_Numero",
                table: "AvoirsClient",
                column: "Numero",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AvoirsFournisseur_FactureFournisseurId",
                table: "AvoirsFournisseur",
                column: "FactureFournisseurId");

            migrationBuilder.CreateIndex(
                name: "IX_AvoirsFournisseur_FournisseurId",
                table: "AvoirsFournisseur",
                column: "FournisseurId");

            migrationBuilder.CreateIndex(
                name: "IX_AvoirsFournisseur_Numero",
                table: "AvoirsFournisseur",
                column: "Numero",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BonCommandeClientLignes_BonCommandeClientId",
                table: "BonCommandeClientLignes",
                column: "BonCommandeClientId");

            migrationBuilder.CreateIndex(
                name: "IX_BonCommandeClientLignes_ProduitId",
                table: "BonCommandeClientLignes",
                column: "ProduitId");

            migrationBuilder.CreateIndex(
                name: "IX_BonCommandeClientLignes_ServiceId",
                table: "BonCommandeClientLignes",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_BonCommandeFournisseurLignes_BonCommandeFournisseurId",
                table: "BonCommandeFournisseurLignes",
                column: "BonCommandeFournisseurId");

            migrationBuilder.CreateIndex(
                name: "IX_BonCommandeFournisseurLignes_ProduitId",
                table: "BonCommandeFournisseurLignes",
                column: "ProduitId");

            migrationBuilder.CreateIndex(
                name: "IX_BonCommandeFournisseurLignes_ServiceId",
                table: "BonCommandeFournisseurLignes",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_BonLivraisonClientLignes_BonLivraisonClientId",
                table: "BonLivraisonClientLignes",
                column: "BonLivraisonClientId");

            migrationBuilder.CreateIndex(
                name: "IX_BonLivraisonClientLignes_ProduitId",
                table: "BonLivraisonClientLignes",
                column: "ProduitId");

            migrationBuilder.CreateIndex(
                name: "IX_BonLivraisonClientLignes_ServiceId",
                table: "BonLivraisonClientLignes",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_BonReceptionFournisseurLignes_BonReceptionFournisseurId",
                table: "BonReceptionFournisseurLignes",
                column: "BonReceptionFournisseurId");

            migrationBuilder.CreateIndex(
                name: "IX_BonReceptionFournisseurLignes_ProduitId",
                table: "BonReceptionFournisseurLignes",
                column: "ProduitId");

            migrationBuilder.CreateIndex(
                name: "IX_BonReceptionFournisseurLignes_ServiceId",
                table: "BonReceptionFournisseurLignes",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_BonsCommandeClient_ClientId",
                table: "BonsCommandeClient",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_BonsCommandeClient_DevisClientId",
                table: "BonsCommandeClient",
                column: "DevisClientId");

            migrationBuilder.CreateIndex(
                name: "IX_BonsCommandeClient_FactureClientId",
                table: "BonsCommandeClient",
                column: "FactureClientId");

            migrationBuilder.CreateIndex(
                name: "IX_BonsCommandeClient_Numero",
                table: "BonsCommandeClient",
                column: "Numero",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BonsCommandeFournisseur_DevisFournisseurId",
                table: "BonsCommandeFournisseur",
                column: "DevisFournisseurId");

            migrationBuilder.CreateIndex(
                name: "IX_BonsCommandeFournisseur_FactureFournisseurId",
                table: "BonsCommandeFournisseur",
                column: "FactureFournisseurId");

            migrationBuilder.CreateIndex(
                name: "IX_BonsCommandeFournisseur_FournisseurId",
                table: "BonsCommandeFournisseur",
                column: "FournisseurId");

            migrationBuilder.CreateIndex(
                name: "IX_BonsCommandeFournisseur_Numero",
                table: "BonsCommandeFournisseur",
                column: "Numero",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BonsEntreeStockage_ChambreFroideId",
                table: "BonsEntreeStockage",
                column: "ChambreFroideId");

            migrationBuilder.CreateIndex(
                name: "IX_BonsEntreeStockage_ClientId",
                table: "BonsEntreeStockage",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_BonsEntreeStockage_Numero",
                table: "BonsEntreeStockage",
                column: "Numero",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BonsEntreeStockage_NumeroLot",
                table: "BonsEntreeStockage",
                column: "NumeroLot",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BonsEntreeStockage_VarieteId",
                table: "BonsEntreeStockage",
                column: "VarieteId");

            migrationBuilder.CreateIndex(
                name: "IX_BonsLivraisonClient_BonCommandeClientId",
                table: "BonsLivraisonClient",
                column: "BonCommandeClientId");

            migrationBuilder.CreateIndex(
                name: "IX_BonsLivraisonClient_ClientId",
                table: "BonsLivraisonClient",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_BonsLivraisonClient_DevisClientId",
                table: "BonsLivraisonClient",
                column: "DevisClientId");

            migrationBuilder.CreateIndex(
                name: "IX_BonsLivraisonClient_FactureClientId",
                table: "BonsLivraisonClient",
                column: "FactureClientId");

            migrationBuilder.CreateIndex(
                name: "IX_BonsLivraisonClient_Numero",
                table: "BonsLivraisonClient",
                column: "Numero",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BonsReceptionFournisseur_BonCommandeFournisseurId",
                table: "BonsReceptionFournisseur",
                column: "BonCommandeFournisseurId");

            migrationBuilder.CreateIndex(
                name: "IX_BonsReceptionFournisseur_DevisFournisseurId",
                table: "BonsReceptionFournisseur",
                column: "DevisFournisseurId");

            migrationBuilder.CreateIndex(
                name: "IX_BonsReceptionFournisseur_FactureFournisseurId",
                table: "BonsReceptionFournisseur",
                column: "FactureFournisseurId");

            migrationBuilder.CreateIndex(
                name: "IX_BonsReceptionFournisseur_FournisseurId",
                table: "BonsReceptionFournisseur",
                column: "FournisseurId");

            migrationBuilder.CreateIndex(
                name: "IX_BonsReceptionFournisseur_Numero",
                table: "BonsReceptionFournisseur",
                column: "Numero",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BonsSortieStockage_BonEntreeStockageId",
                table: "BonsSortieStockage",
                column: "BonEntreeStockageId");

            migrationBuilder.CreateIndex(
                name: "IX_BonsSortieStockage_ClientId",
                table: "BonsSortieStockage",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_BonsSortieStockage_FactureClientId",
                table: "BonsSortieStockage",
                column: "FactureClientId");

            migrationBuilder.CreateIndex(
                name: "IX_BonsSortieStockage_Numero",
                table: "BonsSortieStockage",
                column: "Numero",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Charges_FournisseurId",
                table: "Charges",
                column: "FournisseurId");

            migrationBuilder.CreateIndex(
                name: "IX_Charges_TypeChargeId",
                table: "Charges",
                column: "TypeChargeId");

            migrationBuilder.CreateIndex(
                name: "IX_DevisClient_ClientId",
                table: "DevisClient",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_DevisClient_Numero",
                table: "DevisClient",
                column: "Numero",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DevisClientConditions_DevisClientId",
                table: "DevisClientConditions",
                column: "DevisClientId");

            migrationBuilder.CreateIndex(
                name: "IX_DevisClientLignes_DevisClientId",
                table: "DevisClientLignes",
                column: "DevisClientId");

            migrationBuilder.CreateIndex(
                name: "IX_DevisClientLignes_ProduitId",
                table: "DevisClientLignes",
                column: "ProduitId");

            migrationBuilder.CreateIndex(
                name: "IX_DevisClientLignes_ServiceId",
                table: "DevisClientLignes",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_DevisFournisseur_FournisseurId",
                table: "DevisFournisseur",
                column: "FournisseurId");

            migrationBuilder.CreateIndex(
                name: "IX_DevisFournisseur_Numero",
                table: "DevisFournisseur",
                column: "Numero",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DevisFournisseurConditions_DevisFournisseurId",
                table: "DevisFournisseurConditions",
                column: "DevisFournisseurId");

            migrationBuilder.CreateIndex(
                name: "IX_DevisFournisseurLignes_DevisFournisseurId",
                table: "DevisFournisseurLignes",
                column: "DevisFournisseurId");

            migrationBuilder.CreateIndex(
                name: "IX_DevisFournisseurLignes_ProduitId",
                table: "DevisFournisseurLignes",
                column: "ProduitId");

            migrationBuilder.CreateIndex(
                name: "IX_DevisFournisseurLignes_ServiceId",
                table: "DevisFournisseurLignes",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_FactureClientLignes_BonLivraisonClientId",
                table: "FactureClientLignes",
                column: "BonLivraisonClientId");

            migrationBuilder.CreateIndex(
                name: "IX_FactureClientLignes_FactureClientId",
                table: "FactureClientLignes",
                column: "FactureClientId");

            migrationBuilder.CreateIndex(
                name: "IX_FactureClientLignes_ProduitId",
                table: "FactureClientLignes",
                column: "ProduitId");

            migrationBuilder.CreateIndex(
                name: "IX_FactureClientLignes_ServiceId",
                table: "FactureClientLignes",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_FactureFournisseurLignes_BonReceptionFournisseurId",
                table: "FactureFournisseurLignes",
                column: "BonReceptionFournisseurId");

            migrationBuilder.CreateIndex(
                name: "IX_FactureFournisseurLignes_FactureFournisseurId",
                table: "FactureFournisseurLignes",
                column: "FactureFournisseurId");

            migrationBuilder.CreateIndex(
                name: "IX_FactureFournisseurLignes_ProduitId",
                table: "FactureFournisseurLignes",
                column: "ProduitId");

            migrationBuilder.CreateIndex(
                name: "IX_FactureFournisseurLignes_ServiceId",
                table: "FactureFournisseurLignes",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_FacturesClient_ClientId",
                table: "FacturesClient",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_FacturesClient_DevisClientId",
                table: "FacturesClient",
                column: "DevisClientId");

            migrationBuilder.CreateIndex(
                name: "IX_FacturesClient_Numero",
                table: "FacturesClient",
                column: "Numero",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FacturesFournisseur_DevisFournisseurId",
                table: "FacturesFournisseur",
                column: "DevisFournisseurId");

            migrationBuilder.CreateIndex(
                name: "IX_FacturesFournisseur_FournisseurId",
                table: "FacturesFournisseur",
                column: "FournisseurId");

            migrationBuilder.CreateIndex(
                name: "IX_FacturesFournisseur_Numero",
                table: "FacturesFournisseur",
                column: "Numero",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MouvementsStock_ProduitId",
                table: "MouvementsStock",
                column: "ProduitId");

            migrationBuilder.CreateIndex(
                name: "IX_PaiementsClient_FactureClientId",
                table: "PaiementsClient",
                column: "FactureClientId");

            migrationBuilder.CreateIndex(
                name: "IX_PaiementsFournisseur_FactureFournisseurId",
                table: "PaiementsFournisseur",
                column: "FactureFournisseurId");

            migrationBuilder.CreateIndex(
                name: "IX_Produits_CategorieId",
                table: "Produits",
                column: "CategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_Produits_Reference",
                table: "Produits",
                column: "Reference",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_Reference",
                table: "Services",
                column: "Reference",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TypesCharge_Nom",
                table: "TypesCharge",
                column: "Nom",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSettings");

            migrationBuilder.DropTable(
                name: "AvoirClientLignes");

            migrationBuilder.DropTable(
                name: "AvoirFournisseurLignes");

            migrationBuilder.DropTable(
                name: "BonCommandeClientLignes");

            migrationBuilder.DropTable(
                name: "BonCommandeFournisseurLignes");

            migrationBuilder.DropTable(
                name: "BonLivraisonClientLignes");

            migrationBuilder.DropTable(
                name: "BonReceptionFournisseurLignes");

            migrationBuilder.DropTable(
                name: "BonsSortieStockage");

            migrationBuilder.DropTable(
                name: "Charges");

            migrationBuilder.DropTable(
                name: "DevisClientConditions");

            migrationBuilder.DropTable(
                name: "DevisClientLignes");

            migrationBuilder.DropTable(
                name: "DevisFournisseurConditions");

            migrationBuilder.DropTable(
                name: "DevisFournisseurLignes");

            migrationBuilder.DropTable(
                name: "FactureClientLignes");

            migrationBuilder.DropTable(
                name: "FactureFournisseurLignes");

            migrationBuilder.DropTable(
                name: "MouvementsStock");

            migrationBuilder.DropTable(
                name: "PaiementsClient");

            migrationBuilder.DropTable(
                name: "PaiementsFournisseur");

            migrationBuilder.DropTable(
                name: "StockBacsSociete");

            migrationBuilder.DropTable(
                name: "AvoirsClient");

            migrationBuilder.DropTable(
                name: "AvoirsFournisseur");

            migrationBuilder.DropTable(
                name: "BonsEntreeStockage");

            migrationBuilder.DropTable(
                name: "TypesCharge");

            migrationBuilder.DropTable(
                name: "BonsLivraisonClient");

            migrationBuilder.DropTable(
                name: "BonsReceptionFournisseur");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Produits");

            migrationBuilder.DropTable(
                name: "ChambresFroides");

            migrationBuilder.DropTable(
                name: "VarietesPomme");

            migrationBuilder.DropTable(
                name: "BonsCommandeClient");

            migrationBuilder.DropTable(
                name: "BonsCommandeFournisseur");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "FacturesClient");

            migrationBuilder.DropTable(
                name: "FacturesFournisseur");

            migrationBuilder.DropTable(
                name: "DevisClient");

            migrationBuilder.DropTable(
                name: "DevisFournisseur");

            migrationBuilder.DropTable(
                name: "Tiers");
        }
    }
}
