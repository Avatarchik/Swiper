using UnityEngine;
using System.Collections;

public class DebugElement : MonoBehaviour
{

    void Awake()
    {
#if RELEASE_INSTANCE_TRUE
        Destroy(gameObject);
#endif
    }

}
