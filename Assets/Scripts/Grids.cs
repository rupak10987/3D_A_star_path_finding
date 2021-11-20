using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grids : MonoBehaviour
{
    public Transform seeker;
    public LayerMask colliderr;
    public Vector3 Grid_max_size;
    public float node_radius;
    public int number_of_node_X;
    public int number_of_node_Y;
    public int number_of_node_Z;
   public Node[,,] grid;
    public List<Node> path;

    void Start()
    {
        path = new List<Node>();
        number_of_node_X = Mathf.RoundToInt(Grid_max_size.x / (node_radius * 2f));//right
        number_of_node_Y = Mathf.RoundToInt(Grid_max_size.y / (node_radius * 2f));//up
        number_of_node_Z = Mathf.RoundToInt(Grid_max_size.z / (node_radius * 2f));//forward
        grid = new Node[number_of_node_X, number_of_node_Z, number_of_node_Y];
        create_grid();
    }
    public void create_grid()
    {
       
        Vector3 world_bootom_left =transform.position - transform.right * (Grid_max_size.x /2) - transform.forward * (Grid_max_size.z / 2) - transform.up * (Grid_max_size.y / 2);
        for(int i=0;i<number_of_node_X;i++)
            for (int j = 0; j < number_of_node_Z; j++)
                for (int k = 0; k < number_of_node_Y; k++)
                {
                    Vector3 worldpoint = world_bootom_left + (transform.right * node_radius) + (i * node_radius * 2 * transform.right);
                    worldpoint +=(transform.forward * node_radius) + (j * node_radius * 2 * transform.forward);
                    worldpoint+=(transform.up * node_radius) + (k * node_radius * 2 * transform.up);
                    bool tarverseablee =  !(Physics.CheckSphere(worldpoint, node_radius,colliderr));
                    grid[i, j, k] = new Node(tarverseablee, worldpoint, i, j, k);
                }
    }
    public Node find_node_from_pos(Vector3 pos)
    {
        float percentx = (pos.x + (Grid_max_size.x / 2f)) / Grid_max_size.x;
        float percenty = (pos.y + (Grid_max_size.y / 2f)) / Grid_max_size.y;
        float percentz = (pos.z + (Grid_max_size.z / 2f)) / Grid_max_size.z;
        int x = Mathf.RoundToInt((number_of_node_X - 1) * percentx);
        int y = Mathf.RoundToInt((number_of_node_Y - 1) * percenty);
        int z = Mathf.RoundToInt((number_of_node_Z - 1) * percentz);
        return grid[x, z, y];
    }

    public List<Node> get_neighbors(Node nod_current)
    {
        List<Node> neighbors = new List<Node>();
        for(int i=-1;i<=1;i++)
            for(int j=-1;j<=1;j++)
                for(int k=-1;k<=1;k++)
                {
                    if (i == 0 && j == 0 && k == 0)
                        continue;
                    int checkx = nod_current.My_grid_pos_x + i;
                    int checky = nod_current.My_grid_pos_Y + k;
                    int checkz = nod_current.My_grid_pos_Z + j;
                    if (checkx >= 0 && checkx < number_of_node_X && checkz>= 0 && checkz< number_of_node_Z && checky >= 0 && checky < number_of_node_Y)
                    {
                        neighbors.Add(grid[checkx,checkz,checky]);
                    }
                }
        return neighbors;
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, Grid_max_size);
       
        if (grid!=null)
        {
            foreach(Node n in grid)
            {
                if(path.Contains(n))
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(n.worldpos, n.Parent_node.worldpos);
                }
            }
        }
    }
}
