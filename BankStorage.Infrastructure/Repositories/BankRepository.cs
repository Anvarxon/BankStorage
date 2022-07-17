using BankStorage.Domain.Models;
using BankStorage.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankStorage.Infrastructure.Repositories
{
    public class BankRepository : IRepository<Bank, int>
    {
        private readonly AppDbContext _context;
        public BankRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Bank> Add(Bank entity)
        {
            _context.Banks.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Bank> Delete(int id)
        {
            var entity = await _context.Banks.FindAsync(id);
            if (entity == null)
            {
                return null;
            }
            _context.Banks.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Bank>> GetAll()
        {
            return await _context.Banks.ToListAsync();
        }

        public async Task<Bank> GetById(int id)
        {
            return await _context.Banks.FindAsync(id);
        }

        public async Task<Bank> Update(Bank entity)
        {
            _context.Banks.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

    }
}
