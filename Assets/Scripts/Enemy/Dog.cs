using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{

    private float speed = 1.2f;

    private bool up;
    private bool down;
    private bool right;
    private bool left;

    private bool isRage;
    private bool isWalk;
    private bool isAttack;

    private bool isBack;

    private float moveX;
    private float moveY;

    private Animator anim;

    private GameObject player;
    private PlayerState playerState;

    private Transform target;

    [SerializeField]
    private Transform center;

    private SpriteRenderer sr;

    private void Start()
    {
        player = GameObject.Find("player");
        playerState = player.GetComponent<PlayerState>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        target = player.transform;
        isAttack = false;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 1 && !isAttack)
            StartCoroutine(Attack());

        if (Vector3.Distance(transform.position, center.position) > 10)
            StartCoroutine(Back());

        if (transform.position.y < target.position.y)
        {
            up = true;
            down = false;
        }
        else if (transform.position.y > target.position.y)
        {
            up = false;
            down = true;
        }
        else
        {
            up = false;
            down = false;
        }

        if(transform.position.x < target.position.x)
        {
            right = true;
            left = false;
            sr.flipX = true;
        }
        else if(transform.position.x > target.position.x)
        {
            right = false;
            left = true;
            sr.flipX = false;
        }
        else
        {
            right = false;
            left = false;
        }


        if (right) moveX = 1;
        else if (left) moveX = -1;
        else moveX = 0;

        if (up) moveY = 1;
        else if (down) moveY = -1;
        else moveY = 0;

        if (moveX != 0 && moveY != 0) isWalk = true;

        anim.SetBool("move", isWalk);

        transform.position += new Vector3(moveX, moveY, 0) * Time.deltaTime * speed;
    }

    private IEnumerator Back()
    {
        target = center;
        yield return new WaitForSeconds(7f);
        target = player.transform;
    }

    private IEnumerator Attack()
    {
        anim.SetTrigger("Attack");

        isAttack = true;

        playerState.Health -= 1;
        yield return new WaitForSeconds(1);

        isAttack = false;
    }

    public void Rage()
    {
        StartCoroutine(rageOff());
    }


    IEnumerator rageOff()
    {
        isRage = true;
        anim.SetBool("rage", isRage);
        speed = 2f;

        yield return new WaitForSeconds(1.5f);

        isRage = false;
        anim.SetBool("rage", isRage);
        speed = 1.2f;

        yield return null;
    }

}
