using SimulationGame.Logic;
using SimulationGame.Models;

namespace SimulationGame.Test
{
    [TestClass]
    public class SettlementEngine_Tests
    {
        [TestMethod]
        public void Test_AddSettlement_ShouldAddSettlementToSettlementsList()
        {
            // Arrange
            var settlementEngine = new SettlementEngine();

            // Act
            settlementEngine.AddSettlement("Test", "Test");

            // Assert
            Assert.AreEqual(1, settlementEngine.Settlements.Count);
            Assert.AreEqual("Test", settlementEngine.Settlements[0].Name);
            Assert.AreEqual("Test", settlementEngine.Settlements[0].Description);
        }

        [TestMethod]
        public void Test_RemoveSettlement_RemovesSettlementFromSettlementsList()
        {
            // Arrange
            var settlementEngine = new SettlementEngine();
            settlementEngine.AddSettlement("Test", "Test");

            Assert.AreEqual(1, settlementEngine.Settlements.Count);

            // Act
            var settlementToRemove = settlementEngine.Settlements[0];
            settlementEngine.RemoveSettlement(settlementToRemove);

            // Assert
            Assert.AreEqual(0, settlementEngine.Settlements.Count);
        }
        /*
        [TestMethod]
        public void Test_GetSettlementById_ReturnsCorrectSettlement()
        {
            // Arrange
            var settlementEngine = new SettlementEngine();
            var settlement1 = new Settlement("Test1", "Test", "Test", 1);
            var settlement2 = new Settlement("Test2", "Test", "Test", 2);
            var settlement3 = new Settlement("Test3", "Test", "Test", 3);
            settlementEngine.AddSettlement(settlement1);
            settlementEngine.AddSettlement(settlement2);
            settlementEngine.AddSettlement(settlement3);

            // Act
            var result = settlementEngine.GetSettlementById(2);

            // Assert
            Assert.AreEqual(settlement2, result);
        }*/
        /*
        [TestMethod]
        public void Test_GetSettlementsByOwnerId_ReturnsCorrectSettlements()
        {
            // Arrange
            var settlementEngine = new SettlementEngine();
            var settlement1 = new Settlement("Test1", "Test", "Test", 1);
            var settlement2 = new Settlement("Test2", "Test", "Test", 2);
            var settlement3 = new Settlement("Test3", "Test", "Test", 1);
            settlementEngine.AddSettlement(settlement1);
            settlementEngine.AddSettlement(settlement2);
            settlementEngine.AddSettlement(settlement3);

            // Act
            var result = settlementEngine.GetSettlementsByOwnerId(1);

            // Assert
            Assert.AreEqual(2, result.Count);
            CollectionAssert.Contains(result, settlement1);
            CollectionAssert.Contains(result, settlement3);
        }*/
        /*
        [TestMethod]
        public void Test_GetSettlementsByLocation_ReturnsCorrectSettlements()
        {
            // Arrange
            var settlementEngine = new SettlementEngine();
            var settlement1 = new Settlement("Test1", "Test", "Test1", 1);
            var settlement2 = new Settlement("Test2", "Test", "Test2", 2);
            var settlement3 = new Settlement("Test3", "Test", "DifferentLocation", 3);
            settlementEngine.AddSettlement(settlement1);
            settlementEngine.AddSettlement(settlement2);
            settlementEngine.AddSettlement(settlement3);

            // Act
            var result = settlementEngine.GetSettlementsByLocation("Test");

            // Assert
            Assert.AreEqual(2, result.Count);
            CollectionAssert.Contains(result, settlement1);
            CollectionAssert.Contains(result, settlement2);
        }*/

        [TestMethod]
        public void Test_GetSettlementsByType_ReturnsCorrectSettlements()
        {
            // Arrange
            var settlementEngine = new SettlementEngine();
            settlementEngine.AddSettlement("Test1", "Test", Enums.SettlementTypes.Village, 1);
            settlementEngine.AddSettlement("Test2", "Test", Enums.SettlementTypes.City, 10);
            settlementEngine.AddSettlement("Test3", "Test", Enums.SettlementTypes.Village, 2);

            // Act
            var result = settlementEngine.Settlements.Where(x => x.Type == Enums.SettlementTypes.Village).ToList();
            //var result = settlementEngine.GetSettlementsByType("DifferentType");

            // Assert
            Assert.AreEqual(2, result.Count());
            CollectionAssert.Contains(result, settlementEngine.Settlements[0]);
            CollectionAssert.Contains(result, settlementEngine.Settlements[2]);
        }

        [TestMethod]
        public void Test_GetAllSettlements_ReturnsAllSettlements()
        {
            // Arrange
            var settlementEngine = new SettlementEngine();
            settlementEngine.AddSettlement("Test1", "Test", Enums.SettlementTypes.Village, 1);
            settlementEngine.AddSettlement("Test2", "Test", Enums.SettlementTypes.City, 10);
            settlementEngine.AddSettlement("Test3", "Test", Enums.SettlementTypes.Village, 2);

            // Act
            var result = settlementEngine.Settlements;

            // Assert
            Assert.AreEqual(3, result.Count);
            CollectionAssert.Contains(result, settlementEngine.Settlements[0]);
            CollectionAssert.Contains(result, settlementEngine.Settlements[1]);
            CollectionAssert.Contains(result, settlementEngine.Settlements[2]);
        }
    }
}