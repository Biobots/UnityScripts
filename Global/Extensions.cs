using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public static class Extensions
    {
        #region State
        public static bool Contain(this State state, State value)
        { return (state & value) == value; }
        public static State Add(this State left, State right)
        { return left | (right ^ (left & right)); }
        public static State Remove(this State left, State right)
        { return left ^ (left & right); }
        public static State Swap(this State target, State left, State right)
        {
            if (target.Contain(left))
            {
                target.Remove(left);
                target.Add(right);
            }
            return target;
        }
        public static State ChangeTo(this State left, State right)
        { return right; }
        public static void Update(this State value)
        {
            if (value.Contain(State.Core))
            { value.Add(State.Linked); }
        }
        #endregion

        #region GamePhase
        #endregion
    }
}
