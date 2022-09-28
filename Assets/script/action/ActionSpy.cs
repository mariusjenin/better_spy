namespace script.action
{
    public abstract class ActionSpy
    {
        protected Spy spy;
        public ActionSpy(Spy pSpy)
        {
            spy = pSpy;
        }

        public abstract void Do();
        
        public new abstract string ToString();
    }
}