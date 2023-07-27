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
            var settlementToAdd = new Settlement("Test", "Test", "Test", 1);

            // Act
            settlementEngine.AddSettlement(settlementToAdd);

            // Assert
            Assert.AreEqual(1, settlementEngine.Settlements.Count);
            Assert.AreEqual(settlementToAdd, settlementEngine.Settlements[0]);
        }
    }
}