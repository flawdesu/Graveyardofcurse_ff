using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;

public class AnalyticsManager : MonoBehaviour
{
    public static AnalyticsManager Instance;

    private float sessionStartTime;
    private Dictionary<string, int> itemCollectionCount = new Dictionary<string, int>();

    private async void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            await Initialize();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private async System.Threading.Tasks.Task Initialize()
    {
        await UnityServices.InitializeAsync();
        AnalyticsService.Instance.StartDataCollection();
    }

    private void Start()
    {
        sessionStartTime = Time.time;
    }

    private void OnApplicationQuit()
    {
        SendPlayTimeEvent();
    }

    private void SendPlayTimeEvent()
    {
        float sessionDuration = Time.time - sessionStartTime;

        CustomEvent playTime = new CustomEvent("PlayTime")
        {
            { "Player_1", sessionDuration }
        };

        AnalyticsService.Instance.RecordEvent(playTime);
        Debug.Log($"⏱️ PlayTime Event Sent: {sessionDuration} seconds");
    }

    // ✅ ทำให้ public เพื่อเรียกใช้จาก Item.cs
    public void LogItemCollected(string itemName)
    {
        if (itemCollectionCount.ContainsKey(itemName))
        {
            itemCollectionCount[itemName]++;
        }
        else
        {
            itemCollectionCount[itemName] = 1;
        }

        int totalCount = itemCollectionCount[itemName];

        CustomEvent collectItemEvent = new CustomEvent("CollectItem")
        {
            { "ItemName", itemName },
            { "TotalCollected", totalCount },
            { "Time", Time.timeSinceLevelLoad }
        };

        AnalyticsService.Instance.RecordEvent(collectItemEvent);
        Debug.Log($"📦 CollectItem Event Sent: {itemName} | Total: {totalCount}");
    }

    public void LogPainKillerCollected()
    {
        LogItemCollected("PainKiller");
    }

    public void LogNoteCollected()
    {
        LogItemCollected("Note");
    }
}
