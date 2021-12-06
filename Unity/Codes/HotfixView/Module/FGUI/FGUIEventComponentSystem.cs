using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
	public class FGUIEventComponentAwakeSystem : AwakeSystem<FGUIEventComponent>
	{
		public override void Awake(FGUIEventComponent self)
		{
			if (FGUIEventComponent.Instance != null)
			{
				return;
			}
			FGUIEventComponent.Instance = self;
			var uiEvents = Game.EventSystem.GetTypes(typeof(FGUIEventAttribute));
			foreach (Type type in uiEvents)
			{
				object[] attrs = type.GetCustomAttributes(typeof(FGUIEventAttribute), false);
				if (attrs.Length == 0)
				{
					continue;
				}

				FGUIEventAttribute uiEventAttribute = attrs[0] as FGUIEventAttribute;
				FGUIEvent aUIEvent = Activator.CreateInstance(type) as FGUIEvent;
				self.UIEvents.Add(uiEventAttribute.UIType, aUIEvent);
			}
		}
	}

	public static class FGUIEventComponentSystem
	{
		public static void OnCreate(this FGUIEventComponent self, FGUIComponent component)
		{
			if (self.UIEvents.TryGetValue(component.uiType, out FGUIEvent e))
			{
				e?.OnCreate(component);
			}
		}
		public static void OnShow(this FGUIEventComponent self, FGUIComponent component)
		{

			if (self.UIEvents.TryGetValue(component.uiType, out FGUIEvent e))
			{
				e?.OnShow(component);
			}

		}
		public static void OnHide(this FGUIEventComponent self, FGUIComponent component)
		{
			if (self.UIEvents.TryGetValue(component.uiType, out FGUIEvent e))
			{
				e?.OnHide(component);
			}

		}
		public static void OnDestroy(this FGUIEventComponent self, FGUIComponent component)
		{
			if (self.UIEvents.TryGetValue(component.uiType, out FGUIEvent e))
			{
				e?.OnDestroy(component);
			}


		}
		public static void OnRefresh(this FGUIEventComponent self, FGUIComponent component)
		{

			if (self.UIEvents.TryGetValue(component.uiType, out FGUIEvent e))
			{
				e?.OnRefresh(component);
			}

		}
	}
}