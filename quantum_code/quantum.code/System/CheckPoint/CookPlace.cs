using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum
{
    public unsafe class CookPlace : SystemMainThreadFilter<CookPlace.Filter>, ISignalOnTrigger3D
    {
        public void OnTrigger3D(Frame f, TriggerInfo3D info)
        {
            throw new NotImplementedException();
        }

        public override void Update(Frame f, ref Filter filter)
        {
            throw new NotImplementedException();
        }

        public struct Filter
        {
            public EntityRef Entity;
            public Transform3D* Transforms;

        }
    }
}
