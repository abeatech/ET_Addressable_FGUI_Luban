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
		public Dictionary<Cfg.FGUIType, IFGUIEvent> UIEvents = new Dictionary<Cfg.FGUIType, IFGUIEvent>();
	}
}
