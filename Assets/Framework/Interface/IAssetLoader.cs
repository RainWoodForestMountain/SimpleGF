namespace GameFramework
{
    public interface IAssetLoader : INeedDestroy
    {
        T LoadAsset<T>(string _module, string _res) where T : UnityEngine.Object;
        byte[] LoadAssetBytes<T>(string _module, string _res) where T : UnityEngine.Object;
        void UnloadAsset(string _module, string _res);
        void Refresh();
    }
}