using System.Collections.Generic;

namespace SIKAMTA.ViewModels;

public class DashboardViewModel
{
    // --- Info Mushola untuk Header Premium ---
    public string NamaMushola { get; set; } = string.Empty;
    public string AlamatMushola { get; set; } = string.Empty;
    public string LogoPath { get; set; } = string.Empty;

    // --- Ringkasan Keuangan ---
    public decimal TotalMasuk { get; set; }
    public decimal TotalKeluar { get; set; }
    public decimal Saldo { get; set; }

    // --- Data untuk Grafik Kas Bulanan ---
    public List<string> BulanLabels { get; set; } = new();
    public List<decimal> DataMasuk { get; set; } = new();
    public List<decimal> DataKeluar { get; set; } = new();
}