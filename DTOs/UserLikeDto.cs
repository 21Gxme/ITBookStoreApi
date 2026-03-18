namespace ITBookStoreApi.DTOs;

public class UserLikeRequest
{
    public int user_id { get; set; }
    public string book_id { get; set; } = string.Empty;
}
