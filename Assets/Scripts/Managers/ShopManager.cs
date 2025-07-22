using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Button purchaseButton; 
    [SerializeField] private SkinButton[] skinButtons;

    [Header("Skins")]
    [SerializeField] private Sprite[] skins;

    [Header("Pricing")]
    [SerializeField] private int skinPrice;
    [SerializeField] private Text priceText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        priceText.text = skinPrice.ToString(); // Display the price of the skin
    }
    void Start()
    {
        ConfigureButtons();

        UpdatePurchaseButton();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            UnlockSkin(Random.Range(0, skinButtons.Length)); // For testing purposes, unlock a random skin when space is pressed
        if (Input.GetKeyDown(KeyCode.D))
            PlayerPrefs.DeleteAll(); // For testing purposes, clear all PlayerPrefs when D is pressed      
    }

    private void ConfigureButtons()
    {
        for (int i = 0; i < skinButtons.Length; i++)
        {
            bool unclocked = PlayerPrefs.GetInt("skinButton" + i) == 1; // allows us to save multiple pieces of data in a loop  
            skinButtons[i].Configure(skins[i], unclocked);

            int skinIndex = i; // Capture the current index for the listener

            skinButtons[i].GetButton().onClick.AddListener(() => SelectSkin(skinIndex)); // call a specific method whenever the button is clicked
        }
    }

    public void UnlockSkin(int skinIndex)
    {
        PlayerPrefs.SetInt("skinButton" + skinIndex, 1);
        skinButtons[skinIndex].Unlocked(); // Unlock the skin button visually
    }

    private void UnlockSkin(SkinButton skinButton)
    {
        int skinIndex = skinButton.transform.GetSiblingIndex(); // Get the index of the skin button --> sibblingIndex kacinci child oldugunu verir
        UnlockSkin(skinIndex);
    }

    private void SelectSkin(int skinIndex)
    {

        for (int i = 0; i < skinButtons.Length; i++)
        {
            if (skinIndex == i)
            {
                skinButtons[i].Select();
            }
            else
            {
                skinButtons[i].Deselect();
            }
        }
    }

    public void PurchaseSkin()
    { 
        List<SkinButton> skinButtonsList = new List<SkinButton>();

        for (int i = 0; i < skinButtons.Length; i++)
        {
            if (!skinButtons[i].IsUnlocked())
                skinButtonsList.Add(skinButtons[i]);      
        }
        if (skinButtonsList.Count <= 0)
            return; // If all skins are unlocked, do nothing

        //At this point, we still have some locked sking
        //And we have list of all of them !!

        SkinButton randomSkinButton = skinButtonsList[Random.Range(0, skinButtonsList.Count)];

        UnlockSkin(randomSkinButton); // Unlock a random skin button
        SelectSkin(randomSkinButton.transform.GetSiblingIndex()); // Select the newly unlocked skin

        DataManager.instance.UseCoins(skinPrice); // Deduct the coins from the player

        UpdatePurchaseButton(); 
    }

    public void UpdatePurchaseButton()
    {
        if (DataManager.instance.GetCoins() < skinPrice)
        {
            purchaseButton.interactable = false;
        }
        else        
        {
            purchaseButton.interactable = true;
        }
    }
}
