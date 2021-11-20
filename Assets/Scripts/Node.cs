using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool is_travarseable;
    public Vector3 worldpos;
    public int My_grid_pos_x;
    public int My_grid_pos_Y;
    public int My_grid_pos_Z;
    public int g_cost;
    public int h_cost;
    public Node Parent_node;
    public Node(bool traverse, Vector3 pos, int x, int z, int y)
    {
        is_travarseable = traverse;
        worldpos = pos;
        My_grid_pos_x = x;
        My_grid_pos_Y = y;
        My_grid_pos_Z = z;

    }
    public int f_cost()
    {
        return (g_cost + h_cost);
    }
}
