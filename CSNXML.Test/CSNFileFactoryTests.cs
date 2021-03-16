using CSNXML.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Xunit;

namespace CSNXML.Test
{
    public class CSNFileFactoryTests
    {
        [Fact]
        public void Check_Studeranderapport_Generated_XMLString()
        {
            // Arrange
            string inputJson = File.ReadAllText("input.json");
            var studyPeriods = JsonSerializer.Deserialize<List<StudyPeriod>>(inputJson);

            // Act
            var csnXmlFile_Generated = CSNFileFactory.CSNStuderanderapportXmlDocumentString(CSNFileFactory.CSNStuderanderapport(12345, studyPeriods));

            // Assert
            var csnXmlFile_Expected = File.ReadAllText("CSNXML_ExpectedOutput.xml");
            Assert.Equal(csnXmlFile_Expected, csnXmlFile_Generated);
        }
    }
}
