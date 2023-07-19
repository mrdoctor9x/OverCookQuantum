using System;
using Photon.Deterministic;
using Quantum;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class LocalInput : MonoBehaviour
{
    Vector2 moveDir;
    private void OnEnable()
    {
        QuantumCallback.Subscribe(this, (CallbackPollInput callback) => PollInput(callback));
    }

    public void PollInput(CallbackPollInput callback)
    {
        Quantum.Input i = new Quantum.Input();

        i.Direction = moveDir.ToFPVector2();
        i.Jump = UnityEngine.Input.GetButton("Jump");

        callback.SetInput(i, DeterministicInputFlags.Repeatable);
    }
    public void OnMove(CallbackContext context)
    {
        moveDir = context.ReadValue<Vector2>();
    }
}
