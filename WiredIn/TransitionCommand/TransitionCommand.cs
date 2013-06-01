using WiredIn.View;

namespace WiredIn.TransitCommand
{
    interface TransitionCommand
    {
        void transit();        
        void setDirection(bool d);
        void setView(AbstractView v);
    }
}
