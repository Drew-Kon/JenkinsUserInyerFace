using XnaFan.ImageComparison;

namespace VKAPITask.Utility
{
    internal class CompareImages
    {
        public static float GetPercentageDifference(string image1Path, string image2Path)
        {
            return ImageTool.GetPercentageDifference(image1Path, image2Path);
        }
    }
}