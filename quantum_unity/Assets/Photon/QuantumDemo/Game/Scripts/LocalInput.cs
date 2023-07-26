using System;
using Photon.Deterministic;
using Quantum;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class LocalInput : MonoBehaviour
{
    Vector2 moveDir;
    bool isInteract;
    private void OnEnable()
    {
        QuantumCallback.Subscribe(this, (CallbackPollInput callback) => PollInput(callback));
    }

    public void PollInput(CallbackPollInput callback)
    {
        Quantum.Input i = new Quantum.Input();

        i.Direction = moveDir.ToFPVector2();
        i.Jump = UnityEngine.Input.GetButton("Jump");
        i.Interact = isInteract;

        isInteract = false;

        callback.SetInput(i, DeterministicInputFlags.Repeatable);
    }
    public void OnMove(CallbackContext context)
    {
        moveDir = context.ReadValue<Vector2>();
    }
    public void OnInteract(CallbackContext context)
    {
        switch (context.phase)
        {
            //Performed.
            case InputActionPhase.Performed:
                isInteract = true;
                break;
        }
    }
}
