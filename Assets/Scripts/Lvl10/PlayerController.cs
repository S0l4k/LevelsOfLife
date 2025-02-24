using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class PlayerController : MonoBehaviour
{
    private Animator _animator;
    public EventReference WritingReferance;
    private EventInstance WritingInstance;

    private bool isWriting = false; 

    private void Start()
    {
        WritingInstance = RuntimeManager.CreateInstance(WritingReferance);
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        bool isSpacePressed = Input.GetKey(KeyCode.Space);

        if (isSpacePressed)
        {
            
            Teacher.Instance.SetPlayerIsCheating(true);
            _animator.SetTrigger("Cheating");
            _animator.SetBool("basic", false);

            
            if (!isWriting)
            {
                WritingInstance.start();
                isWriting = true;
            }
        }
        else
        {
           
            Teacher.Instance.SetPlayerIsCheating(false);
            _animator.SetBool("basic", true);

            if (isWriting)
            {
                WritingInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                isWriting = false;
            }
        }
    }
}