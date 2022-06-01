using System;
using System.Collections.Generic;
using System.Text;

namespace BPRMobileApp.Models.Requests
{
    public class QuestionAnswersDTO
    {
        public int Question_id { get; set; }

        public int Selected_option_id { get; set; }

        public QuestionAnswersDTO(int question_id, int selected_option_id)
        {
            Question_id = question_id;
            Selected_option_id = selected_option_id;
        }
    }
}
