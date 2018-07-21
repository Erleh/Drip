using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaterWalkingManager : MonoBehaviour
{
    static WaterWalkingManager instance;

    OnWaterRequest currRequest;

    [SerializeField]
    WaterWalking waterWalking;

    private void Awake()
    {
        instance = this;
    }

    // create request for response to walking on water
    public static void CreateWaterResponseRequest(GameObject self, Action<bool, bool> reply)
    {
        OnWaterRequest newRequest = new OnWaterRequest(reply, self);

        instance.TryRequest();
    }

    // attempt to perform check on request before successful response
    void TryRequest()
    {
        waterWalking.StartWaterAffect(currRequest.self);
    }

    // send results to apply appropriate response
    public void WaterWalkingSuccess(bool enemy, bool player)
    {
        currRequest.response(enemy, player);
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
