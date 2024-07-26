using Artillery.Data.Models.Enums;

namespace Artillery.Data
{
    public static class ValidationConstants
    {
        // Country 
        public const int CountryNameMinLength = 4;
        public const int CountryNameMaxLength = 60;
        public const int CountryArmySizeMinValue = 50_000;
        public const int CountryArmySizeMaxValue = 10_000_000;

        // Shell
        public const string ShellWeightMinValue = "2";
        public const string ShellWeightMaxValue = "1680";
        public const int ShellCaliberMinLength = 4;
        public const int ShellCaliberMaxLength = 30;

        // Gun
        public const int GunWeightMinValue = 100;
        public const int GunWeightMaxValue = 1_350_000;
        public const string GunBarrelMinValue = "2.00";
        public const string GunBarrelMaxValue = "35.00";
        public const int GunRangeMinValue = 1;
        public const int GunRangeMaxValue = 100_000;
       public const int GunTypeMinValue = (int)GunType.Howitzer;
        public const int GunTypeMaxValue = (int)GunType.AntiTankGun;

        // Manufacturer
        public const int ManufacturerNameMinLength = 4;
        public const int ManufacturerNameMaxLength = 40;
        public const int ManufacturerFoundedMinLength = 10;
        public const int ManufacturerFoundedMaxLength = 100;

    }
}
