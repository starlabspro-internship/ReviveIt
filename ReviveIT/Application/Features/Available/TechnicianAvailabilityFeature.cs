using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Available
{
    public class TechnicianAvailabilityFeature : ITechnicianAvailabilityService
    {
        private readonly IApplicationDbContext _context;

        public TechnicianAvailabilityFeature(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TechnicianAvailabilityDto> GetAvailabilityAsync(string technicianId)
        {
            var availability = await _context.TechnicianAvailabilities
                .FirstOrDefaultAsync(a => a.TechnicianId == technicianId);

            if (availability == null)
            {
                return null;
            }

            return new TechnicianAvailabilityDto
            {
                DaysAvailable = availability.DaysAvailable,
                MonthsUnavailable = availability.MonthsUnavailable,
                SpecificUnavailableDates = availability.SpecificUnavailableDates
            };
        }

        public async Task<TechnicianAvailability> CreateAvailabilityAsync(TechnicianAvailabilityDto dto, string technicianId)
        {
            if (string.IsNullOrEmpty(technicianId))
            {
                throw new ArgumentNullException(nameof(technicianId), "Technician ID cannot be null or empty.");
            }

            var availability = new TechnicianAvailability
            {
                TechnicianId = technicianId,
                DaysAvailable = dto.DaysAvailable,
                MonthsUnavailable = dto.MonthsUnavailable,
                SpecificUnavailableDates = dto.SpecificUnavailableDates
            };

            await _context.TechnicianAvailabilities.AddAsync(availability);
            await _context.SaveChangesAsync();

            return availability;
        }

        public async Task<TechnicianAvailabilityDto> UpdateAvailabilityAsync(string technicianId, TechnicianAvailabilityDto dto)
        {
            var availability = await _context.TechnicianAvailabilities
                .FirstOrDefaultAsync(a => a.TechnicianId == technicianId);

            if (availability == null)
            {
                availability = new TechnicianAvailability
                {
                    TechnicianId = technicianId,
                    CreatedAt = DateTime.UtcNow
                };
                _context.TechnicianAvailabilities.Add(availability);
            }

            availability.DaysAvailable = dto.DaysAvailable;
            availability.MonthsUnavailable = dto.MonthsUnavailable;
            availability.SpecificUnavailableDates = dto.SpecificUnavailableDates;
            availability.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new TechnicianAvailabilityDto
            {
                DaysAvailable = availability.DaysAvailable,
                MonthsUnavailable = availability.MonthsUnavailable,
                SpecificUnavailableDates = availability.SpecificUnavailableDates
            };
        }

        public async Task<bool> DeleteAvailabilityAsync(string technicianId)
        {
            var availability = await _context.TechnicianAvailabilities
                .FirstOrDefaultAsync(a => a.TechnicianId == technicianId);

            if (availability == null)
            {
                return false;
            }

            _context.TechnicianAvailabilities.Remove(availability);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<TechnicianAvailabilityDto> GetAvailabilityByTechnicianIdAsync(string technicianId)
        {
            var availability = await _context.TechnicianAvailabilities
                .FirstOrDefaultAsync(a => a.TechnicianId == technicianId);

            if (availability == null)
            {
                return new TechnicianAvailabilityDto();
            }

            return new TechnicianAvailabilityDto
            {
                DaysAvailable = availability.DaysAvailable,
                MonthsUnavailable = availability.MonthsUnavailable,
                SpecificUnavailableDates = availability.SpecificUnavailableDates
            };
        }
    }
}