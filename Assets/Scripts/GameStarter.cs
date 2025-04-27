using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameStarter : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadScene(0);
    }
}
