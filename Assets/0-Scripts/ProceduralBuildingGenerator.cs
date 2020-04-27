using System.Collections;
using System.Collections.Generic;
using UnityEditor.iOS;
using UnityEngine;

[ExecuteInEditMode]
public class ProceduralBuildingGenerator : MonoBehaviour {
    public int currentLevel = 0;
    public int maxNumberOfLevels;
    public GameObject floorSlot, ceilingSlot, wallSlotPZ, wallSlotNZ, wallSlotPX, wallSlotNX;
    private Dictionary<int, List<string>> records = new Dictionary<int, List<string>>();
    private string[] directions = new string[] {"PX", "NZ", "PX", "NZ"};

    private string[] wallTypes = new string[] {"Wall_WithDoor", "Wall_Plain", "WallWindowed_Type1", "WallWindowed_Type2"};
    private int numOfLevels = 0;

    public void GenerateBuildingForLevel(int levelNo, int maxLevels) {
        currentLevel = levelNo;
        numOfLevels = maxLevels;
        ConstructLevel();
    }
    
    public void ConstructLevel() {
        Debug.Log("currentLevel="+currentLevel);
        if (currentLevel == 0) {
            numOfLevels = GetMaxLevels();
        }

        
        
            GameObject floor = Instantiate(ChooseFloorType(currentLevel), floorSlot.transform);
            
            
            
            // PZ0, NZ0, PX0, NX0
            // 0,   1,   2,   3
            for (int j = 0; j < directions.Length; j++) {
                GameObject parentGO = j == 0 ? wallSlotPZ : (j == 1 ? wallSlotNZ : (j == 2 ? wallSlotPX : wallSlotNX));
                GameObject currentWall = Instantiate(ChooseWallType(directions[j], currentLevel), parentGO.transform);
            }
            
           

            if (currentLevel == numOfLevels - 1) {
                Instantiate(Resources.Load("Roof_Type1") as GameObject, ceilingSlot.transform);
            } else {
                GameObject newPrototype = Instantiate(Resources.Load("BuildingPrototype_1") as GameObject, ceilingSlot.transform);
                newPrototype.GetComponent<ProceduralBuildingGenerator>().GenerateBuildingForLevel(currentLevel +1, numOfLevels);
            }
            
            
        
    }
    
    private int GetMaxLevels() {
        int maxLevelChoice = Random.Range(1, maxNumberOfLevels + 1);
        Debug.Log(maxLevelChoice);
        return maxLevelChoice;
    }

    private GameObject ChooseFloorType(int levelNo) {
        Debug.Log("ChooseFloorType(int levelNo) -> incoming levelNo = "+levelNo);
        if (levelNo == 0) {
            return Resources.Load("Floor_10x10") as GameObject;
        } else {
            return Resources.Load("Floor_10x10_Type2") as GameObject;
        }
    }

    private GameObject ChooseWallType(string slotNo, int levelNo) {
        bool mustHaveDoor = false;
        bool isDoorPlaced = false;
        if (levelNo == 0) {
            mustHaveDoor = true;
        }

        int currentNumberOfWallsInLevel = 0;
        if (records.ContainsKey(levelNo)) {
            currentNumberOfWallsInLevel = records[levelNo].Count;
        }

        if (mustHaveDoor && currentNumberOfWallsInLevel == 3 && !isDoorPlaced) {
            records[levelNo].Add("Wall_WithDoor");
            return Resources.Load("Wall_WithDoor") as GameObject;
        } else {
            if (mustHaveDoor) {
                int selectionIndex = Random.Range(0, wallTypes.Length);
                string nameOfSelectedWallPrefab = wallTypes[selectionIndex];
                if (selectionIndex == 0) {
                    isDoorPlaced = true;
                }

                if (!records.ContainsKey(levelNo)) {
                    List<string> namesOfLevelWalls = new List<string>();
                    records.Add(levelNo, namesOfLevelWalls);
                }
                records[levelNo].Add(nameOfSelectedWallPrefab);
                
                return Resources.Load(nameOfSelectedWallPrefab) as GameObject;

            } else {
                int selectionIndex = Random.Range(1, wallTypes.Length);
                string nameOfSelectedWallPrefab = wallTypes[selectionIndex];
                
                if (!records.ContainsKey(levelNo)) {
                    List<string> namesOfLevelWalls = new List<string>();
                    records.Add(levelNo, namesOfLevelWalls);
                }
                
                records[levelNo].Add(nameOfSelectedWallPrefab);
                return Resources.Load(nameOfSelectedWallPrefab) as GameObject;
            }
        }
    }
}
