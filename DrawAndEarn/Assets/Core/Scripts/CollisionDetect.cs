using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetect : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            Debug.Log("GOAL !!!");
            _gameManager.Continue();
            gameObject.SetActive(false);
        }


        else if (collision.gameObject.CompareTag("GameOver")) 
        {
            Debug.Log("!!! Game Over !!!");
            _gameManager.GameOver();
            gameObject.SetActive(false);
        }
        
    }
}
