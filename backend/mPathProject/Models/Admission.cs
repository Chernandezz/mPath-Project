﻿using System.ComponentModel.DataAnnotations;

namespace mPathProject.Models
{
    public class Admission
    {
        public long id { get; set; }
        public string patientName { get; set; }

        [Required]
        public System.DateTime admissionDate { get; set; }
        [Required]
        public string diagnosis { get; set; }
        public string observation { get; set; }
        [Required]
        public long doctorId { get; set; }
        [Required]
        public long patientId { get; set; }


    }
}
