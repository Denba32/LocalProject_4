using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;

    Vector3 targetDir;

    public float maxCheckDistance;
    public LayerMask layerMask;

    private Collider[] detectedTarget;
    private Collider nearTarget;
    public GameObject curInteractGameObject;
    private IInteractable curInteractable;

    private TMP_Text promptText;
    private Camera camera;


    private void Start()
    {
        promptText = UIManager.Instance.promptText;

        camera = Camera.main;
    }

    private void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            detectedTarget = Physics.OverlapSphere(transform.position, maxCheckDistance, layerMask);

            if (detectedTarget != null)
            {
                if (detectedTarget.Length > 0)
                {
                    nearTarget = FindNearTarget(detectedTarget);
                    if(nearTarget != null)
                    {
                        targetDir = (nearTarget.transform.position - transform.position).normalized;
                        lastCheckTime = Time.time;
                        Ray ray = new Ray(transform.position, targetDir);

                        RaycastHit hit;

                        if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
                        {
                            if (hit.collider.gameObject != curInteractGameObject)
                            {
                                curInteractGameObject = hit.collider.gameObject;
                                curInteractable = hit.collider.GetComponent<IInteractable>();

                                SetPromptText();
                            }
                        }
                        else
                        {
                            curInteractGameObject = null;
                            curInteractable = null;
                            promptText.gameObject.SetActive(false);
                        }
                    }
                }
                else
                {
                    nearTarget = null;
                    curInteractGameObject = null;
                    curInteractable = null;
                    promptText.gameObject.SetActive(false);
                }
            }
        }
    }

    private Collider FindNearTarget(Collider[] targets)
    {
        Collider tmp = null;
        float distance = 0;
        for(int i = 0; i <  targets.Length; i++)
        {
            if(tmp == null)
            {
                tmp = targets[i];
                distance = Vector3.Distance(transform.position, targets[i].transform.position);
            }
            else
            {
                if (Vector3.Distance(transform.position, targets[i].transform.position) < distance)
                {
                    tmp = targets[i];
                    distance = Vector3.Distance(transform.position, targets[i].transform.position);
                }
            }

        }
        return tmp;
    }

    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curInteractable.GetInteractPrompt();

    }

    // E 버튼 클릭 시 반응
    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && curInteractable != null)
        {
            curInteractable.OnInteract();
            
            // 초기화
            curInteractGameObject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }
}
