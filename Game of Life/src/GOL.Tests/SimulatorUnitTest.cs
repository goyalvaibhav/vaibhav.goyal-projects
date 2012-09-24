using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GOL.BL;

namespace GOL.Tests
{
    [TestClass]
    public class SimulatorUnitTest
    {
        [TestMethod]
        public void PatternConstructorPositive()
        {
            Simulator simulator;
            bool[,] generation;
            simulator = new Simulator(CommonPattern.Blinker);
            generation = simulator.GetCurrentGeneration();
            Assert.IsNotNull(generation, "Generation is null");
            Assert.IsTrue((generation.GetLength(0) == 5) && (generation.GetLength(1) == 5), "Rows and Columns lengths is not 5");
            Assert.IsTrue(generation[1, 2] && generation[2, 2] && generation[3, 2], "First generation does not have right live cells");            
        }


        [TestMethod]
        public void BoolConstructorPositive() 
        {
            Simulator simulator;
            bool[,] generation;
            bool[,] seed = new bool[2, 2];
            seed[1, 1] = true;
            simulator = new Simulator(seed);
            generation = simulator.GetCurrentGeneration();
            Assert.IsNotNull(generation, "Generation is null");
            Assert.IsTrue((generation.GetLength(0) == seed.GetLength(0)) && (generation.GetLength(1) == seed.GetLength(1)) , "generation and seed do not have same dimension");            
            for (int x = 0; x < seed.GetLength(0); x++)
            {
                for (int y = 0; y < seed.GetLength(1); y++)
                {
                    if (seed[x, y] != generation[x, y])
                    {
                        Assert.Fail("Generation and seed cell are nor equal");
                        break;
                    }
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Argument null exception not thrown, when seed is null")]
        public void BoolConstructorNegative1() 
        {
            Simulator simulator;
            bool[,] seed = null;            
            simulator = new Simulator(seed);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Argument null exception not thrown, when seed has zero elements")]
        public void BoolConstructorNegative2()
        {
            Simulator simulator;
            bool[,] seed = new bool[,]{};
            simulator = new Simulator(seed);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Argument null exception not thrown, when seed is null")]
        public void StringConstructorNegative1() 
        {
            Simulator simulator;
            string seed = null;
            simulator = new Simulator(seed);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Argument exception not thrown, when seed is invalid")]
        public void StringConstructorNegative2()
        {
            Simulator simulator;
            string seed = "ABCD,BAAA|AA";
            simulator = new Simulator(seed);
        }

        [TestMethod]        
        public void StringConstructorPositive()
        {
            Simulator simulator;
            string seed = "1,1|2,2|3,3";
            simulator = new Simulator(seed);
            var generation = simulator.GetCurrentGeneration();
            Assert.IsNotNull(generation, "Generation is null");
            Assert.IsTrue(generation.GetLength(0) == 5 && generation.GetLength(1) == 5,"Invalid dimensions for first genearation array");
            Assert.IsTrue(generation[1,1] && generation[2,2] && generation[3,3],"First genration has incorrect live cells");
        }

        [TestMethod]
        public void StepPositive() 
        {
            Simulator simulator;
            CommonPattern seedPattern = CommonPattern.Pulsar;
            simulator = new Simulator(seedPattern);
            var firstGeneration = simulator.GetCurrentGeneration();
            simulator.Step();
            simulator.Step();
            simulator.Step();
            var thirdGeneration = simulator.GetCurrentGeneration();
            Assert.IsTrue((firstGeneration.GetLength(0) == thirdGeneration.GetLength(0)) && (firstGeneration.GetLength(1) == thirdGeneration.GetLength(1)), "First and third generations dimension are not equal");
            for (int x = 0; x < firstGeneration.GetLength(0); x++)
            {
                for (int y = 0; y < firstGeneration.GetLength(1); y++)
                {
                    if (firstGeneration[x, y] != thirdGeneration[x, y])
                    {
                        Assert.Fail("First Generation and Third Generation are not same.");
                        break;
                    }
                }
            }
        }

        
    }
}
