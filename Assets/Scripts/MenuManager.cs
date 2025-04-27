using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Playables;
public class MenuManager : MonoBehaviour
{
    [SerializeField] PlayableDirector cutScene;
    [SerializeField] GameObject cutSceneStaff;
    public void StartCutScene()
    {
        cutSceneStaff.SetActive(true);
        cutScene.Play();
    }
}