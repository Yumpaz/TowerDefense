using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject TestingObject;
    [SerializeField] public TextMeshProUGUI tcredits, tecredits, WinState;
    private bool running = false, started = false;
    public GameState _gameState = GameState.simulationwait;
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
        UpdateGameState(GameState.simulationwait);
        Instantiate(TestingObject);
        Directory.CreateDirectory(Application.streamingAssetsPath + "/Output/");
        CreateTextFile();
    }

    void Update()
    {
        switch (_gameState)
        {
            case GameState.simulationwait:
                
                break;
            case GameState.simulationstart:
                UpdateGameState(GameState.prepare);
                break;
            case GameState.simulationend:
                
                break;
        }
    }

    public enum GameState
    {
        simulationwait,
        simulationstart,
        prepare,
        play,
        end,
        simulationend
    }

    public void UpdateGameState(GameState gameState)
    {
        _gameState = gameState;
    }

    public void StartSimulation()
    {
        started = true;
        if(!running)
        {
            running = true;
            Testing.Instance.posx.Clear();
            Testing.Instance.posy.Clear();
            UpdateGameState(GameState.simulationstart);
        }
    }

    public void StopSimulation()
    {
        if (started)
        {
            Testing.Instance.ReStartGame();
            running = false;
            Testing.Instance.changeenemies = 0;
            UpdateGameState(GameState.simulationend);
            started = false;
        }
    }

    public void SetType0()
    {
        Testing.Instance.type = 0;
        Testing.Instance.SelectionState(true);
    }

    public void SetType1()
    {
        Testing.Instance.type = 1;
        Testing.Instance.SelectionState(true);
    }

    public void SetType2()
    {
        Testing.Instance.type = 2;
        Testing.Instance.SelectionState(true);
    }

    public void AutoSet()
    {
        if (started)
        {
            Testing.Instance.GetPlayerAutoPositions();
        }
    }

    public void CreateTextFile()
    {
        string txtDocName = Application.streamingAssetsPath + "/Output/" + "Positions" + ".txt";

        if (!File.Exists(txtDocName))
        {
            File.WriteAllText(txtDocName, "");
        }
    }

    public void AddTextToFile(string text)
    {
        string txtDocName = Application.streamingAssetsPath + "/Output/" + "Positions" + ".txt";

        File.AppendAllText(txtDocName, text + "\n");
    }
}
