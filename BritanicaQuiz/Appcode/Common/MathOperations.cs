namespace BritanicaQuiz.Appcode.Common
{
    using System;
    using System.Collections.Generic;

    public class MathOperations
    {
        public int CalculatePercentage(int partNumber, int maxNumber)
        {
            if (partNumber > maxNumber)
            {
                throw new ArgumentException("Invalid arguments");
            }

            return (int)(((double)partNumber / (double)maxNumber) * 100);
        }

        public void Shuffle<T>(IList<T> list, Random random)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}