﻿namespace mPathProject.Application.DTOs
{
    public class UpdateAdmissionRequestDto
    {
        public DateTime AdmissionDate { get; set; }
        public string Diagnosis { get; set; }
        public string? Observation { get; set; }
        public long DoctorId { get; set; }
        public long PatientId { get; set; }
    }
}
