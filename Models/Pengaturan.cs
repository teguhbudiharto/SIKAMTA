namespace SIKAMTA.Models;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
public class Pengaturan
{
    public int Id { get; set; }

    public string NamaMushola { get; set; } = "";

    public string Alamat { get; set; } = "";

    public string KetuaDKM { get; set; } = "";

    public string Bendahara { get; set; } = "";

    public string Telepon { get; set; } = "";

    public string Email { get; set; } = "";

    public string Logo { get; set; } = "";

[NotMapped]
public IFormFile? FileLogo { get; set; }
}