using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOL.BL
{
    /// <summary>
    /// Class that simulates Conway's Game of life
    /// </summary>
    public class Simulator
    {
        private List<Cell> _cells;
        private bool[,] _seedArray;

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="seedArray">Multidimensional bool array where live cell is represented by a true value and a dead cell by false</param>
        public Simulator(bool[,] seedArray)
        {
            if (seedArray != null && seedArray.Length > 0)
            {
                _seedArray = seedArray;
                // Its easier to operate over a strongly typed list of cells for getting neighbors and then determining which cells to kill and which to awake
                _cells = new List<Cell>(seedArray.Length);
                for (int x = 0; x < seedArray.GetLength(0); x++)
                {
                    for (int y = 0; y < seedArray.GetLength(1); y++)
                    {
                        _cells.Add(new Cell(x, y, seedArray[x, y]));
                    }
                }
            }
            else
                throw new ArgumentNullException("Seed is null or is has no cells");
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="seed">String representation of live cells. eg. 1,2|2,3|3,3</param>
        public Simulator(string seed)
            : this(Simulator.GetSeedForString(seed))
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pattern">Common patterns in simulation</param>
        public Simulator(CommonPattern pattern)
            : this(Simulator.GetSeedForPattern(pattern))
        {

        }

        #endregion

        #region Public methods

        /// <summary>
        /// Steps into next generation. 
        /// </summary>
        public void Step()
        {
            // Get Cells to Kill
            var cellsToKill = _cells.Where(ShouldDie).ToList();
            // Get Cells that should be brought back to life
            var cellsToAwake = _cells.Where(ShouldAwake).ToList();
            // Now awake cells to awake
            cellsToAwake.ForEach(c => { c.IsAlive = true; _seedArray[c.XPos, c.YPos] = true; });
            // Now kill cells to kill
            cellsToKill.ForEach(c => { c.IsAlive = false; _seedArray[c.XPos, c.YPos] = false; });
        }

        /// <summary>
        /// Gets current generation of living and dead cells
        /// </summary>
        public bool[,] GetCurrentGeneration() 
        { 
            // We are returning a clone as C# does not have readonly array and 
            //we do not want to corrupt the original seedArray which represents the current generation for outside world. 
            return (bool[,])_seedArray.Clone(); 
        }

        #endregion

        #region Private Static methods

        /// <summary>
        /// Converts string seed to a 2 dimensional bool array
        /// </summary>
        /// <param name="seed">seed in string format. eg 1,2|1,3|4,6</param>
        /// <returns>seed in 2 dimensional bool array</returns>
        private static bool[,] GetSeedForString(string seed)
        {
            bool[,] seedArray = null;
            
                if (!string.IsNullOrEmpty(seed))
                {
                    try
                    {
                        var strLiveCells = seed.Split('|');
                        List<Cell> liveCells = new List<Cell>();
                        foreach (var strCell in strLiveCells)
                        {
                            var strCordinates = strCell.Split(',');
                            int xPos = Convert.ToInt32(strCordinates[0]);
                            int yPos = Convert.ToInt32(strCordinates[1]);
                            liveCells.Add(new Cell(xPos, yPos, true));
                        }
                        int maxX = liveCells.Max(c => c.XPos) + 2;
                        int maxY = liveCells.Max(c => c.YPos) + 2;
                        seedArray = new bool[maxX, maxY];
                        liveCells.ForEach(c => seedArray[c.XPos, c.YPos] = true);
                    }
                    catch (Exception ex) { throw new ArgumentException("Invalid seed string", ex);  }
                }
                else
                {
                    throw new ArgumentNullException("Empty Seed");
                }            
            
            return seedArray;
        }

        /// <summary>
        /// Gets seed in 2 dimensional bool array form for common patterns
        /// </summary>
        /// <param name="pattern">Common pattern</param>
        /// <returns>Seed for the pattern as 2 dimensional bool array</returns>
        private static bool[,] GetSeedForPattern(CommonPattern pattern)
        {
                       
            bool[,] seed;
            switch (pattern)
            {
                case CommonPattern.Blinker:
                    seed = new bool[5, 5];
                    seed[1, 2] = true;
                    seed[2, 2] = true;
                    seed[3, 2] = true;
                    break;
                case CommonPattern.Toad:
                    seed = new bool[6, 6];
                    seed[2, 2] = true;
                    seed[3, 2] = true;
                    seed[4, 2] = true;
                    seed[1, 3] = true;
                    seed[2, 3] = true;
                    seed[3, 3] = true;
                    break;
                case CommonPattern.Beacon:
                    seed = new bool[6, 6];
                    seed[1, 1] = true;
                    seed[2, 1] = true;
                    seed[1, 2] = true;
                    seed[4, 3] = true;
                    seed[3, 4] = true;
                    seed[4, 4] = true;
                    break;
                case CommonPattern.Pulsar:
                    seed = new bool[17, 17];
                    seed[4, 2] = true;
                    seed[5, 2] = true;
                    seed[6, 2] = true;
                    seed[10, 2] = true;
                    seed[11, 2] = true;
                    seed[12, 2] = true;
                    seed[4, 7] = true;
                    seed[5, 7] = true;
                    seed[6, 7] = true;
                    seed[10, 7] = true;
                    seed[11, 7] = true;
                    seed[12, 7] = true;
                    seed[4, 9] = true;
                    seed[5, 9] = true;
                    seed[6, 9] = true;
                    seed[10, 9] = true;
                    seed[11, 9] = true;
                    seed[12, 9] = true;
                    seed[4, 14] = true;
                    seed[5, 14] = true;
                    seed[6, 14] = true;
                    seed[10, 14] = true;
                    seed[11, 14] = true;
                    seed[12, 14] = true;

                    seed[2, 4] = true;
                    seed[2, 5] = true;
                    seed[2, 6] = true;
                    seed[2, 10] = true;
                    seed[2, 11] = true;
                    seed[2, 12] = true;

                    seed[7, 4] = true;
                    seed[7, 5] = true;
                    seed[7, 6] = true;
                    seed[7, 10] = true;
                    seed[7, 11] = true;
                    seed[7, 12] = true;


                    seed[9, 4] = true;
                    seed[9, 5] = true;
                    seed[9, 6] = true;
                    seed[9, 10] = true;
                    seed[9, 11] = true;
                    seed[9, 12] = true;

                    seed[14, 4] = true;
                    seed[14, 5] = true;
                    seed[14, 6] = true;
                    seed[14, 10] = true;
                    seed[14, 11] = true;
                    seed[14, 12] = true;


                    break;
                default:
                    seed = new bool[6, 6];
                    break;

            }
            return seed;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Check if a cell should die
        /// </summary>
        /// <param name="cell">Cell</param>
        /// <returns>true if cell should die,else false if cell should live</returns>
        private bool ShouldDie(Cell cell)
        {
            if (!cell.IsAlive)
                return false;
            var noOfLiveNeighbours = GetCellNeighbours(cell).Count(n => n.IsAlive);
            return (noOfLiveNeighbours < 2 || noOfLiveNeighbours > 3);
        }

        /// <summary>
        /// Check if cell should come to life
        /// </summary>
        /// <param name="cell">Cell</param>
        /// <returns>true if cell should come to life, else false if cell should remain dead</returns>
        private bool ShouldAwake(Cell cell)
        {
            if (cell.IsAlive)
                return false;
            var noOfLiveNeighbours = GetCellNeighbours(cell).Count(n => n.IsAlive);
            return (noOfLiveNeighbours == 3);
        }

        /// <summary>
        /// Gets all neighbours of a given cell
        /// </summary>
        /// <param name="cell">Cell</param>
        /// <returns>All neighbors of a cell</returns>
        private IEnumerable<Cell> GetCellNeighbours(Cell cell)
        {
            return _cells.Where(c => AreNeighbours(c, cell));
        }

        /// <summary>
        /// Checks if 2 cells are neighbours
        /// </summary>
        /// <param name="cell1">Cell 1</param>
        /// <param name="cell2">Cell 2</param>
        /// <returns>True if cells are neighbours, else false</returns>
        private bool AreNeighbours(Cell cell1, Cell cell2)
        {
            if (cell1 == cell2)
                return false;
            if (Math.Abs(cell1.XPos - cell2.XPos) > 1)
                return false;
            if (Math.Abs(cell1.YPos - cell2.YPos) > 1)
                return false;
            return true;
        }
        #endregion
    }
}
