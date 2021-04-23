﻿using System;
using System.Collections.Generic;

namespace Jotunn.Managers
{
    internal class ZoneManager : IManager
    {
        private static ZoneManager _instance;
        public static ZoneManager Instance
        {
            get
            {
                if (_instance == null) _instance = new ZoneManager();
                return _instance;
            }
        }

        internal event EventHandler ZoneLoad;
        internal List<ZoneSystem.ZoneVegetation> Vegetation = new List<ZoneSystem.ZoneVegetation>();

        public void Init()
        {
            On.ZNetScene.Awake += RegisterAllToZNetScene;
        }

        private void RegisterAllToZNetScene(On.ZNetScene.orig_Awake orig, ZNetScene self)
        {
            orig(self);
            
            Logger.LogInfo("---- Registering custom zone data ----");

            // Call event handlers to load prefabs
            ZoneLoad?.Invoke(null, EventArgs.Empty);

            foreach (var veg in Vegetation)
            {
                ZoneSystem.instance.m_vegetation.Add(veg);
                Logger.LogInfo("Added vegetation: " + veg.m_name);
            }
        }

        internal void AddVegetation(ZoneSystem.ZoneVegetation veg)
        {
            Vegetation.Add(veg);
        }
    }
}
