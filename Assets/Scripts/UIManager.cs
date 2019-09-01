using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

	public GameObject searchUI;
	public GameObject foundUI;
	public GameObject changeAd;

	private GameObject trackedGO;
	private GameObject selectedPrefab = null;
	private int index = 0;
	private int length = -1;

	void Start(){
		searchUI.SetActive (true);
		foundUI.SetActive (false);
		changeAd.SetActive (false);
	}

	public void OnFound(string name){
		trackedGO = GameObject.Find (name);
		if (trackedGO) {
			searchUI.SetActive (false);
			foundUI.SetActive (true);
			length = trackedGO.transform.childCount;
			if(length>1)
				selectedPrefab = trackedGO.transform.GetChild (index).gameObject;
			trackedGO.transform.GetChild (length-1).gameObject.SetActive (true);
		}
	}

	public void OnLost(){
		if (trackedGO) {
			foundUI.SetActive (false);
			searchUI.SetActive (true);
			changeAd.SetActive (false);
			if (selectedPrefab) {
				selectedPrefab.SetActive (false);
				selectedPrefab = null;
			}
			trackedGO.transform.GetChild (length-1).gameObject.SetActive (false);
		}
	}

	public void SeeIt(){
		foundUI.SetActive (false);
		if(length>2)
			changeAd.SetActive (true);
		if (selectedPrefab) {
			selectedPrefab.SetActive (true);
		}
		if (trackedGO)
			trackedGO.transform.GetChild (length-1).gameObject.SetActive (false);
	}

	public void NextPrefab(){
		if (selectedPrefab) {
			selectedPrefab.SetActive (false);
			index = (index + 1) % (length-1);
			selectedPrefab = trackedGO.transform.GetChild(index).gameObject;
			selectedPrefab.SetActive (true);
		}
	}

	public void PreviousPrefab(){
		if (selectedPrefab) {
			selectedPrefab.SetActive (false);
			index = (index - 1 + (length-1)) % (length-1);
			selectedPrefab = trackedGO.transform.GetChild(index).gameObject;
			selectedPrefab.SetActive (true);
		}
	}
}
