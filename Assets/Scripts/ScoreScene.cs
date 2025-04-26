using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreScene : MonoBehaviour
{
    public void ResetGame()
    {
        SceneManager.LoadScene(2);
    }
}
