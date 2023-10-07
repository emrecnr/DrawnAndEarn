using System.Collections;

using UnityEngine;


public class BallController : MonoBehaviour
{
    [SerializeField] private GameObject[] balls;
    [SerializeField] private GameObject[] bucketSpawnPos;
    [SerializeField] private GameObject ballLauncherCenter;

    [SerializeField] private GameObject bucket;
    int activeBallIndex;
    int randomBucketPointIndex;

    bool canContinue;

    private void Update()
    {
        Debug.Log("Nothing");
        if (Input.GetButtonDown("Jumps"))
        {
            Debug.Log("Space");
           
            Invoke("ActiveBucket", .5f);
        }
        
    }
    IEnumerator BallLauncherSystem()
    {
        while (true)
        {
            if (!canContinue)
            {
                yield return new WaitForSeconds(.5f);
                balls[activeBallIndex].transform.position = ballLauncherCenter.transform.position;

                balls[activeBallIndex].SetActive(true);
                float angle = Random.Range(70f, 110f);
                Vector3 pos = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;

                balls[activeBallIndex].gameObject.GetComponent<Rigidbody2D>().AddForce(pos * 750);
                if (activeBallIndex != balls.Length - 1) activeBallIndex++;

                else activeBallIndex = 0;

                yield return new WaitForSeconds(.5f);

                randomBucketPointIndex = Random.Range(0, bucketSpawnPos.Length - 1);

                bucket.transform.position = bucketSpawnPos[randomBucketPointIndex].transform.position;

                bucket.SetActive(true);

                canContinue = true;
            }
            else
            {
                yield return null;
            }
        }
    }
    private void ActiveBucket()
    {

    }
}
