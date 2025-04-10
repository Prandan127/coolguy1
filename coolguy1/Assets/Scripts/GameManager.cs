using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Persistent Objects")]
    public GameObject[] persistentObjects;

    [Header("Cached References")]
    public Camera shopKeeperCam;
    public CanvasGroup shopCanvasGroup;
    public ShopManager shopManager;

    private void Awake()
    {
        if (Instance != null) 
        {
            CleanAndDestroy();
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            MarkPersistentObjects();
        }
    }

    private void MarkPersistentObjects()
    {
        foreach (GameObject obj in persistentObjects)
        {
            if (obj != null)
            { 
                DontDestroyOnLoad(obj);
            }
        }
    }
    
    private void CleanAndDestroy()
    {
        foreach (GameObject obj in persistentObjects)
        { 
            Destroy(obj);
        }
        Destroy(gameObject);
    }
}
