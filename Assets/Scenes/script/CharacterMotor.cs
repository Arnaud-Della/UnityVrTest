using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterMotor : MonoBehaviour
{

    //Animation Perso


    //vitesse de déplacement
    public float walkSpeed;
    public float runSpeed;
    public float turnSpeed;

    //Imputs


    CapsuleCollider PlayerCollider;




    void Start()
    {

 
        PlayerCollider = gameObject.GetComponent<CapsuleCollider>();
    }


    void Update()
    {
        //si on avance

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, 0, walkSpeed * Time.deltaTime);

        }

        //si on recule

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, 0, -(walkSpeed / 2) * Time.deltaTime);
 
        }

        //roatation à gauche
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, -turnSpeed * Time.deltaTime, 0);
        }

        //rotaion à droite
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, turnSpeed * Time.deltaTime, 0);
        }
    }
}