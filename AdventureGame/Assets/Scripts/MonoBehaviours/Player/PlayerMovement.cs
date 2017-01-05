using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{

    public Animator animator;
    public NavMeshAgent navMeshAgent;
    public float inputHoldDelay = .5f;
    public float turnSpeedThreshold = .5f;
    public float speedDampTime = .1f;
    public float slowingSpeed = .175f;
    public float turnSmoothing = 15f;


    private WaitForSeconds mInputHoldWait;
    private Vector3 mTargetPosition;

    private const float mStopDistanceProportion = .1f; //within 10% stopping distance, then stop
    private const float mNavMeshSampleDistance = 4f;

    private readonly int mHashSpeedPara = Animator.StringToHash("Speed");   //matches what we put in the animator


    public void OnGroundClick(BaseEventData data)
    {
        PointerEventData pointerEventData = (PointerEventData) data;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(pointerEventData.pointerCurrentRaycast.worldPosition, out hit, mNavMeshSampleDistance,
            NavMesh.AllAreas))
        {
            mTargetPosition = hit.position;
        }
        else
        {
            mTargetPosition = pointerEventData.pointerCurrentRaycast.worldPosition;
        }

        navMeshAgent.SetDestination(mTargetPosition);
        navMeshAgent.Resume();


    }



    private void Start()
    {
        navMeshAgent.updateRotation = false;

        mInputHoldWait = new WaitForSeconds(inputHoldDelay);

        mTargetPosition = transform.position;
    }

    private void OnAnimatorMove()
    {
        //Navmesh moves the character at a speed given by the animator
        navMeshAgent.velocity = animator.deltaPosition / Time.deltaTime;    //speed = distance / time
    }

    private void Update()
    {
        if (navMeshAgent.pathPending)
        {
            return;
        }

        float speed = navMeshAgent.desiredVelocity.magnitude;

        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance * mStopDistanceProportion)
        {
            Stopping(out speed);
        }
        else if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            Slowing(out speed, navMeshAgent.remainingDistance);
        }
        else if(speed > turnSpeedThreshold)
        {
            Moving();
        }

        animator.SetFloat(mHashSpeedPara, speed, speedDampTime, Time.deltaTime);
    }

    private void Stopping(out float speed)
    {
        navMeshAgent.Stop();
        transform.position = mTargetPosition;
        speed = 0f;
    }

    private void Slowing(out float speed, float distanceToTarget)
    {
        navMeshAgent.Stop();
        transform.position = Vector3.MoveTowards(transform.position, mTargetPosition, slowingSpeed*Time.deltaTime);

        float proportionalDistance = 1f - distanceToTarget/navMeshAgent.stoppingDistance;
        speed = Mathf.Lerp(slowingSpeed, 0f, proportionalDistance);

    }

    private void Moving() //misleading - only setting rotation (movement happens in OnAnimatorMove)
    {
        Quaternion targetRotation = Quaternion.LookRotation(navMeshAgent.desiredVelocity);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSmoothing*Time.deltaTime);
    }
}
