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

    public static int ballCount;
    public static int ballCountNumber;

    private void Start()
    {
        ballCountNumber = 0;
        ballCount = 0;
    }
    public void StartGame()
    {
        StartCoroutine(BallLauncherSystem());
    }
    IEnumerator BallLauncherSystem()
    {
        while (true)
        {
            if (!canContinue)
            {
                yield return new WaitForSeconds(.5f);

                if (ballCountNumber != 0 && ballCountNumber % 5 == 0)
                {
                    for (int i = 0; i < 2; i++)

                    {
                        BallLaunchSettings();
                    }


                    ballCount = 2;
                    ballCountNumber++;
                }
                else
                {
                    BallLaunchSettings();

                    ballCount = 1;
                    ballCountNumber++;
                }



                yield return new WaitForSeconds(.7f);

                randomBucketPointIndex = Random.Range(0, bucketSpawnPos.Length - 1);

                bucket.transform.position = bucketSpawnPos[randomBucketPointIndex].transform.position;

                bucket.SetActive(true);

                canContinue = true;
                Invoke("BallCheck", 5f);
            }
            else
            {
                yield return null;
            }
        }
    }


    public void Continue()
    {
        if (ballCount == 1)
        {
            canContinue = false;
            bucket.SetActive(false);
            CancelInvoke();
            ballCount--;
        }
        else
        {
            ballCount--;
        }

    }
    private Vector3 GetPosition(float getAngle)
    {
        return Quaternion.AngleAxis(GetAngle(70f, 110f), Vector3.forward) * Vector3.right;
    }
    private float GetAngle(float value1, float value2)
    {
        return Random.Range(value1, value2);
    }
    void BallCheck()
    {
        if (canContinue) GetComponent<GameManager>().GameOver();

    }
    private void BallLaunchSettings()
    {
        balls[activeBallIndex].transform.position = ballLauncherCenter.transform.position;
        balls[activeBallIndex].SetActive(true);
        balls[activeBallIndex].gameObject.GetComponent<Rigidbody2D>().AddForce(750 * GetPosition(GetAngle(70f, 110f)));


        if (activeBallIndex != balls.Length - 1) activeBallIndex++;

        else activeBallIndex = 0;
    }
}
