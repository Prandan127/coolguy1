using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Persistent Objects")]
    public GameObject[] persistentObjects;

    private void Awake()
    {
        if (Instance != null) 
        {
            CleanAndDestroy();
            Destroy(gameObject);
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
