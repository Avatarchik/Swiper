using UnityEngine;

namespace AraxisTools
{
    public class ActionsQueueOwner : MonoBehaviour
    {
        public ActionsQueue ActionsQueue { get; private set; }

        public string QueueName;

        public bool IsIndestructible;

        void Start()
        {
            if (IsIndestructible)
            {
                DontDestroyOnLoad(gameObject);
            }
            ActionsQueue = new ActionsQueue(QueueName, true);
            StartCoroutine(ActionsQueue.ProcessQueue());
        }

        void OnDestroy()
        {
            ActionsQueue.Instance = null;
        }
    }
}