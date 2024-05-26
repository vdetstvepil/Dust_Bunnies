using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumSplineSwitcher : MonoBehaviour
{
    private SplineFollower _follower;

    void Start()
    {
        _follower = GetComponent<SplineFollower>();
        _follower.onNode += OnNode;
    }

    private void OnNode(List<SplineTracer.NodeConnection> passed)
    {
        SplineTracer.NodeConnection nodeConnection = passed[0];
        //Debug.Log("point=" + (double)nodeConnection.point);
        double nodePercent = (double)nodeConnection.point / (_follower.spline.pointCount - 1);
        double followerPercent = _follower.result.percent;
        float distancePastNode = _follower.spline.CalculateLength(nodePercent, followerPercent);

        Node.Connection[] connections = nodeConnection.node.GetConnections();
        int rnd = Random.Range(0, connections.Length);
        _follower.spline = connections[rnd].spline;
        double newNodePercent = (double)connections[rnd].pointIndex / (connections[rnd].spline.pointCount - 1);
        double newPercent = connections[rnd].spline.Travel(newNodePercent, distancePastNode, _follower.direction);
        //_follower.SetPercent(newPercent);
    }



}