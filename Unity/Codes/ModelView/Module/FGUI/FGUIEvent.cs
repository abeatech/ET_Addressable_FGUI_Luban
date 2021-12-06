
namespace ET
{
    public interface IFGUIEvent
    {
        void InvokeOnCreate(object obj);
        void InvokeOnShow(object obj);
        void InvokeOnHide(object obj);
        void InvokeOnRefresh(object obj);
        void InvokeOnDestroy(object obj);
    }
    public abstract class FGUIEvent<T> : IFGUIEvent
    {
        public void InvokeOnCreate(object obj)
        {
            OnCreate((T)obj);
        }
        public void InvokeOnShow(object obj)
        {
            OnShow((T)obj);
        }

        public void InvokeOnHide(object obj)
        {
            OnHide((T)obj);
        }

        public void InvokeOnRefresh(object obj)
        {
            OnRefresh((T)obj);
        }

        public void InvokeOnDestroy(object obj)
        {
            OnDestroy((T)obj);
        }

        public virtual void OnCreate(T self) { }
        public virtual void OnShow(T self) { }
        public virtual void OnHide(T self) { }
        public virtual void OnRefresh(T self) { }
        public virtual void OnDestroy(T self) { }
    }
}
