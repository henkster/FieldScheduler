using System.IO;
using System.Linq;
using CsvHelper;
using Data;
using Domain;

namespace Services
{
    public class ClubTeamImporter
    {
        private readonly Context _context;

        public ClubTeamImporter(Context context)
        {
            _context = context;
        }

        public void Import(Stream inputStream)
        {
            using (var reader = new StreamReader(inputStream))
            {
                var parser = new CsvParser(reader);
                string[] data;
                
                while ((data = parser.Read()) != null)
                {
                    string teamName = data[0];
                    string gender = data[7];
                    int age = int.Parse(data[3].Substring(1, data[3].Length - 1));
                    string level = data[4];
                    string emailAddress = data[2];
                    string managerName = data[5];
                    string phoneNumber = data[6];

                    ClubTeam team = _context.ClubTeams.FirstOrDefault(t => t.Name == teamName);

                    if (team != null) continue;

                    team = new ClubTeam
                               {
                                   Name = teamName,
                                   Division =
                                       _context.Divisions.SingleOrDefault(
                                           d =>
                                           d.Gender == gender &&
                                           d.Age == age),
                                   Level = FindLevel(level, age)
                               };

                    _context.ClubTeams.Add(team);

                    User manager = _context.Users.FirstOrDefault(m => m.EmailAddress == emailAddress && m.Name == managerName);

                    if (manager == null)
                    {
                        manager = new User
                                      {
                                          EmailAddress = emailAddress,
                                          IsActive = true,
                                          Name = managerName,
                                          Password = "fall2012",
                                          PhoneNumber = phoneNumber,
                                          Roles = Roles.Manager,
                                          Username = emailAddress
                                      };
                    }
                    team.Managers.Add(manager);

                    _context.SaveChanges();
                }
            }
        }

        private Level FindLevel(string levelString, int age)
        {
            if (age <= 11) return Level.Academy;

            switch (levelString)
            {
                case "1" : return Level.D1;
                case "2" : return Level.D2;
                case "3" : return Level.D3;
                default:return Level.Other;
            }
        }
    }
}