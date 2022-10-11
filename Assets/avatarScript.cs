using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReadyPlayerMe;
using UnityEngine.Animations.Rigging;
using System;

public class avatarScript : MonoBehaviour
{
    [SerializeField]
    private string avatarURL = "https://d1a370nemizbjq.cloudfront.net/209a1bc2-efed-46c5-9dfd-edc8a1d9cbe4.glb";
    private GameObject avatar;
    public GameObject rig;
    public GameObject BrasDroit;
    public GameObject BrasGauche;
    public GameObject TeteContrainte;
    private List<Transform> HumanBones = new List<Transform>();


    private void Start()
    {
        Debug.Log($"Started loading avatar");
        AvatarLoader avatarLoader = new AvatarLoader();
        avatarLoader.OnCompleted += AvatarLoadComplete;
        avatarLoader.OnFailed += AvatarLoadFail;
        avatarLoader.LoadAvatar(avatarURL);
    }
    private string GetAvatarNameByUrl(string url)
    {
        return url.Split("/")[url.Split("/").Length-1].Split(".")[url.Split("/")[url.Split("/").Length-1].Split(".").Length-2];
    }


    private void AvatarLoadComplete(object sender, CompletionEventArgs args)
    {
        
        avatar = GameObject.Find(GetAvatarNameByUrl(avatarURL));

        avatar.AddComponent<RigBuilder>();
        

        avatar.GetComponent<Animator>().runtimeAnimatorController = null;
        
        avatar.AddComponent<BoneRenderer>();
        avatar.GetComponent<BoneRenderer>().transforms = GetAllHumanoidBones(avatar.GetComponent<Animator>());
        
        var rig2 = Instantiate(rig, avatar.transform);
        avatar.GetComponent<RigBuilder>().layers.Clear();
        avatar.GetComponent<RigBuilder>().layers.Add(new RigLayer(rig2.GetComponent<Rig>()));
        var BrasDroit2 = Instantiate(BrasDroit, rig2.transform);

        

        

        BrasDroit2.GetComponent<TwoBoneIKConstraint>().data.root = HumanBones[14];
        BrasDroit2.GetComponent<TwoBoneIKConstraint>().data.mid = HumanBones[16];
        BrasDroit2.GetComponent<TwoBoneIKConstraint>().data.tip = HumanBones[18];

        BrasDroit2.GetComponent<TwoBoneIKConstraint>().data.target = BrasDroit2.transform.Find("Target");
        BrasDroit2.GetComponent<TwoBoneIKConstraint>().data.hint = BrasDroit2.transform.Find("Hint");

        BrasDroit2.transform.Find("Target").transform.position = HumanBones[18].transform.position;
        BrasDroit2.transform.Find("Hint").transform.position = HumanBones[17].transform.position;
        BrasDroit2.transform.Find("Target").transform.rotation = HumanBones[18].transform.rotation;
        BrasDroit2.transform.Find("Hint").transform.rotation = HumanBones[17].transform.rotation;

        
        avatar.GetComponent<RigBuilder>().Build();
        avatar.GetComponent<Animator>().enabled = true;
        avatar.GetComponent<Animator>().Rebind();
        /*avatar = GameObject.Find(GetAvatarNameByUrl(avatarURL));
        avatar.GetComponent<Animator>().runtimeAnimatorController = null;
        avatar.AddComponent<RigBuilder>();
        

        avatar.AddComponent<BoneRenderer>();
        avatar.GetComponent<BoneRenderer>().transforms = GetAllHumanoidBones(avatar.GetComponent<Animator>());
        var rig2 = Instantiate(rig, avatar.transform);
        avatar.GetComponent<RigBuilder>().layers.Clear();
        avatar.GetComponent<RigBuilder>().layers.Add(new RigLayer(rig2.GetComponent<Rig>()));
        avatar.GetComponent<RigBuilder>().Build();


        var BrasGauche2 = Instantiate(BrasGauche, rig2.transform);
        var BrasDroit2 = Instantiate(BrasDroit, rig2.transform);
        var TeteContrainte2 = Instantiate(TeteContrainte, rig2.transform);

        

        BrasGauche2.GetComponent<TwoBoneIKConstraint>().data.root = HumanBones[13];
        BrasGauche2.GetComponent<TwoBoneIKConstraint>().data.mid = HumanBones[15];
        BrasGauche2.GetComponent<TwoBoneIKConstraint>().data.tip = HumanBones[17];

        BrasGauche2.GetComponent<TwoBoneIKConstraint>().data.target = BrasGauche2.transform.Find("Target");
        BrasGauche2.GetComponent<TwoBoneIKConstraint>().data.hint = BrasGauche2.transform.Find("Hint");

        BrasGauche2.transform.Find("Target").transform.position = HumanBones[17].transform.position;
        BrasGauche2.transform.Find("Hint").transform.position = HumanBones[16].transform.position;
        BrasGauche2.transform.Find("Target").transform.rotation = HumanBones[17].transform.rotation;
        BrasGauche2.transform.Find("Hint").transform.rotation = HumanBones[16].transform.rotation;

        BrasDroit2.GetComponent<TwoBoneIKConstraint>().data.root = HumanBones[14];
        BrasDroit2.GetComponent<TwoBoneIKConstraint>().data.mid = HumanBones[16];
        BrasDroit2.GetComponent<TwoBoneIKConstraint>().data.tip = HumanBones[18];

        BrasDroit2.GetComponent<TwoBoneIKConstraint>().data.target = BrasDroit2.transform.Find("Target");
        BrasDroit2.GetComponent<TwoBoneIKConstraint>().data.hint = BrasDroit2.transform.Find("Hint");

        BrasDroit2.transform.Find("Target").transform.position = HumanBones[18].transform.position;
        BrasDroit2.transform.Find("Hint").transform.position = HumanBones[17].transform.position;
        BrasDroit2.transform.Find("Target").transform.rotation = HumanBones[18].transform.rotation;
        BrasDroit2.transform.Find("Hint").transform.rotation = HumanBones[17].transform.rotation;

        TeteContrainte2.GetComponent<MultiParentConstraint>().data.constrainedObject = HumanBones[10];
        var tamp = new WeightedTransformArray();
        tamp.Add(new WeightedTransform(TeteContrainte.transform, 1f));
        TeteContrainte2.GetComponent<MultiParentConstraint>().data.sourceObjects = tamp;
        TeteContrainte2.transform.position = HumanBones[9].transform.position;
        TeteContrainte2.transform.rotation = HumanBones[9].transform.rotation;*/



    }

    private void AvatarLoadFail(object sender, FailureEventArgs args)
    {
        Debug.Log($"Avatar loading failed with error message: {args.Message}");
    }
    private Transform[] GetAllHumanoidBones(Animator _animator)
    {

        if (_animator == null) return null;
        
        foreach (HumanBodyBones bone in Enum.GetValues(typeof(HumanBodyBones)))
        {
            if (bone != HumanBodyBones.LastBone)
            {
                Transform tamp = _animator.GetBoneTransform(bone);
                if (tamp != null)
                {
                    HumanBones.Add(tamp);
                }
               
            }

        }
        return HumanBones.ToArray();
    }
}
