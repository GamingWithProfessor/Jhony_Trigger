using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerIk : MonoBehaviour
{
    [SerializeField] RigBuilder rigBuilder;

    [Header("Constraints")]
    [SerializeField] private TwoBoneIKConstraint[] twoBoneIKConstraints;
    [SerializeField] private MultiAimConstraint[] multiAimConstraints;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConfigureIK(Transform ikTarget)
    {
        rigBuilder.enabled = true;

        foreach(TwoBoneIKConstraint twoBoneIKConstraint  in twoBoneIKConstraints)
        {
            twoBoneIKConstraint.data.target = ikTarget;
        }
        foreach(MultiAimConstraint multiAimConstraint in multiAimConstraints)
        {
            WeightedTransformArray weightedTransforms = new WeightedTransformArray();
            weightedTransforms.Add(new WeightedTransform(ikTarget, 1));

            multiAimConstraint.data.sourceObjects = weightedTransforms;
        }

        rigBuilder.Build();
    }

    public void DisableIK()
    {
        rigBuilder.enabled = false;
    }
}
