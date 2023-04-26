using StaticData.Windows;

namespace Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void Load();
        WindowInstantiateData ForWindow(WindowsID windowId);
    }
}