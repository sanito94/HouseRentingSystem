using HouseRentingSystem.Core.Contracts.House;
using System.Text.RegularExpressions;

namespace HouseRentingSystem.Core.Exceptions
{
    public static class ModelExtensions
    {
        public static string GetInformation(this IHouseModel model)
        {
            string info = model.Title.Replace(" ", "-") + GetAddress(model.Address);

            info = Regex.Replace(info, @"[^a-zA-Z0-9\-]", string.Empty);
            return info;
        }

        private static string GetAddress(string position)
        {
            position = string.Join("-", position.Split(' ').Take(3));

            return position;
        }
    }
}
