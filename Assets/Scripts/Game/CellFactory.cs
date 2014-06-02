using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Game;
using UnityEngine;

internal class CellFactory : MonoBehaviour
{
    public GameObject[] prefabs;

    public static CellFactory Instance { get; private set; }

    private void Awake()
    {
        if (Instance)
        {
            Debug.LogWarning("Already created instance of CellFactory");
            Destroy(this);
            return;
        }

        Instance = this;
    }

    private void OnDestoy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public static Cell GetRandomCell(int colors, Vector3 position)
    {
        if (Instance)
        {
            return Instance.RandomCell(colors, position);
        }
        Debug.LogWarning("CellFactory.Instance == null in GetRandomCell");
        return null;
    }

    private Cell RandomCell(int colors, Vector3 position)
    {
        var cell = ((GameObject) Instantiate(prefabs[Random.Range(0, Mathf.Clamp(colors, 1, prefabs.Length))]))
                .GetComponent<Cell>();
        cell.transform.position = position;
        FillNeigbours(cell);
        return cell;
    }

    public static void FillNeigbours(Cell cell)
    {
        List<Pair> neighbours = cell.Neighbours;
        foreach (Pair pair in neighbours)
        {
            if (pair.Neigbour == null)
            {
                var cast = Physics2D.OverlapPoint(pair.WorldPosition);
                Debug.Log("Cast pos: "+pair.WorldPosition);
                if (cast)
                {
                    var neigbourCell = cast.transform.gameObject.GetComponent<Cell>();
                    if (neigbourCell)
                    {
                        Debug.Log("neighbour: "+neigbourCell);
                        pair.Neigbour = neigbourCell;
                    }
                }
            }
        }
    }
}