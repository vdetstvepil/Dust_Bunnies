using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class test_travel_vacuum : MonoBehaviour
{
    private SplineFollower _follower;

    void Start()
    {
        _follower = GetComponent<SplineFollower>();
        _follower.onNode += OnNode;
    }
    private void Update()
    {
    }

    private void OnNode(List<SplineTracer.NodeConnection> passed)
    {
        SplineTracer.NodeConnection nodeConnection = passed[0];
        Debug.Log("=======================");
        Debug.Log("point=" + (double)nodeConnection.point);
        double nodePercent = (double)nodeConnection.point / (_follower.spline.pointCount - 1); // процент пути на узле
        double followerPercent = _follower.result.percent; // процент прохождения пылесосом пути
        Debug.Log("nodepercent=" + nodePercent);

        Debug.Log("followerPercent=" + followerPercent);
        float distancePastNode = _follower.spline.CalculateLength(nodePercent, followerPercent); // разница в процентах между узлом и пылесосом
        Debug.Log("distancePastNode=" + distancePastNode);


        Node.Connection[] connections = nodeConnection.node.GetConnections();
        int rnd = Random.Range(0, connections.Length);
       
        double newNodePercent = (double)connections[rnd].pointIndex / (connections[rnd].spline.pointCount - 1); // процент пути на новом узле
        Debug.Log("pointnew=" + (double)connections[rnd].pointIndex);
        if (connections[rnd].spline == _follower.spline)
            return;

        double newPercent = connections[rnd].spline.Travel(newNodePercent, distancePastNode, _follower.direction);
        Debug.Log("direction=" + _follower.direction);
        Debug.Log("newpercent=" + newPercent);
        Debug.Log("random=" + rnd);

        //_follower.RebuildImmediate();
        _follower.spline = connections[rnd].spline;
        //_follower.RebuildImmediate();
        _follower.SetPercent(newPercent);
        _follower.RebuildImmediate();

        //_follower.SetPercent(newPercent);
        //_follower.spline = connections[rnd].spline;

        /* _follower.enabled = false;
         Node.Connection connection = connections[rnd];
         StartCoroutine(SetPercentAfterDelay(newPercent, connection));*/





    }

  

    /*private IEnumerator SetPercentAfterDelay(double newPercent, Node.Connection connection)
    {
        yield return new WaitForSeconds(1.0f);
        _follower.SetPercent(newPercent);
        _follower.spline = connection.spline;
        yield return new WaitForSeconds(1.0f);
        _follower.enabled = true;

    }*/

    /*
     * 
     * 
        SplineTracer.NodeConnection nodeConnection = passed[0];
        double nodePercent = (double)nodeConnection.point / (_follower.spline.pointCount - 1);
        double followerPercent = _follower.result.percent;
        float distancePastNode = _follower.spline.CalculateLength(nodePercent, followerPercent);    

        Node.Connection[] connections = nodeConnection.node.GetConnections();
        int rnd = Random.Range(0,connections.Length);
        _follower.spline = connections[rnd].spline;
        double newNodePercent = (double)connections[rnd].pointIndex / (connections[rnd].spline.pointCount - 1);
        double newPercent = connections[rnd].spline.Travel(newNodePercent, distancePastNode, _follower.direction);
        _follower.SetPercent(newPercent);
     */


    /*
        SplineTracer.NodeConnection nodeConnection = passed[0];
        Debug.Log(nodeConnection.node.name + " at point" + nodeConnection.point);
        double nodePercent = (double)nodeConnection.point / (_follower.spline.pointCount - 1);
        double followerPercent = _follower.UnclipPercent(_follower.result.percent);
        float distancePastNode = _follower.spline.CalculateLength(nodePercent, followerPercent);
        Debug.Log(nodePercent);

        Dreamteck.Splines.Node.Connection[] connections = nodeConnection.node.GetConnections();
        int rnd = Random.Range(0, connections.Length);
        _follower.spline = connections[rnd].spline;
        double newNodePercent = (double)connections[rnd].pointIndex / (connections[rnd].spline.pointCount - 1);
        double newPercent = connections[rnd].spline.Travel(newNodePercent, distancePastNode, _follower.direction);
        _follower.SetPercent(newPercent);
    */



}
