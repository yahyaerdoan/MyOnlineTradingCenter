using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace MyOnlineTradingCenter.RestfulApplicationInterfaceLayer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MathsController : ControllerBase
{
    [HttpGet("find-pairs")]
    public IActionResult FindPairs(int target)
    {
        int[] arr = { 1, 7, 5, 12, 14, 21, 3, 22, 4 };
        var result = TargetElements(arr, target);

        // Convert result to JSON-friendly format, Log to console
        var formattedResult = result.ConvertAll(pair => $"({pair.Item1}, {pair.Item2})");
        var jsonString = JsonSerializer.Serialize(formattedResult);
        Console.WriteLine(jsonString);

        var flatResult = result.SelectMany(pair => new[] { pair.Item1, pair.Item2 }).ToList();
        return Ok(flatResult);
    }

    private List<(int, int)> TargetElements(int[] arr, int target)
    {
        Dictionary<int, int> indexMap = new Dictionary<int, int>(); // Store number and its index
        List<(int, int)> pairs = new List<(int, int)>(); // Store the result pairs

        for (int i = 0; i < arr.Length; i++)
        {
            int complement = target - arr[i];

            if (indexMap.ContainsKey(complement))
            {
                // Append 1-based indices
                pairs.Add((indexMap[complement] + 1, i + 1));
            }

            // Store index of the current number
            if (!indexMap.ContainsKey(arr[i]))
            {
                indexMap[arr[i]] = i;
            }
        }
        return pairs;
    }
}
