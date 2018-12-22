﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gift : MonoBehaviour
{
    public TextMeshPro text;

    private bool isReady = false;
    private Transform lastParent;

    private void Start()
    {
        lastParent = transform.parent;
    }

    public void Unpack (Treasure newTreasure) {
        text.text = newTreasure.msg;
    }

    private void Update()
    {
        text.transform.LookAt(Camera.main.transform.position);
    }

    public void Open()
    {
        if (isReady)
        {
            GetComponent<Animator>().SetTrigger("open");
            StartCoroutine(FinishPreview());
        }
    }

    public void Preview ()
    {
        // delete from server

        Transform prevPos = GameObject.Find("gif_recieve_pos").transform;

        foreach (var component in Camera.main.GetComponents<Component>())
        {
            Debug.Log(component.name);
        }

        transform.position = prevPos.position;
        transform.SetParent(prevPos);
        transform.localRotation = Quaternion.identity;

        isReady = true;

        FindObjectOfType<Vuforia.VuforiaBehaviour>().enabled = false;
    }

    IEnumerator FinishPreview ()
    {
        yield return new WaitForSeconds(4);

        FindObjectOfType<Vuforia.VuforiaBehaviour>().enabled = true;

        transform.SetParent(lastParent);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        GetComponent<Animator>().SetTrigger("idle");

        isReady = false;

        //gameObject.SetActive(false);
    }
}
