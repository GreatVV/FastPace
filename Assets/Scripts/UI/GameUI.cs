using System.Globalization;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public Game Game;
    public GameObject LoseLabel;
    public GameObject WinLabel;

    public UILabel roundNumber;

    public Sprite[] targetSprites;

    public UI2DSprite targetSprite;

    public void Awake()
    {
        Game.Win += () => OnWin(true);
        Game.Lose += () => OnWin(false);

        Game.NewRound += OnNewRound;
        Game.Started += OnStarted;
    }

    private void OnNewRound(Round round)
    {
        roundNumber.text = round.Number.ToString(CultureInfo.InvariantCulture);
        targetSprite.sprite2D = targetSprites[round.TargetType];
    }

    private void OnStarted()
    {
        WinLabel.SetActive(false);
        LoseLabel.SetActive(false);
    }

    private void OnWin(bool win)
    {
        WinLabel.SetActive(win);
        LoseLabel.SetActive(!win);
    }
}