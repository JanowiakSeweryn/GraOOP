using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resume : MonoBehaviour
{
    public PauseGame pause_game;

    // Update is called once per frame
    public void ResumeGame()
    {
        pause_game.isPaused = false;
    }
}
