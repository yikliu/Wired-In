using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using WiredIn.View;

namespace WiredIn.Command
{
    class NormalTransitCommand : TransitionCommand
    {
        private AbstractView view;
        private bool direction;
        
        public void transit()
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
    }

    
}
