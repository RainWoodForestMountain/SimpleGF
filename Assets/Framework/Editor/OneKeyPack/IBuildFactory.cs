namespace GameFramework
{
	public interface IBuildFactory
    {
        void OnPostProcessBuildEnd(string _pathToBuiltProject);
        void OnSetting(OneKeyBuildPackage _ok);
	}
}