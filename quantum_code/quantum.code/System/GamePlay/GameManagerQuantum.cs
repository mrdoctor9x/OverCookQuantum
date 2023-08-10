using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.Deterministic;

namespace Quantum
{
    public unsafe partial struct GameManager
    {
        public void Initialize(Frame f)
        {
            CheckRoom(f);
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
        public void CheckRoom(Frame f)
        {
            var setting = f.FindAsset<GameplaySettings>(f.RuntimeConfig.GameplaySettings.Id);
            
            if (f.PlayerCount < setting.PlayerCount)
            {
                var slotEmpty = f.PlayerCount - f.Global->playerCount;
                for (int i = f.PlayerCount; i < setting.PlayerCount; i++)
                {
                    OnBotAIDataSet(f, i);
                }
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
        public void OnBotAIDataSet(Frame f, PlayerRef player)
        {
            f.Global->playerCount += 1;
            var data = f.GetPlayerDataOrAI(player);

            // resolve the reference to the prototpye.
            var prototype = f.FindAsset<EntityPrototype>(data.CharacterPrototype.Id);

            // Create a new entity for the player based on the prototype.
            var entity = f.Create(prototype);

            // Create a PlayerLink component. Initialize it with the player. Add the component to the player entity.
            var playerLink = new PlayerLink()
            {
                Player = player,
                playerState = PlayerState.Bot
            };
            f.Add(entity, playerLink);
            var playerData = new PlayerData()
            {
                hp = 1000,
                speed = 2,
            };
            f.Add(entity, playerData);
            
            //navmesh
            var config = f.FindAsset<NavMeshAgentConfig>(f.RuntimeConfig.BotNavmesh.Id);
            var pathfinder = NavMeshPathfinder.Create(f, entity, config);
            // find a random point to move to
            var navmesh = f.Map.NavMeshes["Navmesh"];
            var vectorMove = new FPVector3(0,FP._0_50,-20);
            pathfinder.SetTarget(f, vectorMove, navmesh);

            f.Set(entity, pathfinder);
            f.Set(entity, new NavMeshSteeringAgent());
            
            // Offset the instantiated object in the world, based in its ID.
            if (f.Unsafe.TryGetPointer<Transform3D>(entity, out var transform))
            {
                transform->Position.X = 2 + player;
                transform->Position.Y = 2;
            }
            
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
                playerState = PlayerState.Player
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
