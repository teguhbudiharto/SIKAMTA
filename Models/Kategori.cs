using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIKAMTA.Models;

[Table("kategori")]
public class Kategori
{
    public int Id { get; set; }

    [Column("nama_kategori")]
    [Required]
    public string NamaKategori { get; set; } = "";

    [Required]
    public string Jenis { get; set; } = "";
}