using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    // ITEM DATA //
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;

    // จำนวนสูงสุดที่สามารถเก็บได้ในแต่ละช่อง
    [SerializeField]
    private int maxStack = 99; // ตั้งค่าจำนวนสูงสุด (ปรับค่าตามที่ต้องการ)

    public int MaxStack => maxStack; // ค่าคงที่ที่สามารถเข้าถึงได้จากภายนอก

    // ITEM SLOT //
    [SerializeField]
    private TMP_Text quantityText;

    [SerializeField]
    private Image itemImage;

    // ITEM DES//
    public Image itemDescriptionImage;
    public TMP_Text itemDescriptionNameText;
    public TMP_Text itemDescriptionText;

    // เปลี่ยนเป็น public เพื่อให้สามารถเข้าถึงได้จากภายนอก
    public GameObject selectedShader;  // เปลี่ยนเป็น public
    public bool thisItemSelected;

    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    public void AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        this.itemName = itemName;
        this.quantity = quantity;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDescription;
        isFull = true;

        quantityText.text = quantity.ToString();
        quantityText.enabled = true;
        itemImage.sprite = itemSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    public void OnLeftClick()
    {
        if (thisItemSelected)
        {
            inventoryManager.UseItem(itemName);  // ใช้ไอเท็มที่เลือก
        }

        // ยกเลิกการเลือกไอเท็มก่อนหน้า
        inventoryManager.DeselectAllSlots();
        selectedShader.SetActive(true);  // แสดงว่ากำลังเลือกไอเท็มนี้
        thisItemSelected = true;  // ตั้งสถานะการเลือกไอเท็ม
        itemDescriptionNameText.text = itemName;  // อัพเดทชื่อไอเท็มในคำอธิบาย
        itemDescriptionText.text = itemDescription;  // อัพเดทคำอธิบายไอเท็ม
        itemDescriptionImage.sprite = itemSprite;  // อัพเดทภาพไอเท็ม
    }


    public void OnRightClick()
    {
        // เพิ่มโค้ดลบไอเท็มหรือการกระทำอื่นๆ ที่ต้องการ
    }
}
