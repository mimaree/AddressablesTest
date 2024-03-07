using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalerView : MonoBehaviour
{
  private void LateUpdate()
  {
    transform.localScale = new Vector3(transform.localScale.x + Time.deltaTime, 1, 1);
  }
}
