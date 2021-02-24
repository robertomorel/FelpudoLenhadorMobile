using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrilControll : MonoBehaviour
{

    private Rigidbody2D _rb;  

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BateDireita()
    {
        _rb.velocity = new Vector2(-10, 2);
        _rb.isKinematic = false;
        _rb.AddTorque(100.0f);
        Invoke("ApagaBloco", 2.0f);
    }

    void BateEsquerda()
    {
        _rb.velocity = new Vector2(10, 2);
        _rb.isKinematic = false;
        _rb.AddTorque(-100.0f);
        Invoke("ApagaBloco", 2.0f);
    }

    void ApagaBloco()
    {
        Destroy(this.gameObject);
    }

}
