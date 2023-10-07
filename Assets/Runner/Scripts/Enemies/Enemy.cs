using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    enum State { Idle, Running }

    [Header("Settings")]
    [SerializeField] private float searchRadius;
    [SerializeField] private float moveSpeed;
    private State state;
    private Transform targetRunner;

    [Header("Events")]
    public static Action onRunnerDied;
   
    void Update()
    {
        ManageState();
    }
    private void ManageState()
    {
        switch (state)
        {
            case State.Idle:
                SearchForTarget();
                break;

            case State.Running:
                RunTowardTarget();
                break;

        }
    }
    private void SearchForTarget()
    {
        Collider[] detetcedCollieders = Physics.OverlapSphere(transform.position, searchRadius);
        for(int i =0; i < detetcedCollieders.Length; i++)
        {
            if(detetcedCollieders[i].TryGetComponent(out Runner runner))
            {
                if (runner.IsTarget())
                    continue;
                runner.SetTarget();
                targetRunner = runner.transform;

                StartRunning();

            }
        }
    }
    private void StartRunning()
    {
        state = State.Running;
        GetComponent<Animator>().Play("Run");
    }
    private void RunTowardTarget()
    {
        if(targetRunner == null)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, targetRunner.position, Time.deltaTime * moveSpeed);

        if(Vector3.Distance(transform.position, targetRunner.position) < .1f)
        {
            onRunnerDied?.Invoke();

            Destroy(targetRunner.gameObject);
            Destroy(gameObject);

        }
    }
}
