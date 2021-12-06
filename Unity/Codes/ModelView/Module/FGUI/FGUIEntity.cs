using FairyGUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
	[ObjectSystem]
	public class UIAwakeSystem : AwakeSystem<FGUIEntity, Type,FGUIType>
	{
		public override void Awake(FGUIEntity self, Type type ,FGUIType eType)
		{
			self.ComponentType = type;
			self.UIType = eType;
		}
	}
	public class FGUIEntity : Entity
    {
        public Entity UIComponent => GetComponent(ComponentType);
		public GObject GObject => GetComponent<GObjectComponent>().GObject;
		public Type ComponentType;
		public FGUIType UIType;
    }
}
