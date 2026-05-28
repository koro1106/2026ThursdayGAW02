using TMPro;
using UnityEngine;
/// <summary>
/// ゲーム管理
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("ゲームクリアUI")]
    [SerializeField] GameObject clearObject;

    [Header("スコア表示")]
    [SerializeField] TMP_Text scoreText;

    // 敵の残り数
    int enemyCount;

    // 倒した数
    int defeatedCount;

    void Start()
    {
        // Enemyタグの数取得
        enemyCount =
            GameObject.FindGameObjectsWithTag("Enemy").Length;

        // 最初は非表示
        clearObject.SetActive(false);

        // 初期表示
        scoreText.text = "0";
    }

    /// <summary>
    /// 敵撃破
    /// </summary>
    public void EnemyDefeated()
    {
        // 残り敵数減少
        enemyCount--;

        // 倒した数加算
        defeatedCount++;

        Debug.Log("Score : " + defeatedCount);

        // スコア更新
        scoreText.text =
            defeatedCount.ToString();

        // 全撃破
        if (enemyCount <= 0)
        {
            clearObject.SetActive(true);

            Debug.Log("ゲームクリア");
        }
    }
}
