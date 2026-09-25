namespace SmartSchool.Application.Common
{
    public class JwtSettings
    {
        public string Issuer { get; set; } = "SmartSchoolIssuer";
        public string Audience { get; set; } = "SmartSchoolAudience";
        public string Key { get; set; } = "QKjUoV9JEd+MA/HnfrA0awP12JGgbMDiyG+AHvBPh3o=";
        public int AccessTokenMinutes { get; set; } = 60;
        public int RefreshTokenDays { get; set; } = 7;
    }
}
