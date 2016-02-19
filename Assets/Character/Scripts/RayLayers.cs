using UnityEngine;
using System.Collections;

public static class RayLayers {
    public static readonly int allRays;
    public static readonly int upRay;
    public static readonly int downRay;
    public static readonly int diagonalRays;
    
    static RayLayers()
    {
        allRays = 1 << LayerMask.NameToLayer("Collisions")
            | 1 << LayerMask.NameToLayer("SoftTop")
            | 1 << LayerMask.NameToLayer("SoftBottom")
            | 1 << LayerMask.NameToLayer("Two-Way");
        upRay = 1 << LayerMask.NameToLayer("Collisions")
            | 1 << LayerMask.NameToLayer("SoftTop");
        downRay = 1 << LayerMask.NameToLayer("Collisions")
            | 1 << LayerMask.NameToLayer("SoftTop")
            | 1 << LayerMask.NameToLayer("SoftBottom")
            | 1 << LayerMask.NameToLayer("Two-Way");
        diagonalRays = 1 << LayerMask.NameToLayer("Collisions");
    }  
}
