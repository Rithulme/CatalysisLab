﻿using Exercise;
using Microsoft.Win32;
using Reaction.Entities;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using UtilityTools;
using System;
using StudentLabGui.Entities;
using System.Globalization;

namespace StudentLabGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BasicExercise loadedExercise;
        private List<ResultsData> resultsList;
        private List<String> componentNames;
        private const int numberOfInputs = 7;
        
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

        private void btnLoad_Click(object sender, RoutedEventArgs e)
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

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            setInitialConcentrations();
            setTemperature();
            setTimeStep(); // todo, set total time of experiment
            solveReaction();
            var currentResults = new ResultsWindow(resultsList, componentNames, numberOfInputs + 1);
            currentResults.Show();
        }

        private void solveReaction()
        {
            loadedExercise.Problem.Solve();
            resultsList = new List<ResultsData>();
            var componentList = loadedExercise.Problem.getComponents();
            componentNames = new List<string> { "Tijdstap (s)" };

            foreach (var component in componentList)
            {
                componentNames.Add(component.Name);
            }

            int length = loadedExercise.Problem.ResultConcentration[componentList[0]].Count();

            for (int i = 0; i < length; i++)
            {
                var addedRow = new ResultsData();
                var resultsConcentrationRow = new double [] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
                int counter = 0;
                foreach (var component in componentList)
                {
                    resultsConcentrationRow[counter] = loadedExercise.Problem.ResultConcentration[component][i];
                    counter++;
                }

                addedRow.Conc1 = resultsConcentrationRow[0];
                addedRow.Conc2 = resultsConcentrationRow[1];
                addedRow.Conc3 = resultsConcentrationRow[2];
                addedRow.Conc4 = resultsConcentrationRow[3];
                addedRow.Conc5 = resultsConcentrationRow[4];
                addedRow.Conc6 = resultsConcentrationRow[5];
                addedRow.Conc7 = resultsConcentrationRow[6];

                resultsList.Add(addedRow);
            }
        }

        private void setTimeStep()
        {
            string timeStepString = TimeStep.Text;
            double timeStep = Double.Parse(timeStepString, NumberStyles.Any, CultureInfo.InvariantCulture);
            loadedExercise.Problem.ResultTimestep = timeStep;

            string totalTimeString = TotalTime.Text;
            double totalTime = Double.Parse(totalTimeString, NumberStyles.Any, CultureInfo.InvariantCulture);
            loadedExercise.Problem.TotalTime = totalTime;
        }

        private void setTemperature()
        {
            string temperatureString = Temperature.Text;
            var temperature = Double.Parse(temperatureString, NumberStyles.Any, CultureInfo.InvariantCulture);
            loadedExercise.Problem.ReactionTemperature = temperature + 273.16;
        }

        private void setInitialConcentrations()
        {
            var mainContainer = (Panel)this.Content;
            var elements = mainContainer.Children;
            List<FrameworkElement> lstElement = elements.Cast<FrameworkElement>().ToList();
            Grid dropDownContainer = (Grid)lstElement.Where(x => x.Name.Equals("ConcenrationContainer")).First();
            var containerContents = dropDownContainer.Children;
            lstElement = containerContents.Cast<FrameworkElement>().ToList();
            var dropDownMenus = lstElement.Where(x => x.Name.Contains("Menu"));
            var concentrationFields = lstElement.Where(x => x.Name.Contains("Concentration"));

            loadedExercise.Problem.InitialConcentration = new Dictionary<Component, double>(new Component.EqualityComparer());

            foreach (var field in concentrationFields)
            {
                var textBox = (TextBox)field;
                if (!String.IsNullOrEmpty(textBox.Text))
                {
                    var nummer = field.Name.Substring(field.Name.Length - 1);
                    var dropDownMenu = (ComboBox)dropDownMenus.Where(x => x.Name.Contains(nummer)).Last();
                    string componentName = (string)dropDownMenu.SelectedValue;
                    if (!String.IsNullOrEmpty(componentName))
                    {
                        var components = loadedExercise.Problem.getComponents();
                        var selectedComponent = components.Where(x => x.Name.Equals(componentName)).First().Copy();
                        if (loadedExercise.Problem.InitialConcentration == null || !loadedExercise.Problem.InitialConcentration.ContainsKey(selectedComponent))
                        {
                            loadedExercise.Problem.InitialConcentration.Add(selectedComponent, Double.Parse(textBox.Text, NumberStyles.Any, CultureInfo.InvariantCulture));
                        }
                    }
                }
            }
        }

        private void fillDropdownMenu(List<string> componentNames)
        {
            var mainContainer = (Panel)this.Content;
            var elements = mainContainer.Children;
            List<FrameworkElement> lstElement = elements.Cast<FrameworkElement>().ToList();
            Grid dropDownContainer = (Grid)lstElement.Where(x => x.Name.Equals("ConcenrationContainer")).First();
            var containerContents = dropDownContainer.Children;
            lstElement = containerContents.Cast<FrameworkElement>().ToList();
            var dropDownMenus = lstElement.Where(x => x.Name.Contains("Menu")); 

            foreach (var menu in dropDownMenus)
            {
                var castedMenu = (ComboBox)menu;
                castedMenu.ItemsSource = componentNames;
            }
        }
    }
}
