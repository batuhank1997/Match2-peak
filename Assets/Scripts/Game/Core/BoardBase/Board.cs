using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.Core.Enums;
using Game.Core.ItemBase;
using Game.Items;
using Game.Managers;
using Game.Mechanics;
using UnityEngine;
using Random = System.Random;

namespace Game.Core.BoardBase
{
    public class Board : MonoBehaviour
    {
        [SerializeField] private HintManager _hintManager;
        public const int Rows = 9;
        public const int Cols = 9;

        public const int MinimumMatchCount = 2;
        public const int MinimumSpecialMatchCount = 1;
        public const int MinimumCountForBombCreation = 7;
        public const int MinimumCountForRocketCreation = 5;
        public const int timeForWaitBeforeHint = 3;

        public Transform CellsParent;
        public Transform ItemsParent;
        public Transform ParticlesParent;

        [SerializeField] private Cell CellPrefab;

        public readonly Cell[,] Cells = new Cell[Cols, Rows];

        private readonly MatchFinder _matchFinder = new MatchFinder();
        private readonly ComboManager comboManager = new ComboManager();
        
        public void Prepare()
        {
            CreateCells();
            PrepareCells();
        }

        private void CreateCells()
        {
            for (var y = 0; y < Rows; y++)
            {
                for (var x = 0; x < Cols; x++)
                {
                    var cell = Instantiate(CellPrefab, Vector3.zero, Quaternion.identity, CellsParent);
                    Cells[x, y] = cell;
                }
            }
        }

        private void PrepareCells()
        {
            for (var y = 0; y < Rows; y++)
            {
                for (var x = 0; x < Cols; x++)
                {
                    Cells[x, y].Prepare(x, y, this);
                }
            }
        }

        public void CellTapped(Cell cell)
        {
            if (cell == null) return;

            if (!cell.HasItem()) return;

            if (cell.Item.GetMatchType() == MatchType.Special)
                ExplodeSpecialItems(cell);
            else
                ExplodeMatchingCells(cell);
            
        }

        private void ExplodeSpecialItems(Cell cell)
        {
            var cells = _matchFinder.FindMatches(cell, cell.Item.GetMatchType());
            
            if (cell.HasItem() && cells.Count == 1)
            {
                cell.Item.TryExecute();
            }
            else if (cells.Count > 1)
            {
                

                switch (GetBombCount(cells))
                {
                    case 0:
                        comboManager.GetComboCellListByType(ComboType.RocketRocket, cell);
                        break;
                    case 1:
                        comboManager.GetComboCellListByType(ComboType.BombRocket, cell);
                        break;
                    default:
                        comboManager.GetComboCellListByType(ComboType.BombBomb, cell);
                        break;
                        
                }
            }
        }

        int GetBombCount(List<Cell> cells)
        {
            int bombCount = 0;
            
            cells.ForEach((_cell) =>
            {
                if (_cell.Item is BombItem)
                {
                    bombCount++;
                }
            });

            return bombCount;
        }
        
        private void ExplodeMatchingCells(Cell cell)
        {
            var cells = _matchFinder.FindMatches(cell, cell.Item.GetMatchType());
            
            if (cells.Count < MinimumMatchCount) return;
            
            var specialItemsToExecute = new List<Item>();

            for (var i = 0; i < cells.Count; i++)
            {
                if (!cells[i].HasItem())
                {
                    continue;
                }
                var explodedCell = cells[i];

                CheckSpecialNeighbors(specialItemsToExecute, explodedCell);

                var item = explodedCell.Item;

                item.TryExecute();
            }

            if (cells.Count >= 7)
                CheckBombCreation(cells, cell);
            else if (cells.Count >= 5)
                CheckRocketCreation(cells, cell);
        }

        void CheckBombCreation(List<Cell> _cells, Cell _cell)
        {
            if (_cells.Count >= MinimumCountForBombCreation)
            {
                _cell.Item = ServiceProvider.GetItemFactory.CreateItem(
                    ItemType.Bomb, ItemsParent);
                _cell.Item.transform.position = _cell.transform.position;
            }
        }

        void CheckRocketCreation(List<Cell> _cells, Cell _cell)
        {
            if (_cells.Count >= MinimumCountForRocketCreation)
            {
                var randRocketType = UnityEngine.Random.Range(0, 1) == 0
                    ? ItemType.HorizontalRocket
                    : ItemType.VerticalRocket;

                _cell.Item = ServiceProvider.GetItemFactory.CreateItem(
                    randRocketType, ItemsParent);
                _cell.Item.transform.position = _cell.transform.position;
            }
        }

        void CheckSpecialNeighbors(List<Item> itemsToExecute, Cell cell)
        {
            cell.Neighbours.ForEach((_cell) =>
            {
                if (_cell.HasItem() && _cell.Item.GetCanAffectedByNeighbors() && !itemsToExecute.Contains(_cell.Item))
                {
                    if (_cell.Item.GetMatchType() == MatchType.None)
                    {
                        itemsToExecute.Add(_cell.Item);
                        _cell.Item.TryExecute();
                    }
                    else if (cell.Item.GetMatchType() == _cell.Item.GetMatchType())
                    {
                        itemsToExecute.Add(_cell.Item);
                        _cell.Item.TryExecute();
                    }
                }
            });
        }

        public Cell GetNeighbourWithDirection(Cell cell, Direction direction)
        {
            var x = cell.X;
            var y = cell.Y;
            switch (direction)
            {
                case Direction.None:
                    break;
                case Direction.Up:
                    y += 1;
                    break;
                case Direction.UpRight:
                    y += 1;
                    x += 1;
                    break;
                case Direction.Right:
                    x += 1;
                    break;
                case Direction.DownRight:
                    y -= 1;
                    x += 1;
                    break;
                case Direction.Down:
                    y -= 1;
                    break;
                case Direction.DownLeft:
                    y -= 1;
                    x -= 1;
                    break;
                case Direction.Left:
                    x -= 1;
                    break;
                case Direction.UpLeft:
                    y += 1;
                    x -= 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("direction", direction, null);
            }

            if (x >= Cols || x < 0 || y >= Rows || y < 0) return null;

            return Cells[x, y];
        }
    }
}