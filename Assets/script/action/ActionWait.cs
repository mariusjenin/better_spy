using System.Transactions;

namespace script.action
{
    public class ActionWait : ActionMoove
    {
        public ActionWait(Spy spy) : base(spy, spy.Position)
        {
        }

        public override string ToString()
        {
            return "Wait(" + position.X + ";" + position.Y + ")";
        }
    }
}