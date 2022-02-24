using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

class TaskRace : MonoBehaviour,IDisposable
{
    CancellationTokenSource _cts = new CancellationTokenSource(); 
    async public void Start()
    {
        CancellationToken ct = _cts.Token;
        Task t1 = Task1(ct);
        Task t2 = Task2(ct);
        bool result = await WhatTaskFasterAsync(ct, t1, t2);
        Debug.Log(result);
    }
    async static Task<bool> WhatTaskFasterAsync(CancellationToken ct, Task task1, Task task2)
    {
        Task result = await Task.WhenAny(task1,task2);
        if (ct.IsCancellationRequested)
        {
            Debug.Log("Race stopped by token");
            return false;
        }
        if (result == task1) return true;
        else return false;
    }
    async private Task Task1(CancellationToken ct)
    {
        if (ct.IsCancellationRequested)
        {
            Debug.Log("Task 1 stopped by token");
            return;
        }
        await Task.Delay(1000);
        Debug.Log("Task 1 finised");
    }
    async private Task Task2(CancellationToken ct)
    {   
        int times = 60;
        while (times > 0)
        {
            if (ct.IsCancellationRequested)
            {
                Debug.Log("Task 2 stopped by token");
                return;
            }
            times--;
            await Task.Yield();
        }
        Debug.Log("Task 2 finised");
    }
    public void Dispose()
    {
        _cts.Dispose();
    }
}