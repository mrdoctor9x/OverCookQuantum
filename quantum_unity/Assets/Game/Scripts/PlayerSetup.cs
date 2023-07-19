using Quantum;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    [SerializeField] EntityView entityView;
    PlayerRef playerRef;
    private void Awake()
    {
    }

    public void SetupPlayer(QuantumGame game)
    {
        Debug.Log("init player");
        var f = game.Frames.Predicted;
        var playerLink = f.Get<PlayerLink>(entityView.EntityRef);
        playerRef = playerLink.Player;

        if (!QuantumRunner.Default.Game.PlayerIsLocal(playerRef)) return;
        var cameraFollow = GameObject.Find("CameraFollow").GetComponent<CameraFollow>();
        Debug.Log($"null cam {cameraFollow == null}");
        cameraFollow.SetupTarget(transform);
    }

}
