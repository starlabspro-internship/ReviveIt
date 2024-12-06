namespace Application.DTO
{
    public class SelectApplicantResultDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int? SelectedApplicantID { get; set; }
        public string SelectedApplicantUserId { get; set; } 
    }
}
