using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantReservatie.BL.Interfaces;
using RestaurantReservatie.BL.Managers;
using RestaurantReservatie.BL.Models;
using Moq;
using RestaurantReservatie.Rest.Controllers;
using RestaurantReservatie.Rest.Models.Input;
using RestaurantReservatie.Rest.Models.Output;

namespace RestaurantReservatie.Xunit;

public class RestaurantUnitTest {
    private RestaurantController _restaurantController;
    private RestaurantManager _restaurantManager;
    private Mock<IRestaurantRepository> _restaurantRepository;
    private Mock<IReservationRepository> _reservationRepository;
    private ILoggerFactory logger = new LoggerFactory();

    public RestaurantUnitTest() {
        _restaurantRepository = new Mock<IRestaurantRepository>();
        _reservationRepository = new Mock<IReservationRepository>();
        _restaurantManager = new RestaurantManager(_restaurantRepository.Object, _reservationRepository.Object);
        _restaurantController = new RestaurantController(_restaurantManager, logger);
    }

    [Fact]
    public void GetRestaurantsOpId_Valid() {
        _restaurantRepository.Setup(r => r.RestaurantExists(5)).Returns(true);
        Restaurant r = new Restaurant("Pitta Stonks", new Location("9900", "Eeklo", "weudelaan", "10"), "Turks",
            "0487686465", "pittastonks@info.be");
        RestaurantOutputDTO resto = new RestaurantOutputDTO(5, "Pitta Stonks",
            new Location("9900", "Eeklo", "weudelaan", "10"), "Turks", "0487686465", "pittastonks@info.be",
            new List<Table>());
        _restaurantRepository.Setup(r => r.GetRestaurants(5)).Returns(r);
        var result = _restaurantController.GetRestaurant(5);
        Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsType<RestaurantOutputDTO>(((OkObjectResult)result.Result).Value);
    }

    [Fact]
    public void GetRestaurantsOpId_Invalid() {
        _restaurantRepository.Setup(r => r.RestaurantExists(101)).Returns(false);
        var result = _restaurantController.GetRestaurant(101);
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public void AddRestaurant_Valid() {
        RestaurantInputDTO restaurant = new RestaurantInputDTO
        { Name = "Pitta Stonks",
          Email = "pittastonks@info.be",
          City = "Eeklo",
          Street = "weudelaan",
          HouseNumber = "10",
          Cuisine = "Turks",
          PostalCode = "9900",
          PhoneNumber = "0487686465" };
        _restaurantRepository.Setup(r => r.AddRestaurant(It.IsAny<Restaurant>())).Returns((Restaurant r) => r);
        var result = _restaurantController.AddRestaurant(restaurant);
        Assert.IsType<CreatedAtActionResult>(result.Result);
    }

    [Fact]
    public void AddRestaurant_Invalid() {
        RestaurantInputDTO restaurant = new RestaurantInputDTO
        { Name = "Pitta Stonks",
          Email = "pittastonks@info.be",
          City = "Eeklo",
          Street = "weudelaan",
          HouseNumber = "10",
          Cuisine = "Turks",
          PostalCode = "9900",
          PhoneNumber = "0487686465" };
        _restaurantRepository.Setup(r => r.AddRestaurant(It.IsAny<Restaurant>())).Returns((Restaurant r) => null);
        var result = _restaurantController.AddRestaurant(restaurant);
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public void UpdateRestaurant_Valid() {
        RestaurantInputDTO restaurant = new RestaurantInputDTO
        { Name = "Pitta Stonks",
          Email = "pittastonks@info.be",
          City = "Eeklo",
          Street = "weudelaan",
          HouseNumber = "10",
          Cuisine = "Turks",
          PostalCode = "9900",
          PhoneNumber = "0487686465" };
        _restaurantRepository.Setup(r => r.RestaurantExists(5)).Returns(true);
        _restaurantRepository.Setup(r => r.UpdateRestaurant(It.IsAny<Restaurant>())).Returns((Restaurant r) => r);
        var result = _restaurantController.UpdateRestaurant(5, restaurant);
        Assert.IsType<CreatedAtActionResult>(result);
    }

    [Fact]
    public void UpdateRestaurant_Invalid() {
        RestaurantInputDTO restaurant = new RestaurantInputDTO
        { Name = "Pitta Stonks",
          Email = "pittastonks@info.be",
          City = "Eeklo",
          Street = "weudelaan",
          HouseNumber = "10",
          Cuisine = "Turks",
          PostalCode = "9900",
          PhoneNumber = "0487686465" };
        _restaurantRepository.Setup(r => r.RestaurantExists(5)).Returns(true);
        var result = _restaurantController.UpdateRestaurant(5, restaurant);
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void VerwijderRestaurant_Valid() {
        _restaurantRepository.Setup(r => r.RestaurantExists(5)).Returns(true);
        _reservationRepository.Setup(r => r.GetRestaurantReservations(5)).Returns(new List<Reservation>());
        var result = _restaurantController.DeleteRestaurant(5);
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void VerwijderRestaurant_Invalid() {
        _restaurantRepository.Setup(r => r.RestaurantExists(5)).Returns(false);
        var result = _restaurantController.DeleteRestaurant(5);
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public void GeefReservatiesOpDatum_Valid() {
        _restaurantRepository.Setup(r => r.RestaurantExists(5)).Returns(true);
        _restaurantRepository.Setup(r => r.GetReservationsByDate(5, new DateTime(2020, 12, 12)))
            .Returns(new List<Reservation>());
        var result = _restaurantController.GetReservationsByDate(5, new DateTime(2020, 12, 12));
        Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsType<List<ReservationOutputDTO>>(((OkObjectResult)result.Result).Value);
    }

    [Fact]
    public void GeefReservatiesOpDatum_Invalid() {
        _restaurantRepository.Setup(r => r.RestaurantExists(5)).Returns(false);
        var result = _restaurantController.GetReservationsByDate(5, new DateTime(2020, 12, 12));
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public void VoegTafelToe_Valid() {
        TableInputDTO table = new TableInputDTO
        { NumberOfSeats = 4,
          TableNumber = 1 };

        _restaurantRepository.Setup(r => r.RestaurantExists(5)).Returns(true);
        _restaurantRepository.Setup(r => r.AddTable(It.IsAny<Table>())).Returns((Table t) => t);
        var result = _restaurantController.AddTable(5, table);
        Assert.IsType<CreatedAtActionResult>(result.Result);
    }

    [Fact]
    public void VoegTafelToe_Invalid() {
        TableInputDTO table = new TableInputDTO
        { NumberOfSeats = 4,
          TableNumber = 1 };
        _restaurantRepository.Setup(r => r.RestaurantExists(5)).Returns(false);
        var result = _restaurantController.AddTable(5, table);
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public void UpdateTafel_Valid() {
        TableInputDTO table = new TableInputDTO
        { NumberOfSeats = 4,
          TableNumber = 1 };
        Table t = new Table(4, 1, 5);
        _restaurantRepository.Setup(r => r.RestaurantExists(5)).Returns(true);
        _restaurantRepository.Setup(r => r.TableExists(It.IsAny<Table>())).Returns(true);
        _restaurantRepository.Setup(r => r.UpdateTable(5, It.IsAny<Table>())).Returns((int id, Table t) => t);
        var result = _restaurantController.UpdateTable(1, 5, table);
        Assert.IsType<CreatedAtActionResult>(result);
    }

    [Fact]
    public void UpdateTafel_Invalid() {
        TableInputDTO table = new TableInputDTO
        { NumberOfSeats = 4,
          TableNumber = 1 };
        _restaurantRepository.Setup(r => r.RestaurantExists(5)).Returns(true);
        var result = _restaurantController.UpdateTable(1, 5, table);
        Assert.IsType<NotFoundObjectResult>(result);
    }
}