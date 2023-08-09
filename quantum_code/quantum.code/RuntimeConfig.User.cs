using Photon.Deterministic;
using System;

namespace Quantum {
    partial class RuntimeConfig { 
        public AssetRefGameplaySettings GameplaySettings;
        partial void SerializeUserData(BitStream stream)
        {
            
            stream.Serialize(ref GameplaySettings);
        }
    }
}