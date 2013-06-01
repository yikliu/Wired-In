
using WiredIn.View;

namespace WiredIn.TransitCommand
{
    class NormalTransitCommand : TransitionCommand
    {
        private AbstractView view;
        private bool direction;
        
        public virtual void transit()
        {
            view.updateView(direction);
        }        

        public NormalTransitCommand(AbstractView v, bool goToGood)
        {
            this.view = v;
            this.direction = goToGood;
        }

        public void setDirection(bool d)
        {
            this.direction = d;
        }

        public void setView(AbstractView view)
        {
            this.view = view;
        }
    }

    
}
