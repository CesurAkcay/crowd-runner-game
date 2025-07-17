using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    
    private List<Enemy> activeEnemies = new List<Enemy>();
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        
    }
    
    void Update()
    {
        // Clean up null references from destroyed enemies
        activeEnemies.RemoveAll(enemy => enemy == null);
    }
    
    public void RegisterEnemy(Enemy enemy)
    {
        if (!activeEnemies.Contains(enemy))
        {
            activeEnemies.Add(enemy);
        }
    }
    
    public void UnregisterEnemy(Enemy enemy)
    {
        activeEnemies.Remove(enemy);
    }
    
    public int GetActiveEnemyCount()
    {
        // Clean up null references and return count
        activeEnemies.RemoveAll(enemy => enemy == null);
        return activeEnemies.Count;
    }
    
    public List<Enemy> GetActiveEnemies()
    {
        activeEnemies.RemoveAll(enemy => enemy == null);
        return new List<Enemy>(activeEnemies);
    }
}