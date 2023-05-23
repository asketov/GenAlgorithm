

namespace GenAlgorithm
{
    using System.Numerics;

    public struct Hromosome20Bit
       
    {
        private int _value;

        public Hromosome20Bit()
        {
            _value = 0;
        }

        public Hromosome20Bit(int value)
        {
            if (value is > 0xFFFFF or < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            _value = value;
            toCodeGray();
        }

        public void SetBit(int bitPosition, bool one)
        {
            if (bitPosition is >= 20 or < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bitPosition));
            }

            var mask = one ? 1 << bitPosition : ~(1 << bitPosition);
            _value = one ? _value | mask : _value & mask;
        }

        public void toCodeGray()
        {
            _value = _value ^ (_value >> 1);
        }

        public Hromosome20Bit GetFirstSon(Hromosome20Bit obj, int fromBit)
        {
            for(int i=20;fromBit < i; fromBit++)
            {
                obj.SetBit(fromBit, GetBit(fromBit));
            }
            return obj;
        }
        public Hromosome20Bit GetSecondSon(Hromosome20Bit obj, int fromBit)
        {
            for (int i = 0; fromBit > i; fromBit--)
            {
                obj.SetBit(fromBit, GetBit(fromBit));
            }
            return obj;
        }

        public void mutation()
        {
            _value = ~_value;
        }

        public bool GetBit(int bitPosition)
        {
            if (bitPosition is >= 20 or < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bitPosition));
            }

            var mask = 1 << bitPosition;
            return (_value & mask) != 0;
        }

        public int GetInt()
        {
            return _value;
        }
    }
}
