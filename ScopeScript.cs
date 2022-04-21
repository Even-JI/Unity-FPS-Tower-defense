using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopeScript : MonoBehaviour
{
    public Camera scopeCamera;
    private int[] scopeZoom = {20, 10, 5};
    public int currantZoomIndex;

    private void Start()
    {
        currantZoomIndex = 0;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {

            if(Input.GetAxis("Mouse ScrollWheel") > 0 && currantZoomIndex != scopeZoom.Length -1)
            {
                currantZoomIndex ++;
                scopeCamera.fieldOfView = scopeZoom[currantZoomIndex];
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0 && currantZoomIndex != 0)
            {
                currantZoomIndex--;
                scopeCamera.fieldOfView = scopeZoom[currantZoomIndex];
            }
        }
    }
}