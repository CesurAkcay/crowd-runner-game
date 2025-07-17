using UnityEngine;

public class Runner : MonoBehaviour
{
    [Header("Settings")]
    private bool isTarget;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsTarget() 
    {
        return isTarget;
    }

    public void SetTarget() 
    {
        isTarget = true;
    }
}
