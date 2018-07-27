namespace GameFramework
{
    public class ResourceModule : ModuleBase
    {
        public static ResourceModule instance
        {
            get
            {
                return ModuleController.instance.GetModule<ResourceModule>();
            }
        }

        private IAssetLoader loader;

        public override void Init(long _id)
        {
            base.Init(_id);
            loader = new AssetSolution();
        }
        public override void Destroy()
        {
            base.Destroy();
            loader.Destroy();
        }
        public T LoadAsset<T>(string _module, string _res) where T : UnityEngine.Object
        {
            return loader.LoadAsset<T>(_module, _res);
        }
        public byte[] LoadAssetBytes<T>(string _module, string _res) where T : UnityEngine.Object
        {
            return loader.LoadAssetBytes<T>(_module, _res);
        }
        public void UnloadAsset(string _module, string _res)
        {
            loader.UnloadAsset(_module, _res);
        }
        public void Refresh ()
        {
            loader.Refresh();
        }
    }
}