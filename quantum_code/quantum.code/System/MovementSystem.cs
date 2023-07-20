using Photon.Deterministic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum
{

    public unsafe class MovementSystem : SystemMainThreadFilter<MovementSystem.Filter>
    {
        public struct Filter
        {
            public EntityRef Entity;
            public CharacterController3D* CharacterController;
            public PlayerData* PlayerData;
            public Transform3D* Transforms;
        }
        public override void Update(Frame f, ref Filter filter)
        {
            Input input = default;
            if (f.Unsafe.TryGetPointer(filter.Entity, out PlayerLink* playerLink))
            {
                input = *f.GetPlayerInput(playerLink->Player);
            }
            RotateCharacter(f, filter, input);
            if (input.Jump.WasPressed)
            {
                filter.CharacterController->Jump(f);
            }

            filter.PlayerData->speed = input.Direction.XOY.Magnitude;

            filter.CharacterController->Move(f, filter.Entity, input.Direction.XOY);

        }

        public void RotateCharacter(Frame f, Filter filter, Input input)
        {
            //rotate
            if (input.Direction != default)
            {
                //FPVector3 dir = input.Direction.XOY;
                FPVector3 dir = FPVector3.Lerp(filter.Transforms->Forward, input.Direction.XOY, 20 * f.DeltaTime);

                filter.Transforms->Rotation = FPQuaternion.LookRotation(dir);
            }
        }
    }
}
