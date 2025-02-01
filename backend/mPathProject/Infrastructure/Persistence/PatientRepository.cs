using mPathProject.Application.Interfaces;
using mPathProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mPathProject.Infrastructure.Persistence
{
    public class PatientRepository : IPatientRepository
    {
        private readonly AppDbContext _context;

        public PatientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(List<Patient>, int totaItems)> GetAllAsync(int count, int page, string searchText)
        {
            var query = _context.Patients.AsQueryable();

            if (!string.IsNullOrEmpty(searchText))
                query = query.Where(p => p.FirstName.Contains(searchText) || p.LastName.Contains(searchText) || p.Id.ToString() == searchText || p.Observations.Contains(searchText));

            int totalItems = await query.CountAsync();
            return (await query.Skip(page * count).Take(count).ToListAsync(), totalItems);
        }

        public async Task<Patient> GetByIdAsync(long id)
        {
            return await _context.Patients.FindAsync(id);
        }

        public async Task AddAsync(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Patient patient)
        {
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(List<long> ids)
        {
            var patients = await _context.Patients.Where(p => ids.Contains(p.Id)).ToListAsync();
            if (patients.Any())
            {
                _context.Patients.RemoveRange(patients);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(long id)
        {
            return await _context.Patients.AnyAsync(p => p.Id == id);
        }
    }
}
