using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttack : PlayerState
{
    private int comboCounter;
    private float lastTimeAttacked;
    private float comboWindow = 2;
    public PlayerPrimaryAttack(Player _player, PlayerStateMachine _StateMachine, string _animBoolName) : base(_player, _StateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //if (comboCounter > 2 || Time.time >= lastTimeAttacked+comboWindow)
        //{
        //    comboCounter = 0;
        //}
        if (comboCounter > 2|| Time.time >= lastTimeAttacked + comboWindow)
        {
            comboCounter = 0;
        }

        player.anim.SetInteger("ComboCounter",comboCounter);
        #region Choose attack direction
        float attackDir = player.facingDir;
        if(xInput != 0)
            attackDir=xInput;
        #endregion


        player.anim.SetInteger("ComboCounter", comboCounter);
        stateTimer = .1f;
    }

    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine("BusyFor", .15f);
        comboCounter++;
        lastTimeAttacked = Time.time;

       
        
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer < 0)
        {
            rb.velocity = new Vector2(0, 0);
        }
        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

}