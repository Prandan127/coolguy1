using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMPro.TMP_Dropdown resolutionDropdown;
    public GameObject BGMusic;
    public GameObject[] objs;
    public CanvasGroup settingsCanvas;

    [SerializeField] private Slider volumeSlider;
    private bool settingsOpen = false;

    private AudioSource audiosrc;
    private Resolution[] resolutions;

    private void Awake()
    {
        objs = GameObject.FindGameObjectsWithTag("Sound");
        if (objs.Length > 0 )
        {
            BGMusic = Instantiate(BGMusic);
            BGMusic.name = "BGMusic";
            DontDestroyOnLoad(BGMusic.gameObject);
        }
        else
        {
            BGMusic = GameObject.Find("BGMusic");
        }    
    }

    private void Start()
    {
        audiosrc = GetComponent<AudioSource>();

        float savedVolume = PlayerPrefs.GetFloat("Volume", 0.5f);
        volumeSlider.value = savedVolume;
        SetVolume(savedVolume);
        volumeSlider.onValueChanged.AddListener(SetVolume);


        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        var options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    private void Update()
    {
        if (Input.GetButtonDown("ToggleSettings"))
        {
            if (settingsOpen)
            {
                Time.timeScale = 1.0f;
                settingsCanvas.alpha = 0f;
                settingsCanvas.blocksRaycasts = false;
                settingsOpen = false;
            }
            else
            {
                Time.timeScale = 0f;
                settingsCanvas.alpha = 1f;
                settingsCanvas.blocksRaycasts = true;
                settingsOpen = true;
            }
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float sliderValue)
    {
        float decibels = Mathf.Lerp(-80f, 0f, sliderValue);
        audioMixer.SetFloat("volume", decibels);
        PlayerPrefs.SetFloat("Volume", sliderValue);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
