using System.Collections;
using System.Collections.Generic;
using Game.Core.Enums;
using Game.Core.ItemBase;
using Game.Managers;
using UnityEngine;

public class BalloonItem : SpecialItem
{
    public override void PrepareSpecialItem(ItemBase itemBase, MatchType matchType)
    {
        base.PrepareSpecialItem(itemBase, matchType);
        matchType = MatchType.None;
        var balloonSprite = ServiceProvider.GetImageLibrary.BalloonSprite;
        Prepare(itemBase, balloonSprite);
    }
}
