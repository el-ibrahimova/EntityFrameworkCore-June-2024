namespace Cadastre.Common
{
    public static class ValidationConstants
    {
        //District
        public const int DistrictNameMinLength = 2;
        public const int DistrictNameMaxLength = 80;
        public const int DistrictPostalCodeMaxLength = 8;
        public const int DistrictPostalCodeMinLength = 8;


        //Property
        public const int PropertyIdentifierMinLength = 16;
        public const int PropertyIdentifierMaxLength = 20;
        public const int PropertyDetailsMinLength = 5;
        public const int PropertyDetailsMaxLength = 500;
        public const int PropertyAddressMinLength = 5;
        public const int PropertyAddressMaxLength = 200;
        public const int PropertyAreaMinValue = 0;
        public const int PropertyAreaMaxValue = int.MaxValue;

        //Citizen
        public const int CitizenNameMinLength = 2;
        public const int CitizenNameMaxLength = 30;
    }
}
