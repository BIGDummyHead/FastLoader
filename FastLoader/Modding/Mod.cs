using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static FastandLow.Modding.Utilities.ModUtilities;
using FastandLow.Modding.Events;
namespace FastandLow.Modding
{
    /// <summary>
    /// An Abstract class with virtual methods to be accessed by the Bootstrap
    /// </summary>
    public abstract class Mod
    {
        /// <summary>
        /// Load a Mod with Info on said mod - <see cref="ModInfo"/> <paramref name="info"/> is never null even when there is no Mod.mod File Included
        /// </summary>
        /// <param name="info"></param>
        public virtual void Load(ModInfo info)
        {

        }

        /// <summary>
        /// Unloads the mod
        /// </summary>
        public virtual void UnLoad()
        {
        }
    }
}
