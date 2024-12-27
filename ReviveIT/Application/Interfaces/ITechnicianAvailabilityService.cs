using Application.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITechnicianAvailabilityService
    {
        Task<TechnicianAvailability> CreateAvailabilityAsync(TechnicianAvailabilityDto dto, string technicianId);
        Task<TechnicianAvailabilityDto> GetAvailabilityAsync(string technicianId);
        Task<TechnicianAvailabilityDto> UpdateAvailabilityAsync(string technicianId, TechnicianAvailabilityDto dto); // Updated return type
        Task<bool> DeleteAvailabilityAsync(string technicianId);
        Task<TechnicianAvailabilityDto> GetAvailabilityByTechnicianIdAsync(string technicianId);
    }
}
