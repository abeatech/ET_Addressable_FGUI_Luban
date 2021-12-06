using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
	[ObjectSystem]
	public class UIAwakeSystem : AwakeSystem<FGUIEntity, Type>
	{
		public override void Awake(FGUIEntity self, Type type)
		{
			self.ComponentType = type;
		}
	}
	public class FGUIEntity : Entity
    {
        public FGUIComponent FGUI => GetComponent(ComponentType) as FGUIComponent;
		public Type ComponentType;
    }
}
