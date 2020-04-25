﻿using ProblemTypes;
using Reaction.Entities;
using System;
using System.Collections.Generic;

namespace UtilityTools
{
    public static class TestProblemGenerator
    {
        private static List<Component> MakeComponents()
        {
            var componentList = new List<Component>();
            componentList.Add(new Component("A", "A"));
            componentList.Add(new Component("B", "B"));
            componentList.Add(new Component("C", "C"));

            return componentList;
        }

        public static XmlAnything<IProblemType> CreateExample()
        {
            var componentList = MakeComponents();

            // define partial reactions
            var ListReaction1LHS = new List<Tuple<Component, int>>();
            var ListReaction1RHS = new List<Tuple<Component, int>>();
            ListReaction1LHS.Add(new Tuple<Component, int>(componentList[0], 1));
            ListReaction1RHS.Add(new Tuple<Component, int>(componentList[1], 1));

            var Reaction1 = new ElementaryReaction(ListReaction1LHS, ListReaction1RHS, 250000, 50000, 125000, 52000);

            //make globalreaction
            List<ElementaryReaction> reactionList = new List<ElementaryReaction> { Reaction1 };
            var GlobalReaction = new GlobalReaction(reactionList);

            return new XmlAnything<IProblemType>(new BatchProblemNoDiffusion(1000.0, 10, GlobalReaction));

        }
    }
}
