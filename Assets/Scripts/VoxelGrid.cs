using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelGrid
{
    List<List<List<Voxel>>> _grid = new List<List<List<Voxel>>>();
    public Vector3Int GridDimension;
    float _voxelSize;
    float _margin;

    public VoxelGrid(Vector3Int gridDimension, float voxelSize, float margin)
    {
        GridDimension = gridDimension;
        _voxelSize = voxelSize;
        _margin = margin;

        InstantiateGrid();
    }

    void InstantiateGrid()
    {
        // order Y,X,Z to structure the grid in layers
        for (int y = 0; y < GridDimension.y; y++)
        {
            AddLayerY(y);
        }
    }

    /// <summary>
    /// Add one layer in y direction
    /// </summary>
    public void AddLayerY(int y)
    {
        _grid.Add(new List<List<Voxel>>());
        for (int x = 0; x < GridDimension.x; x++)
        {
            _grid[y].Add(new List<Voxel>());
            for (int z = 0; z < GridDimension.z; z++)
            {
                _grid[y][x].Add(new Voxel(new Vector3Int(x, y, z), _voxelSize, _margin));
            }
        }
    }

    public void LoopGrid(Action<Voxel> acionOnVoxel)
    {
        for (int y = 0; y < GridDimension.y; y++)
            for (int x = 0; x < GridDimension.x; x++)
                for (int z = 0; z < GridDimension.z; z++)
                {
                    Voxel currentVoxel = _grid[y][x][z];
                    acionOnVoxel(currentVoxel);
                }
    }

    public void FlipGrid()
    {
        Action<Voxel> flip = FlipVoxel;//signature needs to match the function
        LoopGrid(flip);
    }

    void FlipVoxel(Voxel vox)
    {
        vox.Status.Alive = !vox.Status.Alive;
    }

    public void SetClickable(bool adding)
    {

        if (adding)
        {
            Action<Voxel> checkCollider = CheckEnableCollider;//signature needs to match the function
            LoopGrid(checkCollider);
        }
        else
        {
            Action<Voxel> disableCollider = DisableDeadCollider;//signature needs to match the function
            LoopGrid(disableCollider);
        }
    }

    void DisableDeadCollider(Voxel vox)
    {
        if (!vox.Status.Alive)
        {
            vox.Status.Clickable = false;
        }
    }

    void CheckEnableCollider(Voxel vox)
    {
        var neighbours = GetNeighbours(vox.Index);

        bool clickable = false;

        int counter = 0;
        while (!clickable && counter < neighbours.Count)
        {
            if (neighbours[counter].Status.Alive)
            {
                clickable = true;
            }

            counter++;
        }

        vox.Status.Clickable = clickable;
    }

    List<Voxel> GetNeighbours(Vector3Int index)
    {
        List<Voxel> neighbours = new List<Voxel>();
        List<Vector3Int> directions = new List<Vector3Int>
        {
            new Vector3Int(1, 0, 0),//East
            //new Vector3Int(1, 0, -1),//SouthEast
            new Vector3Int(0, 0, -1),//South
            //new Vector3Int(-1, 0, -1),//SouthWest
            new Vector3Int(-1, 0, 0),//West
            //new Vector3Int(-1, 0, 1),//NorthWest
            new Vector3Int(0, 0, 1),//North
            //new Vector3Int(1, 0, 1)//NorthEast
            new Vector3Int(0,1,0), //Up
            new Vector3Int(0,-1,0) //Down
        };

        foreach (var direction in directions)
        {
            Vector3Int neighbourIndex = index + direction;

            if (CheckIndex(neighbourIndex))
            {
                neighbours.Add(_grid[neighbourIndex.y][neighbourIndex.x][neighbourIndex.z]);
            }
        }

        return neighbours;
    }

    bool CheckIndex(Vector3Int neighbourIndex)
    {
        if (neighbourIndex.x < 0) return false;
        if (neighbourIndex.y < 0) return false;
        if (neighbourIndex.z < 0) return false;
        if (neighbourIndex.x > GridDimension.x - 1) return false;
        if (neighbourIndex.y > GridDimension.y - 1) return false;
        if (neighbourIndex.z > GridDimension.z - 1) return false;
        return true;
    }

    void BuildJoints()
    {

    }
}
