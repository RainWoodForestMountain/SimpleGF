using UnityEngine;

namespace GameFramework
{
    public class PlantformModule : ModuleBase
    {
        private PlantformSolution solution;
        private GameObject gameObject;

        public override void Init(long _id)
        {
            id = _id;
            gameObject = new GameObject("GameFramework.PlantformModule");
            GameObject.DontDestroyOnLoad(gameObject);
            solution = gameObject.AddComponent<PlantformSolution>();

            MessageModule.instance.AddListener(MessageType.PlatformRequest, solution.SendTo);
        }
        public override void Destroy()
        {
            GameObject.Destroy(gameObject);
            MessageModule.instance.RemoveListener(MessageType.PlatformRequest, solution.SendTo);
        }
    }
}