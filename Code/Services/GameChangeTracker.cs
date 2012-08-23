using Data;
using Domain;

namespace Services
{
    public class GameChangeTracker
    {
        private readonly Context _context;

        public GameChangeTracker(Context context)
        {
            _context = context;
        }

        public void TrackCreation(Game game)
        {
            var history = new GameChangeHistory();

            //Mapper.Map(game, history);

            _context.GameChangeHistories.Add(history);
        }

        public void TrackModification(Game game, User modifier, string note)
        {
            var history = new GameChangeHistory();

            //Mapper.Map(game, history);

            // if changer != owner, send email ... always include note

            _context.GameChangeHistories.Add(history);
        }

        public void TrackCancelation(Game game, User canceler, string note)
        {
            var history = new GameChangeHistory();

            //Mapper.Map(game, history);

            // if changer != owner, send email ... always include note

            _context.GameChangeHistories.Add(history);
        }
    }
}