using Quantum;
using Quantum.Demo;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetup : QuantumCallbacks
{
    [SerializeField] EntityView entityView;
    [SerializeField] HUIDHandle huid;
    [SerializeField] Animator animator;
    [SerializeField] GameObject ingredient;
    [SerializeField] GameObject ingredient2;
    [SerializeField] Transform anchorHold;

    PlayerRef playerRef;
    GameObject item;
    private void Awake()
    {
        QuantumEvent.Subscribe<EventPickUp>(this, OnPickUp);
        QuantumEvent.Subscribe<EventCook>(this, OnCook);
    }

    private void OnCook(EventCook callback)
    {
        if (entityView.EntityRef != callback.Entity)
            return;
        if(item != null)
            Destroy(item);
    }

    private void OnPickUp(EventPickUp callback)
    {
        if (entityView.EntityRef != callback.Entity)
            return;
        Debug.Log($"pick up unity {callback.indexObject}");
        item = Instantiate(ingredient, anchorHold.position, Quaternion.identity, anchorHold);

    }

    public void SetupPlayer(QuantumGame game)
    {

        var f = game.Frames.Predicted;
        var playerLink = f.Get<PlayerLink>(entityView.EntityRef);
        playerRef = playerLink.Player;

        SetupHuid(f);
        if (!QuantumRunner.Default.Game.PlayerIsLocal(playerRef)) return;
        SetupCamera();
    }
    public override void OnUpdateView(QuantumGame game)
    {
        var f = game.Frames.Predicted;
        var playerData = f.Get<PlayerData>(entityView.EntityRef);
        var speed = playerData.speed;

        animator.SetFloat("Speed", speed.AsFloat);
    }
    private void SetupHuid(Frame f)
    {
        huid.Setup(f.GetPlayerDataOrAI(playerRef).playerName);
    }
    private void SetupCamera()
    {
        var cameraFollow = GameObject.Find("CameraFollow").GetComponent<CameraFollow>();
        cameraFollow.SetupTarget(transform);
    }

}
