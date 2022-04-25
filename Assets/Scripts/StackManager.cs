using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StackManager : MonoBehaviour
{
    public static StackManager instance;

    [SerializeField] private float distBetweenObj;
    [SerializeField] private Transform Player;
    [SerializeField] private Transform prevObj;
    [SerializeField] private Transform parent;
    [SerializeField] private PlayerControl playerControl;

    [SerializeField] private int stackCount;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject finishPanel;

    [SerializeField] private Text txt;
    [SerializeField] private Text txt2;

    private int point;

    private Animator animator;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        point = PlayerPrefs.GetInt("point");
        txt.text = point.ToString();
        distBetweenObj = prevObj.localScale.y;
        stackCount = parent.childCount;
        animator = Player.GetComponent<Animator>();
    }
    

    public void PickUp(GameObject pickObj, bool tagged = false, string tag = null)
    {
        if (tagged)
        {
            pickObj.tag = tag;
        }
        pickObj.transform.parent = parent;
        Vector3 desPos = prevObj.localPosition;
        Vector3 playerDesPos = Player.localPosition;

        desPos.y += distBetweenObj;
        playerDesPos.y += distBetweenObj;

        pickObj.transform.localPosition = desPos;
        Player.transform.localPosition = playerDesPos;

        prevObj = pickObj.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Stack")
        {
            PickUp(other.gameObject, true, "Untagged");
            stackCount = parent.childCount;
            animator.SetBool("up", true);
        }
        if (other.tag == "Block")
        {
            if (stackCount <= 1)
            {
                gameOverPanel.SetActive(true);
                playerControl.horizontalSpeed = 0;
                playerControl.verticalSpeed = 0;
            }
            other.tag = "Untagged";
            Destroy(other.gameObject);
            Destroy(parent.GetChild(parent.childCount - 1).gameObject);
            Vector3 playerDesPos = Player.localPosition;
            playerDesPos.y -= distBetweenObj;
            Player.transform.localPosition = playerDesPos;
            prevObj = parent.GetChild(parent.childCount - 2).gameObject.transform;

            stackCount -= 1;
        }
        if (other.tag == "Block2")
        {
            if (stackCount <= 1)
            {
                gameOverPanel.SetActive(true);
                playerControl.horizontalSpeed = 0;
                playerControl.verticalSpeed = 0;
            }
            other.tag = "Untagged";
            Destroy(other.gameObject);
            Destroy(parent.GetChild(parent.childCount - 1).gameObject);
            Destroy(parent.GetChild(parent.childCount - 2).gameObject);

            Vector3 playerDesPos = Player.localPosition;
            playerDesPos.y -= distBetweenObj*2;

            Player.transform.localPosition = playerDesPos;
            prevObj = parent.GetChild(parent.childCount - 3).gameObject.transform;

            stackCount -= 2;
        }
        if (other.tag == "Fin")
        {
            other.tag = "Untagged";
            PlayerPrefs.SetInt("point", point + (stackCount * 10));
            txt.text = (point + (stackCount * 10)).ToString();
            txt2.text = (point + (stackCount * 10)).ToString();
            finishPanel.SetActive(true);
            playerControl.horizontalSpeed = 0;
            playerControl.verticalSpeed = 0;
            animator.SetBool("finish", true);
        }
    }
}
