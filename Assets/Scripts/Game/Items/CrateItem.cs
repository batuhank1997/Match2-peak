using Game.Core.Enums;
using Game.Core.ItemBase;
using Game.Managers;
using UnityEngine;

namespace Game.Items
{
    public class CrateItem : SpecialItem
    {
        private int health = 2;

        public override void PrepareSpecialItem(ItemBase itemBase, MatchType matchType)
        {
            base.PrepareSpecialItem(itemBase, matchType);
            matchType = MatchType.None;
            var crateLayer2Sprite = ServiceProvider.GetImageLibrary.CrateLayer2Sprite;
            Prepare(itemBase, crateLayer2Sprite);
            SetFallAbility(false);
        }

        public override void TryExecute()
        {
            if (GetHealth() != 1)
            {
                DecreaseHealth(1);
                ChangeImage();
                return;
            }

            base.TryExecute();
        }

        int GetHealth()
        {
            return health;
        }

        void DecreaseHealth(int decreaseAmount)
        {
            health -= decreaseAmount;
        }

        void ChangeImage()
        {
            SpriteRenderer.sprite = ServiceProvider.GetImageLibrary.CrateLayer1Sprite;
        }
    }
}