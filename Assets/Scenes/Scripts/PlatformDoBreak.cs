using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDoBreak : Platform
{
    public override float OnLand()
    {
        Destroy(this);
        return jumpHeight;
    }
}
