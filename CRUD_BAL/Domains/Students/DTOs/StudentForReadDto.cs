﻿
using CRUD_DAL.Database.Enums;

namespace CRUD_BAL.Domains.Students.DTOs
{
    public record StudentForReadDto
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string EmailAddress { get; set; } = "";
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public double GPA { get; set; }
        public string? StudentImage { get; set; } = "";
    }
}
