using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class EventAppStartInitFinish_OpenLogin : AEvent<AppStartInitFinish>
    {
        protected override async ETTask Run(AppStartInitFinish a)
        {
            await FGUIComponent.Instance.OpenAysnc(FGUIType.Backgound);
            //游戏正式开始 这里放的是游戏主体的逻辑，依附在一个Scene上
            await FGUIComponent.Instance.OpenAysnc(FGUIType.Login);
        }
    }
}
