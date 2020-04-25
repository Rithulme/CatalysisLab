using ProblemTypes;
using Reaction.Entities;
using System;
using System.Collections.Generic;

namespace BatchReactionTest
{
    class Program
    {
        static List<Component> componentList;

        static void Main(string[] args)
        {
            componentList = MakeComponents();
            //var example = CreateExampleOne();
            var example = CreateExampleTwo();

            example.InitialConcentration = new Dictionary<Component, double>(new Component.EqualityComparer());
            example.InitialConcentration.Add(componentList[0], 0.2);
            example.InitialConcentration.Add(componentList[1], 0.0);
            example.InitialConcentration.Add(componentList[2], 0.0);
            example.ResultTimestep = 5;
            example.ReactionTemperature = 323;

            example.Solve();

            if (true)
            {

            }

        }

        public static List<Component> MakeComponents()
        {
            var componentList = new List<Component>();
            componentList.Add(new Component("A", "A"));
            componentList.Add(new Component("B", "B"));
            componentList.Add(new Component("C", "C"));

            return componentList;
        }

        public static BatchProblemNoDiffusion CreateExampleOne()
        {
            // define partial reactions
            var ListReaction1LHS = new List<ReactionElement>();
            var ListReaction1RHS = new List<ReactionElement>();
            ListReaction1LHS.Add(new ReactionElement(componentList[0], 1));
            ListReaction1RHS.Add(new ReactionElement(componentList[1], 1));

            var Reaction1 = new ElementaryReaction(ListReaction1LHS, ListReaction1RHS, 250000, 50000, 125000, 52000);

            //make globalreaction
            List <ElementaryReaction>  reactionList = new List<ElementaryReaction> { Reaction1 };
            var GlobalReaction = new GlobalReaction(reactionList);

            var returnproblem = new BatchProblemNoDiffusion(1000.0,10,GlobalReaction);

            return returnproblem;

        }

        public static BatchProblemNoDiffusion CreateExampleTwo()
        {
            // define partial reactions
            var ListReaction1LHS = new List<ReactionElement>();
            var ListReaction1RHS = new List<ReactionElement>();
            ListReaction1LHS.Add(new ReactionElement(componentList[0], 1));
            ListReaction1RHS.Add(new ReactionElement(componentList[1], 1));

            var Reaction1 = new ElementaryReaction(ListReaction1LHS, ListReaction1RHS, 250000, 40000, 125000, 42000);

            var ListReaction2LHS = new List<ReactionElement>();
            var ListReaction2RHS = new List<ReactionElement>();
            ListReaction2LHS.Add(new ReactionElement(componentList[1], 1));
            ListReaction2RHS.Add(new ReactionElement(componentList[2], 1));

            var Reaction2 = new ElementaryReaction(ListReaction2LHS, ListReaction2RHS, 220000, 51000, 0, 52000);

            //make globalreaction
            List<ElementaryReaction> reactionList = new List<ElementaryReaction> { Reaction1, Reaction2 };
            var GlobalReaction = new GlobalReaction(reactionList);

            var returnproblem = new BatchProblemNoDiffusion(1000.0, 10, GlobalReaction);

            return returnproblem;
        }
    }
}
