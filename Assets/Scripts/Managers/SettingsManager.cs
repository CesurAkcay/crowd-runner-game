using UnityEngine;
using UnityEngine.UI;
public class SettingsManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private SoundsManager soundsManager;
    [SerializeField] private VibrationManager vibrationManager;
    [SerializeField] private Sprite optionsOnSprite;
    [SerializeField] private Sprite optionsOffSprite;
    [SerializeField] private Image soundsButtonImage;
    [SerializeField] private Image hapticsButtonImage;

    [Header("Settings")]
    private bool soundsState = true;
    private bool hapticsState = true;

    private void Awake()
    {
        soundsState = PlayerPrefs.GetInt("sounds", 1) == 1; // 1 means enabled, 0 means disabled
        hapticsState = PlayerPrefs.GetInt("haptics", 1) == 1; // 1 means enabled, 0 means disabled
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Setup()
    {
        if (soundsState) 
        {
            EnableSounds();
        }
        else
        {
            DisableSounds();
        }


        if (hapticsState)
        {
            EnableHaptics();
        }
        else
        {
            DisableHaptics();
        }
    }

    public void ChangeSoundsState()
    {
        if(soundsState)
        {
            DisableSounds();
        }
        else
        {
            EnableSounds();
        }
        soundsState = !soundsState;
        
        /*
        int soundsSavedState = 0;
        if (soundsState)
        {
            soundsSavedState = 1; 
        }
        else
        {
            soundsSavedState = 0;
        }
        PlayerPrefs.SetInt("sounds", soundsSavedState); //if sound state is true , save 1, else save 0
        */

        PlayerPrefs.SetInt("sounds", soundsState ? 1 : 0); //if sound state is true , save 1, else save 0
    }



    private void EnableSounds()
    {
        // Tell the sounds manager to set volume of allll the sounds 0
        soundsManager.DisableSounds();

        // Change the image of the sounds button
        soundsButtonImage.sprite = optionsOffSprite;
    }
    private void DisableSounds()
    {
        soundsManager.EnableSounds();

        // Change the image of the sounds button
        soundsButtonImage.sprite = optionsOnSprite;
    }

    public void ChangeHapticsState()
    {
        if (hapticsState)
        {
            DisableHaptics();
        }
        else
        {
            EnableHaptics();
        }
        hapticsState = !hapticsState;

        PlayerPrefs.SetInt("haptics", hapticsState ? 1 : 0);
    }

    public void EnableHaptics()
    {
        // Enable haptics in the game
        vibrationManager.EnableVibrations();
        // Change the image of the haptics button
        hapticsButtonImage.sprite = optionsOnSprite;
    }

    public void DisableHaptics()
    {
        // Enable haptics in the game
        vibrationManager.DisableVibrations();
        // Change the image of the haptics button
        hapticsButtonImage.sprite = optionsOffSprite;
    }

}
