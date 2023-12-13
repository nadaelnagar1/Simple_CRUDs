namespace CRUD_BAL.Common.DTOs
{
    public static class ResponseMessages
    {
        public const string Student = "Student";
        public const string Error = "Error";
        public const string notFound = "NotFound";
        public static string Deleted(string intended)
        {
            return $"{intended} Deleted Successfully!";
        }
        public static string NotFound(string intended)
        {
            return $"{intended} Not Found";
        }
    }
}
