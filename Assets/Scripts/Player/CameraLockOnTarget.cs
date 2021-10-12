using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraLockOnTarget : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook lockCamera;
    private List<CharacterManager> avilableTargets = new List<CharacterManager>();
    private Transform myTransform;
    private Transform currentLockOnTarget;
    private Transform nearestLockOnTarget;
    private float maximumLockOnDistance = 25;
    private bool lockOnFlag;

    private Animator animator;

    private void Start()
    {
        myTransform = this.transform;
        animator = GetComponent<Animator>();
    }

    public void TargetLock()
    {
        if (lockOnFlag == false)
        {
            ClearLockOnTargets();
            lockOnFlag = true;
            currentLockOnTarget = nearestLockOnTarget;
            HandleLockOn();

            if (nearestLockOnTarget != null)
            {
                currentLockOnTarget = nearestLockOnTarget;
                lockOnFlag = true;
            }
        }
        else if (lockOnFlag)
        {
            currentLockOnTarget = null;
            nearestLockOnTarget = null;
            lockCamera.Priority = 0;
            lockOnFlag = false;
            animator.SetBool("strafe", false);
        }
    }

    private void ClearLockOnTargets()
    {
        avilableTargets.Clear();
        nearestLockOnTarget = null;
        currentLockOnTarget = null;
    }

    public void TargetDeath()
    {
        ClearLockOnTargets();
        lockCamera.Priority = 0;
        lockOnFlag = false;
        animator.SetBool("strafe", false);
    }
    private void LateUpdate()
    {
        if (currentLockOnTarget != null)
        {
            animator.SetBool("strafe", true);
            lockCamera.Priority = 11;
            Vector3 dir = currentLockOnTarget.position - myTransform.position;
            dir.Normalize();
            dir.y = 0;
            myTransform.rotation = Quaternion.LookRotation(dir);
        }
    }

    public void HandleLockOn()
    {
        float shortestDistance = Mathf.Infinity;

        Collider[] colliders = Physics.OverlapSphere(myTransform.position, 26);

        for (int i = 0; i < colliders.Length; i++)
        {
            CharacterManager character = colliders[i].GetComponent<CharacterManager>();

            if (character != null)
            {
                Vector3 lockTargetDirection = character.transform.position - myTransform.position;
                float distanceFromTarget = Vector3.Distance(myTransform.position, character.transform.position);

                if (character.transform.root != myTransform.transform.root

                    && distanceFromTarget <= maximumLockOnDistance)
                {
                    avilableTargets.Add(character);
                }
            }
        }

        for (int k = 0; k < avilableTargets.Count; k++)
        {
            float distanceFromTarget = Vector3.Distance(myTransform.position, avilableTargets[k].transform.position);

            if (distanceFromTarget < shortestDistance)
            {
                shortestDistance = distanceFromTarget;
                nearestLockOnTarget = avilableTargets[k].lockOnTransform;
            }
        }
    }
}
