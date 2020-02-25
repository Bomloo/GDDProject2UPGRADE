using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class feetcript : MonoBehaviour
{
    // Start is called before the first frame update


    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponentInParent<PlayerController>().isjumping = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        GetComponentInParent<PlayerController>().isjumping = true;
    }
}
