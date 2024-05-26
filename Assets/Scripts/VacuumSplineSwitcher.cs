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
        double nodePercent = (double)nodeConnection.point / (_follower.spline.pointCount - 1); // процент пути на узле
        double followerPercent = _follower.result.percent; // процент прохождения пылесосом пути
        float distancePastNode = _follower.spline.CalculateLength(nodePercent, followerPercent); // разница в процентах между узлом и пылесосом

        Node.Connection[] connections = nodeConnection.node.GetConnections();
        int rnd = Random.Range(0, connections.Length);
        double newNodePercent = (double)connections[rnd].pointIndex / (connections[rnd].spline.pointCount - 1); // процент пути на новом узле
        double newPercent = connections[rnd].spline.Travel(newNodePercent, distancePastNode, _follower.direction);
        _follower.spline = connections[rnd].spline;
        _follower.SetPercent(newPercent);

        _follower.RebuildImmediate();
    }



}