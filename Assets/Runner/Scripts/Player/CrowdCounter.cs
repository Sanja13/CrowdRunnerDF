using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CrowdCounter : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private TextMeshPro crowdCounter;
    [SerializeField] private Transform runnersParent;
   
    void Update()
    {
        crowdCounter.text = runnersParent.childCount.ToString();

        if (runnersParent.childCount <= 0)
            Destroy(gameObject);
    }

}
