
namespace CCL.Security.Identity
{
    public class MedStuff
        : User
    {
        public MedStuff(int userId, string name)
            : base(userId, name, nameof(MedStuff))
        {
        }
    }
}
