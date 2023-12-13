namespace CRUD_DAL.Database.Entities
{
    public class Student 
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string EmailAddress { get; set; } = "";
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public double GPA { get; set; }
        public string? StudentImage { get; set; } = "";
        public DateTime CreadtedAt { get; set; } = DateTime.UtcNow;
        public string? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;



    }
}
