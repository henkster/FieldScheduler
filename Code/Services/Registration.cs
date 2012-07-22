using System.Linq;
using Data;

namespace Services
{
    public interface IRegistration
    {
        bool ValidateUser(string userName, string password);
    }

    public class Registration : IRegistration
    {
        private readonly Context _context = new Context();

        public bool ValidateUser(string userName, string password)
        {
            return _context.Users.Any(u => u.Username == userName && u.Password == password && u.IsActive);
        }
    }
}