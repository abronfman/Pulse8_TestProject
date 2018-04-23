using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pulse8_ProgrammingTest;
using Pulse8_ProgrammingTest.DataAccess;
using Pulse8_ProgrammingTest.Models;
using System.Collections.Generic;
using System.Linq;

namespace Pulse8_ProgrammingTests.UnitTests {
    [TestClass]
    public class DoWorkWrapperUnitTests {


        private IList<MemberDiagnosisDto> testData = new List<MemberDiagnosisDto>();
        private DoWorkWrapper doer;

        [TestInitialize]
        public void init() {

            testData.Add(new MemberDiagnosisDto {
                MemberId = 99,
                FirstName = "Johnny",
                LastName = "Testy",
                MostSevereDiagnosisId = null,
                MostSevereDiagnosisDescription = string.Empty,
                DiagnosisCategoryId = 3,
                DiagnosisCategoryDescription = "Category A",
                DiagnosisCategoryScore = 20,
                IsMostSevere = false
            });

            testData.Add(new MemberDiagnosisDto {
                MemberId = 99,
                FirstName = "Johnny",
                LastName = "Testy",
                MostSevereDiagnosisId = 2,
                MostSevereDiagnosisDescription = "Most Severe",
                DiagnosisCategoryId = 2,
                DiagnosisCategoryDescription = "Category B",
                DiagnosisCategoryScore = 30,
                IsMostSevere = true
            });

            var repository = new Mock<IRepository>();
            repository.Setup(r => r.GetMemberDiagnosis(It.IsAny<int>())).Returns(testData);
            doer = new DoWorkWrapper(repository.Object);
        }

        [TestMethod]
        public void GetMemberDiagnosisDetails_ProperInput_ReturnsResults() {
            var results = doer.GetMemberDiagnosisDetails(99);

            Assert.IsNotNull(results);
            Assert.AreEqual(testData.Count, results.Count);
        }

        [TestMethod]
        public void GetMemberDiagnosisDetails_IncorrectInput_DoesNotReturnsResults() {
            var results = doer.GetMemberDiagnosisDetails(-1);

            Assert.IsNull(results);
        }

        [TestMethod]
        public void GetMemberDiagnosisDetails_ProperInput_ReturnsExpectedResults() {
            var results = doer.GetMemberDiagnosisDetails(99);

            Assert.AreEqual(2, results.Where(m => m.MemberId == 99).Count());
            Assert.AreEqual(1, results.Where(m => m.IsMostSevere).Count());
        }
    }
}
