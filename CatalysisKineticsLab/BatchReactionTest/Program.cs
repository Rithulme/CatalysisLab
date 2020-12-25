using ExerciseCreator;
using Reaction.Entities;
using System.Collections.Generic;

namespace BatchReactionTest
{
    class Program
    {
        static List<Component> componentList;

        static void Main(string[] args)
        {
            var exercise = DemoExerciseOne.CreateExercise();
            var componentList = exercise.Problem.GlobalReaction.GlobalComponentList;

            exercise.Problem.InitialConcentration = new Dictionary<Component, double>(new Component.EqualityComparer());
            exercise.Problem.TotalTime = 5000;
            exercise.Problem.NumberOfSamples = 20;
            exercise.Problem.ReactionTemperature = 393.15;

            foreach (var component in componentList)
            {
                if (component.Name == "P" || component.Name == "BA" )
                {
                    exercise.Problem.InitialConcentration.Add(component, 0);
                }
                else
                {
                    exercise.Problem.InitialConcentration.Add(component, 0.2);
                }
            }

            exercise.Problem.Solve();

            if (true)
            {

            }

        }          
    }
}
