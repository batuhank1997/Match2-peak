using System.Collections;
using System.Collections.Generic;
using Game.Core.BoardBase;
using Game.Core.Enums;
using Game.Core.ItemBase;
using Game.Managers;
using UnityEngine;

public class HorizontalRocketItem : Item
{
    private ParticleSystem particles;
    private bool isAlreadyExploded;

    public void PrepareHorizontalRocketItem(ItemBase itemBase)
    {
        var rocketHorizontalSprite = ServiceProvider.GetImageLibrary.RocketHorizontal;

        SetIsExplodedByTouch(true);
        Prepare(itemBase, rocketHorizontalSprite);
    }
    
    public override MatchType GetMatchType()
    {
        return MatchType.Special;
    }
    
    public override void HintBehaviourOn(bool isRocket)
    {
        if (!particles)
            particles = ServiceProvider.GetParticleManager.PlayComboParticleOnItem(this);
    }
    
    public override void HintBehaviourOff()
    {
        if (particles)
        {
            ServiceProvider.GetParticleManager.StopParticle(particles);
        }
    }
    
    public override void TryExecute()
    {
        if (isAlreadyExploded)
            return;
        
        isAlreadyExploded = true;
        
        for (int i = 0; i < Board.Rows ; i++)
        {
            if (Cell.Board.Cells[i, Cell.Y].HasItem())
                Cell.Board.Cells[i, Cell.Y].Item.TryExecute();
        }
        
        base.TryExecute();
    }
}
