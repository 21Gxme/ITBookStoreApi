using ITBookStoreApi.Data;
using ITBookStoreApi.DTOs;
using ITBookStoreApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITBookStoreApi.Controllers;

[ApiController]
[Route("user")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly AppDbContext _context;

    public UserController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("like")]
    public async Task<IActionResult> LikeBook([FromBody] UserLikeRequest request)
    {
        var user = await _context.Users.FindAsync(request.user_id);
        if (user == null)
            return BadRequest(new { error = "User not found" });

        var existingLike = await _context.LikedBooks.FirstOrDefaultAsync(l => l.UserId == request.user_id && l.BookId == request.book_id);
        if (existingLike != null)
            return BadRequest(new { error = "Book already liked by this user" });

        var likedBook = new LikedBook
        {
            UserId = request.user_id,
            BookId = request.book_id
        };

        _context.LikedBooks.Add(likedBook);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Book liked successfully" });
    }
}
