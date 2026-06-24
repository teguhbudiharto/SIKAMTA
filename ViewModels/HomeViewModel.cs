namespace SIKAMTA.ViewModels;

public class HomeViewModel
{
    public string NamaMushola { get; set; } = string.Empty;
    public string Alamat { get; set; } = string.Empty;
    public string LogoPath { get; set; } = string.Empty;

    public decimal TotalMasuk { get; set; }
    public decimal TotalKeluar { get; set; }
    public decimal Saldo { get; set; }
}