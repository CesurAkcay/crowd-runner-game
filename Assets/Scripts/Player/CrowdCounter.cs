using UnityEngine;
using TMPro;

public class CrowdCounter : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private TextMeshPro crowdCounterText;
    [SerializeField] private Transform runnersParent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        crowdCounterText.text = runnersParent.childCount.ToString();

        if (runnersParent.childCount <= 0) 
        {
            Destroy(gameObject); // Destroy this GameObject if there are no runners left
        }
    }
}
