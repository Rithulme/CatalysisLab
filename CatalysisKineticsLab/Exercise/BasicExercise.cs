using ProblemTypes;
using UtilityTools;

namespace Exercise
{
    public class BasicExercise : IExercise
    {
        public XmlAnything<IProblemType> Problem { get; set; }
        public string Name { get; set; }

    }
}
