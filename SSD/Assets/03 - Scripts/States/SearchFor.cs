using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// This state will search for items in a search radius and returns all the objects as well as objects with specific tag
// this is single use cartride (state) for the state machine but can be modified to do it continually
public class SearchFor : IState
{
    private LayerMask searchLayer;
    private GameObject owner;
    private float searchRadius;
    private string tagToSearchFor;
    public bool SearchCompleted;
    private System.Action<SearchResults> searchResultsCallback; // this is awesome. This asks for a method which is called whenever
    // we're done with the search. This is being added to the constructor of this class as below. In other words, this class asks 
    // for a method to call when its finished its work and that mehtod takes an object of type SearchResults

    public SearchFor(LayerMask searchLayer, GameObject owner, float searchRadius, string tagToSearchFor, Action<SearchResults> searchResultsCallBack)
    {
        this.searchLayer = searchLayer;
        this.owner = owner;
        this.searchRadius = searchRadius;
        this.tagToSearchFor = tagToSearchFor;
        this.searchResultsCallback = searchResultsCallBack;
    }

    public void Enter()
    {

    }

    public void Execute()
    {
        if (!SearchCompleted)
        {
            var hitObjects = Physics.OverlapSphere(this.owner.transform.position, searchRadius);

            for (int i = 0; i < hitObjects.Length; i++)
            {
                var allObjectsWithRequiredTag = new List<Collider>();
                if (hitObjects[i].CompareTag(tagToSearchFor))
                {
                    allObjectsWithRequiredTag.Add(hitObjects[i]);
                }

                var searchResutls = new SearchResults(hitObjects, allObjectsWithRequiredTag);

                // this is where we should send the information back to the owner.
                this.searchResultsCallback(searchResutls); 

                this.SearchCompleted = true;
            }

        }
    }

    public void Exit()
    {

    }

}

// this is the class used to return the results. We include it here so this code is reusable and carries its own requirements
// this class is used to package the items the class returns.
public class SearchResults
{
    public Collider[] AllHitObjectsInSearchDarius; // For someone who uses this state to get all the objects in a search radius
    public List<Collider> AllHitObjectsWithRequiredTag;

    public SearchResults(Collider[] allHitObjectsInSearchDarius, List<Collider> allHitObjectsWithRequiredTag)
    {
        this.AllHitObjectsInSearchDarius = allHitObjectsInSearchDarius;
        this.AllHitObjectsWithRequiredTag = allHitObjectsWithRequiredTag;
        // We can also further process this objects we're sending for example, closest, farthest, tags of objects it finds etc
    }
}
