using UnityEngine;

namespace GameFramework
{
    public class ReporterModule : ModuleBase
    {
        private GameObject reporterObj;

        public override void Init(long _id)
        {
            base.Init(_id);
            reporterObj = GameObject.Instantiate(Resources.Load<GameObject>("ReporterModule_Obj"));
            reporterObj.name = "GameFramework.ReporterModule";
            Reporter _rp = reporterObj.GetComponent<Reporter>();
            _rp.numOfCircleToShow = Debug.isDebugBuild ? 1 : 5;
        }
        public override void Destroy()
        {
            base.Destroy();
            GameObject.Destroy(reporterObj);
        }
    }
}