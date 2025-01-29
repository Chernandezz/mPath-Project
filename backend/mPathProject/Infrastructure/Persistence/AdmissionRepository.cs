﻿using mPathProject.Application.Interfaces;
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

        public async Task<List<Admission>> GetAllAsync(int count, int page, string searchText)
        {
            var query = _context.Admissions.AsQueryable();

            if (!string.IsNullOrEmpty(searchText))
                query = query.Where(a => a.Diagnosis.Contains(searchText));

            return await query.Skip(page * count).Take(count).ToListAsync();
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
