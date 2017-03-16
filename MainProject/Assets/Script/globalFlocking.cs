using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalFlocking : MonoBehaviour {

    [SerializeField]
    private GameObject boidsPrefab;

    [SerializeField]
    private GameObject goalPrefab;

    // taille de la zone de deplacement des boids sur les 3 dimensions (un cube en fait)
    public static int tankSize = 5;

    public static Vector3 origin = new Vector3(20, 12.6f, -5);
    static int numBoids = 60;
    public static GameObject[] allBoids = new GameObject[numBoids];
    public static Vector3 goalPos = origin;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < numBoids; i++)
        {
            Vector3 pos = new Vector3(Random.Range(origin.x - tankSize, origin.x + tankSize),
                                      Random.Range(origin.y - tankSize, origin.y + tankSize),
                                      Random.Range(origin.z - tankSize, origin.z + tankSize)
                                      );
            allBoids[i] = (GameObject)Instantiate(boidsPrefab, pos, Quaternion.identity);
        }	
	}
	
	// Update is called once per frame
	void Update () {
        if (Random.Range(0, 10000) < 50)
        {
            goalPos = new Vector3(Random.Range(-tankSize + origin.x, tankSize - origin.x),
                                      Random.Range(-tankSize + origin.y, tankSize - origin.y),
                                      Random.Range(-tankSize + origin.z, tankSize - origin.z)
                                );
            goalPrefab.transform.position = goalPos;
        }
	}
}
