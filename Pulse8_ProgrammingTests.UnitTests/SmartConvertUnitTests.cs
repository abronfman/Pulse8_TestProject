using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pulse8_ProgrammingTest.Utils;
using System.Data.SqlTypes;

namespace Pulse8_ProgrammingTests.UnitTests {
    [TestClass]
    public class SmartConvertUnitTests {

        #region ToInt Tests

        [TestMethod]
        public void ToIntTest() {
            const string SAMPLE_STRING = "fortythree";
            const string SAMPLE_NUMBER = "45";

            Assert.IsTrue(SmartConvert.ToInt(SAMPLE_NUMBER) == 45);
            Assert.IsFalse(SmartConvert.ToInt(SAMPLE_STRING) == 45);
        }

        [TestMethod]
        public void ToIntTest_WithFloats() {
            var actual = SmartConvert.ToInt(55.4699);
            Assert.AreEqual(actual, 55);

            actual = SmartConvert.ToInt(55.6699);
            Assert.AreEqual(actual, 56);

            actual = SmartConvert.ToInt("55.6699");
            Assert.AreEqual(actual, 56);
        }

        [TestMethod]
        public void ToIntTest_WithMoreThanInteger() {
            var actual = SmartConvert.ToInt("559999999999999.458989894679");
            Assert.AreEqual(0, actual);

            actual = SmartConvert.ToInt("-5599999999999999.458989894679");
            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void ToIntTest_WithInvalidObject() {
            var actual = SmartConvert.ToInt("55.44.45");
            Assert.AreEqual(0, actual);

            actual = SmartConvert.ToInt("test 55.45.89");
            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void ToIntTest_WithNullsAndEmpties() {
            var actual = SmartConvert.ToInt(null);
            Assert.AreEqual(0, actual);

            actual = SmartConvert.ToInt(DBNull.Value);
            Assert.AreEqual(0, actual);

            actual = SmartConvert.ToInt(string.Empty);
            Assert.AreEqual(0, actual);
        }
        #endregion //ToInt Tests

        #region ToBool Tests

        [TestMethod]
        public void ToBoolean_WhenTrue_ReturnsTrue() {
            const string SAMPLE_STRING = "first";
            const int SAMPLE_NUMBER = 45;

            Assert.IsTrue(SmartConvert.ToBool(SAMPLE_NUMBER == 45));
            Assert.IsTrue(SmartConvert.ToBool(SAMPLE_STRING == "first"));
            Assert.IsTrue(SmartConvert.ToBool(1));
            Assert.IsTrue(SmartConvert.ToBool(new SqlInt32(1)));
            Assert.IsTrue(SmartConvert.ToBool(true));
            Assert.IsTrue(SmartConvert.ToBool("true"));
            Assert.IsTrue(SmartConvert.ToBool("TRUE"));
        }

        [TestMethod]
        public void ToBoolean_WhenFalsy_ReturnsFalse() {
            const string SAMPLE_STRING = "first";
            const int SAMPLE_NUMBER = 45;

            Assert.IsFalse(SmartConvert.ToBool('#'));
            Assert.IsFalse(SmartConvert.ToBool(Double.NaN));
            Assert.IsFalse(SmartConvert.ToBool(Double.PositiveInfinity));
            Assert.IsFalse(SmartConvert.ToBool(SAMPLE_STRING == "second"));
            Assert.IsFalse(SmartConvert.ToBool(SAMPLE_NUMBER == 55));
            Assert.IsFalse(SmartConvert.ToBool(0));
            Assert.IsFalse(SmartConvert.ToBool(null));
            Assert.IsFalse(SmartConvert.ToBool(DBNull.Value));
            Assert.IsFalse(SmartConvert.ToBool(false));
            Assert.IsFalse(SmartConvert.ToBool("false"));
            Assert.IsFalse(SmartConvert.ToBool("FALSE"));

        }
        #endregion //ToBool Tests
    }
}
