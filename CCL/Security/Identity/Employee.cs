
namespace CCL.Security.Identity
{
    public class Employee
        : User
    {
        public Employee(int userId, string name)
            : base(userId, name, nameof(Employee))
        {
        }
    }
}
