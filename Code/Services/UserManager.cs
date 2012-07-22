using System.Linq;
using Data;
using Domain;

namespace Services
{
    public class UserManager
    {
        private readonly Context _context = new Context();

        public void Delete(User user)
        {
            _context.Users.Attach(user); // TODO this is getting weird

            if (CanUserBeDeleted(user))
            {
                _context.Users.Remove(user); // TODO what if logged-in user?
            }
            else
            {
                user.IsActive = false;
            }
            _context.SaveChanges();
        }

        public bool CanUserBeDeleted(User user)
        {
            return !HasUserScheduledGames(user);
        }

        private bool HasUserScheduledGames(User user)
        {
            return _context.Games.Any(g => g.ScheduledBy.Id == user.Id);
        }
    }
}