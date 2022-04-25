using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class controllerScript : MonoBehaviour
{
    [SerializeField] private PlayerControl playerControl;

    private bool power = false;

    private float timer = 5;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (power == true)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                power = false;
                timer = 5;
                playerControl.verticalSpeed = playerControl.verticalSpeed / 2;
            }
        }
    }

    public void PowerUp()
    {
        if (power == false)
        {
            playerControl.verticalSpeed = playerControl.verticalSpeed * 2;
        }
        power = true;
         
    }

    public void ContinueAnotherLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
