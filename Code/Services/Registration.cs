using System.IO;
using System.Linq;
using Data;
using Domain;
using Services.Infrastructure;

namespace Services
{
    public interface IRegistration
    {
        bool ValidateUser(string userName, string password);
    }

    public class Registration : IRegistration
    {
        private readonly Emailer _emailer = new Emailer(new ConfigurationFinder());
        private readonly Context _context = new Context();

        public bool ValidateUser(string userName, string password)
        {
            return _context.Users.Any(u => u.Username == userName && u.Password == password && u.IsActive);
        }

        public AccountLookupResult SendAccountInfo(string email, string baseDirectory)
        {
            var users = _context.Users.Where(u => u.EmailAddress == email).ToList();

            if (users.Count == 0)
            {
                return AccountLookupResult.AccountNotFoundByEmailAddres;
            }

            foreach (User user in users)
            {
                _emailer.Send(email, "Scheduler Account Info", Path.Combine(baseDirectory, "templates/AccountInfo.html"), Path.Combine(baseDirectory, "templates/AccountInfo.txt"), user.Username, user.Name);
            }

            return AccountLookupResult.Success;
        }
    }
}