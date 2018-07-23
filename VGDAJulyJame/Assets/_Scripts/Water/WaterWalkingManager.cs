using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaterWalkingManager : MonoBehaviour
{
    Queue<OnWaterRequest> waterQueue = new Queue<OnWaterRequest>();

    static WaterWalkingManager instance;

    OnWaterRequest currRequest;

    [SerializeField]
    WaterWalking waterWalking;

    //bool processingRequest = false;

    private void Awake()
    {
        instance = this;
    }

    // create request for response to walking on water
    public static void CreateWaterResponseRequest(GameObject self, Action<bool, bool> reply)
    {
        OnWaterRequest newRequest = new OnWaterRequest(reply, self);

        instance.waterQueue.Enqueue(newRequest);

        instance.TryRequest();
    }

    // attempt to perform check on request before successful response
    void TryRequest()
    {
        currRequest = waterQueue.Dequeue();

        //processingRequest = true;

        waterWalking.StartWaterAffect(currRequest.self);
    }

    // send results to apply appropriate response
    public void FinishedProcess(bool enemy, bool player, bool success)
    {
        if (success)
        {
            currRequest.response(enemy, player);
        }

        //processingRequest = false;
    }

    // make request to have response from walking on water
    struct OnWaterRequest
    {
        public Action<bool, bool> response;
        public GameObject self;

        public OnWaterRequest(Action<bool, bool> _response, GameObject _self)
        {
            response = _response;
            self = _self;
        }
    }
}
