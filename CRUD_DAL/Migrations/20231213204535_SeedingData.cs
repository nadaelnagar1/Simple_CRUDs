using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUD_DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
         table: "Students",
         columns: new[] { "Id", "FirstName", "LastName", "EmailAddress", "BirthDate", "Gender", "GPA", "StudentImage", "CreadtedAt", "UpdatedAt", "IsDeleted" },
         values: new object[,]
         {
                {
                    Guid.NewGuid(), "John", "Doe", "john@example.com",
                    new DateTime(1995, 5, 15), 1, 3.75, "image1.jpg",
                    DateTime.Now, null, false
                },
                {
                    Guid.NewGuid(), "Emily", "Johanson", "Emily@example.com",
                    new DateTime(1997, 5, 15), 2, 3.5, "image2.jpg",
                    DateTime.Now, null, false
                },
                {
                    Guid.NewGuid(), "Joe", "Smith", "joe@example.com",
                    new DateTime(1998, 5, 15), 1, 3.75, "image3.jpg",
                    DateTime.Now, null, false
                },

         });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
