using UnityEngine;
using UnityEngine.UI;
public class DataManager : MonoBehaviour
{

    public static DataManager instance;

    [Header("Coin Texts")]
    [SerializeField] private Text[] coinsTexts;
    private int coins;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);   // Keep this instance across scenes
        }
        else
        {
            instance = this;// Ensure only one instance exists
        }

        coins = PlayerPrefs.GetInt("coins", 0);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateCoinsText();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateCoinsText() 
    {
        foreach(Text coinText in coinsTexts)
        {
            coinText.text = coins.ToString(); // display the amount of the coins
        }        
    }

    public void AddCoins(int amount)
    {
        coins += amount;

        UpdateCoinsText();

        PlayerPrefs.SetInt("coins", coins);

    }

}
