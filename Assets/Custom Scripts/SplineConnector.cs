using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class SplineConnector : MonoBehaviour {

	//public SplineComputer splnecmptr;
	//public SplineComputer splncmptr2;

	Node ndecnnctr;
	ObjectBender objBender;
	LengthCalculator lengclcltr;

	//public SplineFollower tracer;
	SplineFollower tracer;
	//public float timeToChange;

	void Start () {

		ndecnnctr = GetComponent<Node> ();
		lengclcltr = GetComponent<LengthCalculator> ();
		objBender = GetComponent<ObjectBender> ();

		//ndecnnctr.AddConnection (splncmptr2, 1);
		//StartCoroutine (ChangePlayerSpline());
	}

	void Awake () {

		tracer = GetComponent<SplineFollower> ();
	}

	void Update () {

	}

	private void OnEnable () {

		tracer.onNode += Player_onNode;;
	}
		
	private void OnDisable () {

		tracer.onNode -= Player_onNode;
	}

	void Player_onNode (List<SplineFollower.NodeConnection> passed)

	{

		Debug.Log ("ReachedNode" + passed[0].node.name + "Copnnected At ponit" + passed[0].point);
		Node.Connection[] connections = passed [0].node.GetConnections ();
		if(connections.Length == 1) return;
		int newConnection = 1; //Random.Range (0, connections.Length);
		if (connections [newConnection].spline == tracer.spline && connections [newConnection].pointIndex == passed [0].point)
		{
			newConnection++;
			if(newConnection >= connections.Length) newConnection = 0;
		}

		SwitchSpline (connections[newConnection]);
	}

	void SwitchSpline (Node.Connection to) {

		tracer.spline = to.spline;
		tracer.RebuildImmediate ();
		double startPercent = tracer.ClipPercent (to.spline.GetPointPercent(to.pointIndex));
		tracer.SetPercent(startPercent);
	}
}
