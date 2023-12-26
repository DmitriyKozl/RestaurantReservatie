using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantReservatie.BL.Interfaces;
using RestaurantReservatie.BL.Managers;
using RestaurantReservatie.BL.Models;
using Moq;
using RestaurantReservatie.Rest.Controllers;
using RestaurantReservatie.Rest.Models.Input;


namespace RestaurantReservatie.Xunit;

public class CustomerUnitTest {
    private CustomerController _customerController;
    private RestaurantManager _restaurantManager;
    private ReservationManager _reservationManager;
    private CustomerManager _customerManager;
    private Mock<IRestaurantRepository> _restaurantRepository;
    private Mock<IReservationRepository> _reservationRepository;
    private Mock<ICustomerRepository> _customerRepository;
    private ILoggerFactory _logger = new LoggerFactory();

    public CustomerUnitTest() {
        _restaurantRepository = new Mock<IRestaurantRepository>();
        _reservationRepository = new Mock<IReservationRepository>();
        _customerRepository = new Mock<ICustomerRepository>();
        _restaurantManager = new RestaurantManager(_restaurantRepository.Object, _reservationRepository.Object);
        _reservationManager = new ReservationManager(_reservationRepository.Object);
        _customerManager = new CustomerManager(_customerRepository.Object);
        _customerController =
            new CustomerController(_restaurantManager, _customerManager, _reservationManager, _logger);
    }

    [Fact]
    public void GetCustomer_Valid() {
        _customerRepository.Setup(g => g.GetCustomer(It.IsAny<int>())).Returns((int i) => new Customer());
        var result = _customerController.GetCustomer(5);
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public void GetCustomer_Invalid() {
        _customerRepository.Setup(c => c.GetCustomer(It.IsAny<int>())).Returns((int i) => null);
        var result = _customerController.GetCustomer(5);
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public void AddCustomer_Valid() {
        CustomerInputDTO customer = new CustomerInputDTO
        { Name = "tester",
          PhoneNumber = "047878787",
          Email = "test@test.com",
          City = "testgemeente",
          PostalCode = "9800",
          Street = "testlaan",
          HouseNumber = "123" };
        _customerRepository.Setup(c => c.CustomerExists(It.IsAny<int>())).Returns((int i) => false);
        _customerRepository.Setup(c => c.AddCustomer(It.IsAny<Customer>())).Returns((Customer c) => c);
        var result = _customerController.AddCustomer(customer);
        Assert.IsType<CreatedAtActionResult>(result);
    }

    [Fact]
    public void AddCustomer_Invalid() {
        CustomerInputDTO customer = new CustomerInputDTO
        { Email = "test@test.com",
          City = "testgemeente",
          HouseNumber = "123",
          Name = "tester",
          PostalCode = "9800",
          Street = "testlaan",
          PhoneNumber = "0478787878" };
        _customerRepository.Setup(c => c.CustomerExists(It.IsAny<int>())).Returns((int i) => false);
        var result = _customerController.AddCustomer(customer);
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void UpdateCustomer_Valid() {
        CustomerInputDTO customer = new CustomerInputDTO
        { Email = "test@test.com",
          City = "testgemeente",
          HouseNumber = "123",
          Name = "tester",
          PostalCode = "9800",
          Street = "testlaan",
          PhoneNumber = "047878787" };
        _customerRepository.Setup(c => c.CustomerExists(It.IsAny<int>())).Returns((int i) => true);
        _customerRepository.Setup(c => c.UpdateCustomer(It.IsAny<Customer>())).Returns((Customer c) => c);
        _customerRepository.Setup(c => c.Equals(It.IsAny<Customer>())).Returns(false);
        var result = _customerController.Updatecustomer(1, customer);
        Assert.IsType<CreatedAtActionResult>(result);
    }

    [Fact]
    public void UpdateCustomer_Invalid() {
        CustomerInputDTO customer = new CustomerInputDTO
        { Email = "test@test.com",
          City = "testgemeente",
          HouseNumber = "123",
          Name = "tester",
          PostalCode = "9800",
          Street = "testlaan",
          PhoneNumber = "047878787" };
        _customerRepository.Setup(c => c.CustomerExists(It.IsAny<int>())).Returns((int i) => false);
        var result = _customerController.Updatecustomer(1, customer);
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public void DeleteCustomer_Valid() {
        _customerRepository.Setup(c => c.CustomerExists(It.IsAny<int>())).Returns((int i) => true);
        var result = _customerController.DeleteCustomer(5);
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void DeleteCustomer_Invalid() {
        _customerRepository.Setup(c => c.CustomerExists(It.IsAny<int>())).Returns((int i) => false);
        var result = _customerController.DeleteCustomer(5);
        Assert.IsType<NotFoundObjectResult>(result);
    }


    [Fact]
    public void GetReservation_Valid() {
        _reservationRepository.Setup(res => res.GetReservation(It.IsAny<int>())).Returns((int i) => new Reservation
        { Customer = new Customer(), Restaurant = new Restaurant() });
        _reservationRepository.Setup(res => res.ReservationExists(It.IsAny<int>())).Returns(true);
        var result = _customerController.GetReservation(5);
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public void GetReservation_Invalid() {
        _reservationRepository.Setup(r => r.GetReservation(It.IsAny<int>())).Returns((int i) => new Reservation
        { Customer = new Customer(), Restaurant = new Restaurant() });
        _reservationRepository.Setup(r => r.ReservationExists(It.IsAny<int>())).Returns(false);
        var result = _customerController.GetReservation(5);
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }


    [Fact]
    public void DeleteReservation_Valid() {
        _reservationRepository.Setup(r => r.ReservationExists(It.IsAny<int>())).Returns(true);
        _reservationRepository.Setup(r => r.GetReservation(It.IsAny<int>())).Returns(new Reservation
        { Date = DateTime.Now.AddDays(2) });
        var result = _customerController.DeleteReservation(5);
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void DeleteReservation_Invalid() {
        _reservationRepository.Setup(r => r.ReservationExists(It.IsAny<int>())).Returns(false);
        var result = _customerController.DeleteReservation(5);
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public void GetReservationForCustomerByDate_Valid() {
        _customerRepository.Setup(g => g.CustomerExists(It.IsAny<int>())).Returns(true);
        _customerRepository.Setup(g => g.GetCustomer(It.IsAny<int>())).Returns(new Customer(2, "Jane Doe",
            "0478000001", new Location("1000", "Brussels", "Main Street", "20"), "jane.doe@example.com"));

        _reservationRepository
            .Setup(r => r.GetReservationFromCustomerWithDate(It.IsAny<int>(), It.IsAny<DateTime>()))
            .Returns(new List<Reservation>());
        _reservationRepository
            .Setup(r =>
                r.GetReservationForCustomerWithDate(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .Returns(new List<Reservation>());
        _reservationRepository.Setup(res => res.GetReservationFromCustomer(It.IsAny<Customer>()))
            .Returns(new List<Reservation>());
        var result =
            _customerController.GetReservtionForCustomerByDate(1, DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(3));
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public void GetReservationForCustomerByDate_Invalid() {
        _customerRepository.Setup(c => c.CustomerExists(It.IsAny<int>())).Returns(false);
        var result =
            _customerController.GetReservtionForCustomerByDate(1, DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(3));
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }
}