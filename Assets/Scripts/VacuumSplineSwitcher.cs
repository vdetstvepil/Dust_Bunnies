using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumSplineSwitcher : MonoBehaviour
{
    private SplineFollower _follower;

    // Списки траекторий
    public List<SplineComputer> loopedSplines;
    public List<SplineComputer> linearSplines;

    void Start()
    {
        _follower = GetComponent<SplineFollower>();
        _follower.onNode += OnNodePassed;
    }

    private void OnNodePassed(List<SplineTracer.NodeConnection> passed)
    {
        SplineTracer.NodeConnection nodeConnection = passed[0];
        Debug.Log(nodeConnection.node.name + " at point" + nodeConnection.point);
        double nodePercent = (double)nodeConnection.point / (_follower.spline.pointCount - 1);
        double followerPercent = _follower.UnclipPercent(_follower.result.percent);
        float distancePastNode = _follower.spline.CalculateLength(nodePercent, followerPercent);
        Debug.Log(nodePercent);

        Dreamteck.Splines.Node.Connection[] connections = nodeConnection.node.GetConnections();
        SplineComputer currentSpline = _follower.spline;

        // Проверка, является ли текущая траектория линейной
        if (linearSplines.Contains(currentSpline))
        {
            // Переход на закольцованную траекторию
            foreach (var connection in connections)
            {
                if (loopedSplines.Contains(connection.spline))
                {
                    _follower.spline = connection.spline;
                    double newNodePercent = (double)connection.pointIndex / (connection.spline.pointCount - 1);
                    double newPercent = connection.spline.Travel(newNodePercent, distancePastNode, _follower.direction);
                    _follower.SetPercent(newPercent);
                    return;
                }
            }
        }
        else
        {
            // Случайный переход на любую траекторию
            int rnd = Random.Range(0, connections.Length);
            _follower.spline = connections[rnd].spline;
            double newNodePercent = (double)connections[rnd].pointIndex / (connections[rnd].spline.pointCount - 1);
            double newPercent = connections[rnd].spline.Travel(newNodePercent, distancePastNode, _follower.direction);
            _follower.SetPercent(newPercent);
        }
    }
}
