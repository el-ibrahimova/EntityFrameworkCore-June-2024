﻿namespace Trucks.Common
{
    public static class ValidationConstants
    {
        // Truck
        public const string TruckRegistrationNumberRegEx = @"^[A-Z]{2}\d{4}[A-Z]{2}$";
        public const int TruckVinNumberMinLength = 17;
        public const int TruckVinNumberMaxLength = 17;
        public const int TruckTankCapacityMinValue = 950;
        public const int TruckTankCapacityMaxValue = 1420;
        public const int TruckCargoCapacityMinValue = 5000;
        public const int TruckCargoCapacityMaxValue = 29000;
        public const int TruckCategoryTypeMinValue = 0;
        public const int TruckCategoryTypeMaxValue = 3;
        public const int TruckMakeTypeMinValue = 0;
        public const int TruckMakeTypeMaxValue = 4;

        // Client
        public const int ClientNameMinLength = 3;
        public const int ClientNameMaxLength = 40;
        public const int ClientNationalityMinValue = 2;
        public const int ClientNationalityMaxValue = 40;
        public const int ClientTypeMinLength = 0;
        public const int ClientTypeMaxLength = int.MaxValue;

        // Despatcher

        public const int DespatcherNameMinLength = 2;
        public const int DespatcherNameMaxLength = 40;
        public const int DespatcherPositionMinLength = 0;
        public const int DespatcherPositionMaxLength = int.MaxValue;
    }
}
