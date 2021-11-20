using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathfind : MonoBehaviour
{
    public Grids graph;
    public Transform start;
    public Transform goal;
    public Vector3 start_pos;//for checking if need to update;
    public Vector3 end_pos;//for checking if need to update;
    public bool vai_update_lagbe;
    void Start()
    {
        vai_update_lagbe = false;
        graph = GetComponent<Grids>();
        start_pos = start.position;
        end_pos = goal.position;
        find_path(graph.find_node_from_pos(start.position), graph.find_node_from_pos(goal.position)); ;
        retrace_path(graph.find_node_from_pos(start.position), graph.find_node_from_pos(goal.position));
    }
    void Update()
    {
        if(start.position != start_pos || goal.position != end_pos)
        {
            main_work();
           
        }
        else if(vai_update_lagbe)
        {
            vai_update_lagbe = false;
            main_work();
        }
    }
    public void main_work()
    {
        foreach (Node n in graph.grid)
        {
            n.Parent_node = null;
            n.g_cost = 100000;
            n.h_cost = 1000000;
        }

        find_path(graph.find_node_from_pos(start.position), graph.find_node_from_pos(goal.position)); ;
        retrace_path(graph.find_node_from_pos(start.position), graph.find_node_from_pos(goal.position));
        start_pos = start.position;
        end_pos = goal.position;
    }

    public void find_path(Node strt, Node ending)
    {
      //  Debug.Log("hol");
        List<Node> ready_to_explore=new List<Node>();
        HashSet<Node> explored = new HashSet<Node>();
        ready_to_explore.Add(strt);
        Node current_node;
        while(ready_to_explore.Count>0)
        {
            current_node = ready_to_explore[0];
            for(int i=1;i<ready_to_explore.Count;i++)
            {
                if (current_node.f_cost() > ready_to_explore[i].f_cost() || (current_node.f_cost() == ready_to_explore[i].f_cost() && current_node.h_cost > ready_to_explore[i].h_cost))
                    current_node = ready_to_explore[i];
            }
            ready_to_explore.Remove(current_node);
            explored.Add(current_node);
            
            
            if (current_node == ending)
            {
                return;
            }
                

            foreach(Node nodes in graph.get_neighbors(current_node))
            {
                //...............distance
                if (!nodes.is_travarseable || explored.Contains(nodes))
                { continue; }
                int new_move_cost = nodes.g_cost + dist_btn_nodes(current_node, nodes);
                if(nodes.g_cost>new_move_cost || !ready_to_explore.Contains(nodes))
                {
                    nodes.g_cost = new_move_cost;
                    nodes.h_cost = dist_btn_nodes(nodes, ending);
                    nodes.Parent_node = current_node;
                }
                if(!ready_to_explore.Contains(nodes))
                {
                    ready_to_explore.Add(nodes);
                }
            }
        }
       
    }

    void retrace_path(Node start, Node end)
    {
        List<Node> f_path=new List<Node>();
        Node curreent_node=end;
        while(curreent_node!=start)
        {
           f_path.Add(curreent_node);
            curreent_node = curreent_node.Parent_node;
        }
        f_path.Reverse();
        graph.path = f_path;

    }

    public int dist_btn_nodes(Node A, Node B)
    {
        int dx = Mathf.Abs(A.My_grid_pos_x - B.My_grid_pos_x);
        int dy = Mathf.Abs(A.My_grid_pos_Y - B.My_grid_pos_Y);
        int dz = Mathf.Abs(A.My_grid_pos_Z - B.My_grid_pos_Z);
        int min, max,mid;
        mid = 0;
        min = Mathf.Min(dx,dy,dz);
        max = Mathf.Max(dx, dy, dz);
        if (dx != min && dx != max)
            mid=dx;
        if (dy != min && dy != max)
            mid = dy;
        if (dz != min && dz != max)
            mid = dz;
        int distance = min * 17 + (mid - min) * 14 + (max - mid) * 10;

        return distance;
    }
   
}
