using System;
using System.Collections.Generic;
using NUnit.Framework;
using ResourceConverter.Scripts;

namespace ResourceConverter.Tests.EditMode
{
  public class ResourcesZoneTests
  {
    [Test]
    public void Instantiate()
    {
      //Arrange
      ResourceZone resourceZone = new ResourceZone(1, 1);
      
      //Assert
      Assert.IsNotNull(resourceZone);
      Assert.AreEqual(1, resourceZone.Capacity);
      Assert.AreEqual(1, resourceZone.ResourceCount);
    }

    [TestCase(-1, 0, TestName = "Minus capacity")]
    [TestCase(1, -1, TestName = "Minus resources count")]
    [TestCase(1, 10, TestName = "Overflow resources count")]
    [TestCase(0, 0, TestName = "Zero capacity")]
    public void InstantiateOutOfRange(int capacity, int currentResourcesCount)
    {
      //Assert
      Assert.Catch<ArgumentOutOfRangeException>(() =>
      {
        _ = new ResourceZone(capacity, currentResourcesCount);
      });
    }
      
    [TestCaseSource(nameof(GetAddResourcesCases))]
    public void AddResources(int capacity, int startCount, int addCount, int expectAfter, bool expectResult)
    {
      //Arrange
      ResourceZone resourceZone = new ResourceZone(capacity, startCount);
      
      //Act
      bool success = resourceZone.AddResource(addCount);
      
      //Assert
      Assert.AreEqual(expectResult, success);
      Assert.AreEqual(expectAfter, resourceZone.ResourceCount);
    }

    private static IEnumerable<TestCaseData> GetAddResourcesCases()
    {
      yield return new TestCaseData(5, 3, 1, 4, true).SetName("Simple add");
      yield return new TestCaseData(5, 0, 5, 5, true).SetName("Full add");
      yield return new TestCaseData(5, 0, 8, 5, true).SetName("Overflow add");
      yield return new TestCaseData(5, 0, -1, 0, false).SetName("Minus add");
    }

    [TestCaseSource(nameof(GetWithdrawCases))]
    public void WithdrawResources(int capacity, int startedCount, int countToWithdraw, int expectAfter, bool expectResult)
    {
      //Arrange
      ResourceZone resourceZone = new ResourceZone(capacity, startedCount);
      
      //Act
      bool success = resourceZone.WithdrawResource(countToWithdraw);
      
      //Assert
      Assert.AreEqual(expectResult, success);
      Assert.AreEqual(expectAfter, resourceZone.ResourceCount);
    }

    private static IEnumerable<TestCaseData> GetWithdrawCases()
    {
      yield return new TestCaseData(5, 5, 1, 4, true).SetName("Simple withdraw");
      yield return new TestCaseData(5, 5, 5, 0, true).SetName("Full withdraw");
      yield return new TestCaseData(5, 5, 8, 5, false).SetName("Overflow withdraw");
      yield return new TestCaseData(5, 5, -1, 5, false).SetName("Minus withdraw");
    }
  }
}