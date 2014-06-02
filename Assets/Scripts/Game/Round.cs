using System;
using System.Collections.Generic;
using Assets.Scripts.Game;
using Object = UnityEngine.Object;

[Serializable]
public class Round
{
    public List<Cell> Cells = new List<Cell>();

    public int Number;

    public int TargetType;
    public bool Win { get; set; }

    public void OnClick(Cell cell)
    {
        Win = cell.Type == TargetType;
        InvokeFinish();
    }

    public event Action<Round> Finish;

    protected virtual void InvokeFinish()
    {
        Action<Round> handler = Finish;
        if (handler != null) handler(this);
    }

    public void DestroyAll()
    {
        foreach (Cell cell in Cells)
        {
            Object.Destroy(cell);
        }
    }
}