using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhyllotaxisInstantiateGO : MonoBehaviour
{
    public GameObject prefap;
    public float degree, constant;
    public int n; // count
    public float scale;

    private Vector2 PhyllotaxisPosition;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            PhyllotaxisPosition = CalculatePhyllotXIS(degree, scale, n);
            GameObject prefapInstance = (GameObject)Instantiate(prefap);
            prefapInstance.transform.position = new Vector3(PhyllotaxisPosition.x, PhyllotaxisPosition.y, 0);
            prefapInstance.transform.localScale = new Vector3(scale, scale, scale);
            n++;
        }
    }


    private Vector2 CalculatePhyllotXIS(float degree,float scale,int count)
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
