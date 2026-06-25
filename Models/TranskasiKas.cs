using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIKAMTA.Models;

[Table("transaksi_kas")]
public class TransaksiKas
{
    public int Id { get; set; }

    [Required]
    public DateTime Tanggal { get; set; }

    [Column("kategori_id")]
    public int KategoriId { get; set; }

    [Required]
    public string Jenis { get; set; } = "";

    public string Keterangan { get; set; } = "";

    [Required]
    public decimal Nominal { get; set; }

    [ForeignKey("KategoriId")]
    public Kategori? Kategori { get; set; }
}