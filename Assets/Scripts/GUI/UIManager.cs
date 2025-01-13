using System;
using System.Collections.Generic;
using UnityEngine;
/*
namespace Unchord.UI
{
    public class UIManager : Singleton<UIManager>
    {
        private ResourceManager<UnchordCanvas> m_guiResourceManager;
        private Dictionary<string, UnchordCanvas> m_guiInstanceTables;
        // private Transform m_canvParent;

        #region GUI Canvas Instance Factory Properties
        public FadeCanvas CanvFadeBlack => this.GetCanvas<FadeCanvas>("Prefabs/GUIs/FadeScreen/FadeScreenBlack");
        public MenuCanvas CanvMenu => this.GetCanvas<MenuCanvas>("Prefabs/GUIs/Menu Canvas");
        public HUDCanvas CanvHUD => this.GetCanvas<HUDCanvas>("Prefabs/GUIs/HUD Canvas");
        // public OptionCanvas CanvOption => this.GetCanvas<OptionCanvas>("Prefabs/UI/Option Canvas");
        // public DialogCanvas CanvDialog => this.GetCanvas<DialogCanvas>("Prefabs/UI/Dialog Canvas");
        #endregion

        private void Awake()
        {
            m_guiResourceManager = new ResourceManager<UnchordCanvas>();
            m_guiInstanceTables = new Dictionary<string, UnchordCanvas>(8);
            // m_canvParent = GameObject.Find("GUI").transform;

            // CanvFadeStandard.FadeOutImmediate();
            // CanvLobby.Show();
            // CanvHUD.Hide();
            // CanvOption.Hide();
            // CanvDialog.Hide();
        }

        private void Update()
        {

        }

        private T_Canvas GetCanvas<T_Canvas>(string _resourcePath)
        where T_Canvas : UnchordCanvas
        {
            T_Canvas resource = null;
            string key = typeof(T_Canvas).Name;

            if (!m_guiResourceManager.CanAccess(_resourcePath))
                m_guiResourceManager.Load<T_Canvas>(_resourcePath);

            resource = m_guiResourceManager[_resourcePath] as T_Canvas;

            if (!m_guiInstanceTables.ContainsKey(key))
                // m_guiInstanceTables.Add(key, GameObject.Instantiate(resource, m_canvParent, true));
                m_guiInstanceTables.Add(key, GameObject.Instantiate(resource));

            return m_guiInstanceTables[key] as T_Canvas;
        }

#if UNITY_EDITOR || UNCHORD_DEBUG_BUILD
        public void OnDeveloperModeEnabled()
        {
            foreach(UnchordCanvas canvas in m_guiInstanceTables.Values)
                if (canvas.IsVisible)
                    canvas.OnDeveloperModeEnabled();
        }

        public void OnDeveloperModeDisabled()
        {
            foreach (UnchordCanvas canvas in m_guiInstanceTables.Values)
                if (canvas.IsVisible)
                    canvas.OnDeveloperModeDisabled();
        }
#endif
    }
}
*/