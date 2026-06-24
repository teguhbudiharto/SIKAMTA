using SIKAMTA.Models;

namespace SIKAMTA.ViewModels;

public class LaporanViewModel
{
    public int Bulan { get; set; }

    public int Tahun { get; set; }

    public decimal TotalMasuk { get; set; }

    public decimal TotalKeluar { get; set; }

    public decimal Saldo { get; set; }

    public List<TransaksiKas> Data { get; set; }
        = new();
}