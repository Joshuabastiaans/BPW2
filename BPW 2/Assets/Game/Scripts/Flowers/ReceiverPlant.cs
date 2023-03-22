using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiverPlant : Flower
{
    public override void OnRecieveSignal()
    {
        base.OnRecieveSignal();
    }
    
    protected override void RotateFlower(float valueToChangeBy)
    {
        
    }
}
