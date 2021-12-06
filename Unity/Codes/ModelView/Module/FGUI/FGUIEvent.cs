
namespace ET
{
    public abstract class FGUIEvent
    {
        public virtual void OnCreate(FGUIComponent self) { }
        public virtual void OnShow(FGUIComponent self) { }
        public virtual void OnHide(FGUIComponent self) { }
        public virtual void OnRefresh(FGUIComponent self) { }
        public virtual void OnDestroy(FGUIComponent self) { }
    }
}
