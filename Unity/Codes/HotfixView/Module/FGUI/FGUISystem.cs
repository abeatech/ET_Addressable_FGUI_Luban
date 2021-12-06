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
    public class FGUIAwakeSystem : AwakeSystem<FGUI>
    {
        public override void Awake(FGUI self)
        {
            if(FGUI.Instance != null)
            {
                Debug.LogError($"已创建过{self.GetType().Name}的实例");
                return;
            }
            FGUI.Instance = self;
        }
    }
    public class FGUIDestroySystem : DestroySystem<FGUI>
    {
        public override void Destroy(FGUI self)
        {
            FGUI.Instance = null;

        }
    }
    public static class FGUISystem
    {
        private static bool _initedBasePackages = false;
        private static object _lock = new object();
        private static Dictionary<FGUIType, FGUIEntity> _uiDict = new Dictionary<FGUIType, FGUIEntity>();
        public static async void OnLoadResourceFinished(string name, string extension, System.Type type, PackageItem item)
        {
            Debug.Log($"{name}, {extension}, {type.ToString()}, {item.ToString()}");

            if (type == typeof(Texture))
            {
                Texture t = await Addressables.LoadAssetAsync<Texture>(name).Task;
                item.owner.SetItemAsset(item, t, DestroyMethod.Custom);

            }
        }

        private static async ETTask InitBasePackage()
        {
            TextAsset desc = await AddressableComponent.Instance.LoadAssetByPathAsync<TextAsset>("ABaseComponents_fui");
            UIPackage.AddPackage(desc.bytes, "ABaseComponents", OnLoadResourceFinished);
            _initedBasePackages = true;
            await Task.CompletedTask;
        }
        public static async ETTask OpenAysnc(this FGUI self,FGUIType uiType)
        {
            try
            {
                if (_uiDict.ContainsKey(uiType))
                {
                    FGUIEntity ui = _uiDict[uiType];
                    GRoot.inst.AddChild(ui.FGUI.Root);//显示到最上层
                    FGUIEventComponent.Instance.OnRefresh(ui.FGUI);
                    return;
                }
                if (!_initedBasePackages)
                {
                    await InitBasePackage();
                }
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
                        //如果没指定的类就默认用FGUIComponent
                        type = typeof(FGUIComponent);
                    }
                    FGUIEntity view = self.AddChild<FGUIEntity,Type>(type);
                    FGUIComponent component = view.AddComponent(type) as FGUIComponent;
                    if(component == null)
                    {
                        Log.Error($"打开UI错误，类型为空: {type.Name}");
                    }
                    component.Root = gCom;
                    component.uiType = uiType;
                    FGUIEventComponent.Instance.OnCreate(component);
                    FGUIEventComponent.Instance.OnShow(component);
                    _uiDict.Add(uiType, view);
                });
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        public static void Close(this FGUI self,FGUIType uiType)
        {
            try
            {
                if (!_uiDict.ContainsKey(uiType))
                {
                    return;
                }

                FGUIEntity ui = _uiDict[uiType];
                UnityEngine.Debug.Log($"=================NULL?{ui == null}");
                UnityEngine.Debug.Log($"================22=NULL?{ui.FGUI == null}");
                FGUIEventComponent.Instance.OnHide(ui.FGUI);
                FGUIEventComponent.Instance.OnDestroy(ui.FGUI);
                GRoot.inst.RemoveChild(ui.FGUI.Root, true);
                ui.Dispose();
                _uiDict.Remove(uiType);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        public static void RefreshAll(this FGUI self)
        {
            foreach (FGUIEntity ui in _uiDict.Values)
            {
                FGUIEventComponent.Instance.OnRefresh(ui.FGUI);
            }
        }
        public static void CloseAll(this FGUI self)
        {
            foreach (FGUIEntity ui in _uiDict.Values)
            {
                self.Close(ui.FGUI.uiType);
            }
        }
    }
}
