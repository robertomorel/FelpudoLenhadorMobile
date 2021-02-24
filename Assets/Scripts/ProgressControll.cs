using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressControll : MonoBehaviour
{

    float escalaBarra;
    bool terminou;
    bool comecou;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        escalaBarra = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (comecou)
        {
            if (escalaBarra > 0.03f)
            {
                escalaBarra = escalaBarra - 0.15f * Time.deltaTime;
                transform.localScale = new Vector2(escalaBarra, 1.0f);
            }
            else
            {
                if (!terminou)
                {
                    terminou = true;
                    gameManager.SendMessage("FimDeJogo");
                }
            }
        }
    }

    void Comecou()
    {
        comecou = true;
    }

    void AumentaBarra()
    {
        if (escalaBarra > 1.0f)
        {
            escalaBarra = 1.0f;
        }
        else
        {
            escalaBarra = escalaBarra + 0.035f;
        }
    }

    void ZeraBarra()
    {
        escalaBarra = 0.031f;
    }
}
