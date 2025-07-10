using System;
using UnityEngine;

public class Archer : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    private float currentTime = 0f;
    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= 0.25f)
        {
            currentTime = 0f;
            Instantiate(arrow, transform.position, transform.rotation);
        }
    }
}
