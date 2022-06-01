using System;
using System.Collections.Generic;
using System.Text;

namespace BPRMobileApp.Models.Responses
{
    public class QuestionsDTOResponse
    {
        public string Question_id { get; set; }

        public string Question { get; set; }

        public List<QuestionOptionDTOResponse> Option { get; set; }
    }
}
