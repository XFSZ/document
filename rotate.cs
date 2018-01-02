using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    float FromEuler = 0;                        //开始的角度
    float ToEuler = 60;                        //结束的角度
    float ResidueAngles = 1;                  //剩余的角度

    bool isMove = false;                    //是否开启 用来位置矫正
    bool isTimer = false;                  //是否开启  矫正使用的计时器
    static bool isDisappear = false;      //物体是否应该让图片alpha值下降
    float timer = 0;                     // 角度矫正计时器
    int MaxAngles;                      //检测角度是否超出360
    GameObject hitGO;                  //当前正当中的图片
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(transform.localEulerAngles.y);
        if (Input.GetMouseButtonDown(0)) {
            hitGO = cameraray.hitgameobject;
        }
        if (Input.GetMouseButton(0))                        //使用鼠标 进行旋转  方便测试
        {
            isMove = false;
            isTimer = false;
            transform.Rotate(Vector3.down * Input.GetAxis("Mouse X") * 3, Space.World);
            hitGO.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.7f);
            //   FromEuler = transform.localEulerAngles.y;
        }
        if (Input.GetMouseButtonUp(0))
        {
            timer = 0;                                           //计时器清零
            isMove = true;                                      // 可以移动
            isTimer = true;                                    //可以几时

            for (int i = 30; i < 360; i += 60)
            {
                MaxAngles = i + 60;
                Mathf.Clamp(0, 360, MaxAngles);                        //防止超出360
                if (i < transform.localEulerAngles.y && MaxAngles > transform.localEulerAngles.y)
                {

                    //    ResidueAngles = i + 60 / transform.localEulerAngles.y;           //根据剩余角度计算时间
                    FromEuler = transform.localEulerAngles.y;                             // 旋转起始点
                    ToEuler = i + 30;                                                    //旋转终止点

                }
                if (transform.localEulerAngles.y < 30 && transform.localEulerAngles.y > 0)
                {
                    //       ResidueAngles = i + 60 / transform.localEulerAngles.y;      //根据剩余角度计算时间
                    FromEuler = transform.localEulerAngles.y;                           // 旋转起始点
                    ToEuler = 0;                                                       //旋转终止点
                }
            }
            hitGO.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }

        //旋转
        if (Input.touchCount == 1)                                            //触碰测试
        {
            //   Ray ray = cameras.ScreenPointToRay(Input.mousePosition);
            //   RaycastHit hit;
            // if (Physics.Raycast(ray, out hit))
            //  {
            //  if (hit.transform.name == transform.name)
            //   {
            if (Input.GetTouch(0).phase == TouchPhase.Began) {
                hitGO = cameraray.hitgameobject;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved)     //手指滑动 物体旋转 但只是按手指的移动速度 而且不会自动矫正 矫正放到LateUpdate中
            {

                isMove = false;                                      // 可以移动
                isTimer = false;                                    //可以计时
                Touch touch = Input.GetTouch(0);
                Vector2 deltaPos = touch.deltaPosition;
                transform.Rotate(Vector3.down * deltaPos.x*0.14f , Space.World);      //物体旋转

                
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                timer = 0;                                           //计时器清零
                isMove = true;                                      // 可以移动
                isTimer = true;                                    //可以几时

                for (int i = 30; i < 360; i += 60)
                {
                    MaxAngles = i + 60;
                    Mathf.Clamp(0, 360, MaxAngles);           //防止超出360
                    if (i < transform.localEulerAngles.y && MaxAngles > transform.localEulerAngles.y)
                    {

                        //    ResidueAngles = i + 60 / transform.localEulerAngles.y;           //根据剩余角度计算时间  暂时不用
                        FromEuler = transform.localEulerAngles.y;                             // 旋转起始点
                        ToEuler = i + 30;                                                    //旋转终止点

                    }
                    if (transform.localEulerAngles.y < 30 && transform.localEulerAngles.y > 0)
                    {
                        //       ResidueAngles = i + 60 / transform.localEulerAngles.y;               //根据剩余角度计算时间  暂时不用
                        FromEuler = transform.localEulerAngles.y;                                    // 旋转起始点
                        ToEuler = 0;                                                                //旋转终止点
                    }
                }
            }
            hitGO.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            //  }

            //   }
        }


    }
    private void LateUpdate()
    {
        if (isTimer == true)            //是否计时
        {                               
            timer += Time.deltaTime;       
        }
        if (isMove == true)
        {

            //物体旋转特定角度
            this.transform.rotation = Quaternion.Slerp(Quaternion.Euler(new Vector3(0, FromEuler, 0)), Quaternion.Euler(new Vector3(0, ToEuler, 0)), 1f * timer * ResidueAngles);
        }
        if (timer > 5)
        {              //计时超过5秒自动停止

            isMove = false;
            isTimer = false;

        }


    }
}
