namespace Catalog.DAL.Entities
{
    public class User
    {
        public int userId { get; set; }
        public string name { get; set; }
        private string email { get; set; }
        private string password { get; set; }
        private string login { get; set; }
    }
    public class Admin : User
    {
    }
    public class Employee : User
    {
        private int employeeId { get; set; }
        public string position { get; set; }
        public string department { get; set; }

    }
    public class MedicalStaff : User
    {
        private int doctorId { get; set; }
        public string position { get; set; }
    }
}
