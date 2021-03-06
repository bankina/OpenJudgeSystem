namespace OJS.Web.Areas.Contests
{
    using System.Web.Mvc;

    using OJS.Web.Areas.Contests.Controllers;

    public class ContestsAreaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Contests";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Contests_list",
                "Contests",
                new { controller = "List", action = "Index" },
                new[] { "OJS.Web.Areas.Contests.Controllers" });

            context.MapRoute(
                "Contests_by_submission_type",
                "Contests/BySubmissionType/{submissionType}",
                new { controller = "List", action = "BySubmissionType", submissionType = UrlParameter.Optional });

            context.MapRoute(
                "Contests_details",
                "Contests/{id}/{name}",
                new { controller = "Contests", action = "Details", id = UrlParameter.Optional, name = UrlParameter.Optional },
                new { id = "[0-9]+" },
                new[] { "OJS.Web.Areas.Contests.Controllers" });

            context.MapRoute(
               "Contests_results_compete",
               string.Format("Contests/{0}/Results/{{action}}/{{id}}", CompeteController.CompeteUrl),
               new { controller = "Results", action = "Simple", official = true, id = UrlParameter.Optional });

            context.MapRoute(
               "Contests_results_practice",
               string.Format("Contests/{0}/Results/{{action}}/{{id}}", CompeteController.PracticeUrl),
               new { controller = "Results", action = "Simple", official = false, id = UrlParameter.Optional });

            context.MapRoute(
               "Contests_compete",
               string.Format("Contests/{0}/{{action}}/{{id}}", CompeteController.CompeteUrl),
               new { controller = "Compete", action = "Index", official = true, id = UrlParameter.Optional });

            context.MapRoute(
               "Contests_practice",
               string.Format("Contests/{0}/{{action}}/{{id}}", CompeteController.PracticeUrl),
               new { controller = "Compete", action = "Index", official = false, id = UrlParameter.Optional });

            context.MapRoute(
               "Contests_default",
               "Contests/{controller}/{action}/{id}",
               new { id = UrlParameter.Optional });
        }
    }
}
