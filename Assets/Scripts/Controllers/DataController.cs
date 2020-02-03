using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DataController : MonoBehaviour
{
    public static DataController instance = null;

    [SerializeField] private GameObject snakeTile;

    private int score;
    private int bestScore;
    [SerializeField] private bool isSoundEnabled;

    public UnityEvent onScoreChange = new UnityEvent();
    public UnityEvent onBestScoreChange = new UnityEvent();
    public UnityEvent onVolumeStateChange = new UnityEvent();
    public UnityEvent onDeath = new UnityEvent();
    public UnityEvent onEat = new UnityEvent();

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            if (Score > BestScore)
            {
                BestScore = Score;
            }
            onScoreChange.Invoke();
        }
    }
    public int BestScore
    {
        get
        {
            return bestScore;
        }
        private set
        {
            bestScore = value;
            onBestScoreChange.Invoke();
            UpdateBestScore();
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        InitialiseManager();
    }

    private void InitialiseManager()
    {
        isSoundEnabled = IsSoundEnabled();
        BestScore = GetBestScore();
        Score = 0;
    }
    
    public void Restart()
    {
        Score = 0;
    }

    #region PlayerPrefs
    public void SwitchSoundState()
    {
        isSoundEnabled = !isSoundEnabled;
        PlayerPrefs.SetInt("isSoundEnabled", isSoundEnabled ? 1 : 0);
        if (isSoundEnabled && !gameObject.GetComponent<AudioSource>().isPlaying)
        {
            gameObject.GetComponent<AudioSource>().mute = false;
        }
        else
        {
            gameObject.GetComponent<AudioSource>().mute = true;
        }
        onVolumeStateChange.Invoke();
    }
    public bool IsSoundEnabled()
    {
        if (PlayerPrefs.HasKey("isSoundEnabled"))
        {
            if (PlayerPrefs.GetInt("isSoundEnabled") != 1)
            {
                gameObject.GetComponent<AudioSource>().mute = true;
            }
            return PlayerPrefs.GetInt("isSoundEnabled") == 1;
        }
        else
        {
            PlayerPrefs.SetInt("isSoundEnabled", 1);
            return true;
        }
    }
    private int GetBestScore()
    {
        if (PlayerPrefs.HasKey("bestScore"))
        {
            return PlayerPrefs.GetInt("bestScore");
        }
        else
        {
            PlayerPrefs.SetInt("bestScore", 0);
            return 0;
        }
    }
    private void UpdateBestScore()
    {
        PlayerPrefs.SetInt("bestScore", bestScore);
    }
    #endregion
}
