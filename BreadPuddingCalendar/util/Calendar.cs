using BreadPudding;
using BreadPuddingCalendar.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BreadPuddingCalendar.Util
{
    public class Calendar
    {

        public Calendar(Grid populateGrid, CalendarColorResource colors)
        {
            LeftPad = 0;
            Cells = new();
            LoadedData = new();
            ColorResource = colors;
            SetupGrid(populateGrid);
            SetMonth(DateTime.Now.Date);
        }

        public CalendarColorResource ColorResource { get; set; }

        public List<CalendarCell> Cells;

        private DateTime currentMonth;
        public DateTime CurrentMonth
        {
            get => currentMonth;
            set
            {
                SetMonth(value);
            }
        }

        public TextBlock? HeaderTextField;

        public List<Food[]?> LoadedData;
        private int LeftPad;

        private CalendarCell FindCellFromBorder(Border clicked)
        {
            foreach(CalendarCell cell in Cells)
            {
                if (cell.Border == clicked)
                    return cell;
            }
            throw new IndexOutOfRangeException();
        }

        private void OnClickCell(CalendarCell cell)
        {
            int cellIndex = cell.Index - LeftPad - 1;
            if (cellIndex >= LoadedData.Count) return;
            if (LoadedData[cellIndex] == null || LoadedData[cellIndex]?.Length == 0) return;
            FoodDataWindow data = new FoodDataWindow(LoadedData[cellIndex]);
            data.ShowDialog();
        }

        private void SetupGrid(Grid calendarGrid)
        {
            SolidColorBrush borderLines = new SolidColorBrush(Colors.Black);

            for (int i = 0; i < calendarGrid.RowDefinitions.Count; i++)
            {
                for (int j = 0; j < calendarGrid.ColumnDefinitions.Count; j++)
                {
                    Border b = new Border();
                    b.BorderThickness = new Thickness(2);
                    b.BorderBrush = borderLines;
                    b.CornerRadius = new CornerRadius(5);
                    b.MouseDown += (s, e) => OnClickCell(FindCellFromBorder((Border)s));

                    Grid basicGrid = new Grid();
                    b.Child = basicGrid;

                    TextBlock subtext = new TextBlock();
                    subtext.FontSize = 15;
                    subtext.Text = "99";
                    subtext.HorizontalAlignment = HorizontalAlignment.Left;
                    subtext.VerticalAlignment = VerticalAlignment.Top;
                    subtext.Margin = new Thickness(5);
                    basicGrid.Children.Add(subtext);

                    Ellipse circle = new Ellipse();
                    circle.HorizontalAlignment = HorizontalAlignment.Center;
                    circle.VerticalAlignment = VerticalAlignment.Center;
                    circle.Width = 20;
                    circle.Height = 20;
                    circle.Fill = ColorResource.CircleColor;
                    circle.Visibility = Visibility.Hidden;
                    basicGrid.Children.Add(circle);

                    Grid.SetRow(b, i);
                    Grid.SetColumn(b, j);
                    calendarGrid.Children.Add(b);

                    CalendarCell cell = new CalendarCell()
                    {
                        X = j,
                        Y = i,
                        Index = Cells.Count,
                        Border = b,
                        Text = subtext,
                        Grid = basicGrid,
                        Circle = circle,
                    };

                    Cells.Add(cell);
                }
            }
        }


        public void SetMonth(DateTime month)
        {
            currentMonth = month;
            LoadedData.Clear();

            if(HeaderTextField != null)
            HeaderTextField.Text = $"{month.ToString("MMMM")} {month.Year}";

            int daysInMonth = DateTime.DaysInMonth(month.Year, month.Month);

            LeftPad = (Cells.Count - daysInMonth - 1) / 2;
            Console.WriteLine($"LeftPad now {LeftPad}");

            for (int i = 0; i < Cells.Count; i++)
            {
                CalendarCell cell = Cells[i];
                cell.Circle.Visibility = Visibility.Hidden;
                if (i > LeftPad && i < daysInMonth + LeftPad + 1)
                {
                    cell.Text.Text = (i - LeftPad).ToString();
                    cell.Border.Background = ColorResource.CellAvailable;
                }
                else
                {
                    cell.Text.Text = string.Empty;
                    cell.Border.Background = ColorResource.CellUnavailable;
                }
            }
        }


    }
}
