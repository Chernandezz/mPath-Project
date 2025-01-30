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

        public async Task<(List<Doctor> doctors, int totalItems)> GetAllAsync(int count, int page, string searchText)
        {
            var query = _context.Doctors.AsQueryable();

            if (!string.IsNullOrEmpty(searchText))
                query = query.Where(d => d.FirstName.Contains(searchText));

            int totalItems = await query.CountAsync();
            var doctors = await query.Skip(page * count).Take(count).ToListAsync();

            return (doctors, totalItems);
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

       

        public async Task<bool> DeactivateAsync(long id)
        {

            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id);

            if (doctor == null)
                return false; 

            doctor.Active = false;
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> ExistsAsync(long id)
        {
            return await _context.Doctors.AnyAsync(d => d.Id == id);
        }
    }
}
