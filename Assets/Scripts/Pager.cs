using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pager : MonoBehaviour {

	public Image[] pages;
	private int currentPage;

	void Start() {
		currentPage = 0;
		foreach (var page in pages) {
			page.enabled = false;
		}
		pages [currentPage].enabled = true;
	}

	public void GoNextPage () {
		pages [currentPage].enabled = false;
		if ((currentPage + 1) >= pages.Length) {
			currentPage = 0;
		} else {
			currentPage++;
		}
		pages [currentPage].enabled = true;
	}

	public void GoPreviousPage () {
		if ((currentPage - 1) >= 0) {
			pages [currentPage].enabled = false;
			currentPage--;
		}
		pages [currentPage].enabled = true;
	}
}
