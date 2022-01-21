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

            // grab the name variable value if it was specified
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            //ToDo: Do something with data
           
            log.LogInformation(requestBody);

          

            //NOTE: because we have a Task<IActionResult>, we return a new OkObjectResult(respponseMessage)
            return new  OkObjectResult("");
        }

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
    }
}
