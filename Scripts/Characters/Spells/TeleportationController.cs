using UnityEngine;
using System.Collections;

public class TeleportationController : StateMachineBehaviour {

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    int frameCounter;

    void Teleporte()
    {
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        GameController gameControllerScript = gameController.GetComponent<GameController>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerAttack playerAttack = player.GetComponent<PlayerAttack>();
        Object spellStart = Instantiate(playerAttack.getSpellToCast(), player.transform.position, player.transform.rotation);
        Object spellEnd = Instantiate(playerAttack.getSpellToCast(), gameControllerScript.GetClickPosition(), player.transform.rotation);
        player.transform.position = gameControllerScript.GetClickPosition();
        Destroy(spellStart, 1f);
        Destroy(spellEnd, 1f);
    }

    void OnStateEnter()
    {
        frameCounter = 0;
    }

    void OnStateUpdate()
    {
        frameCounter++;
        if (frameCounter == 35)
        {
            Teleporte();
        }
    }

}
