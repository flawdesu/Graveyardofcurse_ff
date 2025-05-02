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
    private bool isPlayerInRange = false;

    void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            int leftOverItems = inventoryManager.AddItem(itemName, quantity, sprite, itemDescription);

            // ✅ เรียก Analytics แบบแยกตามชื่อ item
            if (leftOverItems < quantity && AnalyticsManager.Instance != null)
            {
                if (itemName == "PainKiller")
                {
                    AnalyticsManager.Instance.LogPainKillerCollected();
                }
                else if (itemName == "Note")
                {
                    AnalyticsManager.Instance.LogNoteCollected();
                }
            }

            if (leftOverItems <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                quantity = leftOverItems;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
