using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace ET
{
    public class SceneChangeComponentUpdateSystem : UpdateSystem<SceneChangeComponent>
    {
        public override void Update(SceneChangeComponent self)
        {
            if (self.loadMapOperation == null ||!self.loadMapOperation.isDone)
            {
                return;
            }

            if (self.tcs == null)
            {
                return;
            }
            
            ETTask tcs = self.tcs;
            self.tcs = null;
            tcs.SetResult();
        }
    }
	
    
    public class SceneChangeComponentDestroySystem: DestroySystem<SceneChangeComponent>
    {
        public override void Destroy(SceneChangeComponent self)
        {
            self.loadMapOperation = null;
            self.tcs = null;
        }
    }

    public static class SceneChangeComponentSystem
    {
        public static async ETTask ChangeSceneAsync(this SceneChangeComponent self, string sceneName)
        {
            self.tcs = ETTask.Create(true);
            // 加载scene
            var instance = await AddressableComponent.Instance.LoadSceneByPathAsync(sceneName, out AsyncOperationHandle<SceneInstance> sceneInstanceHandle);
            AddressableComponent.Instance.UnLoadSceneAsync(instance).Coroutine();
        }
        
        public static int Process(this SceneChangeComponent self)
        {
            if (self.loadMapOperation == null)
            {
                return 0;
            }
            return (int)(self.loadMapOperation.progress * 100);
        }
    }
}