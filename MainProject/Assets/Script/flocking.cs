using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class flocking : MonoBehaviour {

    private float speed = 0.001f;
    float rotationSpeed = 4.0f;
    Vector3 averageHeading;
    Vector3 averagePosition;
    float neighbourDistance = 3.0f;

    [SerializeField]
    GameObject obj;

    Matrix4x4[] matrix;

    [SerializeField]
    MeshFilter myMeshFilter;

    [SerializeField]
    Material myMaterial;

    bool turning = false;

	// Use this for initialization
	void Start () {
        speed = Random.Range(0.5f, 1);
        matrix = new Matrix4x4[2] { obj.transform.localToWorldMatrix, this.transform.localToWorldMatrix };
    }
	
	// Update is called once per frame
	void Update () {
        if(Vector3.Distance(transform.position, globalFlocking.origin) >= globalFlocking.tankSize)
        {
            turning = true;
        }
        else
        {
            turning = false;
        }

        if (turning)
        {
            // On indique ici Vector3.zero car o n suppose que le centre de la scène se trouve en (0,0,0)
            Vector3 direction = globalFlocking.origin - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);

            speed = Random.Range(0.5f, 1);
        }
        else
        {
            if (Random.Range(0, 5) < 1)
            {
                ApplyRules();
            }
        }
        transform.Translate(0, 0, Time.deltaTime * speed);
       // Graphics.DrawMesh(myMeshFilter.mesh, transform.worldToLocalMatrix, );
        Graphics.DrawMeshInstanced(myMeshFilter.mesh, 0, myMaterial, matrix, matrix.Length, null, ShadowCastingMode.On, true, 0, null);
    }

    void ApplyRules()
    {
        GameObject[] gos;
        gos = globalFlocking.allBoids;

        Vector3 vcentre = globalFlocking.origin;
        Vector3 vavoid = globalFlocking.origin;
        float gSpeed = 0.1f;

        Vector3 goalPos = globalFlocking.goalPos;

        float dist;

        int groupSize = 0;

        foreach (GameObject go in gos)
        {
            if (go != this.gameObject)
            {
                dist = Vector3.Distance(go.transform.position, this.transform.position);

                if (dist <= neighbourDistance)
                {
                    vcentre += go.transform.position;
                    groupSize++;

                    if (dist < 1.0f)
                    {
                        vavoid = vavoid + (this.transform.position - go.transform.position);
                    }

                    flocking anotherFlocking = go.GetComponent<flocking>();
                    gSpeed = gSpeed + anotherFlocking.speed;
                }
            }
        }

        if (groupSize > 0)
        {
            vcentre = vcentre / groupSize + (goalPos - this.transform.position);
            speed = gSpeed / groupSize;

            Vector3 direction = (vcentre + vavoid) - transform.position;

            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
            }
        }
    }
}
