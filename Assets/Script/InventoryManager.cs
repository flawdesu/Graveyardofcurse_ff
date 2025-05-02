using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryMenu;
    private bool menuActivated = false;
    public ItemSlot[] itemSlot;
    public static InventoryManager Instance;

    public ItemSO[] itemSOs;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ทำให้ไม่หายตอนเปลี่ยน Scene
        }
        else
        {
            Destroy(gameObject); // ป้องกันซ้ำ
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) // กดปุ่ม I
        {
            menuActivated = !menuActivated; // สลับสถานะเปิด/ปิด
            inventoryMenu.SetActive(menuActivated);

            // หยุด/เดินเวลา
            if (menuActivated)
            {
                Time.timeScale = 0f; // หยุดเวลา
            }
            else
            {
                Time.timeScale = 1f; // เดินเวลา
            }
        }
    }

    public void UseItem(string itemName)
    {
        for (int i = 0; i < itemSOs.Length; i++)
        {
            if (itemSOs[i].itemName == itemName) // ตรวจสอบชื่อไอเท็ม
            {
                itemSOs[i].UseItem();  // ใช้ไอเท็มที่เลือก
                break;  // หยุดค้นหา
            }
        }
    }


    // เพิ่มไอเท็มลงในช่องว่าง
    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (!itemSlot[i].isFull || itemSlot[i].itemName == itemName || itemSlot[i].quantity == 0)
            {
                // เพิ่มไอเท็มลงในช่องที่มี
                int maxStack = itemSlot[i].MaxStack; // เข้าถึง MaxStack ผ่าน getter
                int currentQuantity = itemSlot[i].quantity;
                int totalQuantity = currentQuantity + quantity;

                if (totalQuantity <= maxStack)
                {
                    // ถ้าจำนวนทั้งหมดไม่เกินจำนวนสูงสุดที่เก็บได้
                    itemSlot[i].AddItem(itemName, totalQuantity, itemSprite, itemDescription);
                    return 0; // ไม่มีไอเท็มที่เหลือ
                }
                else
                {
                    // ถ้ามีไอเท็มเกิน
                    int leftOverItems = totalQuantity - maxStack;
                    itemSlot[i].AddItem(itemName, maxStack, itemSprite, itemDescription);
                    return leftOverItems; // คืนจำนวนไอเท็มที่เหลือ
                }
            }
        }
        return quantity; // คืนจำนวนไอเท็มที่ไม่ได้เพิ่ม (กรณีไม่มีช่องว่าง)
    }

    // ยกเลิกการเลือกทุกช่อง
    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false); // สามารถเข้าถึงได้แล้ว
            itemSlot[i].thisItemSelected = false;
        }
    }
}
