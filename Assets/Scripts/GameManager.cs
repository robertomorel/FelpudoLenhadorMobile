using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _player, _playerIdle, _playerHit;

    [SerializeField]
    private GameObject _barril, _barrilDir, _barrilEsq;

    private float _escalaPlayerHorizontal;

    private List<GameObject> _listaBlocos;

    private bool _ladoEsq;

    public Text texto;

    public int score;

    bool comecou;
    bool acabou;

    public GameObject barra;

    public AudioClip somBate, somPerde;

    // Start is called before the first frame update
    void Start()
    {
        _escalaPlayerHorizontal = transform.localScale.x;
        _playerHit.SetActive(false);

        _listaBlocos = new List<GameObject>();

        /*
        Debug.Log("_playerIdle size = " + _playerIdle.GetComponent<SpriteRenderer>().sprite.bounds.size.x);
        Debug.Log("_barril size = " + _barril.GetComponent<SpriteRenderer>().sprite.bounds.size.x);
        */

        CriaBarrilInicio();

        score = 0;

        texto.transform.position = new Vector2(Screen.width / 2, Screen.height / 2 + 50);
        texto.text = "Toque para iniciar!";
        texto.fontSize = 50;

    }

    // Update is called once per frame
    void Update()
    {
        if (!acabou)
        {
            if (Input.GetButtonDown("Fire1"))
            {

                GetComponent<AudioSource>().PlayOneShot(somBate);

                if (!comecou)
                {
                    comecou = true;
                    barra.SendMessage("Comecou");
                }

                texto.text = score.ToString();
                texto.fontSize = 130;

                if (Input.mousePosition.x > Screen.width / 2)
                {
                    // -- Clique foi na parte direita
                    BateDireita();
                    _ladoEsq = false;
                }
                else
                {
                    // -- Clique foi na parte esquerda
                    BateEsquerda();
                    _ladoEsq = true;
                }
                _listaBlocos.RemoveAt(0);
                ReposicionaBlocos();
                ConfereJogada();
            }
        }
    }

    void BateDireita()
    {
        _playerHit.SetActive(true);
        _playerIdle.SetActive(false);
        _player.transform.position = new Vector2(1.1f, _player.transform.position.y);
        _player.transform.localScale = new Vector2(-_escalaPlayerHorizontal, _player.transform.localScale.y);
        Invoke("VoltaAnimacao", 0.25f);
        _listaBlocos[0].SendMessage("BateDireita");
    }

    void BateEsquerda()
    {
        _playerHit.SetActive(true);
        _playerIdle.SetActive(false);
        _player.transform.position = new Vector2(-1.1f, _player.transform.position.y);
        _player.transform.localScale = new Vector2(_escalaPlayerHorizontal, _player.transform.localScale.y);
        Invoke("VoltaAnimacao", 0.25f);
        _listaBlocos[0].SendMessage("BateEsquerda");
    }

    void VoltaAnimacao()
    {
        _playerHit.SetActive(false);
        _playerIdle.SetActive(true);
    }

    public GameObject CriaNovoBarril(Vector2 posicao)
    {
        GameObject novoBarril;

        if (_listaBlocos.Count > 2)
        {
            if (Random.value > 0.5f)
            {
                novoBarril = Instantiate(_barril);
            }
            else
            {
                if (Random.value > 0.5f)
                {
                    novoBarril = Instantiate(_barrilDir);
                }
                else
                {
                    novoBarril = Instantiate(_barrilEsq);
                }
            }

        }
        else
        {
            novoBarril = Instantiate(_barril);
        }

        novoBarril.transform.position = posicao;

        return novoBarril;
    }

    void CriaBarrilInicio()
    {
        for (int i = 0; i < 9; i++)
        {
            GameObject novoBarril = CriaNovoBarril(new Vector2(0, (-3.605f + (i * 1.006f))));
            _listaBlocos.Add(novoBarril);
        }
    }

    void ReposicionaBlocos()
    {
        GameObject novoBarril = CriaNovoBarril(new Vector2(0, (-3.605f + (9 * 1.006f))));
        _listaBlocos.Add(novoBarril);
        for (int i = 0; i < 9; i++)
        {
            _listaBlocos[i].transform.position = new Vector2(_listaBlocos[i].transform.position.x, (_listaBlocos[i].transform.position.y - 1.006f));
        }
    }

    void ConfereJogada()
    {
        string tag = _listaBlocos[0].gameObject.tag;
        if ((tag.CompareTo("Inimigo_Esq") == 0 && _ladoEsq) || (tag.CompareTo("Inimigo_Dir") == 0 && !_ladoEsq))
        {
            // -- Não marcou ponto
            //Debug.Log("Não marcou ponto");
            barra.SendMessage("ZeraBarra");
            FimDeJogo();
        }
        else
        {
            MarcaPonto();
            barra.SendMessage("AumentaBarra");
            // -- Marcou ponto
            //Debug.Log("Marcou ponto");
        }
    }

    void MarcaPonto()
    {
        score++;
        texto.text = score.ToString();
        texto.fontSize = 130;
    }

    void FimDeJogo()
    {
        acabou = true;
        _playerHit.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.35f, 0.35f);
        _playerIdle.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.35f, 0.35f);

        Rigidbody2D rb = _player.GetComponent<Rigidbody2D>();
        rb.isKinematic = false;

        if (_ladoEsq)
        {
            rb.AddTorque(-100.0f);
            rb.velocity = new Vector2(-5.0f, 3.0f);
        }
        else
        {
            rb.AddTorque(100.0f);
            rb.velocity = new Vector2(5.0f, 3.0f);
        }

        GetComponent<AudioSource>().PlayOneShot(somPerde);

        Invoke("RecarregaCena", 2.0f);

    }

    void RecarregaCena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
