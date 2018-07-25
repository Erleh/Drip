using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaterWalkingManager : MonoBehaviour
{
    Queue<OnWaterRequest> waterQueue = new Queue<OnWaterRequest>();
    Queue<OnWaterRequest> offQueue = new Queue<OnWaterRequest>();

    static WaterWalkingManager instance;

    OnWaterRequest currRequest;

    [SerializeField]
    EnemyStatus monsterStatus;
    [SerializeField]
    WaterWalking waterWalking;

    //bool processingRequest = false;

    public bool IsMonsterOnWater()
    {
        return monsterStatus.IsOnWater;
    }

    public void SetMonsterWaterStatus(bool isOnWater)
    {
        monsterStatus.IsOnWater = isOnWater;
    }

    private void Awake()
    {
        instance = this;
    }

    // create request for response to walking on water
    public static void CreateWaterResponseRequest(GameObject self, Action<bool, bool, bool> reply, bool onWater)
    {
        OnWaterRequest newRequest = new OnWaterRequest(reply, self);

        if (onWater)
        {
            instance.waterQueue.Enqueue(newRequest);
        }
        else if (!onWater)
        {
            instance.offQueue.Enqueue(newRequest);
        }

        instance.TryRequest(onWater);
    }

    // attempt to perform check on request before successful response
    void TryRequest(bool onWater)
    {
        if (onWater)
        {
            currRequest = waterQueue.Dequeue();
        }
        else if(!onWater)
        {
            currRequest = offQueue.Dequeue();
        }

        waterWalking.StartWaterAffect(currRequest.self, onWater);
    }

    // send results to apply appropriate response
    public void FinishedProcess(bool enemy, bool player, bool onWater)
    {
        currRequest.response(enemy, player, onWater);
    }

    // make request to have response from walking on water
    struct OnWaterRequest
    {
        public Action<bool, bool, bool> response;
        public GameObject self;

        public OnWaterRequest(Action<bool, bool, bool> _response, GameObject _self)
        {
            response = _response;
            self = _self;
        }
    }
}
