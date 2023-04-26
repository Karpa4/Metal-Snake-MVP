using UnityEngine;

namespace StaticData.Windows
{
  [CreateAssetMenu(fileName = "WindowsStaticData", menuName = "StaticData/UI/Create Windows Static Data", order = 52)]
  public class WindowsStaticData : ScriptableObject
  {
    public WindowInstantiateData[] InstantiateData;
  }
}