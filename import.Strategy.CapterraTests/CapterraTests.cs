using import.ImportStretegy.Strategies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using static import.ImportStretegy.Strategies.CapterraStrategy;

namespace import.Strategy.CapterraTests
{
    [TestClass]
    public class CapterraTests
    {
        [TestMethod]
        public void CapterraTestsImportValidFileExpectedReciveTheProperObjects()
        {
            // Arrange
            var objectsParsed = new List<Capterra>();
            var sut = new CapterraStrategy(new string[] { "-f", "./files/capterra.yaml" });

            // Act
            sut.Import((obj) => objectsParsed.Add(obj));

            // Assert
            Assert.AreEqual(3, objectsParsed.Count);
            Assert.AreEqual("\"Bugs & Issue Tracking,Development Tools\"", objectsParsed[0].Tags);
            Assert.AreEqual("\"GitGHub\"", objectsParsed[0].Name);
            Assert.AreEqual("\"github\"", objectsParsed[0].Twitter);

            Assert.AreEqual("\"Instant Messaging & Chat,Web Collaboration,Productivity\"", objectsParsed[1].Tags);
            Assert.AreEqual("\"Slack\"", objectsParsed[1].Name);
            Assert.AreEqual("\"slackhq\"", objectsParsed[1].Twitter);

            Assert.AreEqual("\"Project Management,Project Collaboration,Development Tools\"", objectsParsed[2].Tags);
            Assert.AreEqual("\"JIRA Software\"", objectsParsed[2].Name);
            Assert.AreEqual("\"jira\"", objectsParsed[2].Twitter);
        }
    }
}
