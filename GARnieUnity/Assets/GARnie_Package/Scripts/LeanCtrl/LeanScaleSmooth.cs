using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Lean.Touch
{
    /// <summary>
    /// Custom LeanScale class 
    /// To use Scale
    /// </summary>
    public class LeanScaleSmooth : Lean.Touch.LeanScale
    {
        /// <summary>
        /// don't need to select model to scale
        /// </summary>
        protected override void Start()
        {
            RequiredSelectable = null;
        }
    }
}