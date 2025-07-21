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

    public void SetTarget()
    {
        isTarget = true;
    }

    public bool IsTarget() 
    {
        return isTarget;
    }

 
}
