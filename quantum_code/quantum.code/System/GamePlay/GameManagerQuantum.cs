﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum
{
    public unsafe partial struct GameManager
    {
        public void Initialize(Frame f)
        {
            SetState(f, GameState.Ready);
        }
        public void Update(Frame f)
        {
            Timer -= f.DeltaTime;

            switch (gameState)
            {
                case GameState.Ready: UpdateReady(f); break;
                case GameState.Start: UpdateStart(f); break;
                case GameState.End: UpdateEnd(f); break;
            }
        }
        public void UpdateReady(Frame f)
        {
            if(Timer <= 0)
            {
                SetState(f, GameState.Start);
            }
        }
        public void UpdateStart(Frame f)
        {
            if (Timer <= 0 || isWin)
            {
                SetState(f, GameState.End);
            }
        }
        public void UpdateEnd(Frame f)
        {

        }

        public void OnPlayerDataSet(Frame f, PlayerRef player)
        {
            f.Global->playerCount += 1;
            var data = f.GetPlayerData(player);

            // resolve the reference to the prototpye.
            var prototype = f.FindAsset<EntityPrototype>(data.CharacterPrototype.Id);

            // Create a new entity for the player based on the prototype.
            var entity = f.Create(prototype);

            // Create a PlayerLink component. Initialize it with the player. Add the component to the player entity.
            var playerLink = new PlayerLink()
            {
                Player = player,
            };
            f.Add(entity, playerLink);
            var playerData = new PlayerData()
            {
                hp = 1000,
                speed = 2,
            };
            f.Add(entity, playerData);
            // Offset the instantiated object in the world, based in its ID.
            if (f.Unsafe.TryGetPointer<Transform3D>(entity, out var transform))
            {
                transform->Position.X = 2 + player;
            }

        }
        public void OnPlayerDisconnected(Frame f, PlayerRef player)
        {
            Log.Debug($"player {player} disconnect");
            f.Global->playerCount -= 1;
            foreach (var p in f.Unsafe.GetComponentBlockIterator<PlayerLink>())
            {
                if (p.Component->Player == player)
                {
                    f.Destroy(p.Entity);
                }
            }
        }

        public void SetState(Frame f, GameState state)
        {
            gameState = state;
            switch (state)
            {
                case GameState.Ready:
                    Timer = 3;
                    break;
                case GameState.Start:
                    Timer = 120;
                    break;
                case GameState.End:
                    break;
            }
            f.Events.GameplayStateChanged(state);
        }
    }
}
