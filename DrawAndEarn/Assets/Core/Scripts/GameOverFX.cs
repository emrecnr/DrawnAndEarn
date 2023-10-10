using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverFX : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnParticleSystemStopped()
    {
        Time.timeScale = 0f;
    }
}
