using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPosition : MonoBehaviour {

    [SerializeField] private Transform followTransform;
    [SerializeField] private bool follow;

	void Update () {
        if (follow && followTransform != null)
        transform.position = followTransform.position;
	}
}
