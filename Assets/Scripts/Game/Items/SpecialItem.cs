using System.Collections;
using System.Collections.Generic;
using Game.Core.Enums;
using Game.Core.ItemBase;
using Game.Managers;
using UnityEngine;

public class SpecialItem : Item
{
    public virtual void PrepareSpecialItem(ItemBase itemBase, MatchType matchType)
    {
        SetCanAffectedByNeighbors(true);
    }
        
    public override void TryExecute()
    {
        ServiceProvider.GetParticleManager.PlayCubeParticle(this);
        
        base.TryExecute();
    }
}
