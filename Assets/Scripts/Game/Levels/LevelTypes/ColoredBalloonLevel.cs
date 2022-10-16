using System.Collections;
using System.Collections.Generic;
using Game.Core.Enums;
using UnityEngine;

public abstract class ColoredBalloonLevel : SpecialLevelType
{
    public override ItemType GetNextFillItemType()
    {
        if (Random.Range(0f, 10f) > 9f)
            return GetRandomBalloonItemType();
            
        return GetRandomCubeItemType();
    }
}
