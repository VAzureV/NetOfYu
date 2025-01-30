/****************************************************
    文件：BoatController.cs
	作者：Azure
	功能：船只控制器
*****************************************************/

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BoatController : MonoBehaviour 
{
    public float maxSpeed = 5f;        // 最大速度
    public float acceleration = 2f;   // 加速度
    public float rotationSpeed = 200f; // 转向速度
    public float drag = 0.95f;        // 阻力系数（0-1，越接近1阻力越小）

    public Vector2 minBounds = new Vector2(-50, -50); // 左下角边界
    public Vector2 maxBounds = new Vector2(50, 50);   // 右上角边界

    private Rigidbody2D rb;
    private float currentSpeed = 0f; // 当前速度
    private float turnInput = 0f;    // 缓存转向输入

    private Camera came;

    private Animator animator;
    private bool isMove = false;
    private bool isCatch = false;

    public GameObject projectilePrefab; // 子弹预制件
    public Transform spawnPoint;        // 发射位置
    public float fireOffset = 1f;
    public Slider charceBar;        //蓄力条UI
    public float maxChargeTime = 2f;    // 最大蓄力时间
    public float minForce = 1f;          // 最小发射力度
    public float maxForce = 10f;        // 最大发射力度

    private bool isCharging = false;    // 是否正在蓄力
    private float chargeStartTime;      // 蓄力开始时间

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // 禁用重力
        
        animator = GetComponentInChildren<Animator>();
        came = Camera.main; 
    }

    void Update()
    {
        if (!isCatch) Move();
        CatchFish();
        
    }

    void FixedUpdate()
    {
        if (!isCatch) FixedMove();
    }

    void Move()
    {
        // 获取玩家输入
        float moveInput = Input.GetAxis("Vertical");   // 前进/后退
        turnInput = Input.GetAxis("Horizontal");       // 转向

        // 计算目标速度
        float targetSpeed = moveInput * maxSpeed;

        // 逐渐加速到目标速度
        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.deltaTime);

        // 更新动画状态
        isMove = Mathf.Abs(currentSpeed) > 0.05f; // 判断是否有明显速度
        animator.SetBool("isMove", isMove);    // 将状态同步到动画
    }
    void FixedMove()
    {
        // 根据当前速度移动船
        Vector2 moveDirection = transform.up * currentSpeed;
        rb.velocity = moveDirection;

        // 转向逻辑
        rb.angularVelocity = -turnInput * rotationSpeed;

        // 应用阻力效果
        rb.velocity *= drag;
        rb.angularVelocity *= drag;

        // 获取船的当前位置
        Vector2 position = rb.position;

        // 限制船的位置在边界内
        position.x = Mathf.Clamp(position.x, minBounds.x, maxBounds.x);
        position.y = Mathf.Clamp(position.y, minBounds.y, maxBounds.y);

        // 更新船的位置
        rb.position = position;
    }

    void CatchFish()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (isCatch)
            {
                isCatch = false;
                StartCoroutine(ChangeCameraSize(came.orthographicSize, 5f, 1));
            }
            else
            {
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0;
                isMove = false;
                isCatch = true;
                StartCoroutine(ChangeCameraSize(came.orthographicSize, 5.5f, 1));
            }
            
        }
        if (isCatch && Input.GetMouseButtonDown(0))
        {
            isCharging = true;
            chargeStartTime = Time.time;
       
        }
        if (Input.GetMouseButtonUp(0) && isCharging)
        {
            isCharging = false;

            // 计算蓄力时间和力度
            float chargeDuration = Mathf.Min(Time.time - chargeStartTime, maxChargeTime); // 蓄力时间不能超过最大值
            float force = Mathf.Lerp(minForce, maxForce, chargeDuration / maxChargeTime);       // 计算发射力度

            Shoot(force);
        }

    }
    private void Shoot(float force)
    {
        // 获取鼠标位置
        Vector3 mouseWorldPosition = came.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0; // 确保在 2D 平面上

        // 计算发射方向
        Vector2 direction = (mouseWorldPosition - spawnPoint.position).normalized;

        // 动态计算发射位置
        Vector3 firePosition = spawnPoint.position + (Vector3)direction * fireOffset;

        // 创建子弹并设置初始位置
        GameObject projectile = Instantiate(projectilePrefab, firePosition, Quaternion.identity);

        // 给子弹添加力
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(direction * force, ForceMode2D.Impulse); // 使用蓄力值作为力度
        }

        // 可选：销毁子弹以防止性能问题
        Destroy(projectile, 5f);
    }
    private IEnumerator ChangeCameraSize(float startSize, float endSize, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration; // 计算插值比例
            came.orthographicSize = Mathf.Lerp(startSize, endSize, t); // 插值计算
            yield return null; // 等待下一帧
        }

        came.orthographicSize = endSize; // 确保最终值是目标值
    }
}