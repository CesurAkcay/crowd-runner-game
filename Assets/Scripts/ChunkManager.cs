using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public static ChunkManager instance; // Singleton instance of ChunkManager

    [Header("Elements")]
    /*
    [SerializeField] private Chunk[] chunkPrefabs;
    [SerializeField] private Chunk[] levelChunks;
    */
    [SerializeField] private LevelSO[] levels; // Array of Level Scriptable Objects
    private GameObject finishLine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep this GameObject across scenes
        }
    }
    void Start()
    {
        Generatelevel();

        finishLine = GameObject.FindWithTag("Finish");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Generatelevel()
    {        
        int currentLevel = GetLevel();

        //what if our current level is greater than the length of the array? it will return error
        currentLevel = currentLevel % levels.Length; // Ensure the current level is within bounds of the levels array

        LevelSO level = levels[currentLevel];
        
        CreateLevel(level.chunks);
    }
    
    private void CreateLevel(Chunk[] levelChunks)
    {
        Vector3 chunkPosition = Vector3.zero;
        for (int i = 0; i < levelChunks.Length; i++)
        {
            Chunk chunkToCreate = levelChunks[i];

            if (i > 0)
            {
                chunkPosition.z += chunkToCreate.GetLength() / 2;
            }

            Chunk chunkInstance = Instantiate(chunkToCreate, chunkPosition, Quaternion.identity, transform);

            chunkPosition.z += chunkInstance.GetLength() / 2;
        }
    }
    /*
    private void CreateRandomLevel()
    {
        Vector3 chunkPosition = Vector3.zero;
        for (int i = 0; i < 5; i++)
        {
            Chunk chunkToCreate = chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];

            if (i > 0)
            {
                chunkPosition.z += chunkToCreate.GetLength() / 2;
            }

            Chunk chunkInstance = Instantiate(chunkToCreate, chunkPosition, Quaternion.identity, transform);

            chunkPosition.z += chunkInstance.GetLength() / 2;
        }
    }
    */


    public float GetFinishPosition()
    {
        return finishLine.transform.position.z;
    }

    public int GetLevel()
    {
        return PlayerPrefs.GetInt("level", 0);
    }
}
