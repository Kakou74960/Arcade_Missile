using NavMeshPlus.Components;
using UnityEngine;

public class NavMeshLogic : MonoBehaviour
{
    [SerializeField] private NavMeshSurface _surface2d;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _surface2d.UpdateNavMesh(_surface2d.navMeshData);
    }
}
