using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Auditor;
using HttpGrabberFunctions;
using Newtonsoft.Json;
namespace AirnomadAudits.GroupCategory
{
    public static class GroupCategoryAudit
    {

        private static async Task<GroupCategoryObject> GetGroupCategories(string orgunitid)
        {
            GroupCategoryObject NewGroupCategory = null;
            try
            {
                var authToken = Environment.GetEnvironmentVariable("API_TOKEN");
                var Json = await (new CanvasGrabber($"/api/v1/courses/{orgunitid}/group_categories")).GetAuthResponse(authToken);
                NewGroupCategory = JsonConvert.DeserializeObject<List<GroupCategoryObject>>(Json)[0];
                System.Console.WriteLine(NewGroupCategory.Id);
            }
            catch (Exception e)
            {
                throw e;
            }
            return NewGroupCategory;
        }
        public static async Task<StatusCollection> ExecuteAudit(string blueprintid, string coursecopyid)
        {
            StatusCollection AuditResults = new StatusCollection("Group Category Audit for " + coursecopyid);
            try
            {

                GroupCategoryObject CourseBlueprint = await GetGroupCategories(blueprintid);
                GroupCategoryObject CourseCopy = await GetGroupCategories(coursecopyid);
                
            }
            catch (Exception e)
            {
                AuditResults.StatusObjects.Add(new StatusObject(-1, "Error Runnig Audit!", e.Message));
            }
            return AuditResults;
        }
    }
}