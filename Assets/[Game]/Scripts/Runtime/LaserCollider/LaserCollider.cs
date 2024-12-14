using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCollider : MonoBehaviour
{
    private BoxCollider boxCollider;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    public void SetColliderSize(float length)
    {
        if (boxCollider != null)
        {
            boxCollider.size = new Vector3(boxCollider.size.x, boxCollider.size.y, length);
            boxCollider.center = new Vector3(0, 0, length / 2); 
        }
    }
}
