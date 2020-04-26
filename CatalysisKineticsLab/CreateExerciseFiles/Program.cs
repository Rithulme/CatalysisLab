using Exercise;
using UtilityTools;
using Newtonsoft.Json;

namespace CreateExerciseFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            var XMLHandler = new XMLHandler();
            var testExercise = new BasicExercise()
            {
                Name = "testOefening",
                Problem = TestProblemGenerator.CreateExample()
            };
            var location = @"D:\Gebruiker\Documents\CatalysisKineticsLab\XMLTest";

            var test = JsonConvert.SerializeObject(testExercise);
        }
    }
}
