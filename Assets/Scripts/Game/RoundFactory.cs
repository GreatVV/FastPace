using System;
using UnityEngine;
using Random = UnityEngine.Random;

internal class RoundFactory : MonoBehaviour
{
    public int MinNumberOfColors = 2;
    public int MaxNumberOfColors = 8;

    public void Awake()
    {
        if (Instance)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    public static RoundFactory Instance { get; private set; }

    public static Round GetRound(int roundNumber)
    {
        if (Instance)
        {
            return Instance.CreateRound(roundNumber);
        }
        throw new NullReferenceException("Not inited RoundFactory.Instance");
    }

    private Round CreateRound(int roundNumber)
    {
        var minWidth = 3;
        var minHeight = 3;
        var maxWidth = 10;
        var maxHeight = 10;

        int width =  Mathf.Clamp(minWidth + roundNumber, minWidth, maxWidth);
        int height = Mathf.Clamp(minHeight + roundNumber, minHeight, maxHeight);
        var round = Round.Create(width, height);
        round.Number = roundNumber;
        var colors = Mathf.Clamp(MinNumberOfColors + roundNumber, 1,MaxNumberOfColors);

        for (int i = 0; i < round.Width; i++)
        {
            for (int j = 0; j < round.Height; j++)
            {
                round.Field[i, j] = Random.Range(0, colors);
            }
        }

        round.TargetColor = round.Field[Random.Range(0, round.Width), Random.Range(0, round.Height)];
        return round;
    }
}