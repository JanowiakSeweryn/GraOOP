using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void ExitGame()
    {
        Application.Quit();
    }

    // Update is called once per frame
    public void start_game()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }

    
}
