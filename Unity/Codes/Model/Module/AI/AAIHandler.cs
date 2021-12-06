using System;

namespace ET
{
    public class AIHandlerAttribute: BaseAttribute
    {
    }
    
    [AIHandler]
    public abstract class AAIHandler
    {
        // 检查是否满足条件
        public abstract int Check(AIComponent aiComponent, Cfg.Demo.AIMeta aiConfig);

        // 协程编写必须可以取消
        public abstract ETVoid Execute(AIComponent aiComponent, Cfg.Demo.AIMeta aiConfig, ETCancellationToken cancellationToken);
    }
}