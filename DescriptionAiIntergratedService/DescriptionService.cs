namespace Machine_Product_Service.DescriptionAiIntergratedService;
using Machine_Product_Service.AiResponse;
using Machine_Product_Service.DTOS;
using Machine_Product_Service.AiResponse;
public interface IAiDescriptionService
{
    Task<string> GenerateDescriptionAsync(CreateMachineDto machineDetails);
}

public class AiDescriptionService : IAiDescriptionService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public AiDescriptionService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    public async Task<string> GenerateDescriptionAsync(CreateMachineDto machineDetails)
    {
        
        // Формируем промпт на основе DTO
        var prompt = $"Составь описание для автомобиля {machineDetails.Brand} {machineDetails.Model} c  {machineDetails.Description} такими характеристиками. Напиши привлекательное описание для объявления.";
      
        var request = new
        {
            prompt = prompt,
            max_tokens = 150
        };

        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_configuration["AiApiKey"]}");
        
        var response = await client.PostAsJsonAsync(_configuration["AiApiUrl"], request);
        response.EnsureSuccessStatusCode();
        var GeneratedText = await response.Content.ReadAsStringAsync();
        var result = await response.Content.ReadFromJsonAsync<AiResponse>();
        return result?.GeneratedText ?? string.Empty;
        
    }
}




