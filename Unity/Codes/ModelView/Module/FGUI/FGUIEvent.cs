
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
    public abstract class FGUIEvent<T> : IFGUIEvent where T : class
    {
        public void InvokeOnCreate(object obj)
        {
            this.OnCreate((T)obj);
        }
        public void InvokeOnShow(object obj)
        {
            this.OnShow((T)obj);
        }

        public void InvokeOnHide(object obj)
        {
            this.OnHide((T)obj);
        }

        public void InvokeOnRefresh(object obj)
        {
            this.OnRefresh((T)obj);
        }

        public void InvokeOnDestroy(object obj)
        {
            this.OnDestroy((T)obj);
        }

        public abstract void OnCreate(T self);
        public abstract void OnShow(T self);
        public abstract void OnHide(T self);
        public abstract void OnRefresh(T self);
        public abstract void OnDestroy(T self);
    }
}
