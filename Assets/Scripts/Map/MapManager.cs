using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class MapManager : MonoBehaviour
{
    private NavMeshSurface[] _navMeshSurfaces;

    private void Awake()
    {
        InitMapBake();
    }

    private void Start()
    {
        InitEnemy();
    }

    private void InitMapBake()
    {
        foreach (NavMeshSurface surface in _navMeshSurfaces)
        {
            surface.BuildNavMesh();
        }
    }

    private void InitEnemy()
    {
        
    }
}
