using Services.WindowsService;
using StaticData.Windows;
using UI.Base;

namespace Services.UIFactory
{
    public interface IUIFactory : IService
    {
        BaseWindow Create(WindowsID id, IWindowsService windowsService);
        void CreateUIRoot();
    }
}

