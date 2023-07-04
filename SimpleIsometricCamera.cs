using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
    **      simple janky script for isometric* orthograpgic camera 
    **      HOW TO USE: 
    **          Simply put the script on the camera 
    **      TODO:
    **          -fix the effect of resolution and aspect ratio on the speed of camera movement
    **          (possibly unfixable without jank due to camera being a gameObject)
    **
    **                                                                  -novff
*/
public class SimpleIsometricCamera : MonoBehaviour
{     
    private Camera _cam;
    [SerializeField][Range(0, 100)]private float _camSpeed = 30f;
    [SerializeField][Range(30, 5)]private float _camZoom = 5f;
    
    [SerializeField][Range(10, 50)]private float _screenEdgeSize = 20f;
    
    private Vector3 _movementVec;

    void Awake()
    { 
        _cam = Camera.main;
        _cam.orthographic = true;
        transform.rotation = Quaternion.Euler(35, 45, 0);
    }
    
    void Update()
    {
        _movementVec = new Vector3(0,0,0);
        if(Input.GetMouseButton(2))
        {
            _movementVec = new Vector3(-Input.GetAxisRaw("Mouse X"), 0, -Input.GetAxisRaw("Mouse Y") * 2f);
        }
        else
        {
            if(Input.mousePosition.x > Screen.width - _screenEdgeSize)
                _movementVec += new Vector3(_camSpeed, 0, 0);
            if(Input.mousePosition.x < _screenEdgeSize)
                _movementVec += new Vector3(-_camSpeed, 0, 0);
            if(Input.mousePosition.y > Screen.height - _screenEdgeSize)
                _movementVec += new Vector3(0, 0, _camSpeed * 2);
            if(Input.mousePosition.y < _screenEdgeSize)
                _movementVec += new Vector3(0, 0, -_camSpeed * 2);

            _movementVec *= Time.deltaTime; 
        }
        transform.position += Quaternion.Euler(0, 45, 0) * _movementVec * (_camZoom / 20f);
        
        _camZoom -= Input.mouseScrollDelta.y;
        _camZoom = Mathf.Clamp(_camZoom, 5f, 30f);
        _cam.orthographicSize = _camZoom;
    }
}