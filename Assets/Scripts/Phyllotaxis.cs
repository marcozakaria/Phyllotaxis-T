using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phyllotaxis : MonoBehaviour
{
    
    public float degree, scale;
    public int numberStart;
    public int stepSize;
    public int maxIteration;

    //Lerbing
    public bool useLerping;
    public float intervalLerp;
    private bool islerbing;
    private Vector3 startPosition, endPosition;
    private float timeStartedLerbing;

    private int currentIteration;
    private int number;
    private Vector2 PhyllotaxisPosition;
    private TrailRenderer trailRenderer;

    private void Awake()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        number = numberStart;
        transform.localPosition = CalculatePhyllotXIS(degree, scale, number);

        if (useLerping)
        {
            StartLerbing();
        }
    }

    private void FixedUpdate()
    {
        if (useLerping)
        {
            if (islerbing)
            {
                float timeSinceStarted = Time.time - timeStartedLerbing;
                float precentComplete = timeSinceStarted / intervalLerp;
                transform.localPosition = Vector3.Lerp(startPosition, endPosition, precentComplete);
                if (precentComplete >= 0.97f) //need optimization
                {
                    transform.localPosition = endPosition;
                    number += stepSize;
                    currentIteration++;
                    if (currentIteration <= maxIteration)
                    {
                        StartLerbing();
                    }
                    else
                    {
                        islerbing = false;
                    }
                }
            }
        }
        else
        {
            PhyllotaxisPosition = CalculatePhyllotXIS(degree, scale, number);
            transform.localPosition = new Vector3(PhyllotaxisPosition.x, PhyllotaxisPosition.y, 0);
            number += stepSize;
            currentIteration++;
        }
    }

    void StartLerbing()
    {
        islerbing = true;
        timeStartedLerbing = Time.time;
        PhyllotaxisPosition = CalculatePhyllotXIS(degree, scale, number);
        startPosition = this.transform.localPosition;
        endPosition = new Vector3(PhyllotaxisPosition.x, PhyllotaxisPosition.y, 0);

    }

    private Vector2 CalculatePhyllotXIS(float degree, float scale, int count)
    {
        double angle = count * (degree * Mathf.Deg2Rad);
        float r = scale * Mathf.Sqrt(count);

        // x = r *cos(angle)  , y =r * sin(angle)
        float x = r * (float)System.Math.Cos(angle); // system uses double but mathf uses float
        float y = r * (float)System.Math.Sin(angle); // here we need persion of double

        Vector2 vec2 = new Vector2(x, y);
        return vec2;
    }
}
