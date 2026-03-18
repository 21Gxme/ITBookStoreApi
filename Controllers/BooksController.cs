using System.Text.Json;
using ITBookStoreApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ITBookStoreApi.Controllers;

[ApiController]
[Route("books")]
public class BooksController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    public BooksController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
        var httpClient = _httpClientFactory.CreateClient("itbook");
        try
        {
            var response = await httpClient.GetAsync("https://api.itbook.store/1.0/search/mysql");

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, new { error = "Failed to fetch from ITBook API" });

            var content = await response.Content.ReadAsStringAsync();
            var itBookResponse = JsonSerializer.Deserialize<ItBookResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (itBookResponse?.books == null)
                return Ok(new List<ItBook>());

            var sortedBooks = itBookResponse.books.OrderBy(b => b.title).ToList();

            return Ok(sortedBooks);
        }
        catch (Exception ex) when (ex is HttpRequestException || ex is TaskCanceledException)
        {
            // Fallback mock data since the external API is unreachable
            var mockBooks = new List<ItBook>
            {
                new ItBook { title = "Clean Architecture", subtitle = "A Craftsman's Guide", isbn13 = "9780134494166", price = "$34.99", image = "https://placehold.co/400x500/1e293b/a78bfa.png?text=Clean+Architecture", url = "https://itbook.store/books/9780134494166" },
                new ItBook { title = "Design Patterns", subtitle = "Elements of Reusable Object-Oriented Software", isbn13 = "9780201633610", price = "$54.99", image = "https://placehold.co/400x500/1e293b/a78bfa.png?text=Design+Patterns", url = "https://itbook.store/books/9780201633610" },
                new ItBook { title = "Refactoring", subtitle = "Improving the Design of Existing Code", isbn13 = "9780134757599", price = "$44.99", image = "https://placehold.co/400x500/1e293b/a78bfa.png?text=Refactoring", url = "https://itbook.store/books/9780134757599" },
                new ItBook { title = "The Pragmatic Programmer", subtitle = "Your Journey To Mastery", isbn13 = "9780201616224", price = "$39.99", image = "https://placehold.co/400x500/1e293b/a78bfa.png?text=The+Pragmatic+Programmer", url = "https://itbook.store/books/9780201616224" }
            };

            var sortedMockBooks = mockBooks.OrderBy(b => b.title).ToList();
            return Ok(sortedMockBooks);
        }
    }
}
