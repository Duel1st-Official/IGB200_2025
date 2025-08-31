using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkHider : MonoBehaviour
{
    private Terrain[] chunks;       // array of all the chunks

    public float visibleDistance;   // max distance a chunk will be visible
    public int chunkSize;           // width/length of each chunk
    public float checkRate;         // how often do we check the chunks?

    void Start ()
    {
        // get all the chunks
        chunks = FindObjectsOfType<Terrain>();

        // check the chunks every 'checkRate' seconds
        InvokeRepeating("CheckChunks", 0.0f, checkRate);
    }

    // checks which chunks should be enabled / disabled based on distance from the player
    void CheckChunks ()
    {
        // get the player position with a Y axis of 0
        Vector3 playerPos = transform.position;
        playerPos.y = 0;

        // loop through each chunk
        foreach(Terrain chunk in chunks)
        {
            // get the center position of the chunk
            Vector3 chunkCenterPos = chunk.transform.position + new Vector3(chunkSize / 2, 0, chunkSize / 2);

            // check the distance
            if(Vector3.Distance(playerPos, chunkCenterPos) > visibleDistance)
                chunk.gameObject.SetActive(false);
            else
                chunk.gameObject.SetActive(true);
        }
    }
}