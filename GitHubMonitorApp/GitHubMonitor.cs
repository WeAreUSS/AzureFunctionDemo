using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GitHubMonitorApp
{
    public static class GitHubMonitor
    {
        [FunctionName("GitHubMonitor")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            //NOTE: route in trigger above is: [url]/api/GitHubMonitor    The HttpTrigger is passed into the "req" variable
            // In our case, the url= http://localhost:7071
            // http://localhost:7071/api/GitHubMonitor?name=Walt <- browser string
            //NOTE: Security is not enforced in dev environment, to do so is quite complicated
            log.LogInformation("GitHubMonitor function processed a request.");


            // read what was entered into address bar
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            // grab the data from the GetHub response and put it in a RootObject
            
            Rootobject data = JsonConvert.DeserializeObject<Rootobject>(requestBody);

            //ToDo: Do something with RootObject data

            log.LogInformation(requestBody);



            //NOTE: because we have a Task<IActionResult>, we return a new OkObjectResult(respponseMessage)
            return new OkObjectResult("");
        }




        // VERSION 2 - Before Json Classes
        //======================================

        //public static class GitHubMonitor
        //{
        //    [FunctionName("GitHubMonitor")]
        //    public static async Task<IActionResult> Run(
        //        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
        //        ILogger log)
        //    {
        //        //NOTE: route in trigger above is: [url]/api/GitHubMonitor    The HttpTrigger is passed into the "req" variable
        //        // In our case, the url= http://localhost:7071
        //        // http://localhost:7071/api/GitHubMonitor?name=Walt <- browser string
        //        //NOTE: Security is not enforced in dev environment, to do so is quite complicated
        //        log.LogInformation("GitHubMonitor function processed a request.");


        //        // read what was entered into address bar
        //        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

        //        // grab the name variable value if it was specified
        //        dynamic data = JsonConvert.DeserializeObject(requestBody);

        //        //ToDo: Do something with data

        //        log.LogInformation(requestBody);



        //        //NOTE: because we have a Task<IActionResult>, we return a new OkObjectResult(respponseMessage)
        //        return new OkObjectResult("");
        //    }








        // VERSION 1
        //======================================
        //[FunctionName("GitHubMonitor")]
        //public static async Task<IActionResult> Run(
        //    [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
        //    ILogger log)
        //{
        //    //NOTE: route in trigger above is: [url]/api/GitHubMonitor    The HttpTrigger is passed into the "req" variable
        //    // In our case, the url= http://localhost:7071
        //    // http://localhost:7071/api/GitHubMonitor?name=Walt <- browser string
        //    //NOTE: Security is not enforced in dev environment, to do so is quite complicated
        //    log.LogInformation("GitHubMonitor function processed a request.");

        //    // from  req, we pull off the name parameter
        //    string name = req.Query["name"];

        //    // read what was entered into address bar
        //    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

        //    // grab the name variable value if it was specified
        //    dynamic data = JsonConvert.DeserializeObject(requestBody);
        //    // assign the data.name value to variable "name"
        //    name ??= data?.name;

        //    // if name value is null return a response without it, otherwise, put the "name" value in response 
        //    string responseMessage = string.IsNullOrEmpty(name)
        //        ? "This HTTP triggered function: GitHubMonitor, executed successfully. Pass a name in the query string or in the request body for a personalized response."
        //        : $"Hello, {name}. This HTTP triggered function: GitHubMonitor executed successfully.";

        //    //NOTE: because we have a Task<IActionResult>, we return a new OkObjectResult(respponseMessage)
        //    return new OkObjectResult(responseMessage);
        /*
         *       OR
         *       ===
         */
        // if name value is null return a response without it, otherwise, put the "name" value in response 
        /* return name !=null
         * ? (ActionResult) new OkObjectResult($"Hello, {name}. This HTTP triggered function: GitHubMonitor executed successfully.");
         * : new BadRequestObjectResult("This HTTP triggered function: GitHubMonitor, executed successfully. Pass a name in the query string or in the request body for a personalized response.");
         */
        //}




        //========================================================
        // Pasted first post to cloud PasteSpecial.JsonAsClasses
        // Now: we can produce specific content in our response message...
        //========================================================

        public class Rootobject
        {
            public string _ref { get; set; }
            public string before { get; set; }
            public string after { get; set; }
            public Repository repository { get; set; }
            public Pusher pusher { get; set; }
            public Sender sender { get; set; }
            public bool created { get; set; }
            public bool deleted { get; set; }
            public bool forced { get; set; }
            public object base_ref { get; set; }
            public string compare { get; set; }
            public Commit[] commits { get; set; }
            public Head_Commit head_commit { get; set; }
        }

        public class Repository
        {
            public int id { get; set; }
            public string node_id { get; set; }
            public string name { get; set; }
            public string full_name { get; set; }
            public bool _private { get; set; }
            public Owner owner { get; set; }
            public string html_url { get; set; }
            public object description { get; set; }
            public bool fork { get; set; }
            public string url { get; set; }
            public string forks_url { get; set; }
            public string keys_url { get; set; }
            public string collaborators_url { get; set; }
            public string teams_url { get; set; }
            public string hooks_url { get; set; }
            public string issue_events_url { get; set; }
            public string events_url { get; set; }
            public string assignees_url { get; set; }
            public string branches_url { get; set; }
            public string tags_url { get; set; }
            public string blobs_url { get; set; }
            public string git_tags_url { get; set; }
            public string git_refs_url { get; set; }
            public string trees_url { get; set; }
            public string statuses_url { get; set; }
            public string languages_url { get; set; }
            public string stargazers_url { get; set; }
            public string contributors_url { get; set; }
            public string subscribers_url { get; set; }
            public string subscription_url { get; set; }
            public string commits_url { get; set; }
            public string git_commits_url { get; set; }
            public string comments_url { get; set; }
            public string issue_comment_url { get; set; }
            public string contents_url { get; set; }
            public string compare_url { get; set; }
            public string merges_url { get; set; }
            public string archive_url { get; set; }
            public string downloads_url { get; set; }
            public string issues_url { get; set; }
            public string pulls_url { get; set; }
            public string milestones_url { get; set; }
            public string notifications_url { get; set; }
            public string labels_url { get; set; }
            public string releases_url { get; set; }
            public string deployments_url { get; set; }
            public int created_at { get; set; }
            public DateTime updated_at { get; set; }
            public int pushed_at { get; set; }
            public string git_url { get; set; }
            public string ssh_url { get; set; }
            public string clone_url { get; set; }
            public string svn_url { get; set; }
            public object homepage { get; set; }
            public int size { get; set; }
            public int stargazers_count { get; set; }
            public int watchers_count { get; set; }
            public string language { get; set; }
            public bool has_issues { get; set; }
            public bool has_projects { get; set; }
            public bool has_downloads { get; set; }
            public bool has_wiki { get; set; }
            public bool has_pages { get; set; }
            public int forks_count { get; set; }
            public object mirror_url { get; set; }
            public bool archived { get; set; }
            public bool disabled { get; set; }
            public int open_issues_count { get; set; }
            public object license { get; set; }
            public bool allow_forking { get; set; }
            public bool is_template { get; set; }
            public object[] topics { get; set; }
            public string visibility { get; set; }
            public int forks { get; set; }
            public int open_issues { get; set; }
            public int watchers { get; set; }
            public string default_branch { get; set; }
            public int stargazers { get; set; }
            public string master_branch { get; set; }
        }

        public class Owner
        {
            public string name { get; set; }
            public string email { get; set; }
            public string login { get; set; }
            public int id { get; set; }
            public string node_id { get; set; }
            public string avatar_url { get; set; }
            public string gravatar_id { get; set; }
            public string url { get; set; }
            public string html_url { get; set; }
            public string followers_url { get; set; }
            public string following_url { get; set; }
            public string gists_url { get; set; }
            public string starred_url { get; set; }
            public string subscriptions_url { get; set; }
            public string organizations_url { get; set; }
            public string repos_url { get; set; }
            public string events_url { get; set; }
            public string received_events_url { get; set; }
            public string type { get; set; }
            public bool site_admin { get; set; }
        }

        public class Pusher
        {
            public string name { get; set; }
            public string email { get; set; }
        }

        public class Sender
        {
            public string login { get; set; }
            public int id { get; set; }
            public string node_id { get; set; }
            public string avatar_url { get; set; }
            public string gravatar_id { get; set; }
            public string url { get; set; }
            public string html_url { get; set; }
            public string followers_url { get; set; }
            public string following_url { get; set; }
            public string gists_url { get; set; }
            public string starred_url { get; set; }
            public string subscriptions_url { get; set; }
            public string organizations_url { get; set; }
            public string repos_url { get; set; }
            public string events_url { get; set; }
            public string received_events_url { get; set; }
            public string type { get; set; }
            public bool site_admin { get; set; }
        }

        public class Head_Commit
        {
            public string id { get; set; }
            public string tree_id { get; set; }
            public bool distinct { get; set; }
            public string message { get; set; }
            public DateTime timestamp { get; set; }
            public string url { get; set; }
            public Author author { get; set; }
            public Committer committer { get; set; }
            public string[] added { get; set; }
            public object[] removed { get; set; }
            public object[] modified { get; set; }
        }

        public class Author
        {
            public string name { get; set; }
            public string email { get; set; }
            public string username { get; set; }
        }

        public class Committer
        {
            public string name { get; set; }
            public string email { get; set; }
            public string username { get; set; }
        }

        public class Commit
        {
            public string id { get; set; }
            public string tree_id { get; set; }
            public bool distinct { get; set; }
            public string message { get; set; }
            public DateTime timestamp { get; set; }
            public string url { get; set; }
            public Author1 author { get; set; }
            public Committer1 committer { get; set; }
            public string[] added { get; set; }
            public object[] removed { get; set; }
            public object[] modified { get; set; }
        }

        public class Author1
        {
            public string name { get; set; }
            public string email { get; set; }
            public string username { get; set; }
        }

        public class Committer1
        {
            public string name { get; set; }
            public string email { get; set; }
            public string username { get; set; }
        }

    }
}
