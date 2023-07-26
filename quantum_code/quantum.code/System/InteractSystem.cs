using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum
{
    public class InteractSystem : SystemSignalsOnly, ISignalOnTriggerEnter3D, ISignalOnTriggerExit3D
    {
        public unsafe void OnTriggerEnter3D(Frame f, TriggerInfo3D info)
        {
            if (f.Has<PlayerData>(info.Other)) {
                var data = f.Unsafe.GetPointer<PlayerData>(info.Other);

                if (f.Has<BoxIngredient>(info.Entity))
                {
                    data->indexInteract = 0;
                    data->EntityInteract = info.Entity;
                }
                if (f.Has<Kitchen>(info.Entity))
                {
                    data->indexInteract = 1;
                    data->EntityInteract = info.Entity;
                }
                if (f.Has<End>(info.Entity))
                {
                    data->indexInteract = 2;
                    data->EntityInteract = info.Entity;
                }

                data->isInteractable = true;
            }
        }

        public unsafe void OnTriggerExit3D(Frame f, ExitInfo3D info)
        {
            Log.Debug($"{info.Entity} {info.Other}");
            if (f.Has<PlayerData>(info.Other))
            {
                var data = f.Unsafe.GetPointer<PlayerData>(info.Other);

                data->isInteractable = false;
            }
        }
    }
}
