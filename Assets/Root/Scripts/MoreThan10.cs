using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class MoreThan10 : MonoBehaviour
{
    private NativeArray<float> array;
    private JobHandle handle;
    void Start()
    {
        array = new NativeArray<float>(10, Allocator.TempJob);
        Debug.Log("Initial array");
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = Random.Range(1,15);
            Debug.Log(array[i]);
        }

        JobArrayFiltter job = new JobArrayFiltter
        {
            array = this.array
        };

        handle = job.Schedule();
        handle.Complete(); 

        Debug.Log("Filttered array");
        for (int i = 0; i < array.Length; i++)
        {
            Debug.Log(array[i]);
        }
        array.Dispose();
    }
    public struct JobArrayFiltter : IJob
    {
        public NativeArray<float> array;
        public void Execute() 
        { 
            for(int i = 0; i < array.Length; i++)
            {
                if (array[i] > 10) array[i] = 0;
            }
        }
    }
    private void OnDestroy()
    {
        if (array.IsCreated) array.Dispose();
    }
}
