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
			var uiEvents = Game.EventSystem.GetTypes(typeof(FGUIEventAttribute));
			foreach (Type type in uiEvents)
			{
				object[] attrs = type.GetCustomAttributes(typeof(FGUIEventAttribute), false);
				if (attrs.Length == 0)
				{
					continue;
				}

				FGUIEventAttribute uiEventAttribute = attrs[0] as FGUIEventAttribute;
				IFGUIEvent aUIEvent = Activator.CreateInstance(type) as IFGUIEvent;
				self.UIEvents.Add(uiEventAttribute.UIType, aUIEvent);
			}
		}
	}

	public static class FGUIEventComponentSystem
	{
		public static void OnCreate(this FGUIEventComponent self, FGUIEntity entity)
		{
			if (self.UIEvents.TryGetValue(entity.UIType, out IFGUIEvent e))
			{
				e?.InvokeOnCreate(entity.UIComponent);
			}
		}
		public static void OnShow(this FGUIEventComponent self, FGUIEntity entity)
		{

			if (self.UIEvents.TryGetValue(entity.UIType, out IFGUIEvent e))
			{
				e?.InvokeOnShow(entity.UIComponent);
			}

		}
		public static void OnHide(this FGUIEventComponent self, FGUIEntity entity)
		{
			if (self.UIEvents.TryGetValue(entity.UIType, out IFGUIEvent e))
			{
				e?.InvokeOnHide(entity.UIComponent);
			}

		}
		public static void OnDestroy(this FGUIEventComponent self, FGUIEntity entity)
		{
			if (self.UIEvents.TryGetValue(entity.UIType, out IFGUIEvent e))
			{
				e?.InvokeOnDestroy(entity.UIComponent);
			}


		}
		public static void OnRefresh(this FGUIEventComponent self, FGUIEntity entity)
		{

			if (self.UIEvents.TryGetValue(entity.UIType, out IFGUIEvent e))
			{
				e?.InvokeOnRefresh(entity.UIComponent);
			}

		}
	}
}