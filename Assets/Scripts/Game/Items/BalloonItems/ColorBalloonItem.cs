using System.Collections;
using System.Collections.Generic;
using Game.Core.Enums;
using Game.Core.ItemBase;
using Game.Managers;
using UnityEngine;

public class ColorBalloonItem : SpecialItem
{
    private MatchType _matchType;
    
    public override void PrepareSpecialItem(ItemBase itemBase, MatchType matchType)
    {
        base.PrepareSpecialItem(itemBase, matchType);

        _matchType = matchType;
        Prepare(itemBase, GetSpritesForMatchType());
    }
    
    private Sprite GetSpritesForMatchType()
    {
        var imageLibrary = ServiceProvider.GetImageLibrary;
            
        switch (_matchType)
        {
            case MatchType.Green:
                return imageLibrary.GreenBalloonSprite;
            case MatchType.Yellow:
                return imageLibrary.YellowBalloonSprite;
            case MatchType.Blue:
                return imageLibrary.BlueBalloonSprite;
            case MatchType.Red:
                return imageLibrary.RedBalloonSprite;
        }

        return null;
    }
    
    public override MatchType GetMatchType()
    {
        return _matchType;
    }
}
