
using System;

namespace Pulse8_ProgrammingTest.Utils {
    public class SmartConvert {
        /// <summary>
        /// Smartly convert anything to int
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static int ToInt(object o) {
            if (o == null || o == DBNull.Value) {
                return 0;
            }
            decimal output;

            if (decimal.TryParse(o.ToString(), out output)
                && (output <= int.MaxValue && output >= int.MinValue)) {
                return Convert.ToInt32(output);
            }
            if (o is Char || o is bool || o is Enum) {
                return Convert.ToInt32(o);
            }
            return 0;
        }

        /// <summary>
        /// Smartly convert anything to nullable int
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static int? ToNullableInt(object o) {
            if (o == null || o == DBNull.Value) {
                return null;
            }
            decimal output;

            if (decimal.TryParse(o.ToString(), out output)
                && (output <= int.MaxValue && output >= int.MinValue)) {
                return Convert.ToInt32(output);
            }
            if (o is Char || o is bool || o is Enum) {
                return Convert.ToInt32(o);
            }
            return null;
        }

        /// <summary>
        /// Proper convert anything to bool
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static Boolean ToBool(object o) {
            if (o == null || o == DBNull.Value) {
                return false;
            }

            bool val;
            if (Boolean.TryParse(o.ToString(), out val)) {
                return val;
            }

            return String.Compare(o.ToString(), "1", true) == 0;
        }


    }
}
