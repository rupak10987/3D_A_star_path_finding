using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle_in_path : MonoBehaviour
{
    public Vector3 pos;
    public Vector3 rot;
    public pathfind path;
    public Grids graph;
    // Start is called before the first frame update
    void Start()
    {
        path = GetComponent<pathfind>();
        graph = GetComponent<Grids>();
        pos = transform.position;
        rot = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        if(pos!=transform.position || rot!=transform.forward)
        {
            graph.create_grid();
            path.vai_update_lagbe = true;
            pos = transform.position;
            rot = transform.forward;
        }
    }
}
