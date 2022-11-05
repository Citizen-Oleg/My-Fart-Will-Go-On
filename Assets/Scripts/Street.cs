using System;
using PersonComponent;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Street : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Person person))
        {
            person.Hide();
        }
    }
}