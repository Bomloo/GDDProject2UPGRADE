using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided");
        if (collision.CompareTag("Player"))
        {
            GameObject gm = GameObject.FindWithTag("GameController");
            gm.GetComponent<GameManager>().WinGame();
        }
    }
}
