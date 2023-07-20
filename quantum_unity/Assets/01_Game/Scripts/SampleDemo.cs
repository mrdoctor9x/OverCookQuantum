using Quantum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleDemo : QuantumCallbacks
{
    Frame f; 
    private void Awake()
    {
        f = QuantumRunner.Default.Game.Frames.Predicted;
    }

    void Update()
    {

    }
    public override void OnUpdateView(QuantumGame game)
    {

    }
}
