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
    public class BinCodeRepository : IRepository<Bin_Code, int>
    {
        private readonly AppDbContext _context;
        public BinCodeRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Bin_Code> GetById(int id)
        {
            return await _context.Bin_Codes.FindAsync(id);
        }

        public async Task<IEnumerable<Bin_Code>> GetAll()
        {
            return await _context.Bin_Codes.ToListAsync();
        }

        public async Task<Bin_Code> Add(Bin_Code entity)
        {
            _context.Bin_Codes.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Bin_Code> Update(Bin_Code entity)
        {
            _context.Bin_Codes.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Bin_Code> Delete(int id)
        {
            var entity = await _context.Bin_Codes.FindAsync(id);
            if (entity == null)
            {
                return entity;
            }

            _context.Bin_Codes.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
