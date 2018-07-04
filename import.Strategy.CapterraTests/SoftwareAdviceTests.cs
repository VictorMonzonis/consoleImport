using import.ImportStretegy.Strategies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static import.ImportStretegy.Strategies.CapterraStrategy;
using static import.ImportStretegy.Strategies.SoftwareAdviceStrategy;

namespace import.Strategy.CapterraTests
{
    [TestClass]
    public class SoftwareAdviceTests
    {
        [TestMethod]
        public void SoftwareAdviceTestsImportValidFileExpectedReciveTheProperObjects()
        {
            // Arrange
            var sut = new SoftwareAdviceStrategy(new string[] { "-f", "./files/softwareadvice.json" });
            SoftwareAdvice objectsParsed = new SoftwareAdvice();
            // Act
            sut.Import(data => objectsParsed = data);

            // Assert
            Assert.AreEqual(2, objectsParsed.Products.Length);
        }
    }
}
