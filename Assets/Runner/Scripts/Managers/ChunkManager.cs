using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public static ChunkManager instance;

    [Header("Elements")]
    [SerializeField] private LevelScriptibleObject[] levels;
    private GameObject finishLine;

    private void Awake()
    {
        if (instance != null && instance !=this)
            Destroy(gameObject);
        else
            instance = this;
    }
    void Start()
    { 
        GenerateLevel();

       
        finishLine = GameObject.FindWithTag("FinishLine");
    }

   
    private void GenerateLevel()
    {
        int currentLevel = GelLevel();

        //currentLevel = currentLevel % levels.Length;

        LevelScriptibleObject level = levels[currentLevel];

        CreteLevel(level.chunks);
    }
    private void CreteLevel(Chunk[] levelChunk)
    {
        Vector3 chunkPosition = Vector3.zero;

        for(int i=0;i< levelChunk.Length; i++)
        {
            Chunk chunkToCreate = levelChunk[i];

            if (i > 0)
            {
                chunkPosition.z += chunkToCreate.GetLenght() / 2;
            }
                Chunk chunkInstance = Instantiate(chunkToCreate, chunkPosition, Quaternion.identity, transform);
            chunkPosition.z += chunkInstance.GetLenght() / 2;
        }
    }
  
    public float GetZposition()
    {
        return finishLine.transform.position.z;
    }
    public int GelLevel()
    {
        return PlayerPrefs.GetInt("level",0);
    }
}
