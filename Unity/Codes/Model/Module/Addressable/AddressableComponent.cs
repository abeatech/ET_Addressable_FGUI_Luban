using FairyGUI;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace ET
{
    [ObjectSystem]
    public class AddressableComponentAwakeSystem : AwakeSystem<AddressableComponent>
    {
        public override void Awake(AddressableComponent self)
        {
            self.Awake();
        }
    }

    public class AddressableComponent : Entity
    {
        public static AddressableComponent Instance { get; set; }

        /// <summary>
        /// Addressable组件是否已经初始化
        /// </summary>
        private static bool initialize = false;

        public void Awake()
        {
            if (Instance != null)
            {
                Log.Error("错误！重复添加Addressable加载组件");
            }
            else
            {
                Instance = this;
                Log.Info("添加Addressable加载组件");
            }
        }

        /// <summary>
        /// Addressable异步初始化
        /// </summary>
        public ETTask AddressableInitializeAsync()
        {
            if (initialize)
            {
                Log.Error("Addressable重复初始化了");
                return null;
            }
            else
            {
                Log.Info("Addressable开始初始化");
                ETTask tcs = ETTask.Create();
                AsyncOperationHandle initializeHandle = Addressables.InitializeAsync();
                initializeHandle.Completed += (handle) =>
                {
                    Log.Info("Addressable初始化完成");
                    initialize = true;
                    tcs.SetResult();
                };

                return tcs.GetAwaiter();
            }
        }

        /// <summary>
        /// 获取需要更新的资源的大小(通过标签DefAssets获取，所有需要更新的资源需要打上DefAssets标签)
        /// </summary>
        /// <returns>返回大小，单位Byte，如果大于0说明需要有更新</returns>
        public ETTask<long> AddressableGetDownloadSizeAsync()
        {
            if (initialize)
            {
                Log.Info("正在检查是否有更新内容");
                ETTask<long> tcs = ETTask<long>.Create();
                AsyncOperationHandle<long> downloadSizeHandle = Addressables.GetDownloadSizeAsync("DefAssets");
                downloadSizeHandle.Completed += (handle) =>
                {
                    tcs.SetResult(handle.Result);
                };
                return tcs.GetAwaiter();
            }
            else
            {
                Log.Error("Addressable获取下载大小前必须先初始化");
                return null;
            }
        }

        /// <summary>
        /// 下载需要更新的资源(通过标签DefAssets下载，所有需要更新的资源需要打上DefAssets标签)
        /// </summary>
        public async ETTask DownloadUpdateAssetsAsync()
        {
            if (initialize)
            {
                ETTask tcs = ETTask.Create();
                AsyncOperationHandle downloadHandle = Addressables.DownloadDependenciesAsync("DefAssets", true);

                long totalBytes = downloadHandle.GetDownloadStatus().TotalBytes;
                while (!downloadHandle.IsDone)
                {
                    DownloadStatus loadStatus = downloadHandle.GetDownloadStatus();
                    Log.Info("下载进度：" + (downloadHandle.PercentComplete * 100).ToString("0.00") + "%\t||\t下载大小：" + (loadStatus.DownloadedBytes / 1024.0f).ToString("0.00") + "KB\t||\t总大小：" + (totalBytes / 1024.0f).ToString("0.00") + "KB");
                    await TimerComponent.Instance.WaitFrameAsync();
                }
                Log.Info("下载完成");
                tcs.SetResult();
            }
            else
            {
                Log.Error("更新下载前必须先初始化");
            }
        }

        /// <summary>
        /// 通过资源路径(AddressableName)同步加载一个资源
        /// </summary>
        public T LoadAssetByPath<T>(string assetPath) where T : UnityEngine.Object
        {
            var op = Addressables.LoadAssetAsync<T>(assetPath);
            T asset = op.WaitForCompletion();
            Addressables.Release(op);
            return asset;
        }
        /// <summary>
        /// 通过资源路径(AddressableName)异步加载一个资源
        /// </summary>
        public ETTask<T> LoadAssetByPathAsync<T>(string assetPath) where T : UnityEngine.Object
        {
            ETTask<T> tcs = ETTask<T>.Create(true);
            AsyncOperationHandle<T> assetHandle = Addressables.LoadAssetAsync<T>(assetPath);
            assetHandle.Completed += (handle) =>
            {
                tcs.SetResult(handle.Result);
                tcs = null;
            };
            return tcs.GetAwaiter();
        }

        /// <summary>
        /// 通过资源路径(AddressableName)异步加载一个资源，需要卸载用这个
        /// </summary>
        public ETTask<T> LoadAssetByPathAsync<T>(string assetPath, out AsyncOperationHandle<T> assetHandle) where T : UnityEngine.Object
        {
            ETTask<T> tcs = ETTask<T>.Create(true);
            assetHandle = Addressables.LoadAssetAsync<T>(assetPath);
            assetHandle.Completed += (handle) =>
            {
                tcs.SetResult(handle.Result);
                tcs = null;
            };
            return tcs.GetAwaiter();
        }

        /// <summary>
        /// 通过资源路径(AddressableName)异步加载一个资源并且实例化
        /// </summary>
        public ETTask<GameObject> LoadGameObjectAndInstantiateByPath(string assetPath, Transform parent = null, bool instantiateInWorldSpace = false, bool trackHandle = true)
        {
            ETTask<GameObject> tcs = ETTask<GameObject>.Create(true);
            AsyncOperationHandle<GameObject> assetHandle = Addressables.InstantiateAsync(assetPath, parent, instantiateInWorldSpace, trackHandle);
            assetHandle.Completed += (handle) =>
            {
                tcs.SetResult(handle.Result);
                tcs = null;
            };
            return tcs.GetAwaiter();
        }

        /// <summary>
        /// 通过资源路径(AddressableName)异步加载一个资源并且实例化，需要卸载用这个
        /// </summary>
        public ETTask<GameObject> LoadGameObjectAndInstantiateByPath(string assetPath, out AsyncOperationHandle<GameObject> assetHandle, Transform parent = null, bool instantiateInWorldSpace = false, bool trackHandle = true)
        {
            ETTask<GameObject> tcs = ETTask<GameObject>.Create(true);
            assetHandle = Addressables.InstantiateAsync(assetPath, parent, instantiateInWorldSpace, trackHandle);
            assetHandle.Completed += (handle) =>
            {
                tcs.SetResult(handle.Result);
                tcs = null;
            };
            return tcs.GetAwaiter();
        }

        /// <summary>
        /// 通过资源路径(AddressableName)异步加载一个资源并且实例化
        /// </summary>
        public ETTask<GameObject> LoadGameObjectAndInstantiateByPath(string assetPath, Vector3 position, Quaternion rotation, Transform parent = null, bool trackHandle = true)
        {
            ETTask<GameObject> tcs = ETTask<GameObject>.Create(true);
            AsyncOperationHandle<GameObject> assetHandle = Addressables.InstantiateAsync(assetPath, position, rotation, parent, trackHandle);
            assetHandle.Completed += (handle) =>
            {
                tcs.SetResult(handle.Result);
                tcs = null;
            };
            return tcs.GetAwaiter();
        }

        /// <summary>
        /// 通过资源路径(AddressableName)异步加载一个资源并且实例化，需要卸载用这个
        /// </summary>
        public ETTask<GameObject> LoadGameObjectAndInstantiateByPath(string assetPath, out AsyncOperationHandle<GameObject> assetHandle, Vector3 position, Quaternion rotation, Transform parent = null, bool trackHandle = true)
        {
            ETTask<GameObject> tcs = ETTask<GameObject>.Create(true);
            assetHandle = Addressables.InstantiateAsync(assetPath, position, rotation, parent, trackHandle);
            assetHandle.Completed += (handle) =>
            {
                tcs.SetResult(handle.Result);
                tcs = null;
            };
            return tcs.GetAwaiter();
        }

        /// <summary>
        /// 通过一个Label异步加载多个资源
        /// </summary>
        /// <param name="label">想要加载的物体的Label</param>
        /// <param name="callBack">每加载完成一个执行回调</param>
        /// <returns>返回符合Label条件的所有资源</returns>
        public ETTask<List<T>> LoadAssetsByLabelAsync<T>(string label, Action<T> callBack) where T : UnityEngine.Object
        {
            ETTask<List<T>> tcs = ETTask<List<T>>.Create(true);
            AsyncOperationHandle<IList<T>> assetHandle = Addressables.LoadAssetsAsync<T>(label, callBack);

            assetHandle.Completed += (handle) =>
            {
                tcs.SetResult(handle.Result.ToList<T>());
                tcs = null;
            };
            return tcs.GetAwaiter();
        }

        /// <summary>
        /// 通过一个Label异步加载多个资源
        /// </summary>
        /// <param name="label">想要加载的物体的Label</param>
        /// <param name="callBack">每加载完成一个执行回调</param>
        /// <returns>返回符合Label条件的所有资源</returns>
        public ETTask<List<T>> LoadAssetsByLabelAsync<T>(string label, out AsyncOperationHandle<IList<T>> assetHandle, Action<T> callBack) where T : UnityEngine.Object
        {
            ETTask<List<T>> tcs = ETTask<List<T>>.Create(true);
            assetHandle = Addressables.LoadAssetsAsync<T>(label, callBack);
            assetHandle.Completed += (handle) =>
            {
                tcs.SetResult(handle.Result.ToList<T>());
                tcs = null;
            };
            return tcs.GetAwaiter();
        }

        /// <summary>
        /// 通过多个Label异步加载多个资源
        /// </summary>
        /// <param name="labels">想要加载的物体的所有Label</param>
        /// <param name="callBack">每加载完成一个执行回调</param>
        /// <param name="mergeMode">Label合并模式</param>
        /// <returns>返回符合Label条件的所有资源</returns>
        public ETTask<List<T>> LoadAssetsByLabelAsync<T>(List<string> labels, Action<T> callBack, Addressables.MergeMode mergeMode = Addressables.MergeMode.None) where T : UnityEngine.Object
        {
            ETTask<List<T>> tcs = ETTask<List<T>>.Create(true);
            AsyncOperationHandle<IList<T>> assetHandle = Addressables.LoadAssetsAsync<T>(labels, callBack, mergeMode);
            assetHandle.Completed += (handle) =>
            {
                tcs.SetResult(handle.Result.ToList<T>());
                tcs = null;
            };
            return tcs.GetAwaiter();
        }

        /// <summary>
        /// 通过多个Label异步加载多个资源，需要卸载用这个
        /// </summary>
        /// <param name="labels">想要加载的物体的所有Label</param>
        /// <param name="callBack">每加载完成一个执行回调</param>
        /// <param name="mergeMode">Label合并模式</param>
        /// <returns>返回符合Label条件的所有资源</returns>
        public ETTask<List<T>> LoadAssetsByLabelAsync<T>(List<string> labels, out AsyncOperationHandle<IList<T>> assetHandle, Action<T> callBack, Addressables.MergeMode mergeMode = Addressables.MergeMode.None) where T : UnityEngine.Object
        {
            ETTask<List<T>> tcs = ETTask<List<T>>.Create(true);
            assetHandle = Addressables.LoadAssetsAsync<T>(labels, callBack, mergeMode);
            assetHandle.Completed += (handle) =>
            {
                tcs.SetResult(handle.Result.ToList<T>());
                tcs = null;
            };
            return tcs.GetAwaiter();
        }

        /// <summary>
        /// 通过场景路径(AddressableName)异步加载场景
        /// </summary>
        /// /// <param name="scenePath">场景资源的路径(AddressableName)</param>
        /// <param name="sceneInstanceHandle">场景加载的句柄，卸载的时候用</param>
        /// <param name="activateOnLoad">加载后是否立刻激活，不等于SceneManager.SetActiveScene()，如果不激活会影响其它资源异步加载完成后的回调</param>
        /// <returns>返回SceneInstance数据，可以通过这个直接获取到</returns>
        public ETTask<SceneInstance> LoadSceneByPathAsync(string scenePath, out AsyncOperationHandle<SceneInstance> sceneInstanceHandle, UnityEngine.SceneManagement.LoadSceneMode loadMode = UnityEngine.SceneManagement.LoadSceneMode.Single, bool activateOnLoad = true, int priority = 100)
        {
            ETTask<SceneInstance> tcs = ETTask<SceneInstance>.Create();
            sceneInstanceHandle = Addressables.LoadSceneAsync(scenePath, loadMode, activateOnLoad, priority);
            sceneInstanceHandle.Completed += (handle) =>
            {
                SceneInstance sceneInstance = handle.Result;
                tcs.SetResult(sceneInstance);
            };
            return tcs.GetAwaiter();
        }

        /// <summary>
        /// 激活加载的场景
        /// </summary>
        public ETTask ActivateLoadScene(SceneInstance sceneInstance)
        {
            ETTask tcs = ETTask.Create();
            AsyncOperation asyncOperation = sceneInstance.ActivateAsync();
            asyncOperation.completed += (operation) =>
            {
                tcs.SetResult();
            };
            return tcs.GetAwaiter();
        }
        public ETTask AddFGUIPackageAsync(string packageName)
        {
            ETTask tcs = ETTask.Create(true);
            AsyncOperationHandle<TextAsset> assetHandle = Addressables.LoadAssetAsync<TextAsset>(packageName);
            
            assetHandle.Completed += (handle) =>
            {
                UIPackage.AddPackage(handle.Result.bytes, packageName, OnTextureLoadComplete);
                Addressables.Release(handle);
                tcs.SetResult();
            };
            return tcs.GetAwaiter();
        }
        public static async void OnTextureLoadComplete(string name, string extension, System.Type type, PackageItem item)
        {
            if (item.type == PackageItemType.Atlas)
            {
                Texture t = await Addressables.LoadAssetAsync<Texture>(name.Replace("_fui","")).Task;
                item.owner.SetItemAsset(item, t, DestroyMethod.Custom);
            }
        }
        /// <summary>
        /// 通过句柄卸载Addressables资源
        /// </summary>
        public void UnLoadAsset(AsyncOperationHandle handle)
        {
            Addressables.Release(handle);
        }

        /// <summary>
        /// 通过泛型句柄卸载Addressables资源
        /// </summary>
        public void UnLoadAsset<T>(AsyncOperationHandle<T> handle) where T : UnityEngine.Object
        {
            Addressables.Release<T>(handle);
        }

        /// <summary>
        /// 通过加载的资源的引用卸载资源
        /// </summary>
        public void UnLoadAsset<T>(T assets) where T : UnityEngine.Object
        {
            Addressables.Release(assets);
        }

        /// <summary>
        /// 通过句柄卸载实例化加载二合一创建的资源
        /// </summary>
        public void UnLoadInstanceAsset(AsyncOperationHandle handle)
        {
            Addressables.ReleaseInstance(handle);
        }

        /// <summary>
        /// 通过句柄卸载实例化加载二合一创建的资源
        /// </summary>
        public void UnLoadInstanceAsset(AsyncOperationHandle<GameObject> handle)
        {
            Addressables.ReleaseInstance(handle);
        }

        /// <summary>
        /// 卸载实例化加载二合一创建的资源
        /// </summary>
        /// <param name="instanceObj">实例化的Gameobject</param>
        public void UnLoadInstanceAsset(GameObject instanceObj)
        {
            Addressables.ReleaseInstance(instanceObj);
        }

        /// <summary>
        /// 通过场景加载的句柄异步卸载场景
        /// </summary>
        public ETTask UnLoadSceneAsync(AsyncOperationHandle asyncOperationHandle)
        {
            ETTask tcs = ETTask.Create();
            Addressables.UnloadSceneAsync(asyncOperationHandle).Completed += (handle) =>
            {
                tcs.SetResult();
            };
            return tcs.GetAwaiter();
        }

        /// <summary>
        /// 通过场景加载的句柄泛型异步卸载场景
        /// </summary>
        public ETTask UnLoadSceneAsync<T>(AsyncOperationHandle<T> asyncOperationHandle)
        {
            ETTask tcs = ETTask.Create();
            Addressables.UnloadSceneAsync(asyncOperationHandle).Completed += (handle) =>
            {
                tcs.SetResult();
            };
            return tcs.GetAwaiter();
        }

        /// <summary>
        /// 通过场景实例化数据卸载场景
        /// </summary>
        public ETTask UnLoadSceneAsync(SceneInstance sceneInstance)
        {
            ETTask tcs = ETTask.Create();
            Addressables.UnloadSceneAsync(sceneInstance).Completed += (handle) =>
            {
                tcs.SetResult();
            };
            return tcs.GetAwaiter();
        }
    }
}

