using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveSelector : MonoBehaviour
{
    public static SaveSelector Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject saveButtonPrefab;
    public Transform saveButtonParent;
    public SaveManager saveManager;
    public string saveFileExtension = ".xml";

    public GameObject saveLoadPanel;

    private List<string> saveFiles = new List<string>();

    private void Start()
    {
        RefreshSaveList();
    }

    public void OpenSaveLoadPanel() 
    {
        saveLoadPanel.SetActive(true);
        RefreshSaveList();
    }

    public void CloseSaveLoadPanel()
    {
        saveLoadPanel.SetActive(false);
    }

    public void RefreshSaveList()
    {
        Debug.Log("RefreshSaveList called!");

        foreach (Transform child in saveButtonParent)
        {
            Destroy(child.gameObject);
        }
        saveFiles.Clear();

        string saveDirectory = Application.persistentDataPath;
        DirectoryInfo directoryInfo = new DirectoryInfo(saveDirectory);
        FileInfo[] fileInfos = directoryInfo.GetFiles("*" + saveFileExtension);

        Debug.Log("Found " + fileInfos.Length + " save files.");

        foreach (FileInfo fileInfo in fileInfos)
        {
            string saveName = Path.GetFileNameWithoutExtension(fileInfo.Name);
            saveFiles.Add(saveName);
            GameObject saveButtonGO = Instantiate(saveButtonPrefab, saveButtonParent);

            if (saveButtonGO == null)
            {
                Debug.LogError("saveButtonGO is null! saveButtonPrefab: " + saveButtonPrefab);
                continue;
            }

            TextMeshProUGUI buttonText = saveButtonGO.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = saveName;
            }
            Button button = saveButtonGO.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() => LoadSelectedSave(saveName));
            }
        }
    }

    public void LoadSelectedSave(string saveName)
    {
        saveManager.saveFileName = saveName + saveFileExtension;
        saveManager.LoadGame();
        Debug.Log("Loading save: " + saveName);
        CloseSaveLoadPanel();
    }
}
