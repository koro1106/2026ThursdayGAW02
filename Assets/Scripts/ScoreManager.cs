using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // スコア表示
    public TextMeshProUGUI scoreText;

    // 最終スコア表示
    public TextMeshProUGUI finalScoreText;

    // ゲームオーバー画面
    public GameObject gameOverPanel;

    // 現在スコア
    private int score = 0;

    // ゲームオーバー状態
    private bool isGameOver = false;

    void Start()
    {
        // 最初は非表示
        gameOverPanel.SetActive(false);

        // スコア更新
        UpdateScore();
    }

    void Update()
    {
        // ゲーム中だけ
        if (!isGameOver)
        {
            // 時間でスコア加算
            score += Mathf.RoundToInt(Time.deltaTime * 10);

            // 表示更新
            UpdateScore();
        }
    }

    // スコア表示更新
    void UpdateScore()
    {
        scoreText.text = "Score : " + score;
    }

    // ゲームオーバー
    public void GameOver()
    {
        isGameOver = true;

        // ゲームオーバー画面表示
        gameOverPanel.SetActive(true);

        // 最終スコア表示
        finalScoreText.text =
            "Score : " + score;
    }

    public void AddScore(int value)
    {
        // スコア加算
        score += value;

        // 表示更新
        UpdateScore();
    }
}
