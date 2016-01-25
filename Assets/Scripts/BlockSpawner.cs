using UnityEngine;
using System.Collections;

public class BlockSpawner : MonoBehaviour, IGizmosable
{
    #region IGizmosable implementation

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.3f);
    }

    public void OnDrawGizmosSelected()
    {

    }

    #endregion
}
