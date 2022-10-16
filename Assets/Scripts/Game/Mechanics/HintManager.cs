using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Game.Core.BoardBase;
using Game.Core.Enums;
using Game.Items;
using UnityEngine;

namespace Game.Mechanics
{
    public class HintManager : MonoBehaviour
    {
        [SerializeField] private Board Board;
        private readonly MatchFinder _matchFinder = new MatchFinder();

        private int specialHintCondition = 2;
        private int bombHintCondition = 7;
        private int rocketHintCondition = 5;
        
        private void Update()
        {
            for (int i = 0; i < Board.Rows; i++)
            {
                for (int j = 0; j < Board.Cols; j++)
                {
                    if (!Board.Cells[i, j].HasItem() || Board.Cells[i, j].Item.GetMatchType() == MatchType.None)
                        continue;

                    if (Board.Cells[i, j].Item.GetMatchType() == MatchType.Special)
                    {
                        SpecialHint(i, j);
                    }
                    else
                    {
                        NormalHint(i, j);
                    }
                }
            }
        }

        void SpecialHint(int i, int j)
        {
            if (_matchFinder.FindMatches(Board.Cells[i, j], Board.Cells[i, j].Item.GetMatchType()).Count >= specialHintCondition)
                Board.Cells[i, j].Item.HintBehaviourOn();
            else
                Board.Cells[i, j].Item.HintBehaviourOff();
        }

        void NormalHint(int i, int j)
        {
            if (_matchFinder.FindMatches(Board.Cells[i, j], Board.Cells[i, j].Item.GetMatchType()).Count >= bombHintCondition)
            {
                Board.Cells[i, j].Item.HintBehaviourOn();
            }
            else if (_matchFinder.FindMatches(Board.Cells[i, j], Board.Cells[i, j].Item.GetMatchType()).Count >= rocketHintCondition)
            {
                Board.Cells[i, j].Item.HintBehaviourOn(true);
            }
            else
            {
                Board.Cells[i, j].Item.HintBehaviourOff();
            }
        }
        
    }
}