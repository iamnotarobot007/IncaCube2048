
using UnityEngine;
using TMPro;
using System.IO;

public class Score : MonoBehaviour
{
  
    [SerializeField] private TextMeshProUGUI[] currScoreText;
    [SerializeField] private TextMeshProUGUI[] highScoreText;
    [SerializeField] private TextMeshProUGUI[] coinText;
  

    private int ScoreNum;
    private int HighScoreNum;
    public int CoinNum;
    public int PowerUpBombNum;

    private string scoreFilePath;

    public static Score inst;

    private void Awake()
    {
        scoreFilePath = Application.persistentDataPath + "/ScoreCube.json";
        Debug.Log(scoreFilePath);
        inst = this;
    }

    void Start()
    {
        LoadScore();
        UpdateHighScoreText();
        UpdateCoinText();

    }

    public void ScoreAdd(int addedscore)
    {
        ScoreNum += addedscore * 2;
        UpdateScoreText();

        CoinNum += 10;
        UpdateCoinText();

        if (ScoreNum > HighScoreNum)
        {
            HighScoreNum = ScoreNum;
            UpdateHighScoreText();
        }
    }


    private void UpdateScoreText()
    {
       for(int i = 0; i < currScoreText.Length; i++)
        {
            currScoreText[i].text= ScoreNum.ToString();
        }
    }

    private void UpdateHighScoreText()
    {
      
        for (int i = 0; i < highScoreText.Length; i++)
        {
            highScoreText[i].text = HighScoreNum.ToString();
        }
        SaveScore();
    }
    public void UpdateCoinText()
    {
        for (int i = 0; i < coinText.Length; i++)
        {
            coinText[i].text = CoinNum.ToString();
        }
        SaveScore();
    }
    


    public void SaveScore()
    {
        ScoreData scoreData = new ScoreData { ScoreNum = ScoreNum, HighScoreNum = HighScoreNum, CoinNum = CoinNum };
        string jsonData = JsonUtility.ToJson(scoreData);
        File.WriteAllText(scoreFilePath, jsonData);
    }

    private void LoadScore()
    {
        if (File.Exists(scoreFilePath))
        {
            string jsonData = File.ReadAllText(scoreFilePath);
            ScoreData scoreData = JsonUtility.FromJson<ScoreData>(jsonData);
            HighScoreNum = scoreData.HighScoreNum;
            CoinNum = scoreData.CoinNum;
        }
    }
    public void ResetScore()
    {
        ScoreNum = 0;
        UpdateScoreText();
    }
}

[System.Serializable]
public class ScoreData
{
    public int ScoreNum;
    public int HighScoreNum;
    public int CoinNum;
}

