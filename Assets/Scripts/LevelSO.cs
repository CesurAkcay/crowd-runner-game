using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Scriptable Object/Level", order = 0 )]

public class LevelSO : ScriptableObject
{
    public Chunk[] chunks; // Array of chunks that make up the level
}
