using System;
using System.Collections.Generic;
using System.IO;

namespace FileRandomLib
{
    public class FileFiller
    {
        private readonly string _fileName = "default.txt";
        private readonly int _numberAmount = 100;
        private readonly Random _rng;

        public FileFiller()
        {
            _rng = new Random((int) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
        }

        public FileFiller(string path) : this()
        {
            _fileName = path;
        }

        /// <summary>
        ///     Creates or opens file and fills it with random numbers
        /// </summary>
        /// <param name="lowerLimit">Lower limit for random number generator</param>
        /// <param name="higherLimit">Higher limit for random number generator</param>
        public void Fill(int lowerLimit, int higherLimit)
        {
            if (higherLimit < lowerLimit)
                throw new ArgumentOutOfRangeException(nameof(lowerLimit) + " " + nameof(higherLimit));

            using var fs = new StreamWriter(File.Open(_fileName, FileMode.Append));

            foreach (var number in CreateNumberSequence(lowerLimit, higherLimit))
            {
                fs.Write(number);
                fs.Write("\n");
            }
        }

        /// <summary>
        ///     Appends random unique number at the end of file
        /// </summary>
        public void Append()
        {
            var rn = _rng.Next() % _numberAmount;

            while (FindInFile(rn)) rn = _rng.Next() % _numberAmount;

            using var fs = new StreamWriter(File.Open(_fileName, FileMode.Append));

            fs.Write(rn);
            fs.Write("\n");
        }

        /// <summary>
        ///     Appends predefined number at the end of file if it isn't unique returns false
        /// </summary>
        public bool Append(int number)
        {
            if (FindInFile(number))
                return false;

            using var fs = new StreamWriter(File.Open(_fileName, FileMode.Append));

            fs.Write(number);
            fs.Write("\n");
            return true;
        }

        /// <summary>
        ///     Finds number in file
        /// </summary>
        /// <param name="number">Number to find</param>
        public bool Find(int number)
        {
            return FindInFile(number);
        }

        /// <summary>
        ///     Get all numbers from file
        /// </summary>
        public int[] ReadAllFile()
        {
            return ReadSequence();
        }

        private IEnumerable<int> CreateNumberSequence(int lowerLimit, int higherLimit)
        {
            var numberList = new List<int>();

            var totalNumbers = _numberAmount <= Math.Abs(higherLimit - lowerLimit)
                ? _numberAmount
                : Math.Abs(higherLimit - lowerLimit);

            var rn = _rng.Next(lowerLimit, higherLimit);

            while (numberList.Count < totalNumbers)
            {
                if (numberList.Contains(rn))
                {
                    rn = _rng.Next(lowerLimit, higherLimit);
                    continue;
                }

                numberList.Add(rn);
                rn = _rng.Next(lowerLimit, higherLimit);
            }

            return numberList.ToArray();
        }

        private int[] ReadSequence()
        {
            if (!File.Exists(_fileName))
                throw new FileNotFoundException(_fileName);

            using var fs = new StreamReader(File.Open(_fileName, FileMode.OpenOrCreate));

            var numberList = new List<int>();

            string line;

            while ((line = fs.ReadLine()) != null) numberList.Add(Convert.ToInt32(line));

            return numberList.ToArray();
        }

        private bool FindInFile(int numberToFind)
        {
            if (!File.Exists(_fileName))
                throw new FileNotFoundException(_fileName);

            using var fs = new StreamReader(File.Open(_fileName, FileMode.OpenOrCreate));

            string line;

            while ((line = fs.ReadLine()) != null)
                if (numberToFind == Convert.ToInt32(line))
                    return true;

            return false;
        }
    }
}