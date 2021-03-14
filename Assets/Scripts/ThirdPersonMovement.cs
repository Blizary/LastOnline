using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    CharacterController controller;
    FarPersonManager manager;
    public Transform cam;
    public float speed;
    public float rotSpeed;
    float turnSmoothVel;

    public bool hasTarget;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<FarPersonManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(horizontal, 0f, vertical).normalized;

        if(dir.magnitude>=0.1f)
        {
            float targetAngle = Mathf.Atan2(dir.x, dir.z)*Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle,ref turnSmoothVel, rotSpeed);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }


        //mouse click

        if(Input.GetMouseButtonDown(0))
        {
            Targetted();
        }

    }

    void Targetted()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast (ray, out hit,100.0f))
        {
            //found a target
            if(hit.transform.gameObject.GetComponent<NPCController>())
            {
                //found a npc
                Debug.Log("found yah");
                manager.UpdateTarget(hit.transform.gameObject.GetComponent<NPCController>().npcInfo);
            }
            else
            {
                manager.NoTarget();
            }
        }
       
    }


 
}
