﻿namespace mPathProject.Application.DTOs
{
    public class UpdatePatientRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string? Observations { get; set; }
    }
}
