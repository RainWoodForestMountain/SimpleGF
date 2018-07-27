namespace GameFramework
{
	public interface IModule : ICanSleep, INeedDestroy
    {
		long id { get; }
		void Init (long _id);
	}
}