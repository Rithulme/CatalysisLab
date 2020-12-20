using Reaction.Entities;

namespace Exercise.Entities
{
    class ConcentrationBoundaries
    {
        public Component Component { get; set; }
        public double MinimumConcentration { get; set; }
        public double MaximumConcentration { get; set; }
        public bool ConcentrationCanBeSet { get; set; }
    }
}
