using System;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Round CurrentRound;
    public int RoundNumber = 0;

    public void Start()
    {
        RoundNumber = 0;
        StartGame(RoundNumber);
    }

    private void StartGame(int roundNumber)
    {
        Round round;
        if (roundNumber == 0)
        {
            if (CurrentRound != null)
            {
                CurrentRound.DestroyAll();
            }
            
        }

        if (roundNumber == 0)
        {
            round = RoundFactory.FirstRound();
        }
        else
        {
            round = RoundFactory.GetRound(CurrentRound);
        }

        
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
            RoundNumber = 0;
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