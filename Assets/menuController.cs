using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuController : MonoBehaviour
{
    [SerializeField] private Text txt;

    private int point;
    void Start()
    {
        point = PlayerPrefs.GetInt("point");
        txt.text = point.ToString();
    }

    public void ContinueGame()
    {
        if (PlayerPrefs.GetInt("lastLevel") != 0)
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("lastLevel"));
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
