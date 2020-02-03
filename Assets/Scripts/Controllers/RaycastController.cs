using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastController : MonoBehaviour
{
    private static Dictionary<GameObject, IRaycastable> raycastableObjects = new Dictionary<GameObject, IRaycastable>();

    private bool isActive;
    private RaycastHit2D hitObjects;
    public bool IsActive
    {
        get
        {
            return isActive;
        }
        set
        {
            isActive = value;
        }
    }
    public static void AddToRaycasttable(Collider2D colliderToAdd)
    {
        IRaycastable raycastInterface = colliderToAdd.gameObject.GetComponent<IRaycastable>();
        if (!raycastableObjects.ContainsKey(colliderToAdd.gameObject))
        {
            raycastableObjects.Add(colliderToAdd.gameObject, raycastInterface);
        }
    }
    public bool Ray(Vector3 ray, string byWho)
    {
        if (isActive)
        {
            hitObjects = Physics2D.Raycast(ray, Vector2.zero);
            if (hitObjects.transform != null)
            {
                if (raycastableObjects.ContainsKey(hitObjects.collider.gameObject))
                {
                    raycastableObjects[hitObjects.collider.gameObject].OnRaycastHit(byWho);
                    return true;
                }
                return false;
            }
            else
                return false;
        }
        else
            return true;
    }
}

