using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer timer { get; private set; }

    public int tick = 0;

    private void Awake()
    {
        if (timer != null)
            Destroy(this);
        else
            timer = this;
    }
    private void Start()
    {
        StartCoroutine(SumTicks());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            tick++;
            Debug.Log(tick);
        }
    }
    IEnumerator SumTicks()
    {
        yield return new WaitForSeconds(1);
        tick++;
        StartCoroutine(SumTicks());
    }
}