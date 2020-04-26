using ProblemTypes;

namespace Exercise
{
    public class BasicExercise : IExercise
    {
        public IProblemType Problem { get; set; }
        public string Name { get; set; }

    }
}
