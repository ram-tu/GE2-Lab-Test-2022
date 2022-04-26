using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nematode : MonoBehaviour
{
    public int higherLimit = 20;
    public int lowerLimit = 5;
    

    public Material material;

    void Awake()
    {
        int length = Random.Range(lowerLimit, higherLimit);
        int halfwayPoint = length / 2;
        float increaseSizeCounter = 0.2f;
        // Put your code here!
        for (int i = 0; i < length; i++)
        {
            Vector3 size = Vector3.zero;
            if(i < halfwayPoint)
                size = new Vector3(i * increaseSizeCounter, i * increaseSizeCounter,
                1);
            else
            {
                
                size = new Vector3((length - i) * increaseSizeCounter, (length - i) *increaseSizeCounter,
                    1);
                Debug.Log("reached halfway point so size is " + size);
            }
            GameObject segment = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            segment.transform.position = transform.TransformPoint(new Vector3(0, 0, i * 1));
            segment.transform.rotation = transform.rotation;
            segment.transform.localScale = size;
            segment.GetComponent<Renderer>().material.color = Color.HSVToRGB(Random.Range(0.0f, 1.0f), 1, 1);
            segment.transform.parent = this.transform;
            if (i == 0)
            {
                MakeBoid(segment);
            }
        }
    }

    void MakeBoid(GameObject segment)
    {
        segment.AddComponent<Boid>();
        segment.AddComponent<NoiseWander>();
        segment.AddComponent<ObstacleAvoidance>();
        segment.AddComponent<Constrain>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
