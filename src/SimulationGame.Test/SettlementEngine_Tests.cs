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
            settlementEngine.AddSettlement(settlementToAdd);

            // Assert
            Assert.AreEqual(1, settlementEngine.Settlements.Count);
            Assert.AreEqual(settlementToAdd, settlementEngine.Settlements[0]);
        }
    }
}