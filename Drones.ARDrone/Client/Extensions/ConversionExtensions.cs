using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones.ARDrone.Client.Extensions
{
    public static class ConversionExtensions
    {
        /// <summary> Convert to an int according to IEEE-74 format. </summary>
        ///
        /// <param name="value"> The value to act on. </param>
        ///
        /// <returns> value as an int. </returns>
        public static int ToInt(this float value)
        {
            int result = BitConverter.ToInt32(BitConverter.GetBytes(value), 0);
            return result;
        }

        /// <summary> Convert to a float according to IEEE-74 format. </summary>
        ///
        /// <param name="value"> The value to act on. </param>
        ///
        /// <returns> value as a float. </returns>
        public static float ToFloat(this int value)
        {
            float result = BitConverter.ToSingle(BitConverter.GetBytes(value), 0);
            return result;
        }

        /// <summary> Convert to a float according to IEEE-74 format. </summary>
        ///
        /// <param name="value"> The value to act on. </param>
        ///
        /// <returns> value as a float. </returns>
        public static float ToFloat(this uint value)
        {
            float result = BitConverter.ToSingle(BitConverter.GetBytes(value), 0);
            return result;
        }
    }
}