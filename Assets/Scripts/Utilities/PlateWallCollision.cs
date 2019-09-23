using UnityEngine;

public class PlateWallCollision : MonoBehaviour
{
    string wallLayer = "Wall";
    string plateLayer = "Plate";

    // Use this for initialization
    private void Start()
    {
        int plateLayerIndex = LayerMask.NameToLayer(plateLayer);
        int wallLayerIndex = LayerMask.NameToLayer(wallLayer);
        Physics.IgnoreLayerCollision(plateLayerIndex, wallLayerIndex);
    }
}
