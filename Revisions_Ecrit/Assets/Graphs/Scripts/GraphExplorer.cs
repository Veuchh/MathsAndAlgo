using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphExplorer : MonoBehaviour
{
    public List<GraphNode> nodes;
    public GraphNode startNode;
    public float delay = .2f;

    private void ResetAllNodes()
    {
        StopAllCoroutines();

        foreach (var item in nodes)
        {
            item.ResetNode();
        }
    }

    [Button]
    void ExploreDepth()
    {
        ResetAllNodes();

        StartCoroutine(ExploreGraphInDepth(startNode, true));
    }

    IEnumerator ExploreGraphInDepth(GraphNode node, bool isFirst = false)
    {
        yield return new WaitForSeconds(delay);

        if (isFirst)
            node.MarkAsFirst();
        else
            node.MarkNode();
        foreach (var connectedNode in node.connectedNodes)
        {
            if (!connectedNode.isVisited)
            {
                yield return StartCoroutine(ExploreGraphInDepth(connectedNode));
            }
        }
    }

    [Button]
    void ExploreWidth()
    {
        ResetAllNodes();

        StartCoroutine(ExploreGraphInWidth(startNode));
    }

    GraphNode ExploreGraphBreadthFirst(GraphNode node, GraphNode targetNode)
    {
        Queue<GraphNode> queue = new Queue<GraphNode>();

        queue.Enqueue(node);
        node.MarkNode();

        while (queue.Count != 0)
        {
            node = queue.Dequeue();
            foreach (var connectedNode in node.connectedNodes)
            {
                if (connectedNode == targetNode)
                    return connectedNode;

                if (!connectedNode.isVisited)
                {
                    queue.Enqueue(connectedNode);
                    connectedNode.MarkNode();
                }
            }
        }

        return null;
    }
}
