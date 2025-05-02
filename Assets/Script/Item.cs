using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private string itemName;

    [SerializeField]
    private int quantity;

    [SerializeField]
    private Sprite sprite;

    [TextArea]
    [SerializeField]
    private string itemDescription;

    private InventoryManager inventoryManager;
    private bool isPlayerInRange = false; // ตรวจว่าผู้เล่นอยู่ในระยะหรือยัง

    void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    void Update()
    {
        // ถ้า Player อยู่ในระยะ และกดปุ่ม E
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            int leftOverItems = inventoryManager.AddItem(itemName, quantity, sprite, itemDescription); // ส่งครบ 4 พารามิเตอร์
            if (leftOverItems <= 0)
            {
                Destroy(gameObject); // ลบไอเท็มออกจากเกมหลังจากเพิ่มใน Inventory
            }
            else
            {
                quantity = leftOverItems; // ปรับจำนวนที่เหลือ
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true; // ตั้งค่าว่าผู้เล่นอยู่ในระยะ
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false; // ออกจากระยะ
        }
    }
}
