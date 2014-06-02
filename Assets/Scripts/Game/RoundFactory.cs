using System;
using System.Collections.Generic;
using Assets.Scripts.Game;
using UnityEngine;
using Random = UnityEngine.Random;

internal class RoundFactory : MonoBehaviour
{
    public int MaxNumberOfColors = 8;
    public int MinNumberOfColors = 2;

    public static RoundFactory Instance { get; private set; }

    public void Awake()
    {
        if (Instance)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    public static Round GetRound(Round round)
    {
        if (Instance)
        {
            return Instance.UpgradeRound(round);
        }
        throw new NullReferenceException("Not inited RoundFactory.Instance");
    }

    private Round UpgradeRound(Round round)
    {
        round.Number++;
        int colors = Mathf.Clamp(MinNumberOfColors + round.Number, 1, MaxNumberOfColors);
        var newCells = new List<Cell>();
        foreach (Cell cell in round.Cells)
        {
            foreach (Pair neighbour in cell.Neighbours)
            {
                if (neighbour.Neigbour == null)
                {
                    Vector3 newPos = cell.transform.position + neighbour.OffsetRelativeCenter;
                    var cast = Physics2D.OverlapPoint(newPos);
                    if (!cast)
                    {
                        Cell newCell = CellFactory.GetRandomCell(colors,
                            newPos);
                        neighbour.Neigbour = newCell;
                        newCell.Clicked += round.OnClick;
                        newCells.Add(newCell);
                        break;
                    }
                    neighbour.Neigbour = cast.transform.GetComponent<Cell>();
                }
            }
        }

        round.Cells.AddRange(newCells);

        round.TargetType = newCells.ToArray()[Random.Range(0, newCells.Count)].Type;
        CenterCameraOnField.Instance.CenterCameraOnChuzzles(round.Cells, true);
        return round;
    }

    public static Round FirstRound()
    {
        var round = new Round();
        Cell randomCell = CellFactory.GetRandomCell(10, Vector3.zero);
        randomCell.Clicked += round.OnClick;
        round.TargetType = randomCell.Type;
        round.Cells.Add(randomCell);
        return round;
    }
}