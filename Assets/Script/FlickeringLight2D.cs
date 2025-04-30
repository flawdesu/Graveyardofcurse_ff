using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlickeringLight2D : MonoBehaviour
{
    public Light2D light2D;

    [Header("Flicker Settings")]
    public float minIntensity = 0.3f;
    public float maxIntensity = 1f;
    public float flickerSpeed = 0.1f; // ความเร็วในการเปลี่ยนความสว่าง
    public bool useRandom = true;     // ใช้สุ่มหรือแค่เปิด-ปิด

    private float timer;

    void Start()
    {
        if (light2D == null)
            light2D = GetComponent<Light2D>();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            if (useRandom)
            {
                light2D.intensity = Random.Range(minIntensity, maxIntensity);
                timer = Random.Range(flickerSpeed * 0.5f, flickerSpeed * 1.5f);
            }
            else
            {
                light2D.enabled = !light2D.enabled;
                timer = flickerSpeed;
            }
        }
    }
}

