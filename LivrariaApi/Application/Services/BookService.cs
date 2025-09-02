using LivrariaApi.Domain;
using LivrariaApi.Application.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LivrariaApi.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILogger<BookService> _logger;

        public BookService(IBookRepository bookRepository, ILogger<BookService> logger)
        {
            _bookRepository = bookRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            _logger.LogInformation("Buscando todos os livros, ordenados por título.");
            var books = await _bookRepository.GetAllAsync();
            return books.OrderBy(b => b.Title);
        }

        public async Task<Book?> GetBookByIdAsync(Guid id)
        {
            _logger.LogInformation($"Buscando livro pelo ID: {id}");
            return await _bookRepository.GetByIdAsync(id);
        }

        public async Task<(Book? book, string? errorMessage)> CreateBookAsync(
            string title, string author, string genre, decimal price, int stockQuantity)
        {
            _logger.LogInformation($"Tentando criar um novo livro com título: {title}");

            // Basic validation
            if (string.IsNullOrWhiteSpace(title))
                return (null, "Title is required.");
            if (string.IsNullOrWhiteSpace(author))
                return (null, "Author is required.");
            if (price < 0)
                return (null, "Price must be non-negative.");
            if (stockQuantity < 0)
                return (null, "Stock quantity must be non-negative.");

            // Check for duplicate
            var existingBook = await _bookRepository.FindByTitleAndAuthorAsync(title, author);
            if (existingBook != null)
            {
                var errorMessage = $"A book with the same title and author already exists. {title}, {author}";
                _logger.LogWarning(errorMessage);
                return (null, errorMessage);
            }

            var book = new Book
            {
                Id = Guid.NewGuid(),
                Title = title,
                Author = author,
                Genre = genre,
                Price = price,
                StockQuantity = stockQuantity
            };

            await _bookRepository.AddAsync(book);
            await _bookRepository.SaveChangesAsync();

            _logger.LogInformation($"Livro criado com sucesso. ID: {book.Id}");
            return (book, null);
        }

        public async Task<(bool success, string? errorMessage)> UpdateBookAsync(
            Guid id, string title, string author, string genre, decimal price, int stockQuantity)
        {
            _logger.LogInformation($"Tentando atualizar o livro com ID: {id}");
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                var errorMessage = $"Book with ID {id} not found.";
                _logger.LogWarning(errorMessage);
                return (false, errorMessage);
            }

            if (string.IsNullOrWhiteSpace(title))
                return (false, "Title is required.");
            if (string.IsNullOrWhiteSpace(author))
                return (false, "Author is required.");
            if (price < 0)
                return (false, "Price must be non-negative.");
            if (stockQuantity < 0)
                return (false, "Stock quantity must be non-negative.");

            book.Title = title;
            book.Author = author;
            book.Genre = genre;
            book.Price = price;
            book.StockQuantity = stockQuantity;

            await _bookRepository.UpdateAsync(book);
            await _bookRepository.SaveChangesAsync();

            _logger.LogInformation("Livro com ID: {BookId} atualizado com sucesso.", id);
            return (true, null);
        }

        public async Task<bool> DeleteBookAsync(Guid id)
        {
            _logger.LogInformation("Tentando deletar o livro com ID: {BookId}", id);
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                _logger.LogWarning("Falha ao deletar: Livro com ID {BookId} não encontrado.", id);
                return false;
            }

            await _bookRepository.DeleteAsync(book);
            await _bookRepository.SaveChangesAsync();

            _logger.LogInformation("Livro com ID: {BookId} deletado com sucesso.", id);
            return true;
        }
    }
}