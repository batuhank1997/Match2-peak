using Game.Core.Enums;
using Game.Core.ItemBase;
using Game.Managers;
using UnityEngine;

namespace Game.Items
{
    public class CubeItem : Item
    {
        private MatchType _matchType;

        public void PrepareCubeItem(ItemBase itemBase, MatchType matchType)
        {
            _matchType = matchType;
            Prepare(itemBase, GetSpritesForMatchType());
        }

        public Sprite GetSpritesForMatchType()
        {
            var imageLibrary = ServiceProvider.GetImageLibrary;
            
            switch (_matchType)
            {
                case MatchType.Green:
                    return imageLibrary.GreenCubeSprite;
                case MatchType.Yellow:
                    return imageLibrary.YellowCubeSprite;
                case MatchType.Blue:
                    return imageLibrary.BlueCubeSprite;
                case MatchType.Red:
                    return imageLibrary.RedCubeSprite;
            }

            return null;
        }
        
        public Sprite ChangeSpriteByBombMatchType()
        {
            var imageLibrary = ServiceProvider.GetImageLibrary;
            
            switch (_matchType)
            {
                case MatchType.Green:
                    return imageLibrary.GreenCubeBombHintSprite;
                case MatchType.Yellow:
                    return imageLibrary.YellowCubeBombHintSprite;
                case MatchType.Blue:
                    return imageLibrary.BlueCubeBombHintSprite;
                case MatchType.Red:
                    return imageLibrary.RedCubeBombHintSprite;
            }

            return null;
        }
        
        public Sprite ChangeSpriteByRocketMatchType()
        {
            var imageLibrary = ServiceProvider.GetImageLibrary;
            
            switch (_matchType)
            {
                case MatchType.Green:
                    return imageLibrary.GreenCubeRocketHintSprite;
                case MatchType.Yellow:
                    return imageLibrary.YellowCubeRocketHintSprite;
                case MatchType.Blue:
                    return imageLibrary.BlueCubeRocketHintSprite;
                case MatchType.Red:
                    return imageLibrary.RedCubeRocketHintSprite;
            }

            return null;
        }

        public override void HintBehaviourOn(bool isRocket)
        {
            if (isRocket)
                SpriteRenderer.sprite =  ChangeSpriteByRocketMatchType();
            else
                SpriteRenderer.sprite =  ChangeSpriteByBombMatchType();
        }
        
        public override void HintBehaviourOff()
        {
            SpriteRenderer.sprite = GetSpritesForMatchType();
        }

        public override MatchType GetMatchType()
        {
            return _matchType;
        }

        public override void TryExecute()
        {
            ServiceProvider.GetParticleManager.PlayCubeParticle(this);
            
            base.TryExecute();
        }
    }
}
