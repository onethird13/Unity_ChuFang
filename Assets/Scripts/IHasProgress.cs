using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IHasProgress
{
    public event EventHandler<OnProgressChangeArgs> onProgressChange;
   

    public class OnProgressChangeArgs : EventArgs
    {
        public float progressNormalized;
        
    }
}
