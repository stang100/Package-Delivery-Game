using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour
{
	public float positionSmoothTime = 1f;		// a public variable to adjust smoothing of camera motion
    public float rotationSmoothTime = 1f;
    public float positionMaxSpeed = 50f;        //max speed camera can move
    public float rotationMaxSpeed = 50f;
	public Transform desiredPose;			// the desired pose for the camera, specified by a transform in the game
    public Transform target;
	
    private Vector3 lastTargetPosition;
    private Vector3 lastSelfPosition;
    
    private Vector3 originalDelta;
    
    protected Vector3 currentPositionCorrectionVelocity;
    //protected Vector3 currentFacingCorrectionVelocity;
    //protected float currentFacingAngleCorrVel;
    protected Quaternion quaternionDeriv;

    protected float angle;
    
    void Start ()
    {
    	originalDelta = target.transform.position - this.transform.position;
    }
    
	void LateUpdate ()
	{

        if (desiredPose != null)
        {
        if (lastTargetPosition != null)
        	{
            transform.position = Vector3.SmoothDamp(transform.position, desiredPose.position, ref currentPositionCorrectionVelocity, positionSmoothTime, positionMaxSpeed, Time.deltaTime);

            //var targForward = desiredPose.forward;
            var targForward = (target.position - this.transform.position).normalized;
	    
	    var poseDelta = target.position - lastTargetPosition;
	    
	    this.transform.position = lastSelfPosition + poseDelta *1.3f;
	    
           transform.rotation = QuaternionUtil.SmoothDamp(transform.rotation,
                Quaternion.LookRotation(targForward, Vector3.up), ref quaternionDeriv, rotationSmoothTime);
	
        	}
        }
        lastTargetPosition = target.position;
        lastSelfPosition = this.transform.position;
    }
}
