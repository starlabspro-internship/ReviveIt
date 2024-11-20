﻿namespace Application.DTO
{
    public class UpdateProfileResultDTO
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}