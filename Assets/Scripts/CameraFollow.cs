using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Transform playertrans;
    private void LateUpdate()
    {
        Vector3 newpos = playertrans.position;
        newpos.z = transform.position.z;
        newpos.y = newpos.y + 0.2f;
        transform.position = newpos;
    }
}
