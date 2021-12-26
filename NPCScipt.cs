using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class NPCScipt : MonoBehaviour
{
    // Variaveis
    Animator animator;
    public NavMeshAgent Inimigo;
    public AudioSource alerta;
    public GameObject Player;
    private int carregarlevel;
    [SerializeField] private float FOV = 8.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        alerta = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        Inimigo = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.transform.position);

        // Corre atrás do player
        if(distance < FOV) 
        {
            animator.SetBool("Caçando", true); 
            // Botar o nome do booleano utilizado no animator no lugar de "Caçando"

            Vector3 dirtoplayer = transform.position - Player.transform.position;
            Vector3 newpos = transform.position - dirtoplayer;
            Inimigo.SetDestination(newpos);
        }
        else
        {
            alerta.Play();
            animator.SetBool("Caçando", false);
            // Botar o nome do booleano utilizado no animator no lugar de "Caçando"

        }
    }
    private void OnTriggerStay(Collider other) 
    {
        // Pegou o player
        SceneManager.LoadScene(carregarlevel);
    }
}
