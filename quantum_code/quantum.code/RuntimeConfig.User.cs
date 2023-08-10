using Photon.Deterministic;
using System;

namespace Quantum {
    partial class RuntimeConfig { 
        public AssetRefGameplaySettings GameplaySettings;
        public AssetRefNavMeshAgentConfig BotNavmesh;
        partial void SerializeUserData(BitStream stream)
        {
            
            stream.Serialize(ref GameplaySettings);
            stream.Serialize(ref BotNavmesh);
        }
    }
}