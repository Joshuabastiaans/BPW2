using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableTarget : MonoBehaviour
{
    public void DestroyTarget()
    {
        Destroy(gameObject);
    }
}
