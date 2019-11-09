using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCollision : MonoBehaviour
{
    public ProgressBar Pb;

    private void Start()
    {
        Pb.BarValue = 100;
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("PisoReactivo"))
        {
            Pb.BarValue -= 1;
        }
    }
}
