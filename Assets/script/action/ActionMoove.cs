using System.Numerics;
using UnityEngine.UIElements;

namespace script.action
{
    public class ActionMoove : ActionSpy
    {
        protected Vector2 position;
        public ActionMoove(Spy spy, Vector2 pos) : base(spy)
        {
            position = pos;
        }

        public override void Do()
        {
            spy.Moove(position);
        }

        public override string ToString()
        {
            return "Move(" + position.X + ";" + position.Y + ")";
        }
    }
}