namespace ITBookStoreApi.Models;

public class LikedBook
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public User? User { get; set; }

    public string BookId { get; set; } = string.Empty;
}
