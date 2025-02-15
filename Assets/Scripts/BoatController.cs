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

    private Animator animator;
    private bool isMove = false;
    public bool canMove = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // 禁用重力
        canMove = true;
        animator = GetComponentInChildren<Animator>();
    }
    void Start()
    {
        if (GameManger.Instance.CurGameData.GetBoatLevel() == 3)
        {
            maxSpeed = 6f;
            acceleration = 3f;
            rotationSpeed = 200f;
            drag = 0.97f;
        }
        else if (GameManger.Instance.CurGameData.GetBoatLevel() == 2)
        {
            maxSpeed = 4f;
            acceleration = 2f;
            rotationSpeed = 100f;
            drag = 0.96f;
        }
        else
        {
            maxSpeed = 2f;
            acceleration = 1f;   
            rotationSpeed = 50f;
            drag = 0.95f;
        }
    }

    void Update()
    {
        if (canMove) Move();
        
    }

    void FixedUpdate()
    {
        if (canMove) FixedMove();
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
    public void StopBoat()
    {
        currentSpeed = 0;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
    }

}