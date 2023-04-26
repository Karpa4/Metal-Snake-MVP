using Services.UIFactory;
using StaticData.Windows;
using UI.Base;

namespace Services.WindowsService
{
  public class WindowsService : IWindowsService
  {
    private readonly IUIFactory uiFactory;

    public WindowsService(IUIFactory uiFactory)
    {
      this.uiFactory = uiFactory;
    }
    public BaseWindow Open(WindowsID id) => 
      uiFactory.Create(id, this);
  }
}