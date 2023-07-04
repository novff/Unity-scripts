using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    **      simple camera-follows-object script for basic 2d platformers and topdown games
    **      HOW TO USE: 
    **          -put this script on a camera and specify a target in editor, play with Lerp Slider to your hearts desire
    **      TODO:
    **          -clean up the code
    **          -make it more versatile
    **                                                                  -novff
*/
public class Simple2DFollowTargetCamera : MonoBehaviour
{
    [SerializeField]private GameObject _target;
    [SerializeField][Range(1, 0.01f)]private float CameraLerpTime = 0.25F;
    private float CameraSpeed = 40F;

    void FixedUpdate() 
    {
        Vector3 _offset = transform.position;
        _offset.z = _target.transform.position.z;
        Vector3 targetDirection = _target.transform.position - _offset;
        Vector3 targetPosition = transform.position + (targetDirection.normalized * targetDirection.magnitude * CameraSpeed * Time.deltaTime); 
        transform.position = Vector3.Lerp( transform.position, targetPosition, CameraLerpTime);
    }
}