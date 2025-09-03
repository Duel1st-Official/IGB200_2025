using UnityEngine;
using System.Collections.Generic;

public class RockPlacementSystem : MonoBehaviour
{
    [Header("Placement Settings")]
    public float placementRange = 5f;
    public LayerMask groundLayer;
    public GameObject rockPrefab;
    public KeyCode placementKey = KeyCode.E;

    [Header("Visual Feedback")]
    public GameObject placementIndicator;
    public Material validPlacementMaterial;
    public Material invalidPlacementMaterial;

    private Camera playerCamera;
    private bool canPlace = false;
    private Vector3 placementPosition;
    private InventorySystem inventory;

    void Start()
    {
        playerCamera = Camera.main;
        inventory = GetComponent<InventorySystem>();

        if (placementIndicator != null)
        {
            placementIndicator.SetActive(false);
        }
    }

    void Update()
    {
        UpdatePlacementIndicator();

        if (Input.GetKeyDown(placementKey) && canPlace)
        {
            TryPlaceRock();
        }
    }

    void UpdatePlacementIndicator()
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, placementRange, groundLayer))
        {
            placementPosition = hit.point;
            canPlace = true;

            if (placementIndicator != null)
            {
                placementIndicator.SetActive(true);
                placementIndicator.transform.position = placementPosition + Vector3.up * 0.1f;

                // Update material if valid
                MeshRenderer renderer = placementIndicator.GetComponent<MeshRenderer>();
                if (renderer != null)
                {
                    renderer.material = canPlace ? validPlacementMaterial : invalidPlacementMaterial;
                }
            }
        }
        else
        {
            canPlace = false;
            if (placementIndicator != null)
            {
                placementIndicator.SetActive(false);
            }
        }
    }

    void TryPlaceRock()
    {
        if (inventory != null && inventory.HasItem("Rock"))
        {
            PlaceRock();
            inventory.RemoveItem("Rock", 1);
        }
        else
        {
            Debug.Log("No rocks in inventory!");
        }
    }

    void PlaceRock()
    {
        if (rockPrefab != null)
        {
            Instantiate(rockPrefab, placementPosition, Quaternion.identity);
            Debug.Log("Rock placed successfully!");
        }
    }
}