using System;

namespace Domain
{
    [Flags]
    public enum Roles
    {
        None = 0,
        Admin = 1,
        Manager = 2,
        Referee = 4,
        Reader = 8
    }
}