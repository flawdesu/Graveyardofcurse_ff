using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itemName;

    // ใช้ enum สำหรับการเปลี่ยนค่าของ stat
    public StatToChange statToChange = StatToChange.None;
    public int amountToChangeStat;

    // ใช้ enum สำหรับการเปลี่ยนค่าของ attribute
    public AttributesToChange attributeToChange = AttributesToChange.None;
    public int amountToChangeAttribute;

    public void UseItem()
    {
        // หา Player ที่มีสคริปต์ PlayerHealth
        GameObject player = GameObject.Find("Player"); // ค้นหาวัตถุ Player
        if (player != null)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>(); // รับ Component PlayerHealth
            if (playerHealth != null)
            {
                if (statToChange == StatToChange.health)
                {
                    playerHealth.ChangeHealth(amountToChangeStat); // เรียกฟังก์ชัน ChangeHealth
                }
            }
            else
            {
                Debug.LogError("PlayerHealth component not found on Player.");
            }
        }
        else
        {
            Debug.LogError("Player GameObject not found.");
        }
    }


    public enum StatToChange
    {
        None,
        health,
        mana,
        stamina
    }

    public enum AttributesToChange
    {
        None,
        strength,
        defense,
        intelligence,
        agility
    }
}
