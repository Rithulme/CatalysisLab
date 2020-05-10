using Exercise;
using UtilityTools;
using Newtonsoft.Json;

namespace CreateExerciseFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            var JSONHandler = new JSONHandler();
            var testExercise = new BasicExercise()
            {
                Name = "testOefening",
                Problem = TestProblemGenerator.CreateExample()
            };            

            var location = @"D:\Gebruiker\Documents\CatalysisKineticsLab\JSONTest\test.txt";
            JSONHandler.SerializeObject<BasicExercise>(testExercise, location);

            var readFile = JSONHandler.DeSerializeObject<BasicExercise>(location);
            
        }
    }
}
