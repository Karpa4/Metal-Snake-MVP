using System.Collections.Generic;
using System.Linq;
using StaticData.Windows;
using UnityEngine;

namespace Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<WindowsID, WindowInstantiateData> windows;

        public void Load()
        {
            windows = Resources
              .Load<WindowsStaticData>(ConstantVariables.WindowsStaticDataPath)
              .InstantiateData
              .ToDictionary(x => x.ID, x => x);

            Resources.UnloadUnusedAssets();
        }

        public WindowInstantiateData ForWindow(WindowsID windowId)
        {
            Debug.Log("ForWindow");
            return windows.TryGetValue(windowId, out WindowInstantiateData staticData)
            ? staticData
            : new WindowInstantiateData();
        }
          
    }
}