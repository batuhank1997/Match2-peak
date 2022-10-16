using System.Collections;
using System.Collections.Generic;
using Game.Core.BoardBase;
using Game.Core.Enums;
using Game.Core.ItemBase;
using Game.Managers;
using UnityEngine;

public class VerticalRocketItem : Item
{
    private ParticleSystem particles;
    private bool isAlreadyExploded;

    public void PrepareVerticalRocketItem(ItemBase itemBase)
    {
        var rocketVerticalSprite = ServiceProvider.GetImageLibrary.RocketVertical;

        SetIsExplodedByTouch(true);
        Prepare(itemBase, rocketVerticalSprite);
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

        for (int i = 0; i < Board.Cols ; i++)
        {
            if (Cell.Board.Cells[Cell.X, i].HasItem())
                Cell.Board.Cells[Cell.X, i].Item.TryExecute();
        }
        
        base.TryExecute();
    }
}
