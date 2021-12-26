using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player2 : MonoBehaviour
{
    // Variaveis
    public static Player2 instance;
    [SerializeField] private Slider stamina;
    [SerializeField] private int maxstamina = 100;
    [SerializeField] private float hor;
    [SerializeField] private float ver;
    [SerializeField] private float staminaatual;  
    [SerializeField] private Vector3 playermovements;
    private WaitForSeconds regen = new WaitForSeconds(0.1f);
    [SerializeField] private Coroutine regeneração;
    [SerializeField] private float speed = 5;

    private void Awake() 
    {
        instance = this;
    }
    
    // Parte da Stamina
    private void Start() 
    {
        staminaatual = maxstamina;
        stamina.maxValue = maxstamina;
        stamina.value = maxstamina;
    }
    private void Update() 
    { 
        playermovement();
        
        //Parte da Stamina
        if(staminaatual > 0) 
        {
            if(Input.GetKey(KeyCode.LeftShift)) useStamina(0.4f);
            speed = 5;
        }
        if(staminaatual <= 1)
        {
            speed = 2;
        }
    }

    public void playermovement()
    {
        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");
        playermovements = new Vector3(hor, 0f, ver).normalized * speed * Time.deltaTime;
        if(Input.GetKey(KeyCode.LeftShift))
        {
            playermovements = new Vector3(hor, 0f, ver).normalized * speed * Time.deltaTime * 3;
        }
        transform.Translate(playermovements, Space.Self);
    }

    // Parte da Stamina
    public void useStamina(float amount)
    {
        if(staminaatual - amount >= 0)
        {
            staminaatual -= amount;
            stamina.value = staminaatual;
            if(regeneração != null) StopCoroutine(regeneração);
            regeneração = StartCoroutine(RegenStamina());
        }
        if(staminaatual - amount <= 0)
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                playermovements = new Vector3(hor, 0f, ver).normalized * speed * Time.deltaTime * 3;
            }
        }
    }
    
     // Parte da Stamina
    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(1);
        while(staminaatual < maxstamina) 
        {
            staminaatual += maxstamina / 100;
            stamina.value = staminaatual;
            yield return regen;
        }
    }
}
