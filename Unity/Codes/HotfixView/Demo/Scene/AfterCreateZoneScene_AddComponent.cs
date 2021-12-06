namespace ET
{
    public class AfterCreateZoneScene_AddComponent: AEvent<EventType.AfterCreateZoneScene>
    {
        protected override async ETTask Run(EventType.AfterCreateZoneScene args)
        {
            Scene zoneScene = args.ZoneScene;
            FGUI fgui = zoneScene.AddComponent<FGUI>();
            fgui.AddComponent<FGUIEventComponent>();
            await ETTask.CompletedTask;
        }
    }
}