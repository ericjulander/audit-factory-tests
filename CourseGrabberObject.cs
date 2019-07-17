namespace AirnomadAudits.GroupCategory
{
    // Generated by https://quicktype.io

    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class GroupCategoryObject
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("role")]
        public object Role { get; set; }

        [JsonProperty("self_signup")]
        public object SelfSignup { get; set; }

        [JsonProperty("group_limit")]
        public object GroupLimit { get; set; }

        [JsonProperty("auto_leader")]
        public object AutoLeader { get; set; }

        [JsonProperty("context_type")]
        public string ContextType { get; set; }

        [JsonProperty("course_id")]
        public long CourseId { get; set; }

        [JsonProperty("sis_group_category_id")]
        public object SisGroupCategoryId { get; set; }

        [JsonProperty("protected")]
        public bool Protected { get; set; }

        [JsonProperty("allows_multiple_memberships")]
        public bool AllowsMultipleMemberships { get; set; }

        [JsonProperty("is_member")]
        public bool IsMember { get; set; }
    }

    public partial class GroupCategory
    {
        public static GroupCategory FromJson(string json) => JsonConvert.DeserializeObject<GroupCategory>(json,  AirnomadAudits.GroupCategory.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this GroupCategory self) => JsonConvert.SerializeObject(self,  AirnomadAudits.GroupCategory.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
