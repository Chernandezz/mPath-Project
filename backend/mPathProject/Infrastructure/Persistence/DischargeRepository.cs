using mPathProject.Application.Interfaces;
using mPathProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mPathProject.Infrastructure.Persistence
{
    public class DischargeRepository : IDischargeRepository
    {
        private readonly AppDbContext _context;

        public DischargeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(List<Discharge>, int totaItems)> GetAllAsync(int count, int page, string searchText)
        {
            var query = _context.Discharges.AsQueryable();

            if (!string.IsNullOrEmpty(searchText))
                query = query.Where(d => d.Treatment.Contains(searchText));

            int totalItems = await query.CountAsync();
            return (await query.Skip(page * count).Take(count).ToListAsync(), totalItems);
        }


        public async Task<Discharge> GetByIdAsync(long id)
        {
            return await _context.Discharges.FindAsync(id);
        }

        public async Task AddAsync(Discharge discharge)
        {
            _context.Discharges.Add(discharge);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Discharge discharge)
        {
            _context.Discharges.Update(discharge);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(long id)
        {
            return await _context.Discharges.AnyAsync(d => d.Id == id);
        }
    }
}
