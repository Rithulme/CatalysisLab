using Exercise;
using UtilityTools;

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

            XMLHandler.SerializeObject<BasicExercise>(testExercise, location);
        }
    }
}
