using mPathProject.Application.Interfaces;
using mPathProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mPathProject.Infrastructure.Persistence
{
    public class AdmissionRepository : IAdmissionRepository
    {
        private readonly AppDbContext _context;

        public AdmissionRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<(List<Admission>, int totaItems)> GetAllAsync(int count, int page, string searchText)
        {
            var query = _context.Admissions.AsQueryable();

            if (!string.IsNullOrEmpty(searchText))
                query = query.Where(a => a.Diagnosis.Contains(searchText) || a.Observation.Contains(searchText) || a.Id.ToString() == searchText);

            int totalItems = await query.CountAsync();
            return (await query.Skip(page * count).Take(count).ToListAsync(), totalItems);
        }
        public async Task<(List<Admission>, int totalItems)> GetAllByPatientIdAsync(int count, int page, string searchText, long patientId)
        {
            var query = _context.Admissions
                .Where(a => a.PatientId == patientId)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchText))
                query = query.Where(a => a.Diagnosis.Contains(searchText) || a.Observation.Contains(searchText));

            int totalItems = await query.CountAsync();
            var result = await query.Skip(page * count).Take(count).ToListAsync();
            return (result, totalItems);
        }
        public async Task<Admission> GetByIdAsync(long id)
        {
            return await _context.Admissions.FindAsync(id);
        }

        public async Task AddAsync(Admission admission)
        {
            _context.Admissions.Add(admission);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Admission admission)
        {
            _context.Admissions.Update(admission);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(long id)
        {
            return await _context.Admissions.AnyAsync(a => a.Id == id);
        }
    }
}
