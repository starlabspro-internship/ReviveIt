namespace Domain.Entities
{
    public class SelectedJobApplicant
    {
        public int SelectedApplicantID { get; set; }
        public DateTime SelectedDate { get; set; }

        public int JobID { get; set; }
        public Jobs Job { get; set; }

        public int ApplicationID { get; set; }
        public JobApplication JobApplication { get; set; }

        public string SelectedApplicantUserId { get; set; }
        public Users SelectedApplicantUser { get; set; }

        public string SelectedByUserId { get; set; }
        public Users SelectedByUser { get; set; }
    } 
}