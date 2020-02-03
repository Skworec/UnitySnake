using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TileTypeScript : MonoBehaviour, IRaycastable
{
    private void Awake()
    {
        RaycastController.AddToRaycasttable(gameObject.GetComponent<Collider2D>());
    }
    public void OnRaycastHit(string byWho)
    {
        if (!gameObject.CompareTag("Food"))
        {
            if (byWho != "Food")
            {
                Debug.Log("Death");
                Debug.Log(gameObject.ToString());
                DataController.instance.onDeath.Invoke();
            }
        }
        else
        {
            DataController.instance.onEat.Invoke();
            DataController.instance.Score += 15;
            Destroy(gameObject);
        }
    }
}
