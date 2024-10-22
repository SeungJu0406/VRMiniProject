using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Pool 
{
    public static SocketPool Socket { get { return SocketPool.Instance; } }
}
