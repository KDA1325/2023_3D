using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField]
    private NavMeshSurface navMeshSurface;
    [SerializeField]
    private GameObject cube;

    private void Update()
    {
        if(Input.GetKeyDown("1"))
        {
            float x = Random.Range(-14.5f, 14.5f);
            float z = Random.Range(-10.0f, 10.0f);

            Instantiate(cube, new Vector3(x, 10, z), Quaternion.identity, transform);
        }
        else if(Input.GetKeyDown("2"))
        {
            navMeshSurface.BuildNavMesh();
        }
    }
}
