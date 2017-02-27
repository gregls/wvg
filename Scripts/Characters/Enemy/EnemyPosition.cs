using UnityEngine;
using System.Collections;

public class EnemyPosition : MonoBehaviour {
    
    public float degreeRotation = 90f;
    public string orientation = "S";
    public bool canMove = false;
    public bool canRotate = true;

    public void Rotate()
    {
        switch(orientation)
        {
            case "S":
                Move(new Vector3(0, 0, 1));
                break;
            case "W":
                Move(new Vector3(1, 0, 0));
                break;
            case "N":
                Move(new Vector3(0, 0, -1));
                break;
            case "E":
                Move(new Vector3(-1, 0, 0));
                break;
        }
    }

    void DoRotate()
    {
        if (canRotate)
        {
            if (degreeRotation > 0)
            {
                switch (orientation)
                {
                    case "S":
                        orientation = "W";
                        gameObject.transform.rotation = Quaternion.AngleAxis(degreeRotation, new Vector3(0, 1, 0));
                        break;
                    case "W":
                        orientation = "N";
                        gameObject.transform.rotation = Quaternion.AngleAxis(2 * degreeRotation, new Vector3(0, 1, 0));
                        break;
                    case "N":
                        orientation = "E";
                        gameObject.transform.rotation = Quaternion.AngleAxis(-degreeRotation, new Vector3(0, 1, 0));
                        break;
                    case "E":
                        orientation = "S";
                        gameObject.transform.rotation = Quaternion.AngleAxis(0, new Vector3(0, 1, 0));
                        break;
                }
            } else
            {
                switch (orientation)
                {
                    case "S":
                        orientation = "E";
                        gameObject.transform.rotation = Quaternion.AngleAxis(degreeRotation, new Vector3(0, 1, 0));
                        break;
                    case "W":
                        orientation = "S";
                        gameObject.transform.rotation = Quaternion.AngleAxis(0, new Vector3(0, 1, 0));
                        break;
                    case "N":
                        orientation = "W";
                        gameObject.transform.rotation = Quaternion.AngleAxis(-degreeRotation, new Vector3(0, 1, 0));
                        break;
                    case "E":
                        orientation = "N";
                        gameObject.transform.rotation = Quaternion.AngleAxis(2 * degreeRotation, new Vector3(0, 1, 0));
                        break;
                }
            }
        }
    }

    void Move(Vector3 movement)
    {
        if(canMove)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position + movement, 0.5f);
            if (hitColliders.Length == 0)
            {
                DoMove(movement);
            } else
            {
                int i = 0;
                bool canEffectivlyMove = true;
                while (i < hitColliders.Length)
                {
                    if (hitColliders[i].tag == "Enemy" || hitColliders[i].tag == "Player")
                    {
                        canEffectivlyMove = false;
                        break;
                    }
                    i++;
                }

                if (canEffectivlyMove)
                {
                    DoMove(movement);
                } else
                {
                    DoRotate();
                }
            }
        } else
        {
            DoRotate();
        }
    }

    void DoMove(Vector3 movement)
    {
        GameObject myWall;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.3f);
        int i = 0; while (i < hitColliders.Length)
        {
            if (hitColliders[i].tag == "EnemyWall")
            {
                myWall = hitColliders[i].gameObject;
                Debug.Log(myWall.transform.position);
                myWall.transform.position = myWall.transform.position + movement;
                break;
            }
            i++;
        }
        Rigidbody rigidBody = gameObject.GetComponent<Rigidbody>();
        rigidBody.MovePosition(transform.position + movement);
    }
}
