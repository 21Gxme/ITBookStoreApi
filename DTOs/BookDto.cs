namespace ITBookStoreApi.DTOs;

public class ItBookResponse
{
    public string error { get; set; } = string.Empty;
    public string total { get; set; } = string.Empty;
    public string page { get; set; } = string.Empty;
    public List<ItBook> books { get; set; } = new();
}

public class ItBook
{
    public string title { get; set; } = string.Empty;
    public string subtitle { get; set; } = string.Empty;
    public string isbn13 { get; set; } = string.Empty;
    public string price { get; set; } = string.Empty;
    public string image { get; set; } = string.Empty;
    public string url { get; set; } = string.Empty;
}
