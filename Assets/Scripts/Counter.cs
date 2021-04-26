using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Counter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI counterText;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip plusOne;
    [SerializeField] AudioClip minusOne;
    public int Count { get; private set; }

    void Start()
    {
        Count = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        Count++;
        ChangeColor(Color.green, 0.1f / (Count + 1));
        audioSource.PlayOneShot(plusOne);
        UpdateCounter();
    }

    void OnTriggerExit(Collider other)
    {
        Count--;
        ChangeColor(Color.red, 0.1f / (Count + 1));
        audioSource.PlayOneShot(minusOne);
        UpdateCounter();
    }

    void UpdateCounter()
    {
        counterText.text = "In cart: " + Count;
        ChangeColor(Color.white, 0.5f);
    }

    void ChangeColor(Color color, float duration)
    {
        counterText.CrossFadeColor(color, duration, false, false);
    }
}
