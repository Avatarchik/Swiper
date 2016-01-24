using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour
{
	public Transform Target;
	public float damping = 1;
	public float lookAheadFactor = 3;
	public float lookAheadReturnSpeed = 0.5f;
	public float lookAheadMoveThreshold = 0.1f;
	public bool  lockXAxis = false;
	
	private float m_OffsetZ;
	private Vector3 m_LastTargetPosition;
	private Vector3 m_CurrentVelocity;
	private Vector3 m_LookAheadPos;

	
	// Use this for initialization
	private void Start()
	{
	    if (Target != null)
	    {
	        transform.position = new Vector3(Target.position.x, Target.position.y, transform.position.z);
	        m_LastTargetPosition = Target.position;
	        m_OffsetZ = (transform.position - Target.position).z;
	        transform.parent = null;

	        var cam = GetComponent<Camera>();
	        if (cam != null)
	        {
	            cam.orthographicSize = 8f;
	        }
	    }
	}
	
	
	// Update is called once per frame
	private void Update()
	{
	    if (Target != null)
	    {
	        // only update lookahead pos if accelerating or changed direction
	        float xMoveDelta = (Target.position - m_LastTargetPosition).x;
	        float xPos = transform.position.x;

	        bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

	        if (updateLookAheadTarget)
	        {
	            m_LookAheadPos = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
	        }
	        else
	        {
	            m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
	        }

	        Vector3 aheadTargetPos = Target.position + m_LookAheadPos + Vector3.forward*m_OffsetZ;
	        Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

	        if (lockXAxis)
	        {
	            newPos.x = xPos;
	        }

	        transform.position = newPos;

	        m_LastTargetPosition = Target.position;
	    }
	}
}