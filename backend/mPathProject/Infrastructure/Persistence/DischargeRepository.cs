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
                query = query.Where(d => d.Recommendation.Contains(searchText) || d.Id.ToString() == searchText);

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

        public async Task<(List<Discharge>, int totalItems)> GetAllRecommendationsByUserIdAsync(int count, int page, string searchText, long userId)
        {
            var query = _context.Discharges
                .Include(d => d.Admission) 
                .Where(d => d.Admission.PatientId == userId) 
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchText))
                query = query.Where(d => d.Recommendation.Contains(searchText));

            int totalItems = await query.CountAsync();
            var result = await query.Skip(page * count).Take(count).ToListAsync();
            return (result, totalItems);
        }

        public async Task<bool> DeactivateAsync(long id)
        {

            var discharge = await _context.Discharges.FirstOrDefaultAsync(d => d.Id == id);

            if (discharge == null)
                return false;

            discharge.IsCompleted = false;
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> ActivateAsync(long id)
        {

            var discharge = await _context.Discharges.FirstOrDefaultAsync(d => d.Id == id);

            if (discharge == null)
                return false;

            discharge.IsCompleted = true;
            await _context.SaveChangesAsync();
            return true;

        }
    }
}

