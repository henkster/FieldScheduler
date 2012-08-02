
using System;
using System.Linq;
using Data;
using Domain;

namespace Services
{
    public class GameCreator
    {
        private readonly Context _context;
        private readonly string _baseDirectory;

        public GameCreator(Context context, string baseDirectory)
        {
            _context = context;
            _baseDirectory = baseDirectory;
        }

        public GameCreationResult Create(int slotId, int team1Id, int team2Id, Activities activity, string notes, bool areRefereesNeeded, User user)
        {
            Slot slot = _context.Slots.Find(slotId);

            if (slot == null || !slot.IsAvailable) return GameCreationResult.SlotNotAvailable;

            Team team1 = _context.ExternalTeams.SingleOrDefault(t => t.Id == team1Id) ?? (Team)_context.ClubTeams.SingleOrDefault(t => t.Id == team1Id);
            Team team2 = _context.ExternalTeams.SingleOrDefault(t => t.Id == team2Id) ?? (Team)_context.ClubTeams.SingleOrDefault(t => t.Id == team2Id);

            var game = new Game
                           {
                               Activity = activity,
                               ScheduledBy = user,
                               ScheduledOn = DateTime.Now,
                               Slot = slot,
                               Team1 = team1,
                               Team2 = team2,
                               Notes = notes,
                               AreRefereesNeeded = areRefereesNeeded
                           };


            _context.Games.Add(game);

            _context.SaveChanges();

            new RefereeEmailer(_context, _baseDirectory).EmailNew(game);

            return GameCreationResult.Success;
        }
    }

    public enum GameCreationResult
    {
        Success,
        SlotNotAvailable
    }
}