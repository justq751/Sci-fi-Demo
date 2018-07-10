using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookX : MonoBehaviour
{
    private float _mouseX;
    [SerializeField] private float _sensitivity = 1f;

    void Start()
    {
		
	}
	
	void Update()
    {
        _mouseX = Input.GetAxis("Mouse X");
        Vector3 newRotation = transform.localEulerAngles;
        newRotation.y += _mouseX * _sensitivity;
        transform.localEulerAngles = newRotation;
    }
}
