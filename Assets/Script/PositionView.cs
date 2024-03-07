using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionView : MonoBehaviour
{
  private void LateUpdate()
  {
    transform.position = new Vector3(transform.position.x + Time.deltaTime, 1, 1);
  }
}
