namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerApplicationService _customerApplicationService;

        public CustomersController(ICustomerApplicationService customerApplicationService)
        {
            _customerApplicationService = customerApplicationService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] CustomerDto customerDto)
        {
            try
            {
                _customerApplicationService.SaveCustomer(customerDto);
                return Created("api/customers", customerDto);
            }
            catch (DuplicateEmailException e)
            {
                return StatusCode(StatusCodes.Status409Conflict, e.Message);
            }
            catch (ArgumentException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Put([FromBody] CustomerDto customerDto)
        {
            try
            {
                _customerApplicationService.UpdateCustomer(customerDto);
                return Ok(customerDto);
            }
            catch (ArgumentException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
        [HttpDelete("customers/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(string id)
        {
            try
            {
                var formattedId = Uri.UnescapeDataString(id);
                _customerApplicationService.DeleteCustomer(formattedId);
                return Ok();
            }
            catch (ArgumentException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetCustomers()
        {
            try
            {
                var customers = _customerApplicationService.GetCustomers();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        
        [HttpGet("customers/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetCustomerById(string id)
        {
            try
            {
                var formattedId = Uri.UnescapeDataString(id);
                var customers = _customerApplicationService.GetCustomerById(formattedId);
                if (customers == null)
                {
                    return NotFound();
                }
                return Ok(customers);
            }
            catch (ArgumentException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}