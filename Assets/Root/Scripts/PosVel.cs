using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class PosVel : MonoBehaviour
{
    private NativeArray<Vector3> positions;
    private NativeArray<Vector3> velocities;

    private NativeArray<Vector3> finalPositions;

    private JobHandle handle;

    void Start()
    {
        positions = new NativeArray<Vector3>(10, Allocator.TempJob);
        velocities = new NativeArray<Vector3>(10, Allocator.TempJob);
        finalPositions = new NativeArray<Vector3>(10, Allocator.TempJob);

        Debug.Log("Initial arrays");
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = Random.onUnitSphere;
            velocities[i] = Random.onUnitSphere;
            Debug.Log($"Pos {positions[i]}, Vel {velocities[i]}");
        }

        CalculateFinalPositions job = new CalculateFinalPositions
        {
            positions = this.positions,
            velocities = this.velocities,
            finalPositions = this.finalPositions
        };

        handle = job.Schedule(10,5);
        handle.Complete();

        Debug.Log("finalPositions array");
        for (int i = 0; i < finalPositions.Length; i++)
        {
            Debug.Log(finalPositions[i]);
        }

        positions.Dispose();
        velocities.Dispose();
        finalPositions.Dispose();
    }
    public struct CalculateFinalPositions : IJobParallelFor
    {
        public NativeArray<Vector3> positions;
        public NativeArray<Vector3> velocities;
        public NativeArray<Vector3> finalPositions;

        public void Execute(int index) 
        {
            finalPositions[index] = positions[index] + velocities[index]; 
        }
    }

    private void OnDestroy()
    {
        if (positions.IsCreated) positions.Dispose();
        if (velocities.IsCreated) velocities.Dispose();
        if (finalPositions.IsCreated) finalPositions.Dispose();
    }
}
