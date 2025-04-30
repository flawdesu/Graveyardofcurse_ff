using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnClick : MonoBehaviour
{
    public string sceneName = "Scenes1";  // ตั้งชื่อฉากที่ต้องการเปลี่ยนไป

    void OnMouseDown()
    {
        SceneManager.LoadScene(sceneName);
    }
}
