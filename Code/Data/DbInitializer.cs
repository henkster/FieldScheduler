using System;
using System.Data.Entity;
using Domain;

namespace Data
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<Context>
    {
        protected override void Seed(Context context)
        {
            context.Users.Add(new User
            {
                EmailAddress = "steve@henkster.com",
                IsActive = true,
                Name = "Steve Henke (admin)",
                Password = "spring2012",
                PhoneNumber = "615-522-2457",
                Username = "shenke",
                IsAdmin = true
            });

            var user = new User
                           {
                               EmailAddress = "steve@henkster.com",
                               IsActive = true,
                               Name = "Steve Henke (non-admin)",
                               Password = "password",
                               PhoneNumber = "615-522-2457",
                               Username = "shenke2",
                               IsAdmin = false
                           };

            context.Users.Add(user);

            var club = new Club
                           {
                               Name = "Harpeth Futbol Club",
                               CityState = "Bellevue, TN"
                           };

            context.Clubs.Add(club);

            var division1 = new Division(12, "Boys");

            var team2 = new ExternalTeam
                            {
                                Club = club,
                                Name = "HFC 01 Black",
                                CityState = "Bellevue, TN",
                                Division = division1,
                                ContactName = "Joe Mama",
                                ContactEmailAddress = "joemama@henkster.com",
                                ContactPhoneNumber = "6155551212"
                            };

            context.ExternalTeams.Add(team2);

            var team1 = new ClubTeam
                            {
                                Name = "01 Boys Premier South",
                                Division = division1
                            };

            team1.Managers.Add(user);

            context.ClubTeams.Add(team1);

            var field1 = new Field
                             {
                                 Description = "Downs - 23A",
                                 AreRefereesRequired = true,
                                 HasLights = true,
                                 Size = FieldSize.EightVsEight,
                                 AllowedActivities = Activities.Friendly | Activities.StateLeague
                             };

            context.Fields.Add(field1);

            var field2 = new Field
                             {
                                 Description = "Crockett - 4A",
                                 AreRefereesRequired = true,
                                 HasLights = false,
                                 Size = FieldSize.EightVsEight,
                                 AllowedActivities = Activities.Friendly | Activities.StateLeague | Activities.Training
                             };

            context.Fields.Add(field2);

            context.Fields.Add(new Field
                             {
                                 Description = "Downs - 22",
                                 AreRefereesRequired = true,
                                 HasLights = true,
                                 Size = FieldSize.ElevenVsEleven,
                                 AllowedActivities = Activities.Friendly | Activities.StateLeague
                             });

            var slotStart = new DateTime(2012, 8, 26, 11, 30, 00);

            context.Slots.Add(new Slot
                                  {
                                      StartDateTime = slotStart,
                                      EndDateTime = slotStart.AddMinutes((int)SlotDuration.Ninety),
                                      Field = field1
                                  });

            slotStart = new DateTime(2012, 8, 26, 13, 00, 00);

            context.Slots.Add(new Slot
                                  {
                                      StartDateTime = slotStart,
                                      EndDateTime = slotStart.AddMinutes((int)SlotDuration.Ninety),
                                      Field = field1
                                  });

            slotStart = new DateTime(2012, 8, 26, 14, 30, 00);

            var slot = new Slot
                           {
                               StartDateTime = slotStart,
                               EndDateTime = slotStart.AddMinutes((int)SlotDuration.Ninety),
                               Field = field1
                           };

            context.Slots.Add(slot);

            slotStart = new DateTime(2012, 8, 26, 16, 00, 00);

            context.Slots.Add(new Slot
                                  {
                                      StartDateTime = slotStart,
                                      EndDateTime = slotStart.AddMinutes((int)SlotDuration.Ninety),
                                      Field = field1
                                  });

            context.Games.Add(new Game
                                  {
                                      Activity = Activities.Friendly,
                                      AreRefereesNeeded = true,
                                      Notes = "Nothing special - just testing.",
                                      ScheduledBy = user,
                                      ScheduledOn = DateTime.Now,
                                      Slot = slot,
                                      Team1 = team1,
                                      Team2 = team2
                                  });

            context.Settings.Add(new Setting
                                     {
                                         Key = "system-mode",
                                         Description = "System mode",
                                         Value = "public"
                                     });

            context.Settings.Add(new Setting
                                     {
                                         Key = "home-page-message",
                                         Description = "Home page message",
                                         Value =
                                             @"    <p>We can start playing Games at the Complex on 3/8/2012</p>
    <p><span style=""font-size: medium; ""><span style=""color: rgb(255, 0, 0); ""><em><strong>No Games can Be Played at the REC Center. Schedule Rec Center Fields for Training only<br>
    <br>
    Rec Center Fields Closed 2/25 and 3/3 From 11AM - 12PM<br>
    </strong></em></span></span></p>
    <p><span style=""font-size: medium; ""><span style=""color: rgb(255, 0, 0); ""><em><strong>Rec Center Fields will Be Closed Mar 31st For annual Easter Egg Hunt and Maybe Closed Apr 7th if Easter Egg unt is Rained Out.</strong></em></span></span></p>
    <p>Ref Assignor Randy Pavlovich Email is <a href=""mailto:referee@tennesseesoccerclub.org?subject=TSC%20Scheduled%20Games"">referee@tnfc.org</a></p>
    <p>Please email <a href=""http://www.tennesseesoccerclub.org/page/show/533586-board-of-directors"" target=""_blank"">Bill Bailey, VP of Operations</a> with any suggestions you may have to improve this process.</p>
"
                                     });

            context.Settings.Add(new Setting
                                     {
                                         Key = "setup-mode-message",
                                         Description = "System Setup Mode Message",
                                         Value = "<p>The system is being set up.  We will notify you as soon as it comes online.</p>"
                                     });

            context.Settings.Add(new Setting
                                     {
                                         Key = "maintenance-mode-message",
                                         Description = "System Maintenance Mode Message",
                                         Value =
                                             "<p>The system is in maintenance mode.  We will notify you as soon as it comes online.</p>"
                                     });

            context.Settings.Add(new Setting
                                     {
                                         Key = "state-league-mode-message",
                                         Description = "System State League Mode Message",
                                         Value =
                                             "<p>The system is currently only available for setting up State League games.  We will notify you as soon as it is available for friendlies and training.</p>"
                                     });

            CreateDivisions(context, division1);

            base.Seed(context);
        }

        private void CreateDivisions(Context context, Division division)
        {
            context.Divisions.Add(new Division(8, "Boys"));
            context.Divisions.Add(new Division(8, "Girls"));
            context.Divisions.Add(new Division(9, "Boys"));
            context.Divisions.Add(new Division(9, "Girls"));
            context.Divisions.Add(new Division(10,"Boys"));
            context.Divisions.Add(new Division(10,"Girls"));
            context.Divisions.Add(new Division(11,"Boys"));
            context.Divisions.Add(new Division(11,"Girls"));
            context.Divisions.Add(division);
            context.Divisions.Add(new Division(12,"Girls"));
            context.Divisions.Add(new Division(13,"Boys"));
            context.Divisions.Add(new Division(13,"Girls"));
            context.Divisions.Add(new Division(14,"Boys"));
            context.Divisions.Add(new Division(14,"Girls"));
            context.Divisions.Add(new Division(15,"Boys"));
            context.Divisions.Add(new Division(15,"Girls"));
            context.Divisions.Add(new Division(16,"Boys"));
            context.Divisions.Add(new Division(16,"Girls"));
            context.Divisions.Add(new Division(17,"Boys"));
            context.Divisions.Add(new Division(17,"Girls"));
            context.Divisions.Add(new Division(18,"Boys"));
            context.Divisions.Add(new Division(18,"Girls"));
        }
    }
}