using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Goal")) Debug.Log("GOAL !!!");

        else if (collision.gameObject.CompareTag("GameOver")) Debug.Log("!!! Game Over !!!");
    }
}
