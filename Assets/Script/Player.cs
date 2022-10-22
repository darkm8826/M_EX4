using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Joystick joystick;
    public float ControlTime = 0;
    public Animator myAnimator;

    private CharacterController cc;


    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (ControlTime != 1)
        {
            float h = joystick.Horizontal;
            float v = joystick.Vertical;
            Vector3 dir = new Vector3(h, 0, v);

            if (dir.magnitude > 0.1f)
            {
                myAnimator.SetBool("Next", true);
                myAnimator.SetInteger("Status", 1);
                float faceAngle = Mathf.Atan2(h, v) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler(0, faceAngle, 0);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.1f);
            }
            else if (dir.magnitude < 0.1f)
            {
                myAnimator.SetInteger("Status", 0);
                myAnimator.SetBool("Next", true);
            }

            if (!cc.isGrounded)
            {
                dir.y = -9.8f * 30 * Time.deltaTime;
            }

            cc.Move(dir * Time.deltaTime * 10);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Teleport")
            StartCoroutine(MyCoroutine());
    }

    IEnumerator MyCoroutine()
    {
        ControlTime = 1;
        yield return new WaitForSeconds(1);
        ControlTime = 0;
    }
}