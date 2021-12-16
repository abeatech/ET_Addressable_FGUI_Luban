using UnityEngine;

namespace ET
{
    public class AI_XunLuo: AAIHandler
    {
        public override int Check(AIComponent aiComponent, Cfg.Demo.AIMeta aiConfig)
        {
            long sec = TimeHelper.ClientNow() / 1000 % 15;
            if (sec < 10)
            {
                return 0;
            }
            return 1;
        }

<<<<<<< HEAD
        public override async ETVoid Execute(AIComponent aiComponent, Cfg.Demo.AIMeta aiConfig, ETCancellationToken cancellationToken)
=======
        public override async ETTask Execute(AIComponent aiComponent, AIConfig aiConfig, ETCancellationToken cancellationToken)
>>>>>>> 15b8fb9bdedc02cd1f9842980bd08858d9bb7c18
        {
            Scene zoneScene = aiComponent.DomainScene();

            Unit myUnit = zoneScene.GetComponent<UnitComponent>().MyUnit;
            if (myUnit == null)
            {
                return;
            }
            
            Log.Debug("开始巡逻");

            while (true)
            {
                XunLuoPathComponent xunLuoPathComponent = myUnit.GetComponent<XunLuoPathComponent>();
                Vector3 nextTarget = xunLuoPathComponent.GetCurrent();
                int ret = await myUnit.MoveToAsync(nextTarget, cancellationToken);
                if (ret != 0)
                {
                    return;
                }
                xunLuoPathComponent.MoveNext();
            }
        }
    }
}