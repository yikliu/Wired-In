using WiredIn.View;

namespace WiredIn.TransitionCommand
{
    abstract public class AbstractTransitionCommand
    {
        protected AbstractView view;

        protected Constants.State state;
        
        abstract public void Transit();        
        
        abstract public void SetState(Constants.State s);

        public Constants.State GetState()
        {
            return this.state;
        }

        abstract public void SetUp();

        abstract public void TearDown();

        abstract public void Pause();

        abstract public void Start();
    }
}
