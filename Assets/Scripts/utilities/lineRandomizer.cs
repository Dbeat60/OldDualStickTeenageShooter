using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class lineRandomizer : MonoBehaviour
{
    LineRenderer lr;
    [MinMaxSlider (-10f,10f)]
    public Vector2 minMaxY;
     [Range(.0002f, .2f)]
    public float segmentwidth = 0.1f;
    [Range(10, 500)]
    public int NumOfSegments=75;
    public Mesh m;
   public List<Vector3> vertex;
   public List<int> triangles;
    public List<Vector2> uv;
    [Range(-.09f,.09f)]
    public float amplitude = 2f;

    [MinMaxSlider(0f, 500)]
    public Vector2 turbulence = new Vector2(50,130);
    public bool updateAlways;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount= NumOfSegments;
       /* m = new Mesh();
        m.vertices = vertex.ToArray();
        m.triangles = triangles.ToArray();
        m.uv = uv.ToArray();
        GetComponent<MeshFilter>().mesh = m;*/
    }
    float getDeterminant(Vector2 initpos, Vector2 lastPos, Vector2 highestPos)
    {
        float x1 = initpos.x* initpos.x;
        float x2 = highestPos.x*highestPos.x;
        float x3 = lastPos.x*lastPos.x;
        
        float y1 = initpos.x;
        float y2 = highestPos.x;
        float y3 = lastPos.x;

        float primD = x1 * y2 * +x2 * y3 + x3 * y1;
        float secD = y2 * x3 + y3 * x1 + y1 * x2;
         
        return primD-secD;
    }
    float getaxisDet(int axis, Vector2 initpos, Vector2 lastPos, Vector2 highestPos, float Determinant)
    {
        float x1;
        float x2;
        float x3;
        float y1;
        float y2;
        float y3;


        float z1;
        float z3;
        float z2;

        if (axis==0)
        {
            x1 = initpos.y;
            x2 = highestPos.y;
            x3 = lastPos.y;
        }
        else
        {
            x1 = initpos.x * initpos.x;
            x2 = highestPos.x * highestPos.x;
            x3 = lastPos.x * lastPos.x;
        }

        if(axis==1)
        {
            y1 = initpos.y;
            y2 = highestPos.y;
            y3 = lastPos.y;
        }
        else
        {
            y1 = initpos.x;
            y2 = highestPos.x;
            y3 = lastPos.x;
        }


        if (axis == 2)
        {
            z1 = initpos.x;
            z3 = highestPos.x;
            z2 = lastPos.x;
        }
        else
        {
            z1 = 1;
            z3 = 1;
            z2 = 1;
        }

        float primD = x1 * y2 *z3 +x2 * y3*z1 + x3 * y1*z2;
        float secD = z1*y2 * x3 +z2* y3 * x1 + z3*y1 * x2;
        return primD-secD;
    }
    float getVarValue(float DS, float Daxis)
    {
        return Daxis / DS;
    }
    float calculateY(float x, float a,float b, float c)
    {
        /*
         1,1
         5,20
         10,1

        1= a*1*1+b*1+c
        20=a*5*5+b*5+c
        1= a*10*10+b*10+c

        1a+1b+c=1
        25a+5b+c=20
        100a+10b+c=1

     A)   DS= determinante del sistema

              X   Y  Z
            | 1   1  1 | 1
        DS= | 25  5  1 | 20
            | 100 10 1 | 1

        Regla de Sarrus 
        1.- agregar las primeras dos filas nuevamente
        2.- multiplicar las 3 diagonales principales(izq a der 3 numeros) y sumar los resultados
        3.- multiplicar las 3 diagonales secundarias(der a iz 3 numeros) y sumar los resultados
        4 restar 2.- menos 3.-
        
        1)
              X   Y  Z
            | 1   1  1 |
        DS= | 25  5  1 |
            | 100 10 1 | 
            | 1   1  1 |
            | 25  5  1 |

        2.- (1*5*1,25*10*1,100*1*1)
            (5+250+100)
            355

        3.- (1*5*100+1*10*1+1*1*25)
            (500+10+25)
            535
        4.- (355)-
            (535)
            -180


     B)   obtener DX,DY,DZ
        1, remplazar la fila deseada por los Terminos Independientes( el resultado( valor de Y))
        2 usar regla de Sarrus

      
     C) obtener x, Y ,Z
        1 Dividir cada Determinante entre el Determinante del sistema para asi obtener el valor de cada Variable
        x= DX/DS
        y= DY/DS
        z= DZ/DS

         */
        //ax2 + bx + c.

        return a * (x * x) + b * x + c;
    }
    private void OnValidate()
    {
        if (updateAlways)
            UpdateLaser();
    }
    // Update is called once per frame
    [Button("Force Update")]
    void UpdateLaser()
    {
         if(!lr)
        {
            Start();
        }
        {
            lr.positionCount = NumOfSegments;
            Vector2 starpos = new Vector2(0,0);
            Vector2 highestpos = new Vector2(NumOfSegments * segmentwidth / 2f, amplitude);
            Vector2 lastPos = new Vector2(NumOfSegments * segmentwidth, 0);

            for (int i = 0; i < lr.positionCount; i++)
            {
                float x = i * segmentwidth;
                float DS = getDeterminant(starpos, lastPos, highestpos);
                float y = calculateY(x,
                            getaxisDet(0, starpos, lastPos, highestpos, DS),
                            getaxisDet(1, starpos, lastPos, highestpos, DS),
                            getaxisDet(2, starpos, lastPos, highestpos, DS));
                if (i>Mathf.Max(turbulence.x ,0)&& i < Mathf.Min(NumOfSegments, turbulence.y))
                lr.SetPosition(i,new Vector3(x, y+  Random.Range(minMaxY.x, minMaxY.y) , 0f));
                else
                    lr.SetPosition(i,new Vector3(x, y , 0f));

            }
        }
    }

    private void Update()
    {
        if (updateAlways)
            UpdateLaser();
    }
}
