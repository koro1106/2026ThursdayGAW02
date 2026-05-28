using UnityEngine;
/// <summary>
/// 敵HP管理
/// </summary>
public class Enemy : MonoBehaviour
{
    [Header("敵HP")]
    [SerializeField] int hp = 30;
    [SerializeField] GameManager gameManager;
    /// <summary>
    /// ダメージ受ける
    /// </summary>
    public void TakeDamage(int damage)
    {
        hp -= damage;

        Debug.Log("敵HP : " + hp);

        // HP0以下で消える
        if (hp <= 0)
        {
            gameManager.EnemyDefeated();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Playerに当たった
        if (collision.gameObject.CompareTag("Player"))
        {
            // PlayerMove取得
            PlayerMove player =
                collision.gameObject.GetComponent<PlayerMove>();

            // Playerがあるならダメージ取得
            if (player != null)
            {
                TakeDamage(player.attackPower);
            }
        }
    }
}
