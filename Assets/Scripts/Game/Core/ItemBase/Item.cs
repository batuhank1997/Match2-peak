using Game.Core.BoardBase;
using Game.Core.Enums;
using Game.Items;
using Game.Mechanics;
using UnityEngine;

namespace Game.Core.ItemBase
{
    public abstract class Item : MonoBehaviour
    {
        private const int BaseSortingOrder = 10;
        
        private bool canFall = true;
        private bool canAffectedByNeighbors = false;
        private bool isExplodedByTouch = false;

        public SpriteRenderer SpriteRenderer;
        public FallAnimation FallAnimation;

        private int _childSpriteOrder;

        private Cell _cell;

        public Cell Cell
        {
            get { return _cell; }
            set
            {
                if (_cell == value) return;

                var oldCell = _cell;
                _cell = value;

                if (oldCell != null && oldCell.Item == this)
                {
                    oldCell.Item = null;
                }

                if (value != null)
                {
                    value.Item = this;
                    gameObject.name = _cell.gameObject.name + " " + GetType().Name;
                }

            }
        }

        public void Prepare(ItemBase itemBase, Sprite sprite)
        {
            SpriteRenderer = AddSprite(sprite);
            FallAnimation = itemBase.FallAnimation;
            FallAnimation.Item = this;
        }

        public SpriteRenderer AddSprite(Sprite sprite)
        {
            var spriteRenderer = new GameObject("Sprite_" + _childSpriteOrder).AddComponent<SpriteRenderer>();
            spriteRenderer.transform.SetParent(transform);
            spriteRenderer.sprite = sprite;
            spriteRenderer.sortingLayerID = SortingLayer.NameToID("Item");
            spriteRenderer.sortingOrder = BaseSortingOrder + _childSpriteOrder++;

            return spriteRenderer;
        }

        public void RemoveSprite(SpriteRenderer spriteRenderer)
        {
            if (spriteRenderer == SpriteRenderer)
            {
                SpriteRenderer = null;
            }

            Destroy(spriteRenderer.gameObject);
        }

        public virtual MatchType GetMatchType()
        {
            return MatchType.None;
        }

        public void SetFallAbility(bool ability)
        {
            canFall = ability;
        }
        
        public bool GetFallAbility()
        {
            return canFall;
        }
        
        public void SetCanAffectedByNeighbors(bool canAffected)
        {
            canAffectedByNeighbors = canAffected;
        }
        
        public bool GetCanAffectedByNeighbors()
        {
            return canAffectedByNeighbors;
        }

        public bool IsExplodedByTouch()
        {
            return isExplodedByTouch;
        }

        public void SetIsExplodedByTouch(bool _isBomb)
        {
            isExplodedByTouch = _isBomb;
        }
        
        public bool IsFalling()
        {
            return FallAnimation.IsFalling;
        }

        public void Fall()
        {
            FallAnimation.FallTo(Cell.GetFallTarget());				
        }
        
        public virtual void TryExecute()
        {
            RemoveItem();
        }

        public void RemoveItem()
        {
            Cell.Item = null;
            Cell = null;

            Destroy(gameObject);
        }

        public override string ToString()
        {
            return gameObject.name;
        }

        public virtual void HintBehaviourOn(bool isRocket = false)
        {
            
        }
        
        public virtual void HintBehaviourOff()
        {
            
        }
    }
}