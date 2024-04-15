using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace EntegraCaseStudy;

public static class BusinessLogic
{
    public static async Task<List<Product>> GetProducts(string token)
    {
        const string endpoint = $"{ConstString.BaseUrl}/product/page=1/";
        var response = await HttpHelper.GetAsync(endpoint, token);
    
        var jsonObject = JObject.Parse(response); 
        var productList = jsonObject["productList"];
        List<Product> products = new();

        foreach (var product in productList.Take(5)) 
        {
            var name = product["name"].ToString();
            var quantity = Convert.ToInt32(product["quantity"].ToString());
            var productCode = product["productCode"].ToString();

            products.Add(new Product
            {
                Name = name,
                Quantity = quantity,
                ProductCode = productCode
            }) ;
        }

        return products;
    }

    public static async Task<string> GetAccessToken(string email, string password)
    {
        const string endpoint = $"{ConstString.BaseUrl}/api/user/token/obtain/";
    
        using StringContent jsonContent = new(
            JsonSerializer.Serialize(new
            {
                email = email,
                password = password
            }),
            Encoding.UTF8,
            "application/json");
    
        var response = await HttpHelper.PostAsync(endpoint, jsonContent);
        dynamic jsonResponse = JsonConvert.DeserializeObject(response);
        return jsonResponse.access;
    }
}
