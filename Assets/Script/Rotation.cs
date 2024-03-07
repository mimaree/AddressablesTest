using UnityEngine;

public class Rotation : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.Rotate(0, 70f * Time.deltaTime, 0);
    }
}
