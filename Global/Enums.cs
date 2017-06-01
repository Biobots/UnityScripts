using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    [Flags]
    public enum State
    {
        Shown = 0x01,
        Highlighted = 0x02,
        Hovered = 0x04,
        Selected = 0x08,
        Locked = 0x10,
        Linked = 0x20,

        Core = 0x40
    }

    public enum PropType
    {
        Key = 0,
        Name = 1,
        Info = 2
    }

    public enum CardColor
    {
        Blue,
        Red,
        Brown,
        Green
    }
    public enum GamePhase
    {
        Load,
        Start,
        Structure,
        Main,
        Complete
    }
}
