using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SearchFor : IState
{
    private LayerMask searchLayer;
    private GameObject owner;
    private float searchRadius;
    private string tagToSearchFor;
    // Not part of the main State
    private NavMeshAgent navMeshAgent;

    public SearchFor(LayerMask searchLayer, GameObject owner, float searchRadius, string tagToSearchFor, NavMeshAgent navMeshAgent)
    {
        this.searchLayer = searchLayer;
        this.owner = owner;
        this.searchRadius = searchRadius;
        this.tagToSearchFor = tagToSearchFor;
        this.navMeshAgent = navMeshAgent;
    }


    public void Enter()
    {

    }

    public void Execute()
    {
        var hitObjects = Physics.OverlapSphere(this.owner.transform.position, searchRadius);
        for (int i = 0; i < hitObjects.Length; i++)
        {
            if (hitObjects[i].CompareTag(tagToSearchFor))
            {
                // Actually the best way is to return the found objects however in this demo we move to the found object.
                // The reference to the Nav Mesh Agent is also not part of the main funtionality
                this.navMeshAgent.SetDestination(hitObjects[i].transform.position);
            }
            break;
        }
    }

    public void Exit()
    {

    }
}
