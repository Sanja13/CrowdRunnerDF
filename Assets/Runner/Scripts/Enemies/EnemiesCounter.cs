using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemiesCounter : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private TextMeshPro crowdCounter;
    [SerializeField] private Transform runnersParent;

    private bool isWaiting = false;

    void Update()
    {
        if (!isWaiting)
        {
            StartCoroutine(DelayedUpdate());
        }
    }

    private IEnumerator DelayedUpdate()
    {
        isWaiting = true;
        yield return new WaitForSeconds(1f);

        crowdCounter.text = runnersParent.childCount.ToString();

        if (runnersParent.childCount <= 0)
            Destroy(gameObject);

        isWaiting = false;
    }
}
