
namespace API.Controllers;

//  [Authorize]
public class CityController : BaseApiController
{
    public IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CityController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;

    }

    [HttpGet("GetById/{id}")]
    public IActionResult GetById(int id)
    {
        var City = _unitOfWork.CityRepository.GetByIdAsync(id);

        return (City != null) ? Ok(City) : BadRequest("This City Not Found !!");
    }
    [HttpGet("GetByParentId/{id}")]
        public async Task<IActionResult> GetByParentId(int id)
        {
            var cities=await _unitOfWork.CityRepository.FindAllAsync(c=>c.GovernorateId==id);

           return  (cities != null) ? Ok(cities): BadRequest("This Governorates Not Found !!") ;
        }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        
        return Ok(await _unitOfWork.CityRepository.FindAllAsync(x => x.Id > 0, w => w.Governorate.Country));

    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var City = _unitOfWork.CityRepository.GetById(id);

        if (City != null)
        {

            _unitOfWork.CityRepository.Delete(City);
            return (await _unitOfWork.Complete()) ? Ok("deleted successfully ...") : BadRequest("Failed to delete City !!");
        }

        return BadRequest("Failed to delete City !!");
    }

    [HttpPut]
    public async Task<ActionResult> Update(City city)
    {
        var City = _unitOfWork.CityRepository.GetById(city.Id);

        if (City != null)
        {

            City.Name = city.Name;

            _unitOfWork.CityRepository.Update(City);

            return (await _unitOfWork.Complete()) ? Ok("updated successfully ...") : BadRequest("Failed to update City !!");
        }

        return BadRequest("Failed to update City !!");

    }
    [HttpPost]
    public async Task<ActionResult> Create(City city)
    {
        _unitOfWork.CityRepository.Add(city);

        return (await _unitOfWork.Complete()) ? Ok("created successfully ...") : BadRequest("Failed to create City !!");
    }

}
