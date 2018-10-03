using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamageAble))]
public class DestructableTerrain : MonoBehaviour
{
    private void Start()
    {
        GetComponent<DamageAble>().OnZeroHealthEvent.AddListener(() => Destroy(this.gameObject));
    }
}
