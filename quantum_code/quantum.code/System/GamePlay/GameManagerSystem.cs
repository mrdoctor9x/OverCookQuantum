using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum
{
    public unsafe class GameManagerSystem : SystemMainThread, ISignalOnPlayerDataSet, ISignalOnPlayerDisconnected
    {
        public override void OnInit(Frame f)
        {
            f.Unsafe.GetOrAddSingletonPointer<GameManager>()->Initialize(f);
        }
        public override void Update(Frame f)
        {
            f.Unsafe.GetPointerSingleton <GameManager>()->Update(f);
        }
        public void OnPlayerDataSet(Frame f, PlayerRef player)
        {
            f.Unsafe.GetPointerSingleton<GameManager>()->OnPlayerDataSet(f, player);
        }

        public void OnPlayerDisconnected(Frame f, PlayerRef player)
        {
            f.Unsafe.GetPointerSingleton<GameManager>()->OnPlayerDisconnected(f, player);
        }


    }
}
