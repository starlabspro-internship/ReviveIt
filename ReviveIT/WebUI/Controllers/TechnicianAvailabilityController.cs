using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace WebUI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TechnicianAvailabilityController : ControllerBase
    {
        private readonly ITechnicianAvailabilityService _availabilityService;

        public TechnicianAvailabilityController(ITechnicianAvailabilityService availabilityService)
        {
            _availabilityService = availabilityService;
        }

        [Authorize(Roles = "Technician,Company")]
        [HttpGet("AvailableTechnician")]
        public async Task<IActionResult> GetAvailability()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized(new { success = false, message = "User not authenticated." });
            }

            var availability = await _availabilityService.GetAvailabilityAsync(userIdClaim);

            if (availability == null)
            {
                return NotFound(new { success = false, message = "No availability found for the authenticated technician." });
            }

            return Ok(new GetAllAvailableResultDto
            {
                Success = true,
                Message = "Availability data retrieved successfully.",
                Available = new List<TechnicianAvailabilityDto> { availability }
            });
        }

        [Authorize(Roles = "Technician,Company")]
        [HttpPost("PostAvailable")]
        public async Task<IActionResult> CreateAvailability([FromBody] TechnicianAvailabilityDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var technicianId = User.FindFirst("UserId")?.Value;
            if (technicianId == null)
            {
                return Unauthorized();
            }

            try
            {
                var availability = await _availabilityService.CreateAvailabilityAsync(dto, technicianId);
                return CreatedAtAction(nameof(GetAvailability), new { technicianId = availability.TechnicianId }, availability);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Technician,Company")]
        [HttpPut("update-available")]
        public async Task<IActionResult> UpdateAvailability([FromBody] TechnicianAvailabilityDto dto)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized(new { success = false, message = "User not authenticated." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { success = false, message = "Invalid input data.", errors = ModelState });
            }

            var updatedAvailability = await _availabilityService.UpdateAvailabilityAsync(userIdClaim, dto);

            if (updatedAvailability == null)
            {
                return NotFound(new { success = false, message = "Availability not found or unable to update." });
            }

            return Ok(new { success = true, message = "Availability updated successfully.", data = updatedAvailability });
        }

        [Authorize(Roles = "Technician,Company")]
        [HttpDelete("delete-availableTechnician")]
        public async Task<IActionResult> DeleteAvailability()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized(new { success = false, message = "User not authenticated." });
            }

            var isDeleted = await _availabilityService.DeleteAvailabilityAsync(userIdClaim);

            if (!isDeleted)
            {
                return NotFound(new { success = false, message = "No availability found for the authenticated technician." });
            }

            return Ok(new { success = true, message = "Availability deleted successfully." });
        }

        [HttpGet("GetAvailabilityByTechnicianId/{technicianId}")]
        public async Task<IActionResult> GetAvailabilityByTechnicianId(string technicianId)
        {
            if (string.IsNullOrEmpty(technicianId))
            {
                return BadRequest(new { success = false, message = "Technician ID is required." });
            }

            var availability = await _availabilityService.GetAvailabilityByTechnicianIdAsync(technicianId);


            if (availability == null)
            {
                return NotFound(new { success = false, message = "No availability found for the specified technician." });
            }

            return Ok(new
            {
                success = true,
                message = "Availability data retrieved successfully.",
                availability
            });
        }
    }
}
