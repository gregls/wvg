using UnityEngine;
using System.Collections;

public class BuffController : StateMachineBehaviour {

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

    void CreateBuff()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
		PlayerAttack playerAttack = player.GetComponent<PlayerAttack>();
		PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        GameObject spell = (GameObject)Instantiate(playerAttack.getSpellToCast(), player.transform.position, new Quaternion(-90f, 0, 0, 0));
        playerAttack.AddSpell(spell);
		playerHealth.setArmorEffect (spell);
    }

    void OnStateEnter()
    {
        frameCounter = 0;
    }

    void OnStateUpdate()
    {
        frameCounter++;
        if (frameCounter == 26)
        {
            CreateBuff();
        }
    }
}
