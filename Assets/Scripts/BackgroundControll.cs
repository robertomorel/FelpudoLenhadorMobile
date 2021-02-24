using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundControll : MonoBehaviour
{
    private float _larguraTela;
    private float _alturaTela;

    private float _larguraImagem;
    private float _alturaImagem;

    SpriteRenderer _grafico;

    // Start is called before the first frame update
    void Start()
    {
        _grafico = GetComponent<SpriteRenderer>();

        // -- Dimensões da imagem em pixels
        _larguraImagem = _grafico.sprite.bounds.size.x;
        _alturaImagem = _grafico.sprite.bounds.size.y;

        // -- Tamanho vertical (altura) da tela * 2 em u.m.
        _alturaTela = Camera.main.orthographicSize * 2.0f;

        float valorCmPorPx = _alturaTela / Screen.height;

        // -- Largura da tela * 2 em u.m.
        _larguraTela = valorCmPorPx * Screen.width;

        Vector2 novaEscala = transform.localScale;
        novaEscala.x = _larguraTela / _larguraImagem;
        novaEscala.y = _alturaTela / _alturaImagem;

        transform.localScale = novaEscala;

        transform.position = new Vector2(0, 0);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
