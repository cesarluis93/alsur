using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;


public class MapInfo
{
	public int type;
	public int[][] floor;
	public int[][] enviorment;

	public MapInfo() {
	}

	public MapInfo(int type, int[][] floor, int [][] enviorment) {
		this.type = type;
		this.floor = floor;
		this.enviorment = enviorment;
	}

	public static MapInfo createFromJSON(string path) {
		string data = File.ReadAllText (path);
		var map = JSON.Parse(data);
		int type = map["type"].AsInt;

		// Need object to access other methods from class
		MapInfo info = new MapInfo();

		// Read the floor info
		int[][] floor = info.jsonNodeTo2Array (map["floor"].AsArray);

		// Read the enviorment info
		int [][] enviorment = info.jsonNodeTo2Array(map["enviorment"].AsArray);

		return new MapInfo(type, floor, enviorment);
	}

	// Take a json node array and make it a two dimension array
	public int[][] jsonNodeTo2Array(SimpleJSON.JSONArray jsonArray){
		int rows = jsonArray.Count;
		int[][] mmArray = new int[jsonArray.Count][];
		for (int i = 0; i < rows; i++){
			int[] row = new int[jsonArray[i].Count];
			for (int j = 0; j < jsonArray[i].Count; j++){
				row[j] = jsonArray[i][j].AsInt;
			}
			mmArray[i] = row;
		}
		return mmArray;
	}

	public int getType(){
		return type;
	}

	public void setType(int type){
		this.type = type;
	}

	public int[][] getFloor(){
		return floor;
	}

	public void setFloor(int[][] floor){
		this.floor = floor;
	}

	public int[][] getEnviorment(){
		return enviorment;
	}

	public void setEnviorment(int[][] enviorment){
		this.enviorment = enviorment;
	}
}

