﻿using Application.DTO;
using Application.Features.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PortfolioController : ControllerBase
    {
        private readonly AddPhotoToPortfolioFeature _addPhotoFeature;
        private readonly DeletePhotoFromPortfolioFeature _deletePhotoFeature;
        private readonly GetPortfolioPhotosFeature _getPortfolioFeature;

        public PortfolioController(AddPhotoToPortfolioFeature addPhotoFeature, DeletePhotoFromPortfolioFeature deletePhotoFeature, GetPortfolioPhotosFeature getPortfolioFeature)
        {
            _addPhotoFeature = addPhotoFeature;
            _deletePhotoFeature = deletePhotoFeature;
            _getPortfolioFeature = getPortfolioFeature;
        }

        [Authorize(Roles = "Technician,Company")]
        [HttpPost("upload-portfolio")]
        public async Task<IActionResult> UploadPortfolioPhoto([FromForm] AddPortfolioPhotoDto dto)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized(new { Message = "User not authenticated." });

            var result = await _addPhotoFeature.AddPhotoAsync(dto, userIdClaim);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [Authorize(Roles = "Technician,Company")]
        [HttpDelete("delete-portfolio/{portfolioDocumentId}")]
        public async Task<IActionResult> DeletePortfolioPhoto(int portfolioDocumentId)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized(new { Message = "User not authenticated." });

            var result = await _deletePhotoFeature.DeletePhotoAsync(portfolioDocumentId, userIdClaim);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [Authorize(Roles = "Technician,Company")]
        [HttpGet("my-portfolio")]
        public async Task<IActionResult> GetMyPortfolio()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized(new { Message = "User not authenticated." });

            var result = await _getPortfolioFeature.GetPortfolioPhotosAsync(userIdClaim);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}