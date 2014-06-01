using System;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Round CurrentRound;
    public int MaxHeight = 10;
    public int MaxWidth = 10;

    public int RoundNumber = 0;
    public int StartHeight = 3;
    public int StartWidth = 3;

    public void Start()
    {
        RoundNumber = 0;
        StartGame(RoundNumber);
    }

    private void StartGame(int roundNumber)
    {
        Round round = RoundFactory.GetRound(roundNumber);
        TileFactory.CreateTileForRound(round);
        round.Finish += OnRoundFinish;
        InvokeStarted();
        CurrentRound = round;
        InvokeNewRound();
        
    }

    private void OnRoundFinish(Round round)
    {
        round.Finish -= OnRoundFinish;

        if (round.Win)
        {
            RoundNumber++;
            StartGame(RoundNumber);
        }
        else
        {
            InvokeLose();
        }
    }

    public event Action Started;

    protected virtual void InvokeStarted()
    {
        Action handler = Started;
        if (handler != null) handler();
    }

    public event Action<Round> NewRound;

    protected virtual void InvokeNewRound()
    {
        Action<Round> handler = NewRound;
        if (handler != null) handler(CurrentRound);
    }

    public event Action Win;

    protected virtual void InvokeWin()
    {
        Action handler = Win;
        if (handler != null) handler();
    }

    public event Action Lose;

    protected virtual void InvokeLose()
    {
        Action handler = Lose;
        if (handler != null) handler();
    }
}