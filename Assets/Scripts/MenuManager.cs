using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< Updated upstream
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
=======
>>>>>>> Stashed changes

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
    }
}
