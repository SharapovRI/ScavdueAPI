namespace Scavdue.OpenStreetMaps.Adapter.Constants
{
    public static class TagDictionaries
    {
        public static Dictionary<string, string> ResidentialBuildingsTags = new Dictionary<string, string>()
        {
            { "apartments", "building" },
            { "barracks", "building" },
            { "bungalow", "building" },
            { "detached", "building" },
            { "dormitory", "building" },
            { "house", "building" },
            { "residential", "building" },
            { "semidetached_house", "building" },
        };

        public static Dictionary<string, string> ReligiousBuildingsTags = new Dictionary<string, string>()
        {
            { "cathedral", "building" },
            { "chapel", "building" },
            { "church", "building" },
            { "monastery", "building" },
            { "mosque", "building" },
            { "synagogue", "building" },
            { "temple", "building" },
            { "religious", "building" },
        };

        public static Dictionary<string, string> HealthBuildingsTags = new Dictionary<string, string>()
        {
            { "clinic", "amenity" },
            { "dentist", "amenity" },
            { "doctors", "amenity" },
            { "hospital", "amenity" },
            { "pharmacy", "amenity" },
            { "veterinary", "amenity" },
        };

        public static Dictionary<string, string> FinancialBuildingsTags = new Dictionary<string, string>()
        {
            { "atm", "amenity" },
            { "bank", "amenity" },
            { "bureau_de_change", "amenity" },
        };

        public static Dictionary<string, string> EmergencyServicesTags = new Dictionary<string, string>()
        {
            { "ambulance_station", "emergency" },
            { "fire_station", "amenity" },
            { "police", "amenity" },
        };

        public static Dictionary<string, string> CommercialBuildingsTags = new Dictionary<string, string>()
        {
            { "commercial", "building" },
            { "industrial", "building" },
            { "kiosk", "building" },
            { "office", "building" },
            { "supermarket", "building" },
            { "warehouse", "building" },
        };

        public static Dictionary<string, string> SportsBuildingsTags = new Dictionary<string, string>()
        {
            { "grandstand", "building" },
            { "pavilion", "building" },
            { "riding_hall", "building" },
            { "sports_hall", "building" },
            { "stadium", "building" },
        };

        public static Dictionary<string, string> EducationBuildingsTags = new Dictionary<string, string>()
        {
            { "college", "amenity" },
            { "driving_school", "amenity" },
            { "kindergarten", "amenity" },
            { "language_school", "amenity" },
            { "library", "amenity" },
            { "toy_library", "amenity" },
            { "music_school", "amenity" },
            { "school", "amenity" },
            { "university", "amenity" },
        };

        public static Dictionary<string, string> EntertainmentFacilitiesTags = new Dictionary<string, string>()
        {
            { "arts_centre", "amenity" },
            { "brothel", "amenity" },
            { "casino", "amenity" },
            { "cinema", "amenity" },
            { "community_centre", "amenity" },
            { "nightclub", "amenity" },
            { "planetarium", "amenity" },
            { "theatre", "amenity" },
            { "stripclub", "amenity" },
        };

        public static Dictionary<string, Dictionary<string, string>> BuildingClasses = new Dictionary<string, Dictionary<string, string>>()
        {
            ["Residential"] = ResidentialBuildingsTags,
            ["Religious"] = ReligiousBuildingsTags,
            ["Health"] = HealthBuildingsTags,
            ["Financial"] = FinancialBuildingsTags,
            ["EmergencyServices"] = EmergencyServicesTags,
            ["Commercial"] = CommercialBuildingsTags,
            ["Sports"] = SportsBuildingsTags,
            ["Education"] = EducationBuildingsTags,
            ["Entertainment"] = EntertainmentFacilitiesTags,
        };
    }
}
