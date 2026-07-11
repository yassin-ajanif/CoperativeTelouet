using CoperativeTelouet.Domain.Common;

namespace CoperativeTelouet.Domain.Entities;

public class AppSettings : BaseEntity
{
    public string? SocieteNom { get; set; }
    public string? SocieteAdresse { get; set; }
    public string? SocieteICE { get; set; }
    public string? SocieteMentionsLegales { get; set; }
    public string? SocieteLogoPath { get; set; }
    public string? TauxTVAJson { get; set; }
    public bool BlocageSiStockInsuffisant { get; set; }
    public int DevisValiditeJoursDefaut { get; set; }
    public string? Devise { get; set; }
    public string? UiLanguage { get; set; }
    public bool BackupEnabled { get; set; }
    public int BackupIntervalHours { get; set; }
    public string? BackupIntervalUnit { get; set; }
    public int BackupRetentionDays { get; set; }
    public string? BackupDirectory { get; set; }
    public DateTime? LastBackupDate { get; set; }
    public decimal PrixStockageParBacParJour { get; set; }
}
