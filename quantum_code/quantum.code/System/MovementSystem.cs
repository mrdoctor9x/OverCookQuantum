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
            public PlayerLink* player;
        }
        public override void Update(Frame f, ref Filter filter)
        {
            //var filtered = f.Filter<Transform3D, PhysicsBody3D>();
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
            if (input.Interact.WasPressed && filter.PlayerData->isInteractable)
            {

                if(filter.PlayerData->indexInteract == 0)
                {
                    if(filter.PlayerData->indexObject == 0)
                    {
                        Interact(f, filter);
                    }
                }
                else if(filter.PlayerData->indexInteract == 1)
                {
                    if (filter.PlayerData->indexObject != 0)
                    {
                        Cook(f, filter);
                    }

                }
                else
                {

                }

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
        public void Interact(Frame f, Filter filter)
        {
            int random = f.RNG->Next(0, 100);
            Log.Debug($"Pick Up {random}");
            f.Events.PickUp(filter.player->Player, filter.Entity, 1);

            filter.PlayerData->indexObject = 1;
        }
        public void Cook(Frame f, Filter filter)
        {
            var setting = f.FindAsset<GameplaySettings>("Resources/DB/Asset/GameSetting");
            filter.PlayerData->Cook();
            var kitchen = f.Unsafe.GetPointer<Kitchen>(filter.PlayerData->EntityInteract);
            if (kitchen->isCook)
            {

            }
            else
            {
                kitchen->indexObject = filter.PlayerData->indexObject;
                filter.PlayerData->indexObject = 0;
                kitchen->isCook = true;

                f.Events.Cook(filter.player->Player, filter.Entity);
            }
        }
    }
}

