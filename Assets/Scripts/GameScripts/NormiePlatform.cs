using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormiePlatform : Platform
{
    public override float OnLand()
    {
        return jumpHeight;
    }
} 
