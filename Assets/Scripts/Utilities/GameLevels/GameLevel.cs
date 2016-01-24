using UnityEngine;
using System.Collections;

namespace AraxisTools
{
    public class GameLevel<T> : Singleton<T> where T : MonoBehaviour
    {
        public override void Awake()
        {
            base.Awake();
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}
