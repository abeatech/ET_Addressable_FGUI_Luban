using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class FGUIEntity : Entity
    {
        public FGUIComponent FGUI => GetComponent<FGUIComponent>();
    }
}
