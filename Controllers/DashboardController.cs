using Microsoft.AspNetCore.Mvc;
using SIKAMTA.Data;
using SIKAMTA.ViewModels;

namespace SIKAMTA.Controllers;

public class DashboardController : Controller
{
    private readonly AppDbContext _context;

    public DashboardController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        // 1. Pengecekan Session Login
        if (string.IsNullOrEmpty(
            HttpContext.Session.GetString("Username")))
        {
            return RedirectToAction(
                "Login",
                "Account");
        }

        // 2. Ambil Data Profil Mushola dari Database
        // Asumsi: Ada tabel 'Pengaturan' di database. Jika namanya beda, sesuaikan di bawah ini.
        var profil = _context.Pengaturan.FirstOrDefault();

        // 3. Kalkulasi Total Kas
        decimal totalMasuk = _context.TransaksiKas
            .Where(x => x.Jenis == "Masuk")
            .Sum(x => (decimal?)x.Nominal) ?? 0;

        decimal totalKeluar = _context.TransaksiKas
            .Where(x => x.Jenis == "Keluar")
            .Sum(x => (decimal?)x.Nominal) ?? 0;

        // 4. Proses Data Grafik Bulanan
        var bulanLabels = new List<string>();
        var dataMasuk = new List<decimal>();
        var dataKeluar = new List<decimal>();

        for (int bulan = 1; bulan <= 12; bulan++)
        {
            bulanLabels.Add(GetNamaBulan(bulan));

            decimal masuk = _context.TransaksiKas
                .Where(x =>
                    x.Jenis == "Masuk" &&
                    x.Tanggal.Month == bulan)
                .Sum(x => (decimal?)x.Nominal) ?? 0;

            decimal keluar = _context.TransaksiKas
                .Where(x =>
                    x.Jenis == "Keluar" &&
                    x.Tanggal.Month == bulan)
                .Sum(x => (decimal?)x.Nominal) ?? 0;

            dataMasuk.Add(masuk);
            dataKeluar.Add(keluar);
        }

       // 5. Masukkan Semua Data ke ViewModel
        DashboardViewModel model =
            new DashboardViewModel
            {
                NamaMushola = profil?.NamaMushola ?? "Mushola At-Taubah",
                AlamatMushola = profil?.Alamat ?? "Alamat belum diatur",
                
                // Cek apakah ada logo di database. Jika ada, arahkan ke folder upload. 
                // Jika tidak, gunakan logo default.
                LogoPath = string.IsNullOrEmpty(profil?.Logo) 
                           ? "/images/default-logo.png" 
                           : $"/uploads/{profil.Logo}", // Sesuaikan folder penyimpanan logo Anda

                TotalMasuk = totalMasuk,
                TotalKeluar = totalKeluar,
                Saldo = totalMasuk - totalKeluar,
                BulanLabels = bulanLabels,
                DataMasuk = dataMasuk,
                DataKeluar = dataKeluar
            };

        return View(model);
    }

    private string GetNamaBulan(int bulan)
    {
        string[] namaBulan =
        {
            "",
            "Jan",
            "Feb",
            "Mar",
            "Apr",
            "Mei",
            "Jun",
            "Jul",
            "Ags",
            "Sep",
            "Okt",
            "Nov",
            "Des"
        };

        return namaBulan[bulan];
    }
}