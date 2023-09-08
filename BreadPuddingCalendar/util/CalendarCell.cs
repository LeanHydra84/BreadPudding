using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace BreadPuddingCalendar.Util
{
    public class CalendarCell
    {
        public int X;
        public int Y;
        public int Index;

        public Border Border;
        public TextBlock Text;
        public Grid Grid;
        public Ellipse Circle;
    }

}
