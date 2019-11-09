using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlColisionEnemigo : MonoBehaviour
{
    public ProgressBar Pb;
    public int nivelDeAfectacion;

    private void Start()
    {
        GetComponent<AudioSource>().playOnAwake = false;
        Pb.BarValue = 100;
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Pb.BarValue += nivelDeAfectacion;
            
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetComponent<AudioSource>().Play();
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetComponent<AudioSource>().Pause();
        }
    }
}
