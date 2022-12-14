using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 1f;

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
