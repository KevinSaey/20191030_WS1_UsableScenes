using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voxel
{
    public Vector3Int Index;
    public GameObject GOVoxel;
    public VoxelStatus Status;
    Rigidbody _rigidBody;

    float _mass = 1f;

    public Voxel(Vector3Int index, float voxelSize, float margin)
    {
        Index = index;
        GOVoxel = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GOVoxel.name = $"Voxel {Index}";
        GOVoxel.tag = "Voxel";
        GOVoxel.transform.localScale = Vector3.one * voxelSize;

        GOVoxel.transform.position = (Vector3)Index * (voxelSize + margin);
        _rigidBody = GOVoxel.AddComponent<Rigidbody>();
        _rigidBody.mass = _mass;
        _rigidBody.isKinematic = true;

        Status = GOVoxel.AddComponent<VoxelStatus>();
        Status.Alive = true;
    }

    public void AddJoint(Voxel voxToJoint, float breakForce)
    {
        var joint = GOVoxel.AddComponent<FixedJoint>();
        joint.connectedBody = voxToJoint.GOVoxel.GetComponent<Rigidbody>();
        joint.breakForce = breakForce;
    }

    public void SwitchKinematic(bool kinematic)
    {
        _rigidBody.isKinematic = false;
    }

}

