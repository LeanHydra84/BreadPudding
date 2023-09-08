using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BreadPuddingCalendar.util
{
    public class CalendarColorResource
    {
        public SolidColorBrush BorderOutline = new();
        public SolidColorBrush CellUnavailable = new();
        public SolidColorBrush CellAvailable = new();
        public SolidColorBrush CellPopulated = new();
        public SolidColorBrush CircleColor = new();
    }
}
