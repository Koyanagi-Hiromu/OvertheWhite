namespace PPD
{
    public static class EX_String
    {
        /// <summary>
        /// null || "";
        /// 
        /// ! HasValue()
        /// </summary>
        public static bool IsNullOrEmpty(this string str)
        {
            return str == null || str.Length == 0;
        }
    }
}
