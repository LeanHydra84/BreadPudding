using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using BreadPuddingCalendar.Util;
using BreadPudding;
using BreadPuddingCalendar.util;

namespace BreadPuddingCalendar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {


            InitializeComponent();

            CalendarColorResource colors = new CalendarColorResource
            {
                BorderOutline = new SolidColorBrush(Colors.Black),
                CellUnavailable = new SolidColorBrush(Colors.DimGray),
                CellAvailable = new SolidColorBrush(Colors.White),
                CellPopulated = new SolidColorBrush(Colors.LightBlue),
                CircleColor = new SolidColorBrush(Colors.Orange),
            };

            cal = new Util.Calendar(calendarGrid, colors);
            cal.HeaderTextField = yearMonthField;
            cal.SetMonth(cal.CurrentMonth);

            loader = new CalendarLoader(new BreadCacheManager("data"), cal);
            loader.Parser.AddFoodRule(a => a.Name.Contains("Bread Pudding"));

        }


        private Util.Calendar cal;
        private CalendarLoader loader;

        private void NextMonth_Click(object sender, RoutedEventArgs e)
        {
            cal.CurrentMonth = cal.CurrentMonth.AddMonths(1);
        }

        private void PrevMonth_Click(object sender, RoutedEventArgs e)
        {
            cal.CurrentMonth = cal.CurrentMonth.AddMonths(-1);
        }

        private void LoadMonthData_Click(object sender, RoutedEventArgs e)
        {
            DateTime curMonth = cal.CurrentMonth;
            DateTime firstOfMonth = new DateTime(curMonth.Year, curMonth.Month, 1);
            loader.LoadMonth(firstOfMonth);
        }


    }
}
