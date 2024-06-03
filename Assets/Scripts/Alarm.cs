using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _speed;

    private float _maxVolume = 1f;
    private float _minVolume = 0;
    private bool _isInHouse = false;

    private void Update()
    {
        if(_isInHouse == true)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _maxVolume, _speed * Time.deltaTime);
        }
        else
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _minVolume, _speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _isInHouse = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _isInHouse = false;
    }

}
