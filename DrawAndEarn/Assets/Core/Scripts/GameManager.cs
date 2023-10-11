using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] BallController _ballController;
    [SerializeField] LineController _lineController;

    [Header("OBJECTS")]

    [SerializeField] ParticleSystem _bucketScore;
    [SerializeField] ParticleSystem _bestScoreParticle;

    [SerializeField] private AudioSource[] _audios;

    [Header("UI")]

    [SerializeField] private GameObject[] _panels;
    [SerializeField] private TextMeshProUGUI[] _scoreTexts;

    public bool isGameOver = false;

    private int _currentScore = 0;
    private void Start()
    {
        _currentScore = 0;
        isGameOver = false;
        //Time.timeScale = 0f;
        if (PlayerPrefs.HasKey("BestScore"))
        {
            _scoreTexts[0].text = "BestScore\n" +PlayerPrefs.GetInt("BestScore").ToString();

        }
        else
        {
            PlayerPrefs.SetInt("BestScore", 0);
            _scoreTexts[0].text = "0";
        }
    }
    public void Continue(Vector2 Pos)
    {
        _bucketScore.transform.position = Pos;
        _bucketScore.gameObject.SetActive(true);
        _bucketScore.Play();

        _currentScore++;
        _audios[0].Play();
        _ballController.Continue();
        _lineController.Continue();
    }
    public void GameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            _audios[1].Play();
            _panels[1].SetActive(true);
            _panels[2].SetActive(false);
            Debug.Log("GAME OVER !!!");

            _scoreTexts[1].text = PlayerPrefs.GetInt("BestScore").ToString();
            _scoreTexts[2].text = _currentScore.ToString();

            if (_currentScore > PlayerPrefs.GetInt("BestScore"))
            {
                PlayerPrefs.SetInt("BestScore", _currentScore);
                _bestScoreParticle.gameObject.SetActive(true);
                _bestScoreParticle.Play();
            }
            _lineController.StopLine();
        }

    }

    public void CarpiIkiSkor()
    {
        int carpiiki = _currentScore * 2;
        _currentScore = carpiiki;
        _scoreTexts[2].text = _currentScore.ToString();
        if (_currentScore > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", _currentScore);
            _bestScoreParticle.gameObject.SetActive(true);
            _bestScoreParticle.Play();
        }
    }
    public void StartGame()
    {
        _panels[0].SetActive(false);
        _ballController.StartGame();
        _lineController.StartLine();
        _panels[2].SetActive(true);
    }
    public void TryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
