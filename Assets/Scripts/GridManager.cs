using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    VoxelGrid _grid;

    [SerializeField]
    Vector3Int _gridDimensions;
    [SerializeField]
    float _voxelSize;
    [SerializeField]
    float _margin;

    bool _add = false;
    public bool Add
    {
        get
        {
            return _add;
        }
        set
        {
            if (value != _add)
            {
                if (value) _grid.SetClickable(value);
            }
            _add = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InstantiateGrid();

    }

    void InstantiateGrid()
    {
        _grid = new VoxelGrid(_gridDimensions, _voxelSize, _margin);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // the object identified by hit.transform was clicked
                // do whatever you want
                if (hit.transform.tag == "Voxel")
                {
                    VoxelStatus status = hit.transform.gameObject.GetComponent<VoxelStatus>();
                    _grid.SetClickable(_add);

                    if (status.Alive != _add)
                    {
                        
                        status.Alive = !status.Alive;
                    }
                    
                }
            }
        }
    }

    void OnGUI()
    {
        int padding = 5;
        int buttonHeight = 30;
        int buttonWidth = 170;
        int buttonCounter = 0;

        if (GUI.Button(new Rect(padding, padding + ((buttonHeight + padding) * buttonCounter++),
            buttonWidth, buttonHeight), "Grow grid"))
        {
            _grid.AddLayerY(_gridDimensions.y);
            _grid.GridDimension += Vector3Int.up;
            _gridDimensions = _grid.GridDimension;
            _grid.SetClickable(_add);
        }
        if (GUI.Button(new Rect(padding, padding + ((buttonHeight + padding) * buttonCounter++),
            buttonWidth, buttonHeight), "Flip grid"))
        {
            _grid.FlipGrid();
            _grid.SetClickable(!_add);
            _grid.SetClickable(_add);
        }


        Add = GUI.Toggle(new Rect(padding, padding + ((buttonHeight + padding) * buttonCounter++),
            buttonWidth, buttonHeight), Add, "Add voxels");

        if (GUI.Button(new Rect(padding, padding + ((buttonHeight + padding) * buttonCounter++),
            buttonWidth, buttonHeight), "Simulate"))
        {
            _grid.BuildJoints();
            _grid.EnableKinematic();
        }
    }
}
