using System;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public event Action GotInHouse;
    public event Action GotOutHouse;

    private void OnTriggerEnter(Collider other)
    {
        GotInHouse?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        GotOutHouse?.Invoke();
    }
}
