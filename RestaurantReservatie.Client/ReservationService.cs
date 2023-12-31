using System.Net.Http.Headers;
using System.Net.Http.Json;
using Newtonsoft.Json;
using RestaurantReservatie.Rest.Models.Input;
using RestaurantReservatie.Rest.Models.Output;
using CustomerOutputDTO = TestEFLayer.Output.CustomerOutputDTO;
using RestaurantOutputDTO = TestEFLayer.Output.RestaurantOutputDTO;

namespace RestaurantReservatie.Client;

public class ReservationService
{
    private HttpClient client;

    public ReservationService()
    {
        client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:5187/");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<List<RestaurantOutputDTO>> GetRestaurantByCuisineAsync(string cuisine)
    {
        List<RestaurantOutputDTO> restaurants = null;
        HttpResponseMessage response = await client.GetAsync($"api/Restaurant/GetRestaurantsByCuisine/{cuisine}");
        if (response.IsSuccessStatusCode)
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            restaurants = JsonConvert.DeserializeObject<List<RestaurantOutputDTO>>(responseBody);
            return restaurants;
        }

        return null;
    }
    public async Task<int> GetRestaurantByNameAsync(string name)
    {
        RestaurantOutputDTO restaurant = null;
        HttpResponseMessage response = await client.GetAsync($"api/Restaurant/GetRestaurantByName/{name}");
        if (response.IsSuccessStatusCode)
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            restaurant = JsonConvert.DeserializeObject<RestaurantOutputDTO>(responseBody);
            return restaurant.Id;
        }

        return -1;
    }
    
    
    public async Task<CustomerOutputDTO> AddUserAsync(CustomerInputDTO user)
    {
        CustomerOutputDTO customerOutputDTO = null;
        HttpResponseMessage response = await client.PostAsJsonAsync("api/Customer/AddCustomer", user);
        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();

            customerOutputDTO = JsonConvert.DeserializeObject<CustomerOutputDTO>(json);
            return customerOutputDTO;
        }
        return null;
    }
    
    public async Task<ReservationOutputDTO> AddReservationAsync(ReservationInputDTO reservation)
    {
        ReservationOutputDTO reservationOutputDTO = null;
        HttpResponseMessage response = await client.PostAsJsonAsync("api/Reservation/AddReservation", reservation);
        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();

            reservationOutputDTO = JsonConvert.DeserializeObject<ReservationOutputDTO>(json);
            return reservationOutputDTO;
        }
        return null;
    }
}