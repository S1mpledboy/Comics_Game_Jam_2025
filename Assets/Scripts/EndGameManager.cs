using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _endScore, _endTime;

    private void OnEnable()
    {
        _endScore.text = CharacterController.score.ToString("f1");
        _endTime.text = CharacterController.timeToEndGame.ToString("f3");
        Time.timeScale = 0;
    }
}
