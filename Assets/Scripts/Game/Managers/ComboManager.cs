using System.Collections;
using System.Collections.Generic;
using Game.Core.BoardBase;
using UnityEngine;

public class ComboManager
{
    public void GetComboCellListByType(ComboType comboType, Cell originCell)
    {
        switch (comboType)
        {
            case ComboType.BombBomb:
                BombBomb(originCell);
                break;
            case ComboType.BombRocket:
                BombRocket(originCell);
                break;
            case ComboType.RocketRocket:
                 RocketRocket(originCell);
                 break;
            default:
                break;
        }
    }

    void  BombBomb(Cell originCell)
    {
        int startX = Mathf.Max(0,originCell.X - 3);
        int startY = Mathf.Max(0,originCell.Y - 3);
        
        int finishX = Mathf.Min(Board.Rows,originCell.X + 3);
        int finishY = Mathf.Min(Board.Cols,originCell.Y + 3);
        
        for (int i = startX; i < finishX ; i++)
        {
            for (int j = startY; j < finishY ; j++)
            {
                if (originCell.Board.Cells[i, j].HasItem())
                    originCell.Board.Cells[i, j].Item.TryExecute();
            }
        }
    }
    
    void  BombRocket(Cell originCell)
    {
        for (int i = 0; i < Board.Rows ; i++)
        {
            if (Board.Cols > originCell.Y + 1)
            {
                if (originCell.Board.Cells[i, originCell.Y + 1].HasItem())
                    originCell.Board.Cells[i, originCell.Y + 1].Item.TryExecute();
            }
            if (0 <= originCell.Y - 1)
            {
                if (originCell.Board.Cells[i, originCell.Y - 1].HasItem())
                    originCell.Board.Cells[i, originCell.Y - 1].Item.TryExecute();
            }
            
            if (originCell.Board.Cells[i, originCell.Y].HasItem())
                originCell.Board.Cells[i, originCell.Y].Item.TryExecute();
        }
        
        for (int i = 0; i < Board.Cols ; i++)
        {
            if (Board.Rows > originCell.X + 1)
            {
                if (originCell.Board.Cells[originCell.X + 1, i].HasItem())
                    originCell.Board.Cells[originCell.X + 1, i].Item.TryExecute();
            }
            if (0 <= originCell.X - 1)
            {
                if (originCell.Board.Cells[originCell.X - 1, i].HasItem())
                    originCell.Board.Cells[originCell.X - 1, i].Item.TryExecute();
            }

            if (originCell.Board.Cells[originCell.X, i].HasItem())
                originCell.Board.Cells[originCell.X, i].Item.TryExecute();
        }
    }
    
    void  RocketRocket(Cell originCell)
    {
        for (int i = 0; i < Board.Rows ; i++)
        {
            if (originCell.Board.Cells[i, originCell.Y].HasItem())
                originCell.Board.Cells[i, originCell.Y].Item.TryExecute();
        }
        
        for (int i = 0; i < Board.Cols ; i++)
        {
            if (originCell.Board.Cells[originCell.X, i].HasItem())
                originCell.Board.Cells[originCell.X, i].Item.TryExecute();
        }
    }

}
