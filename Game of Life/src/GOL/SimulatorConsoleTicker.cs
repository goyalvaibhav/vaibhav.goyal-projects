using GOL.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GOL
{
    /// <summary>
    /// Class used to print the simualtion results after every specified interval
    /// </summary>
    class SimulatorConsoleTicker
    {
        private Simulator _simulator;
        private int _tickDuration;

        #region Contructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pattern">Common Pattern</param>
        /// <param name="tickDuration">Frequency of genration in milliseconds</param>
        public SimulatorConsoleTicker(CommonPattern pattern, int tickDuration)
        {
            _simulator = new Simulator(pattern);
            _tickDuration = tickDuration;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="seed">Seed in 2 dimensional bool array form</param>
        /// <param name="tickDuration">Frequency of genration in milliseconds</param>
        public SimulatorConsoleTicker(bool[,] seed, int tickDuration)
        {
            _simulator = new Simulator(seed);
            _tickDuration = tickDuration;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userInput">Seed in string form</param>
        /// <param name="tickDuration">Frequency of genration in milliseconds</param>
        public SimulatorConsoleTicker(string userInput, int tickDuration)
        {
            _simulator = new Simulator(userInput);
            _tickDuration = tickDuration;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Starts the simulation
        /// </summary>
        public void Start()
        {
            PrintAndSteptoNext();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Prints current generation, steps to next generation and sleeps the thread for tick duration before running the same steps recursively
        /// </summary>
        private void PrintAndSteptoNext()
        {           
            PrintCurrentGeneration();
            _simulator.Step();

            Thread.Sleep(_tickDuration);
            PrintAndSteptoNext();
        }

        /// <summary>
        /// Clear console and Print current generation
        /// </summary>
        private void PrintCurrentGeneration()
        {
            Console.Clear();
            var currentGeneration = _simulator.GetCurrentGeneration();
            for (int y = 0; y < currentGeneration.GetLength(1); y++)
            {
                for (int x = 0; x < currentGeneration.GetLength(0); x++)
                {
                    if (currentGeneration[x, y])
                        Console.Write('*');
                    else
                        Console.Write(' ');
                }
                Console.WriteLine();
            }
        }
        #endregion

    }
}
