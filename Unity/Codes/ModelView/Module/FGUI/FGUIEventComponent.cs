using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ET
{
    public class FGUIEventComponent : Entity
	{
		public static FGUIEventComponent Instance;
		public Dictionary<FGUIType, FGUIEvent> UIEvents = new Dictionary<FGUIType, FGUIEvent>();
	}
}
