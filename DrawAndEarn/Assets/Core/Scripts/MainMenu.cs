using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_InputField _nicknameInput;
    

    private void Start()
    {
        if (PlayerPrefs.HasKey("Nickname"))
        {
            SceneManager.LoadScene(1);
        }
    }


    public void StartGame()
    {
        if (string.IsNullOrEmpty(_nicknameInput.text))
        {
            Debug.LogError("Nickname boþ býrakýlamaz");
        }
        else if(_nicknameInput.text.Length<3 || _nicknameInput.text.Length > 8)
        {
            Debug.LogError("En fazla 8 en az 3 karakter giriniz");
        }
        else
        {

            PlayerPrefs.SetString("Nickname", _nicknameInput.text);
            SceneManager.LoadScene(1);
        }
       
    }
}
