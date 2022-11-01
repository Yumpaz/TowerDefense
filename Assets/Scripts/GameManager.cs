using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject TestingObject;
    [SerializeField] public TextMeshProUGUI tcredits, tecredits;
    private bool running = false;
    public GameState _gameState = GameState.simulationwait;
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
        UpdateGameState(GameState.simulationwait);
        Instantiate(TestingObject);
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
        if(!running)
        {
            running = true;
            UpdateGameState(GameState.simulationstart);
        }
    }

    public void StopSimulation()
    {
        Testing.Instance.ReStartGame();
        running = false;
        Testing.Instance.changeenemies = 0;
        UpdateGameState(GameState.simulationend);
    }
}
