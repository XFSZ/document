using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//the script by ZZZ
public class GameObjectray : MonoBehaviour
{
    public GameObject hitgameobject = null;  //直线扫描到的物体
    Collider[] hitColliders = null;        //球形扫描到的物体
    public string HitTag = "";                  //需要做出行为的物体的标签
    public float rayRadius = 300;       //半径长度
    public float raydistance = 100;      //射线长度
    Vector3 fwd;                        //物体的前方向
    float ScaleTime = 0.8f;                  //缩放时间  (0~1)
    float Scaletimer = 0;                //计时  Time.deltaTime
    bool isScan = true;                 //是否开启扫描
    bool isFirst = false;                //是不是第一次点击
    bool isTimer = false;                //是否计时 以防 数字过大崩掉
    // Use this for initialization
    void Start()
    {
        fwd = transform.TransformDirection(Vector3.forward);             //直线扫描 向前扫描
                                                                         // ExplosionDamage(transform.position, 100);   // 球形扫描
    }
    void ExplosionDamage(Vector3 center, float radius)                        //球形扫描
    {
        hitColliders = Physics.OverlapSphere(center, radius);    //扫描到的物体
                                                                 //  hitgameobject[1] = hitColliders[1].gameObject;
        int i = 0;
        while (i < hitColliders.Length)
        {
            //被扫到的碰撞体的行为，例如：
            Debug.Log(hitColliders[i].transform.name);
            i++;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isTimer == true)
        {                 //开启计时
            Scaletimer += Time.deltaTime;      //计时
        }
        if (Scaletimer > 5)
        {                 //时间过长就不计时
            isTimer = false;
            Scaletimer = 0;
        }


        if (hitColliders != null)
        {
            for (int i = 0; i < hitColliders.Length; i++)              //被扫到的碰撞体的行为 
            {
                if (isFirst == true)
                {
                    if (hitColliders[i].gameObject.name == this.transform.name && hitColliders[i].gameObject.tag == HitTag)              //如果是该物体本身 
                    {
                        //  hitColliders[i].gameObject.transform.localScale =     new Vector3(2, 2, 2);     //直接放大
                        hitColliders[i].gameObject.transform.localScale = Vector3.Lerp(hitColliders[i].gameObject.transform.localScale, new Vector3(2, 2, 2), Scaletimer * ScaleTime);  //插值放大

                    }
                    else                    //不是该物体本身
                    {
                        //  hitColliders[i].gameObject.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);    //直接缩小
                        hitColliders[i].gameObject.transform.localScale = Vector3.Lerp(hitColliders[i].gameObject.transform.localScale, new Vector3(0.4f, 0.4f, 0.4f), Scaletimer * ScaleTime);    //插值缩小
                    }

                }
                else
                {
                    if (hitColliders[i].gameObject.name == this.transform.name && hitColliders[i].gameObject.tag == HitTag)
                    {

                        hitColliders[i].gameObject.transform.localScale = Vector3.Lerp(hitColliders[i].gameObject.transform.localScale, new Vector3(1, 1, 1), Scaletimer * ScaleTime);  //插值放大

                    }
                    else
                    {
                        hitColliders[i].gameObject.transform.localScale = Vector3.Lerp(hitColliders[i].gameObject.transform.localScale, new Vector3(1, 1, 1), Scaletimer * ScaleTime);    //插值缩小
                    }

                }

            }
        }


    }

    public void touchpic()
    {
        if (isScan == true)
        {
            ExplosionDamage(transform.position, rayRadius);   //扫描
            isScan = false;           //防止二次扫描
        }
        isTimer = true;     //开启计时
        Scaletimer = 0;      //计时器清零
        isFirst = !isFirst;   //点击完就换一种情况
    }

    void lineray()         //直线扫描
    {
        fwd = transform.TransformDirection(Vector3.forward);
        //  Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, fwd, out hit, raydistance))

        {
            hitgameobject = hit.collider.gameObject;
            //  Debug.Log(hitgameobject.transform.parent);
            Debug.DrawLine(transform.position, hit.point, Color.red);
            //   Debug.Log(hit.collider.name);
            //  hitgameobject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.7f);
        }
        else
        {
            hitgameobject = null;
        }

    }
}
