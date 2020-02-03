using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreChangeHandler : MonoBehaviour
{
    [SerializeField] Text ScoreText;
    private void Start()
    {
        ScoreText.text = "Score: 0";
        DataController.instance.onScoreChange.AddListener(OnScoreChangeHandler);
    }
    private void OnScoreChangeHandler()
    {
        ScoreText.text = string.Format("Score: " + DataController.instance.Score.ToString());
    }
}
