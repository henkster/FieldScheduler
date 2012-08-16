using System;
using System.IO;
using System.Linq;
using Data;
using Domain;
using Services.Infrastructure;

namespace Services
{
    public class RefereeEmailer
    {
        private readonly Context _context;
        private readonly string _baseDirectory;
        private readonly Emailer _emailer = new Emailer(new ConfigurationFinder());

        public RefereeEmailer(Context context, string baseDirectory)
        {
            _context = context;
            _baseDirectory = baseDirectory;
        }

        public void EmailNew(Game game)
        {
            if (OutsideLeadTime(game) || !AreRefereesNeeded(game)) return;

            SendRefereeMessage(game, "New Game");
        }
        public void EmailCanceled(Game game)
        {
            if (OutsideLeadTime(game) || !AreRefereesNeeded(game)) return;

            SendRefereeMessage(game, "Canceled Game");
        }
        public void EmailModified(Game game)
        {
            if (OutsideLeadTime(game) || !AreRefereesNeeded(game)) return;

            SendRefereeMessage(game, "Modified Game");
        }

        private void SendRefereeMessage(Game game, string subject)
        {
            foreach (User user in _context.Users.Where(u => (u.RolesAsInt & (int)Roles.Referee) == (int)Roles.Referee && u.IsActive))
            {
                _emailer.Send(user.EmailAddress,
                              subject,
                              Path.Combine(_baseDirectory, "templates/RefereeMessage.html"),
                              Path.Combine(_baseDirectory, "templates/RefereeMessage.txt"),
                              subject.ToUpper(),
                              game.Slot.StartDateTime.ToString("M/d/yy"),
                              game.Slot.StartDateTime.ToShortTimeString(),
                              game.Slot.Field.Description,
                              game.Team1.FullName,
                              game.Team2.FullName,
                              string.Format("U{0}", game.Age),
                              game.Gender,
                              game.Level,
                              game.Activity,
                              game.ScheduledBy.Name,
                              game.ScheduledOn,
                              game.Notes);
            }
        }

        private bool OutsideLeadTime(Game game)
        {
            return game.Slot.StartDateTime.Date > DateTime.Now.Date.AddDays(10);
        }

        private bool AreRefereesNeeded(Game game)
        {
            return game.AreRefereesNeeded;
        }
    }
}