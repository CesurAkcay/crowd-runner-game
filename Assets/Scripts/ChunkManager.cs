using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Chunk[] chunkPrefabs;
    [SerializeField] private Chunk[] levelChunks;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateOrderedLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateOrderedLevel()
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
}
