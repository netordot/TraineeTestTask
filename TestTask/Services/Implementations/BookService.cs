using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;
        private readonly DateTime CAROLUS_REX_RELEASE_DATE = new DateTime(2012, 5, 2);

        public BookService(ApplicationDbContext context)
        {
            _context = context; 
        }
        /// <summary>
        /// Method for getting the book with the biggest cost of circulation
        /// </summary>
        /// <returns>Book</returns>
        public async Task<Book> GetBook()
        {
            var book = _context.Books
                .Select(book => new
                {
                    Book = book,
                    TotalPrice = book.Price * book.QuantityPublished
                })
                .OrderByDescending(book => book.TotalPrice)
                .FirstOrDefault();

            return book.Book;
        }
        /// <summary>
        /// Method for getting the books that have "Red" in their name and were published after "Carolus rex" release
        /// </summary>
        /// <returns>List<Book></returns>
        public async Task<List<Book>> GetBooks()
        {
            var books = await _context.Books
                .Where(b => b.Title.Contains("Red") && b.PublishDate > CAROLUS_REX_RELEASE_DATE)
                .ToListAsync();

            return books;       
        }
    }
}
