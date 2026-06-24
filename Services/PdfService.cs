using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SIKAMTA.ViewModels;

namespace SIKAMTA.Services;

public class PdfService
{
    public byte[] GenerateLaporan(LaporanViewModel model)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        string logoPath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot",
            "images",
            "logo.png");

        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(20);

                // =========================================
                // HEADER
                // =========================================
                page.Header().Column(header =>
                {
                    header.Item().Row(row =>
                    {
                        // Logo
                        if (File.Exists(logoPath))
                        {
                            row.ConstantItem(70)
                                .Height(70)
                                .Image(logoPath);
                        }

                        row.ConstantItem(10);

                        // Informasi Mushola
                        row.RelativeItem()
                            .Column(col =>
                            {
                                col.Item()
                                    .Text("MUSHOLA AT-TAUBAH")
                                    .FontSize(16)
                                    .Bold();

                                col.Item()
                                    .Text("Perum Bukit Panorama Indah")
                                    .FontSize(10);

                                col.Item()
                                    .Text("Ciseureuh, Purwakarta")
                                    .FontSize(10);

                                col.Item()
                                    .Text("SISTEM INFORMASI KAS MUSHOLA")
                                    .FontSize(9)
                                    .Italic();
                            });
                    });

                    header.Item().PaddingTop(5);

                    header.Item().LineHorizontal(2);

                    header.Item().PaddingTop(2);

                    header.Item().LineHorizontal(0.5f);
                });

                // =========================================
                // CONTENT
                // =========================================
                page.Content().Column(col =>
                {
                    col.Spacing(8);

                    // Judul
                    col.Item()
                        .PaddingTop(5)
                        .AlignCenter()
                        .Text($"LAPORAN KAS BULAN {GetNamaBulan(model.Bulan).ToUpper()} {model.Tahun}")
                        .FontSize(12)
                        .Bold();

                    col.Item()
                        .AlignCenter()
                        .Text($"Tanggal Cetak : {DateTime.Now:dd MMMM yyyy}")
                        .FontSize(8);

                    // Ringkasan
                    col.Item()
                        .PaddingTop(5)
                        .Border(1)
                        .Padding(8)
                        .Column(summary =>
                        {
                            summary.Spacing(3);

                            summary.Item()
                                .Text($"Total Pemasukan : Rp {model.TotalMasuk:N0}")
                                .FontSize(9);

                            summary.Item()
                                .Text($"Total Pengeluaran : Rp {model.TotalKeluar:N0}")
                                .FontSize(9);

                            summary.Item()
                                .Text($"Saldo Kas : Rp {model.Saldo:N0}")
                                .Bold()
                                .FontSize(10);
                        });

                    // Detail
                    col.Item()
                        .PaddingTop(8)
                        .Text("DETAIL TRANSAKSI")
                        .Bold()
                        .FontSize(10);

                    col.Item()
                        .DefaultTextStyle(x => x.FontSize(8))
                        .Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(25);
                                columns.ConstantColumn(55);
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(3);
                                columns.ConstantColumn(70);
                                columns.ConstantColumn(70);
                            });

                            // Header
                            table.Header(header =>
                            {
                                header.Cell().Border(1).Padding(3).Text("No").Bold();
                                header.Cell().Border(1).Padding(3).Text("Tanggal").Bold();
                                header.Cell().Border(1).Padding(3).Text("Kategori").Bold();
                                header.Cell().Border(1).Padding(3).Text("Keterangan").Bold();
                                header.Cell().Border(1).Padding(3).AlignRight().Text("Masuk").Bold();
                                header.Cell().Border(1).Padding(3).AlignRight().Text("Keluar").Bold();
                            });

                            int no = 1;

                            foreach (var item in model.Data)
                            {
                                table.Cell().Border(1).Padding(3)
                                    .Text(no++.ToString());

                                table.Cell().Border(1).Padding(3)
                                    .Text(item.Tanggal.ToString("dd/MM/yyyy"));

                                table.Cell().Border(1).Padding(3)
                                    .Text(item.Kategori?.NamaKategori ?? "");

                                table.Cell().Border(1).Padding(3)
                                    .Text(item.Keterangan ?? "");

                                table.Cell().Border(1).Padding(3)
                                    .AlignRight()
                                    .Text(item.Jenis == "Masuk"
                                        ? item.Nominal.ToString("N0")
                                        : "");

                                table.Cell().Border(1).Padding(3)
                                    .AlignRight()
                                    .Text(item.Jenis == "Keluar"
                                        ? item.Nominal.ToString("N0")
                                        : "");
                            }

                            // TOTAL
                            table.Cell()
                                .ColumnSpan(4)
                                .Border(1)
                                .Padding(4)
                                .AlignRight()
                                .Text("TOTAL")
                                .Bold();

                            table.Cell()
                                .Border(1)
                                .Padding(4)
                                .AlignRight()
                                .Text(model.TotalMasuk.ToString("N0"))
                                .Bold();

                            table.Cell()
                                .Border(1)
                                .Padding(4)
                                .AlignRight()
                                .Text(model.TotalKeluar.ToString("N0"))
                                .Bold();
                        });

                    // Tanda Tangan
                    col.Item()
                        .PaddingTop(20);

                    col.Item()
                        .AlignRight()
                        .Text($"Purwakarta, {DateTime.Now:dd MMMM yyyy}")
                        .FontSize(9);

                    col.Item()
                        .PaddingTop(15);

                    col.Item()
                        .Row(row =>
                        {
                            row.RelativeItem()
                                .AlignCenter()
                                .Text("Bendahara")
                                .FontSize(9);

                            row.RelativeItem()
                                .AlignCenter()
                                .Text("Ketua DKM")
                                .FontSize(9);
                        });

                    col.Item()
                        .PaddingTop(40);

                    col.Item()
                        .Row(row =>
                        {
                            row.RelativeItem()
                                .AlignCenter()
                                .Text("(____________________)")
                                .FontSize(9);

                            row.RelativeItem()
                                .AlignCenter()
                                .Text("(____________________)")
                                .FontSize(9);
                        });
                });

                // =========================================
                // FOOTER
                // =========================================
                page.Footer()
                    .Row(row =>
                    {
                        row.RelativeItem()
                            .Text(text => text.Span("SIKAMTA - Sistem Informasi Kas Mushola At-Taubah").FontSize(8));

                        row.ConstantItem(100)
                            .AlignRight()
                            .Text(text =>
                            {
                                text.Span("Halaman ").FontSize(8);
                                text.CurrentPageNumber().FontSize(8);
                                text.Span(" / ").FontSize(8);
                                text.TotalPages().FontSize(8);
                            });
                    });
            });
        })
        .GeneratePdf();
    }

    private string GetNamaBulan(int bulan)
    {
        string[] bulanIndonesia =
        {
            "",
            "Januari",
            "Februari",
            "Maret",
            "April",
            "Mei",
            "Juni",
            "Juli",
            "Agustus",
            "September",
            "Oktober",
            "November",
            "Desember"
        };

        return bulanIndonesia[bulan];
    }
}