using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator characterAnimator;
    public int heightAmount = 4;
    [SerializeField] private Transform heightCube;
    
    // cached components
    private PlayerMovement movement;

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
        characterAnimator.transform.localPosition = Vector3.up * heightAmount * .25f;
    }

    internal void StartGame()
    {
        movement.StartGame();
        characterAnimator.SetBool("Start", true);
        characterAnimator.transform.DOLocalRotate(new Vector3(0, -75, 0), .5f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GrowUp();    
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Decrease(1);
        }
        
    }

    public void GrowUp()
    {
        heightAmount++;
        Sequence sequence = DOTween.Sequence();

        sequence.Append(characterAnimator.transform.DOLocalMoveY(heightAmount * .25f, .25f));
        sequence.Join(heightCube.DOScaleY(heightAmount, .25f));
    }

    public void Decrease(int amount)
    {
        heightAmount = Mathf.Max(heightAmount - amount, 0);

        Sequence sequence = DOTween.Sequence();
        
        sequence.Append(characterAnimator.transform.DOLocalMoveY(heightAmount * .25f, .25f));
        sequence.Join(heightCube.DOScaleY(heightAmount, .25f));
        
        if (heightAmount == 0)
        {
            movement.enabled = false;
            GameManager.Instance.EndGame(false);
            characterAnimator.SetBool("Fail", true);
            heightCube.DOComplete();
            heightCube.gameObject.SetActive(false);
        }
    }
}
