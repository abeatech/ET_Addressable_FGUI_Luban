using FairyGUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class GObjectComponentAwakeSystem : AwakeSystem<GObjectComponent,GObject>
    {
        public override void Awake(GObjectComponent self,GObject gObject)
        {
            self.GObject = gObject;
        }
    }

    public class GObjectComponent : Entity
    {
        public GObject GObject;
    }
}
