using LivrariaApi.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivrariaApi.Application.Interfaces
{
    public interface IBookRepository
    {
        Task<Book?> GetByIdAsync(Guid id);
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book?> FindByTitleAndAuthorAsync(string title, string author);
        Task AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(Book book);
        Task<int> SaveChangesAsync();
    }
}