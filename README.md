## Synopsis

A collection of tests that show the differences in CPU and memory usage between various operations in Unity.

Tests run:
- Loop comparisons:
  - for
  - for with a hoisted count
  - for with a hoisted index
  - for with both hoisted
  - foreach
  - while
  - while with a hosited count
  - enumerator
- Garbage creation comparison:
  - create an object and assign it to a local variable
  - create an object and assign it to an instance property
  - create an object and assign it to a class property
  - create a struct and assign it to a local variable
  - create a struct and assign it to an instance property
  - create a struct and assign it to a class property

## Installation

This is a full Unity project.

Use this by running the scene called BaseTest in the editor. To see the performace differences between the tests, open the Unity profiler.

Data is also output to a file. You can configure which the file in the BaseTest.cs file, in the OutputWriter property. Fields written include:
- test name
- number of frames executed during each test
- number of times the garbage collector executed during each test
- number of frames per GC execution for each test
- the amount of total garbage created per test
- the average amount of garbage generated per frame
- the duration of the test
- the average FPS of the test
- the average time per frame (reciprocal of average FPS)

## Contributors

Created and maintained by Tristan Bellman-Greenwood. 

## License

MIT license.
