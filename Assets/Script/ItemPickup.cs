using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public GameObject pickupPromptUI;   // UI ข้อความ "Press E to pick up"
    public GameObject itemDisplayUI;    // UI ที่จะโชว์ไอเท็มหลังเก็บ (Note, รูป, ฯลฯ)

    private bool isPlayerInRange = false;
    private bool isItemUIOpen = false;

    void Start()
    {
        pickupPromptUI.SetActive(false);
        itemDisplayUI.SetActive(false); // ปิดหน้าต่างแสดงไอเท็มไว้ก่อน
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ShowItemUI();
        }

        if (isItemUIOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseItemUI();
        }
    }

    void ShowItemUI()
    {
        Debug.Log("เก็บไอเท็มแล้ว!");
        pickupPromptUI.SetActive(false);
        itemDisplayUI.SetActive(true);
        isItemUIOpen = true;
        Time.timeScale = 0f; // หยุดเกมชั่วคราวขณะดูไอเท็ม (optional)
    }

    void CloseItemUI()
    {
        itemDisplayUI.SetActive(false);
        isItemUIOpen = false;
        Time.timeScale = 1f;
        Destroy(gameObject); // ลบ GameObject ของไอเท็มหลังปิด
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            pickupPromptUI.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            pickupPromptUI.SetActive(false);
        }
    }
}
