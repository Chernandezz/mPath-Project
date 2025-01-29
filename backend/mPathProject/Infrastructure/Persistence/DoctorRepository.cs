using mPathProject.Application.Interfaces;
using mPathProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mPathProject.Infrastructure.Persistence
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppDbContext _context;

        public DoctorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Doctor>> GetAllAsync(int count, int page, string searchText)
        {
            var query = _context.Doctors.AsQueryable();

            if (!string.IsNullOrEmpty(searchText))
                query = query.Where(d => d.FirstName.Contains(searchText));

            return await query.Skip(page * count).Take(count).ToListAsync();
        }

        public async Task<Doctor> GetByIdAsync(long id)
        {
            return await _context.Doctors.FindAsync(id);
        }

        public async Task AddAsync(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(List<long> ids)
        {
            var doctors = await _context.Doctors.Where(d => ids.Contains(d.Id)).ToListAsync();
            if (doctors.Any())
            {
                _context.Doctors.RemoveRange(doctors);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(long id)
        {
            return await _context.Doctors.AnyAsync(d => d.Id == id);
        }
    }
}
