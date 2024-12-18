using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class GetUserReviewResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public GetUserReviewDto Review { get; set; }
    }
}