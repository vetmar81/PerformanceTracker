using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vema.PerfTracker.Database.Helper
{
    /// <summary>
    /// Markus Vetsch, 14.02.2012 12:33
    /// Generic utility class to store a pair of object types.
    /// </summary>
    /// <typeparam name="L">The type of the left object.</typeparam>
    /// <typeparam name="R">The type of the right object.</typeparam>
    public class Pair<L, R>
    {
        /// <summary>
        /// Gets the left object.
        /// </summary>
        public L Left { get; private set; }

        /// <summary>
        /// Gets the right object.
        /// </summary>
        public R Right { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Pair&lt;L, R&gt;"/> class.
        /// </summary>
        /// <param name="left">The left object.</param>
        /// <param name="right">The right object.</param>
        public Pair(L left, R right)
        {
            Left = left;
            Right = right;
        }
    }
}
