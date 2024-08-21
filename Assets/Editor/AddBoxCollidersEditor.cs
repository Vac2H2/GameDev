using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AddBoxCollidersEditor : EditorWindow
{
    [MenuItem("Tools/Add Box Colliders to Buildings and Ground")]
    public static void AddBoxColliders()
    {
        // Get the parent object containing Buildings and Ground
        GameObject wayOut = GameObject.Find("WayOut");

        if (wayOut != null)
        {
            // Find Buildings and Ground
            Transform buildings = wayOut.transform.Find("Buildings");
            Transform ground = wayOut.transform.Find("Ground");

            // Add BoxColliders to all children of Buildings
            if (buildings != null)
            {
                foreach (Transform child in buildings)
                {
                    AddBoxCollider(child.gameObject);
                }
            }

            // Add BoxColliders to all children of Ground
            if (ground != null)
            {
                foreach (Transform child in ground)
                {
                    AddBoxCollider(child.gameObject);
                }
            }

            Debug.Log("Box Colliders added to all children of Buildings and Ground.");
        }
        else
        {
            Debug.LogWarning("WayOut object not found in the scene.");
        }
    }

    private static void AddBoxCollider(GameObject obj)
    {
        // Check if the child already has a BoxCollider
        BoxCollider boxCollider = obj.GetComponent<BoxCollider>();
        if (boxCollider == null)
        {
            // Add a BoxCollider to the child object
            boxCollider = obj.AddComponent<BoxCollider>();

            // Set the size and center of the BoxCollider based on the mesh bounds
            MeshRenderer meshRenderer = obj.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                Bounds bounds = meshRenderer.bounds;
                boxCollider.size = bounds.size;
                boxCollider.center = bounds.center - obj.transform.position;
            }
        }
    }
}

