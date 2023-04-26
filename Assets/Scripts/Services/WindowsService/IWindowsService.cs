using StaticData.Windows;
using UI.Base;

namespace Services.WindowsService
{
    public interface IWindowsService : IService
    {
        BaseWindow Open(WindowsID id);
    }
}
