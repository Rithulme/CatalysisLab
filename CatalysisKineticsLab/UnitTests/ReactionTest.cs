using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProblemTypes;
using Reaction.Entities;

namespace UnitTests
{
    [TestClass]
    public class ReactionTest
    {
        [TestMethod]
        public void ReactionTestOne()
        {
            //Arrange
            var testreaction = CreateExampleOne();
            var components = testreaction.GlobalReaction.GlobalComponentList;
            var reference = new Dictionary<Component, List<double>>(new Component.EqualityComparer())
            {
                { new Component("A", "A"), new List<double> { 0.200, 0.198, 0.196, 0.194, 0.192, 0.190, 0.188, 0.186, 0.184, 0.183, 0.181, 0.179, 0.177, 0.175, 0.174,
                0.172, 0.170, 0.169, 0.167, 0.165, 0.164, 0.162, 0.161, 0.159, 0.158, 0.156, 0.155, 0.153, 0.152, 0.150, 0.149, 0.147, 0.146, 0.145, 0.143,
                0.142, 0.141, 0.139, 0.138, 0.137, 0.136, 0.134, 0.133, 0.132, 0.131, 0.130, 0.128, 0.127, 0.126, 0.125, 0.124, 0.123, 0.122, 0.121, 0.120,
                0.119, 0.118, 0.117, 0.116, 0.115, 0.114, 0.113, 0.112, 0.111, 0.110, 0.109, 0.108, 0.107, 0.107, 0.106, 0.105, 0.104, 0.103, 0.102, 0.102,
                0.101, 0.100, 0.099, 0.098, 0.098, 0.097, 0.096, 0.095, 0.095, 0.094, 0.093, 0.093, 0.092, 0.091, 0.091, 0.090, 0.089, 0.089, 0.088, 0.087, 0.087,
                0.086, 0.086, 0.085, 0.084, 0.084, 0.083, 0.083, 0.082, 0.082, 0.081, 0.080, 0.080, 0.079, 0.079, 0.078, 0.078, 0.077, 0.077, 0.076, 0.076, 0.075,
                0.075, 0.074, 0.074, 0.074, 0.073, 0.073, 0.072, 0.072, 0.071, 0.071, 0.071, 0.070, 0.070, 0.069, 0.069, 0.069, 0.068, 0.068, 0.067, 0.067, 0.067,
                0.066, 0.066, 0.066, 0.065, 0.065, 0.065, 0.064, 0.064, 0.064, 0.063, 0.063, 0.063, 0.062, 0.062, 0.062, 0.062, 0.061, 0.061, 0.061, 0.060, 0.060,
                0.060, 0.060, 0.059, 0.059, 0.059, 0.059, 0.058, 0.058, 0.058, 0.058, 0.057, 0.057, 0.057, 0.057, 0.056, 0.056, 0.056, 0.056, 0.055, 0.055, 0.055,
                0.055, 0.055, 0.054, 0.054, 0.054, 0.054, 0.054, 0.053, 0.053, 0.053, 0.053, 0.053, 0.052, 0.052, 0.052, 0.052, 0.052, 0.052, 0.051, 0.051, 0.051 } },

                { new Component("B", "B"),  new List<double> {0.000,0.002,0.004,0.006,0.008,0.010,0.012,0.014,0.016,0.017,0.019,0.021,0.023,0.025,0.026,0.028,0.030,0.031,
                0.033,0.035,0.036,0.038,0.039,0.041,0.042,0.044,0.045,0.047,0.048,0.050,0.051,0.053,0.054,0.055,0.057,0.058,0.059,0.061,0.062,0.063,0.064,0.066,
                0.067,0.068,0.069,0.070,0.072,0.073,0.074,0.075,0.076,0.077,0.078,0.079,0.080,0.081,0.082,0.083,0.084,0.085,0.086,0.087,0.088,0.089,0.090,0.091,
                0.092,0.093,0.093,0.094,0.095,0.096,0.097,0.098,0.098,0.099,0.100,0.101,0.102,0.102,0.103,0.104,0.105,0.105,0.106,0.107,0.107,0.108,0.109,0.109,
                0.110,0.111,0.111,0.112,0.113,0.113,0.114,0.114,0.115,0.116,0.116,0.117,0.117,0.118,0.118,0.119,0.120,0.120,0.121,0.121,0.122,0.122,0.123,0.123,
                0.124,0.124,0.125,0.125,0.126,0.126,0.126,0.127,0.127,0.128,0.128,0.129,0.129,0.129,0.130,0.130,0.131,0.131,0.131,0.132,0.132,0.133,0.133,0.133,
                0.134,0.134,0.134,0.135,0.135,0.135,0.136,0.136,0.136,0.137,0.137,0.137,0.138,0.138,0.138,0.138,0.139,0.139,0.139,0.140,0.140,0.140,0.140,0.141,
                0.141,0.141,0.141,0.142,0.142,0.142,0.142,0.143,0.143,0.143,0.143,0.144,0.144,0.144,0.144,0.145,0.145,0.145,0.145,0.145,0.146,0.146,0.146,0.146,
                0.146,0.147,0.147,0.147,0.147,0.147,0.148,0.148,0.148,0.148,0.148,0.148,0.149,0.149,0.149}}
            };

            testreaction.InitialConcentration = new Dictionary<Component, double>(new Component.EqualityComparer());
            testreaction.InitialConcentration.Add(components.Single(x => x.Name == "A"), 0.2);
            testreaction.InitialConcentration.Add(components.Single(x => x.Name == "B"), 0.0);
            testreaction.ResultTimestep = 5;
            testreaction.ReactionTemperature = 323;

            //Act
            testreaction.Solve();

            //Assert
            foreach (var component in components)
            {
                var counter = 0;
                foreach (var concentration in testreaction.ResultConcentration[component])
                {
                    Assert.AreEqual(reference[component][counter], concentration, 0.01);
                    counter++;
                }
            }
            
        }
        
        [TestMethod]
        public void ReactionTestTwo()
        {
            //Arrange
            var testreaction = CreateExampleTwo();
            var components = testreaction.GlobalReaction.GlobalComponentList;
            var reference = new Dictionary<Component, List<double>>(new Component.EqualityComparer())
            {
                { new Component("A", "A"), new List<double> {0.200,0.134,0.095,0.071,0.058,0.050,0.045,0.042,0.040,0.039,0.038,0.037,0.037,0.037,0.037,0.036,0.036,
                    0.036,0.036,0.036,0.035,0.035,0.035,0.035,0.035,0.034,0.034,0.034,0.034,0.034,0.034,0.033,0.033,0.033,0.033,0.033,0.033,0.032,0.032,0.032,0.032,
                    0.032,0.032,0.032,0.031,0.031,0.031,0.031,0.031,0.031,0.030,0.030,0.030,0.030,0.030,0.030,0.030,0.029,0.029,0.029,0.029,0.029,0.029,0.028,0.028,
                    0.028,0.028,0.028,0.028,0.028,0.028,0.027,0.027,0.027,0.027,0.027,0.027,0.027,0.026,0.026,0.026,0.026,0.026,0.026,0.026,0.026,0.025,0.025,0.025,
                    0.025,0.025,0.025,0.025,0.024,0.024,0.024,0.024,0.024,0.024,0.024,0.024,0.024,0.023,0.023,0.023,0.023,0.023,0.023,0.023,0.023,0.022,0.022,0.022,
                    0.022,0.022,0.022,0.022,0.022,0.022,0.021,0.021,0.021,0.021,0.021,0.021,0.021,0.021,0.021,0.021,0.020,0.020,0.020,0.020,0.020,0.020,0.020,0.020,
                    0.020,0.020,0.019,0.019,0.019,0.019,0.019,0.019,0.019,0.019,0.019,0.019,0.018,0.018,0.018,0.018,0.018,0.018,0.018,0.018,0.018,0.018,0.018,0.017,
                    0.017,0.017,0.017,0.017,0.017,0.017,0.017,0.017,0.017,0.017,0.017,0.016,0.016,0.016,0.016,0.016,0.016,0.016,0.016,0.016,0.016,0.016,0.016,0.015,
                    0.015,0.015,0.015,0.015,0.015,0.015,0.015,0.015,0.015,0.015,0.015,0.015,0.015,0.014,0.014
                } },

                { new Component("B", "B"),  new List<double> {0.000,0.066,0.105,0.127,0.140,0.147,0.151,0.153,0.154,0.154,0.154,0.154,0.153,0.152,0.152,0.151,0.150,
                    0.149,0.149,0.148,0.147,0.146,0.146,0.145,0.144,0.144,0.143,0.142,0.141,0.141,0.140,0.139,0.139,0.138,0.137,0.137,0.136,0.135,0.134,0.134,0.133,
                    0.132,0.132,0.131,0.130,0.130,0.129,0.129,0.128,0.127,0.127,0.126,0.125,0.125,0.124,0.123,0.123,0.122,0.122,0.121,0.120,0.120,0.119,0.119,0.118,
                    0.117,0.117,0.116,0.116,0.115,0.114,0.114,0.113,0.113,0.112,0.112,0.111,0.111,0.110,0.109,0.109,0.108,0.108,0.107,0.107,0.106,0.106,0.105,0.105,
                    0.104,0.104,0.103,0.102,0.102,0.101,0.101,0.100,0.100,0.099,0.099,0.098,0.098,0.097,0.097,0.096,0.096,0.096,0.095,0.095,0.094,0.094,0.093,0.093,
                    0.092,0.092,0.091,0.091,0.090,0.090,0.089,0.089,0.089,0.088,0.088,0.087,0.087,0.086,0.086,0.085,0.085,0.085,0.084,0.084,0.083,0.083,0.083,0.082,
                    0.082,0.081,0.081,0.080,0.080,0.080,0.079,0.079,0.078,0.078,0.078,0.077,0.077,0.077,0.076,0.076,0.075,0.075,0.075,0.074,0.074,0.074,0.073,0.073,
                    0.072,0.072,0.072,0.071,0.071,0.071,0.070,0.070,0.070,0.069,0.069,0.069,0.068,0.068,0.067,0.067,0.067,0.066,0.066,0.066,0.065,0.065,0.065,0.065,
                    0.064,0.064,0.064,0.063,0.063,0.063,0.062,0.062,0.062,0.061,0.061,0.061,0.060,0.060,0.060
                } },

                { new Component("C", "C"),  new List<double> {0.000,0.000,0.001,0.001,0.002,0.003,0.004,0.005,0.006,0.007,0.008,0.009,0.010,0.011,0.012,0.013,0.014,
                    0.015,0.016,0.016,0.017,0.018,0.019,0.020,0.021,0.022,0.023,0.024,0.025,0.025,0.026,0.027,0.028,0.029,0.030,0.031,0.032,0.032,0.033,0.034,0.035,
                    0.036,0.037,0.037,0.038,0.039,0.040,0.041,0.041,0.042,0.043,0.044,0.045,0.045,0.046,0.047,0.048,0.048,0.049,0.050,0.051,0.051,0.052,0.053,0.054,
                    0.054,0.055,0.056,0.057,0.057,0.058,0.059,0.059,0.060,0.061,0.062,0.062,0.063,0.064,0.064,0.065,0.066,0.066,0.067,0.068,0.068,0.069,0.070,0.070,
                    0.071,0.072,0.072,0.073,0.074,0.074,0.075,0.075,0.076,0.077,0.077,0.078,0.079,0.079,0.080,0.080,0.081,0.082,0.082,0.083,0.083,0.084,0.084,0.085,
                    0.086,0.086,0.087,0.087,0.088,0.088,0.089,0.090,0.090,0.091,0.091,0.092,0.092,0.093,0.093,0.094,0.094,0.095,0.096,0.096,0.097,0.097,0.098,0.098,
                    0.099,0.099,0.100,0.100,0.101,0.101,0.102,0.102,0.103,0.103,0.104,0.104,0.105,0.105,0.106,0.106,0.106,0.107,0.107,0.108,0.108,0.109,0.109,0.110,
                    0.110,0.111,0.111,0.112,0.112,0.112,0.113,0.113,0.114,0.114,0.115,0.115,0.115,0.116,0.116,0.117,0.117,0.118,0.118,0.118,0.119,0.119,0.120,0.120,
                    0.120,0.121,0.121,0.122,0.122,0.122,0.123,0.123,0.124,0.124,0.124,0.125,0.125,0.125,0.126
                } }
            };

            testreaction.InitialConcentration = new Dictionary<Component, double>(new Component.EqualityComparer());
            testreaction.InitialConcentration.Add(components.Single(x => x.Name == "A"), 0.2);
            testreaction.InitialConcentration.Add(components.Single(x => x.Name == "B"), 0.0);
            testreaction.InitialConcentration.Add(components.Single(x => x.Name == "C"), 0.0);
            testreaction.ResultTimestep = 5;
            testreaction.ReactionTemperature = 323;

            //Act
            testreaction.Solve();

            //Assert
            foreach (var component in components)
            {
                var counter = 0;
                foreach (var concentration in testreaction.ResultConcentration[component])
                {
                    Assert.AreEqual(reference[component][counter], concentration, 0.01);
                    counter++;
                }
            }

        }

        private BatchProblemNoDiffusion CreateExampleOne()
        {
            //components
            var componentList = new List<Component>();
            componentList.Add(new Component("A", "A"));
            componentList.Add(new Component("B", "B"));

            // define partial reactions
            var ListReaction1LHS = new List<ReactionElement>();
            var ListReaction1RHS = new List<ReactionElement>();
            ListReaction1LHS.Add(new ReactionElement(componentList[0], 1));
            ListReaction1RHS.Add(new ReactionElement(componentList[1], 1));

            var Reaction1 = new ElementaryReaction(ListReaction1LHS, ListReaction1RHS, 250000, 50000, 125000, 52000);

            //make globalreaction
            List<ElementaryReaction> reactionList = new List<ElementaryReaction> { Reaction1 };
            var GlobalReaction = new GlobalReaction(reactionList);

            var returnproblem = new BatchProblemNoDiffusion(1000.0, 10, GlobalReaction);
            returnproblem.InitialConcentration = new Dictionary<Component, double>(new Component.EqualityComparer());

            return returnproblem;

        }

        private BatchProblemNoDiffusion CreateExampleTwo()
        {
            //components
            var componentList = new List<Component>();
            componentList.Add(new Component("A", "A"));
            componentList.Add(new Component("B", "B"));
            componentList.Add(new Component("C", "C"));

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

        private List<Component> MakeComponents()
        {
            var componentList = new List<Component>();
            componentList.Add(new Component("A", "A"));
            componentList.Add(new Component("B", "B"));
            componentList.Add(new Component("C", "C"));

            return componentList;
        }
    }
}
