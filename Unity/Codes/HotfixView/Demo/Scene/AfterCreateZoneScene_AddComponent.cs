namespace ET
{
    public class AfterCreateZoneScene_AddComponent: AEvent<EventType.AfterCreateZoneScene>
    {
        protected override async ETTask Run(EventType.AfterCreateZoneScene args)
        {
            Scene zoneScene = args.ZoneScene;
            zoneScene.AddComponent<FGUIComponent>();
            zoneScene.AddComponent<FGUIEventComponent>();
            await ETTask.CompletedTask;
        }
    }
}