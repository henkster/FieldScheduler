using System;

namespace Domain
{
    [Flags]
    public enum Activities
    {
        None = 0,
        StateLeague = 1,
        Friendly = 2,
        Training = 4
    }
}