using System;

namespace Code.Logic.Tools
{
    public struct SubString
    {
        public string Str{ get; private set; }
        public int StartIndex{ get; private set; }
        public int Length { get; private set; }
        
        private string _finalStr;
        #if UNITY_EDITOR
        public string DebugStr => Str.Substring(StartIndex, Length);
        #endif
        public SubString(string str,int startIndex, int length)
        {
            StartIndex = startIndex;
            Length = length;
            Str = str;
            _finalStr = null;
        }
        
        public char this[int index]
        {
            get
            {
                if (index < 0 || index >= Length)
                {
                    throw new IndexOutOfRangeException();
                }
                return Str[StartIndex + index];
            }
        }

        public char SafeGet(int index)
        {
            if (index < 0 || index >= Length)
            {
                return ' ';
            }
            return Str[StartIndex + index];
        }
        public SubString Sub(int startIndex, int length)
        {
            if (startIndex < 0 || startIndex >= Length)
            {
                throw new IndexOutOfRangeException();
            }
            if (startIndex + length > Length)
            {
                throw new IndexOutOfRangeException();
            }
            return new SubString(Str, StartIndex + startIndex, length);
        }
        
        public bool Equals(SubString other)
        {
            if (Length != other.Length)
            {
                return false;
            }
            for (int i = 0; i < Length; i++)
            {
                if (this[i] != other[i])
                {
                    return false;
                }
            }
            return true;
        }
        public override string ToString()
        {
            if (_finalStr != null)
            {
                return _finalStr;
            }
            _finalStr = Str.Substring(StartIndex, Length);
            return _finalStr;
        }
    }
}