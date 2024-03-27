using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class IKFootPosition : MonoBehaviour
{
    [SerializeField] private float _rayDistanceToGround;
    [SerializeField] private LayerMask _layerMask;

    private Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (_anim == null)
        {
            return;
        }

        _anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, _anim.GetFloat("LeftFootCurve"));
        _anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1f);

        _anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);
        _anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1f);

        //Left
        RaycastHit hit;
        Ray ray = new Ray(_anim.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down);
        if(Physics.Raycast(ray, out hit, _rayDistanceToGround + 1f, _layerMask))
        {
            Vector3 footPosition = hit.point;
            footPosition.y += _rayDistanceToGround;
            _anim.SetIKPosition(AvatarIKGoal.LeftFoot, footPosition);
            _anim.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(transform.forward, hit.normal));
        }

        ray = new Ray(_anim.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, Vector3.down);
        if (Physics.Raycast(ray, out hit, _rayDistanceToGround + 1f, _layerMask))
        {
            Vector3 footPosition = hit.point;
            footPosition.y += _rayDistanceToGround;
            _anim.SetIKPosition(AvatarIKGoal.RightFoot, footPosition);
            _anim.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(transform.forward, hit.normal));
        }
    }
}
