using System.ComponentModel.DataAnnotations;

namespace ITBookStoreApi.Models;

public class User
{
    public int Id { get; set; }

    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    [Required]
    public string Fullname { get; set; } = string.Empty;

    public ICollection<LikedBook> LikedBooks { get; set; } = new List<LikedBook>();
}
