using System;
using System.Collections.Generic;
using NUnit.Framework;
using ResourceConverter.Scripts;

namespace ResourceConverter.Tests.EditMode
{
  public class ConverterTests
  {
    [Test]
    public void ConverterInstantiate()
    {
      //Arrange
      int inputCount = 1;
      int outputCount = 1;
      float interval = 0;

      //Act
      Converter converter = new Converter(
        10,
        0,
        10,
        0,
        inputCount,
        outputCount,
        interval);

      //Assert
      Assert.IsNotNull(converter);
      Assert.AreEqual(inputCount, converter.InputCount);
      Assert.AreEqual(outputCount, converter.OutputCount);
      Assert.AreEqual(10, converter.InputZoneCapacity);
      Assert.AreEqual(0, converter.InputZoneAmount);
      Assert.AreEqual(10, converter.OutputZoneCapacity);
      Assert.AreEqual(0, converter.OutputZoneAmount);
      Assert.IsFalse(converter.IsWorking);
    }

    [TestCaseSource(nameof(GetOutOfRangeArgs))]
    public void ConverterInstantiateOutOfRange(int inputCount, int outputCount, float interval)
    {
      //Assert

      Assert.Catch<ArgumentOutOfRangeException>(() =>
      {
        var _ = new Converter(
          1,
          1,
          1,
          1,
          inputCount,
          outputCount,
          interval);
      });
    }

    private static IEnumerable<TestCaseData> GetOutOfRangeArgs()
    {
      yield return new TestCaseData(-1, 1, 1).SetName("Minus input count");
      yield return new TestCaseData(1, -1, 1).SetName("Minus output count");
      yield return new TestCaseData(1, 1, -1).SetName("Minus interval");
    }

    [Test]
    public void StartConverter()
    {
      //Arrange
      Converter converter = new Converter(
        10,
        5,
        10,
        5,
        1,
        1,
        0);

      //Act
      converter.Start();

      //Assert
      Assert.IsTrue(converter.IsWorking);
    }

    [Test]
    public void StopConverter()
    {
      //Arrange
      Converter converter = new Converter(
        10,
        0,
        10,
        5,
        1,
        1,
        0);

      //Act
      converter.Start();
      converter.Stop();

      //Assert
      Assert.IsFalse(converter.IsWorking);
    }

    [Test]
    public void StopConverterInProgress()
    {
      //Arrange
      Converter converter = new Converter(
        10,
        5,
        10,
        5,
        1,
        1,
        1);

      //Act
      converter.Start();
      converter.Update(0.5f);
      converter.Stop();

      //Assert
      Assert.IsFalse(converter.IsWorking);
      Assert.AreEqual(5, converter.InputZoneAmount);
      Assert.AreEqual(5, converter.OutputZoneAmount);
    }

    [Test]
    public void StopConverterInProgressWithOverflowInputZone()
    {
      //Arrange
      Converter converter = new Converter(
        10,
        5,
        10,
        0,
        5,
        1,
        1);

      //Act
      converter.Start();
      converter.Update(0.5f);
      converter.AddResourcesToInputZone(6, out int overflow);
      converter.Stop();

      //Assert
      Assert.IsFalse(converter.IsWorking);
      Assert.AreEqual(0, overflow);
      Assert.AreEqual(10, converter.InputZoneAmount);
      Assert.AreEqual(0, converter.OutputZoneAmount);
    }

    [Test]
    public void UpdateIfNotStarted()
    {
      //Arrange
      Converter converter = new Converter(
        10,
        5,
        10,
        0,
        1,
        1,
        0.5f);

      //Act
      converter.Update(0.5f);

      //Assert
      Assert.AreEqual(5, converter.InputZoneAmount);
      Assert.AreEqual(0, converter.OutputZoneAmount);
    }

    [TestCase(0, TestName = "Zero")]
    [TestCase(-1, TestName = "Minus")]
    public void UpdateDeltaTime(float deltaTime)
    {
      //Arrange
      Converter converter = new Converter(
        10,
        5,
        10,
        0,
        1,
        1,
        0.5f);

      //Act
      Assert.Catch<ArgumentOutOfRangeException>(() => { converter.Update(deltaTime); });
    }

    [TestCaseSource(nameof(GetUpdateCases))]
    public void Update(Converter converter, int expectResourceCountInputZone, int expectResourceCountOutputZone,
      float deltaTime, int updateTimes)
    {
      //Act
      converter.Start();
      converter.Update(deltaTime * updateTimes);
      converter.Stop();

      //Assert
      Assert.AreEqual(expectResourceCountInputZone, converter.InputZoneAmount);
      Assert.AreEqual(expectResourceCountOutputZone, converter.OutputZoneAmount);
    }

    private static IEnumerable<TestCaseData> GetUpdateCases()
    {
      yield return new TestCaseData(new Converter(
          10,
          10,
          10,
          0,
          1,
          1,
          1),
        9, 1, 1, 1).SetName("One time");

      yield return new TestCaseData(new Converter(
          10,
          10,
          10,
          0,
          1,
          1,
          1),
        0, 10, 1, 10).SetName("Full");

      yield return new TestCaseData(new Converter(
          10,
          1,
          10,
          0,
          1,
          10,
          1),
        0, 10, 1, 1).SetName("Full big difference");

      yield return new TestCaseData(new Converter(
         10,
         1,
         10,
         0,
          1,
          20,
          1),
        1, 0, 1, 1).SetName("Overflow difference");

      yield return new TestCaseData(new Converter(
          10,
          10,
          10,
          9,
          1,
          1,
          1),
        9, 10, 1, 5).SetName("Overflow output");

      yield return new TestCaseData(new Converter(
          10,
          3,
          10,
          0,
          1,
          1,
          1),
        0, 3, 1, 5).SetName("Input empty");
    }

    [Test]
    public void TestAddResourcesOutOfRange()
    {
      //Arrange
      Converter converter = new Converter(
        10,
        0,
        10,
        0,
        1,
        1,
        1);

      //Assert
      Assert.Catch<ArgumentOutOfRangeException>(() => { converter.AddResourcesToInputZone(-1, out int overflowCount); });
    }

    [TestCase(0, 1, 0, 1, TestName = "Simple")]
    [TestCase(9, 5, 4, 10, TestName = "Overflow")]
    public void TestAddResources(int startCount, int addCount, int expectedOverflowCount, int expectedResourcesCount)
    {
      //Arrange
      Converter converter = new Converter(
        10,
        startCount,
        10,
        0,
        1,
        1,
        1);

      //Act
      converter.AddResourcesToInputZone(addCount, out int overflowCount);

      //Assert
      Assert.AreEqual(expectedResourcesCount, converter.InputZoneAmount);
      Assert.AreEqual(expectedOverflowCount, overflowCount);
    }
  }
}