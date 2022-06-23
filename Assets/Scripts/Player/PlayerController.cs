using UnityEngine;
using  DG.Tweening;
using static Utilities;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator characterAnimator;
    public int heightAmount = 4;
    [SerializeField] private Transform heightCube;
    [SerializeField] private ParticleSystem collectParticle;

    public Transform GetCharacterTransform => characterAnimator.transform;
    
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

    /// <summary>
    /// When player collected a cube increase 0.25 on y per called this method
    /// </summary>
    public void GrowUp()
    {
        heightAmount++;
        Sequence sequence = DOTween.Sequence();

        collectParticle.Play();
        
        sequence.Append(characterAnimator.transform.DOLocalJump(Vector3.up *heightAmount * .25f, .75f, 0, .5f));
        sequence.Join(heightCube.DOScaleY(heightAmount, .25f));
        sequence.Join(heightCube.DOLocalJump(Vector3.zero, .25f, 0, .5f));
    }

    /// <summary>
    /// When player hit the obstacle, run this method
    /// </summary>
    /// <param name="amount">Value acting on the cube</param>
    public void Decrease(int amount)
    {
        heightAmount = Mathf.Max(heightAmount - amount, 0);

        Sequence sequence = DOTween.Sequence();
        
        sequence.Append(characterAnimator.transform.DOLocalMoveY(heightAmount * .25f, .25f));
        sequence.Join(heightCube.DOScaleY(heightAmount, .25f));
        
        if (heightAmount == 0)
        {
            FailProcess();
            heightCube.DOComplete();
            heightCube.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// When player finish the level, run this method
    /// </summary>
    internal void WinProcess()
    {
        GetCharacterTransform.DOLocalRotate(Vector3.zero, .5f);
        characterAnimator.SetBool("Win", true);
        heightCube.transform.DOScaleY(0, 1f);
        GetCharacterTransform.DOLocalMoveY(0, 1f).OnComplete(() => heightCube.gameObject.SetActive(false));
    }

    /// <summary>
    /// When player fail the level, run this method 
    /// </summary>
    internal void FailProcess()
    {
        movement.enabled = false;
        GameManager.Instance.EndGame(false);
        characterAnimator.SetBool("Fail", true);
    }

    internal void PowerUp()
    {
        movement.IncreaseSpeed();
    }
}
