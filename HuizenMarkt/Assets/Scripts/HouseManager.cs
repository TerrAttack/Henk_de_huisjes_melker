using UnityEngine;

public class HouseManager : MonoBehaviour
{
	public House[] houses;
	private House selectedHouse;
	private bool houseGotSelected = false;

	[SerializeField] EconomyManagerScript moneyScript = null;

	private void Start()
	{
		houses = new House[transform.childCount];

		for (int i = 0; i < transform.childCount; i++)
		{
			GameObject t = transform.GetChild(i).gameObject;

			houses[i] = t.GetComponent<House>();
			houses[i].id = i;
			houses[i].houseManager = this;
		}
	}

	public void ClickCheck()
	{
		houseGotSelected = false;

		foreach (House house in houses)
		{
			if (house.CheckForClick())
			{

				switch (house.houseState)
				{
					case House.HouseState.Unlocked:
						SelectHouse(house);
						houseGotSelected = true;
						break;

					case House.HouseState.Locked:
						if (moneyScript.money >= house.houseCost)
						{
							moneyScript.money -= house.houseCost;
							SelectHouse(house);
							houseGotSelected = true;
						}
						break;

					default:
						break;
				}
			}
		}
		if (!houseGotSelected)
			UnSelect();
	}

	private void SelectHouse(House _house)
	{
		if (selectedHouse != null)
			selectedHouse.SwitchState(House.HouseState.Unlocked);
		_house.SwitchState(House.HouseState.Selected);
		selectedHouse = _house;
	}

	public void UnSelect()
	{
		if (selectedHouse != null)
		{
			selectedHouse.SwitchState(House.HouseState.Unlocked);
			selectedHouse = null;
		}
	}

	public House SelectedHouse
	{
		get { return selectedHouse; }
	}
}