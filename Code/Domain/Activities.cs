using System;

namespace Domain
{
    [Flags]
    public enum Activities
    {
        StateLeague = 1,
        Friendly = 2,
        Training = 4
    }
}