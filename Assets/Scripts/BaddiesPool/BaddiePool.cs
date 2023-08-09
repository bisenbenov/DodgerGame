using UnityEngine;

public class BaddiePool : ObjectPool<Baddie>
{
    public BaddiePool(Baddie prefab, int preloadCount)
        : base (() => Preload(prefab), GetAction, ReturnAction, preloadCount)
    {    }

    public static Baddie Preload(Baddie prefab)
    {
        var baddie = InstantiateBaddie(prefab);
        return baddie;
    }

    public static void GetAction(Baddie baddie) 
    {
        baddie.gameObject.SetActive(true); 
    }

    public static void ReturnAction(Baddie baddie) 
    {
        if (baddie != null)
        {
            baddie.gameObject.SetActive(false);
        }
    }

    public static Baddie InstantiateBaddie(Baddie baddie)
    {
        var newBaddie = GameObject.Instantiate(baddie);
        return newBaddie;
    }
}
