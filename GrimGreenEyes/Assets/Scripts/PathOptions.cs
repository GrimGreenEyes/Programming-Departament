using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathOptions : MonoBehaviour
{
    // Start is called before the first frame update

    //public GameObject[] pathOpt;
  //  public Dictionary<GameObject,bool> opt = new Dictionary<GameObject, bool>();

	[SerializeField] public SubClass[] myArray;
	public void SetValue(int index, SubClass subClass)
	{

		// Perform any validation checks here.
		myArray[index] = subClass;
	}
	public SubClass GetValue(int index)
	{
		// Perform any validation checks here.
		return myArray[index];
	}

	public bool getGameobjects(GameObject node)
    {
		//Check if node is in myArray and bool isup
		foreach (var x in myArray){
			if (x.node.GetInstanceID() == node.GetInstanceID()){
				if (x.isUp == true)
					return true;
			}
        }
		return false;
    }
}
[System.Serializable]
public class SubClass
{
	public GameObject node;
	public bool isUp;
}

