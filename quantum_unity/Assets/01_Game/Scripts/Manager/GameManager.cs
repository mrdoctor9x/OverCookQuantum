using Quantum;
using Quantum.Demo;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        QuantumEvent.Subscribe<EventGameplayStateChanged>(this, OnStateChange);
    }

    private void OnStateChange(EventGameplayStateChanged callback)
    {
        switch (callback.gameState)
        {
            case GameState.Ready:
                Debug.Log($"Game State {callback.gameState}");
                break;
            case GameState.Start:
                Debug.Log($"Game State {callback.gameState}");
                break;
            case GameState.End:
                Debug.Log($"Game State {callback.gameState}");
                break;
        }
    }
}
