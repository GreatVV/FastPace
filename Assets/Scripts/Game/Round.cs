using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Round
{
    public int Width;
    public int Height;

    public int[,] Field;
    public int Number;

    public class Cell
    {
        public int X;
        public int Y;
        public int Type;
    }

    public bool Win { get; set; }
    public int TargetColor;

    public static Round Create(int width, int height, List<Cell> cells = null)
    {
        var round = new Round()
        {
            Width = width,
            Height = height,
            Field = new int[width, height]
        };

        if (cells != null)
            foreach (var cell in cells)
            {
                if (cell.X >= 0 && cell.X < width && cell.Y >= 0 && cell.Y < height)
                {
                    round.Field[cell.X, cell.Y] = cell.Type;
                }
            }

        return round;
    }

    public void OnClick(Tile tile)
    {
        Win = Field[tile.X, tile.Y] == TargetColor;
        InvokeFinish();
    }

    public event Action<Round> Finish;

    protected virtual void InvokeFinish()
    {
        Action<Round> handler = Finish;
        if (handler != null) handler(this);
    }
}