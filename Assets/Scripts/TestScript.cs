/*
 *    https://docs.unity3d.com/Manual/Coroutines.html
 *
 *     This script has been made with the purpose of testing Coroutine's
 *     Specifically if a Coroutine could be ended during a WaitForSeconds() call/return
 *
 *     yield return null              << is used to specify a point at which the coroutine will resume the next frame
 *     yield return WaitForSeconds(x) << is used to wait for x seconds before resuming
 *     yield break                    << is used to break out of the entire coroutine
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private bool moving = false;
    private IEnumerator coroutine;

    // Use this for initialization
    void Start()
    {
        coroutine = MyCoroutine();
        StartCoroutine(coroutine);
        moving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (moving)
            {
                return;
            }
            coroutine = MyCoroutine();
            StartCoroutine(coroutine);
            moving = true;
        }
        else if (Input.GetMouseButton(1))
        {
            if (!moving)
            {
                return;
            }
            StopCoroutine(coroutine);
            coroutine = null;
            moving = false;
        }
    }

    IEnumerator MyCoroutine()
    {
        while (true)
        {
            Debug.Log("THINGS");
            if (this.transform.position.y < 10)
            {
                this.transform.Translate(Vector3.up * Time.deltaTime * 2);
            }
            else
            {
                this.transform.Translate(Vector3.down * 10);
            }
            yield return null;
        }
    }
}