using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Trigger _trigger;
    [SerializeField] private float _speed;

    private Coroutine _increaseVolume;
    private Coroutine _decreaseVolume;
    private float _maxVolume = 1f;
    private float _minVolume = 0f;

    private void OnEnable()
    {
        _trigger.GotInHouse += TurnOnAlarm;
        _trigger.GotOutHouse += TurnOffAlarm;
    }

    private void OnDisable()
    {
        _trigger.GotInHouse -= TurnOnAlarm;
        _trigger.GotOutHouse -= TurnOffAlarm;
    }

    private void TurnOnAlarm()
    {
        Stop(_decreaseVolume);
        _increaseVolume = StartCoroutine(IncreaseVolume());
    }

    private void TurnOffAlarm()
    {
        Stop(_increaseVolume);
        _decreaseVolume = StartCoroutine(DecreaseVolume());
    }

    private IEnumerator DecreaseVolume()
    {
        while(_audioSource.volume > _minVolume)
        {
            ChangeVolume(_minVolume);
            yield return null;
        }

        _audioSource.Stop();
    }

    private IEnumerator IncreaseVolume()
    {
        _audioSource.Play();

        while (_audioSource.volume < _maxVolume)
        {
            ChangeVolume(_maxVolume);
            yield return null;
        }
    }

    private void Stop(Coroutine coroutine)
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
    }

    private void ChangeVolume(float to)
    {
        _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, to, _speed * Time.deltaTime);
    }
}
