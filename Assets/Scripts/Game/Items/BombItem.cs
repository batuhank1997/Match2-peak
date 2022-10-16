using System.Collections;
using System.Collections.Generic;
using Game.Core.BoardBase;
using Game.Core.Enums;
using Game.Core.ItemBase;
using Game.Managers;
using Unity.VisualScripting;
using UnityEngine;

public class BombItem : Item
{
    private ParticleSystem particles;
    private bool isAlreadyExploded;
    
    public void PrepareBombItem(ItemBase itemBase)
    {
        var bombSprite = ServiceProvider.GetImageLibrary.BombSprite;

        SetIsExplodedByTouch(true);
        Prepare(itemBase, bombSprite);
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
        
        Cell.UpdateBombItemNeighbours(Cell.Board);
        
        Cell.Neighbours.ForEach(neigbor =>
        {
            if (neigbor.Item != null)
                neigbor.Item.TryExecute();

        });

        base.TryExecute();
        // Cell.Neighbours.Clear();
    }
}
