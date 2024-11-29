using System.Linq;
using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

public class AuthorService : IAuthorService
{
    private readonly DateTime DATE_2015 = new DateTime(2015, 12, 31);
    private readonly ApplicationDbContext _context;

    public AuthorService(ApplicationDbContext context)
    {
        _context = context;
    }
    /// <summary>
    /// Method for getting the author with the longest book title
    /// </summary>
    /// <returns></returns>
    public async Task<Author> GetAuthor()
    {
        var authorWithLongestTitle = await _context.Authors
            .Select(a => new
            {      
                Author = a,
                LongestTitleLength = a.Books.Max(b => b.Title.Length)
            })
            .OrderByDescending(a => a.LongestTitleLength)
            .ThenBy(a => a.Author.Id)
            .FirstOrDefaultAsync();

        return authorWithLongestTitle?.Author;
    }

    /// <summary>
    /// Method for getiing the authors who wrote even number of books after 2015
    /// /// </summary>
    /// <returns></returns>
    public async Task<List<Author>> GetAuthors()
    {
        var authors = await _context.Authors
            .Where(a => a.Books.Count(b => b.PublishDate > DATE_2015) % 2 == 0)
            .ToListAsync();

        return authors;
    }

}
