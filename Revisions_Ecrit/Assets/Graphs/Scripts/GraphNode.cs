using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphNode : MonoBehaviour
{
    [SerializeField] Material neutralMaterial;
    [SerializeField] Material exploredMaterial;
    [SerializeField] Material firstMaterial;
    [SerializeField] LineRenderer graphConnectionPrefab;
    public List<GraphNode> connectedNodes;
    public bool isVisited = false;

    Dictionary<GraphNode, LineRenderer> connections = new Dictionary<GraphNode, LineRenderer>();

    Renderer myRenderer;

    void Start()
    {
        myRenderer = GetComponent<Renderer>();

        foreach (GraphNode node in connectedNodes)
        {
            connections.Add(node, Instantiate(graphConnectionPrefab));
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GraphNode node in connectedNodes)
        {
            connections[node].SetPosition(0, transform.position);
            connections[node].SetPosition(1, node.transform.position);
        }
    }

    public void ResetNode()
    {
        myRenderer.material = neutralMaterial;
        isVisited = false;
    }

    public void MarkNode()
    {
        myRenderer.material = exploredMaterial;
        isVisited = true;
    }

    internal void MarkAsFirst()
    {
        myRenderer.material = firstMaterial;
        isVisited = true;
    }
}
