using System;
using UI.Base;

namespace StaticData.Windows
{
  [Serializable]
  public struct WindowInstantiateData
  {
    public WindowsID ID;
    public BaseWindow Window;
  }
}