using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private enum LookAtMode
    {
        LookAt,
        LookAtInverted,
        CameraForward,
        CameraForwardInverted,
    }
  [SerializeField] private LookAtMode lookAtMode;
  private void LateUpdate()
  {
      switch (lookAtMode)
      {case LookAtMode.LookAt:
          transform.LookAt(Camera.main.transform);
          break;
      case LookAtMode.LookAtInverted:
          Vector3 dirFromCamera=GetSymmetricPoint(transform.position,Camera.main.transform.position);
          transform.LookAt(dirFromCamera);
          break;    
      case LookAtMode.CameraForward:
          transform.forward=Camera.main.transform.forward;
          break;
      case LookAtMode.CameraForwardInverted:
          transform.forward = -Camera.main.transform.forward;
          break;
      }
  }

  private Vector3 GetSymmetricPoint(Vector3 pointCenter, Vector3 pointB)
  {
      return new Vector3(2*pointCenter.x-pointB.x, 2*pointCenter.y-pointB.y, 2*pointCenter.z-pointB.z);
  }
}
