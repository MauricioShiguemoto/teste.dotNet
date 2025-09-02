using LivrariaApi.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivrariaApi.Application.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(Guid id);
        Task<(Book? book, string? errorMessage)> CreateBookAsync(string title, string author, string genre, decimal price, int stockQuantity);
        Task<(bool success, string? errorMessage)> UpdateBookAsync(Guid id, string title, string author, string genre, decimal price, int stockQuantity);
        Task<bool> DeleteBookAsync(Guid id);
    }
}