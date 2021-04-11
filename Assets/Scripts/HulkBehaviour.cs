using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class HulkBehaviour : MonoBehaviour{//}, ITrackableEventHandler {

    //public GameObject hulkPreFab;
    private Vector3 final_pos = new Vector3(0, 0, 6.81f);
    private float translateSpeed = 0.01f;

    public Vector3 maxLocalScale;
    private float maxlocalScaleMagnitude;

	// Use this for initialization
	void Start () {
        //initial behind poster(-0.98f, -1.06f, 7.23f)
        //final in front poster (-0.98f, -1.06f, 6.81f)

        maxLocalScale = new Vector3(100, 100, 100);
        maxlocalScaleMagnitude = maxLocalScale.magnitude;
	}
	
	// Update is called once per frame
	void Update () {

        float actualLocalScaleMagnitude = transform.localScale.magnitude;

        //transform.Translate(0, Time.deltaTime * translateSpeed, Time.deltaTime * translateSpeed);//x,y,z

        if (actualLocalScaleMagnitude < maxlocalScaleMagnitude )
        {
            transform.localScale += new Vector3(0.03f, 0.03f, 0.03f);
        }
    }
}
