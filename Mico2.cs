using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Mico2 : MonoBehaviour
{
    public static Mico2 instance;
    [SerializeField] private GameObject PlayerLife1, PlayerLife2, PlayerLife3;
    [SerializeField] private float PlayerLife = 3;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform player;
    [SerializeField] private Slider healthbar;
    [SerializeField] private Animator anima;
    [SerializeField] private Material mico;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        instance = this;
    }

    public void Update()
    {
	Death();
        ChasePlayer();
        
	if (PlayerLife == 2)
        {
            PlayerLife1.SetActive(false);
        }
        
        if (PlayerLife == 1)
        {
            PlayerLife2.SetActive(false);
        }
        
        if (PlayerLife == 0)
        {
            PlayerLife3.SetActive(false);
            anima.SetBool("Walking", false);
            agent.isStopped = true;
            //SceneManager.LoadScene("Menu");
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        if(agent.transform.position.z != 0)
        { anima.SetBool("Walking", true); }
        else
        { anima.SetBool("Walking", false); }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (PlayerLife != 0)
        {
            if(other.gameObject.name == "PlayerArmature")
            {
                anima.SetTrigger("Attack");
                anima.SetBool("Attacking", false);
            }
        }  
        else
        { 
           anima.SetBool("Attacking", false);
        }

        if (other.gameObject.name == "Machado" && PlayerLife != 0 && healthbar.value != 0)
        {
            anima.SetTrigger("Dano");
            anima.SetBool("Damage", false);
            healthbar.value -= 0.5f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (healthbar.value != 0)
        { anima.SetBool("Attacking", false); }
        else
        { }
    }

    public void Death()
    {
        if(healthbar.value == 0)
        {
            anima.SetTrigger("Die");
            anima.enabled = false;
            agent.isStopped = true;
#if UNITY_EDITOR
            Debug.Log("Morri");
        }
#endif
    }

    IEnumerator Shader()
    {
        yield return new WaitForSeconds(1f);
        this.GetComponent<Renderer>().material = mico;
    }
}
