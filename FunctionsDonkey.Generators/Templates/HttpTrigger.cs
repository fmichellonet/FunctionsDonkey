namespace {{Namespace}}
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Azure.WebJobs.Host;
    using Newtonsoft.Json;
	using FunctionsDonkey;

    public static class {{Name}}
    {

        [FunctionName("{{Name}}")]
		public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "{{Name}}")]
            HttpRequest request, TraceWriter log, ExecutionContext context)
		{
			var requestBody = new StreamReader(request.Body).ReadToEnd();
			var command = JsonConvert.DeserializeObject<{{CommandTypeName}}>(requestBody);

			if (command == null)
			{
				throw new ArgumentNullException(nameof(command));
			}

			var response = await Functions.Runtime().HandleCommand(command);

			return new OkObjectResult(response);
		}
	}
}