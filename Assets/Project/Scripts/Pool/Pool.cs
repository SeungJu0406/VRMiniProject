using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Pool 
{
    public static StackPool Stack { get { return StackPool.Instance; } }
}
