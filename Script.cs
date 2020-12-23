using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject prefab_plane;
    public GameObject[,] prefab_array = new GameObject[10,10];
    public float a_mn;
    public float a, b;
    public float m, n;
    float t = 0.0f;
    float stepx = 0.0f;
    float stepy = 0.0f;
    float h = 0.08f;
    
    void Start()
    {

        for (int y = 0; y < 10; ++y)
        {
            for (int x = 0; x < 10; ++x)
            {

                prefab_array[y, x] = Instantiate(prefab_plane, new Vector3(20 * x, 0, y * 20), Quaternion.identity);
                
            }

        }
    }

    float ReimannSum(float a, float b, int p, int q, float m, float n, float dX, float dY)
    {
        float aMN = 0.0f;

        stepx += 0.1f;
        stepy += 0.1f;

        for (int i = 0; i < p; ++i)
        {
            for (int j = 0; j < q; ++j)
            {
                float t1 = Mathf.Sin((m * Mathf.PI * i+1) / a);
                float t2 = Mathf.Sin((n * Mathf.PI * j+1) / b);

                float xSq = i+1 + j+1;

                aMN += (4.0f / (a * b)) * xSq * t1 * t2 * dX * dY;

             
            }
        }


        return aMN;
    }


    float GetSolution(float a, float b, int p, int q, float m, float n, float dX, float dY, float t, float h, float c)
    {
        float K_mn = Mathf.PI * (Mathf.Sqrt(Mathf.Pow(m / a, 2) + Mathf.Pow(n / a, 2)));

        float u_ij = 0;

     

        for (int i = 0; i < p; ++i)
        {
            for (int j = 0; j < q; ++j)
            {

                float t1 = Mathf.Sin((m * Mathf.PI * i+1) / a);
                float t2 = Mathf.Sin((n * Mathf.PI * j+1) / b);

                u_ij += u_ij + (ReimannSum(a, b, p, q, m, n, dX, dY) * Mathf.Cos(K_mn * t * c) * t1 * t2);               
         
            }
        }

        

        return u_ij;
    }

    // Update is called once per frame
    void Update()
    {
        a = 10; b = 10;
        
        a_mn = (4 / (a * b));
        
        float solution = 0;

        for(int m = 0; m < 10; ++m)
        {
            for(int n = 0; n < 10; ++n)
            {
                solution += GetSolution(20*5, 20*5, 3, 3, (m+2)*2, (n+2)*2, 5, 5, t, h, 1.0f);
                prefab_array[n, m].transform.Translate(0, solution, 0);

             
            }
        }


        t += h;
    }



}
