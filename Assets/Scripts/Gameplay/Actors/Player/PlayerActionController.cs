using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionController : MonoBehaviour
{
    //Referencia a Stats
    //Referencia a PlayerController
    PlayerController controller;
    PlayerStats stats;

    //INputAction
    InputAction m_action1Action, m_action2Action;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
        stats = (PlayerStats)controller.GetStats();

        m_action1Action = controller.playerInput.actions["Action2"];
        m_action2Action = controller.playerInput.actions["Action3"];
    }

    void Update()
    {
        //Accion1
        if (m_action1Action.triggered)
        {
            Debug.Log("Action 1");
            StartCoroutine(CoolDown(m_action1Action, stats.action1.cooldown));
            stats.action1?.Use(gameObject);
            /*GameObject obj = Instantiate(stats.action1, transform.position, Quaternion.identity);
            obj.layer = gameObject.layer;
            controller.ApplyTempInvulnerabilityWithotColor2D(0.5f);
            */

        }

        //Accion 2
        if (m_action2Action.triggered)
        {
            Debug.Log("Action 2");
            StartCoroutine(CoolDown(m_action2Action, 3.0F));
            stats.action2?.Use(gameObject);
            /*GameObject obj = Instantiate(stats.action2, transform.position, Quaternion.identity);

            //Inicializo el ataque
            obj.GetComponent<OnAttackImpact>()?.Initialize(1.0f);
            obj.GetComponent<AttackMoveTowards>()?.Initialize(10f, transform.right);
            
            obj.layer = gameObject.layer;
            */
        }

    }

    public IEnumerator CoolDown(InputAction action, float cooldownTime)
    {
        action.Disable();
        yield return new WaitForSeconds(cooldownTime);
        action.Enable();
    }
}
