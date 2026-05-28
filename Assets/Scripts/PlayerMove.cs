using UnityEngine;

/// <summary>
/// プレイヤー操作
/// マウスドラッグで発射
/// </summary>
public class PlayerMove : MonoBehaviour
{
    [Header("発射威力")]
    [SerializeField] float shotPower = 10f;

    [Header("停止判定")]
    [SerializeField] float stopSpeed = 0.1f;

    [Header("軌道線")]
    [SerializeField] LineRenderer line;

    [Header("攻撃力")]
    public int attackPower = 10;
    Rigidbody2D rb;

    Camera cam;

    // ドラッグ開始位置
    Vector2 startPos;

    // ドラッグ中
    bool isDragging;

    // 発射可能
    bool canShoot = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        cam = Camera.main;
    }

    void Update()
    {
        // =========================
        // 停止判定
        // =========================
        if (!canShoot)
        {
            if (rb.linearVelocity.magnitude <= stopSpeed)
            {
                rb.linearVelocity = Vector2.zero;

                canShoot = true;
            }
        }

        WallBounce();

        // 発射できないなら終了
        if (!canShoot) return;

        // =========================
        // マウス押した
        // =========================
        if (Input.GetMouseButtonDown(0))
        {
            startPos =
                cam.ScreenToWorldPoint(
                    Input.mousePosition);

            isDragging = true;

            // 軌道線表示
            if (line != null)
            {
                line.enabled = true;
            }
        }

        // =========================
        // ドラッグ中
        // =========================
        if (isDragging)
        {
            DrawLine();
        }

        // =========================
        // マウス離した
        // =========================
        if (Input.GetMouseButtonUp(0))
        {
            Shoot();

            isDragging = false;

            // 軌道線非表示
            if (line != null)
            {
                line.enabled = false;
            }
        }

       
    }

    /// <summary>
    /// 発射
    /// </summary>
    void Shoot()
    {
        Vector2 endPos =
            cam.ScreenToWorldPoint(
                Input.mousePosition);

        // 引っ張り方向
        Vector2 dir = startPos - endPos;

        // 発射
        rb.linearVelocity = dir * shotPower;

        canShoot = false;
    }

    /// <summary>
    /// 軌道線描画
    /// </summary>
    void DrawLine()
    {
        if (line == null) return;

        Vector2 currentPos =
            cam.ScreenToWorldPoint(
                Input.mousePosition);

        Vector2 dir =
            startPos - currentPos;

        line.positionCount = 2;

        line.SetPosition(
            0,
            transform.position);

        line.SetPosition(
            1,
            (Vector2)transform.position + dir);
    }

    /// <summary>
    /// 画面端反射
    /// </summary>
    void WallBounce()
    {
        Vector2 pos = transform.position;

        Vector2 velocity = rb.linearVelocity;

        // =========================
        // 左右
        // =========================
        if (pos.x <= -6.5f)
        {
            pos.x = -6.5f;

            velocity.x *= -1;
        }
        else if (pos.x >= 6.5f)
        {
            pos.x = 6.5f;

            velocity.x *= -1;
        }

        // =========================
        // 上下
        // =========================
        if (pos.y <= -12f)
        {
            pos.y = -12f;

            velocity.y *= -1;
        }
        else if (pos.y >= 12f)
        {
            pos.y = 12f;

            velocity.y *= -1;
        }

        // 位置反映
        transform.position = pos;

        // 速度反映
        rb.linearVelocity = velocity;
    }
}