using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraray : MonoBehaviour {
    public static  GameObject hitgameobject = null;    
    public float raydistance = 100;      //射线长度
    Vector3 fwd;                        //物体的前方向
    // Use this for initialization
    void Start () {
        fwd = transform.TransformDirection(Vector3.forward);
    }
    void ExplosionDamage(Vector3 center, float radius)                        //球形扫描，目前用不到
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);    //扫描到的物体
        int i = 0;
        while (i < hitColliders.Length)
        {
            //被扫到的碰撞体的行为，例如：
           Debug.Log(hitColliders[i].transform.name);
            i++;
        }
    }
    // Update is called once per frame
    void Update () {
      //  Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, fwd, out hit, raydistance))           //直线扫描
        {
            hitgameobject = hit.collider.gameObject;
            Debug.Log(hitgameobject.transform.parent);
            Debug.DrawLine(transform.position, hit.point, Color.red);
            Debug.Log(hit.collider.name);
          //  hitgameobject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.7f);
        }
        else {
            hitgameobject = null;
        }

            
     

    }
}
