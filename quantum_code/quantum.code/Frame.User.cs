using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quantum {
    unsafe partial class Frame {
        public RuntimePlayer GetPlayerDataOrAI(PlayerRef playerRef)
        {
            var playerData = GetPlayerData(playerRef);
            if (playerData != null)
                return playerData;
            //return null;

            var settings = FindAsset<GameplaySettings>(RuntimeConfig.GameplaySettings.Id);
            return settings.AIPlayers[playerRef];
        }
    }
}
