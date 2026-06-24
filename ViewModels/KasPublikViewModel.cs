using SIKAMTA.Models;

namespace SIKAMTA.ViewModels;

public class KasPublikViewModel
{
    public decimal TotalMasuk { get; set; }

    public decimal TotalKeluar { get; set; }

    public decimal Saldo { get; set; }

    public List<TransaksiKas> TransaksiTerakhir { get; set; }
        = new();
}