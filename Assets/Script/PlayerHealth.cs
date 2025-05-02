using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 6;
    public int currentHealth;
    public Image[] heartImages;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    void Update()
    {
        // สำหรับการทดสอบ
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void ChangeHealth(int amount)
    {
        // เพิ่มหรือลด Health ตามจำนวนที่ได้รับจากไอเท็ม
        currentHealth += amount;

        // ป้องกันไม่ให้ Health เกิน maxHealth หรือ น้อยกว่า 0
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        else if (currentHealth < 0)
            currentHealth = 0;

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < currentHealth)
                heartImages[i].sprite = fullHeart;
            else
                heartImages[i].sprite = emptyHeart;
        }
    }

    void Die()
    {
        Debug.Log("Player Dead");
    }
}
