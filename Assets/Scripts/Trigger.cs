using System;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public event Action GotInHouse;
    public event Action GotOutHouse;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Robber>(out Robber robber))
        {
            GotInHouse?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Robber>(out Robber robber))
        {
            GotOutHouse?.Invoke();
        }
    }
}
