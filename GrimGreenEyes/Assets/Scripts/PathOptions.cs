using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathOptions : MonoBehaviour
{
    // Start is called before the first frame update

    //public GameObject[] pathOpt;
  //  public Dictionary<GameObject,bool> opt = new Dictionary<GameObject, bool>();

	[SerializeField] public SubClass[] myArray;

	public GameObject navigationController;
	public bool firstItem;
	//public bool isLast;


	private void Start()
	{
		navigationController = GameObject.Find("NavigationController");
		if (firstItem)
		{
			navigationController.GetComponent<Navigation>().actualNode = this.gameObject;
		}
        /*if (isLast)
		{
			Array.Resize(ref myArray, myArray.Length + 1);
			SubClass testt = new SubClass();
			myArray[1] = testt;
		}*/
    }

	private void OnDrawGizmos()
    {
		foreach(var x in myArray)
        {
			Gizmos.color = Color.blue;
			Gizmos.DrawLine(this.transform.position, x.node.transform.position);
		}
	}
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

	public void navigate(GameObject thisNode)
	{
		navigationController.GetComponent<Navigation>().moveToNode(thisNode);
	}
}
[System.Serializable]
public class SubClass
{
	public GameObject node;
	public bool isUp;
}

