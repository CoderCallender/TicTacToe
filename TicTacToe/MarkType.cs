using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    /// <summary>
    /// the type of value a cell in the game currently has 
    /// </summary>
    public enum MarkType
    {
        /// <summary>
        /// the cell hasn't been clicke yet
        /// </summary>
        free,

        /// <summary>
        /// The cell is a 'o'
        /// </summary>
        Nought,

        /// <summary>
        /// the cell is an x
        /// </summary>
        Cross
    }
}
