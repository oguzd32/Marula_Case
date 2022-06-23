using DG.Tweening;
using UnityEngine;
using static Utilities;

public class PlayerCollisonHandler : MonoBehaviour
{
    // cached components
    private PlayerController controller;
    private PlayerMovement movement;

    // private variables
    private GameObject lastTriggeredObj = default;
    
    private void Start()
    {
        controller = GetComponent<PlayerController>();
        movement = GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject otherObj = other.gameObject;

        // check same object multiple trigger
        if(lastTriggeredObj == otherObj) return;
        lastTriggeredObj = otherObj;
        
        switch (otherObj.tag)
        {
            case "Finish":

                for (int i = 0; i < _GameReferenceHolder.finishConfetties.Length; i++)
                {
                    _GameReferenceHolder.finishConfetties[i].SetActive(true);
                }
                controller.WinProcess();
                movement.enabled = false;

                Sequence sequence = DOTween.Sequence();
                sequence.AppendInterval(1f);
                sequence.AppendCallback(() => _GameManager.EndGame(true, controller.heightAmount));
                break;
            
            case "Obstacle":

                if (otherObj.TryGetComponent(out Obstacle obstacle))
                {
                    _GameReferenceHolder.cameraFollow.Shake(.5f);
                    GameObject obj = _ObjectPooler.GetPooledObject("Hit");
                    obj.transform.position = otherObj.transform.position;
                    obj.SetActive(true);
                    controller.Decrease(obstacle.power);
                    Destroy(otherObj);
                }
                break;
            
            case "Collect":

                GameObject obj1 = _ObjectPooler.GetPooledObject("CollectParticle");

                obj1.transform.position = otherObj.transform.position;
                obj1.SetActive(true);
                controller.GrowUp();
                otherObj.transform.DOMoveY(2f, .5f);
                otherObj.transform.DOScale(Vector3.zero, .5f).OnComplete(() =>
                {
                    Destroy(otherObj);
                });
                break;
        }
    }
}
