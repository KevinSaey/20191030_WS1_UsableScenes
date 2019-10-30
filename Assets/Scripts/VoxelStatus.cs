using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelStatus : MonoBehaviour
{
    public bool Alive
    {
        get
        {
            return _alive;
        }
        set
        {
            _alive = value;
            GetComponent<MeshRenderer>().enabled = _alive;
        }
    }

    public bool Clickable
    {
        get
        {
            return _clickable;
        }
        set
        {
            _clickable = value;
            GetComponent<BoxCollider>().enabled = _clickable;
        }
    }

    bool _alive = true;
    bool _clickable = true;
    public bool NextState;
}
