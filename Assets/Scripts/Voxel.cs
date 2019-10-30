using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voxel
{
    public Vector3Int Index;
    GameObject _goVoxel;
    public VoxelStatus Status;

    public Voxel(Vector3Int index, float voxelSize, float margin)
    {
        Index = index;
        _goVoxel = GameObject.CreatePrimitive(PrimitiveType.Cube);
        _goVoxel.name = $"Voxel {Index}";
        _goVoxel.tag = "Voxel";
        _goVoxel.transform.localScale = Vector3.one * voxelSize;

        //move the grid to the centre of the camera
        _goVoxel.transform.position = (Vector3)Index * (voxelSize+margin);
        Status = _goVoxel.AddComponent<VoxelStatus>();
        Status.Alive = true;
    }
}

