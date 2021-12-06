using Cfg.Fgui;
using FairyGUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ET
{
    public class FGUIAwakeSystem : AwakeSystem<FGUIComponent>
    {
        public override void Awake(FGUIComponent self)
        {
            if(FGUIComponent.Instance != null)
            {
                Debug.LogError($"已创建过{self.GetType().Name}的实例");
                return;
            }
            FGUIComponent.Instance = self;
        }
    }
    public class FGUIDestroySystem : DestroySystem<FGUIComponent>
    {
        public override void Destroy(FGUIComponent self)
        {
            FGUIComponent.Instance = null;

        }
    }
    public static class FGUISystem
    {
        public static async void OnLoadResourceFinished(string name, string extension, System.Type type, PackageItem item)
        {
            Debug.Log($"{name}, {extension}, {type.ToString()}, {item.ToString()}");

            if (type == typeof(Texture))
            {
                Texture t = await Addressables.LoadAssetAsync<Texture>(name).Task;
                item.owner.SetItemAsset(item, t, DestroyMethod.Custom);

            }
        }

        private static async ETTask InitBasePackage(this FGUIComponent self)
        {
            if (self.InitializedBasePackages)
            {
                return;
            }
            TextAsset desc = await AddressableComponent.Instance.LoadAssetByPathAsync<TextAsset>("ABaseComponents_fui");
            UIPackage.AddPackage(desc.bytes, "ABaseComponents", OnLoadResourceFinished);
            self.InitializedBasePackages = true;
            await Task.CompletedTask;
        }
        public static async ETTask OpenAysnc(this FGUIComponent self,FGUIType uiType)
        {
            try
            {
                if (self.UIDict.ContainsKey(uiType))
                {
                    FGUIEntity entity = self.UIDict[uiType];
                    GRoot.inst.AddChild(entity.GObject);//显示到最上层
                    self.Event.OnRefresh(entity);
                    return;
                }
                await self.InitBasePackage();
                
                FguiConfig config = ConfigUtil.Tables.TbFguiConfig.Get((int)uiType);
                await AddressableComponent.Instance.AddFGUIPackageAsync(config.Path);
                GComponent gCom = null;
                UIPackage.CreateObjectAsync(config.PackageName, config.ComponentName, (go) =>
                {
                    gCom = go as GComponent;
                    gCom.sortingOrder = config.Layer * 100;
                    gCom.displayObject.name = config.ComponentName;
                    GRoot.inst.AddChild(gCom);
                    Type type = Type.GetType(config.ClassName);
                    if(type == null)
                    {
                        //如果没指定的类就默认用UIDefaultComponent
                        type = typeof(UIDefaultComponent);
                    }
                    FGUIEntity entity = self.AddChild<FGUIEntity, Type, FGUIType>(type, uiType);
                    Entity component = entity.AddComponent(type);
                    if(component == null)
                    {
                        Log.Error($"打开UI错误，类型为空: {type.Name}");
                    }
                    entity.AddComponent<GObjectComponent, GObject>(gCom);
                    FGUIHelper.BindRoot(type, component, gCom);
                    self.Event.OnCreate(entity);
                    self.Event.OnShow(entity);
                    self.UIDict.Add(uiType, entity);
                });
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        public static void Close(this FGUIComponent self,FGUIType uiType)
        {
            try
            {
                if (!self.UIDict.ContainsKey(uiType))
                {
                    return;
                }

                FGUIEntity entity = self.UIDict[uiType];
                self.Event.OnHide(entity);
                self.Event.OnDestroy(entity);
                GRoot.inst.RemoveChild(entity.GObject, true);
                entity.Dispose();
                self.UIDict.Remove(uiType);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        public static void RefreshAll(this FGUIComponent self)
        {
            foreach (FGUIEntity entity in self.UIDict.Values)
            {
                self.Event.OnRefresh(entity);
            }
        }
    }
}
