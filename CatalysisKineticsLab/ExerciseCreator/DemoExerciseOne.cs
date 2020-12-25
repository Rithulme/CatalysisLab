using Exercise;
using ProblemTypes;
using Reaction.Entities;
using System.Collections.Generic;

namespace ExerciseCreator
{
    public static class DemoExerciseOne
    {
        public static BasicExercise CreateExercise()
        {
            return new BasicExercise()
            {
                Problem = CreateProblem(),
                Name = @"Demo oefening 1"
            };
        }

        private static List<Component> MakeComponents()
        {
            var componentList = new List<Component>();
            componentList.Add(new Component("A", "A"));
            componentList.Add(new Component("B", "B"));
            componentList.Add(new Component("C", "C"));
            componentList.Add(new Component("D", "D"));
            componentList.Add(new Component("E", "E"));
            componentList.Add(new Component("P", "P"));
            componentList.Add(new Component("BA", "BA"));

            return componentList;
        }

        private static BatchProblemNoDiffusion CreateProblem()
        {
            var componentList = MakeComponents();

            // define partial reactions
            //B + E -> P
            var ListReaction1LHS = new List<ReactionElement>();
            var ListReaction1RHS = new List<ReactionElement>();
            ListReaction1LHS.Add(new ReactionElement(componentList[1].Copy(), 1));
            ListReaction1LHS.Add(new ReactionElement(componentList[4].Copy(), 1));
            ListReaction1RHS.Add(new ReactionElement(componentList[5].Copy(), 1));

            var Reaction1 = new ElementaryReaction(ListReaction1LHS, ListReaction1RHS, 250000, 60000, 62500, 76000);

            //B + A -> BA
            var ListReaction2LHS = new List<ReactionElement>();
            var ListReaction2RHS = new List<ReactionElement>();
            ListReaction2LHS.Add(new ReactionElement(componentList[1].Copy(), 1));
            ListReaction2LHS.Add(new ReactionElement(componentList[0].Copy(), 1));
            ListReaction2RHS.Add(new ReactionElement(componentList[6].Copy(), 1));

            var Reaction2 = new ElementaryReaction(ListReaction2LHS, ListReaction2RHS, 250000, 50000, 125000, 22000);

            //BA + E => P + A
            var ListReaction3LHS = new List<ReactionElement>();
            var ListReaction3RHS = new List<ReactionElement>();
            ListReaction3LHS.Add(new ReactionElement(componentList[6].Copy(), 1));
            ListReaction3LHS.Add(new ReactionElement(componentList[4].Copy(), 1));
            ListReaction3RHS.Add(new ReactionElement(componentList[5].Copy(), 1));
            ListReaction3RHS.Add(new ReactionElement(componentList[0].Copy(), 1));

            var Reaction3 = new ElementaryReaction(ListReaction3LHS, ListReaction3RHS, 300000, 18000, 150000, 64000);

            //dummy reaction C + D => P

            var ListReaction4LHS = new List<ReactionElement>();
            var ListReaction4RHS = new List<ReactionElement>();
            ListReaction4LHS.Add(new ReactionElement(componentList[2].Copy(), 1));
            ListReaction4LHS.Add(new ReactionElement(componentList[3].Copy(), 1));
            ListReaction4RHS.Add(new ReactionElement(componentList[5].Copy(), 1));

            var Reaction4 = new ElementaryReaction(ListReaction4LHS, ListReaction4RHS, 0, 1000000, 0, 1000000);

            //make globalreaction
            List<ElementaryReaction> reactionList = new List<ElementaryReaction> { Reaction1, Reaction2, Reaction3, Reaction4 };
            var GlobalReaction = new GlobalReaction(reactionList);

            return new BatchProblemNoDiffusion(1000.0, 10, GlobalReaction);

        }
    }    
}
