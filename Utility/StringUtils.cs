namespace VKAPITask.Utility
{
    public static class StringUtils
    {
        /// <summary>
        /// Генерация случайной строки установленного размера
        /// </summary>
        /// <param name="length">Длина случайной строки</param>
        /// <param name="random">Просто передай метод Random</param>
        /// <returns>Randomly generated string</returns>
        public static string GenerateString(int length, Random random)
        {
            string randomString = "abcdefghijklmnopqrstuvwxyz";
            string returnString = "";
            for (int i = 0; i < length; i++)
            {
                returnString = returnString + randomString[random.Next(0, randomString.Length)];
            }
            return returnString;
        }
    }
}