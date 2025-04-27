using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< Updated upstream
using UnityEngine.Playables;
public class MenuManager : MonoBehaviour
{
    [SerializeField] PlayableDirector cutScene;
    [SerializeField] GameObject cutSceneStaff;
    public void StartCutScene()
    {
        cutSceneStaff.SetActive(true);
        cutScene.Play();
=======

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
>>>>>>> Stashed changes
    }
}
