using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); // เพิ่มการดึง Animator
    }

    void Update()
    {
        // รับ input จาก WASD หรือปุ่มลูกศร
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // ป้องกันการเดินทะแยงเร็วเกินไป
        movement = movement.normalized;

        // กลับหน้าซ้าย/ขวา (เฉพาะแนวนอน)
        if (movement.x != 0)
        {
            spriteRenderer.flipX = movement.x < 0;
        }

        // ส่งค่าการเคลื่อนที่ให้ Animator
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude); // ใช้ sqrMagnitude เพื่อให้ Speed = 0 ตอนไม่เดิน
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
