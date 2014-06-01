using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class TileFactory : MonoBehaviour
{
    [SerializeField] private List<Tile> _prefabs = new List<Tile>();

    [SerializeField] private float tileHeight = 1f;

    [SerializeField] private float tileWidth = 1f;
    public static TileFactory Instance { get; private set; }
    public static List<Tile> Tiles = new List<Tile>();

    public void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public static void CreateTileForRound(Round round)
    {
        if (Instance)
        {
            Instance.Init(round);
            return;
        }
        throw new NullReferenceException("Not inited TileFactory.Instance");
    }

    private void Init(Round round)
    {
        var transforms = GetComponentsInChildren<Transform>().ToList();
        transforms.Remove(transform);
        foreach (var child in transforms)
        {
            Destroy(child.gameObject);
        }
        Tiles.Clear();
        for (int i = 0; i < round.Width; i++)
        {
            for (int j = 0; j < round.Height; j++)
            {
                Tile tilePrefab = _prefabs[round.Field[i, j]];
                var clone = Instantiate(tilePrefab) as Tile;
                Tiles.Add(clone);
                clone.transform.parent = transform;
                clone.transform.localPosition = new Vector3(i*tileWidth, j*tileHeight, 0f);
                clone.Init(i, j, round.Field[i, j]);
                clone.Clicked += round.OnClick;
            }
        }
        CenterCameraOnField.Instance.CenterCameraOnChuzzles(Tiles, true);
    }
}