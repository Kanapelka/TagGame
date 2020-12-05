using System;
using TagGame.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    private const int Size = 4;
    private Game _game;

    public Text MovesText;

    public void Start()
    {
        _game = new Game(Size);
        _game.Start(1024 + DateTime.Now.DayOfYear);
        ShowTiles();
        MovesText.text = "Tap on the tiles to move them".ToUpper();
    }

    public void OnClick()
    {
        if (_game.IsSolved())
            return;

        var objectName = EventSystem.current.currentSelectedGameObject.name;
        var x = int.Parse(objectName.Substring(0, 1));
        var y = int.Parse(objectName.Substring(1, 1));
        _game.PressAt(x, y);
        
        ShowTiles();

        MovesText.text = _game.IsSolved() 
            ? $"The game was solved in {_game.Moves} moves".ToUpper() 
            : $"{_game.Moves} moves".ToUpper();
    }

    public void OnRestartClick()
    {
        _game.Start(1024 + DateTime.Now.DayOfYear);
        ShowTiles();
        MovesText.text = "Tap on the tiles to move them".ToUpper();
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }

    private void ShowTiles()
    {
        for (var x = 0; x < Size; x++)
        for (var y = 0; y < Size; y++)
            ShowDigitAt(_game.GetDigitAt(x, y), x, y);
    }

    private void ShowDigitAt(int digit, int x, int y)
    {
        var buttonName = $"{x}{y}";
        var button = GameObject.Find(buttonName);
        var text = button.GetComponentInChildren<Text>();
        text.text = $"{digit}";
        button.GetComponentInChildren<Image>().color = digit == 0 ? Color.clear : Color.white;
    }
}