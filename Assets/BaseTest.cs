using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Profiling;

public abstract class BaseTest : MonoBehaviour {
	public static BaseTest Instance;
	public static List<BaseTest> BenchTests = new List<BaseTest>();
	private static int _currentTest = 0;
	
	private const int PreTestFrames = 5;
	private const int FramesPerTest = 1000;

	private static int _currentFrame = -PreTestFrames;
	
	protected static long[] MemoryUsed = new long[FramesPerTest];

	private static long _previousMemoryUsage = 0L;
	private static bool _waitForGc = true;

	private static DateTime _currentTestStartTime;
	
	private static readonly StreamWriter OutputWriter = new StreamWriter(@"D:\Downloads\benchOut.csv");

	private const int TestListCount = 1000000;
	protected const int TestAllocationCount = 10000;
	
	protected static List<TestStruct> TestList = new List<TestStruct>(TestListCount);
	
	private void Awake () {
		if (Instance == null) {
			Instance = this;
			
			for (int i = 0; i < TestListCount; i++) {
				TestList.Add(new TestStruct(i));
			}
			
			OutputWriter.WriteLine("number," +
			                       "name," +
			                       "frames," +
			                       "gc count," +
			                       "frames per gc," +
			                       "garbage generated," +
			                       "garbage generated per frame," +
			                       "duration," +
			                       "average frames per second," +
			                       "average tme per frame");
		}
		
		BenchTests.Add(this);
	}

	private void Start () {
		if (Instance == this) {
			Debug.Log("Tests to be run: " + BenchTests.Count);
		}
	}

	private void Update () {
		if (Instance != this) {
			return;
		}
		
		if (_currentFrame < 0) {
			_currentFrame++;
			return;
		}

		if (_waitForGc) {
			long currentMemoryUsage = Profiler.GetMonoUsedSizeLong();
			
			if (currentMemoryUsage < _previousMemoryUsage) {
				_waitForGc = false;
				_currentTestStartTime = DateTime.Now;
			} else {
				_previousMemoryUsage = currentMemoryUsage;
				return;
			}
		}

		if (_currentTest < BenchTests.Count) {
			BenchTests[_currentTest].Test();
			
			MemoryUsed[_currentFrame] = Profiler.GetMonoUsedSizeLong();
			
			_currentFrame++;

			if (_currentFrame == FramesPerTest) {
				double duration = DateTime.Now.Subtract(_currentTestStartTime).TotalMilliseconds;
			
				long lastMem = MemoryUsed[0];

				int collectionCount = 0;
				long garbageGeneratedTotal = 0L;
				int garbageGeneratedFramesCount = 0;
			
				for (int i = 1; i < FramesPerTest; i++) {
					long currentMem = MemoryUsed[i];

					if (currentMem < lastMem) {
						collectionCount++;
					} else {
						garbageGeneratedTotal += currentMem - lastMem;
						garbageGeneratedFramesCount++;
					}

					lastMem = currentMem;
				}
			
				Debug.Log("Test name: " + BenchTests[_currentTest].GetType().FullName);
				Debug.Log("Number of times GC ran: " + collectionCount);
				Debug.Log("Frames per GC: " + FramesPerTest / (float)collectionCount);
				Debug.Log("Amount of garbage generated: " + garbageGeneratedTotal + " bytes");
				Debug.Log("Garbage generated per frame: " + garbageGeneratedTotal / (float)garbageGeneratedFramesCount + " bytes");
				Debug.Log("Total test time: " + duration + " ms");
				Debug.Log("Frames per second: " + FramesPerTest / (duration / 1000.0));
				Debug.Log("Average time per frame: " + duration / FramesPerTest + " ms");
				
				OutputWriter.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", 
					_currentTest,
					BenchTests[_currentTest].GetType().FullName,
					FramesPerTest,
					collectionCount,
					FramesPerTest / (float)collectionCount,
					garbageGeneratedTotal,
					garbageGeneratedTotal / (float)garbageGeneratedFramesCount,
					duration,
					FramesPerTest / (duration / 1000.0),
					duration / FramesPerTest);
				
				BenchTests[_currentTest].CleanUp();

				_currentFrame = 0;
				_currentTest++;
				_waitForGc = true;

				if (_currentTest == BenchTests.Count) {
					Debug.Log("All Done!");
					OutputWriter.Close();
				}
			}
		}
	}

	public abstract void Test ();
	public virtual void CleanUp () {}
}
