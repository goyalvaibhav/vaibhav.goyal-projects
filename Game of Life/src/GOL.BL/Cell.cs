using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOL.BL
{
    /// <summary>
    /// Represents a live or dead cell in the simulation
    /// </summary>
    internal class Cell
    {
        
        /// <summary>
        ///Gets X position of a cell
        /// </summary>
        public int XPos { get; private set; }
       
        /// <summary>
        ///Gets Y position of a cell
        /// </summary>
        public int YPos { get; private set; }
       
        /// <summary>
        ///Gets or Sets cells status. true if cell is alive, false if cell is dead
        /// </summary>
        public bool IsAlive { get; set; }
        
        public Cell(int xPos, int yPos, bool isAlive) 
        {
            XPos = xPos;
            YPos = yPos;
            IsAlive = isAlive;
        }
    }
}
