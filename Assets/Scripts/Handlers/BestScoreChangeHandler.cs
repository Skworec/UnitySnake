using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestScoreChangeHandler : MonoBehaviour
{
    [SerializeField] Text BestScoreText;
    private void Start()
    {
        BestScoreText.text = string.Format("Best: " + DataController.instance.BestScore.ToString());
        DataController.instance.onBestScoreChange.AddListener(OnBestScoreChangeHandler);
    }
    private void OnBestScoreChangeHandler()
    {
        BestScoreText.text = string.Format("Best: " + DataController.instance.BestScore.ToString());
    }
}
