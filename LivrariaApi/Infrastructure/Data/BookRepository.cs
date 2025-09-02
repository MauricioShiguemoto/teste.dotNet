using LivrariaApi.Application.Interfaces;
using LivrariaApi.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LivrariaApi.Infrastructure.Data
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Book?> GetByIdAsync(Guid id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book?> FindByTitleAndAuthorAsync(string title, string author)
        {
            return await _context.Books
                .FirstOrDefaultAsync(b => b.Title == title && b.Author == author);
        }

        public async Task AddAsync(Book book)
        {
            await _context.Books.AddAsync(book);
        }

        public async Task UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Book book)
        {
            _context.Books.Remove(book);
            await Task.CompletedTask;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}