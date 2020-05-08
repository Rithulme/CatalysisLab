using Exercise;
using StudentLabGui.Entities;
using System.Collections.Generic;
using System.Windows;

namespace StudentLabGui
{
    /// <summary>
    /// Interaction logic for ResultsWindow.xaml
    /// </summary>
    public partial class ResultsWindow : Window
    {
        public ResultsWindow(List<ResultsData> resultsList)
        {
            InitializeComponent();
            FillDatagrid(resultsList);
        }

        private void FillDatagrid(List<ResultsData> resultsList)
        {
            Results.ItemsSource = resultsList;
        }
    }
}
