using Exercise;
using Microsoft.Win32;
using Reaction.Entities;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using UtilityTools;

namespace StudentLabGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BasicExercise loadedExercise;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            var JSONReader = new JSONHandler();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                loadedExercise = JSONReader.DeSerializeObject<BasicExercise>(openFileDialog.FileName);
                ExerciseName.Text = loadedExercise.Name;
            }        
        }

        private void btnLoad_click(object sender, RoutedEventArgs e)
        {
            if (loadedExercise != null)
            {
                var componentList = loadedExercise.Problem.getComponents();
                List<string> componentNames = new List<string>();
                foreach (var component in componentList)
                {
                    componentNames.Add(component.Name);
                }
                fillDropdownMenu(componentNames);
            }
        }

        private void fillDropdownMenu(List<string> componentNames)
        {
            var mainContainer = (Panel)this.Content;
            var elements = mainContainer.Children;
            //cast to a list
            List<FrameworkElement> lstElement = elements.Cast<FrameworkElement>().ToList();
            Grid dropDownContainer = (Grid)lstElement.Where(x => x.Name.Equals("ConcenrationContainer")).First();
            var containerContents = dropDownContainer.Children;
            lstElement = containerContents.Cast<FrameworkElement>().ToList();
            var dropDownMenus = lstElement.Where(x => x.Name.Contains("Menu")); //todo:this needs to be casted when used!

            foreach (var menu in dropDownMenus)
            {
                var castedMenu = (ComboBox)menu;
                castedMenu.ItemsSource = componentNames;
            }
        }
    }
}
