using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int? SurveyResponse { get; set; }
        public string? FeatureSuggestion { get; set; }
        public DateTime Date { get; set; }
        public string? UserId { get; set; }
        public Users User { get; set; }
    }
}