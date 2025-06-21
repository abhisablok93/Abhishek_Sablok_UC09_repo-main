using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSTraining.Models
{
    public class TrainingDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CourseId { get; set; }

        public string CourseName { get; set; }
        public string Category { get; set; }
        public string Skill { get; set; }
        public string Level { get; set; }

        public string Duration { get; set; }
    }
}