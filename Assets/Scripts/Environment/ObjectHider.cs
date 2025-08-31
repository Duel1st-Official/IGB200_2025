using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHider : MonoBehaviour
{
    private Bounds bounds;                  // bounds of the terrain
    private GameObject[] hidableObjects;    // dynamic objects we can hide

    void Awake ()
    {
        // get the hidable objects
        hidableObjects = GameObject.FindGameObjectsWithTag("ChunkObject");

        // get the bounds of our terrain
        Terrain terrain = GetComponent<Terrain>();

        Vector3 dimensions = new Vector3(terrain.terrainData.heightmapResolution, 1000, terrain.terrainData.heightmapResolution);
        Vector3 pos = transform.position + new Vector3(dimensions.x / 2, 0, dimensions.z / 2);

        bounds = new Bounds(pos, dimensions);
    }

    // called when the object gets enabled
    void OnEnable ()
    {
        // loop through each hidable object
        foreach(GameObject obj in hidableObjects)
        {
            // is the object inside the chunk bounds?
            if(obj != null && bounds.Contains(obj.transform.position))
                obj.SetActive(true);
        }
    }

    // called when the object gets disabled
    void OnDisable ()
    {
        // loop through each hidable object
        foreach(GameObject obj in hidableObjects)
        {
            // is the object inside the chunk bounds?
            if(obj != null && bounds.Contains(obj.transform.position))
                obj.SetActive(false);
        }
    }
}