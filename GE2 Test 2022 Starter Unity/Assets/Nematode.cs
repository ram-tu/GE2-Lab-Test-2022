using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nematode : MonoBehaviour
{
    public int higherLimit = 20;
    public int lowerLimit = 5;
    public float startingSize = 0.5f;

    public GameObject head;
    public Material material;
    public ParticleSystem trail_nema;
    public int lifespanMin = 10;

    void Awake()
    {
        int lifespan = Random.Range(lifespanMin, lifespanMin * 5);
        int length = Random.Range(lowerLimit, higherLimit);
        int halfwayPoint = length / 2;
        float increaseSizeCounter = 0.2f;
        float upperHeight = 1.5f;
        int decreaseCounter = 1;
        // Put your code here!
        for (int i = 0; i < length; i++)
        {
            Vector3 size = Vector3.zero;
            
            if (i < halfwayPoint && i * increaseSizeCounter < upperHeight)
            {
                size = new Vector3(startingSize + ((i+1) * increaseSizeCounter), startingSize + ((i+1) * increaseSizeCounter), 1);
            }
            else if(i < halfwayPoint && i * increaseSizeCounter < upperHeight)
            {
                size = new Vector3(startingSize + ((length - (i+1)) * increaseSizeCounter + 1), startingSize + ((length - (i+1)) *increaseSizeCounter + 1),
                    1);
                Debug.Log("reached halfway point so size is " + size);
            }
            else if (i < halfwayPoint && i * increaseSizeCounter > upperHeight)
            {
                size = new Vector3(upperHeight, upperHeight, 1);
            }
            else
            {
                size = new Vector3(startingSize + (upperHeight - (decreaseCounter * increaseSizeCounter)), startingSize + (upperHeight - decreaseCounter*increaseSizeCounter),
                    1);
                decreaseCounter++;
            }

            GameObject segment;
            if (i == 0)
            {
                segment = Instantiate(head);
            }
            else
            {
                segment = GameObject.CreatePrimitive(PrimitiveType.Sphere); 
            }
            segment.transform.position = transform.TransformPoint(new Vector3(0, 0, i * 1));
            segment.transform.rotation = transform.rotation;
            segment.transform.localScale = size;
            segment.GetComponent<Renderer>().material.color = Color.HSVToRGB(Random.Range(0.0f, 1.0f), 1, 1);
            //segment.AddComponent<Rigidbody>();
            //.GetComponent<Rigidbody>().useGravity = false;
            segment.transform.parent = this.transform;
            
            /*if (!(i + 1 < length))
            {
                //ParticleSystem trail = segment.AddComponent<ParticleSystem>();
                ParticleSystem newTrail = Instantiate(trail_nema);
                newTrail.transform.parent = segment.transform;
                newTrail.transform.position = transform.TransformPoint(Vector3.zero);
            }*/
            //if (i == 0)
            //{
            //    MakeBoid(segment);
            //}
        }
        Invoke("Die",lifespan);
    }

    void MakeBoid(GameObject segment)
    {
        segment.AddComponent<Boid>();
        segment.AddComponent<NoiseWander>();
        segment.AddComponent<ObstacleAvoidance>();
        segment.AddComponent<Constrain>();
    }

    void Die()
    {
        GetComponent<SpineAnimator>().enabled = false;
        transform.parent = null;
        foreach (Transform child in transform)
        {
            child.transform.parent = null;
            child.transform.gameObject.AddComponent<Rigidbody>().useGravity = true;
            child.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-3, 3), Random.Range(3, 5), Random.Range(-3, 3)
            );
            Destroy(child.transform.gameObject,Random.Range(2,5));
        }

        gameObject.AddComponent<Rigidbody>().useGravity = true;
        Destroy(transform.gameObject, Random.Range(2,5));
        
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
